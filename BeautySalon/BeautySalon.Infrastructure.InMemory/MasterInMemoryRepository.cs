using BeautySalon.Domain;
using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;

namespace BeautySalon.Infrastructure.InMemory;

/// <summary>
/// Репозиторий для мастеров
/// </summary>
public class MasterInMemoryRepository : IRepository<Master, int>
{
    private readonly List<Master> _masters = DataSeeder.Masters.ToList();
    private int _lastId = DataSeeder.Masters.Count + 1;

    /// <inheritdoc/>
    public Task<Master> Create(Master entity)
    {
        entity.Id = _lastId++;
        _masters.Add(entity);
        return Task.FromResult(entity);
    }

    /// <inheritdoc/>
    public Task<bool> Delete(int entityId)
    {
        var master = Read(entityId).Result;
        if (master == null)
            return Task.FromResult(false);

        var result = _masters.Remove(master);
        return Task.FromResult(result);
    }

    /// <inheritdoc/>
    public Task<List<Master>> ReadAll()
    {
        return Task.FromResult(_masters.ToList());
    }

    /// <inheritdoc/>
    public Task<Master?> Read(int entityId)
    {
        var master = _masters.FirstOrDefault(c => c.Id == entityId);
        return Task.FromResult(master);
    }

    /// <inheritdoc/>
    public Task<Master> Update(Master entity)
    {
        var master = Read(entity.Id).Result;

        if (master == null)
            return Create(entity);

        master.FullName = entity.FullName;
        master.PassportNumber = entity.PassportNumber;
        master.Gender = entity.Gender;
        master.Specialization = entity.Specialization;
        master.WorkExperience = entity.WorkExperience;

        return Task.FromResult(master);
    }
}
