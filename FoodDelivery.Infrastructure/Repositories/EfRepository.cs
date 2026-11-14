using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Repositories;
using FoodDelivery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FoodDelivery.Infrastructure.Repositories;

/// <summary>
/// Реализация обобщённого репозитория на базе Entity Framework Core и PostgreSQL.
/// </summary>
public class EfRepository<TEntity>(FoodDeliveryDbContext context) : IRepository<TEntity>
    where TEntity : class, IEntity
{
    private readonly DbSet<TEntity> _dbSet = context.Set<TEntity>();

    public IReadOnlyList<TEntity> GetAll() =>
        BuildQueryWithIncludes().AsNoTracking().ToList();

    public TEntity? GetById(int id) =>
        BuildQueryWithIncludes().FirstOrDefault(e => e.Id == id);

    public TEntity Create(TEntity entity)
    {
        NormalizeEntityDates(entity);

        if (entity is Order order && order.Dishes.Count > 0)
        {
            var dishIds = order.Dishes.Select(d => d.Id).ToList();
            order.Dishes = context.Dishes.Where(d => dishIds.Contains(d.Id)).ToList();
        }

        _dbSet.Add(entity);
        context.SaveChanges();
        return GetById(entity.Id) ?? entity;
    }

    public bool Update(TEntity entity)
    {
        NormalizeEntityDates(entity);

        var existing = BuildQueryWithIncludes().FirstOrDefault(e => e.Id == entity.Id);
        if (existing is null)
        {
            return false;
        }

        context.Entry(existing).CurrentValues.SetValues(entity);

        if (existing is Order existingOrder && entity is Order updatedOrder)
        {
            var dishIds = updatedOrder.Dishes.Select(d => d.Id).ToList();
            existingOrder.Dishes.Clear();
            var dishes = context.Dishes.Where(d => dishIds.Contains(d.Id)).ToList();
            existingOrder.Dishes.AddRange(dishes);
        }

        context.SaveChanges();
        return true;
    }

    public void Delete(int id)
    {
        var existing = _dbSet.FirstOrDefault(e => e.Id == id);
        if (existing is not null)
        {
            _dbSet.Remove(existing);
            context.SaveChanges();
        }
    }

    private IQueryable<TEntity> BuildQueryWithIncludes()
    {
        IQueryable<TEntity> query = _dbSet;

        if (typeof(TEntity) == typeof(Dish))
        {
            query = ((IQueryable<Dish>)query)
                .Include(d => d.Category)
                .Include(d => d.Restaurant)
                .Cast<TEntity>();
        }
        else if (typeof(TEntity) == typeof(Order))
        {
            query = ((IQueryable<Order>)query)
                .Include(o => o.Client)
                .Include(o => o.Restaurant)
                .Include(o => o.Dishes)
                .Cast<TEntity>();
        }

        return query;
    }

    private static void NormalizeEntityDates(TEntity entity)
    {
        if (entity is Order order)
        {
            if (order.OrderDateTime.Kind == DateTimeKind.Unspecified)
            {
                order.OrderDateTime = DateTime.SpecifyKind(order.OrderDateTime, DateTimeKind.Utc);
            }

            if (order.DeliveryDateTime is { Kind: DateTimeKind.Unspecified } delivery)
            {
                order.DeliveryDateTime = DateTime.SpecifyKind(delivery, DateTimeKind.Utc);
            }
        }
    }
}