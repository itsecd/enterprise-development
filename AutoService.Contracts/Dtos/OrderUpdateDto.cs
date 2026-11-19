namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для обновления данных заказа.
/// </summary>
public class OrderUpdateDto
{
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
    /// Дата и время выдачи.
    /// </summary>
    public DateTime? IssuedAt { get; set; }
}