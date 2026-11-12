using Hotel.Application.Contracts.Bookings;
using Hotel.Application.Contracts.Rooms;
using Hotel.Domain;
using Hotel.Domain.Entities;

namespace Hotel.Application.Services;

/// <summary>
/// Служба работы с номерами гостиницы
/// </summary>
public class RoomService(IRepository<Room, int> roomRepository, IRepository<RoomType, int> roomTypeRepository, IRepository<Booking, int> bookingRepository) : IRoomService
{
    /// <summary>
    /// Получить все номера
    /// </summary>
    public async Task<IReadOnlyList<RoomDto>> GetAll()
    {
        var rooms = await roomRepository.GetAll();

        return rooms
            .Select(ToDto)
            .ToList();
    }

    /// <summary>
    /// Получить номер по идентификатору
    /// </summary>
    public async Task<RoomDto?> GetById(int id)
    {
        var room = await roomRepository.GetById(id);

        return room is null ? null : ToDto(room);
    }

    /// <summary>
    /// Создать номер
    /// </summary>
    public async Task<RoomDto> Create(CreateRoomDto dto)
    {
        var roomType = await roomTypeRepository.GetById(dto.RoomTypeId);

        if (roomType is null) throw new ArgumentException(
            $"Не удалось найти тип номера с идентификатором {dto.RoomTypeId}");

        var rooms = await roomRepository.GetAll();

        var newId = rooms.Count == 0
            ? 1
            : rooms.Max(room => room.Id) + 1;

        var room = new Room
        {
            Id = newId,
            RoomNumber = dto.RoomNumber,
            Floor = dto.Floor,
            HasBalcony = dto.HasBalcony,
            RoomType = roomType
        };

        var createdRoom = await roomRepository.Add(room);

        return ToDto(createdRoom);
    }

    /// <summary>
    /// Обновить номер
    /// </summary>
    public async Task<RoomDto?> Update(int id, CreateRoomDto dto)
    {
        var existingRoom = await roomRepository.GetById(id);

        if (existingRoom is null) return null;

        var roomType = await roomTypeRepository.GetById(dto.RoomTypeId);

        if (roomType is null) throw new ArgumentException(
            $"Не удалось найти тип номера с идентификатором {dto.RoomTypeId}");

        var room = new Room
        {
            Id = id,
            RoomNumber = dto.RoomNumber,
            Floor = dto.Floor,
            HasBalcony = dto.HasBalcony,
            RoomType = roomType
        };

        var updatedRoom = await roomRepository.Update(room);

        return updatedRoom is null ? null : ToDto(updatedRoom);
    }

    /// <summary>
    /// Удалить номер
    /// </summary>
    public Task<bool> Delete(int id)
    {
        return roomRepository.Delete(id);
    }

    /// <summary>
    /// Получить бронирования указанного номера
    /// </summary>
    public async Task<IReadOnlyList<BookingDto>?> GetBookingsByRoomId(int roomId)
    {
        var room = await roomRepository.GetById(roomId);

        if (room is null) return null;

        var bookings = await bookingRepository.GetAll();

        return bookings
            .Where(booking => booking.Room.Id == roomId)
            .Select(booking => new BookingDto(
                booking.Id,
                booking.Client.Id,
                booking.Room.Id,
                booking.CheckInDate,
                booking.DaysCount))
            .ToList();
    }

    /// <summary>
    /// Получить пять самых часто бронируемых номеров
    /// </summary>
    public async Task<IReadOnlyList<RoomBookingCountDto>> GetTopFiveMostBookedRooms()
    {
        var bookings = await bookingRepository.GetAll();
        var rooms = await roomRepository.GetAll();

        return rooms
            .Select(room => new RoomBookingCountDto(
                ToDto(room),
                bookings.Count(booking => booking.Room.Id == room.Id)))
            .OrderByDescending(item => item.BookingCount)
            .ThenBy(item => item.Room.Id)
            .Take(5)
            .ToList();
    }

    /// <summary>
    /// Получить количество бронирований каждого номера
    /// </summary>
    public async Task<IReadOnlyList<RoomBookingCountDto>> GetBookingCountForEachRoom()
    {
        var bookings = await bookingRepository.GetAll();
        var rooms = await roomRepository.GetAll();

        return rooms
            .Select(room => new RoomBookingCountDto(
                ToDto(room),
                bookings.Count(booking => booking.Room.Id == room.Id)))
            .OrderBy(item => item.Room.Id)
            .ToList();
    }

    /// <summary>
    /// Преобразовать доменную сущность в DTO
    /// </summary>
    private static RoomDto ToDto(Room room)
    {
        return new RoomDto(
            room.Id,
            room.RoomNumber,
            room.Floor,
            room.HasBalcony,
            room.RoomType.Id
        );
    }
}