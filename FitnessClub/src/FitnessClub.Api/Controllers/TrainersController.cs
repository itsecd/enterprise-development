using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Управление тренерами
/// </summary>
public class TrainersController(ICrudService<TrainerDto, TrainerEditDto> service) : CrudControllerBase<TrainerDto, TrainerEditDto>(service);