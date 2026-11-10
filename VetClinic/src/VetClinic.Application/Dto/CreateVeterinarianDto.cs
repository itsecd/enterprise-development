namespace VetClinic.Application.DTOs;

/// <summary>
/// Данные для создания или обновления ветеринара
/// </summary>
public class CreateVeterinarianDto
{
    /// <summary>
    /// Номер паспорта врача
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// ФИО врача
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Год рождения
    /// </summary>
    public int BirthYear { get; set; }

    /// <summary>
    /// Стаж работы в годах
    /// </summary>
    public int ExperienceYears { get; set; }

    /// <summary>
    /// Идентификатор специализации
    /// </summary>
    public int SpecializationId { get; set; }
}