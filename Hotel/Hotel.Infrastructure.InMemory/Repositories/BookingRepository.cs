using Hotel.Domain;
using Hotel.Domain.Data;
using Hotel.Domain.Entities;

namespace Hotel.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий бронирований гостиницы в памяти
/// </summary>
public class BookingRepository : IRepository<Booking, int>
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
    public Task<IReadOnlyList<Booking>> GetAll()
    {
        return Task.FromResult<IReadOnlyList<Booking>>(_bookings.AsReadOnly());
    }

    /// <summary>
    /// Получить бронирование по идентификатору
    /// </summary>
    public Task<Booking?> GetById(int id)
    {
        return Task.FromResult(_bookings.FirstOrDefault(booking => booking.Id == id));
    }

    /// <summary>
    /// Добавить новое бронирование
    /// </summary>
    public Task<Booking> Add(Booking booking)
    {
        _bookings.Add(booking);

        return Task.FromResult(booking);
    }

    /// <summary>
    /// Обновить существующее бронирование
    /// </summary>
    public Task<Booking?> Update(Booking booking)
    {
        var index = _bookings.FindIndex(item => item.Id == booking.Id);

        if (index == -1) return Task.FromResult<Booking?>(null);

        _bookings[index] = booking;

        return Task.FromResult<Booking?>(booking);
    }

    /// <summary>
    /// Удалить бронирование по идентификатору
    /// </summary>
    public async Task<bool> Delete(int id)
    {
        var booking = await GetById(id);

        if (booking is null) return false;

        return _bookings.Remove(booking);
    }
}