using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.API.Infrastructure;

/// <summary>
/// Обработчик исключений для безопасного отображения ошибки
/// </summary>
public class ApiExceptionHandler(ILogger<ApiExceptionHandler> logger) : IExceptionHandler
{
    /// <summary>
    /// Обрабатывает исключение и выводит структуру ошибки в json
    /// </summary>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        int status;
        string title;

        if (exception is KeyNotFoundException)
        {
            status = StatusCodes.Status404NotFound;
            title = "Ресурс не найден";
        }
        else if (exception is ArgumentException)
        {
            status = StatusCodes.Status400BadRequest;
            title = "Некорректные данные";
        }
        else
        {
            status = StatusCodes.Status500InternalServerError;
            title = "Ошибка сервера";
        }

        var detail = string.Empty;

        if (status >= 500 && status <= 599)
        {
            logger.LogError(exception, "Ошибка сервера: {Method} {Path}", httpContext.Request.Method, httpContext.Request.Path);
            detail = "Рекомендуем обратиться в поддержку";
        }
        else if (status >= 400 && status <= 499)
        {
            logger.LogWarning(exception, "Ошибка клиента: {Method} {Path}", httpContext.Request.Method, httpContext.Request.Path);
            detail = exception.Message;
        }

        var problem = new ProblemDetails
        {
            Status = status,
            Title = title,
            Instance = httpContext.Request.Path,
            Detail = detail
        };

        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(problem, options: null, contentType: "application/problem+json", cancellationToken: cancellationToken);
        return true;
    }
}