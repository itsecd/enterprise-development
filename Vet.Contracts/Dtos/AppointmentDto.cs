namespace Vet.Contracts.Dtos;

/// <summary>
/// DTO записи на прием для GET запросов
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="Pet">DTO питомца</param>
/// <param name="Veterinarian">DTO ветеринара</param>
/// <param name="AppointmentDateTime">Дата и время приема</param>
/// <param name="RoomNumber">Номер кабинета</param>
/// <param name="IsRepeat">Флаг повторного приема</param>
public sealed record AppointmentDto(int Id, PetDto Pet, VeterinarianDto Veterinarian, DateTime AppointmentDateTime, int RoomNumber, bool IsRepeat);