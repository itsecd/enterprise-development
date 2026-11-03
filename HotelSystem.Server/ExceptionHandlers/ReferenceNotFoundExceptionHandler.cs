using HotelSystem.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HotelSystem.Server.ExceptionHandlers;

/// <summary>
/// Преобразует обращение к несуществующей связанной сущности в ответ 422
/// </summary>
public class ReferenceNotFoundExceptionHandler(
    IProblemDetailsService problemDetailsService,
    ILogger<ReferenceNotFoundExceptionHandler> logger) : IExceptionHandler
{
    /// <inheritdoc />
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        if (exception is not ReferenceNotFoundException)
        {
            return false;
        }

        logger.LogWarning("Запрос отклонён: {Message}", exception.Message);

        httpContext.Response.StatusCode = StatusCodes.Status422UnprocessableEntity;

        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status422UnprocessableEntity,
                Title = "Связанная сущность не найдена",
                Detail = exception.Message
            }
        });
    }
}