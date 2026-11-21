using System.Text.Json;
using FoodDelivery.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;

namespace FoodDelivery.Api.ErrorHandling;

/// <summary>
/// Преобразует исключения приложения в HTTP-ответы
/// </summary>
/// <param name="logger">Средство журналирования</param>
/// <param name="problemDetailsService">Служба формирования ответов об ошибках</param>
public sealed class ApiExceptionHandler(
    ILogger<ApiExceptionHandler> logger,
    IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    private static readonly JsonSerializerOptions JsonOptions =
        new(JsonSerializerDefaults.Web);

    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, title) = exception switch
        {
            ResourceNotFoundException => (
                StatusCodes.Status404NotFound,
                "Ресурс не найден"),
            MySqlException { Number: 1205 or 1213 } or
            DbUpdateException { InnerException: MySqlException { Number: 1205 or 1213 } } => (
                StatusCodes.Status409Conflict,
                "Данные изменяются другим запросом"),
            DbUpdateException { InnerException: MySqlException { Number: 1451 or 1452 } } => (
                StatusCodes.Status409Conflict,
                "Операция нарушает связь между сущностями"),
            ResourceConflictException => (
                StatusCodes.Status409Conflict,
                "Конфликт данных"),
            InvalidReferenceException => (
                StatusCodes.Status422UnprocessableEntity,
                "Некорректная связь между сущностями"),
            _ => (
                StatusCodes.Status500InternalServerError,
                "Внутренняя ошибка сервера")
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            logger.LogError(
                exception,
                "Ошибка при выполнении {Method} {Path}",
                httpContext.Request.Method,
                httpContext.Request.Path);
        }
        else
        {
            logger.LogWarning(
                exception,
                "Отклонён запрос {Method} {Path}, код ответа: {StatusCode}",
                httpContext.Request.Method,
                httpContext.Request.Path,
                statusCode);
        }

        var problem = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = exception is DbUpdateException or MySqlException || statusCode == StatusCodes.Status500InternalServerError
                ? null
                : exception.Message,
            Instance = httpContext.Request.Path.Value
        };

        httpContext.Response.StatusCode = statusCode;

        var written = await problemDetailsService.TryWriteAsync(
            new ProblemDetailsContext
            {
                HttpContext = httpContext,
                ProblemDetails = problem
            });

        if (!written)
        {
            httpContext.Response.ContentType = "application/problem+json";

            await JsonSerializer.SerializeAsync(
                httpContext.Response.Body,
                problem,
                JsonOptions,
                cancellationToken);
        }

        return true;
    }
}
