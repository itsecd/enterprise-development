namespace AutoService.Domain.Models;

/// <summary>
/// Механик автосервиса
/// </summary>
public class Mechanic
{
    /// <summary>
    /// Уникальный идентификатор механика
    /// </summary>
    public Guid Id { get; init; } = Guid.NewGuid();

    /// <summary>
    /// Номер паспорта
    /// </summary>
    public required string Passport { get; set; }
    
    /// <summary>
    /// ФИО механика
    /// </summary>
    public required string FullName { get; set; }
    
    /// <summary>
    /// Специализация механика
    /// </summary>
    public Specialization Specialization { get; set; }
    
    /// <summary>
    /// Стаж работы, г.
    /// </summary>
    public int Experience { get; set; }
}