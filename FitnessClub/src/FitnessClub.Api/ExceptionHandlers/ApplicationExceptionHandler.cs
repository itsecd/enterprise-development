using FitnessClub.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.ExceptionHandlers;

/// <summary>
/// Превращает исключения приложения в HTTP-ответы
/// </summary>
public class ApplicationExceptionHandler(ILogger<ApplicationExceptionHandler> logger) : IExceptionHandler
{
    /// <summary>
    /// Обработать исключение и сформировать HTTP-ответ
    /// </summary>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (status, title) = exception switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, "Ресурс не найден"),
            ConflictException => (StatusCodes.Status409Conflict, "Конфликт данных"),
            _ => (0, string.Empty)
        };
        if (status == 0)
        {
            logger.LogError(exception, "Необработанная ошибка при обработке HTTP-запроса");
            return false;
        }
        httpContext.Response.StatusCode = status;
        await httpContext.Response.WriteAsJsonAsync(
            new ProblemDetails { Status = status, Title = title, Detail = exception.Message },
            cancellationToken);
        return true;
    }
}