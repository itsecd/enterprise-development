namespace AutoService.Domain.Models;

/// <summary>
/// Заказ в автосервисе, который всё связывает - клиента, его авто, механика и список работ
/// </summary>
public class Order
{
    /// <summary>
    /// Уникальный идентификатор заказа
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Ссылка на авто по Id
    /// </summary>
    public Guid CarId { get; set; }

    /// <summary>
    /// Ссылка на клиента
    /// </summary>
    public Guid ClientId { get; set; }

    /// <summary>
    /// Ссылка на механика
    /// </summary>
    public Guid MechanicId { get; set; }

    /// <summary>
    /// Список видов работ
    /// </summary>
    public required List<WorkType> Works { get; set; } = [];

    /// <summary>
    /// Дата и время приёма авто
    /// </summary>
    public DateTime AcceptedAt { get; set; }

    /// <summary>
    /// Дата и время выдачи авто (null - не выдан)
    /// </summary>
    public DateTime? IssuedAt { get; set; }

    /// <summary>
    /// Итоговая стоимость
    /// </summary>
    public decimal TotalCost => Works.Sum(w => w.Price);
}