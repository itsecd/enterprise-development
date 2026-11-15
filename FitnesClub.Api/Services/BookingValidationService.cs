using FitnesClub.Api.Contracts.Bookings;
using FitnesClub.Domain.Entities;
using FitnesClub.Domain.Interfaces;

namespace FitnesClub.Api.Services;

/// <summary>
/// Реализация сервиса валидации записей на тренировки.
/// </summary>
/// <param name="trainerRepo">Репозиторий тренеров.</param>
/// <param name="memberRepo">Репозиторий клиентов.</param>
/// <param name="bookingRepo">Репозиторий записей на тренировки.</param>
public class BookingValidationService(
    IRepository<Trainer, Guid> trainerRepo,
    IRepository<Member, Guid> memberRepo,
    IRepository<Booking, Guid> bookingRepo) : IBookingValidationService
{
    /// <inheritdoc />
    public string? Validate(BookingCreateUpdateDto dto, Guid? currentBookingId = null)
    {
        var trainer = trainerRepo.Read(dto.TrainerId);
        if (trainer is null)
            return $"Тренер с ID {dto.TrainerId} не существует.";

        var member = memberRepo.Read(dto.MemberId);
        if (member is null)
            return $"Клиент с ID {dto.MemberId} не существует.";

        if (dto.LessonDateTime < member.MembershipStartDate || dto.LessonDateTime > member.MembershipEndDate)
        {
            return $"У клиента {member.LastName} {member.FirstName} не действует абонемент на дату {dto.LessonDateTime:dd.MM.yyyy HH:mm}. Срок действия: с {member.MembershipStartDate:dd.MM.yyyy} по {member.MembershipEndDate:dd.MM.yyyy}.";
        }

        var newStart = dto.LessonDateTime;
        var newEnd = dto.LessonDateTime + dto.Duration;

        var trainerBookings = bookingRepo.ReadAll()
            .Where(b => b.TrainerId == dto.TrainerId && b.Id != currentBookingId);

        foreach (var b in trainerBookings)
        {
            var existingStart = b.LessonDateTime;
            var existingEnd = b.LessonDateTime + b.Duration;

            if (newStart < existingEnd && newEnd > existingStart)
            {
                return $"Тренер занят в это время! Уже есть тренировка с {existingStart:dd.MM.yyyy HH:mm} по {existingEnd:HH:mm}.";
            }
        }

        return null;
    }
}