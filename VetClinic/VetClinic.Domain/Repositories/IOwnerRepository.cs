using VetClinic.Domain.Models;

namespace VetClinic.Domain.Repositories;

/// <summary>
/// Репозиторий владельцев
/// </summary>
public interface IOwnerRepository : IRepository<Owner>
{
    /// <summary>
    /// Владельцы с несколькими питомцами, отсортированные по ФИО
    /// </summary>
    /// <param name="cancellationToken">Токен отмены</param>
    public Task<IReadOnlyList<OwnerPetsCount>> GetWithMultiplePetsOrderedByFullNameAsync(CancellationToken cancellationToken = default);
}
