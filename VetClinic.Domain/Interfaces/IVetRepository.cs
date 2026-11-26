using VetClinic.Domain.Entities;
using VetClinic.Domain.Shared.Enums;

namespace VetClinic.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория врачей
/// </summary>
public interface IVetRepository : IRepository<Vet>
{
    /// <summary>
    /// Поиск врачей, специализирующихся на конкретных видах животных 
    /// </summary>
    /// <param name="species">Целевой вид животного</param>
    /// <returns>Список врачей</returns>
    public Task<List<Vet>> FindVetsBySpecies(Species species);
}
