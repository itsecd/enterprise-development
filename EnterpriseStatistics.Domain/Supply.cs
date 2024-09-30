namespace EnterpriseStatistics.Domain;
/// <summary>
/// Поставка
/// </summary>
public class Supply
{
    /// <summary>
    /// Идентификатор поставки
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// Поставщик
    /// </summary>
    public required Supplier Supplier { get; set; }
    /// <summary>
    /// Предприятие
    /// </summary>
    public required Enterprise Enterprise { get; set; }
    /// <summary>
    /// Количество единиц сырья
    /// </summary>
    public required int Quanity { get; set; }
    /// <summary>
    /// Дата поставки
    /// </summary>
    public required DateTime Date { get; set; }
}
