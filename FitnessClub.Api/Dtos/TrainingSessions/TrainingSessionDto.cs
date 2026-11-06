
namespace FitnessClub.Api.Dtos.TrainingSessions;

/// <summary>
/// Данные записи на занятие для передачи через API.
/// </summary>
public record TrainingSessionDto(
    int Id,
    int ClientId,
    string ClientName,
    int TrainerId,
    string TrainerName,
    DateTime StartsAt,
    DateTime? EndsAt,
    int GymId,
    string GymName,
    bool IsTrial);
