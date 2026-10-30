using System.ComponentModel.DataAnnotations;

namespace FitnessGym.Contracts.Dtos;

/// <summary>
/// DTO для создания записи на занятие
/// </summary>
public record TrainingSessionEditDto(
    [Required]
    int ClientId,

    [Required]
    int TrainerId,

    [Required]
    DateTime StartsTraining,

    [Required]
    DateTime EndsTraining,

    [Required]
    string Hall,

    bool IsTrial);