using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
using Microsoft.AspNetCore.Diagnostics;

namespace VeterinaryClinic.Api;

/// <summary>
/// Обрабатывает исключения API и возвращает соответствующий HTTP-ответ
/// </summary>
public sealed class ApiExceptionHandler(ILogger<ApiExceptionHandler> logger) : IExceptionHandler
{
    /// <inheritdoc />
    public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception,
        CancellationToken cancellationToken)
    {
        var status = exception switch
        {
            KeyNotFoundException => StatusCodes.Status404NotFound,
            ArgumentException => StatusCodes.Status400BadRequest,
            InvalidOperationException => StatusCodes.Status409Conflict,
            _ => StatusCodes.Status500InternalServerError
        };
        if (status == 500) logger.LogError(exception, "Необработанная ошибка API");
        else logger.LogWarning(exception, "Запрос отклонён с кодом {Status}", status);
        context.Response.StatusCode = status;
        await Results.Problem(statusCode: status, title: status == 500
            ? "Внутренняя ошибка сервера" : exception.Message).ExecuteAsync(context);
        return true;
    }
}
