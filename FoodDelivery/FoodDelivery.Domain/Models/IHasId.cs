namespace FoodDelivery.Domain.Models;

/// <summary>
/// Сущность с целочисленным идентификатором
/// </summary>
public interface IHasId
{
    /// <summary>
    /// Идентификатор сущности
    /// </summary>
    int Id { get; }
}
