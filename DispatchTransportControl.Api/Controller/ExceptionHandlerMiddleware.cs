using System.Net;
using System.Text.Json;

namespace DispatchTransportControl.Api.Controller;

/// <summary>
///     Класс для обработки исключений
/// </summary>
public class ExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
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

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;

        var result = JsonSerializer.Serialize(new { error = exception.Message });
        return context.Response.WriteAsync(result);
    }
}