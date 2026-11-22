using DryCleaner.Shared.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace DryCleaner.Domain.Entities;

/// <summary>
/// Заказ
/// </summary>
[Table("order")]
public class Order
{
    /// <summary>
    /// Идентификатор
    /// </summary>
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    [Column("client")]
    public required Client Client { get; set; }

    /// <summary>
    /// Изделие
    /// </summary>
    [Column("product")]
    public required Product Product { get; set; }

    /// <summary>
    /// Время приема заказа
    /// </summary>
    [Column("accepted_at")]
    public required DateTime AcceptedAt { get; set; }

    /// <summary>
    /// Срок выполнения в днях
    /// </summary>
    [Column("processing_days")]
    public int ProcessingDays { get; set; }

    /// <summary>
    /// Статус заказа
    /// </summary>
    [Column("status")]
    public OrderStatus Status { get; set; }

}