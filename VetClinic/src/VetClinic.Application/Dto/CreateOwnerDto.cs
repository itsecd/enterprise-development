namespace VetClinic.Application.DTOs;

/// <summary>
/// Данные для создания владельца
/// </summary>
public class CreateOwnerDto
{
    /// <summary>
    /// ФИО владельца
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string Phone { get; set; }

    /// <summary>
    /// Адрес проживания
    /// </summary>
    public string? Address { get; set; }
}