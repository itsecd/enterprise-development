namespace BeautySalon.Domain.Models;

/// <summary>
/// Мастер салона красоты.
/// </summary>
public class Master : Person
{
    /// <summary>
    /// Номер паспорта мастера
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// Специализации мастера
    /// </summary>
    public HashSet<string> Specializations { get; set; } = new(StringComparer.OrdinalIgnoreCase);

    /// <summary>
    /// Стаж работы в годах
    /// </summary>
    public required int ExperienceYears { get; set; }

    /// <summary>
    /// Строковое представление
    /// </summary>
    public override string ToString() =>
        $"Master: {base.ToString()}, " +
        $"Passport: {PassportNumber}, " +
        $"Specializations: {string.Join(", ", Specializations)}, " +
        $"Experience: {ExperienceYears} years";
}