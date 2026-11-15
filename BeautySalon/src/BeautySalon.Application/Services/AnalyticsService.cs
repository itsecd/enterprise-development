using BeautySalon.Application.Exceptions;
using BeautySalon.Application.Mapping;
using BeautySalon.Contracts.Dto;
using BeautySalon.Contracts.Interfaces;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Application.Services;

/// <summary>
/// Аналитические запросы по данным салона красоты
/// </summary>
public class AnalyticsService(
    IMasterRepository masterRepository,
    IAppointmentRepository appointmentRepository) : IAnalyticsService
{
    private static readonly TimeOnly WorkDayStart = new(9, 0);
    private static readonly TimeOnly WorkDayEnd = new(18, 0);

    /// <inheritdoc />
    public async Task<IReadOnlyList<MasterDto>> GetExperiencedMastersAsync(
        int minExperienceYears = 5, CancellationToken cancellationToken = default)
    {
        var masters = await masterRepository.GetAllAsync(cancellationToken);

        return [.. masters
            .Where(m => m.WorkExperience >= minExperienceYears)
            .Select(m => m.ToDto())];
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<TimeSlotDto>> GetFreeSlotsAsync(
        int masterId, DateOnly date, CancellationToken cancellationToken = default)
    {
        _ = await masterRepository.GetByIdAsync(masterId, cancellationToken)
            ?? throw new NotFoundException("Мастер", masterId);

        var dayStart = date.ToDateTime(WorkDayStart);
        var dayEnd = date.ToDateTime(WorkDayEnd);

        // Занятые интервалы мастера в этот день, начинающиеся до конца рабочего дня, по возрастанию времени начала
        var appointments = await appointmentRepository.GetAllAsync(cancellationToken);
        var busySlots = appointments
            .Where(a => a.Master.Id == masterId && DateOnly.FromDateTime(a.DateTime) == date && a.DateTime < dayEnd)
            .OrderBy(a => a.DateTime)
            .Select(a => (Start: a.DateTime, End: a.DateTime + a.Service.Duration));

        List<TimeSlotDto> freeSlots = [];

        // freeFrom - момент, начиная с которого мастер свободен после уже обработанных записей
        var freeFrom = dayStart;
        foreach (var (start, end) in busySlots)
        {
            // Между концом предыдущей записи и началом этой есть промежуток - это свободное окно
            if (start > freeFrom)
            {
                freeSlots.Add(new TimeSlotDto(freeFrom, start));
            }

            // Записи могут пересекаться, поэтому двигаем границу только вперёд
            freeFrom = end > freeFrom ? end : freeFrom;
        }

        // Остаток рабочего дня после последней записи
        if (freeFrom < dayEnd)
        {
            freeSlots.Add(new TimeSlotDto(freeFrom, dayEnd));
        }

        return freeSlots;
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<PopularServiceDto>> GetPopularServicesAsync(
        int top = 5, CancellationToken cancellationToken = default)
    {
        var appointments = await appointmentRepository.GetAllAsync(cancellationToken);

        return [.. appointments
            .GroupBy(a => a.Service.Name)
            .Select(g => new PopularServiceDto(g.Key, g.Count()))
            .OrderByDescending(s => s.AppointmentsCount)
            .Take(top)];
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<RepeatClientDto>> GetRepeatClientsAsync(
        int year, int month, CancellationToken cancellationToken = default)
    {
        var appointments = await appointmentRepository.GetAllAsync(cancellationToken);

        return [.. appointments
            .Where(a => a.DateTime.Year == year && a.DateTime.Month == month)
            .GroupBy(a => a.Client.Id)
            .Where(g => g.Count() > 1)
            .Select(g => new RepeatClientDto(g.First().Client.ToDto(), g.Count()))
            .OrderByDescending(r => r.AppointmentsCount)];
    }

    /// <inheritdoc />
    public async Task<IReadOnlyList<ClientDto>> GetClientsWithMultipleMastersAsync(
        CancellationToken cancellationToken = default)
    {
        var appointments = await appointmentRepository.GetAllAsync(cancellationToken);

        return [.. appointments
            .GroupBy(a => a.Client.Id)
            .Where(g => g.Select(a => a.Master.Id).Distinct().Count() >= 2)
            .Select(g => g.First().Client)
            .OrderBy(c => c.BirthDate)
            .Select(c => c.ToDto())];
    }
}