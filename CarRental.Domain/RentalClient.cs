namespace CarRental.Domain;

/// <summary>
/// Реализация клиента проката
/// </summary>
/// <param name="passport">Паспорт клиента</param>
/// <param name="fullName">ФИО клиента</param>
/// <param name="birthDate">Дата рождения клиента</param>
public class RentalClient(string passport, string fullName, DateTime birthDate)
{
    /// <summary>
    /// Пасспорт клиента
    /// </summary>
    public string? Passport { get; set; } = passport;
    /// <summary>
    /// ФИО клиента
    /// </summary>
    public string? FullName { get; set; } = fullName;
    /// <summary>
    /// Дата рождения клиента
    /// </summary>
    public DateTime? BirthDate { get; set; } = birthDate;
}
