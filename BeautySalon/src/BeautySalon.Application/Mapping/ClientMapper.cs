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
    public static ClientDto ToDto(this Client client) =>
        new(client.Id, client.FullName, client.Gender, client.BirthDate, client.PhoneNumber);

    /// <summary>
    /// Создаёт нового клиента из DTO (идентификатор назначит репозиторий)
    /// </summary>
    public static Client ToEntity(this ClientEditDto dto) =>
        new()
        {
            FullName = dto.FullName,
            Gender = dto.Gender!.Value,
            BirthDate = dto.BirthDate!.Value,
            PhoneNumber = dto.PhoneNumber
        };

    /// <summary>
    /// Переносит данные из DTO в существующего клиента
    /// </summary>
    public static void UpdateEntity(this Client client, ClientEditDto dto)
    {
        client.FullName = dto.FullName;
        client.Gender = dto.Gender!.Value;
        client.BirthDate = dto.BirthDate!.Value;
        client.PhoneNumber = dto.PhoneNumber;
    }
}