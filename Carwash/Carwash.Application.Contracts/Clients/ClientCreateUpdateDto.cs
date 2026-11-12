namespace Carwash.Application.Contracts.Clients;

/// <summary>
/// Dto для Create/Update запрсов к клиентам
/// </summary>
/// <param name="FullName"> полное имя клиента</param>
/// <param name="PhoneNumber"> номер телефона клиента</param>
public record ClientCreateUpdateDto(
    string FullName,
    string PhoneNumber
);
