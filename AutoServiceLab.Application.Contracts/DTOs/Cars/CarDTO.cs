namespace AutoServiceLab.Application.Contracts.DTOs.Cars;

/// <summary>
/// DTO для автомобиля
/// </summary>
/// <param name="Id">Идентификатор автомобиля</param>
/// <param name="LicensePlate">Гос. номер</param>
/// <param name="Brand">Марка</param>
/// <param name="Model">Модель</param>
/// <param name="Year">Год выпуска</param>
/// <param name="ClientId">Идентификатор владельца</param>
public record CarDto(
    int Id,
    string LicensePlate,
    string Brand,
    string Model,
    int Year,
    int ClientId
);
