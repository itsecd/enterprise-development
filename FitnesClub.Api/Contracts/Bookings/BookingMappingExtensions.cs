using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Contracts.Bookings;

/// <summary>
/// Методы расширения для преобразования между доменной сущностью <see cref="Booking"/> и DTO записей на тренировки.
/// </summary>
public static class BookingMappingExtensions
{
    /// <summary>
    /// Преобразует доменную сущность бронирования занятия в объект передачи данных <see cref="BookingDto"/>.
    /// </summary>
    /// <param name="b">Сущность бронирования занятия.</param>
    /// <returns>DTO с полными данными о записи на тренировку.</returns>
    public static BookingDto ToDto(this Booking b) => new(
        b.Id,
        b.MemberId,
        b.TrainerId,
        b.LessonDateTime,
        b.Duration,
        b.RoomName,
        b.IsTrial
    );

    /// <summary>
    /// Создает новый экземпляр доменной сущности <see cref="Booking"/> на основе входящего DTO.
    /// </summary>
    /// <param name="dto">Входные данные для создания бронирования.</param>
    /// <returns>Новая сущность бронирования с новым идентификатором <see cref="Guid"/>.</returns>
    public static Booking ToEntity(this BookingCreateUpdateDto dto) => new()
    {
        Id = Guid.NewGuid(),
        MemberId = dto.MemberId,
        TrainerId = dto.TrainerId,
        LessonDateTime = dto.LessonDateTime,
        Duration = dto.Duration,
        RoomName = dto.RoomName,
        IsTrial = dto.IsTrial,
    };

    /// <summary>
    /// Обновляет поля существующей сущности бронирования значениями из переданного DTO.
    /// </summary>
    /// <param name="dto">DTO с актуальными данными бронирования.</param>
    /// <param name="b">Существующая сущность бронирования, поля которой будут перезаписаны.</param>
    public static void UpdateEntity(this BookingCreateUpdateDto dto, Booking b)
    {
        b.MemberId = dto.MemberId;
        b.TrainerId = dto.TrainerId;
        b.LessonDateTime = dto.LessonDateTime;
        b.Duration = dto.Duration;
        b.RoomName = dto.RoomName;
        b.IsTrial = dto.IsTrial;
    }
}