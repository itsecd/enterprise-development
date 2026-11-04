using BeautySalon.Contracts.Analytics;
using BeautySalon.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Server.Controllers;

/// <summary>
/// Аналитические запросы по данным салона.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AnalyticsController(
    IAppointmentRepository appointmentRepository,
    IMasterRepository masterRepository,
    IServiceRepository serviceRepository,
    IClientRepository clientRepository) : ControllerBase
{
    /// <summary>
    /// Мастера со стажем не менее указанного количества лет.
    /// </summary>
    [HttpGet("masters-with-experience")]
    [ProducesResponseType(typeof(MastersWithExperienceResponse), StatusCodes.Status200OK)]
    public ActionResult<MastersWithExperienceResponse> GetMastersWithExperience([FromQuery] int minYears = 5)
    {
        var masters = masterRepository.GetAll()
            .Where(m => m.ExperienceYears >= minYears)
            .Select(m => new MasterExperienceItem
            {
                Id = m.Id,
                FullName = m.FullName,
                Specialization = m.Specialization,
                ExperienceYears = m.ExperienceYears
            })
            .ToList();

        var response = new MastersWithExperienceResponse
        {
            MinYears = minYears,
            Count = masters.Count,
            Masters = masters
        };

        return Ok(response);
    }

    /// <summary>
    /// Временные промежутки между записями выбранного мастера.
    /// </summary>
    [HttpGet("master-gaps/{masterId:int}")]
    [ProducesResponseType(typeof(MasterGapsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<MasterGapsResponse> GetMasterTimeGaps(int masterId)
    {
        var master = masterRepository.GetById(masterId);
        if (master is null) return NotFound($"Мастер с id '{masterId}' не найден.");

        var appointments = appointmentRepository.GetAll()
            .Where(a => a.MasterId == masterId)
            .OrderBy(a => a.StartTime)
            .ToList();

        if (appointments.Count < 2)
        {
            return Ok(new MasterGapsResponse
            {
                MasterId = masterId,
                MasterName = master.FullName,
                GapsCount = 0,
                Message = "У мастера меньше двух записей — окошек нет."
            });
        }

        var gaps = new List<GapItem>();
        for (var i = 0; i < appointments.Count - 1; i++)
        {
            var current = appointments[i];
            var next = appointments[i + 1];
            var currentEnd = current.StartTime + current.Service.Duration;

            if (next.StartTime > currentEnd)
            {
                gaps.Add(new GapItem
                {
                    From = currentEnd,
                    To = next.StartTime,
                    DurationMinutes = (int)(next.StartTime - currentEnd).TotalMinutes
                });
            }
        }

        return Ok(new MasterGapsResponse
        {
            MasterId = masterId,
            MasterName = master.FullName,
            GapsCount = gaps.Count,
            Gaps = gaps
        });
    }

    /// <summary>
    /// Топ самых популярных услуг.
    /// </summary>
    [HttpGet("top-services")]
    [ProducesResponseType(typeof(IEnumerable<TopServiceItem>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<TopServiceItem>> GetTopServices([FromQuery] int take = 5)
    {
        var top = appointmentRepository.GetAll()
            .GroupBy(a => a.ServiceId)
            .Select(g => new TopServiceItem
            {
                ServiceId = g.Key,
                ServiceName = serviceRepository.GetById(g.Key)!.Name,
                Category = serviceRepository.GetById(g.Key)!.Category,
                AppointmentCount = g.Count()
            })
            .OrderByDescending(x => x.AppointmentCount)
            .Take(take)
            .ToList();

        return Ok(top);
    }

    /// <summary>
    /// Клиенты с повторными записями за указанный период.
    /// </summary>
    [HttpGet("repeat-clients")]
    [ProducesResponseType(typeof(RepeatClientsResponse), StatusCodes.Status200OK)]
    public ActionResult<RepeatClientsResponse> GetRepeatClients([FromQuery] int days = 30)
    {
        var threshold = DateTime.Now.AddDays(-days);

        var repeats = appointmentRepository.GetAll()
            .Where(a => a.StartTime >= threshold)
            .GroupBy(a => a.ClientId)
            .Where(g => g.Count() > 1)
            .Select(g => new RepeatClientItem
            {
                ClientId = g.Key,
                ClientName = clientRepository.GetById(g.Key)!.FullName,
                RepeatCount = g.Count()
            })
            .OrderByDescending(x => x.RepeatCount)
            .ToList();

        return Ok(new RepeatClientsResponse
        {
            PeriodDays = days,
            ClientsWithRepeats = repeats.Count,
            Details = repeats
        });
    }

    /// <summary>
    /// Клиенты, записанные к нескольким мастерам, отсортированные по дате рождения.
    /// </summary>
    [HttpGet("clients-multiple-masters")]
    [ProducesResponseType(typeof(ClientsMultipleMastersResponse), StatusCodes.Status200OK)]
    public ActionResult<ClientsMultipleMastersResponse> GetClientsWithMultipleMasters()
    {
        var result = appointmentRepository.GetAll()
            .GroupBy(a => a.ClientId)
            .Where(g => g.Select(a => a.MasterId).Distinct().Count() > 1)
            .Select(g => new ClientMultipleMastersItem
            {
                ClientId = g.Key,
                FullName = clientRepository.GetById(g.Key)!.FullName,
                BirthDate = clientRepository.GetById(g.Key)!.BirthDate,
                UniqueMastersCount = g.Select(a => a.MasterId).Distinct().Count()
            })
            .OrderBy(x => x.BirthDate)
            .ToList();

        return Ok(new ClientsMultipleMastersResponse
        {
            Count = result.Count,
            Clients = result
        });
    }
}