namespace FitnessGym.Contracts.Dtos;

/// <summary>
/// DTO для представления информации о тренере фитнес-клуба
/// </summary>
public record TrainerDto(
    int Id,
    string PassportNumber,
    string FullName,
    string Gender,
    DateOnly BirthDate,
    int SpecializationId,
    string SpecializationName,
    int ExperienceYears);