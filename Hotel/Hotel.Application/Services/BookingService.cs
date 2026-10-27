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
    public IReadOnlyList<BookingDto> GetAll()
    {
        return bookingRepository.GetAll()
            .Select(ToDto)
            .ToList();
    }

    /// <summary>
    /// Получить бронирование по идентификатору
    /// </summary>
    public BookingDto? GetById(int id)
    {
        var booking = bookingRepository.GetById(id);
        return booking is null ? null : ToDto(booking);
    }

    /// <summary>
    /// Создать бронирование
    /// </summary>
    public BookingDto Create(CreateBookingDto dto)
    {
        var client = clientRepository.GetById(dto.ClientId);

        if (client is null) throw new ArgumentException(
            $"Не удалось найти клиента с идентификатором {dto.ClientId}");

        var room = roomRepository.GetById(dto.RoomId);

        if (room is null) throw new ArgumentException(
            $"Не удалось найти номер с идентификатором {dto.RoomId}");

        if (IsRoomOccupied(dto.RoomId, dto.CheckInDate, dto.DaysCount))
            throw new ArgumentException($"Номер с идентификатором {dto.RoomId} уже забронирован на указанный период");

        var bookings = bookingRepository.GetAll();

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

        return ToDto(bookingRepository.Add(booking));
    }

    /// <summary>
    /// Обновить бронирование
    /// </summary>
    public BookingDto? Update(int id, CreateBookingDto dto)
    {
        var existingBooking = bookingRepository.GetById(id);

        if (existingBooking is null) return null;

        var client = clientRepository.GetById(dto.ClientId);

        if (client is null) throw new ArgumentException(
            $"Не удалось найти клиента с идентификатором {dto.ClientId}");

        var room = roomRepository.GetById(dto.RoomId);

        if (room is null) throw new ArgumentException(
            $"Не удалось найти номер с идентификатором {dto.RoomId}");

        if (IsRoomOccupied(dto.RoomId, dto.CheckInDate, dto.DaysCount, id))
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

        var updatedBooking = bookingRepository.Update(booking);

        return updatedBooking is null ? null : ToDto(updatedBooking);
    }

    /// <summary>
    /// Удалить бронирование
    /// </summary>
    public bool Delete(int id)
    {
        return bookingRepository.Delete(id);
    }

    /// <summary>
    /// Получить текущие бронирования
    /// </summary>
    public IReadOnlyList<BookingDto> GetBookedRooms()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);

        return bookingRepository.GetAll()
            .Where(booking =>
                booking.CheckInDate <= today &&
                booking.CheckInDate.AddDays(booking.DaysCount) > today)
            .Select(ToDto)
            .ToList();
    }

    /// <summary>
    /// Проверить занятость номера на указанный период
    /// </summary>
    private bool IsRoomOccupied(
        int roomId,
        DateOnly checkInDate,
        int daysCount,
        int? excludedBookingId = null)
    {
        var checkOutDate = checkInDate.AddDays(daysCount);

        return bookingRepository.GetAll().Any(booking =>
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