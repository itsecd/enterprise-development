using Hotel.Domain;
using Hotel.Domain.Entities;

/// <summary>
/// Наследник базового репозитория для клиентов
/// </summary>
public interface IClientRepository : IRepository<Client, int> { }