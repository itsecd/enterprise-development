namespace VeterinaryClinic.Application.Contracts.Appointments;

/// <summary>
/// Данные приёма в ответе
/// </summary>
/// <param name="Id">Идентификатор</param>
/// <param name="PetId">Идентификатор питомца</param>
/// <param name="VeterinarianId">Идентификатор ветеринара</param>
/// <param name="AppointmentDate">Дата и время приёма</param>
/// <param name="RoomNumber">Номер кабинета</param>
/// <param name="IsRepeated">Признак повторного приёма</param>
public record AppointmentDto(int Id, int PetId, int VeterinarianId, DateTime AppointmentDate, int RoomNumber,
    bool IsRepeated);
