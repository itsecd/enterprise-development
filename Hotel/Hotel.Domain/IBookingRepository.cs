using Hotel.Domain.Entities;
using Hotel.Domain.Shared.Enums;

namespace Hotel.Domain;

/// <summary>
/// Репозиторий бронирований
/// </summary>
public interface IBookingRepository : IRepository<Booking, int>
{
    /// <summary>
    /// Проверить, есть ли пересекающееся бронирование номера
    /// </summary>
    /// <param name="roomId">Идентификатор номера</param>
    /// <param name="checkInDate">Дата заезда</param>
    /// <param name="checkOutDate">Дата выезда</param>
    /// <param name="excludedBookingId">Идентификатор бронирования, которое пропускаем при проверке</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<bool> HasOverlappingBookingAsync(int roomId, DateOnly checkInDate, DateOnly checkOutDate, int? excludedBookingId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить клиентов, бронировавших номер указанной категории
    /// </summary>
    /// <param name="category">Категория номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<IReadOnlyList<HotelClient>> GetClientsByRoomCategoryAsync(RoomCategory category, CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить пять клиентов с наибольшей общей стоимостью проживания
    /// </summary>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<IReadOnlyList<(HotelClient Client, decimal TotalStayCost)>> GetTopFiveClientsByTotalStayCostAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Получить количество бронирований по идентификаторам номеров
    /// </summary>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<IReadOnlyDictionary<int, int>> GetBookingCountsByRoomAsync(CancellationToken cancellationToken = default);
}