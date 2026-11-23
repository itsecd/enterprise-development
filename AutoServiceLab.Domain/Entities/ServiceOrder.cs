using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoServiceLab.Domain.Entities;

/// <summary>
/// Заказ на обслуживание автомобиля
/// </summary>
[Table("service_orders")]
public class ServiceOrder
{
    /// <summary>
    /// Идентификатор заказа
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// Дата приема автомобиля
    /// </summary>
    [Column("reception_date")]
    public DateTime ReceptionDate { get; set; } = DateTime.Now;

    /// <summary>
    /// Дата выдачи автомобиля
    /// </summary>
    [Column("issue_date")]
    public DateTime? IssueDate { get; set; }

    /// <summary>
    /// Стоимость
    /// </summary>
    [NotMapped]
    public decimal LaborCost => OrderWorks.Sum(ow => ow.WorkType.LaborCost);

    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    [Column("client_id")]
    public int ClientId { get; set; }

    /// <summary>
    /// Клиент
    /// </summary>
    public required Client Client { get; set; }

    /// <summary>
    /// Идентификатор автомобиля
    /// </summary>
    [Column("car_id")]
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
