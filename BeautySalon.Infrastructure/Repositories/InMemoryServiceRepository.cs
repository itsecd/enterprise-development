using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.Repositories;

/// <summary>
/// In memory реализация репозитория услуг.
/// </summary>
public class InMemoryServiceRepository : IServiceRepository
{
    public IEnumerable<Service> GetAll() => SeederData.Services;
    public Service? GetById(int id) => SeederData.Services.FirstOrDefault(s => s.Id == id);
    
    public void Add(Service service)
    {
        service.Id = SeederData.Services.Count > 0 ? SeederData.Services.Max(s => s.Id) + 1 : 1;
        SeederData.Services.Add(service);
    }
    
    public void Update(Service service) { }
    public void Delete(Service service) => SeederData.Services.Remove(service);
}