using Microsoft.AspNetCore.Mvc;
using VetClinic.Application.Contracts;
using VetClinic.Domain.Shared.Enums;

namespace VetClinic.Api.Host.Controllers;

/// <summary>
/// Контроллер для выполнения сложных аналитических запросов
/// </summary>
/// <param name="service">Сервис аналитики</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("api/[controller]")]
public class AnalyticController(IAnalyticService service, ILogger<AnalyticController> logger) : ControllerBase
{
    /// <summary>
    /// Получает список ветеринаров, специализирующихся на выбранном виде животных
    /// </summary>
    /// <param name="species">Интересуемый вид животного</param>
    /// <returns>Список врачей в виде DTO</returns>
    [HttpGet("vets/by-species/{species}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAllVetsBySpecies(Species species)
    {
        try
        {
            logger.LogInformation("API: Получение врачей со специализацией по виду: {Species}", species);
            var result = await service.GetAllVetsBySpecies(species);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "API: Ошибка при получении врачей для вида {Species}", species);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получить питомцев, записанных к указанному врачу, упорядоченных по кличке
    /// </summary>
    /// <param name="vetId">Id врача</param>
    /// <return>Список DTO питомцев </return>
    [HttpGet("pets/by-vet/{vetId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetPetsByVet(int vetId)
    {
        try
        {
            logger.LogInformation("API: Получение списка питомцев для врача с Id {VetId}", vetId);
            var result = await service.GetAllPetsByVetOrderedByName(vetId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "API: Ошибка при сборе питомцев для врача с Id {VetId}", vetId);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получить количество повторных приемов выбранной породы
    /// </summary>
    /// <param name="breedId">Id породы</param>
    /// <return>Число повторных приёмов</return>
    [HttpGet("appointments/repeat-count/by-breed/{breedId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRepeatCount(int breedId)
    {
        try
        {
            logger.LogInformation("API: Подсчет повторных визитов для породы с Id {BreedId}", breedId);
            var result = await service.FindNumberOfSecondaryVisitForSpecificAnimalBreed(breedId);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "API: Ошибка подсчета повторных визитов для породы с Id {BreedId}", breedId);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получить владельцев, имеющих более одного питомца, упорядоченных по ФИО
    /// </summary>
    /// <return>Список владельцев</return>
    [HttpGet("owners/multiple-pets")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOwnersWithMultiplePets()
    {
        try
        {
            logger.LogInformation("API: Запрос списка владельцев с несколькими питомцами");
            var result = await service.GetAllOwnersWithMultiplePets();
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "API: Ошибка получения списка многодетных владельцев");
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }

    /// <summary>
    /// Получить приемы за интересующий месяц, проходящие в выбранном кабинете
    /// </summary>
    /// <param name="roomNumber">Номер интересуемого кабинета</param>
    /// <param name="month">Интересуемый месяц</param>
    /// <param name="year">Интересуемый год</param>
    /// 
    /// <return>Список записей на приём </return>
    [HttpGet("appointments/current-month/by-room/{roomNumber:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetCurrentMonthAppointments(int roomNumber, int month, int year)
    {
        try
        {
            logger.LogInformation("API: Запрос расписания на текущий месяц для кабинета {Room}", roomNumber);
            var result = await service.GetAllAppointmentsForCurrentMonthByRoom(roomNumber, month, year);
            return Ok(result);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "API: Ошибка при получении приемов текущего месяца для кабинета {Room}", roomNumber);
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

