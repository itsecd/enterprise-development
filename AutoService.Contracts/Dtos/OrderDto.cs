namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для передачи данных о заказе.
/// </summary>
public class OrderDto
{
    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Идентификатор автомобиля.
    /// </summary>
    public Guid CarId { get; set; }

    /// <summary>
    /// Идентификатор клиента.
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Идентификатор механика.
    /// </summary>
    public Guid MechanicId { get; set; }

    /// <summary>
    /// Список идентификаторов работ.
    /// </summary>
    public List<Guid> WorkIds { get; set; } = new();

    /// <summary>
    /// Дата и время приема.
    /// </summary>
    public DateTime AcceptedAt { get; set; }

    /// <summary>
    /// Дата и время выдачи (null, если заказ еще не выдан).
    /// </summary>
    public DateTime? IssuedAt { get; set; }
}