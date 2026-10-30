namespace FitnessGym.Contracts.Dtos;

/// <summary>
/// DTO для представления информации о записи клиента на занятие
/// </summary>
public record TrainingSessionDto(
    int Id,
    int ClientId,
    string ClientFullName,
    int TrainerId,
    string TrainerFullName,
    DateTime StartsTraining,
    DateTime EndsTraining,
    string Hall,
    bool IsTrial);