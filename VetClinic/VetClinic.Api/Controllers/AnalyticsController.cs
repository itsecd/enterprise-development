using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using VetClinic.Contracts.Dto.Appointments;
using VetClinic.Contracts.Dto.Owners;
using VetClinic.Contracts.Dto.Pets;
using VetClinic.Contracts.Dto.Vets;
using VetClinic.Contracts.Services;
using VetClinic.Shared;

namespace VetClinic.Api.Controllers;

/// <summary>
/// Аналитические запросы ветеринарной клиники
/// </summary>
/// <param name="service">Сервис аналитики</param>
/// <param name="logger">Логгер</param>
[ApiController]
[Route("api/analytics")]
[Tags("Аналитика")]
[Produces("application/json")]
public class AnalyticsController(IAnalyticsService service, ILogger<AnalyticsController> logger) : ControllerBase
{
    /// <summary>
    /// Все ветеринары, специализирующиеся на выбранном виде животных
    /// </summary>
    /// <param name="species">Вид животного</param>
    /// <response code="200">Список врачей</response>
    [HttpGet("vets-by-species/{species}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<VetDto>>> GetVetsBySpecies(AnimalSpecies species)
    {
        logger.LogInformation("Аналитика: врачи по виду {Species}", species);
        return Ok(await service.GetVetsBySpeciesAsync(species));
    }

    /// <summary>
    /// Все питомцы, записанные на прием к указанному врачу, упорядоченные по кличке
    /// </summary>
    /// <param name="vetId">Идентификатор врача</param>
    /// <response code="200">Список питомцев</response>
    [HttpGet("pets-by-vet/{vetId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<PetDto>>> GetPetsByVet(int vetId)
    {
        logger.LogInformation("Аналитика: питомцы врача {VetId}", vetId);
        return Ok(await service.GetPetsByVetOrderedByNicknameAsync(vetId));
    }

    /// <summary>
    /// Количество повторных приемов выбранной породы животного
    /// </summary>
    /// <param name="breedId">Идентификатор породы</param>
    /// <response code="200">Количество повторных приемов</response>
    [HttpGet("repeat-appointments-by-breed/{breedId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<int>> GetRepeatAppointmentCountByBreed(int breedId)
    {
        logger.LogInformation("Аналитика: повторные приемы породы {BreedId}", breedId);
        return Ok(await service.GetRepeatAppointmentCountByBreedAsync(breedId));
    }

    /// <summary>
    /// Владельцы, у которых больше одного питомца, упорядоченные по ФИО
    /// </summary>
    /// <response code="200">Список владельцев</response>
    [HttpGet("owners-with-multiple-pets")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<OwnerPetsCountDto>>> GetOwnersWithMultiplePets()
    {
        logger.LogInformation("Аналитика: владельцы с несколькими питомцами");
        return Ok(await service.GetOwnersWithMultiplePetsOrderedByFullNameAsync());
    }

    /// <summary>
    /// Приемы за выбранный месяц в выбранном кабинете
    /// </summary>
    /// <param name="roomNumber">Номер кабинета</param>
    /// <param name="year">Год, по умолчанию текущий</param>
    /// <param name="month">Месяц от 1 до 12, по умолчанию текущий</param>
    /// <response code="200">Список приемов</response>
    /// <response code="400">Неверные параметры</response>
    [HttpGet("appointments-by-room")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> GetAppointmentsByRoomAndMonth(
        [FromQuery, Required] string roomNumber,
        [FromQuery, Range(1900, 9999)] int? year = null,
        [FromQuery, Range(1, 12)] int? month = null)
    {
        var today = DateTime.Now;
        var targetYear = year ?? today.Year;
        var targetMonth = month ?? today.Month;

        logger.LogInformation("Аналитика: приемы в кабинете {Room} за {Month}.{Year}", roomNumber, targetMonth, targetYear);
        return Ok(await service.GetAppointmentsByRoomAndMonthAsync(roomNumber, targetYear, targetMonth));
    }
}
