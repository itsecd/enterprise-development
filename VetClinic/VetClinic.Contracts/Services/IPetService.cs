using VetClinic.Contracts.Dto.Pets;

namespace VetClinic.Contracts.Services;

/// <summary>
/// Сервис для работы с питомцами
/// </summary>
public interface IPetService : ICrudService<PetDto, PetEditDto>
{
}
