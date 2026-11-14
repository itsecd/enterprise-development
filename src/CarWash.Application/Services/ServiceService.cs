using CarWash.Application.Mapping;
using CarWash.Contracts.Dto;
using CarWash.Contracts.Interfaces;
using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;

namespace CarWash.Application.Services;

/// <summary>
/// Сервис для работы с услугами
/// </summary>
/// <param name="repository">Репозиторий услуг</param>
public class ServiceService(IServiceRepository repository) : IServiceService
{
    /// <inheritdoc />
    public async Task<IEnumerable<ServiceDto>> GetAllAsync()
    {
        var services = await repository.GetAllAsync();
        return services.Select(s => s.ToDto());
    }

    /// <inheritdoc />
    public async Task<ServiceDto?> GetByIdAsync(int id)
    {
        var service = await repository.GetByIdAsync(id);
        return service?.ToDto();
    }

    /// <inheritdoc />
    public async Task<ServiceDto> CreateAsync(ServiceEditDto editDto)
    {
        var service = new Service
        {
            Name = editDto.Name,
            Category = editDto.Category,
            Price = editDto.Price,
            DurationMinutes = editDto.DurationMinutes
        };

        var created = await repository.AddAsync(service);
        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(int id, ServiceEditDto editDto)
    {
        var service = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Услуга не найдена");

        service.Name = editDto.Name;
        service.Category = editDto.Category;
        service.Price = editDto.Price;
        service.DurationMinutes = editDto.DurationMinutes;
        await repository.UpdateAsync(service);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id)
    {
        _ = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Услуга не найдена");

        await repository.DeleteAsync(id);
    }
}
