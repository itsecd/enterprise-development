using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace VetClinic.Api.Middleware;

/// <summary>
/// Возвращает 409 при конфликте параллельной записи или нарушении известного ограничения SQL Server
/// </summary>
public class DbUpdateExceptionHandler(
    IProblemDetailsService problemDetails,
    ILogger<DbUpdateExceptionHandler> logger) : IExceptionHandler
{
    private const int ConstraintViolation = 547;
    private const int DuplicateUniqueIndexKey = 2601;
    private const int DuplicateConstraintKey = 2627;

    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var detail = exception switch
        {
            DbUpdateConcurrencyException =>
                "Запись была изменена или удалена. Обновите данные и повторите запрос.",
            DbUpdateException { InnerException: SqlException { Number: DuplicateUniqueIndexKey or DuplicateConstraintKey } } =>
                "Запись с таким уникальным значением уже существует.",
            DbUpdateException { InnerException: SqlException { Number: ConstraintViolation } } =>
                "Изменение нарушает целостность данных.",
            _ => null
        };

        if (detail is null)
        {
            return false;
        }

        logger.LogWarning("Изменение отклонено базой данных: {Detail}", detail);

        httpContext.Response.StatusCode = StatusCodes.Status409Conflict;
        return await problemDetails.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails = new ProblemDetails
            {
                Status = StatusCodes.Status409Conflict,
                Title = "Конфликт данных",
                Detail = detail
            }
        });
    }
}
