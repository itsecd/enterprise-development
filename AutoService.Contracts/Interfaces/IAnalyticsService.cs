using AutoService.Contracts.Dtos;

namespace AutoService.Contracts.Interfaces;

/// <summary>
/// Сервис для аналитических запросов.
/// </summary>
public interface IAnalyticsService
{
    public Task<IEnumerable<MechanicReportDto>> GetMechanicsByWorkAsync(string workName);
    public Task<IEnumerable<ClientMechanicReportDto>> GetClientsByMechanicAsync(Guid mechanicId);
    public Task<IEnumerable<RepeatClientReportDto>> GetRepeatClientsAsync();
    public Task<decimal> GetOrderTotalCostAsync(Guid orderId);
    public Task<IEnumerable<TopWorkReportDto>> GetTop5WorksAsync();
}