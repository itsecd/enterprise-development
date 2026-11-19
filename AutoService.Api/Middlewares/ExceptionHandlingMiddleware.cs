using System.Net;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

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
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Запрашиваемый ресурс не найден: {Message}", ex.Message);
            await WriteErrorAsync(context, HttpStatusCode.NotFound, ex.Message);
        }
        catch (DbUpdateException ex)
        {
            logger.LogWarning(ex, "Изменение данных нарушает ограничения базы данных");
            await WriteErrorAsync(
                context,
                HttpStatusCode.Conflict,
                "Операция нарушает ограничения базы данных. Проверьте связанные записи и уникальность данных");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Произошла непредвиденная ошибка: {Message}", ex.Message);
            await WriteErrorAsync(context, HttpStatusCode.InternalServerError, "Внутренняя ошибка сервера");
        }
    }

    /// <summary>
    /// Записывает сообщение об ошибке в HTTP-ответ.
    /// </summary>
    private static async Task WriteErrorAsync(HttpContext context, HttpStatusCode statusCode, string message)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(new { error = message }));
    }
}
