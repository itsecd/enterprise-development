using MuseumSystem.Shared.Enums;
using MuseumSystem.Domain.Interfaces;

namespace MuseumSystem.Domain.Entities;

/// <summary>
/// Билет на экскурсию (в качестве контракта)
/// </summary>
public class Ticket : IEntity
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Внешний ключ на экскурсию (для БД и сидинга)
    /// </summary>
    public int ExcursionId { get; set; }

    /// <summary>
    /// Экскурсия, на которую куплен билет
    /// </summary>
    public required Excursion Excursion { get; set; }

    /// <summary>
    /// Внешний ключ на посетителя (для БД и сидинга)
    /// </summary>
    public int VisitorId { get; set; }

    /// <summary>
    /// Посетитель, купивший билет
    /// </summary>
    public required Visitor Visitor { get; set; }

    /// <summary>
    /// Тип билета
    /// </summary>
    public required TicketType Type { get; set; }

    /// <summary>
    /// Цена билета
    /// </summary>
    public required decimal Price { get; set; }
}