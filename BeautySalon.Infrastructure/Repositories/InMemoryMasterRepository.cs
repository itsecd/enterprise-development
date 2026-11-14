using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.Repositories;

/// <summary>
/// In memory реализация репозитория мастеров.
/// </summary>
public class InMemoryMasterRepository : IMasterRepository
{
    public IEnumerable<Master> GetAll() => SeederData.Masters;
    public Master? GetById(int id) => SeederData.Masters.FirstOrDefault(m => m.Id == id);
    
    public void Add(Master master)
    {
        master.Id = SeederData.Masters.Count > 0 ? SeederData.Masters.Max(m => m.Id) + 1 : 1;
        SeederData.Masters.Add(master);
    }
    
    public void Update(Master master) { }
    public void Delete(Master master) => SeederData.Masters.Remove(master);
}