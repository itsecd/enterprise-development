using FitnessClub.Contracts.Interfaces;

namespace FitnessClub.Contracts.Dtos.Read;
/// <summary>
/// Данные бронирования тренировки
/// </summary>
/// <param name="Id">Идентификатор брони</param>
/// <param name="ClientId">Идентификатор клиента</param>
/// <param name="TrainerId">Идентификатор тренера</param>
/// <param name="HallId">Идентификатор зала</param>
/// <param name="DateTime">Дата и время тренировки</param>
/// <param name="IsTrial">Пробная ли тренировка</param>
/// <param name="Duration">Длительность тренировки</param>
public record BookingDto
(
    int Id,
    int ClientId,
    int TrainerId,
    int HallId,
    DateTime DateTime,
    bool IsTrial,
    TimeSpan Duration
) : IHasId;