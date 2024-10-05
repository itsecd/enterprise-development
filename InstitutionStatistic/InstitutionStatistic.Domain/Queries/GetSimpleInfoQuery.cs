using InstitutionStatistic.Domain.Models.BaseModel;

namespace InstitutionStatistic.Domain.Queries;

/// <summary>
/// Универсальный класс запросов сущностей, наследуемых от Entity
/// </summary>
public abstract class GetSimpleInfoQuery<TEntity> where TEntity : Entity
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
    public TEntity? GetById(Guid id) => Repository.Where(x => x.Id == id).FirstOrDefault();
}
