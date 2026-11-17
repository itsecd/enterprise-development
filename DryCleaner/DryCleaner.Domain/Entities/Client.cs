using System.ComponentModel.DataAnnotations.Schema;

namespace DryCleaner.Domain.Entities;

/// <summary>
/// Клиент химчистки
/// </summary>
[Table("students")]
public class Client
{
    /// <summary>
    /// Идинтификатор
    /// </summary
    [Column("id")]
    public required int Id { get; set; }

    /// <summary>
    /// ФИО клиента
    /// </summary>
    [Column("name")]
    public required string Name { get; set; }

    /// <summary>
    /// Номер клиента
    /// </summary>
    [Column("phone")]
    public string? Phone { get; set; }
}