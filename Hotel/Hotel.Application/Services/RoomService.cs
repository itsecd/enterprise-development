using Hotel.Application.Contracts.Bookings;
using Hotel.Application.Contracts.Rooms;
using Hotel.Domain;
using Hotel.Domain.Entities;

namespace Hotel.Application.Services;

/// <summary>
/// Служба работы с номерами гостиницы
/// </summary>
public class RoomService(IRepository<Room, int> roomRepository, IRepository<RoomType, int> roomTypeRepository, IBookingRepository bookingRepository) : IRoomService
{
    /// <summary>
    /// Получить все номера
    /// </summary>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<IReadOnlyList<RoomDto>> GetAll(CancellationToken cancellationToken = default)
    {
        var rooms = await roomRepository.GetAll(cancellationToken);

        return rooms
            .Select(ToDto)
            .ToList();
    }

    /// <summary>
    /// Получить номер по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<RoomDto?> GetById(int id, CancellationToken cancellationToken = default)
    {
        var room = await roomRepository.GetById(id, cancellationToken);

        return room is null ? null : ToDto(room);
    }

    /// <summary>
    /// Создать номер
    /// </summary>
    /// <param name="dto">Данные для создания номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<RoomDto> Create(CreateRoomDto dto, CancellationToken cancellationToken = default)
    {
        var roomType = await roomTypeRepository.GetById(dto.RoomTypeId, cancellationToken);

        if (roomType is null) throw new ArgumentException(
            $"Не удалось найти тип номера с идентификатором {dto.RoomTypeId}");

        var room = new Room
        {
            Id = 0,
            RoomNumber = dto.RoomNumber,
            Floor = dto.Floor,
            HasBalcony = dto.HasBalcony,
            RoomType = roomType
        };

        var createdRoom = await roomRepository.Add(room, cancellationToken);

        return ToDto(createdRoom);
    }

    /// <summary>
    /// Обновить номер
    /// </summary>
    /// <param name="id">Идентификатор номера</param>
    /// <param name="dto">Данные для обновления номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<RoomDto?> Update(int id, CreateRoomDto dto, CancellationToken cancellationToken = default)
    {
        var existingRoom = await roomRepository.GetById(id, cancellationToken);

        if (existingRoom is null) return null;

        var roomType = await roomTypeRepository.GetById(dto.RoomTypeId, cancellationToken);

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

        var updatedRoom = await roomRepository.Update(room, cancellationToken);

        return updatedRoom is null ? null : ToDto(updatedRoom);
    }

    /// <summary>
    /// Удалить номер
    /// </summary>
    /// <param name="id">Идентификатор номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        return roomRepository.Delete(id, cancellationToken);
    }

    /// <summary>
    /// Получить бронирования указанного номера
    /// </summary>
    /// <param name="roomId">Идентификатор номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<IReadOnlyList<BookingDto>?> GetBookingsByRoomId(int roomId, CancellationToken cancellationToken = default)
    {
        var room = await roomRepository.GetById(roomId, cancellationToken);

        if (room is null) return null;

        var bookings = await bookingRepository.GetAll(cancellationToken);

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
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<IReadOnlyList<RoomBookingCountDto>> GetTopFiveMostBookedRooms(CancellationToken cancellationToken = default)
    {
        var bookings = await bookingRepository.GetAll(cancellationToken);
        var rooms = await roomRepository.GetAll(cancellationToken);

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
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<IReadOnlyList<RoomBookingCountDto>> GetBookingCountForEachRoom(CancellationToken cancellationToken = default)
    {
        var countsByRoomId = await bookingRepository.GetBookingCountsByRoomAsync(cancellationToken);

        var rooms = await roomRepository.GetAll(cancellationToken);

        return rooms
            .OrderBy(room => room.Id)
            .Select(room => new RoomBookingCountDto(
                ToDto(room),
                countsByRoomId.GetValueOrDefault(room.Id)))
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