using Museum.Domain;

namespace Museum.Contracts.Dto;

/// <summary>
/// Хранит информацию о билете для ответа API
/// </summary>
public sealed class TicketDto
{
    /// <summary>
    /// Идентификатор билета
    /// </summary>
    public int Id { get; init; }

    /// <summary>
    /// Идентификатор экскурсии
    /// </summary>
    public int TourId { get; init; }

    /// <summary>
    /// Идентификатор посетителя
    /// </summary>
    public int VisitorId { get; init; }

    /// <summary>
    /// Тип билета
    /// </summary>
    public TicketType Type { get; init; }

    /// <summary>
    /// Стоимость билета
    /// </summary>
    public decimal Price { get; init; }
}
