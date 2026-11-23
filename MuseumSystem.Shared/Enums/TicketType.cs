using System.Text.Json.Serialization;

namespace MuseumSystem.Shared.Enums;

/// <summary>
/// Тип билета
/// </summary>
[JsonConverter(typeof(JsonStringEnumConverter))]
public enum TicketType
{
    /// <summary>
    /// Взрослый
    /// </summary>
    Adult = 0,

    /// <summary>
    /// Льготный
    /// </summary>
    Discounted = 1,
}