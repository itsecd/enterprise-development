using VetClinic.Domain.Models;

namespace VetClinic.Domain.Repositories;

/// <summary>
/// Репозиторий питомцев
/// </summary>
public interface IPetRepository : IRepository<Pet>
{
    /// <summary>
    /// Питомцы, записанные на прием к врачу, по кличке
    /// </summary>
    /// <param name="vetId">Идентификатор врача</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<IReadOnlyList<Pet>> GetByVetOrderedByNicknameAsync(int vetId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Есть ли питомцы указанной породы
    /// </summary>
    /// <param name="breedId">Идентификатор породы</param>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<bool> AnyByBreedAsync(int breedId, CancellationToken cancellationToken = default);
}
