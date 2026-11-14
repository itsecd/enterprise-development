using BeautySalon.Contracts.Masters;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BeautySalon.Server.Controllers;

/// <summary>
/// CRUD-операции над мастерами салона.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class MastersController(IMasterRepository masterRepository, ILogger<MastersController> logger) : ControllerBase
{
    /// <summary>
    /// Возвращает всех мастеров.
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<MasterResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<IEnumerable<MasterResponse>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос на получение списка всех мастеров.");
            return Ok(masterRepository.GetAll().Select(MapToResponse));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении списка мастеров.");
            return Problem("Внутренняя ошибка сервера при получении мастеров.");
        }
    }

    /// <summary>
    /// Возвращает мастера по идентификатору.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(MasterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<MasterResponse> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос на получение мастера с ID: {MasterId}", id);
            var master = masterRepository.GetById(id);
            if (master is null)
            {
                logger.LogWarning("Мастер с ID {MasterId} не найден.", id);
                return NotFound();
            }
            return Ok(MapToResponse(master));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при получении мастера с ID: {MasterId}", id);
            return Problem("Внутренняя ошибка сервера при получении мастера.");
        }
    }

    /// <summary>
    /// Создает нового мастера.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(MasterResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public ActionResult<MasterResponse> Create([FromBody] MasterRequest request)
    {
        try
        {
            logger.LogInformation("Запрос на создание нового мастера: {FullName}", request.FullName);
            
            var master = new Master
            {
                FullName = request.FullName,
                Gender = request.Gender,
                PassportNumber = request.PassportNumber,
                Specialization = request.Specialization,
                ExperienceYears = request.ExperienceYears
            };

            masterRepository.Add(master);
            
            logger.LogInformation("Мастер успешно создан с ID: {MasterId}", master.Id);
            return CreatedAtAction(nameof(GetById), new { id = master.Id }, MapToResponse(master));
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при создании мастера: {FullName}", request.FullName);
            return Problem("Внутренняя ошибка сервера при создании мастера.");
        }
    }

    /// <summary>
    /// Обновляет данные мастера по идентификатору.
    /// </summary>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Update(int id, [FromBody] MasterRequest request)
    {
        try
        {
            logger.LogInformation("Запрос на обновление мастера с ID: {MasterId}", id);
            
            var master = masterRepository.GetById(id);
            if (master is null)
            {
                logger.LogWarning("Мастер с ID {MasterId} не найден для обновления.", id);
                return NotFound();
            }

            master.FullName = request.FullName;
            master.Gender = request.Gender;
            master.PassportNumber = request.PassportNumber;
            master.Specialization = request.Specialization;
            master.ExperienceYears = request.ExperienceYears;

            masterRepository.Update(master);
            
            logger.LogInformation("Мастер с ID {MasterId} успешно обновлен.", id);
            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при обновлении мастера с ID: {MasterId}", id);
            return Problem("Внутренняя ошибка сервера при обновлении мастера.");
        }
    }

    /// <summary>
    /// Удаляет мастера по идентификатору.
    /// </summary>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
        {
            logger.LogInformation("Запрос на удаление мастера с ID: {MasterId}", id);
            
            var master = masterRepository.GetById(id);
            if (master is not null)
            {
                masterRepository.Delete(master);
                logger.LogInformation("Мастер с ID {MasterId} успешно удален.", id);
            }
            else
            {
                logger.LogWarning("Мастер с ID {MasterId} не найден (удаление пропущено).", id);
            }

            return NoContent();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Ошибка при удалении мастера с ID: {MasterId}", id);
            return Problem("Внутренняя ошибка сервера при удалении мастера.");
        }
    }

    private static MasterResponse MapToResponse(Master master) => new()
    {
        Id = master.Id,
        FullName = master.FullName,
        Gender = master.Gender,
        PassportNumber = master.PassportNumber,
        Specialization = master.Specialization,
        ExperienceYears = master.ExperienceYears
    };
}