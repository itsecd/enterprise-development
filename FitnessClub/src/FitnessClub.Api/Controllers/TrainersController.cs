using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;

namespace FitnessClub.Api.Controllers;

/// <summary>
/// Управление тренерами
/// </summary>
/// <param name="service">
/// Сервис управления тренерами
/// </param>
public class TrainersController(ICrudService<TrainerDto, TrainerEditDto> service) : CrudControllerBase<TrainerDto, TrainerEditDto>(service);