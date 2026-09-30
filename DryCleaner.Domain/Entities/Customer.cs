namespace DryCleaner.Domain.Entities;

/// <summary>
/// Клиент химчистки
/// </summary>
public class Customer
{
    /// <summary>
    /// Идентификатор клиента
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// Имя клиента
    /// </summary>
    public required string FirstName { get; set; }
    /// <summary>
    /// Отчество клиента
    /// </summary>
    public string? Patronymic { get; set; }
    /// <summary>
    /// Фамилия клиента
    /// </summary>
    public required string LastName { get; set; }
    /// <summary>
    /// Номер телефона клиента
    /// </summary>
    public required string PhoneNumber { get; set; }
}