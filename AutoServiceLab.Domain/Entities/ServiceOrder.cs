namespace AutoServiceLab.Domain.Entities;

/// <summary>
/// Заказ на обслуживание автомобиля
/// </summary>
public class ServiceOrder
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Дата приема автомобиля
    /// </summary>
    public DateTime ReceptionDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Дата выдачи автомобиля
    /// </summary>
    public DateTime? IssueDate { get; set; }

    /// <summary>
    /// Стоимость
    /// </summary>
    public decimal LaborCost => OrderWorks.Sum(ow => ow.WorkType.LaborCost);

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int ClientId { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Идентификатор автомобиля
    /// </summary>
    public int CarId { get; set; }

    /// <summary>
    /// Автомобиль
    /// </summary>
    public required Car Car { get; set; }

    /// <summary>
    /// Список работ заказа
    /// </summary>
    public List<OrderWork> OrderWorks { get; set; } = [];

    /// <summary>
    /// Список механиков заказа
    /// </summary>
    public List<OrderMechanic> OrderMechanics { get; set; } = [];

    /// <summary>
    /// Возвращает строковое представление заказа
    /// </summary>
    public override string ToString() => $"Заказ #{Id}";
}
