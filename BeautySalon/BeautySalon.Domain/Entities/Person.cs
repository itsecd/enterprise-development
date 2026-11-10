using BeautySalon.Shared.Enum;

namespace BeautySalon.Domain.Entities;

/// <summary>
/// Человек
/// </summary>
public class Person
{
    /// <summary>
    /// ФИО
    /// </summary>
    public required string FullName {  get; set; }

    /// <summary>
    /// Пол
    /// </summary>
    public required Gender Gender { get; set; }
}
