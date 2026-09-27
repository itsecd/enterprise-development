using AutoServiceLab.Domain.Enums;

namespace AutoServiceLab.Domain.Entities;

/// <summary>
/// Механик автосервиса
/// </summary>
public class Mechanic
{
    /// <summary>
    /// Идентификатор механика
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// ФИО механика
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Специализация механика
    /// </summary>
    public required MechanicSpecialization Specialization { get; set; }

    /// <summary>
    /// Номер паспорта механика
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Номер телефона механика
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Опыт работы в годах
    /// </summary>
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
