using EnterpriseDevelopment.Contracts.DTOs.Clients;

namespace EnterpriseDevelopment.Application.Interfaces;

/// <summary>
/// Интерфейс сервиса для работы с клиентами
/// </summary>
public interface IClientService
    : ICrudService<ClientDto, CreateClientDto, UpdateClientDto>
{
}
