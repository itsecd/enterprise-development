namespace BeautySalon.Domain.Entities;

/// <summary>
/// Специалист салона красоты
/// </summary>
public class BeautySpecialist : Person
{
    /// <summary>
    /// Номер паспорта
    /// </summary>
    public string? PassportNumber { get; set; }

    /// <summary>
    /// Специализация
    /// </summary>
    public string? Specialization { get; set; }

    /// <summary>
    /// Стаж работы в годах
    /// </summary>
    public int WorkExperience { get; set; }
}
