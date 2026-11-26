using VetClinic.Contracts.Dto.Specializations;

namespace VetClinic.Contracts.Services;

/// <summary>
/// Сервис для работы с специализациями врачей
/// </summary>
public interface ISpecializationService : ICrudService<SpecializationDto, SpecializationEditDto>
{
}
