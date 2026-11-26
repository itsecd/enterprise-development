using Microsoft.AspNetCore.Mvc;
using VetClinic.Application.Contracts.Appointments;

namespace VetClinic.Api.Host.Controllers;
/// <summary>
/// Контроллер для управления журналом записи на приемы и историей посещений
/// </summary>
/// <param name="service">Сервис управления приемами</param>
/// <param name="logger">Логгер контроллера приемов</param>
public class AppointmentController(IAppointmentAppService service, ILogger<AppointmentController> logger)
    : CrudControllerBase<AppointmentDto, AppointmentCreateUpdateDto, int>(service, logger)
{
    /// <summary>
    /// Извлекает полную историю посещений клиники для конкретного питомца
    /// </summary>
    /// <param name="petId">Уникальный идентификатор питомца</param>
    /// <returns>Список всех приемов данного питомца</returns>
    [HttpGet("by-pet/{petId:int}")]
    public async Task<IActionResult> GetAppointmentsByPet(int petId)
    {
        logger.LogInformation("Запрос истории приемов для питомца с Id {PetId}", petId);
        var result = await service.GetAppointmentsByPetId(petId);
        return Ok(result);
    }
}
