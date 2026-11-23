using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;
using BeautySalon.Infrastructure.Data;

namespace BeautySalon.Infrastructure.Repositories;

public class EfMasterRepository(BeautySalonDbContext context) : IMasterRepository
{
    /// <inheritdoc />
    public IEnumerable<Master> GetAll() => context.Masters.ToList();

    /// <inheritdoc />
    public Master? GetById(int id) => context.Masters.FirstOrDefault(m => m.Id == id);

    /// <inheritdoc />
    public void Add(Master master) 
    { 
        context.Masters.Add(master); 
        context.SaveChanges(); 
    }

    /// <inheritdoc />
    public void Update(Master master) 
    { 
        context.Masters.Update(master); 
        context.SaveChanges(); 
    }

    /// <inheritdoc />
    public void Delete(Master master) 
    { 
        context.Masters.Remove(master); 
        context.SaveChanges(); 
    }
}