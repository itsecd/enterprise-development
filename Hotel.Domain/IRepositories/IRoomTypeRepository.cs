using Hotel.Domain;
using Hotel.Domain.Entities;

/// <summary>
/// Наследник базового репозитория для типов номеров
/// </summary>
public interface IRoomTypeRepository : IRepository<RoomType, int> { }