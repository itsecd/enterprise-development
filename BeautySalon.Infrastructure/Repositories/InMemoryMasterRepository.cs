using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.Repositories;

/// <summary>
/// In memory реализация репозитория мастеров.
/// </summary>
public class InMemoryMasterRepository : IMasterRepository
{
    private readonly List<Master> _masters;

    /// <summary>
    /// Инициализирует репозиторий, копируя данные из SeederData.
    /// </summary>
    public InMemoryMasterRepository()
    {
        _masters = SeederData.Masters.ToList();
    }

    /// <inheritdoc />
    public Task<IEnumerable<Master>> GetAllAsync() => Task.FromResult<IEnumerable<Master>>(_masters);

    /// <inheritdoc />
    public Task<Master?> GetByIdAsync(int id) => Task.FromResult(_masters.FirstOrDefault(m => m.Id == id));

    /// <inheritdoc />
    public Task AddAsync(Master entity)
    {
        entity.Id = _masters.Count > 0 ? _masters.Max(m => m.Id) + 1 : 1;
        _masters.Add(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task UpdateAsync(Master entity) => Task.CompletedTask;

    /// <inheritdoc />
    public Task DeleteAsync(Master entity)
    {
        _masters.Remove(entity);
        return Task.CompletedTask;
    }
}