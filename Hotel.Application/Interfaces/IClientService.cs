using Hotel.Contracts.DTO;
using Hotel.Contracts.Request;

namespace Hotel.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса для управления клиентами
/// </summary>
public interface IClientService : ICrudService<ClientRequest, ClientDto>
{
}