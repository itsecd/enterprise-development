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
}
