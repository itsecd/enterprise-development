using System.ComponentModel.DataAnnotations;

namespace Hotel.Contracts.Dto;

/// <summary>
/// Данные для создания или изменения клиента
/// </summary>
public class ClientEditDto
{
    /// <summary>
    /// Номер паспорта
    /// </summary>
    [Required, MaxLength(30)]
    public required string PassportNumber { get; set; }

    /// <summary>
    /// ФИО клиента
    /// </summary>
    [Required, MaxLength(150)]
    public required string FullName { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Гражданство
    /// </summary>
    [Required, MaxLength(80)]
    public required string Citizenship { get; set; }
}
