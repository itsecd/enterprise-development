using System.ComponentModel.DataAnnotations;

namespace CarWash.Contracts.Dto;

/// <summary>
/// Данные для создания и изменения клиента
/// </summary>
public class ClientEditDto
{
    /// <summary>
    /// ФИО клиента
    /// </summary>
    [Required(ErrorMessage = "ФИО обязательно")]
    [MaxLength(200, ErrorMessage = "ФИО не может быть длиннее 200 символов")]
    public required string FullName { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    [Phone(ErrorMessage = "Некорректный формат телефона")]
    [MaxLength(30, ErrorMessage = "Телефон не может быть длиннее 30 символов")]
    public string? Phone { get; set; }
}
