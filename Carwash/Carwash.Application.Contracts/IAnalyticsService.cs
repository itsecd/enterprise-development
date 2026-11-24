using Carwash.Application.Contracts.Cars;
using Carwash.Application.Contracts.Analytics;

namespace Carwash.Application.Contracts;

/// <summary>
/// Интерфейс службы приложения для аналитичискеих запросов
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получение списка из пяти Dto клиентов с наибольшим количеством посещений.
    /// </summary>
    /// <returns>Коллекция ClientVisitDto</returns>
    public Task<IList<ClientVisitDto>> GetTop5ClientsByVisits();

    /// <summary>
    /// Получение списка автомобилей, которые в данный момент находятся на автомойке.
    /// </summary>
    /// <returns>Список CarDto</returns>
    public Task<IList<CarDto>> GetCarsCurrentlyAtCarWash();

    /// <summary>
    /// Получение списка из пяти Dto самых популярных услуг
    /// </summary>
    /// <returns>Список PopularServiceDto</returns>
    public Task<IList<PopularServiceDto>> GetTop5MostPopularServices();

    /// <summary>
    /// Получение времени освобождения бокса с указанным номером
    /// </summary>
    /// <param name="BoxNumber">Номер бокса</param>
    /// <returns>Время освобождения</returns>
    public Task<DateTime?> GetBoxReleaseTime(int BoxNumber);

    /// <summary>
    /// Получение списка Dto всех услуг с суммарной выручкой по каждой услуге
    /// </summary>
    /// <returns>Список ServiceRevenueDto</returns>
    public Task<IList<ServiceRevenueDto>> GetTotalRevenueByServices();
}
