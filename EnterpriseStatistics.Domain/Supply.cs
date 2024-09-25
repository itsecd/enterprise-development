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
    /// Идентификатор поставщика
    /// </summary>
    public required int IdSupplier { get; set; }
    /// <summary>
    /// Идентификатор предприятия
    /// </summary>
    public required int IdEnterprise { get; set; }
    /// <summary>
    /// Колличество единиц сырья
    /// </summary>
    public required int Quanity { get; set; }
    /// <summary>
    /// Дата поставки
    /// </summary>
    public required DateTime Date { get; set; }
}
