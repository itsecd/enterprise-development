namespace FitnessGym.Contracts.Dtos;

/// <summary>
/// DTO для представления информации о записи клиента на занятие
/// </summary>
/// <param name="Id">Уникальный идентификатор записи</param>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="ClientFullName">ФИО клиента</param>
/// <param name="TrainerId">Идентификатор тренера</param>
/// <param name="TrainerFullName">ФИО тренера</param>
/// <param name="StartsTraining">Дата и время начала занятия</param>
/// <param name="EndsTraining">Дата и время окончания занятия</param>
/// <param name="Hall">Название зала</param>
/// <param name="IsTrial">Является ли посещение пробным</param>
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