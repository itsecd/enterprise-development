using Hotel.Domain;
using Hotel.Domain.Data;
using Hotel.Domain.Entities;

namespace Hotel.Infrastructure.InMemory.Repositories;


/// <summary>
/// Репозиторий номеров гостиницы в памяти
/// </summary>
public class RoomRepository : IRepository<Room, int>
{
    /// <summary>
    /// Данные номеров гостиницы
    /// </summary>
    private readonly List<Room> _rooms;

    /// <summary>
    /// Инициализация репозитория данными
    /// </summary>
    public RoomRepository()
    {
        _rooms = HotelData.Rooms.ToList();
    }

    /// <summary>
    /// Получить все номера
    /// </summary>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<IReadOnlyList<Room>> GetAll(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult<IReadOnlyList<Room>>(_rooms.AsReadOnly());
    }

    /// <summary>
    /// Получить номер по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<Room?> GetById(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(_rooms.FirstOrDefault(room => room.Id == id));
    }

    /// <summary>
    /// Добавить новый номер
    /// </summary>
    /// <param name="room">Новый номер</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public Task<Room> Add(Room room, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        _rooms.Add(room);

        return Task.FromResult(room);
    }

    /// <summary>
    /// Обновить существующий номер
    /// </summary>
    /// <param name="room">Обновлённые данные номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<Room?> Update(Room room, CancellationToken cancellationToken = default)
    {
        var existingRoom = await GetById(room.Id, cancellationToken);

        if (existingRoom is null) return null;

        existingRoom.RoomNumber = room.RoomNumber;
        existingRoom.Floor = room.Floor;
        existingRoom.HasBalcony = room.HasBalcony;
        existingRoom.RoomType = room.RoomType;

        return existingRoom;
    }

    /// <summary>
    /// Удалить номер по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор номера</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    public async Task<bool> Delete(int id, CancellationToken cancellationToken = default)
    {
        var room = await GetById(id, cancellationToken);

        if (room is null) return false;

        return _rooms.Remove(room);
    }
}