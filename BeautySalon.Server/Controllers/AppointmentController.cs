using BeautySalon.Contracts.Appointments;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Server.Controllers;

/// <summary>
/// CRUD-операции над записями клиентов к мастерам.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class AppointmentController(
    IAppointmentRepository appointmentRepository,
    IClientRepository clientRepository,
    IMasterRepository masterRepository,
    IServiceRepository serviceRepository) : ControllerBase
{
    /// <summary>
    /// Возвращает все записи.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AppointmentResponse>), StatusCodes.Status200OK)]
    public ActionResult<IEnumerable<AppointmentResponse>> GetAll()
    {
        return Ok(appointmentRepository.GetAll().Select(MapToResponse));
    }

    /// <summary>
    /// Возвращает запись по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public ActionResult<AppointmentResponse> GetById(int id)
    {
        var appointment = appointmentRepository.GetById(id);
        return appointment is null ? NotFound() : Ok(MapToResponse(appointment));
    }

    /// <summary>
    /// Создает новую запись.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<AppointmentResponse> Create([FromBody] AppointmentRequest request)
    {
        var client = clientRepository.GetById(request.ClientId);
        var master = masterRepository.GetById(request.MasterId);
        var service = serviceRepository.GetById(request.ServiceId);

        if (client is null || master is null || service is null)
        {
            return BadRequest("Указан неверный ID клиента, мастера или услуги.");
        }

        var appointment = new Appointment
        {
            StartTime = request.StartTime,
            IsRegularClient = request.IsRegularClient,
            ClientId = client.Id,
            Client = client,
            MasterId = master.Id,
            Master = master,
            ServiceId = service.Id,
            Service = service
        };

        appointmentRepository.Add(appointment);
        
        return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, MapToResponse(appointment));
    }

    /// <summary>
    /// Обновляет запись.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult Update(int id, [FromBody] AppointmentRequest request)
    {
        var appointment = appointmentRepository.GetById(id);
        if (appointment is null) return NotFound();

        var client = clientRepository.GetById(request.ClientId);
        var master = masterRepository.GetById(request.MasterId);
        var service = serviceRepository.GetById(request.ServiceId);

        if (client is null || master is null || service is null)
        {
            return BadRequest("Указан неверный ID клиента, мастера или услуги.");
        }

        appointment.StartTime = request.StartTime;
        appointment.IsRegularClient = request.IsRegularClient;
        appointment.ClientId = client.Id;
        appointment.Client = client;
        appointment.MasterId = master.Id;
        appointment.Master = master;
        appointment.ServiceId = service.Id;
        appointment.Service = service;

        appointmentRepository.Update(appointment);
        return NoContent();
    }

    /// <summary>
    /// Удаляет запись по идентификатору.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public IActionResult Delete(int id)
    {
        var appointment = appointmentRepository.GetById(id);
        
        // Если записи нет, всё равно возвращаем 204 (идемпотентность)
        if (appointment is not null)
        {
            appointmentRepository.Delete(appointment);
        }

        return NoContent();
    }

    // Метод маппинга
    private static AppointmentResponse MapToResponse(Appointment appointment) => new()
    {
        Id = appointment.Id,
        StartTime = appointment.StartTime,
        IsRegularClient = appointment.IsRegularClient,
        ClientId = appointment.ClientId,
        MasterId = appointment.MasterId,
        ServiceId = appointment.ServiceId
    };
}