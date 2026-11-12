using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Infrastructure.Repositories;

/// <summary>
/// Реализация обобщённого репозитория для хранения доменных сущностей в оперативной памяти.
/// </summary>
public class InMemoryRepository<TEntity>(List<TEntity> items) : IRepository<TEntity>
    where TEntity : class, IEntity
{
    public IReadOnlyList<TEntity> GetAll() => items;

    public TEntity? GetById(int id) => items.FirstOrDefault(x => x.Id == id);

    public TEntity Create(TEntity entity)
    {
        entity.Id = items.Count == 0 ? 1 : items.Max(x => x.Id) + 1;
        items.Add(entity);
        return entity;
    }

    public bool Update(TEntity entity)
    {
        var existing = GetById(entity.Id);
        if (existing is null) return false;

        var index = items.IndexOf(existing);
        items[index] = entity;
        return true;
    }

    public void Delete(int id)
    {
        var existing = GetById(id);
        if (existing is not null)
        {
            items.Remove(existing);
        }
    }
}