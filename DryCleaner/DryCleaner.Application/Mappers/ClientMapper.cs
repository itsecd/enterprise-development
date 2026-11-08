using DryCleaner.Application.Contracts.Client;
using DryCleaner.Domain.Entities;

namespace DryCleaner.Application.Mappers;

/// <summary>
/// Преобразует сущности Client в объекты ClientDto или ClientDto в сущности Client.
/// </summary>
internal class ClientMapper
{
    /// <summary>
    /// Преобразует сущность Client в объект ClientDto.
    /// </summary>
    /// <param name="entity">Сущность Client для преобразования.</param>
    /// <returns>Экземпляр ClientDto, созданный на основе переданной сущности.</returns>
    public static ClientDto Map(Client entity)
    {
        return new(entity.Id, entity.Name, entity.Phone);
    }

    /// <summary>
    /// Преобразует список сущностей Client в список объектов ClientDto.
    /// </summary>
    /// <param name="entity">Список сущностей Client для преобразования.</param>
    /// <returns>Список объектов ClientDto.</returns>
    public static IList<ClientDto> Map(IList<Client> entity)
    {
        return [.. entity.Select(Map)];
    }

    /// <summary>
    /// Создает новый экземпляр Client на основе данных из ClientCreateUpdateDto.
    /// </summary>
    /// <param name="dto">Объект с данными для создания или обновления клиента.</param>
    /// <returns>Новый экземпляр Client с заполненными свойствами.</returns>
    public static Client Map(ClientCreateUpdateDto dto)
    {
        return new Client { Id = 0, Name = dto.Name, Phone = dto.Phone };
    }
}
