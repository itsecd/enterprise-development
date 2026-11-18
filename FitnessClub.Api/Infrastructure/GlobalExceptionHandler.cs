using Microsoft.AspNetCore.Diagnostics;

namespace FitnessClub.Api.Infrastructure;

/// <summary>
/// Глобальный обработчик необработанных исключений
/// </summary>
/// <param name="logger">Логгер</param>
public sealed class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    /// <summary>
    /// Обработка исключения
    /// </summary>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (statusCode, title) = exception switch
        {
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Запись не найдена"),
            ArgumentException => (StatusCodes.Status400BadRequest, "Некорректный запрос"),
            _ => (StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера"),
        };

        if (statusCode == StatusCodes.Status500InternalServerError)
        {
            logger.LogError(exception, "Необработанное исключение при обработке запроса {Method} {Path}",
                httpContext.Request.Method, httpContext.Request.Path);
        }
        else
        {
            logger.LogWarning("Ошибка {StatusCode} при обработке запроса {Method} {Path}: {Message}",
                statusCode, httpContext.Request.Method, httpContext.Request.Path, exception.Message);
        }

        await Results.Problem(
            title: title,
            detail: statusCode < 500 ? exception.Message : null,
            statusCode: statusCode)
            .ExecuteAsync(httpContext);

        return true;
    }
}