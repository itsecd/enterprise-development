using BeautySalon.Contracts.Dto;
using BeautySalon.Domain.Entities;

namespace BeautySalon.Application.Mapping;

/// <summary>
/// Преобразования между клиентом и его DTO
/// </summary>
public static class ClientMapper
{
    /// <summary>
    /// Преобразует клиента в DTO для чтения
    /// </summary>
    /// <param name="client">Клиент</param>
    /// <returns>DTO клиента</returns>
    public static ClientDto ToDto(this Client client) =>
        new(client.Id, client.FullName, client.Gender, client.BirthDate, client.PhoneNumber);

    /// <summary>
    /// Создаёт нового клиента из DTO (идентификатор назначит репозиторий)
    /// </summary>
    /// <param name="dto">Данные для создания</param>
    /// <returns>Новый клиент</returns>
    public static Client ToEntity(this ClientEditDto dto) =>
        new()
        {
            FullName = dto.FullName,
            Gender = dto.Gender,
            BirthDate = dto.BirthDate,
            PhoneNumber = dto.PhoneNumber
        };

    /// <summary>
    /// Переносит данные из DTO в существующего клиента
    /// </summary>
    /// <param name="client">Обновляемый клиент</param>
    /// <param name="dto">Новые данные</param>
    public static void UpdateEntity(this Client client, ClientEditDto dto)
    {
        client.FullName = dto.FullName;
        client.Gender = dto.Gender;
        client.BirthDate = dto.BirthDate;
        client.PhoneNumber = dto.PhoneNumber;
    }
}
