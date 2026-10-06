using System;
using System.Collections.Generic;
using System.Text;

namespace FoodDelivery.Domain;
/// <summary>
/// Заказанное блюдо
/// </summary>
public class Dish
{
    /// <summary>
    /// ID блюда
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Название блюда
    /// </summary>
    public required string Title { get; set; }
    /// <summary>
    /// Вес в граммах
    /// </summary>
    public int Weight { get; set; }
    /// <summary>
    /// Цена блюда
    /// </summary>
    public decimal Price { get; set; }
    /// <summary>
    /// ID категории блюда
    /// </summary>
    public int CategoryId { get; set; }
    /// <summary>
    /// Ссылка на категорию блюда
    /// </summary>
    public required Category Category { get; set; }
}
