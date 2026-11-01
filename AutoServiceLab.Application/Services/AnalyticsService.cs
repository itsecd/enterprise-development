using AutoServiceLab.Application.Contracts.DTOs.Clients;
using AutoServiceLab.Application.Contracts.DTOs.Mechanics;
using AutoServiceLab.Application.Contracts.DTOs.WorkTypes;
using AutoServiceLab.Application.Contracts.Interfaces;
using AutoServiceLab.Domain.Enums;
using AutoServiceLab.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace AutoServiceLab.Application.Services;

/// <summary>
/// Сервис для аналитических запросов
/// </summary>
public class AnalyticsService(
    IMechanicRepository mechanicRepository,
    IClientRepository clientRepository,
    IWorkTypeRepository workTypeRepository,
    IServiceOrderRepository orderRepository,
    IOrderWorkRepository orderWorkRepository,
    IOrderMechanicRepository orderMechanicRepository,
    ILogger<AnalyticsService> logger) : IAnalyticsService
{
    /// <inheritdoc />
    public IList<MechanicDto> GetMechanicsByWorkCategory(MechanicSpecialization category)
    {
        logger.LogInformation("Получение механиков по категории работ: {Category}", category);

        var result = mechanicRepository.GetAll()
            .Where(m => m.Specialization == category)
            .Select(m => new MechanicDto(m.Id, m.FullName, m.Specialization, m.ExperienceYears))
            .ToList();

        return result;
    }

    /// <inheritdoc />
    public IList<ClientDto> GetClientsByMechanic(int mechanicId)
    {
        logger.LogInformation("Получение клиентов, обслуживавшихся у механика {Id}", mechanicId);

        var orderIds = orderMechanicRepository.GetAll()
            .Where(om => om.MechanicId == mechanicId)
            .Select(om => om.ServiceOrderId)
            .Distinct()
            .ToList();

        var clientIds = orderRepository.GetAll()
            .Where(o => orderIds.Contains(o.Id))
            .Select(o => o.ClientId)
            .Distinct()
            .ToList();

        var result = clientRepository.GetAll()
            .Where(c => clientIds.Contains(c.Id))
            .OrderBy(c => c.FullName)
            .Select(c => new ClientDto(c.Id, c.FullName, c.Phone))
            .ToList();

        return result;
    }

    /// <inheritdoc />
    public int GetRepeatClientsCount()
    {
        logger.LogInformation("Подсчёт повторных обращений за последний месяц");

        var monthAgo = DateTime.Now.AddMonths(-1);

        var count = orderRepository.GetAll()
            .Where(o => o.ReceptionDate >= monthAgo)
            .GroupBy(o => o.ClientId)
            .Count(g => g.Count() > 1);

        return count;
    }

    /// <inheritdoc />
    public decimal GetOrderTotalCost(int orderId)
    {
        logger.LogInformation("Подсчёт стоимости заказа {Id}", orderId);

        var cost = orderWorkRepository.GetAll()
            .Where(ow => ow.ServiceOrderId == orderId)
            .Sum(ow => ow.WorkType?.LaborCost ?? 0);

        return cost;
    }

    /// <inheritdoc />
    public IList<WorkTypeDto> GetTopFiveWorkTypes()
    {
        logger.LogInformation("Получение топ 5 видов работ");

        var topWorkTypeIds = orderWorkRepository.GetAll()
            .GroupBy(ow => ow.WorkTypeId)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => g.Key)
            .ToList();

        var result = workTypeRepository.GetAll()
            .Where(w => topWorkTypeIds.Contains(w.Id))
            .Select(w => new WorkTypeDto(w.Id, w.Name, w.Category, w.Duration, w.LaborCost))
            .ToList();

        return result;
    }
}
