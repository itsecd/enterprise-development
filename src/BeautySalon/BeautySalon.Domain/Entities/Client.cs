namespace BeautySalon.Domain.Entities;

/// <summary>
/// Класс клиента, реализующий класс Person.
/// </summary>
public class Client : Person
{
    /// <summary>
    /// Номер телефона клиента.
    /// </summary>
    public required string PhoneNumber { get; set; }

    /// <summary>
    /// Дата рождения клиента.
    /// </summary>
    public required DateTime BirthDate { get; set; }
}
