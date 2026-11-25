using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BeautySalon.Infrastructure.SqlServer;

internal static class ScheduleLock
{
    internal static async Task<IDbContextTransaction> LockScheduleAsync(
        this SalonDbContext context, CancellationToken cancellationToken)
    {
        var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        try
        {
            // Блокировка живёт до завершения транзакции и общая для всех экземпляров API
            await context.Database.ExecuteSqlRawAsync(
                """
                DECLARE @result int;
                EXEC @result = sys.sp_getapplock
                    @Resource = 'BeautySalon.Schedule',
                    @LockMode = 'Exclusive',
                    @LockOwner = 'Transaction',
                    @LockTimeout = 10000;
                IF @result < 0
                    THROW 51000, 'Could not lock the salon schedule', 1;
                """,
                cancellationToken);
            return transaction;
        }
        catch (SqlException exception) when (exception.Number == 51000)
        {
            await transaction.DisposeAsync();
            throw new InvalidOperationException("Расписание занято другим запросом, повторите попытку", exception);
        }
        catch
        {
            await transaction.DisposeAsync();
            throw;
        }
    }
}
