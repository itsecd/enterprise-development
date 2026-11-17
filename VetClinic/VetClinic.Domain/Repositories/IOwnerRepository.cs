using VetClinic.Domain.Models;

namespace VetClinic.Domain.Repositories;

/// <summary>
/// Репозиторий владельцев
/// </summary>
public interface IOwnerRepository : IRepository<Owner>
{
    /// <summary>
    /// Владельцы, у которых больше одного питомца, по ФИО, вместе с питомцами
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<IReadOnlyList<Owner>> GetWithMultiplePetsOrderedByFullNameAsync(CancellationToken cancellationToken = default);
}
