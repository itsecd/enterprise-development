using AutoService.Contracts.Dtos;
using AutoService.Contracts.Interfaces;
using AutoService.Domain.Interfaces;

namespace AutoService.Application.Services;

/// <summary>
/// Сервис для выполнения аналитических запросов.
/// </summary>
public class AnalyticsService(
    IMechanicRepository mechanicRepo,
    IClientRepository clientRepo,
    IOrderRepository orderRepo,
    IWorkTypeRepository workRepo) : IAnalyticsService
{
    /// <summary>
    /// 1. Получить механиков, специализирующихся на выбранном виде работ.
    /// </summary>
    /// <param name="workName">Название работы (например, "Диагностика ходовой").</param>
    public async Task<IEnumerable<MechanicReportDto>> GetMechanicsByWorkAsync(string workName)
    {
        var works = await workRepo.GetAllAsync();
        var work = works.FirstOrDefault(w => w.Name == workName) ?? throw new Exception("Работа не найдена");

        var mechanics = await mechanicRepo.GetAllAsync();
        return mechanics
            .Where(m => m.Specialization.ToString() == work.Category)
            .Select(m => new MechanicReportDto
            {
                Id = m.Id,
                FullName = m.FullName,
                Specialization = m.Specialization.ToString()
            });
    }

    /// <summary>
    /// 2. Получить клиентов указанного механика, упорядоченных по ФИО.
    /// </summary>
    /// <param name="mechanicId">Идентификатор механика.</param>
    public async Task<IEnumerable<ClientMechanicReportDto>> GetClientsByMechanicAsync(Guid mechanicId)
    {
        var mechanics = await mechanicRepo.GetAllAsync();
        var mechanic = mechanics.FirstOrDefault(m => m.Id == mechanicId)
            ?? throw new Exception("Механик не найден");

        var orders = await orderRepo.GetAllAsync();
        var clientIds = orders
            .Where(o => o.MechanicId == mechanic.Id)
            .Select(o => o.ClientId)
            .Distinct()
            .ToList();

        var clients = await clientRepo.GetAllAsync();
        return clients
            .Where(c => clientIds.Contains(c.Id))
            .OrderBy(c => c.FullName)
            .Select(c => new ClientMechanicReportDto
            {
                Id = c.Id,
                FullName = c.FullName,
                Phone = c.Phone
            });
    }

    /// <summary>
    /// 3. Получить клиентов с повторными обращениями за последний месяц.
    /// </summary>
    public async Task<IEnumerable<RepeatClientReportDto>> GetRepeatClientsAsync()
    {
        var monthAgo = DateTime.UtcNow.AddDays(-30);

        var orders = await orderRepo.GetAllAsync();
        var repeatClientIds = orders
            .Where(o => o.AcceptedAt >= monthAgo)
            .GroupBy(o => o.ClientId)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();

        var clients = await clientRepo.GetAllAsync();
        return clients
            .Where(c => repeatClientIds.Contains(c.Id))
            .Select(c => new RepeatClientReportDto { Id = c.Id, FullName = c.FullName });
    }

    /// <summary>
    /// 4. Подсчитать суммарную стоимость работ для выбранного заказа.
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    public async Task<decimal> GetOrderTotalCostAsync(Guid orderId)
    {
        var orders = await orderRepo.GetAllAsync();
        var order = orders.FirstOrDefault(o => o.Id == orderId)
            ?? throw new Exception("Заказ не найден");

        return order.Works.Sum(w => w.Price);
    }

    /// <summary>
    /// 5. Получить топ 5 наиболее частых видов работ.
    /// </summary>
    public async Task<IEnumerable<TopWorkReportDto>> GetTop5WorksAsync()
    {
        var orders = await orderRepo.GetAllAsync();

        return orders
            .SelectMany(o => o.Works)
            .GroupBy(w => w.Name)
            .Select(g => new TopWorkReportDto { Name = g.Key, Count = g.Count() })
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Name)
            .Take(5);
    }
}