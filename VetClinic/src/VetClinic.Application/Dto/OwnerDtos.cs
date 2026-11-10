namespace VetClinic.Application.DTOs;

/// <summary>
/// DTO владельца
/// </summary>
public class OwnerDto
{
    /// <summary>
    /// Идентификатор владельца
    /// </summary>
    public int Id { get; set; }

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

    /// <summary>
    /// Количество зарегистрированных питомцев
    /// </summary>
    public int PetsCount { get; set; }
}