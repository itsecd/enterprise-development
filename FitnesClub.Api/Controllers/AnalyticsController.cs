using Microsoft.AspNetCore.Mvc;
using FitnesClub.Api.Contracts.Analytics;
using FitnesClub.Api.Contracts.Bookings;
using FitnesClub.Api.Contracts.Members;
using FitnesClub.Api.Contracts.Trainers;
using FitnesClub.Domain.Entities;
using FitnesClub.Domain.Interfaces;

namespace FitnesClub.Api.Controllers;

/// <summary>
/// Контроллер аналитических выборок и бизнес-статистики фитнес-клуба.
/// </summary>
/// <param name="trainerRepo">Репозиторий тренеров.</param>
/// <param name="bookingRepo">Репозиторий записей на тренировки.</param>
/// <param name="memberRepo">Репозиторий клиентов клуба.</param>
/// <param name="logger">Сервис структурированного логирования событий контроллера.</param>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class AnalyticsController(
    IRepository<Trainer, Guid> trainerRepo,
    IRepository<Booking, Guid> bookingRepo,
    IRepository<Member, Guid> memberRepo,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// 1. Возвращает список всех тренеров со стажем работы не менее указанного (по умолчанию от 5 лет).
    /// </summary>
    /// <param name="minExperience">Минимальный требуемый стаж в годах (по умолчанию 5).</param>
    /// <returns>Список тренеров с соответствующим стажем.</returns>
    [HttpGet("experienced-trainers")]
    [ProducesResponseType(typeof(IReadOnlyList<TrainerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<TrainerDto>> GetExperiencedTrainers([FromQuery] int minExperience = 5)
    {
        logger.LogInformation("Запрос аналитики: получение тренеров со стажем от {MinExperience} лет", minExperience);

        try
        {
            var trainers = trainerRepo.ReadAll()
                .Where(trainer => trainer.WorkExperienceYears >= minExperience)
                .Select(trainer => trainer.ToDto())
                .ToList();

            return Ok(trainers);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при формировании выборки опытных тренеров");
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера при формировании аналитики." });
        }
    }

    /// <summary>
    /// 2. Проверяет доступность выбранного зала на указанную дату и время.
    /// </summary>
    /// <param name="roomName">Название проверяемого зала (например, "Зал №1").</param>
    /// <param name="dateTime">Дата и время для проверки (если не передана, берется текущее UTC время).</param>
    /// <returns>Объект со статусом доступности зала.</returns>
    [HttpGet("room-availability")]
    [ProducesResponseType(typeof(RoomAvailabilityDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<RoomAvailabilityDto> CheckRoomAvailability([FromQuery] string roomName, [FromQuery] DateTime? dateTime)
    {
        if (string.IsNullOrWhiteSpace(roomName))
        {
            logger.LogWarning("Запрос проверки доступности зала отклонен: не указано имя зала");
            return BadRequest(new { message = "Параметр 'roomName' обязателен для заполнения." });
        }

        var targetDate = dateTime ?? DateTime.UtcNow;
        logger.LogInformation("Запрос проверки доступности зала {RoomName} на время {CheckTime}", roomName, targetDate);

        try
        {
            var isRoomAvailable = !bookingRepo.ReadAll().Any(booking =>
                booking.RoomName == roomName &&
                booking.LessonDateTime <= targetDate &&
                booking.LessonDateTime.Add(booking.Duration) > targetDate);

            return Ok(new RoomAvailabilityDto(roomName, targetDate, isRoomAvailable));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при проверке занятости зала {RoomName}", roomName);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера при проверке доступности зала." });
        }
    }

    /// <summary>
    /// 3. Возвращает список клиентов с просроченным абонементом, упорядоченный по ФИО.
    /// </summary>
    /// <param name="asOfDate">Дата, относительно которой проверяется просрочка (по умолчанию текущая дата UTC).</param>
    /// <returns>Список клиентов с истекшим абонементом, отсортированный по фамилии и имени.</returns>
    [HttpGet("expired-memberships")]
    [ProducesResponseType(typeof(IReadOnlyList<MemberDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<MemberDto>> GetExpiredMemberships([FromQuery] DateTime? asOfDate)
    {
        var targetDate = asOfDate ?? DateTime.UtcNow;
        logger.LogInformation("Запрос аналитики: клиенты с истекшим абонементом на момент {TargetDate}", targetDate);

        try
        {
            var expiredMembers = memberRepo.ReadAll()
                .Where(member => member.MembershipEndDate < targetDate)
                .OrderBy(member => member.LastName)
                .ThenBy(member => member.FirstName)
                .Select(member => member.ToDto())
                .ToList();

            return Ok(expiredMembers);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка клиентов с просроченными абонементами");
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера при формировании аналитики." });
        }
    }

    /// <summary>
    /// 4. Возвращает информацию о занятиях в выбранном зале за указанный месяц и год.
    /// </summary>
    /// <param name="roomName">Название зала (например, "Зал №1").</param>
    /// <param name="year">Год (по умолчанию текущий год).</param>
    /// <param name="month">Месяц от 1 до 12 (по умолчанию текущий месяц).</param>
    /// <returns>Список занятий в указанном зале за выбранный период.</returns>
    [HttpGet("room-bookings")]
    [ProducesResponseType(typeof(IReadOnlyList<BookingDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<BookingDto>> GetRoomBookings(
        [FromQuery] string roomName,
        [FromQuery] int? year,
        [FromQuery] int? month)
    {
        if (string.IsNullOrWhiteSpace(roomName))
        {
            logger.LogWarning("Запрос расписания зала отклонен: не указано имя зала");
            return BadRequest(new { message = "Параметр 'roomName' обязателен для заполнения." });
        }

        var targetYear = year ?? DateTime.UtcNow.Year;
        var targetMonth = month ?? DateTime.UtcNow.Month;

        if (targetMonth is < 1 or > 12)
        {
            logger.LogWarning("Передан некорректный месяц: {Month}", targetMonth);
            return BadRequest(new { message = "Значение месяца должно быть в диапазоне от 1 до 12." });
        }

        logger.LogInformation("Запрос занятий зала {RoomName} за {Month}.{Year}", roomName, targetMonth, targetYear);

        try
        {
            var bookings = bookingRepo.ReadAll()
                .Where(booking =>
                    booking.RoomName == roomName &&
                    booking.LessonDateTime.Year == targetYear &&
                    booking.LessonDateTime.Month == targetMonth)
                .Select(booking => booking.ToDto())
                .ToList();

            return Ok(bookings);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении записей зала {RoomName} за {Month}.{Year}", roomName, targetMonth, targetYear);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера при выборке занятий зала." });
        }
    }

    /// <summary>
    /// 5. Возвращает топ-5 наиболее популярных тренеров по количеству оформленных занятий.
    /// </summary>
    /// <returns>Список из пяти наиболее востребованных тренеров клуба.</returns>
    [HttpGet("top-trainers")]
    [ProducesResponseType(typeof(IReadOnlyList<TrainerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<TrainerDto>> GetTopFiveTrainers()
    {
        logger.LogInformation("Запрос аналитики: получение топ-5 популярных тренеров");

        try
        {
            var bookingCounts = bookingRepo.ReadAll()
                .GroupBy(b => b.TrainerId)
                .ToDictionary(g => g.Key, g => g.Count());

            var topTrainers = trainerRepo.ReadAll()
                .Select(trainer => new
                {
                    Trainer = trainer,
                    BookingCount = bookingCounts.GetValueOrDefault(trainer.Id, 0)
                })
                .OrderByDescending(x => x.BookingCount)
                .ThenBy(x => x.Trainer.LastName)
                .Take(5)
                .Select(x => x.Trainer.ToDto())
                .ToList();

            return Ok(topTrainers);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при вычислении топ-5 тренеров");
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера при расчете популярности тренеров." });
        }
    }
}