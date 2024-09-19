namespace DistrictEnterpriseStatisticalData.Domain;

/// <summary>
///     Поставщик
/// </summary>
public class Supplier
{
    /// <summary>
    ///     Идентификатор поставщика
    /// </summary>
    public required int Id { get; set; }
    
    /// <summary>
    /// Имя поставщика
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Список предприятий, которым осуществляется поставка
    /// </summary>
    public List<Enterprise> Enterprises { get; set; } = [];

    /// <summary>
    ///     Список поставок
    /// </summary>
    public List<Supply> Supplies { get; set; } = [];
}