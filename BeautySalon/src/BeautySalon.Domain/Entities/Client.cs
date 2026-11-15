namespace BeautySalon.Domain.Entities;

/// <summary>
/// Клиент салона красоты
/// </summary>
public class Client : Person
{
    /// <summary>
    /// Номер телефона
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateOnly BirthDate { get; set; }
}