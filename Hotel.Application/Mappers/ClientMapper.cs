using Hotel.Application.Contracts.Client;
using Hotel.Domain.Entities;

namespace Hotel.Application.Mappers;

/// <summary>
/// Маппер для перевода клиента в DTO и наоборот
/// </summary>
public static class ClientMapper
{
    public static ClientResponseDto ToDto(Client c) => new(
        c.Id,
        c.Name,
        c.Surname,
        c.Patronymic,
        c.PassportNumber,
        c.BirthDate,
        c.Citizenship
    );
    public static Client ToEntity(ClientCreateUpdateDto dto) => new()
    {
        Name = dto.Name!,
        Surname = dto.Surname!,
        Patronymic = dto.Patronymic,
        PassportNumber = dto.PassportNumber!,
        BirthDate = dto.BirthDate!.Value,
        Citizenship = dto.Citizenship!
    };
}
