using System.ComponentModel.DataAnnotations;
using MuseumSystem.Shared.Enums;

namespace MuseumSystem.Contracts.Models;

/// <summary>
/// Данные для обновления билета
/// </summary>
public class TicketUpdateRequest
{
    /// <summary>
    /// Идентификатор экскурсии
    /// </summary>
    [Required]
    public int ExcursionId { get; set; }

    /// <summary>
    /// Идентификатор посетителя
    /// </summary>
    [Required]
    public int VisitorId { get; set; }

    /// <summary>
    /// Тип билета (взрослый/льготный)
    /// </summary>
    [Required]
    public TicketType Type { get; set; }

    /// <summary>
    /// Цена билета
    /// </summary>
    [Range(0, 1000000)]
    public decimal Price { get; set; }
}