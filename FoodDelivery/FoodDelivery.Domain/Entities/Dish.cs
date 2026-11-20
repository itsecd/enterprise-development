using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Domain.Entities;

/// <summary>
/// Блюдо из меню ресторана
/// </summary>
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
    /// Идентификатор категории блюда
    /// </summary>
    public int CategoryId { get; set; }

    /// <summary>
    /// Вес блюда в граммах
    /// </summary>
    public int Weight { get; set; }

    /// <summary>
    /// Стоимость блюда в рублях
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// Категория блюда
    /// </summary>
    public DishCategory Category { get; set; }

    /// <summary>
    /// Ресторан, в меню которого входит блюдо
    /// </summary>
    public Restaurant? Restaurant { get; set; }
}