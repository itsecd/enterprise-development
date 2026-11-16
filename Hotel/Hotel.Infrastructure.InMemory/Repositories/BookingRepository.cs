using Hotel.Domain;
using Hotel.Domain.Data;
using Hotel.Domain.Entities;
using System.Linq.Expressions;

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
    public Task<bool> AnyAsync(Expression<Func<Booking, bool>> predicate, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(_bookings.Any(predicate.Compile()));
    }

    /// <inheritdoc/>
    public Task<IReadOnlyList<TResult>> QueryAsync<TResult>(
        Func<IQueryable<Booking>, IQueryable<TResult>> query,
        CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();

        return Task.FromResult<IReadOnlyList<TResult>>(
            query(_bookings.AsQueryable()).ToList());
    }
}