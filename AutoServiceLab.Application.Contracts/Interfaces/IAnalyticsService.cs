using AutoServiceLab.Application.Contracts.DTOs.Clients;
using AutoServiceLab.Application.Contracts.DTOs.Mechanics;
using AutoServiceLab.Application.Contracts.DTOs.WorkTypes;
using AutoServiceLab.Domain.Enums;

namespace AutoServiceLab.Application.Contracts.Interfaces;

/// <summary>
/// Контракт для аналитических запросов
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Механики по выбранной категории работ
    /// </summary>
    public IList<MechanicDto> GetMechanicsByWorkCategory(MechanicSpecialization category);

    /// <summary>
    /// Клиенты указанного механика, отсортированные по ФИО
    /// </summary>
    public IList<ClientDto> GetClientsByMechanic(int mechanicId);

    /// <summary>
    /// Количество повторных обращений клиентов за последний месяц
    /// </summary>
    public int GetRepeatClientsCount();

    /// <summary>
    /// Суммарная стоимость работ для заказа
    /// </summary>
    public decimal GetOrderTotalCost(int orderId);

    /// <summary>
    /// Топ-5 наиболее частых видов работ
    /// </summary>
    public IList<WorkTypeDto> GetTopFiveWorkTypes();
}
