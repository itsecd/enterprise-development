using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Domain.Repositories;

/// <summary>
/// Обобщённый интерфейс репозитория для базовых операций CRUD с доменными сущностями.
/// </summary>
public interface IRepository<TEntity> where TEntity : class, IEntity
{
    public IReadOnlyList<TEntity> GetAll();
    public TEntity? GetById(int id);
    public TEntity Create(TEntity entity);
    public bool Update(TEntity entity);
    public void Delete(int id);
}