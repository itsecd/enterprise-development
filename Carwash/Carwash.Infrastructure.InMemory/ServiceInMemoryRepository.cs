using Carwash.Domain;
using Carwash.Domain.Data;
using Carwash.Domain.Entities;

namespace Carwash.Infrastructure.InMemory;

/// <summary>
/// Имплементация репозитория для услуг
/// </summary>
public class ServiceInMemoryRepository : IRepository<Service, int>
{
    private readonly List<Service> _services = DataSeed.Services;
    private int _lastId = DataSeed.Services.Count + 1;

    /// <inheritdoc/>
    public Task<Service> Create(Service entity)
    {
        entity.Id = _lastId++;
        _services.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc/>
    public Task<bool> Delete(int entityId)
    {
        var service = Read(entityId).Result;
        if (service == null)
            return Task.FromResult(false);

        var result = _services.Remove(service);
        return Task.FromResult(result);
    }

    /// <inheritdoc/>
    public Task<List<Service>> ReadAll()
    {
        return Task.FromResult(_services.ToList());
    }

    /// <inheritdoc/>
    public Task<Service?> Read(int entityId)
    {
        var service = _services.FirstOrDefault(s => s.Id == entityId);
        return Task.FromResult(service);
    }

    /// <inheritdoc/>
    public Task<Service> Update(Service entity)
    {
        var service = Read(entity.Id).Result;

        if (service == null)
            return Create(entity);

        service.Name = entity.Name;
        service.Price = entity.Price;
        service.Category = entity.Category;
        service.DurationInMinutes = entity.DurationInMinutes;

        return Task.FromResult(service);
    }
}
