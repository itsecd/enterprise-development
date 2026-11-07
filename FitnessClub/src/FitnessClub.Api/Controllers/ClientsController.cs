using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Управление клиентами
/// </summary>
/// <param name="service">
/// Сервис управления клиентами
/// </param>
public class ClientsController(ICrudService<ClientDto, ClientEditDto> service) : CrudControllerBase<ClientDto, ClientEditDto>(service);