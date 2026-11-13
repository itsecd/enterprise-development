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
    public required string FullName { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    [Phone(ErrorMessage = "Некорректный формат телефона")]
    public string? Phone { get; set; }
}
