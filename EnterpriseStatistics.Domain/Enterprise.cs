namespace EnterpriseStatistics.Domain;

/// <summary>
/// Предприятие
/// </summary>
public class Enterprise
{    
    /// <summary>
    /// Идентификатор предприятия - ОГРН
    /// </summary>
    public required ulong MainStateRegistrationNumber { get; set; }
    /// <summary>
    /// Название
    /// </summary>
    public required string Name { get; set; }
    /// <summary>
    /// Адрес
    /// </summary>
    public required string Address { get; set; }
    /// <summary>
    /// Телефон
    /// </summary>
    public required string Phone { get; set; }
    /// <summary>
    /// Количество работников
    /// </summary>
    public required int EmployeeCount { get; set; }
    /// <summary>
    /// Общая площадь
    /// </summary>
    public required int TotalArea { get; set; }
    /// <summary>
    /// Тип отрасли
    /// </summary>
    public required IndustryTypes IndustryType { get; set; }
    /// <summary>
    /// Форма собственности
    /// </summary>
    public required OwnershipForms OwnershipForm { get; set; }  

}

