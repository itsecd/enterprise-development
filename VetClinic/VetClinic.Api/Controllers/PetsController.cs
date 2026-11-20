using Microsoft.AspNetCore.Mvc;
using VetClinic.Contracts.Dto.Pets;
using VetClinic.Contracts.Services;

namespace VetClinic.Api.Controllers;

/// <summary>
/// Питомцы
/// </summary>
/// <param name="service">Сервис</param>
/// <param name="logger">Логгер</param>
[Route("api/pets")]
[Tags("Питомцы")]
public class PetsController(IPetService service, ILogger<PetsController> logger)
    : CrudControllerBase<PetDto, PetEditDto>(service, logger)
{
    /// <inheritdoc/>
    protected override int GetId(PetDto dto) => dto.Id;
}
