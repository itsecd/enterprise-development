using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VetClinic.Api.Middleware;

/// <summary>
/// Превращает <see cref="DbUpdateException"/> в ответ 409, например при повторе уникального номера паспорта
/// </summary>
/// <param name="problemDetails">Сервис формирования ProblemDetails</param>
/// <param name="logger">Логгер</param>
public class DbUpdateExceptionHandler(
    IProblemDetailsService problemDetails,
    ILogger<DbUpdateExceptionHandler> logger) : IExceptionHandler
{
    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not DbUpdateException)
        {
            return false;
        }

        logger.LogWarning(exception, "Изменение отклонено базой данных");

        httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
        return await problemDetails.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Конфликт данных",
                Detail = "Изменение нарушает ограничения базы данных, например уникальность номера паспорта"
            }
        });
    }
}
