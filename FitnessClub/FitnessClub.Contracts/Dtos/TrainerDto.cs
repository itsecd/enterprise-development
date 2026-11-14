namespace FitnessClub.Contracts.Dtos;

/// <summary>
/// Данные тренера фитнес-клуба.
/// </summary>
public class TrainerDto
{
    /// <summary>
    /// ID тренера.
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// Номер паспорта тренера.
    /// </summary>
    public required string PassportNumber { get; set; }

    /// <summary>
    /// ФИО тренера.
    /// </summary>
    public required string FullName { get; set; }

    /// <summary>
    /// Пол тренера.
    /// </summary>
    public GenderDto Gender { get; set; }

    /// <summary>
    /// Дата рождения тренера.
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// ID специализации тренера.
    /// </summary>
    public int SpecializationId { get; set; }

    /// <summary>
    /// Стаж работы тренера в годах.
    /// </summary>
    public int WorkExperienceYears { get; set; }
}