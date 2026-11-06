
using FitnessClub.Domain.Enums;

namespace FitnessClub.Api.Dtos.Trainers;

/// <summary>
/// Данные тренера для передачи через API.
/// </summary>
public record TrainerDto(
    int Id,
    string PassportNumber,
    string FullName,
    Gender Gender,
    DateOnly BirthDate,
    int SpecializationId,
    string SpecializationName,
    int WorkExperience);
