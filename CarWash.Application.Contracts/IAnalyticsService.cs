namespace CarWash.Application.Contracts;

/// <summary>
/// Служба для выполнения аналитических запросов автомойки
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получает топ 5 клиентов по количеству посещений автомойки
    /// </summary>
    List<TopCustomerDto> GetTopFiveCustomersByVisitCount();

    /// <summary>
    /// Получает автомобили, находящиеся на мойке в данный момент
    /// </summary>
    List<int> GetCarsCurrentlyAtCarWash();

    /// <summary>
    /// Получает топ 5 наиболее популярных услуг автомойки
    /// </summary>
    List<int> GetTopPopularProcedures();

    /// <summary>
    /// Получает время освобождения выбранного моечного бокса
    /// </summary>
    DateTime GetWashBoxReleaseTime(int washBoxNumber);

    /// <summary>
    /// Рассчитывает суммарную выручку по выбранной процедуре
    /// </summary>
    decimal CalculateProcedureRevenue(int procedureId);
}
