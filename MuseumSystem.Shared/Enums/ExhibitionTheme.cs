using System.Text.Json.Serialization;

namespace MuseumSystem.Shared.Enums;

/// <summary>
/// Тематика выставки
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ExhibitionTheme
{
    /// <summary>
    /// Историческая
    /// </summary>
    Historical = 0,

    /// <summary>
    /// Художественная
    /// </summary>
    Art = 1,

    /// <summary>
    /// Научная
    /// </summary>
    Science = 2,

    /// <summary>
    /// Этнографическая
    /// </summary>
    Ethnographic = 3,

    /// <summary>
    /// Техническая
    /// </summary>
    Technical = 4,
}