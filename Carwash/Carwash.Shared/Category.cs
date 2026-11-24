using System.Text.Json.Serialization;

namespace Carwash.Shared;

/// <summary>
/// Категория автомобиля
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter<Category>))]
public enum Category
{
    /// <summary>
    /// Стандартная категория автомобиля
    /// </summary>
    Standard,

    /// <summary>
    /// Премиальная категория автомобиля
    /// </summary>
    Premium,

    /// <summary>
    /// Люксовая категория автомобиля
    /// </summary>
    Luxury,

    /// <summary>
    /// Категория внедорожника
    /// </summary>
    Suv,

    /// <summary>
    /// Категория минивэна
    /// </summary>
    Minivan
}
