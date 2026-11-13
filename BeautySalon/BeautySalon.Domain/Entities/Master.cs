using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalon.Domain.Entities;

/// <summary>
/// Мастер
/// </summary>
[Table("masters")]
public class Master : Person
{

    /// <summary>
    /// Идентификатор мастера
    /// </summary>
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// Номер паспорта
    /// </summary>
    [Column("passport_number")]
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Специализация
    /// </summary>
    [Column("specialization")]
    public required string Specialization { get; set; }

    /// <summary>
    /// Стаж работы
    /// </summary>
    [Column("work_experience")]
    public required int WorkExperience { get; set; }
}
