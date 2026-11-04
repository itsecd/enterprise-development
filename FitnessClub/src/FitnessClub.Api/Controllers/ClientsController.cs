using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Управление клиентами
/// </summary>
public class ClientsController(ICrudService<ClientDto, ClientEditDto> service) : CrudControllerBase<ClientDto, ClientEditDto>(service);