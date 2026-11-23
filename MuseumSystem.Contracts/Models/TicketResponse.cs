using MuseumSystem.Shared.Enums;

namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Данные билета для ответа
/// </summary>
public class TicketResponse
{
    /// <summary>
    /// Уникальный идентификатор билета
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор экскурсии
    /// </summary>
    public int ExcursionId { get; set; }

    /// <summary>
    /// Идентификатор посетителя
    /// </summary>
    public int VisitorId { get; set; }

    /// <summary>
    /// Тип билета (взрослый/льготный)
    /// </summary>
    public TicketType Type { get; set; }

    /// <summary>
    /// Цена билета
    /// </summary>
    public decimal Price { get; set; }
}