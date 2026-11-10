using Museum.Shared;

namespace Museum.Domain;

/// <summary>
/// Билет на экскурсию
/// </summary>
public class Ticket
{
    /// <summary>
    /// Идентификатор билета
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Экскурсия, на которую куплен билет
    /// </summary>
    public required Tour Tour { get; set; }

    /// <summary>
    /// Посетитель, купивший билет
    /// </summary>
    public required Visitor Visitor { get; set; }

    /// <summary>
    /// Тип билета (взрослый или льготный)
    /// </summary>
    public TicketType Type { get; set; }

    /// <summary>
    /// Цена билета
    /// </summary>
    public decimal Price { get; set; }
}
