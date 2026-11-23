using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

public class MechanicRepository : IMechanicRepository
{
    private readonly List<Mechanic> _mechanics = new(AutoServiceData.Mechanics);

    /// <inheritdoc />
    public Mechanic? Get(int id) => _mechanics.FirstOrDefault(m => m.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<Mechanic> GetAll() => _mechanics;

    /// <inheritdoc />
    public void Add(Mechanic entity)
    {
        entity.Id = _mechanics.Count == 0 ? 1 : _mechanics.Max(m => m.Id) + 1;
        _mechanics.Add(entity);
    }

    /// <inheritdoc />
    public void Update(Mechanic entity)
    {
        var index = _mechanics.FindIndex(m => m.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException();
        _mechanics[index] = entity;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var m = Get(id);
        return m != null && _mechanics.Remove(m);
    }
}
