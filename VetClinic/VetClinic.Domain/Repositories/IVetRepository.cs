using VetClinic.Domain.Models;
using VetClinic.Shared;

namespace VetClinic.Domain.Repositories;

/// <summary>
/// Репозиторий ветеринаров
/// </summary>
public interface IVetRepository : IRepository<Vet>
{
    /// <summary>
    /// Ветеринары, специализирующиеся на выбранном виде животных
    /// </summary>
    /// <param name="species">Вид животного</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<IReadOnlyList<Vet>> GetBySpeciesAsync(AnimalSpecies species, CancellationToken cancellationToken = default);
}
