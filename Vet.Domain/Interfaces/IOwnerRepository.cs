using Vet.Domain.Entities;

namespace Vet.Domain.Interfaces;

/// <summary>
/// Репозиторий владельцев с аналитическими выборками
/// </summary>
public interface IOwnerRepository : IRepository<Owner, int>
{
    /// <summary>
    /// Асинхронно получает владельцев с более чем одним питомцем, упорядоченных по ФИО
    /// </summary>
    /// <returns>Список владельцев с несколькими питомцами</returns>
    public Task<IReadOnlyList<Owner>> GetWithMultiplePetsAsync();
}