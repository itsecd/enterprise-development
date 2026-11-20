using Microsoft.AspNetCore.Mvc;
using VetClinic.Contracts.Dto.Breeds;
using VetClinic.Contracts.Services;

namespace VetClinic.Api.Controllers;

/// <summary>
/// Породы животных
/// </summary>
/// <param name="service">Сервис</param>
/// <param name="logger">Логгер</param>
[Route("api/breeds")]
[Tags("Породы животных")]
public class BreedsController(IBreedService service, ILogger<BreedsController> logger)
    : CrudControllerBase<BreedDto, BreedEditDto>(service, logger)
{
    /// <inheritdoc/>
    protected override int GetId(BreedDto dto) => dto.Id;
}
