using Hotel.Domain;
using Hotel.Domain.Entities;

/// <summary>
/// Наследник базового репозитория для номеров
/// </summary>
public interface IRoomRepository : IRepository<Room, int> { }