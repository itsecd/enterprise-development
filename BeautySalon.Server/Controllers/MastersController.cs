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
    public async Task<ActionResult<IEnumerable<MasterResponse>>> GetAll()
    {
        try
        {
            logger.LogInformation("Запрос на получение списка всех мастеров.");
            var items = await masterRepository.GetAllAsync(); 
            return Ok(items.Select(MapToResponse));
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
    /// <param name="id">Идентификатор мастера.</param>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(MasterResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MasterResponse>> GetById(int id)
    {
        try
        {
            logger.LogInformation("Запрос на получение мастера с ID: {MasterId}", id);
            var master = await  masterRepository.GetByIdAsync(id);
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
    /// <param name="request">Данные нового мастера.</param>
    [HttpPost]
    [ProducesResponseType(typeof(MasterResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<MasterResponse>> Create([FromBody] MasterRequest request)
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

            await masterRepository.AddAsync(master);
            
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
    /// <param name="id">Идентификатор мастера.</param>
    /// <param name="request">Новые данные мастера.</param>
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Update(int id, [FromBody] MasterRequest request)
    {
        try
        {
            logger.LogInformation("Запрос на обновление мастера с ID: {MasterId}", id);
            
            var master = await masterRepository.GetByIdAsync(id);
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

            await masterRepository.UpdateAsync(master);
            
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
    /// <param name="id">Идентификатор мастера.</param>
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            logger.LogInformation("Запрос на удаление мастера с ID: {MasterId}", id);
            
            var master = await masterRepository.GetByIdAsync(id);
            if (master is not null)
            {
                await masterRepository.DeleteAsync(master);
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