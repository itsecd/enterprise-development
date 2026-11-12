using BeautySalon.Contracts.Specialists;

namespace BeautySalon.Contracts.Interfaces;

/// <summary>
/// Контракт сервиса SpecialistService
/// </summary>
public interface ISpecialistService : ICrudService<SaveSpecialistRequest, SpecialistResponse>
{
}
