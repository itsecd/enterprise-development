namespace Carwash.Application.Contracts.Cars;

/// <summary>
/// Dto для Get запросов к автомобилям
/// </summary>
/// <param name="Id">Уникальный идентификатор автомобиля</param>
/// <param name="LicensePlate">Государственный номер автомобиля</param>
/// <param name="Brand">Марка автомобиля</param>
/// <param name="ClientId">Идентификатор клиента, которому принадлежит автомобиль</param>
public record CarDto(
    int Id,
    string LicensePlate,
    string Brand,
    int ClientId
);
