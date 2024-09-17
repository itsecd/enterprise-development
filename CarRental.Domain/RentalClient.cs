namespace CarRental.Domain;
/// <summary>
/// Реализация клиента проката
/// </summary>
/// <param name="passport">Паспорт клиента</param>
/// <param name="fullName">ФИО клиента</param>
/// <param name="birthDate">Дата рождения клиента</param>
public class RentalClient(string passport, string fullName, DateTime birthDate)
{
    public string? Passport { get; set; } = passport;
    public string? FullName { get; set; } = fullName;
    public DateTime? BirthDate { get; set; } = birthDate;
}
