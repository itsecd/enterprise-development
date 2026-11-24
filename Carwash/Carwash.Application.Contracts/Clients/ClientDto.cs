using Carwash.Application.Contracts.Cars;

namespace Carwash.Application.Contracts.Clients;

/// <summary>
/// Dto для Get запросов к клиентам
/// </summary>
/// <param name="Id"> идентификатор клиента</param>
/// <param name="FullName"> полное имя клиента</param>
/// <param name="PhoneNumber"> номер телефона клиента</param>
/// <param name="Cars"> список машин клиента</param>
public record ClientDto(
    int Id,
    string FullName,
    string PhoneNumber,
    List<CarDto> Cars
);
