using BeautySalon.Contracts.Interfaces;
using BeautySalon.Shared.Enums;

namespace BeautySalon.Contracts.Dto;

/// <summary>
/// Услуга салона (для чтения)
/// </summary>
/// <param name="Id">Уникальный идентификатор</param>
/// <param name="Name">Название услуги</param>
/// <param name="Category">Категория услуги</param>
/// <param name="Price">Цена услуги</param>
/// <param name="Duration">Продолжительность услуги</param>
public record ServiceDto(
    int Id,
    string Name,
    Category Category,
    decimal Price,
    TimeSpan Duration) : IHasId;