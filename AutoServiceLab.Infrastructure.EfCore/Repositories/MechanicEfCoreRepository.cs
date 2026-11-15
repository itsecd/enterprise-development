using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;

namespace AutoServiceLab.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий механиков (EF Core)
/// </summary>
public class MechanicEfCoreRepository(AutoServiceDbContext context) : IMechanicRepository
{
    /// <inheritdoc />
    public Mechanic? Get(int id) => context.Mechanics.FirstOrDefault(m => m.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<Mechanic> GetAll() => context.Mechanics.ToList();

    /// <inheritdoc />
    public void Add(Mechanic entity)
    {
        context.Mechanics.Add(entity);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public void Update(Mechanic entity)
    {
        context.Mechanics.Update(entity);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var mechanic = Get(id);
        if (mechanic == null) return false;

        context.Mechanics.Remove(mechanic);
        context.SaveChanges();
        return true;
    }
}
