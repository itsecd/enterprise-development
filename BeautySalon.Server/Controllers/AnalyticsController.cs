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
    IClientRepository clientRepository,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Мастера со стажем не менее указанного количества лет.
    /// </summary>
    /// <param name="minYears">Минимальный стаж работы в годах (по умолчанию 5).</param>
    [HttpGet("masters-with-experience")]
    [ProducesResponseType(typeof(MastersWithExperienceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MastersWithExperienceResponse>> GetMastersWithExperience([FromQuery] int minYears = 5)
    {
        try
        {
            logger.LogInformation("Запрос аналитики: мастера со стажем от {MinYears} лет.", minYears);
            
            var masters = (await masterRepository.GetAllAsync())
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
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении аналитики по мастерам.");
            return Problem("Внутренняя ошибка сервера при получении аналитики по мастерам.");
        }
    }

    /// <summary>
    /// Временные промежутки между записями выбранного мастера.
    /// </summary>
    /// <param name="masterId">Идентификатор мастера.</param>
    [HttpGet("master-gaps/{masterId:int}")]
    [ProducesResponseType(typeof(MasterGapsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MasterGapsResponse>> GetMasterTimeGaps(int masterId)
    {
        try
        {
            logger.LogInformation("Запрос аналитики: окошки для мастера с ID {MasterId}.", masterId);
            
            var master = await masterRepository.GetByIdAsync(masterId);
            if (master is null)
            {
                logger.LogWarning("Мастер с ID {MasterId} не найден для аналитики окошек.", masterId);
                return NotFound($"Мастер с id '{masterId}' не найден.");
            }

            var appointments = (await appointmentRepository.GetAllAsync())
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
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении аналитики окошек для мастера с ID {MasterId}.", masterId);
            return Problem("Внутренняя ошибка сервера при получении аналитики окошек.");
        }
    }

    /// <summary>
    /// Топ самых популярных услуг.
    /// </summary>
    /// <param name="take">Количество услуг в топе (по умолчанию 5).</param>
    [HttpGet("top-services")]
    [ProducesResponseType(typeof(IEnumerable<TopServiceItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<TopServiceItem>>> GetTopServices([FromQuery] int take = 5)
    {
        try
        {
            logger.LogInformation("Запрос аналитики: топ {Take} популярных услуг.", take);
            
            var appointments = await appointmentRepository.GetAllAsync();
            var services = (await serviceRepository.GetAllAsync()).ToDictionary(s => s.Id);

            var top = appointments
                .GroupBy(a => a.ServiceId)
                .Select(g => new TopServiceItem
                {
                    ServiceId = g.Key,
                    ServiceName = services.TryGetValue(g.Key, out var service) ? service.Name : "Неизвестная услуга",
                    Category = services.TryGetValue(g.Key, out var s) ? s.Category : "Неизвестно",
                    AppointmentCount = g.Count()
                })
                .OrderByDescending(x => x.AppointmentCount)
                .Take(take)
                .ToList();

            return Ok(top);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении топа популярных услуг.");
            return Problem("Внутренняя ошибка сервера при получении топа услуг.");
        }
    }

    /// <summary>
    /// Клиенты с повторными записями за указанный период.
    /// </summary>
    /// <param name="days">Период в днях (по умолчанию 30).</param>
    [HttpGet("repeat-clients")]
    [ProducesResponseType(typeof(RepeatClientsResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RepeatClientsResponse>> GetRepeatClients([FromQuery] int days = 30)
    {
        try
        {
            logger.LogInformation("Запрос аналитики: повторные клиенты за {Days} дней.", days);
            
            var threshold = DateTime.Now.AddDays(-days);

            var appointments = await appointmentRepository.GetAllAsync();
            var clients = (await clientRepository.GetAllAsync()).ToDictionary(c => c.Id);
            
            var repeats = appointments
                .Where(a => a.StartTime >= threshold)
                .GroupBy(a => a.ClientId)
                .Where(g => g.Count() > 1)
                .Select(g => new RepeatClientItem
                {
                    ClientId = g.Key,
                    ClientName = clients.TryGetValue(g.Key, out var client) ? client.FullName : "Неизвестный клиент",
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
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении аналитики повторных клиентов.");
            return Problem("Внутренняя ошибка сервера при получении повторных клиентов.");
        }
    }

    /// <summary>
    /// Клиенты, записанные к нескольким мастерам, отсортированные по дате рождения.
    /// </summary>
    [HttpGet("clients-multiple-masters")]
    [ProducesResponseType(typeof(ClientsMultipleMastersResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<ClientsMultipleMastersResponse>> GetClientsWithMultipleMasters()
    {
        try
        {
            logger.LogInformation("Запрос аналитики: клиенты у нескольких мастеров.");
            
            var appointments = await appointmentRepository.GetAllAsync();
            var clients = (await clientRepository.GetAllAsync()).ToDictionary(c => c.Id);

            var result = appointments
                .GroupBy(a => a.ClientId)
                .Where(g => g.Select(a => a.MasterId).Distinct().Count() > 1)
                .Select(g =>
                {
                    clients.TryGetValue(g.Key, out var client); 
                    return new ClientMultipleMastersItem
                    {
                        ClientId = g.Key,
                        FullName = client?.FullName ?? "Неизвестный клиент",
                        BirthDate = client?.BirthDate ?? DateTime.MinValue,
                        UniqueMastersCount = g.Select(a => a.MasterId).Distinct().Count()
                    };
                })
                .OrderBy(x => x.BirthDate)
                .ToList();

            return Ok(new ClientsMultipleMastersResponse
            {
                Count = result.Count,
                Clients = result
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении аналитики клиентов у нескольких мастеров.");
            return Problem("Внутренняя ошибка сервера при получении аналитики.");
        }
    }
}