using Hotel.Contracts.DTO;

namespace Hotel.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса для выполнения аналитических запросов
/// </summary>
public interface IAnalyticsService
{
    /// <summary>
    /// Получение клиентов, проживавших в номерах указанного типа
    /// </summary>
    /// <param name="roomTypeId">Id типа комнаты</param>
    /// <returns>Список клиентов</returns>
    public Task<IReadOnlyList<ClientDto>> GetClientsByRoomTypeAsync(int roomTypeId);

    /// <summary>
    /// Получение номеров, занятых в указанную дату
    /// </summary>
    /// <param name="date">Дата проверки</param>
    /// <returns>Список номеров</returns>
    public Task<IReadOnlyList<int>> GetBookedRoomsAsync(DateTime date);

    /// <summary>
    /// Получение топ 5 наиболее часто бронируемых номеров
    /// </summary>
    /// <returns>Список номеров</returns>
    public Task<IReadOnlyList<int>> GetTop5BookedRoomsAsync();

    /// <summary>
    /// Получение количества бронирований для каждого номера
    /// </summary>
    /// <returns>Список с количеством бронирований</returns>
    public Task<IReadOnlyList<WorkFrequencyDto>> GetBookingsCountPerRoomAsync();

    /// <summary>
    /// Получение топ 5 клиентов по суммарной стоимости проживания
    /// </summary>
    /// <returns>Список клиентов с суммами</returns>
    public Task<IReadOnlyList<TopClientDto>> GetTop5ClientsByStayAsync();
}