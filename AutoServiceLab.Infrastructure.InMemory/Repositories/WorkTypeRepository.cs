using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Infrastructure.InMemory.Data;

namespace AutoServiceLab.Infrastructure.InMemory.Repositories;

public class WorkTypeRepository : IWorkTypeRepository
{
    /// <inheritdoc />
    public WorkType? Get(int id) => AutoServiceData.WorkTypes.FirstOrDefault(w => w.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<WorkType> GetAll() => AutoServiceData.WorkTypes;

    /// <inheritdoc />
    public void Add(WorkType entity)
    {
        entity.Id = AutoServiceData.WorkTypes.Count == 0 ? 1 : AutoServiceData.WorkTypes.Max(w => w.Id) + 1;
        AutoServiceData.WorkTypes.Add(entity);
    }

    /// <inheritdoc />
    public void Update(WorkType entity)
    {
        var index = AutoServiceData.WorkTypes.FindIndex(w => w.Id == entity.Id);
        if (index == -1) throw new KeyNotFoundException();
        AutoServiceData.WorkTypes[index] = entity;
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var w = Get(id);
        return w != null && AutoServiceData.WorkTypes.Remove(w);
    }
}
