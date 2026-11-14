using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Vet.Api;

/// <summary>
/// Глобальный обработчик исключений
/// </summary>
/// <param name="logger">Логгер обработчика исключений</param>
public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    /// <inheritdoc />
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, title, detail) = exception switch
        {
            KeyNotFoundException notFound => (
                StatusCodes.Status404NotFound,
                "Ресурс не найден",
                notFound.Message),

            ArgumentException badRequest => (
                StatusCodes.Status400BadRequest,
                "Некорректный запрос",
                badRequest.Message),

            InvalidOperationException conflict => (
                StatusCodes.Status409Conflict,
                "Конфликт бизнес-логики",
                conflict.Message),

            _ => (
                StatusCodes.Status500InternalServerError,
                "Внутренняя ошибка сервера",
                "Произошла непредвиденная ошибка")
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            logger.LogError(exception, "Необработанное исключение: {Message}", exception.Message);
        }
        else
        {
            logger.LogWarning("Бизнес-исключение ({StatusCode}): {Message}", statusCode, exception.Message);
        }

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = httpContext.Request.Path
        };

        httpContext.Response.StatusCode = statusCode;
        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}