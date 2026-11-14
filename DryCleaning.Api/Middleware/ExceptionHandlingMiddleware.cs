using System.ComponentModel.DataAnnotations;
using DryCleaning.Contracts.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace DryCleaning.Api.Middleware;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try { await next(context); }
        catch (ValidationException ex)
        {
            logger.LogWarning(ex, "Ошибка валидации");
            await Write(context, StatusCodes.Status400BadRequest, "Ошибка валидации", ex.Message);
        }
        catch (ClientHasOrdersException ex)
        {
            logger.LogWarning(ex, "Конфликт при удалении");
            await Write(context, StatusCodes.Status409Conflict, "Конфликт", "У клиента есть заказы");
        }
        catch (CategoryHasItemsException ex)
        {
            logger.LogWarning(ex, "Конфликт при удалении");
            await Write(context, StatusCodes.Status409Conflict, "Конфликт", "Категория используется изделиями");
        }
        catch (ItemHasOrdersException ex)
        {
            logger.LogWarning(ex, "Конфликт при удалении");
            await Write(context, StatusCodes.Status409Conflict, "Конфликт", "С изделием связаны заказы");
        }
        catch (ArgumentException ex)
        {
            logger.LogWarning(ex, "Некорректный аргумент");
            await Write(context, StatusCodes.Status400BadRequest, "Некорректный аргумент", ex.Message);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Внутренняя ошибка");
            await Write(context, StatusCodes.Status500InternalServerError, "Внутренняя ошибка сервера", null);
        }
    }

    private static async Task Write(HttpContext context, int status, string title, string? detail)
    {
        context.Response.StatusCode = status;
        await context.Response.WriteAsJsonAsync(new ProblemDetails { Status = status, Title = title, Detail = detail });
    }
}
