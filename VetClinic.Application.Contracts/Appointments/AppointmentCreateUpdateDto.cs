using VetClinic.Domain.Shared.Enums;
namespace VetClinic.Application.Contracts.Appointments;

/// <summary>
/// DTO входящих данных для оформления или редактирования записи на прием
/// </summary>
/// <param name="DateTime">Дата и время проведения приема</param>
/// <param name="Diagnosis">Предварительный или итоговый диагноз</param>
/// <param name="PetId">Идентификатор питомца-пациента</param>
/// <param name="VetId">Идентификатор лечащего врача</param>
/// <param name="ServiceId">Идентификатор оказываемой медицинской услуги</param>
/// <param name="RoomNumber">Номер смотрового кабинета</param>
/// <param name="RepeatVisitIndicator">Признак первичного или повторного обращения</param>
public record AppointmentCreateUpdateDto(DateTime DateTime, string? Diagnosis, int PetId, int VetId, int ServiceId, int RoomNumber, RepeatVisitIndicator RepeatVisitIndicator);
