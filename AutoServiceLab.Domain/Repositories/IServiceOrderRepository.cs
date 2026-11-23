using AutoServiceLab.Domain.Entities;

namespace AutoServiceLab.Domain.Repositories;

/// <summary>
/// Репозиторий заказов
/// </summary>
public interface IServiceOrderRepository : IRepository<ServiceOrder, int>
{
}
