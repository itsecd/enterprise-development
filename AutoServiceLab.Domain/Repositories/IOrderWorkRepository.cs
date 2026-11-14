using AutoServiceLab.Domain.Entities;

namespace AutoServiceLab.Domain.Repositories;

/// <summary>
/// Репозиторий связей заказов и работ
/// </summary>
public interface IOrderWorkRepository : IRepository<OrderWork, int>
{
}
