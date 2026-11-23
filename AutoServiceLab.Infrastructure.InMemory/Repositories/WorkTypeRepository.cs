using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

public class WorkTypeRepository : IWorkTypeRepository
{
    private readonly List<WorkType> _workTypes = new(AutoServiceData.WorkTypes);

    /// <inheritdoc />
    public Task<WorkType?> GetAsync(int id) => Task.FromResult(_workTypes.FirstOrDefault(w => w.Id == id));

    /// <inheritdoc />
    public Task<IReadOnlyList<WorkType>> GetAllAsync() => Task.FromResult<IReadOnlyList<WorkType>>(_workTypes);

    /// <inheritdoc />
    public Task AddAsync(WorkType entity)
    {
        entity.Id = _workTypes.Count == 0 ? 1 : _workTypes.Max(w => w.Id) + 1;
        _workTypes.Add(entity);
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public Task UpdateAsync(WorkType entity)
    {
        var index = _workTypes.FindIndex(w => w.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException();
        _workTypes[index] = entity;
        return Task.CompletedTask;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var w = await GetAsync(id);
        return w != null && _workTypes.Remove(w);
    }
}
