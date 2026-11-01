namespace AutoServiceLab.Application.Contracts.DTOs.Clients;

/// <summary>
/// DTO для клиента автосервиса
/// </summary>
/// <param name="Id">Идентификатор клиента</param>
/// <param name="FullName">ФИО клиента</param>
/// <param name="Phone">Номер телефона клиента</param>
public record ClientDto(
    int Id,
    string FullName,
    string Phone
);
