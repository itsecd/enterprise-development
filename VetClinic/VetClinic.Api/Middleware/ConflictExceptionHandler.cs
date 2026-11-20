using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VetClinic.Application.Exceptions;

namespace VetClinic.Api.Middleware;

/// <summary>
/// Превращает <see cref="ConflictException"/> из сервисов в ответ 409 с ProblemDetails
/// </summary>
/// <param name="problemDetails">Сервис формирования ProblemDetails</param>
/// <param name="logger">Логгер</param>
public class ConflictExceptionHandler(
    IProblemDetailsService problemDetails,
    ILogger<ConflictExceptionHandler> logger) : IExceptionHandler
{
    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ConflictException)
        {
            return false;
        }

        logger.LogWarning("Конфликт данных: {Message}", exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
        return await problemDetails.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Конфликт данных",
                Detail = exception.Message
            }
        });
    }
}
