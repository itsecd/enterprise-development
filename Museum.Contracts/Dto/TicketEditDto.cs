using System.ComponentModel.DataAnnotations;
using Museum.Domain;

namespace Museum.Contracts.Dto;

/// <summary>
/// Хранит данные для создания и изменения билета
/// </summary>
public sealed class TicketEditDto
{
    /// <summary>
    /// Идентификатор экскурсии
    /// </summary>
    [Range(1, int.MaxValue)]
    public int TourId { get; init; }

    /// <summary>
    /// Идентификатор посетителя
    /// </summary>
    [Range(1, int.MaxValue)]
    public int VisitorId { get; init; }

    /// <summary>
    /// Тип билета
    /// </summary>
    [EnumDataType(typeof(TicketType))]
    public TicketType Type { get; init; }

    /// <summary>
    /// Стоимость билета
    /// </summary>
    [Range(typeof(decimal), "0.01", "1000000", ParseLimitsInInvariantCulture = true, ConvertValueInInvariantCulture = true)]
    public decimal Price { get; init; }
}
