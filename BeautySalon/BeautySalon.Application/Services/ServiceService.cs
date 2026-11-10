using BeautySalon.Application.Contracts;
using BeautySalon.Application.Contracts.Services;
using BeautySalon.Domain;
using BeautySalon.Domain.Entities;

namespace BeautySalon.Application.Services;

/// <summary>
/// Служба для CRUD операций c услугами
/// </summary>
/// <param name="serviceRepository">Репозиторий услуг</param>
public class ServiceService(IRepository<Service, int> serviceRepository) : IApplicationService<ServiceDto, ServiceCreateUpdateDto, int>
{
    /// <inheritdoc/>
    public async Task<ServiceDto> Create(ServiceCreateUpdateDto dto)
    {
        var service = Mapper.Map(dto);

        var result = await serviceRepository.Create(service);

        return Mapper.Map(result);
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int id) => await serviceRepository.Delete(id);

    /// <inheritdoc/>
    public async Task<ServiceDto?> Get(int id)
    {
        var service = await serviceRepository.Read(id);

        if (service == null)
            return null;

        return Mapper.Map(service);
    }

    /// <inheritdoc/>
    public async Task<IList<ServiceDto>> GetAll() => Mapper.Map(await serviceRepository.ReadAll());

    /// <inheritdoc/>
    public async Task<ServiceDto> Update(ServiceCreateUpdateDto dto, int id)
    {
        var service = Mapper.Map(dto);
        service.Id = id;

        var result = await serviceRepository.Update(service);
        return Mapper.Map(result);
    }
}
