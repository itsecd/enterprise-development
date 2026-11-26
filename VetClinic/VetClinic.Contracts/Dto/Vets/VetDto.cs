namespace VetClinic.Contracts.Dto.Vets;

/// <summary>
/// Ветеринарный врач
/// </summary>
public class VetDto
{
    /// <summary>
    /// Идентификатор врача
    /// </summary>
    public required int Id { get; init; }

    /// <summary>
    /// Номер паспорта в формате 0000 000000
    /// </summary>
    public required string PassportNumber { get; init; }

    /// <summary>
    /// ФИО врача
    /// </summary>
    public required string FullName { get; init; }

    /// <summary>
    /// Год рождения
    /// </summary>
    public required int BirthYear { get; init; }

    /// <summary>
    /// Идентификатор специализации, null если специализация не указана
    /// </summary>
    public int? SpecializationId { get; init; }

    /// <summary>
    /// Стаж работы в годах
    /// </summary>
    public required int WorkExperienceYears { get; init; }
}
