using System.Net;
using System.Text.Json;

namespace AutoService.Api.Middlewares;

/// <summary>
/// Middleware для глобальной обработки исключений и логирования.
/// </summary>
public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Произошла непредвиденная ошибка: {Message}", ex.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new
            {
                error = "Внутренняя ошибка сервера",
                details = ex.Message
            };
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}