using CarWash.Domain.Shared.Enums;

namespace CarWash.Application.Contracts.Cars;

/// <summary>
/// DTO для GET запросов к авто
/// </summary>
public record CarDto(
    int Id,
    CarCategory CarCategory,
    string LicensePlate,
    string Brand,
    int CustomerId) : IHasId<int>;