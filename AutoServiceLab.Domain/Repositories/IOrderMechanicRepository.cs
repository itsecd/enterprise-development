using AutoServiceLab.Domain.Entities;

namespace AutoServiceLab.Domain.Repositories;

/// <summary>
/// Репозиторий связей заказов и механиков
/// </summary>
public interface IOrderMechanicRepository : IRepository<OrderMechanic, int>
{
}
