using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.Repositories;

/// <summary>
/// In memory реализация репозитория услуг.
/// </summary>
public class InMemoryServiceRepository : IServiceRepository
{
    private readonly List<Service> _services;

    /// <summary>
    /// Инициализирует репозиторий, копируя данные из SeederData.
    /// </summary>
    public InMemoryServiceRepository()
    {
        _services = SeederData.Services.ToList();
    }

    /// <inheritdoc />
    public Task<IEnumerable<Service>> GetAllAsync() => Task.FromResult<IEnumerable<Service>>(_services);

    /// <inheritdoc />
    public Task<Service?> GetByIdAsync(int id) => Task.FromResult(_services.FirstOrDefault(s => s.Id == id));

    /// <inheritdoc />
    public Task AddAsync(Service entity)
    {
        entity.Id = _services.Count > 0 ? _services.Max(s => s.Id) + 1 : 1;
        _services.Add(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task UpdateAsync(Service entity) => Task.CompletedTask;

    /// <inheritdoc />
    public Task DeleteAsync(Service entity)
    {
        _services.Remove(entity);
        return Task.CompletedTask;
    }
}