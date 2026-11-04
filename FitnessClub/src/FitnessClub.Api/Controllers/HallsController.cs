using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Управление залами
/// </summary>
public class HallsController(ICrudService<HallDto, HallEditDto> service) : CrudControllerBase<HallDto, HallEditDto>(service);