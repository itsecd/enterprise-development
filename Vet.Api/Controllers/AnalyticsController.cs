using Microsoft.AspNetCore.Mvc;
using Vet.Application.Interfaces;
using Vet.Contracts.Dtos;
using Vet.Domain.Enums;

namespace Vet.Api.Controllers;

/// <summary>
/// Контроллер аналитических запросов
/// </summary>
/// <param name="analyticsService">Сервис аналитики</param>
/// <param name="logger">Логгер контроллера</param>
[ApiController]
[Route("api/[controller]")]
public sealed class AnalyticsController(
    IAnalyticsService analyticsService,
    ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Запрос 1 Получить ветеринаров по виду животного
    /// </summary>
    /// <param name="species">Вид животного</param>
    /// <returns>Список ветеринаров</returns>
    [HttpGet("veterinarians-by-species")]
    [ProducesResponseType(typeof(IReadOnlyList<VeterinarianDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetVeterinariansBySpecies([FromQuery] Species species)
    {
        try
        {
            if (!Enum.IsDefined(typeof(Species), species))
            {
                logger.LogWarning("Передан недопустимый вид животного {Species}", species);
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Некорректный запрос",
                    Detail = $"Значение {species} не является допустимым видом животного"
                });
            }

            logger.LogInformation("Аналитика получение врачей по виду {Species}", species);
            var vets = await analyticsService.GetVeterinariansBySpeciesAsync(species);
            return Ok(vets);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении ветеринаров по виду {Species}", species);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Запрос 2 Получить уникальных питомцев у врача отсортированных по кличке
    /// </summary>
    /// <param name="veterinarianId">Идентификатор ветеринара</param>
    /// <returns>Список питомцев</returns>
    [HttpGet("pets-by-veterinarian/{veterinarianId:int}")]
    [ProducesResponseType(typeof(IReadOnlyList<PetDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPetsByVeterinarian(int veterinarianId)
    {
        try
        {
            if (veterinarianId < 0)
            {
                logger.LogWarning("Передан отрицательный идентификатор ветеринара {VetId}", veterinarianId);
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Некорректный запрос",
                    Detail = "Идентификатор ветеринара не может быть отрицательным числом"
                });
            }

            logger.LogInformation("Аналитика получение питомцев ветеринара с Id {VetId}", veterinarianId);
            var pets = await analyticsService.GetPetsByVeterinarianAsync(veterinarianId);
            return Ok(pets);
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Ветеринар с Id {VetId} не найден", veterinarianId);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден"
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении питомцев врача с Id {VetId}", veterinarianId);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Запрос 3 Количество повторных приемов для породы
    /// </summary>
    /// <param name="breedId">Идентификатор породы</param>
    /// <returns>Количество повторных приемов</returns>
    [HttpGet("repeat-appointments-count/by-breed/{breedId:int}")]
    [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRepeatCountByBreed(int breedId)
    {
        try
        {
            if (breedId < 0)
            {
                logger.LogWarning("Передан отрицательный идентификатор породы {BreedId}", breedId);
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Некорректный запрос",
                    Detail = "Идентификатор породы не может быть отрицательным числом"
                });
            }

            logger.LogInformation("Аналитика подсчет повторных приемов для породы с Id {BreedId}", breedId);
            var count = await analyticsService.GetRepeatAppointmentsCountByBreedAsync(breedId);
            return Ok(new { BreedId = breedId, RepeatAppointmentsCount = count });
        }
        catch (KeyNotFoundException ex)
        {
            logger.LogWarning(ex, "Порода с Id {BreedId} не найдена", breedId);
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Ресурс не найден"
            });
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при подсчете повторных приемов породы с Id {BreedId}", breedId);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Запрос 4 Владельцы с более чем одним питомцем
    /// </summary>
    /// <returns>Список владельцев</returns>
    [HttpGet("owners-with-multiple-pets")]
    [ProducesResponseType(typeof(IReadOnlyList<OwnerDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOwnersWithMultiplePets()
    {
        try
        {
            logger.LogInformation("Аналитика получение владельцев с несколькими питомцами");
            var owners = await analyticsService.GetOwnersWithMultiplePetsAsync();
            return Ok(owners);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении владельцев с несколькими питомцами");
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }

    /// <summary>
    /// Запрос 5 Приёмы в кабинете за выбранный месяц и год
    /// </summary>
    /// <param name="roomNumber">Номер кабинета</param>
    /// <param name="year">Год</param>
    /// <param name="month">Месяц от 1 до 12</param>
    /// <returns>Список записей на прием</returns>
    [HttpGet("appointments-by-room")]
    [ProducesResponseType(typeof(IReadOnlyList<AppointmentDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAppointmentsByRoom(
        [FromQuery] int roomNumber,
        [FromQuery] int year,
        [FromQuery] int month)
    {
        try
        {
            if (roomNumber <= 0)
            {
                logger.LogWarning("Передан некорректный номер кабинета {Room}", roomNumber);
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Некорректный запрос",
                    Detail = "Номер кабинета должен быть положительным числом больше нуля"
                });
            }

            if (month is < 1 or > 12)
            {
                logger.LogWarning("Передан некорректный месяц {Month}", month);
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Некорректный запрос",
                    Detail = "Месяц должен быть в диапазоне от 1 до 12"
                });
            }

            if (year is < 2000 or > 2100)
            {
                logger.LogWarning("Передан некорректный год {Year}", year);
                return BadRequest(new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Некорректный запрос",
                    Detail = "Год должен находиться в диапазоне от 2000 до 2100"
                });
            }

            logger.LogInformation("Аналитика приемы в кабинете {Room} за {Month} {Year}", roomNumber, month, year);
            var appointments = await analyticsService.GetAppointmentsByRoomAsync(roomNumber, year, month);
            return Ok(appointments);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении приемов в кабинете {Room} за {Month} {Year}", roomNumber, month, year);
            return StatusCode(StatusCodes.Status500InternalServerError, new ProblemDetails
            {
                Status = StatusCodes.Status500InternalServerError,
                Title = "Ошибка сервера"
            });
        }
    }
}