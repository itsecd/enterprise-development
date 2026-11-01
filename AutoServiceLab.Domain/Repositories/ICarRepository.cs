using AutoServiceLab.Domain.Entities;

namespace AutoServiceLab.Domain.Repositories;

/// <summary>
/// Репозиторий автомобилей
/// </summary>
public interface ICarRepository : IRepository<Car, int>
{
}
