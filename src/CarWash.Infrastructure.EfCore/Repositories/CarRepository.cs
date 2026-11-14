using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;

namespace CarWash.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий автомобилей в базе данных
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class CarRepository(CarWashDbContext context) : EfRepository<Car>(context), ICarRepository;
