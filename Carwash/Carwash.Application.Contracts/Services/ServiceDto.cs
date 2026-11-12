using Carwash.Shared;

namespace Carwash.Application.Contracts.Services;

/// <summary>
/// Dto для Get запросов к услугам
/// </summary>
/// <param name="Id">Уникальный идентификатор услуги</param>
/// <param name="Name">Название услуги</param>
/// <param name="Price">Цена услуги</param>
/// <param name="Category">Категория услуги</param>
/// <param name="DurationInMinutes">Продолжительность услуги в минутах</param>
public record ServiceDto(
    int Id,
    string Name,
    decimal Price,
    Category Category,
    int DurationInMinutes
);
