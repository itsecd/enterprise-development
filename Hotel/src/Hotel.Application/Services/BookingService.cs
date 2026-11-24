using System.ComponentModel.DataAnnotations;
using Hotel.Contracts.Dto;
using Hotel.Contracts.Exceptions;
using Hotel.Contracts.Interfaces;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Models;

namespace Hotel.Application.Services;

/// <summary>Сервис бронирований гостиницы.</summary>
/// <param name="bookingRepository">Репозиторий бронирований.</param>
/// <param name="clientRepository">Репозиторий клиентов.</param>
/// <param name="roomRepository">Репозиторий номеров.</param>
/// <param name="relationshipLock">Защита связанных операций.</param>
public class BookingService(
    IBookingRepository bookingRepository,
    IClientRepository clientRepository,
    IRoomRepository roomRepository,
    IRelationshipLock relationshipLock) : IBookingService
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IClientRepository _clientRepository = clientRepository;
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IRelationshipLock _relationshipLock = relationshipLock;

    public async Task<IReadOnlyList<BookingDto>> GetAllAsync()
    {
        var bookings = await _bookingRepository.GetAllAsync();
        return bookings.OrderBy(booking => booking.CheckInDate)
            .ThenBy(booking => booking.Id).Select(MapToDto).ToList();
    }

    public async Task<BookingDto?> GetByIdAsync(int id)
    {
        var booking = await _bookingRepository.GetByIdAsync(id);
        return booking is null ? null : MapToDto(booking);
    }

    public async Task<BookingDto> AddAsync(BookingEditDto request)
    {
        ValidateBooking(request);
        using var operation = await _relationshipLock.AcquireAsync();

        var client = await _clientRepository.GetByIdAsync(request.ClientId)
            ?? throw new ValidationException("Указанный клиент не найден.");
        var room = await _roomRepository.GetByIdAsync(request.RoomId)
            ?? throw new ValidationException("Указанный номер не найден.");

        var bookings = await _bookingRepository.GetAllAsync();
        EnsureRoomIsAvailable(bookings, room.Id, request.CheckInDate, request.Nights, null);

        var booking = new Booking
        {
            Client = client,
            Room = room,
            CheckInDate = request.CheckInDate.Date,
            Nights = request.Nights
        };

        return MapToDto(await _bookingRepository.AddAsync(booking));
    }

    public async Task<bool> UpdateAsync(int id, BookingEditDto request)
    {
        ValidateBooking(request);
        using var operation = await _relationshipLock.AcquireAsync();

        if (await _bookingRepository.GetByIdAsync(id) is null)
            return false;

        var client = await _clientRepository.GetByIdAsync(request.ClientId)
            ?? throw new ValidationException("Указанный клиент не найден.");
        var room = await _roomRepository.GetByIdAsync(request.RoomId)
            ?? throw new ValidationException("Указанный номер не найден.");

        var bookings = await _bookingRepository.GetAllAsync();
        EnsureRoomIsAvailable(bookings, room.Id, request.CheckInDate, request.Nights, id);

        var booking = new Booking
        {
            Id = id,
            Client = client,
            Room = room,
            CheckInDate = request.CheckInDate.Date,
            Nights = request.Nights
        };

        return await _bookingRepository.UpdateAsync(booking);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var operation = await _relationshipLock.AcquireAsync();
        return await _bookingRepository.DeleteAsync(id);
    }

    private static void ValidateBooking(BookingEditDto request)
    {
        ArgumentNullException.ThrowIfNull(request);
        Validator.ValidateObject(request, new ValidationContext(request), validateAllProperties: true);

        if (request.CheckInDate == default)
            throw new ValidationException("Необходимо указать дату заезда.");

        try
        {
            _ = request.CheckInDate.Date.AddDays(request.Nights);
        }
        catch (ArgumentOutOfRangeException)
        {
            throw new ValidationException("Дата выезда выходит за допустимый диапазон.");
        }
    }

    private static void EnsureRoomIsAvailable(
        IEnumerable<Booking> bookings,
        int roomId,
        DateTime checkInDate,
        int nights,
        int? excludedBookingId)
    {
        var newStart = checkInDate.Date;
        var newEnd = newStart.AddDays(nights);
        var hasOverlap = bookings.Any(existing =>
        {
            if (existing.Id == excludedBookingId || existing.Room.Id != roomId)
                return false;

            var existingStart = existing.CheckInDate.Date;
            var existingEnd = existingStart.AddDays(existing.Nights);
            return existingStart < newEnd && newStart < existingEnd;
        });

        if (hasOverlap)
            throw new ResourceConflictException("Выбранный номер уже забронирован на часть указанного периода.");
    }

    private static BookingDto MapToDto(Booking booking)
    {
        var roomType = booking.Room.Type;

        return new BookingDto
        {
            Id = booking.Id,
            Client = new ClientDto
            {
                Id = booking.Client.Id,
                PassportNumber = booking.Client.PassportNumber,
                FullName = booking.Client.FullName,
                BirthDate = booking.Client.BirthDate,
                Citizenship = booking.Client.Citizenship
            },
            Room = new RoomDto
            {
                Id = booking.Room.Id,
                RoomNumber = booking.Room.RoomNumber,
                Floor = booking.Room.Floor,
                HasBalcony = booking.Room.HasBalcony,
                Type = new RoomTypeDto
                {
                    Id = roomType.Id,
                    Category = roomType.Category,
                    Area = roomType.Area,
                    BedCount = roomType.BedCount,
                    HasBathroom = roomType.HasBathroom,
                    PricePerNight = roomType.PricePerNight
                }
            },
            CheckInDate = booking.CheckInDate,
            Nights = booking.Nights,
            CheckOutDate = booking.CheckInDate.Date.AddDays(booking.Nights),
            TotalPrice = booking.Nights * roomType.PricePerNight
        };
    }
}
