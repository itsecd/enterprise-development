using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

public class WorkTypeRepository : IWorkTypeRepository
{
    private readonly List<WorkType> _workTypes = new(AutoServiceData.WorkTypes);

    /// <inheritdoc />
    public WorkType? Get(int id) => _workTypes.FirstOrDefault(w => w.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<WorkType> GetAll() => _workTypes;

    /// <inheritdoc />
    public void Add(WorkType entity)
    {
        entity.Id = _workTypes.Count == 0 ? 1 : _workTypes.Max(w => w.Id) + 1;
        _workTypes.Add(entity);
    }

    /// <inheritdoc />
    public void Update(WorkType entity)
    {
        var index = _workTypes.FindIndex(w => w.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException();
        _workTypes[index] = entity;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var w = Get(id);
        return w != null && _workTypes.Remove(w);
    }
}
