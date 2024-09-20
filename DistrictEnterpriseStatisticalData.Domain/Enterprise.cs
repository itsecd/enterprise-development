namespace DistrictEnterpriseStatisticalData.Domain;

/// <summary>
///     Предприятие
/// </summary>
public class Enterprise
{
    /// <summary>
    ///     Регистрационный номер
    /// </summary>
    public required int RegistrationNumber { get; set; }

    /// <summary>
    ///     Тип отрасли
    /// </summary>
    public required EnterpriseType Type { get; set; }

    /// <summary>
    ///     Наименование
    /// </summary>
    public required string Name { get; set; }

    /// <summary>
    ///     Адрес
    /// </summary>
    public required string Address { get; set; }

    /// <summary>
    ///     Телефон
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    ///     Форма собственности
    /// </summary>
    public required FormOfOwnership Form { get; set; }

    /// <summary>
    ///     Количество работающих
    /// </summary>
    public required int EmployeesNumber { get; set; }

    /// <summary>
    ///     Общая площадь
    /// </summary>
    public required int TotalArea { get; set; }

    /// <summary>
    ///     Поставщики
    /// </summary>
    public List<Supplier> Suppliers { get; set; } = [];

    /// <summary>
    ///     Поставки
    /// </summary>
    public List<Supply> Supplies { get; set; } = [];
}

/// <summary>
///     Типы предприятий
/// </summary>
public enum EnterpriseType
{
    Agriculture,
    Transport,
    LightIndustry,
    HeavyIndustry,
    Building,
    MaterialAndTechnicalSupply
}

/// <summary>
///     Формы собственности
/// </summary>
public enum FormOfOwnership
{
    StateFederal,
    MunicipalUrban,
    TOO,
    Private,
    JointStock
}