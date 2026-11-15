using BeautySalon.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Api.ExceptionHandlers;

/// <summary>
/// Тексты внутренних ошибок клиенту не отдаются: неизвестные исключения обрабатывает стандартный обработчик (ответ 500).
/// </summary>
public class ApplicationExceptionHandler(ILogger<ApplicationExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (status, title) = exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, "Запись не найдена"),
            ReferenceNotFoundException => (StatusCodes.Status400BadRequest, "Некорректные данные"),
            _ => (0, string.Empty)
        };

        if (status == 0)
        {
            return false;
        }

        logger.LogWarning(exception, "Запрос завершился ошибкой {Status}: {Message}", status, exception.Message);

        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(
            new ProblemDetails { Status = status, Title = title, Detail = exception.Message },
            cancellationToken);
        return true;
    }
}