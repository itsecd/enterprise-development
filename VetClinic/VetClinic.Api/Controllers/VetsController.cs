using Microsoft.AspNetCore.Mvc;
using VetClinic.Contracts.Dto.Vets;
using VetClinic.Contracts.Services;

namespace VetClinic.Api.Controllers;

/// <summary>
/// Ветеринарные врачи
/// </summary>
/// <param name="service">Сервис</param>
/// <param name="logger">Логгер</param>
[Route("api/vets")]
[Tags("Ветеринарные врачи")]
public class VetsController(IVetService service, ILogger<VetsController> logger)
    : CrudControllerBase<VetDto, VetEditDto>(service, logger)
{
    /// <inheritdoc/>
    protected override int GetId(VetDto dto) => dto.Id;
}
