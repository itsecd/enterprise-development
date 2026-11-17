using Hotel.Domain;
using Hotel.Domain.Data;
using Hotel.Domain.Entities;
using Hotel.Domain.Shared.Enums;

namespace Hotel.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий бронирований гостиницы в памяти
/// </summary>
public class BookingRepository : IBookingRepository
{
    /// <summary>
    /// Список бронирований
    /// </summary>
    private readonly List<Booking> _bookings;

    /// <summary>
    /// Инициализировать репозиторий исходными данными
    /// </summary>
    public BookingRepository()
    {
        _bookings = HotelData.Bookings.ToList();
    }

    /// <summary>
    /// Получить все бронирования
    /// </summary>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<IReadOnlyList<Booking>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult<IReadOnlyList<Booking>>(_bookings.AsReadOnly());
    }

    /// <summary>
    /// Получить бронирование по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор бронирования</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<Booking?> GetById(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(_bookings.FirstOrDefault(booking => booking.Id == id));
    }

    /// <summary>
    /// Добавить новое бронирование
    /// </summary>
    /// <param name="booking">Новое бронирование</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<Booking> Add(Booking booking, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _bookings.Add(booking);

        return Task.FromResult(booking);
    }

    /// <summary>
    /// Обновить существующее бронирование
    /// </summary>
    /// <param name="booking">Обновлённое бронирование</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<Booking?> Update(Booking booking, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var index = _bookings.FindIndex(item => item.Id == booking.Id);

        if (index == -1) return Task.FromResult<Booking?>(null);

        _bookings[index] = booking;

        return Task.FromResult<Booking?>(booking);
    }

    /// <summary>
    /// Удалить бронирование по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор бронирования</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        var booking = await GetById(id, cancellationToken);

        if (booking is null) return false;

        return _bookings.Remove(booking);
    }

    /// <inheritdoc/>
    public Task<bool> HasOverlappingBookingAsync(int roomId, DateOnly checkInDate, DateOnly checkOutDate, int? excludedBookingId, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult(_bookings.Any(booking =>
            booking.Room.Id == roomId &&
            booking.Id != excludedBookingId &&
            booking.CheckInDate < checkOutDate &&
            booking.CheckInDate.AddDays(booking.DaysCount) > checkInDate));
    }

    /// <inheritdoc/>
    public Task<IReadOnlyList<HotelClient>> GetClientsByRoomCategoryAsync(RoomCategory category, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var clients = _bookings
            .Where(booking => booking.Room.RoomType.Category == category)
            .Select(booking => booking.Client)
            .Distinct()
            .OrderBy(client => client.LastName)
            .ThenBy(client => client.FirstName)
            .ThenBy(client => client.MiddleName)
            .ToList();

        return Task.FromResult<IReadOnlyList<HotelClient>>(clients);
    }

    /// <inheritdoc/>
    public Task<IReadOnlyList<(HotelClient Client, decimal TotalStayCost)>> GetTopFiveClientsByTotalStayCostAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var topClients = _bookings
            .GroupBy(booking => booking.Client.Id)
            .Select(group => (
                Client: group.First().Client,
                TotalStayCost: group.Sum(booking => booking.Room.RoomType.PricePerDay * booking.DaysCount)))
            .OrderByDescending(item => item.TotalStayCost)
            .ThenBy(item => item.Client.Id)
            .Take(5)
            .ToList();

        return Task.FromResult<IReadOnlyList<(HotelClient Client, decimal TotalStayCost)>>(topClients);
    }

    /// <inheritdoc/>
    public Task<IReadOnlyDictionary<int, int>> GetBookingCountsByRoomAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        var counts = _bookings
            .GroupBy(booking => booking.Room.Id)
            .ToDictionary(group => group.Key, group => group.Count());

        return Task.FromResult<IReadOnlyDictionary<int, int>>(counts);
    }
}