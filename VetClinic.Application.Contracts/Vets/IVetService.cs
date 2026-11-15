using VetClinic.Domain.Shared.Enums;

namespace VetClinic.Application.Contracts.Vets;

/// <summary>
/// Интерфейс прикладного сервиса для управления кадровым составом ветеринарных врачей
/// </summary>
public interface IVetAppService : IApplicationService<VetDto, VetCreateUpdateDto, int>
{
    /// <summary>
    /// Возвращает список ветеринарных врачей, специализирующихся на лечении указанного вида животных
    /// </summary>
    /// <param name="species">Целевой вид животного (например, Cat, Dog)</param>
    /// <returns>Список подходящих специалистов в виде DTO</returns>
    public Task<List<VetDto>> GetVetsBySpecies(Species species);
}
