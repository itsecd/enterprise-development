using BeautySalon.Shared.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySalon.Domain.Entities;

/// <summary>
/// Человек
/// </summary>
public class Person
{
    /// <summary>
    /// ФИО
    /// </summary>
    [Column("full_name")]
    public required string FullName {  get; set; }

    /// <summary>
    /// Пол
    /// </summary>
    [Column("gender")]
    public required Gender Gender { get; set; }
}
