using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

public class MechanicRepository : IMechanicRepository
{
    /// <inheritdoc />
    public Mechanic? Get(int id) => AutoServiceData.Mechanics.FirstOrDefault(m => m.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<Mechanic> GetAll() => AutoServiceData.Mechanics;

    /// <inheritdoc />
    public void Add(Mechanic entity)
    {
        entity.Id = AutoServiceData.Mechanics.Count == 0 ? 1 : AutoServiceData.Mechanics.Max(m => m.Id) + 1;
        AutoServiceData.Mechanics.Add(entity);
    }

    /// <inheritdoc />
    public void Update(Mechanic entity)
    {
        var index = AutoServiceData.Mechanics.FindIndex(m => m.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException();
        AutoServiceData.Mechanics[index] = entity;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var m = Get(id);
        return m != null && AutoServiceData.Mechanics.Remove(m);
    }
}
