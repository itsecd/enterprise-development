using VetClinic.Contracts.Dto.Vets;

namespace VetClinic.Contracts.Services;

/// <summary>
/// Сервис для работы с ветеринарами
/// </summary>
public interface IVetService : ICrudService<VetDto, VetEditDto>
{
}
