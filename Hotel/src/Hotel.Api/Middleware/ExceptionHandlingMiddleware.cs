using System.ComponentModel.DataAnnotations;
using Hotel.Contracts.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.Api.Middleware;

/// <summary>Преобразует ошибки приложения в стандартные HTTP-ответы.</summary>
/// <param name="next">Следующий компонент конвейера.</param>
/// <param name="logger">Журнал ошибок.</param>
public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    /// <summary>Выполняет следующий компонент и обрабатывает возникшие исключения.</summary>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException exception)
        {
            _logger.LogWarning(exception, "Ошибка проверки данных при запросе {Method} {Path}",
                context.Request.Method, context.Request.Path);

            var details = new ValidationProblemDetails(
                new Dictionary<string, string[]> { ["request"] = [exception.Message] })
            {
                Status = StatusCodes.Status400BadRequest,
                Title = "Некорректные данные запроса",
                Instance = context.Request.Path.ToString()
            };

            context.Response.StatusCode = StatusCodes.Status400BadRequest;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(details);
        }
        catch (ResourceConflictException exception)
        {
            _logger.LogWarning(exception, "Конфликт данных при запросе {Method} {Path}",
                context.Request.Method, context.Request.Path);

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Конфликт данных",
                Detail = exception.Message,
                Instance = context.Request.Path.ToString()
            };

            context.Response.StatusCode = StatusCodes.Status409Conflict;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(details);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Ошибка сервера при запросе {Method} {Path}",
                context.Request.Method, context.Request.Path);

            var details = new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Внутренняя ошибка сервера",
                Detail = "Во время обработки запроса произошла непредвиденная ошибка.",
                Instance = context.Request.Path.ToString()
            };

            context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            context.Response.ContentType = "application/problem+json";
            await context.Response.WriteAsJsonAsync(details);
        }
    }
}
