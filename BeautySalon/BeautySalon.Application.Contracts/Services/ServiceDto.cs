namespace BeautySalon.Application.Contracts.Services;

/// <summary>
/// Dto для get запросов к услугам
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Name">Название</param>
/// <param name="Category">Категория</param>
/// <param name="Price">Стоимость</param>
/// <param name="Duration">Длительность</param>
public record ServiceDto(int Id, string Name, string Category, decimal Price, int Duration);
