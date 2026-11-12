using Hotel.Application.Contracts.Bookings;
using Hotel.Domain;
using Hotel.Domain.Entities;

namespace Hotel.Application.Services;

/// <summary>
/// Служба работы с бронированиями гостиницы
/// </summary>
public class BookingService(IRepository<Booking, int> bookingRepository, IRepository<HotelClient, int> clientRepository, IRepository<Room, int> roomRepository) : IBookingService
{
    /// <summary>
    /// Получить все бронирования
    /// </summary>
    public async Task<IReadOnlyList<BookingDto>> GetAll()
    {
        var bookings = await bookingRepository.GetAll();

        return bookings
            .Select(ToDto)
            .ToList();
    }

    /// <summary>
    /// Получить бронирование по идентификатору
    /// </summary>
    public async Task<BookingDto?> GetById(int id)
    {
        var booking = await bookingRepository.GetById(id);
        return booking is null ? null : ToDto(booking);
    }

    /// <summary>
    /// Создать бронирование
    /// </summary>
    public async Task<BookingDto> Create(CreateBookingDto dto)
    {
        var client = await clientRepository.GetById(dto.ClientId);

        if (client is null) throw new ArgumentException(
            $"Не удалось найти клиента с идентификатором {dto.ClientId}");

        var room = await roomRepository.GetById(dto.RoomId);

        if (room is null) throw new ArgumentException(
            $"Не удалось найти номер с идентификатором {dto.RoomId}");

        var bookings = await bookingRepository.GetAll();

        if (IsRoomOccupied(bookings, dto.RoomId, dto.CheckInDate, dto.DaysCount))
            throw new ArgumentException($"Номер с идентификатором {dto.RoomId} уже забронирован на указанный период");

        var newId = bookings.Count == 0
            ? 1
            : bookings.Max(booking => booking.Id) + 1;

        var booking = new Booking
        {
            Id = newId,
            Client = client,
            Room = room,
            CheckInDate = dto.CheckInDate,
            DaysCount = dto.DaysCount
        };

        var createdBooking = await bookingRepository.Add(booking);

        return ToDto(createdBooking);
    }

    /// <summary>
    /// Обновить бронирование
    /// </summary>
    public async Task<BookingDto?> Update(int id, CreateBookingDto dto)
    {
        var existingBooking = await bookingRepository.GetById(id);

        if (existingBooking is null) return null;

        var client = await clientRepository.GetById(dto.ClientId);

        if (client is null) throw new ArgumentException(
            $"Не удалось найти клиента с идентификатором {dto.ClientId}");

        var room = await roomRepository.GetById(dto.RoomId);

        if (room is null) throw new ArgumentException(
            $"Не удалось найти номер с идентификатором {dto.RoomId}");

        var bookings = await bookingRepository.GetAll();

        if (IsRoomOccupied(bookings, dto.RoomId, dto.CheckInDate, dto.DaysCount, id))
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

        var updatedBooking = await bookingRepository.Update(booking);

        return updatedBooking is null ? null : ToDto(updatedBooking);
    }

    /// <summary>
    /// Удалить бронирование
    /// </summary>
    public Task<bool> Delete(int id)
    {
        return bookingRepository.Delete(id);
    }

    /// <summary>
    /// Получить текущие бронирования
    /// </summary>
    public async Task<IReadOnlyList<BookingDto>> GetBookedRooms()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var bookings = await bookingRepository.GetAll();

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
    private static bool IsRoomOccupied(
        IReadOnlyList<Booking> bookings,
        int roomId,
        DateOnly checkInDate,
        int daysCount,
        int? excludedBookingId = null)
    {
        var checkOutDate = checkInDate.AddDays(daysCount);

        return bookings.Any(booking =>
            booking.Room.Id == roomId &&
            booking.Id != excludedBookingId &&
            checkInDate < booking.CheckInDate.AddDays(booking.DaysCount) &&
            booking.CheckInDate < checkOutDate);
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