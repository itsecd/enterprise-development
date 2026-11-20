using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace VetClinic.Api.Middleware;

/// <summary>
/// Превращает <see cref="KeyNotFoundException"/> из сервисов в ответ 404 с ProblemDetails
/// </summary>
/// <param name="problemDetails">Сервис формирования ProblemDetails</param>
/// <param name="logger">Логгер</param>
public class KeyNotFoundExceptionHandler(
    IProblemDetailsService problemDetails,
    ILogger<KeyNotFoundExceptionHandler> logger) : IExceptionHandler
{
    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not KeyNotFoundException)
        {
            return false;
        }

        logger.LogWarning("Связанная сущность не найдена: {Message}", exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
        return await problemDetails.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Связанная сущность не найдена",
                Detail = exception.Message
            }
        });
    }
}
