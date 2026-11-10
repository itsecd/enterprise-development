using BeautySalon.Application.Contracts;
using BeautySalon.Application.Contracts.Analytics;
using BeautySalon.Application.Contracts.Clients;
using BeautySalon.Application.Contracts.Masters;
using BeautySalon.Domain;
using BeautySalon.Domain.Entities;

namespace BeautySalon.Application.Services;

/// <summary>
/// Служба аналитических запросов к данным
/// </summary>
/// <param name="appointmentRepository">Репозиторий записей</param>
/// <param name="masterRepository">Репозиторий мастеров</param>
public class AnalyticService(
    IRepository<Appointment, int> appointmentRepository,
    IRepository<Master, int> masterRepository) : IAnalyticService
{
    /// <inheritdoc/>
    public async Task<IList<MasterDto>> GetMastersWithExperienceAtLeastFiveYears()
    {
        var masters = await masterRepository.ReadAll();

        var result = masters
            .Where(m => m.WorkExperience >= 5)
            .OrderByDescending(m => m.WorkExperience)
            .ThenBy(m => m.FullName)
            .ToList();

        return Mapper.Map(result);
    }

    /// <inheritdoc/>
    public async Task<IList<FreeSlotDto>?> GetFreeSlotsForSelectedMaster(int masterId, DateOnly? date = null)
    {
        var appointments = (await appointmentRepository.ReadAll()).Where(a => a.Master.Id == masterId);

        if (date.HasValue)
            appointments = appointments.Where(a => DateOnly.FromDateTime(a.StartTime) == date.Value);

        return appointments
            .OrderBy(a => a.StartTime)
            .GroupBy(a => a.StartTime.Date)
            .SelectMany(g => g.Zip(g.Skip(1), (current, next) => (
                Start: current.StartTime.AddMinutes(current.Service.Duration),
                End: next.StartTime)))
            .Where(s => s.End > s.Start)
            .Select(s => new FreeSlotDto(s.Start, s.End))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<IList<PopularServiceDto>> GetTopFivePopularServices()
    {
        var appointments = await appointmentRepository.ReadAll();

        return appointments
            .GroupBy(a => a.Service.Id)
            .Select(g => (Service: g.First().Service, Count: g.Count()))
            .OrderByDescending(x => x.Count)
            .ThenBy(x => x.Service.Name)
            .Take(5)
            .Select(x => Mapper.Map(x.Service, x.Count))
            .ToList();
    }

    /// <inheritdoc/>
    public async Task<int> GetNumberOfRepeatClientAppointmentsForLastMonth()
    {
        var appointments = await appointmentRepository.ReadAll();

        var now = DateTime.Now;
        var from = now.AddMonths(-1);

        var result = appointments
        .Where(appointment =>
            appointment.StartTime.Date >= from.Date &&
            appointment.StartTime.Date <= now.Date)
        .GroupBy(appointment => appointment.Client)
        .Where(group => group.Count() > 1)
        .Sum(group => group.Count() - 1);

        return result;
    }

    /// <inheritdoc/>
    public async Task<IList<ClientDto>> GetClientsRegisteredWithSeveralMasters()
    {
        var appointments = await appointmentRepository.ReadAll();

        var clients = appointments
            .GroupBy(a => a.Client.Id)
            .Where(g => g.Select(a => a.Master.Id).Distinct().Count() > 1)
            .Select(g => g.First().Client)
            .OrderBy(c => c.Birthday)
            .ToList();

        return Mapper.Map(clients);
    }
}
