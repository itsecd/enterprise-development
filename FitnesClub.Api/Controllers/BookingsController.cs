using Microsoft.AspNetCore.Mvc;
using FitnesClub.Api.Contracts.Bookings;
using FitnesClub.Api.Storage;
using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IRepository<Booking, Guid> _bookingRepo;
    private readonly IRepository<Trainer, Guid> _trainerRepo;
    private readonly IRepository<Member, Guid> _memberRepo;

    public BookingsController(
        IRepository<Booking, Guid> bookingRepo,
        IRepository<Trainer, Guid> trainerRepo,
        IRepository<Member, Guid> memberRepo)
    {
        _bookingRepo = bookingRepo;
        _trainerRepo = trainerRepo;
        _memberRepo = memberRepo;
    }

    [HttpGet]
    public ActionResult<IReadOnlyList<BookingDto>> GetAll()
    {
        var bookings = _bookingRepo.ReadAll();
        return Ok(bookings.Select(b => b.ToDto()).ToList());
    }

    [HttpGet("{id:guid}")]
    public ActionResult<BookingDto> GetById(Guid id)
    {
        var booking = _bookingRepo.Read(id);
        if (booking is null)
            return NotFound($"Запись с ID {id} не найдена.");

        return Ok(booking.ToDto());
    }

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
    /// Метод проверки целостности связей и бизнес-правил
    /// </summary>
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