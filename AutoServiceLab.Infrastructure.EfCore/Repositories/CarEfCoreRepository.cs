using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;

namespace AutoServiceLab.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий автомобилей (EF Core)
/// </summary>
public class CarEfCoreRepository(AutoServiceDbContext context) : ICarRepository
{
    /// <inheritdoc />
    public Car? Get(int id) => context.Cars.FirstOrDefault(c => c.Id == id);

    /// <inheritdoc />
    public IReadOnlyList<Car> GetAll() => context.Cars.ToList();

    /// <inheritdoc />
    public void Add(Car entity)
    {
        context.Cars.Add(entity);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public void Update(Car entity)
    {
        context.Cars.Update(entity);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        var car = Get(id);
        if (car == null) return false;

        context.Cars.Remove(car);
        context.SaveChanges();
        return true;
    }
}
