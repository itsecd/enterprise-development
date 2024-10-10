using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DistrictEnterpriseStatisticalData.Domain.Entity;

/// <summary>
///     Предприятие
/// </summary>
[Table("enterprise")]
public class Enterprise
{
    /// <summary>
    ///     Регистрационный номер
    /// </summary>
    [Key]
    public int RegistrationNumber { get; set; }

    /// <summary>
    ///     Тип отрасли
    /// </summary>
    [Column("enterprise_type_id")]
    public int EnterpriseTypeId { get; set; }

    public virtual EnterpriseType Type { get; set; } = null!;

    /// <summary>
    ///     Наименование
    /// </summary>
    [Column("name")]
    [MaxLength(50)]
    public required string Name { get; set; }

    /// <summary>
    ///     Адрес
    /// </summary>
    [Column("address")]
    [MaxLength(50)]
    public required string Address { get; set; }

    /// <summary>
    ///     Телефон
    /// </summary>
    [Column("phone_number")]
    [MaxLength(11)]
    public required string PhoneNumber { get; set; }

    /// <summary>
    ///     Форма собственности
    /// </summary>
    [Column("form")]
    public int FormId { get; set; }

    public virtual FormOfOwnership Form { get; set; } = null!;

    /// <summary>
    ///     Количество работающих
    /// </summary>
    [Column("employees_number")]
    public required int EmployeesNumber { get; set; }

    /// <summary>
    ///     Общая площадь
    /// </summary>
    [Column("total_area")]
    public required int TotalArea { get; set; }

    /// <summary>
    ///     Поставки
    /// </summary>
    [Column("supplies")]
    public virtual List<Supply> Supplies { get; set; } = [];
}