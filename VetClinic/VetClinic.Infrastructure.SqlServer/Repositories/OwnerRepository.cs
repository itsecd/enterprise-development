using Microsoft.EntityFrameworkCore;
using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;

namespace VetClinic.Infrastructure.SqlServer.Repositories;

/// <summary>
/// Репозиторий владельцев
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class OwnerRepository(VetClinicDbContext context) : EfRepository<Owner>(context), IOwnerRepository
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<OwnerPetsCount>> GetWithMultiplePetsOrderedByFullNameAsync(CancellationToken cancellationToken = default) =>
        await Entities
            .Where(o => o.Pets.Count > 1)
            .OrderBy(o => o.FullName)
            .ThenBy(o => o.Id)
            .Select(o => new OwnerPetsCount(o.Id, o.FullName, o.Pets.Count))
            .ToListAsync(cancellationToken);
}
