using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VetClinic.Application.Exceptions;

namespace VetClinic.Api.Middleware;

/// <summary>
/// Возвращает 400, если указанная порода относится к другому виду
/// </summary>
public class InvalidPetBreedExceptionHandler(
    IProblemDetailsService problemDetails,
    ILogger<InvalidPetBreedExceptionHandler> logger) : IExceptionHandler
{
    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        if (exception is not InvalidPetBreedException)
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
