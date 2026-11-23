using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Hotel.WebApi.Middleware;

/// <summary>
/// Middleware для перехвата ошибок и корректного ответа пользователю
/// </summary>
/// <param name="next">следующий обработчик</param>
/// <param name="logger">Логгер</param>
public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    /// <summary>
    /// Передаём запрос дальше, если ошибка - обрабатываем
    /// </summary>
    /// <param name="context">Запрос</param>
    /// <returns></returns>
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        if (context.Response.HasStarted) return;

        context.Response.ContentType = "application/json";
        int statusCode;
        string title;
        var detail = exception.Message;

        switch (exception)
        {
            case ArgumentException or InvalidOperationException:
                statusCode = (int)HttpStatusCode.BadRequest;
                title = "Ошибка в запросе";
                logger.LogWarning(exception, "Ошибка валидации: {Message}", exception.Message);
                break;

            default:
                statusCode = (int)HttpStatusCode.InternalServerError;
                title = "Внутренняя ошибка сервера";
                detail = "Произошла непредвиденная ошибка.";
                logger.LogError(exception, "Необработанное исключение: {Message}", exception.Message);
                break;
        }

        context.Response.StatusCode = statusCode;

        var problemDetails = new ProblemDetails
        {
            Status = statusCode,
            Title = title,
            Detail = detail,
            Instance = context.Request.Path
        };

        var jsonOptions = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
        await context.Response.WriteAsJsonAsync(problemDetails, jsonOptions);
    }
}