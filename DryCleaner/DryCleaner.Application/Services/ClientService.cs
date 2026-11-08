using DryCleaner.Application.Contracts.Client;
using DryCleaner.Application.Contracts.ServiceInterfaces;
using DryCleaner.Application.Mappers;
using DryCleaner.Domain.Entities;
using DryCleaner.Domain.Interfaces;

namespace DryCleaner.Application.Services;

/// <summary>
/// Сервис для управления клиентами.
/// </summary>
/// <param name="clientRepository">Репозиторий для доступа к данным клиентов.</param>
public class ClientService(IRepository<Client, int> clientRepository) : IClientService
{
    /// <summary>
    /// Создает нового клиента на основе предоставленных данных.
    /// </summary>
    /// <param name="dto">Данные для создания или обновления клиента.</param>
    /// <returns>DTO созданного клиента.</returns>
    public async Task<ClientDto> Create(ClientCreateUpdateDto dto)
    {
        var client = ClientMapper.Map(dto);

        var result = await clientRepository.Create(client);

        return ClientMapper.Map(result);
    }

    /// <summary>
    /// Возвращает клиента по идентификатору или null, если клиент не найден.
    /// </summary>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>Объект клиента или null, если клиент не найден.</returns>
    public async Task<ClientDto?> Get(int id)
    {
        var client = await clientRepository.Read(id);

        if (client == null)
            return null;

        return ClientMapper.Map(client);
    }

    /// <summary>
    /// Получает список всех клиентов в виде объектов ClientDto.
    /// </summary>
    /// <returns>Список объектов ClientDto.</returns>
    public async Task<IList<ClientDto>> GetAll()
    {
        return ClientMapper.Map(await clientRepository.ReadAll());
    }

    /// <summary>
    /// Обновляет данные клиента по идентификатору.
    /// </summary>
    /// <param name="dto">Данные для обновления клиента.</param>
    /// <param name="id">Идентификатор клиента.</param>
    /// <returns>Обновлённые данные клиента.</returns>
    public async Task<ClientDto> Update(ClientCreateUpdateDto dto, int id)
    {
        var client = ClientMapper.Map(dto);
        client.Id = id;

        var result = await clientRepository.Update(client);

        return ClientMapper.Map(result);
    }

    /// <summary>
    /// Удаляет клиента по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор клиента для удаления.</param>
    /// <returns>Значение true, если клиент успешно удалён; в противном случае — false.</returns>
    public async Task<bool> Delete(int id)
    {
        var result = await clientRepository.Delete(id);

        return result;
    }
}
