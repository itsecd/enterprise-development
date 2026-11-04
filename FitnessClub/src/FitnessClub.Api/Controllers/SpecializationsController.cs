using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Управление специализациями
/// </summary>
public class SpecializationsController(ICrudService<SpecializationDto, SpecializationEditDto> service) : CrudControllerBase<SpecializationDto, SpecializationEditDto>(service);