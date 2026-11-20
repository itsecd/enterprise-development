using Microsoft.EntityFrameworkCore;
using VeterinaryClinic.Application.Services;
using VeterinaryClinic.Domain.Models;

namespace VeterinaryClinic.Infrastructure.PostgreSQL;

public sealed class PostgreSqlOwnerAnalytics(ClinicDbContext db) : IOwnerAnalytics
{
    public async Task<IReadOnlyList<Owner>> OwnersWithMultiplePetsAsync(CancellationToken cancellationToken = default)
        => await db.Owners.AsNoTracking()
            .Where(o => o.Pets.Count > 1)
            .OrderBy(o => o.LastName).ThenBy(o => o.FirstName).ThenBy(o => o.Patronymic)
            .ToListAsync(cancellationToken);
}
