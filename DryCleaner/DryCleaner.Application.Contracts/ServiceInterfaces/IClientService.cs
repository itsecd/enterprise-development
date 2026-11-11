using DryCleaner.Application.Contracts.Client;

namespace DryCleaner.Application.Contracts.ServiceInterfaces;

/// <summary>
/// Определяет контракт для работы с клиентом
/// </summary>
public interface IClientService : IAplicationService<ClientDto, ClientCreateUpdateDto, int>;
