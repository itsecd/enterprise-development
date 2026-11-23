using AutoServiceLab.Shared.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AutoServiceLab.Domain.Entities;

/// <summary>
/// Механик автосервиса
/// </summary>
[Table("mechanics")]
public class Mechanic
{
    /// <summary>
    /// Идентификатор механика
    /// </summary>
    [Key]
    [Column("id")]
    public int Id { get; set; }

    /// <summary>
    /// ФИО механика
    /// </summary>
    [Column("full_name")]
    public required string FullName { get; set; }

    /// <summary>
    /// Специализация механика
    /// </summary>
    [Column("specialization")]
    public required MechanicSpecialization Specialization { get; set; }

    /// <summary>
    /// Номер паспорта механика
    /// </summary>
    [Column("passport_number")]
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Номер телефона механика
    /// </summary>
    [Column("phone")]
    public required string Phone { get; set; }

    /// <summary>
    /// Опыт работы в годах
    /// </summary>
    [Column("experience_years")]
    public int ExperienceYears { get; set; }

    /// <summary>
    /// Работы механика
    /// </summary>
    public List<OrderMechanic> OrderMechanics { get; set; } = [];

    /// <summary>
    /// Возвращает строковое представление механика
    /// </summary>
    public override string ToString() => $"{FullName} ({Specialization})";
}
