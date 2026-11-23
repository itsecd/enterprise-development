using AutoServiceLab.Application.Contracts.Dtos.Clients;
using AutoServiceLab.Application.Contracts.Dtos.Mechanics;
using AutoServiceLab.Application.Contracts.Dtos.WorkTypes;
using AutoServiceLab.Application.Contracts.Interfaces;
using AutoServiceLab.Domain.Repositories;
using AutoServiceLab.Shared.Enums;
using Microsoft.Extensions.Logging;

namespace AutoServiceLab.Application.Services;

/// <summary>
/// Сервис для аналитических запросов
/// </summary>
public class AnalyticsService(
    IMechanicRepository mechanicRepository,
    IServiceOrderRepository orderRepository,
    IOrderWorkRepository orderWorkRepository,
    IOrderMechanicRepository orderMechanicRepository,
    ILogger<AnalyticsService> logger) : IAnalyticsService
{
    /// <inheritdoc />
    public async Task<IList<MechanicDto>> GetMechanicsByWorkCategory(MechanicSpecialization category)
    {
        logger.LogInformation("Получение механиков по категории работ: {Category}", category);

        var result = (await mechanicRepository.GetAllAsync())
            .Where(m => m.Specialization == category)
            .Select(m => new MechanicDto(m.Id, m.FullName, m.Specialization, m.ExperienceYears))
            .ToList();

        return result;
    }

    /// <inheritdoc />
    public async Task<IList<ClientDto>> GetClientsByMechanic(int mechanicId)
    {
        logger.LogInformation("Получение клиентов, обслуживавшихся у механика {Id}", mechanicId);

        if (await mechanicRepository.GetAsync(mechanicId) == null)
        {
            throw new KeyNotFoundException($"Механик с Id {mechanicId} не найден");
        }

        var result = (await orderMechanicRepository.GetAllAsync())
        .Where(om => om.MechanicId == mechanicId)
        .Select(om => om.ServiceOrder)
        .Select(order => order.Client)
        .Distinct()
        .OrderBy(c => c.FullName)
        .Select(c => new ClientDto(c.Id, c.FullName, c.Phone))
        .ToList();

        return result;
    }

    /// <inheritdoc />
    public async Task<int> GetRepeatClientsCount()
    {
        logger.LogInformation("Подсчёт повторных обращений за последний месяц");

        var monthAgo = DateTime.Now.AddMonths(-1);

        var count = (await orderRepository.GetAllAsync())
            .Where(o => o.ReceptionDate >= monthAgo)
            .GroupBy(o => o.ClientId)
            .Count(g => g.Count() > 1);

        return count;
    }

    /// <inheritdoc />
    public async Task<decimal> GetOrderTotalCost(int orderId)
    {
        logger.LogInformation("Подсчёт стоимости заказа {Id}", orderId);

        if (await orderRepository.GetAsync(orderId) == null)
        {
            throw new KeyNotFoundException($"Заказ с Id {orderId} не найден");
        }

        var cost = (await orderWorkRepository.GetAllAsync())
            .Where(ow => ow.ServiceOrderId == orderId)
            .Sum(ow => ow.WorkType?.LaborCost ?? 0);

        return cost;
    }

    /// <inheritdoc />
    public async Task<IList<WorkTypeDto>> GetTopFiveWorkTypes()
    {
        logger.LogInformation("Получение топ 5 видов работ");

        var result = (await orderWorkRepository.GetAllAsync())
        .GroupBy(ow => ow.WorkType)
        .OrderByDescending(g => g.Count())
        .Take(5)
        .Select(g => new WorkTypeDto(
            g.Key.Id,
            g.Key.Name,
            g.Key.Category,
            g.Key.Duration,
            g.Key.LaborCost))
        .ToList();

        return result;
    }
}
