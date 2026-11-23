using AutoServiceLab.Shared.Enums;

namespace AutoServiceLab.Application.Contracts.Dtos.WorkTypes;

/// <summary>
/// DTO для создания или обновления вида работ
/// </summary>
/// <param name="Name">Название работы</param>
/// <param name="Category">Категория работы</param>
/// <param name="Duration">Продолжительность (часы)</param>
/// <param name="LaborCost">Стоимость работы</param>
public record WorkTypeCreateUpdateDto(
    string Name,
    WorkCategory Category,
    decimal Duration,
    decimal LaborCost
);
