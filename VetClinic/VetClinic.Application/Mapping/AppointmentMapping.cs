using VetClinic.Contracts.Dto.Appointments;
using VetClinic.Domain.Models;

namespace VetClinic.Application.Mapping;

/// <summary>
/// Преобразования между записью на прием и её DTO
/// </summary>
public static class AppointmentMapping
{
    /// <summary>
    /// Преобразовать запись на прием в DTO для чтения
    /// </summary>
    /// <param name="entity">Запись на прием</param>
    public static AppointmentDto ToDto(this Appointment entity) => new()
    {
        Id = entity.Id,
        DateTime = entity.DateTime,
        RoomNumber = entity.RoomNumber,
        IsRepeat = entity.IsRepeat,
        PetId = entity.PetId,
        VetId = entity.VetId
    };

    /// <summary>
    /// Создать запись на прием из DTO без связей, связи назначает сервис
    /// </summary>
    /// <param name="dto">Данные записи</param>
    public static Appointment ToEntity(this AppointmentEditDto dto) => new()
    {
        DateTime = dto.DateTime,
        PetId = dto.PetId,
        VetId = dto.VetId,
        RoomNumber = dto.RoomNumber,
        IsRepeat = dto.IsRepeat
    };

    /// <summary>
    /// Перенести простые поля из DTO в существующую запись, связи назначает сервис
    /// </summary>
    /// <param name="entity">Изменяемая запись</param>
    /// <param name="dto">Новые данные</param>
    public static void Apply(this Appointment entity, AppointmentEditDto dto)
    {
        entity.DateTime = dto.DateTime;
        entity.RoomNumber = dto.RoomNumber;
        entity.IsRepeat = dto.IsRepeat;
        entity.PetId = dto.PetId;
        entity.VetId = dto.VetId;
    }
}
