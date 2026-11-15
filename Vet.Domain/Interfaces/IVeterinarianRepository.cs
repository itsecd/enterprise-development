using Vet.Domain.Entities;
using Vet.Domain.Enums;

namespace Vet.Domain.Interfaces;

/// <summary>
/// Репозиторий ветеринаров с аналитическими выборками
/// </summary>
public interface IVeterinarianRepository : IRepository<Veterinarian, int>
{
    /// <summary>
    /// Получить ветеринаров по специализации на виде животного, упорядоченных по ФИО
    /// </summary>
    /// <param name="species">Вид животного</param>
    /// <returns>Список ветеринаров</returns>
    public Task<IReadOnlyList<Veterinarian>> GetBySpeciesAsync(Species species);

    /// <summary>
    /// Проверяет, назначена ли специализация хотя бы одному ветеринару
    /// </summary>
    /// <param name="specializationId">Идентификатор специализации</param>
    /// <returns>True, если да; False иначе</returns>
    public Task<bool> AnyBySpecializationIdAsync(int specializationId);
}