using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Управление залами
/// </summary>
/// <param name="service">
/// Сервис управления залами
/// </param>
public class HallsController(ICrudService<HallDto, HallEditDto> service) : CrudControllerBase<HallDto, HallEditDto>(service);