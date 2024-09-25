namespace EnterpriseStatistics.Domain;
/// <summary>
/// Поставщик
/// </summary>
public class Supplier
{
    /// <summary>
    /// Идентификатор поставщика
    /// </summary>
    public required int Id { get; set; }
    /// <summary>
    /// ФИО
    /// </summary>
    public required string FullName { get; set; }
    /// <summary>
    /// Адрес
    /// </summary>
    public required string Address { get; set; }
    /// <summary>
    /// Телефон
    /// </summary>
    public required string Phone { get; set; }

}
