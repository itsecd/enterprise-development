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
    public IEnumerable<Service> GetAll() => _services;

    /// <inheritdoc />
    public Service? GetById(int id) => _services.FirstOrDefault(s => s.Id == id);

    /// <inheritdoc />
    public void Add(Service service)
    {
        service.Id = _services.Count > 0 ? _services.Max(s => s.Id) + 1 : 1;
        _services.Add(service);
    }

    /// <inheritdoc />
    public void Update(Service service) { }

    /// <inheritdoc />
    public void Delete(Service service) => _services.Remove(service);
}