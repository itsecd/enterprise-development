using Microsoft.AspNetCore.Mvc;
using VetClinic.Contracts.Dto.Specializations;
using VetClinic.Contracts.Services;

namespace VetClinic.Api.Controllers;

/// <summary>
/// Специализации врачей
/// </summary>
/// <param name="service">Сервис</param>
/// <param name="logger">Логгер</param>
[Route("api/specializations")]
[Tags("Специализации врачей")]
public class SpecializationsController(ISpecializationService service, ILogger<SpecializationsController> logger)
    : CrudControllerBase<SpecializationDto, SpecializationEditDto>(service, logger)
{
    /// <inheritdoc/>
    protected override int GetId(SpecializationDto dto) => dto.Id;
}
