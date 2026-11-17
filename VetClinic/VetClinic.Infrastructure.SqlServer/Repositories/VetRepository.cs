using Microsoft.EntityFrameworkCore;
using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;
using VetClinic.Shared;

namespace VetClinic.Infrastructure.SqlServer.Repositories;

/// <summary>
/// Репозиторий ветеринаров
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class VetRepository(VetClinicDbContext context) : EfRepository<Vet>(context), IVetRepository
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<Vet>> GetBySpeciesAsync(AnimalSpecies species, CancellationToken cancellationToken = default) =>
        await Entities
            .AsNoTracking()
            .Where(v => v.Specialization != null && v.Specialization.FocusSpecies == species)
            .OrderBy(v => v.Id)
            .ToListAsync(cancellationToken);
}
