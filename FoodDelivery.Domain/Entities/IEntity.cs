namespace FoodDelivery.Domain.Entities;

/// <summary>
/// Определяет общие свойства доменной сущности
/// </summary>
public interface IEntity
{
    /// <summary>
    /// Уникальный идентификатор сущности
    /// </summary>
    int Id { get; set; }
}
