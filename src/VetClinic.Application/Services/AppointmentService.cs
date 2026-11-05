using Microsoft.Extensions.Logging;
using VetClinic.Application.Mapping;
using VetClinic.Application.Validation;
using VetClinic.Contracts.Appointments;
using VetClinic.Domain.Exceptions;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Application.Services;

public sealed class AppointmentService(
    IAppointmentRepository appointments,
    IBreedRepository breeds,
    TimeProvider time,
    ILogger<AppointmentService> logger) : IAppointmentService
{
    public async Task<IReadOnlyList<AppointmentDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение списка записей на приём");
        var result = (await appointments.GetAllAsync(cancellationToken)).Select(ClinicMapper.ToAppointment).ToArray();
        logger.LogInformation("Список записей на приём получен, записей: {Count}", result.Length);
        return result;
    }

    public async Task<AppointmentDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение записи на приём {AppointmentId}", id);
        var appointment = await appointments.FindByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException("запись на приём", id);
        logger.LogInformation("Запись на приём {AppointmentId} получена", id);
        return ClinicMapper.ToAppointment(appointment);
    }

    public async Task<IReadOnlyList<AppointmentDto>> GetByVeterinarianAsync(int veterinarianId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение приёмов врача {VeterinarianId}", veterinarianId);
        var result = (await appointments.GetByVeterinarianAsync(veterinarianId, cancellationToken)).Select(ClinicMapper.ToAppointment).ToArray();
        logger.LogInformation("Приёмы врача {VeterinarianId} получены, записей: {Count}", veterinarianId, result.Length);
        return result;
    }

    public async Task<IReadOnlyList<AppointmentDto>> GetByPetAsync(int petId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение приёмов питомца {PetId}", petId);
        var result = (await appointments.GetByPetAsync(petId, cancellationToken)).Select(ClinicMapper.ToAppointment).ToArray();
        logger.LogInformation("Приёмы питомца {PetId} получены, записей: {Count}", petId, result.Length);
        return result;
    }

    public async Task<IReadOnlyList<AppointmentDto>> GetCurrentMonthAsync(string roomNumber, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение приёмов текущего месяца в кабинете {RoomNumber}", roomNumber);
        var room = DtoValidator.RequireText(roomNumber, "Укажите номер кабинета.");
        var now = time.GetLocalNow().DateTime;
        var monthStart = new DateTime(now.Year, now.Month, 1);
        var nextMonth = monthStart.AddMonths(1);
        var result = (await appointments.GetByRoomAsync(room, monthStart, nextMonth, cancellationToken)).Select(ClinicMapper.ToAppointment).ToArray();
        logger.LogInformation("Приёмы кабинета {RoomNumber} получены, записей: {Count}", room, result.Length);
        return result;
    }

    public async Task<FollowUpCountDto> CountFollowUpsAsync(int breedId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Подсчёт повторных приёмов породы {BreedId}", breedId);
        var breed = await breeds.FindByIdAsync(breedId, cancellationToken) ?? throw new EntityNotFoundException("порода", breedId);
        var count = await appointments.CountFollowUpsAsync(breedId, cancellationToken);
        logger.LogInformation("Повторных приёмов породы {BreedId}: {Count}", breedId, count);
        return new FollowUpCountDto
        {
            BreedId = breed.Id,
            BreedName = breed.Name,
            Count = count
        };
    }

    public async Task<AppointmentDto> CreateAsync(AppointmentEditDto dto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Создание записи на приём");
        var values = Read(dto);
        var created = await appointments.AddAsync(
            values.PetId,
            values.VeterinarianId,
            values.VisitDateTime,
            values.RoomNumber,
            values.IsFollowUp, cancellationToken);
        logger.LogInformation("Запись на приём {AppointmentId} создана", created.Id);
        return ClinicMapper.ToAppointment(created);
    }

    public async Task<AppointmentDto> UpdateAsync(int id, AppointmentEditDto dto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Обновление записи на приём {AppointmentId}", id);
        var values = Read(dto);
        var updated = await appointments.UpdateAsync(
            id,
            values.PetId,
            values.VeterinarianId,
            values.VisitDateTime,
            values.RoomNumber,
            values.IsFollowUp, cancellationToken);
        logger.LogInformation("Запись на приём {AppointmentId} обновлена", id);
        return ClinicMapper.ToAppointment(updated);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Удаление записи на приём {AppointmentId}", id);
        await appointments.RemoveAsync(id, cancellationToken);
        logger.LogInformation("Запись на приём {AppointmentId} удалена", id);
    }

    private static (int PetId, int VeterinarianId, DateTime VisitDateTime, string RoomNumber, bool IsFollowUp) Read(
        AppointmentEditDto dto)
    {
        DtoValidator.EnsureValid(dto);
        var visitDateTime = dto.VisitDateTime ?? throw new DomainRuleException("Укажите дату и время приёма.");
        if (visitDateTime.Year < 2000 || visitDateTime.Year > 2100)
        {
            throw new DomainRuleException("Дата приёма указана некорректно.");
        }

        return (
            DtoValidator.RequireId(dto.PetId, "Укажите питомца."),
            DtoValidator.RequireId(dto.VeterinarianId, "Укажите врача."),
            visitDateTime,
            DtoValidator.RequireText(dto.RoomNumber, "Укажите номер кабинета."),
            dto.IsFollowUp);
    }
}
