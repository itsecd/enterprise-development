using CarWash.Contracts.Dto;

namespace CarWash.Contracts.Interfaces;

/// <summary>
/// Сервис аналитических запросов по автомойке
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Возвращает топ-5 клиентов по количеству посещений
    /// </summary>
    /// <returns>Клиенты и количество их посещений</returns>
    public Task<IEnumerable<TopClientDto>> GetTop5ClientsAsync();

    /// <summary>
    /// Возвращает автомобили, находящиеся на мойке в указанный момент
    /// </summary>
    /// <param name="time">Момент времени (если не указан, используется текущее время)</param>
    /// <returns>Автомобили на мойке</returns>
    public Task<IEnumerable<CurrentCarDto>> GetCurrentCarsAsync(DateTime? time);

    /// <summary>
    /// Возвращает топ-5 наиболее популярных услуг
    /// </summary>
    /// <returns>Услуги и количество их заказов</returns>
    public Task<IEnumerable<TopServiceDto>> GetTop5PopularServicesAsync();

    /// <summary>
    /// Возвращает время, начиная с которого бокс мойки свободен
    /// </summary>
    /// <param name="boxNumber">Номер бокса</param>
    /// <param name="time">Момент времени (если не указан, используется текущее время)</param>
    /// <returns>Время освобождения бокса</returns>
    public Task<BoxFreeTimeDto> GetBoxFreeTimeAsync(int boxNumber, DateTime? time);

    /// <summary>
    /// Возвращает суммарную выручку по каждой услуге
    /// </summary>
    /// <returns>Словарь: название услуги и суммарная выручка</returns>
    public Task<Dictionary<string, decimal>> GetRevenueForEachServiceAsync();
}
