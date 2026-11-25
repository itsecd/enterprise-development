using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.SqlServer.Repositories;

public sealed class ServiceRepository(SalonDbContext context)
    : SqlRepository<Service>(context), IServiceRepository
{
    public override async Task<bool> UpdateAsync(
        Service entity, CancellationToken cancellationToken = default)
    {
        await using var transaction = await Context.LockScheduleAsync(cancellationToken);

        var duration = await ReadQuery.Where(item => item.Id == entity.Id)
            .Select(item => (int?)item.DurationMinutes)
            .SingleOrDefaultAsync(cancellationToken);

        if (duration is null)
        {
            return false;
        }

        if (duration != entity.DurationMinutes &&
            await Context.Appointments.AnyAsync(item => EF.Property<int>(item, "ServiceId") == entity.Id, cancellationToken))
        {
            throw new InvalidOperationException("Нельзя менять длительность услуги с существующими записями");
        }

        var updated = await base.UpdateAsync(entity, cancellationToken);
        await transaction.CommitAsync(cancellationToken);
        return updated;
    }
}
