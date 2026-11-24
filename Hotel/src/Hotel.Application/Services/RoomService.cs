using System.ComponentModel.DataAnnotations;
using Hotel.Contracts.Dto;
using Hotel.Contracts.Exceptions;
using Hotel.Contracts.Interfaces;
using Hotel.Domain.Interfaces;
using Hotel.Domain.Models;

namespace Hotel.Application.Services;

/// <summary>Сервис гостиничных номеров и аналитики бронирований.</summary>
/// <param name="roomRepository">Репозиторий номеров.</param>
/// <param name="roomTypeRepository">Репозиторий типов номеров.</param>
/// <param name="bookingRepository">Репозиторий бронирований.</param>
/// <param name="relationshipLock">Защита связанных операций.</param>
public class RoomService(
    IRoomRepository roomRepository,
    IRoomTypeRepository roomTypeRepository,
    IBookingRepository bookingRepository,
    IRelationshipLock relationshipLock) : IRoomService
{
    private readonly IRoomRepository _roomRepository = roomRepository;
    private readonly IRoomTypeRepository _roomTypeRepository = roomTypeRepository;
    private readonly IBookingRepository _bookingRepository = bookingRepository;
    private readonly IRelationshipLock _relationshipLock = relationshipLock;

    public async Task<IReadOnlyList<RoomDto>> GetAllAsync()
    {
        var rooms = await _roomRepository.GetAllAsync();
        return rooms.OrderBy(room => room.RoomNumber, StringComparer.Ordinal)
            .Select(MapToDto).ToList();
    }

    public async Task<RoomDto?> GetByIdAsync(int id)
    {
        var room = await _roomRepository.GetByIdAsync(id);
        return room is null ? null : MapToDto(room);
    }

    public async Task<RoomDto> AddAsync(RoomEditDto request)
    {
        ValidateRoom(request);
        using var operation = await _relationshipLock.AcquireAsync();

        var roomType = await _roomTypeRepository.GetByIdAsync(request.RoomTypeId)
            ?? throw new ValidationException("Указанный тип номера не найден.");

        var rooms = await _roomRepository.GetAllAsync();
        EnsureRoomNumberIsUnique(rooms, request.RoomNumber, null);

        var room = new Room
        {
            RoomNumber = request.RoomNumber.Trim(),
            Floor = request.Floor,
            HasBalcony = request.HasBalcony,
            Type = roomType
        };

        return MapToDto(await _roomRepository.AddAsync(room));
    }

    public async Task<bool> UpdateAsync(int id, RoomEditDto request)
    {
        ValidateRoom(request);
        using var operation = await _relationshipLock.AcquireAsync();

        if (await _roomRepository.GetByIdAsync(id) is null)
            return false;

        var roomType = await _roomTypeRepository.GetByIdAsync(request.RoomTypeId)
            ?? throw new ValidationException("Указанный тип номера не найден.");

        var rooms = await _roomRepository.GetAllAsync();
        EnsureRoomNumberIsUnique(rooms, request.RoomNumber, id);

        var room = new Room
        {
            Id = id,
            RoomNumber = request.RoomNumber.Trim(),
            Floor = request.Floor,
            HasBalcony = request.HasBalcony,
            Type = roomType
        };

        return await _roomRepository.UpdateAsync(room);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        using var operation = await _relationshipLock.AcquireAsync();

        if (await _roomRepository.GetByIdAsync(id) is null)
            return false;

        if (await _bookingRepository.HasBookingsForRoomAsync(id))
            throw new ResourceConflictException("Нельзя удалить номер, для которого существуют бронирования.");

        return await _roomRepository.DeleteAsync(id);
    }

    public async Task<IReadOnlyList<RoomDto>> GetCurrentBookingsAsync(DateTime date)
    {
        var currentDate = date.Date;
        var bookings = await _bookingRepository.GetAllAsync();

        return bookings
            .Where(booking => booking.CheckInDate.Date <= currentDate &&
                              booking.CheckInDate.Date.AddDays(booking.Nights) > currentDate)
            .Select(booking => booking.Room)
            .DistinctBy(room => room.Id)
            .OrderBy(room => room.RoomNumber, StringComparer.Ordinal)
            .Select(MapToDto)
            .ToList();
    }

    public async Task<IReadOnlyList<RoomBookingCountDto>> GetMostBookedAsync()
    {
        var bookings = await _bookingRepository.GetAllAsync();

        return bookings
            .GroupBy(booking => booking.Room.Id)
            .Select(group => new RoomBookingCountDto
            {
                RoomId = group.Key,
                RoomNumber = group.First().Room.RoomNumber,
                BookingCount = group.Count()
            })
            .OrderByDescending(result => result.BookingCount)
            .ThenBy(result => result.RoomNumber, StringComparer.Ordinal)
            .Take(5)
            .ToList();
    }

    public async Task<IReadOnlyList<RoomBookingCountDto>> GetBookingCountsAsync()
    {
        var rooms = await _roomRepository.GetAllAsync();
        var bookings = await _bookingRepository.GetAllAsync();

        return rooms
            .OrderBy(room => room.RoomNumber, StringComparer.Ordinal)
            .Select(room => new RoomBookingCountDto
            {
                RoomId = room.Id,
                RoomNumber = room.RoomNumber,
                BookingCount = bookings.Count(booking => booking.Room.Id == room.Id)
            })
            .ToList();
    }

    private static void ValidateRoom(RoomEditDto request)
    {
        ArgumentNullException.ThrowIfNull(request);
        Validator.ValidateObject(request, new ValidationContext(request), validateAllProperties: true);

        if (string.IsNullOrWhiteSpace(request.RoomNumber))
            throw new ValidationException("Номер комнаты не должен состоять только из пробелов.");
    }

    private static void EnsureRoomNumberIsUnique(IEnumerable<Room> rooms, string roomNumber, int? excludedId)
    {
        var exists = rooms.Any(room => room.Id != excludedId &&
            string.Equals(room.RoomNumber, roomNumber.Trim(), StringComparison.OrdinalIgnoreCase));

        if (exists)
            throw new ResourceConflictException("Комната с таким номером уже существует.");
    }

    private static RoomDto MapToDto(Room room)
    {
        return new RoomDto
        {
            Id = room.Id,
            RoomNumber = room.RoomNumber,
            Floor = room.Floor,
            HasBalcony = room.HasBalcony,
            Type = new RoomTypeDto
            {
                Id = room.Type.Id,
                Category = room.Type.Category,
                Area = room.Type.Area,
                BedCount = room.Type.BedCount,
                HasBathroom = room.Type.HasBathroom,
                PricePerNight = room.Type.PricePerNight
            }
        };
    }
}
