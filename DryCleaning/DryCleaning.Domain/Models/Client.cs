namespace DryCleaning.Domain.Models;

/// <summary>
/// Клиент
/// </summary>

public class Client
{
    public int Id { get; set; }

    /// <summary>
    /// ФИО клиента
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string PhoneNumber { get; set; }
}
