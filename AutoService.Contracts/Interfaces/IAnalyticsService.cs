using AutoService.Contracts.Dtos;

namespace AutoService.Contracts.Interfaces;

/// <summary>
/// Сервис для аналитических запросов.
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получить механиков, специализирующихся на выбранном виде работ.
    /// </summary>
    /// <param name="workName">Название вида работ.</param>
    public Task<IEnumerable<MechanicReportDto>> GetMechanicsByWorkAsync(string workName);

    /// <summary>
    /// Получить клиентов, чьи автомобили обслуживались у указанного механика, с сортировкой по ФИО.
    /// </summary>
    /// <param name="mechanicId">Идентификатор механика.</param>
    public Task<IEnumerable<ClientMechanicReportDto>> GetClientsByMechanicAsync(Guid mechanicId);

    /// <summary>
    /// Получить клиентов с повторными обращениями за последний месяц.
    /// </summary>
    public Task<IEnumerable<RepeatClientReportDto>> GetRepeatClientsAsync();

    /// <summary>
    /// Подсчитать суммарную стоимость работ для выбранного заказа.
    /// </summary>
    /// <param name="orderId">Идентификатор заказа.</param>
    public Task<decimal> GetOrderTotalCostAsync(Guid orderId);

    /// <summary>
    /// Получить пять наиболее частых видов работ.
    /// </summary>
    public Task<IEnumerable<TopWorkReportDto>> GetTop5WorksAsync();
}
