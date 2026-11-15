using Microsoft.AspNetCore.Mvc;
using FitnesClub.Api.Contracts.Bookings;
using FitnesClub.Api.Services;
using FitnesClub.Domain.Entities;
using FitnesClub.Domain.Interfaces;

namespace FitnesClub.Api.Controllers;

/// <summary>
/// Контроллер для управления записями на персональные тренировки.
/// </summary>
/// <param name="bookingRepo">Репозиторий записей на тренировки.</param>
/// <param name="validationService">Сервис бизнес-валидации бронирований.</param>
/// <param name="logger">Сервис структурированного логирования событий контроллера.</param>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BookingsController(
    IRepository<Booking, Guid> bookingRepo,
    IBookingValidationService validationService,
    ILogger<BookingsController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает список всех существующих записей на тренировки.
    /// </summary>
    /// <returns>Коллекция DTO записей на тренировки.</returns>
    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<BookingDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IReadOnlyList<BookingDto>> GetAll()
    {
        logger.LogInformation("Запрос на получение списка всех записей на тренировки");

        try
        {
            var bookings = bookingRepo.ReadAll();
            return Ok(bookings.Select(b => b.ToDto()).ToList());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка всех записей");
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Возвращает запись на тренировку по её уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор записи.</param>
    /// <returns>DTO найденной записи или статус 404, если запись не найдена.</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BookingDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<BookingDto> GetById(Guid id)
    {
        logger.LogInformation("Запрос на получение записи с ID: {BookingId}", id);

        try
        {
            var booking = bookingRepo.Read(id);
            if (booking is null)
            {
                logger.LogWarning("Запись с ID {BookingId} не найдена", id);
                return NotFound(new { message = $"Запись с ID {id} не найдена." });
            }

            return Ok(booking.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении записи с ID: {BookingId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Создает новую запись на тренировку.
    /// </summary>
    /// <param name="dto">Данные для создания записи на тренировку.</param>
    /// <returns>Созданная запись с присвоенным идентификатором или статус 400 при нарушении правил валидации.</returns>
    [HttpPost]
    [ProducesResponseType(typeof(BookingDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<BookingDto> Create([FromBody] BookingCreateUpdateDto dto)
    {
        logger.LogInformation("Попытка создания записи: клиент {MemberId}, тренер {TrainerId}", dto.MemberId, dto.TrainerId);

        try
        {
            var validationError = validationService.Validate(dto);
            if (validationError is not null)
            {
                logger.LogWarning("Ошибка валидации при создании записи: {ValidationError}", validationError);
                return BadRequest(new { message = validationError });
            }

            var newBooking = dto.ToEntity();
            bookingRepo.Create(newBooking);

            logger.LogInformation("Запись успешно создана с ID: {BookingId}", newBooking.Id);
            return CreatedAtAction(nameof(GetById), new { id = newBooking.Id }, newBooking.ToDto());
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании записи для клиента {MemberId}", dto.MemberId);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Обновляет параметры существующей записи на тренировку.
    /// </summary>
    /// <param name="id">Уникальный идентификатор обновляемой записи.</param>
    /// <param name="dto">Новые параметры записи.</param>
    /// <returns>Статус 204 при успехе, 404 если запись не найдена, либо 400 при ошибке валидации.</returns>
    [HttpPut("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Update(Guid id, [FromBody] BookingCreateUpdateDto dto)
    {
        logger.LogInformation("Попытка обновления записи с ID: {BookingId}", id);

        try
        {
            var existingBooking = bookingRepo.Read(id);
            if (existingBooking is null)
            {
                logger.LogWarning("Запись с ID {BookingId} не найдена для обновления", id);
                return NotFound(new { message = $"Запись с ID {id} не найдена." });
            }

            var validationError = validationService.Validate(dto, currentBookingId: id);
            if (validationError is not null)
            {
                logger.LogWarning("Ошибка валидации при обновлении записи {BookingId}: {ValidationError}", id, validationError);
                return BadRequest(new { message = validationError });
            }

            dto.UpdateEntity(existingBooking);
            bookingRepo.Update(existingBooking);

            logger.LogInformation("Запись с ID {BookingId} успешно обновлена", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении записи с ID: {BookingId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера." });
        }
    }

    /// <summary>
    /// Удаляет запись на тренировку по её идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор удаляемой записи.</param>
    /// <returns>Статус 204 при успешном удалении (в том числе повторном).</returns>
    [HttpDelete("{id:guid}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(Guid id)
    {
        logger.LogInformation("Запрос на удаление записи с ID: {BookingId}", id);

        try
        {
            var existingBooking = bookingRepo.Read(id);
            if (existingBooking is not null)
            {
                bookingRepo.Delete(id);
                logger.LogInformation("Запись с ID {BookingId} удалена", id);
            }
            else
            {
                logger.LogInformation("Запись с ID {BookingId} уже отсутствует в хранилище", id);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении записи с ID: {BookingId}", id);
            return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Внутренняя ошибка сервера." });
        }
    }
}