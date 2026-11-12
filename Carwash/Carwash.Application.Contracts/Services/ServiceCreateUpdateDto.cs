using Carwash.Shared;

namespace Carwash.Application.Contracts.Services;

/// <summary>
/// Dto для Create/Update запросов к услугам
/// </summary>
/// <param name="Name">Название услуги</param>
/// <param name="Price">Цена услуги</param>
/// <param name="Category">Категория услуги</param>
/// <param name="DurationInMinutes">Продолжительность услуги в минутах</param>
public record ServiceCreateUpdateDto(
    string Name,
    decimal Price,
    Category Category,
    int DurationInMinutes
);
