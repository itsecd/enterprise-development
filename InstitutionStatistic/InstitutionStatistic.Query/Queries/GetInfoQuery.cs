using InstitutionStatistic.Query.Models.BaseModel;

namespace InstitutionStatistic.Query.Queries;

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
    /// Получить сущность по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public TEntity? GetById(Guid id)
    {
        return Repository.Where(x => x.Id == id).FirstOrDefault();
    }

    /// <summary>
    /// Получить сущность по имени
    /// </summary>
    /// <param name="name"></param>
    public TEntity? GetByName(string name)
    {
        return Repository.Where(x => x.Name == name).FirstOrDefault();
    }

}

/// <summary>
/// Универсальный класс запросов сущностей, наследуемых от Entity
/// </summary>
public abstract class GetSimpleInfoQuery<TEntity> where TEntity: Entity
{
    /// <summary>
    /// Repository
    /// </summary>
    public ICollection<TEntity> Repository { get; set; }

    /// <summary>
    /// ctor
    /// </summary>
    /// <param name="repository"></param>
    public GetSimpleInfoQuery(ICollection<TEntity> repository)
    {
        Repository = repository;
    }


    /// <summary>
    /// Получить сущность по id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public TEntity? GetById(Guid id)
    {
        return Repository.Where(x => x.Id == id).FirstOrDefault();
    }
}
