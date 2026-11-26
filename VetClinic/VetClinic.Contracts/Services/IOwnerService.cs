using VetClinic.Contracts.Dto.Owners;

namespace VetClinic.Contracts.Services;

/// <summary>
/// Сервис для работы с владельцами
/// </summary>
public interface IOwnerService : ICrudService<OwnerDto, OwnerEditDto>
{
}
