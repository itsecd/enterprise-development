namespace AutoService.Contracts.Dtos;

/// <summary>
/// DTO для результата подсчета стоимости заказа.
/// </summary>
public class OrderTotalReportDto
{
    /// <summary>
    /// Идентификатор заказа.
    /// </summary>
    public Guid OrderId { get; set; }

    /// <summary>
    /// Суммарная стоимость работ.
    /// </summary>
    public decimal TotalCost { get; set; }
}
