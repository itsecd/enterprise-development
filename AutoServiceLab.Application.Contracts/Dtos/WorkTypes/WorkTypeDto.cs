using AutoServiceLab.Shared.Enums;

namespace AutoServiceLab.Application.Contracts.Dtos.WorkTypes;

/// <summary>
/// DTO для вида работ
/// </summary>
/// <param name="Id">Идентификатор вида работ</param>
/// <param name="Name">Название работы</param>
/// <param name="Category">Категория работы</param>
/// <param name="Duration">Продолжительность (часы)</param>
/// <param name="LaborCost">Стоимость работы</param>
public record WorkTypeDto(
    int Id,
    string Name,
    WorkCategory Category,
    decimal Duration,
    decimal LaborCost
);
