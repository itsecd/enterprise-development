using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.ErrorHandling;

/// <summary>
/// Преобразование ошибок приложения в HTTP ответы
/// </summary>
/// <param name="logger">Журнал ошибок</param>
public sealed class ApiExceptionHandler(
    ILogger<ApiExceptionHandler> logger) : IExceptionHandler
{
    /// <inheritdoc />
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var status = exception switch
        {
            ArgumentException => StatusCodes.Status400BadRequest,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            InvalidOperationException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };

        var title = status switch
        {
            400 => "Некорректные данные",
            404 => "Объект не найден",
            409 => "Операция конфликтует с текущими данными",
            _ => "Внутренняя ошибка сервера"
        };

        if (status == StatusCodes.Status500InternalServerError)
        {
            logger.LogError(exception,
                "Ошибка обработки запроса {Method} {Path}",
                httpContext.Request.Method, httpContext.Request.Path);
        }

        httpContext.Response.StatusCode = status;

        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Detail = status == StatusCodes.Status500InternalServerError
                ? "Не удалось выполнить запрос"
                : exception.Message,
            Instance = httpContext.Request.Path.Value
        };
        problem.Extensions["traceId"] = httpContext.TraceIdentifier;

        await httpContext.Response.WriteAsJsonAsync(
            problem,
            options: null,
            contentType: "application/problem+json",
            cancellationToken: cancellationToken);

        return true;
    }
}