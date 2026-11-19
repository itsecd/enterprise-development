namespace FoodDelivery.Domain;

/// <summary>
/// Класс ресторана
/// </summary>
public class Restaurant
{
    /// <summary>
    /// ID ресторана
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Название ресторана
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    /// Адрес ресторана
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    /// Рейтинг ресторана
    /// </summary>
    public double Rating { get; set; }

    /// <summary>
    /// Часы работы ресторана
    /// </summary>
    public required string WorkingHours { get; set; }
}
