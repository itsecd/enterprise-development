using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

public class MechanicRepository : IMechanicRepository
{
    private readonly List<Mechanic> _mechanics = new(AutoServiceData.Mechanics);

    /// <inheritdoc />
    public Task<Mechanic?> GetAsync(int id) => Task.FromResult(_mechanics.FirstOrDefault(m => m.Id == id));

    /// <inheritdoc />
    public Task<IReadOnlyList<Mechanic>> GetAllAsync() => Task.FromResult<IReadOnlyList<Mechanic>>(_mechanics);

    /// <inheritdoc />
    public Task AddAsync(Mechanic entity)
    {
        entity.Id = _mechanics.Count == 0 ? 1 : _mechanics.Max(m => m.Id) + 1;
        _mechanics.Add(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task UpdateAsync(Mechanic entity)
    {
        var index = _mechanics.FindIndex(m => m.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException();
        _mechanics[index] = entity;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var m = await GetAsync(id);
        return m != null && _mechanics.Remove(m);
    }
}
