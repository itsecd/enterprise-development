using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;

namespace AutoServiceLab.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий видов работ (EF Core)
/// </summary>
public class WorkTypeEfCoreRepository(AutoServiceDbContext context) : IWorkTypeRepository
{
    /// <inheritdoc />
    public WorkType? Get(int id) => context.WorkTypes.FirstOrDefault(w => w.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<WorkType> GetAll() => context.WorkTypes.ToList();

    /// <inheritdoc />
    public void Add(WorkType entity)
    {
        context.WorkTypes.Add(entity);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public void Update(WorkType entity)
    {
        context.WorkTypes.Update(entity);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var workType = Get(id);
        if (workType == null) return false;

        context.WorkTypes.Remove(workType);
        context.SaveChanges();
        return true;
    }
}
