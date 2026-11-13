using BeautySalon.Domain;
using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Репозиторий для услуг
/// </summary>
public class ServiceInMemoryRepository : IRepository<Service, int>
{
    private readonly List<Service> _services = DataSeeder.Services.ToList();
    private int _lastId = DataSeeder.Services.Count + 1;

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
        var service = _services.FirstOrDefault(c => c.Id == entityId);
        return Task.FromResult(service);
    }

    /// <inheritdoc/>
    public Task<Service> Update(Service entity)
    {
        var service = Read(entity.Id).Result;

        if (service == null)
            return Create(entity);

        service.Name = entity.Name;
        service.Category = entity.Category;
        service.Price = entity.Price;
        service.Duration = entity.Duration;

        return Task.FromResult(service);
    }
}
