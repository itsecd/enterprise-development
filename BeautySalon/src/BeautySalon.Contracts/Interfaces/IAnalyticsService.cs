using BeautySalon.Contracts.Dto;

namespace BeautySalon.Contracts.Interfaces;

/// <summary>
/// Аналитические запросы по данным салона красоты
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Мастера со стажем не менее указанного
    /// </summary>
    /// <param name="minExperienceYears">Минимальный стаж в годах</param>
    public Task<IReadOnlyList<MasterDto>> GetExperiencedMastersAsync(
        int minExperienceYears = 5, CancellationToken cancellationToken = default);

    /// <summary>
    /// Свободные окна мастера на выбранную дату (рабочий день 09:00–18:00)
    /// </summary>
    /// <param name="masterId">Идентификатор мастера</param>
    /// <param name="date">Дата</param>
    /// <returns>Список свободных окон. Если мастера нет, сервис выбрасывает исключение «не найдено»</returns>
    public Task<IReadOnlyList<TimeSlotDto>> GetFreeSlotsAsync(
        int masterId, DateOnly date, CancellationToken cancellationToken = default);

    /// <summary>
    /// Топ наиболее популярных услуг по количеству записей
    /// </summary>
    /// <param name="top">Сколько услуг вернуть</param>
    public Task<IReadOnlyList<PopularServiceDto>> GetPopularServicesAsync(
        int top = 5, CancellationToken cancellationToken = default);

    /// <summary>
    /// Клиенты, записавшиеся более одного раза в выбранном месяце
    /// </summary>
    /// <param name="year">Год</param>
    /// <param name="month">Месяц (1–12)</param>
    public Task<IReadOnlyList<RepeatClientDto>> GetRepeatClientsAsync(
        int year, int month, CancellationToken cancellationToken = default);

    /// <summary>
    /// Клиенты, записанные к нескольким мастерам, упорядоченные по дате рождения
    /// </summary>
    public Task<IReadOnlyList<ClientDto>> GetClientsWithMultipleMastersAsync(
        CancellationToken cancellationToken = default);
}