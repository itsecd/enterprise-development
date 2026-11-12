namespace Carwash.Application.Contracts.Cars;

/// <summary>
/// Dto для Create/Update запросов к автомобилям
/// </summary>
/// <param name="LicensePlate">Государственный номер автомобиля</param>
/// <param name="Brand">Марка автомобиля</param>
/// <param name="ClientId">Идентификатор клиента, которому принадлежит автомобиль</param>
public record CarCreateUpdateDto(
    string LicensePlate,
    string Brand,
    int ClientId
);
