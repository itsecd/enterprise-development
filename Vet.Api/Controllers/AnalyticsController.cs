using Microsoft.AspNetCore.Mvc;
using Vet.Contracts.Dtos;
using Vet.Contracts.Interfaces;
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
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<VeterinarianDto>>> GetVeterinariansBySpecies([FromQuery] Species species)
    {
        if (!Enum.IsDefined(species))
        {
            logger.LogWarning("Передан недопустимый вид животного {Species}", species);
            throw new ArgumentException($"Значение {species} не является допустимым видом животного", nameof(species));
        }

        var vets = await analyticsService.GetVeterinariansBySpeciesAsync(species);
        return Ok(vets);
    }

    /// <summary>
    /// Запрос 2 Получить уникальных питомцев у врача отсортированных по кличке
    /// </summary>
    /// <param name="veterinarianId">Идентификатор ветеринара</param>
    /// <returns>Список питомцев</returns>
    [HttpGet("pets-by-veterinarian/{veterinarianId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<PetDto>>> GetPetsByVeterinarian(int veterinarianId)
    {
        if (veterinarianId <= 0)
        {
            throw new ArgumentException("Идентификатор ветеринара должен быть положительным числом", nameof(veterinarianId));
        }

        var pets = await analyticsService.GetPetsByVeterinarianAsync(veterinarianId);
        return Ok(pets);
    }

    /// <summary>
    /// Запрос 3 Количество повторных приемов для породы
    /// </summary>
    /// <param name="breedId">Идентификатор породы</param>
    /// <returns>DTO количества повторных приемов</returns>
    [HttpGet("repeat-appointments-count/by-breed/{breedId:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<RepeatAppointmentsCountDto>> GetRepeatCountByBreed(int breedId)
    {
        if (breedId <= 0)
        {
            throw new ArgumentException("Идентификатор породы должен быть положительным числом", nameof(breedId));
        }

        var count = await analyticsService.GetRepeatAppointmentsCountByBreedAsync(breedId);
        return Ok(new RepeatAppointmentsCountDto(breedId, count));
    }

    /// <summary>
    /// Запрос 4 Владельцы с более чем одним питомцем
    /// </summary>
    /// <returns>Список владельцев</returns>
    [HttpGet("owners-with-multiple-pets")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<OwnerDto>>> GetOwnersWithMultiplePets()
    {
        var owners = await analyticsService.GetOwnersWithMultiplePetsAsync();
        return Ok(owners);
    }

    /// <summary>
    /// Запрос 5 Приёмы в кабинете за выбранный месяц и год
    /// </summary>
    /// <param name="roomNumber">Номер кабинета</param>
    /// <param name="year">Год</param>
    /// <param name="month">Месяц от 1 до 12</param>
    /// <returns>Список записей на прием</returns>
    [HttpGet("appointments-by-room")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IReadOnlyList<AppointmentDto>>> GetAppointmentsByRoom(
        [FromQuery] int roomNumber,
        [FromQuery] int year,
        [FromQuery] int month)
    {
        if (roomNumber <= 0)
        {
            throw new ArgumentException("Номер кабинета должен быть положительным числом больше нуля", nameof(roomNumber));
        }

        if (month is < 1 or > 12)
        {
            throw new ArgumentException("Месяц должен быть в диапазоне от 1 до 12", nameof(month));
        }

        if (year is < 2000 or > 2100)
        {
            throw new ArgumentException("Год должен находиться в диапазоне от 2000 до 2100", nameof(year));
        }

        var appointments = await analyticsService.GetAppointmentsByRoomAsync(roomNumber, year, month);
        return Ok(appointments);
    }
}