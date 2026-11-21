using CarWash.Domain.Data;
using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;

namespace CarWash.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий, хранящий услуги в памяти
/// </summary>
public class ServiceRepository : IServiceRepository
{
    private readonly List<Service> _services = [.. DataSeed.Services];

    /// <inheritdoc />
    public Task<IEnumerable<Service>> GetAllAsync()
        => Task.FromResult<IEnumerable<Service>>([.. _services]);

    /// <inheritdoc />
    public Task<Service?> GetByIdAsync(int id)
        => Task.FromResult(_services.FirstOrDefault(s => s.Id == id));

    /// <inheritdoc />
    public Task<Service> AddAsync(Service entity)
    {
        entity.Id = _services.Count != 0 ? _services.Max(s => s.Id) + 1 : 1;
        _services.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc />
    public Task UpdateAsync(Service entity)
    {
        var index = _services.FindIndex(s => s.Id == entity.Id);
        if (index != -1)
        {
            _services[index] = entity;
        }

        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task DeleteAsync(int id)
    {
        var item = _services.FirstOrDefault(s => s.Id == id);
        if (item != null)
        {
            _services.Remove(item);
        }

        return Task.CompletedTask;
    }
}
