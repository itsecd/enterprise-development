using Microsoft.EntityFrameworkCore;
using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;

namespace VetClinic.Infrastructure.SqlServer.Repositories;

/// <summary>
/// Репозиторий питомцев
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class PetRepository(VetClinicDbContext context) : EfRepository<Pet>(context), IPetRepository
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<Pet>> GetByVetOrderedByNicknameAsync(int vetId, CancellationToken cancellationToken = default) =>
        await Entities
            .AsNoTracking()
            .Where(p => Context.Appointments.Any(a => a.PetId == p.Id && a.VetId == vetId))
            .OrderBy(p => p.Nickname)
            .ThenBy(p => p.Id)
            .ToListAsync(cancellationToken);

    /// <inheritdoc/>
    public async Task<bool> AnyByBreedAsync(int breedId, CancellationToken cancellationToken = default) =>
        await Entities.AnyAsync(p => p.BreedId == breedId, cancellationToken);
}
