namespace FoodDelivery.Application.Contracts.DTOs;

/// <summary>
/// DTO категории блюда
/// </summary>
/// <param name="Id">Идентификатор категории</param>
/// <param name="Name">Название категории</param>
public record CategoryDto(int Id, string Name);
