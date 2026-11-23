using System.ComponentModel.DataAnnotations;

namespace Museum.Contracts;

/// <summary>Данные для создания или изменения посетителя.</summary>
public class VisitorRequest
{
    /// <summary>ФИО посетителя.</summary>
    [Required]
    public required string FullName { get; set; }

    /// <summary>Номер телефона посетителя.</summary>
    [Required]
    public required string Phone { get; set; }

    /// <summary>Дата рождения посетителя.</summary>
    public DateOnly BirthDate { get; set; }
}
