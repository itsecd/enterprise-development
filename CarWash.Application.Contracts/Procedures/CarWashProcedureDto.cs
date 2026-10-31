using CarWash.Domain.Shared.Enums;

namespace CarWash.Application.Contracts.Procedures;

/// <summary>
/// DTO для GET запросов к услугам автомойки
/// </summary>
public record CarWashProcedureDto(
    int Id,
    string Name,
    CarCategory CarCategory,
    decimal Price,
    TimeSpan Duration) : IHasId<int>;