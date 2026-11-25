using CarWash.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace CarWash.Api.Errors;

/// <summary>
/// Обработка исключений приложения в стандартном формате Problem Details
/// </summary>
public sealed class ApplicationExceptionHandler(ILogger<ApplicationExceptionHandler> logger) : IExceptionHandler
{
    /// <inheritdoc />
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken cancellationToken)
    {
        var status = exception switch
        {
            EntityNotFoundException => StatusCodes.Status404NotFound,
            EntityConflictException => StatusCodes.Status409Conflict,
            RequestValidationException => StatusCodes.Status400BadRequest,
            _ => StatusCodes.Status500InternalServerError
        };

        if (status == StatusCodes.Status500InternalServerError)
            logger.LogError(exception, "Ошибка при обработке {Method} {Path}; TraceId: {TraceId}",
                context.Request.Method, context.Request.Path, context.TraceIdentifier);
        else
            logger.LogWarning("HTTP {Status}: {Message}; {Method} {Path}; TraceId: {TraceId}",
                status, exception.Message, context.Request.Method, context.Request.Path, context.TraceIdentifier);

        ProblemDetails problem = exception is RequestValidationException validation
            ? new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                [validation.Field] = [validation.Message]
            })
            : new ProblemDetails();
        problem.Status = status;
        problem.Detail = status == StatusCodes.Status500InternalServerError
            ? "Внутренняя ошибка сервера."
            : exception.Message;

        await Results.Problem(problem).ExecuteAsync(context);
        return true;
    }
}
