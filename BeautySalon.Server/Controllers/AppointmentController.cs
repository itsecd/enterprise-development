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
    IServiceRepository serviceRepository,
    ILogger<AppointmentController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает все записи.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AppointmentResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<IEnumerable<AppointmentResponse>>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос на получение списка всех записей.");
            var items = await appointmentRepository.GetAllAsync(); 
            return Ok(items.Select(MapToResponse));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка записей.");
            return Problem("Внутренняя ошибка сервера при получении записей.");
        }
    }

    /// <summary>
    /// Возвращает запись по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор записи.</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AppointmentResponse>> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос на получение записи с ID: {AppointmentId}", id);
            var appointment = await appointmentRepository.GetByIdAsync(id);
            if (appointment is null)
            {
                logger.LogWarning("Запись с ID {AppointmentId} не найдена.", id);
                return NotFound();
            }
            return Ok(MapToResponse(appointment));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении записи с ID: {AppointmentId}", id);
            return Problem("Внутренняя ошибка сервера при получении записи.");
        }
    }

    /// <summary>
    /// Создает новую запись.
    /// </summary>
    /// <param name="request">Данные новой записи.</param>
    [HttpPost]
    [ProducesResponseType(typeof(AppointmentResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<AppointmentResponse>> Create([FromBody] AppointmentRequest request)
    {
        try
        {
            logger.LogInformation("Запрос на создание новой записи.");
            
            var client = await clientRepository.GetByIdAsync(request.ClientId);
            var master = await masterRepository.GetByIdAsync(request.MasterId);
            var service = await serviceRepository.GetByIdAsync(request.ServiceId);

            if (client is null || master is null || service is null)
            {
                logger.LogWarning("Указан неверный ID клиента, мастера или услуги при создании записи.");
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

            await appointmentRepository.AddAsync(appointment);
            
            logger.LogInformation("Запись успешно создана с ID: {AppointmentId}", appointment.Id);
            return CreatedAtAction(nameof(GetById), new { id = appointment.Id }, MapToResponse(appointment));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании записи.");
            return Problem("Внутренняя ошибка сервера при создании записи.");
        }
    }

    /// <summary>
    /// Обновляет запись.
    /// </summary>
    /// <param name="id">Идентификатор записи.</param>
    /// <param name="request">Новые данные записи.</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] AppointmentRequest request)
    {
        try
        {
            logger.LogInformation("Запрос на обновление записи с ID: {AppointmentId}", id);
            
            var appointment = await appointmentRepository.GetByIdAsync(id);
            if (appointment is null)
            {
                logger.LogWarning("Запись с ID {AppointmentId} не найдена для обновления.", id);
                return NotFound();
            }

            var client = await clientRepository.GetByIdAsync(request.ClientId);
            var master = await masterRepository.GetByIdAsync(request.MasterId);
            var service = await serviceRepository.GetByIdAsync(request.ServiceId);

            if (client is null || master is null || service is null)
            {
                logger.LogWarning("Указан неверный ID клиента, мастера или услуги при обновлении записи с ID: {AppointmentId}", id);
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

            await appointmentRepository.UpdateAsync(appointment);
            
            logger.LogInformation("Запись с ID {AppointmentId} успешно обновлена.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении записи с ID: {AppointmentId}", id);
            return Problem("Внутренняя ошибка сервера при обновлении записи.");
        }
    }

    /// <summary>
    /// Удаляет запись по идентификатору.
    /// </summary>
    /// <param name="id">Идентификатор записи.</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            logger.LogInformation("Запрос на удаление записи с ID: {AppointmentId}", id);
            
            var appointment = await appointmentRepository.GetByIdAsync(id);
            if (appointment is not null)
            {
                await appointmentRepository.DeleteAsync(appointment);
                logger.LogInformation("Запись с ID {AppointmentId} успешно удалена.", id);
            }
            else
            {
                logger.LogWarning("Запись с ID {AppointmentId} не найдена (удаление пропущено).", id);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении записи с ID: {AppointmentId}", id);
            return Problem("Внутренняя ошибка сервера при удалении записи.");
        }
    }

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