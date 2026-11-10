using BeautySalon.Application.Contracts.Analytics;
using BeautySalon.Application.Contracts.Clients;
using BeautySalon.Application.Contracts.Masters;

namespace BeautySalon.Application.Contracts;

/// <summary>
/// Интерфейс службы для аналитических операций с Dto
/// </summary>
public interface IAnalyticService
{
    /// <summary>
    /// Получает список мастеров с опытом работы не менее 5 лет
    /// </summary>
    /// <returns>Список мастеров</returns>
    public Task<IList<MasterDto>> GetMastersWithExperienceAtLeastFiveYears();

    /// <summary>
    /// Получает свободные окошки для выбранного мастера
    /// </summary>
    /// <param name="masterId">Id мастера</param>
    /// <returns>Список окошек</returns>
    public Task<IList<FreeSlotDto>?> GetFreeSlotsForSelectedMaster(int masterId, DateOnly? date = null);

    /// <summary>
    /// Получает список из пяти самых популярных услуг
    /// </summary>
    /// <returns>Список услуг с кол-вом их выбора</returns>
    public Task<IList<PopularServiceDto>> GetTopFivePopularServices();

    /// <summary>
    /// Получает количество повторных записей клиентов за последний месяц
    /// </summary>
    /// <returns>Кол-во повторных записей</returns>
    public Task<int> GetNumberOfRepeatClientAppointmentsForLastMonth();

    /// <summary>
    /// Получает клиентов, записавшихся к разным мастерам
    /// </summary>
    /// <returns>Список клиентов</returns>
    public Task<IList<ClientDto>> GetClientsRegisteredWithSeveralMasters();
}
