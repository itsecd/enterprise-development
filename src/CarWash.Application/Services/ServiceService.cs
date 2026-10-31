using CarWash.Contracts.Dto;
using CarWash.Contracts.Interfaces;
using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;

namespace CarWash.Application.Services;

public class ServiceService(IServiceRepository repository) : IServiceService
{
    public async Task<IEnumerable<ServiceDto>> GetAllAsync()
    {
        var services = await repository.GetAllAsync();
        return services.Select(s => new ServiceDto
        {
            Id = s.Id,
            Name = s.Name,
            Category = s.Category,
            Price = s.Price,
            DurationMinutes = s.DurationMinutes
        });
    }

    public async Task<ServiceDto?> GetByIdAsync(int id)
    {
        var service = await repository.GetByIdAsync(id);
        if (service == null) return null;

        return new ServiceDto
        {
            Id = service.Id,
            Name = service.Name,
            Category = service.Category,
            Price = service.Price,
            DurationMinutes = service.DurationMinutes
        };
    }

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

        return new ServiceDto
        {
            Id = created.Id,
            Name = created.Name,
            Category = created.Category,
            Price = created.Price,
            DurationMinutes = created.DurationMinutes
        };
    }

    public async Task UpdateAsync(int id, ServiceEditDto editDto)
    {
        var service = await repository.GetByIdAsync(id);
        if (service == null) throw new KeyNotFoundException("Услуга не найдена");

        service.Name = editDto.Name;
        service.Category = editDto.Category;
        service.Price = editDto.Price;
        service.DurationMinutes = editDto.DurationMinutes;

        await repository.UpdateAsync(service);
    }

    public async Task DeleteAsync(int id)
    {
        await repository.DeleteAsync(id);
    }
}