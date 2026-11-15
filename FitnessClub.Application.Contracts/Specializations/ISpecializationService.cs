using FitnessClub.Application.Contracts.Trainers;

namespace FitnessClub.Application.Contracts.Specializations;

/// <summary>
/// Контракт для работы со специализациями
/// </summary>
public interface ISpecializationService:
    IApplicationService<
    SpecializationDto,
    SpecializationCreateUpdateDto,
    int>
{
    /// <summary>
    /// Получает тренеров выбранной специализации
    /// </summary>
    Task<IList<TrainerDto>> GetTrainers(int id);
}