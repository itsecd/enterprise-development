namespace FoodDelivery.Domain.Entities;

/// <summary>
/// Базовый интерфейс доменной сущности с идентификатором.
/// </summary>
public interface IEntity
{
    public int Id { get; set; }
}
