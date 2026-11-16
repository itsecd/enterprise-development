using Hotel.Application.Contracts.Bookings;
using Hotel.Domain;
using Hotel.Domain.Entities;

namespace Hotel.Application.Services;

/// <summary>
/// Служба работы с бронированиями гостиницы
/// </summary>
public class BookingService(IBookingRepository bookingRepository, IRepository<HotelClient, int> clientRepository, IRepository<Room, int> roomRepository) : IBookingService
{
    /// <summary>
    /// Получить все бронирования
    /// </summary>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<IReadOnlyList<BookingDto>> GetAll(CancellationToken cancellationToken = default)
    {
        var bookings = await bookingRepository.GetAll(cancellationToken);

        return bookings
            .Select(ToDto)
            .ToList();
    }

    /// <summary>
    /// Получить бронирование по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор бронирования</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<BookingDto?> GetById(int id, CancellationToken cancellationToken = default)
    {
        var booking = await bookingRepository.GetById(id, cancellationToken);
        return booking is null ? null : ToDto(booking);
    }

    /// <summary>
    /// Создать бронирование
    /// </summary>
    /// <param name="dto">Данные для создания бронирования</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<BookingDto> Create(CreateBookingDto dto, CancellationToken cancellationToken = default)
    {
        var client = await clientRepository.GetById(dto.ClientId, cancellationToken);

        if (client is null) throw new ArgumentException(
            $"Не удалось найти клиента с идентификатором {dto.ClientId}");

        var room = await roomRepository.GetById(dto.RoomId, cancellationToken);

        if (room is null) throw new ArgumentException(
            $"Не удалось найти номер с идентификатором {dto.RoomId}");

        if (await IsRoomOccupiedAsync(dto.RoomId, dto.CheckInDate, dto.DaysCount, cancellationToken: cancellationToken))
            throw new ArgumentException(
                $"Номер с идентификатором {dto.RoomId} уже забронирован на указанный период");

        var booking = new Booking
        {
            Id = 0,
            Client = client,
            Room = room,
            CheckInDate = dto.CheckInDate,
            DaysCount = dto.DaysCount
        };

        var createdBooking = await bookingRepository.Add(booking, cancellationToken);

        return ToDto(createdBooking);
    }

    /// <summary>
    /// Обновить бронирование
    /// </summary>
    /// <param name="id">Идентификатор бронирования</param>
    /// <param name="dto">Данные для обновления бронирования</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<BookingDto?> Update(int id, CreateBookingDto dto, CancellationToken cancellationToken = default)
    {
        var existingBooking = await bookingRepository.GetById(id, cancellationToken);

        if (existingBooking is null) return null;

        var client = await clientRepository.GetById(dto.ClientId, cancellationToken);

        if (client is null) throw new ArgumentException(
            $"Не удалось найти клиента с идентификатором {dto.ClientId}");

        var room = await roomRepository.GetById(dto.RoomId, cancellationToken);

        if (room is null) throw new ArgumentException(
            $"Не удалось найти номер с идентификатором {dto.RoomId}");

        if (await IsRoomOccupiedAsync(dto.RoomId, dto.CheckInDate, dto.DaysCount, id, cancellationToken))
        {
            throw new ArgumentException(
                $"Номер с идентификатором {dto.RoomId} уже забронирован на указанный период");
        }

        var booking = new Booking
        {
            Id = id,
            Client = client,
            Room = room,
            CheckInDate = dto.CheckInDate,
            DaysCount = dto.DaysCount
        };

        var updatedBooking = await bookingRepository.Update(booking, cancellationToken);

        return updatedBooking is null ? null : ToDto(updatedBooking);
    }

    /// <summary>
    /// Удалить бронирование
    /// </summary>
    /// <param name="id">Идентификатор бронирования</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        return bookingRepository.Delete(id, cancellationToken);
    }

    /// <summary>
    /// Получить текущие бронирования
    /// </summary>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<IReadOnlyList<BookingDto>> GetBookedRooms(CancellationToken cancellationToken = default)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var bookings = await bookingRepository.GetAll(cancellationToken);

        return bookings
            .Where(booking =>
                booking.CheckInDate <= today &&
                booking.CheckInDate.AddDays(booking.DaysCount) > today)
            .Select(ToDto)
            .ToList();
    }

    /// <summary>
    /// Проверить занятость номера на указанный период
    /// </summary>
    /// <param name="roomId">Идентификатор номера</param>
    /// <param name="checkInDate">Дата заезда</param>
    /// <param name="daysCount">Количество дней проживания</param>
    /// <param name="excludedBookingId">Идентификатор бронирования, которое пропускаем при проверке</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    private Task<bool> IsRoomOccupiedAsync(int roomId, DateOnly checkInDate, int daysCount, int? excludedBookingId = null, CancellationToken cancellationToken = default)
    {
        var checkOutDate = checkInDate.AddDays(daysCount);

        return bookingRepository.AnyAsync(booking =>
            booking.Room.Id == roomId &&
            booking.Id != excludedBookingId &&
            booking.CheckInDate < checkOutDate &&
            booking.CheckInDate.AddDays(booking.DaysCount) > checkInDate, cancellationToken);
    }

    /// <summary>
    /// Преобразовать доменную сущность в DTO
    /// </summary>
    private static BookingDto ToDto(Booking booking)
    {
        return new BookingDto(
            booking.Id,
            booking.Client.Id,
            booking.Room.Id,
            booking.CheckInDate,
            booking.DaysCount
        );
    }
}