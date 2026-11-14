using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace FitnessClub.Api;

/// <summary>Преобразует конфликты данных в HTTP 409 без раскрытия деталей БД.</summary>
public sealed class DatabaseExceptionHandler(ILogger<DatabaseExceptionHandler> logger) : IExceptionHandler
{
    /// <inheritdoc />
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var title = exception switch
        {
            DbUpdateConcurrencyException => "Запись была изменена или удалена другим запросом.",
            DbUpdateException { InnerException: PostgresException { SqlState: PostgresErrorCodes.ForeignKeyViolation } }
                => "Операция нарушает связь между записями.",
            DbUpdateException { InnerException: PostgresException { SqlState: PostgresErrorCodes.UniqueViolation } }
                => "Запись с такими уникальными данными уже существует.",
            _ => null
        };

        if (title is null)
        {
            return false;
        }

        logger.LogWarning(exception, "Конфликт данных при запросе {Path}.", httpContext.Request.Path);
        httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
        await httpContext.Response.WriteAsJsonAsync(new ProblemDetails
        {
            Status = StatusCodes.Status409Conflict,
            Title = title
        }, cancellationToken);
        return true;
    }
}
