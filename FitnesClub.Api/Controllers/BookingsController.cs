using Microsoft.AspNetCore.Mvc;
using FitnesClub.Api.Contracts.Bookings;
using FitnesClub.Api.Storage;
using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Controllers;

/// <summary>
/// Контроллер для управления записями на персональные тренировки.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IRepository<Booking, Guid> _bookingRepo;
    private readonly IRepository<Trainer, Guid> _trainerRepo;
    private readonly IRepository<Member, Guid> _memberRepo;

    /// <summary>
    /// Инициализирует новый экземпляр контроллера записей на тренировки.
    /// </summary>
    /// <param name="bookingRepo">Репозиторий записей на тренировки.</param>
    /// <param name="trainerRepo">Репозиторий тренеров.</param>
    /// <param name="memberRepo">Репозиторий клиентов.</param>
    public BookingsController(
        IRepository<Booking, Guid> bookingRepo,
        IRepository<Trainer, Guid> trainerRepo,
        IRepository<Member, Guid> memberRepo)
    {
        _bookingRepo = bookingRepo;
        _trainerRepo = trainerRepo;
        _memberRepo = memberRepo;
    }

    /// <summary>
    /// Возвращает список всех существующих записей на тренировки.
    /// </summary>
    /// <returns>Коллекция DTO записей на тренировки.</returns>
    [HttpGet]
    public ActionResult<IReadOnlyList<BookingDto>> GetAll()
    {
        var bookings = _bookingRepo.ReadAll();
        return Ok(bookings.Select(b => b.ToDto()).ToList());
    }

    /// <summary>
    /// Возвращает запись на тренировку по её уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор записи.</param>
    /// <returns>DTO найденной записи или статус 404, если запись не найдена.</returns>
    [HttpGet("{id:guid}")]
    public ActionResult<BookingDto> GetById(Guid id)
    {
        var booking = _bookingRepo.Read(id);
        if (booking is null)
            return NotFound($"Запись с ID {id} не найдена.");

        return Ok(booking.ToDto());
    }

    /// <summary>
    /// Создает новую запись на тренировку с предварительной валидацией срока абонемента и расписания тренера.
    /// </summary>
    /// <param name="dto">Данные для создания записи.</param>
    /// <returns>Созданная запись с назначенным идентификатором или статус 400 при нарушении бизнес-правил.</returns>
    [HttpPost]
    public ActionResult<BookingDto> Create([FromBody] BookingCreateUpdateDto dto)
    {
        var validationError = ValidateBooking(dto);
        if (validationError is not null)
        {
            return BadRequest(validationError);
        }

        var newBooking = dto.ToEntity();
        _bookingRepo.Create(newBooking);

        return CreatedAtAction(nameof(GetById), new { id = newBooking.Id }, newBooking.ToDto());
    }

    /// <summary>
    /// Обновляет параметры существующей записи на тренировку.
    /// </summary>
    /// <param name="id">Уникальный идентификатор обновляемой записи.</param>
    /// <param name="dto">Новые параметры записи.</param>
    /// <returns>Статус 204 при успехе, 404 если запись не найдена, либо 400 при нарушении бизнес-правил.</returns>
    [HttpPut("{id:guid}")]
    public IActionResult Update(Guid id, [FromBody] BookingCreateUpdateDto dto)
    {
        var existingBooking = _bookingRepo.Read(id);
        if (existingBooking is null)
            return NotFound($"Запись с ID {id} не найдена.");

        var validationError = ValidateBooking(dto, currentBookingId: id);
        if (validationError is not null)
        {
            return BadRequest(validationError);
        }

        dto.UpdateEntity(existingBooking);
        _bookingRepo.Update(existingBooking);

        return NoContent();
    }

    /// <summary>
    /// Удаляет запись на тренировку по её уникальному идентификатору.
    /// </summary>
    /// <param name="id">Уникальный идентификатор удаляемой записи.</param>
    /// <returns>Статус 204 при успешном удалении или 404, если запись не найдена.</returns>
    [HttpDelete("{id:guid}")]
    public IActionResult Delete(Guid id)
    {
        var existingBooking = _bookingRepo.Read(id);
        if (existingBooking is null)
            return NotFound($"Запись с ID {id} не найдена.");

        _bookingRepo.Delete(id);
        return NoContent();
    }

    /// <summary>
    /// Проверяет существование связанных сущностей, активность абонемента клиента и отсутствие пересечений в расписании тренера.
    /// </summary>
    /// <param name="dto">Параметры проверяемой записи.</param>
    /// <param name="currentBookingId">Идентификатор текущей записи при обновлении для исключения её из проверки наложений.</param>
    /// <returns>Сообщение об ошибке валидации либо <see langword="null"/>, если все правила соблюдены.</returns>
    private string? ValidateBooking(BookingCreateUpdateDto dto, Guid? currentBookingId = null)
    {
        var trainer = _trainerRepo.Read(dto.TrainerId);
        if (trainer is null)
            return $"Тренер с ID {dto.TrainerId} не существует.";

        var member = _memberRepo.Read(dto.MemberId);
        if (member is null)
            return $"Клиент с ID {dto.MemberId} не существует.";

        if (dto.LessonDateTime < member.MembershipStartDate || dto.LessonDateTime > member.MembershipEndDate)
        {
            return $"У клиента {member.LastName} {member.FirstName} не действует абонемент на дату {dto.LessonDateTime:dd.MM.yyyy HH:mm}. Срок действия: с {member.MembershipStartDate:dd.MM.yyyy} по {member.MembershipEndDate:dd.MM.yyyy}.";
        }

        var newStart = dto.LessonDateTime;
        var newEnd = dto.LessonDateTime + dto.Duration;

        var trainerBookings = _bookingRepo.ReadAll()
            .Where(b => b.TrainerId == dto.TrainerId && b.Id != currentBookingId);

        foreach (var b in trainerBookings)
        {
            var existingStart = b.LessonDateTime;
            var existingEnd = b.LessonDateTime + b.Duration;

            if (newStart < existingEnd && newEnd > existingStart)
            {
                return $"Тренер занят в это время! Уже есть тренировка с {existingStart:dd.MM.yyyy HH:mm} по {existingEnd:HH:mm}.";
            }
        }

        return null;
    }
}