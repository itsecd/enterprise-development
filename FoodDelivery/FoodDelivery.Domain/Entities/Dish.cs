using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Domain.Entities;

/// <summary>
/// Блюдо из меню ресторана
/// </summary>
[Table("Dishes")]
public class Dish
{
    /// <summary>
    /// Уникальный идентификатор блюда
    /// </summary>
    [Key]
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор ресторана, которому принадлежит блюдо
    /// </summary>
    public int RestaurantId { get; set; }

    /// <summary>
    /// Название блюда
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Подробное описание блюда и состава (необязательное поле)
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Стоимость блюда в рублях
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Категория блюда
    /// </summary>
    public DishCategory Category { get; set; }

    /// <summary>
    /// Доступно ли блюдо для заказа в данный момент
    /// </summary>
    public bool IsAvailable { get; set; } = true;
}