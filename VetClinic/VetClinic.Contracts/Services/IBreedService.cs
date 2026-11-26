using VetClinic.Contracts.Dto.Breeds;

namespace VetClinic.Contracts.Services;

/// <summary>
/// Сервис для работы с породами
/// </summary>
public interface IBreedService : ICrudService<BreedDto, BreedEditDto>
{
}
