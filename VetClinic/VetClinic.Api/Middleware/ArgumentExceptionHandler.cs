using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace VetClinic.Api.Middleware;

/// <summary>
/// Превращает <see cref="ArgumentException"/> из сервисов в ответ 400 с ProblemDetails
/// </summary>
/// <param name="problemDetails">Сервис формирования ProblemDetails</param>
/// <param name="logger">Логгер</param>
public class ArgumentExceptionHandler(
    IProblemDetailsService problemDetails,
    ILogger<ArgumentExceptionHandler> logger) : IExceptionHandler
{
    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not ArgumentException)
        {
            return false;
        }

        logger.LogWarning("Некорректные данные: {Message}", exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
        return await problemDetails.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Некорректные данные",
                Detail = exception.Message
            }
        });
    }
}
