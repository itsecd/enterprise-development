using Vet.Domain.Entities;

namespace Vet.Domain.Interfaces;

/// <summary>
/// Репозиторий питомцев с дополнительными выборками
/// </summary>
public interface IPetRepository : IRepository<Pet, int>
{
    /// <summary>
    /// Асинхронно проверяет, существуют ли питомцы с указанной породой
    /// </summary>
    /// <param name="breedId">Идентификатор породы</param>
    /// <returns>True, если есть хотя бы один питомец данной породы; иначе False</returns>
    public Task<bool> AnyByBreedIdAsync(int breedId);
}