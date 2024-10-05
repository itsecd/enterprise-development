using InstitutionStatistic.Domain.Models.BaseModel;

namespace InstitutionStatistic.Domain.Queries;

/// <summary>
/// Универсальный класс запросов сущнсотей, наследуемых от EntityWithName
/// </summary>
public abstract class GetInfoQuery<TEntity>: GetSimpleInfoQuery<TEntity> where TEntity : EntityWithName
{
    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="repository"></param>
    public GetInfoQuery(ICollection<TEntity> repository): base(repository) { }

    /// <summary>
    /// Получить сущность по имени
    /// </summary>
    /// <param name="name"></param>
    public TEntity? GetByName(string name) => Repository.Where(x => x.Name == name).FirstOrDefault();
}