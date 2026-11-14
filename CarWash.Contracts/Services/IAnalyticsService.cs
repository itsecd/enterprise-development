using CarWash.Contracts.Responses;

namespace CarWash.Contracts.Services;

/// <summary>
/// Аналитические запросы автомойки
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получить пять клиентов с наибольшим количеством посещений
    /// </summary>
    Task<IReadOnlyList<TopCustomerResponse>> GetTopCustomersAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить автомобили, находящиеся сейчас на мойке
    /// </summary>
    Task<IReadOnlyList<CarResponse>> GetCurrentCarsAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить пять наиболее популярных услуг
    /// </summary>
    Task<IReadOnlyList<PopularServiceResponse>> GetTopServicesAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить время освобождения бокса
    /// </summary>
    Task<BoxAvailabilityResponse> GetBoxAvailabilityAsync(int boxNumber, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить выручку по каждой услуге
    /// </summary>
    Task<IReadOnlyList<ServiceRevenueResponse>> GetRevenuePerServiceAsync(CancellationToken cancellationToken = default);
}
