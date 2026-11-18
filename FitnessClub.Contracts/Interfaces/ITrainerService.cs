using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// CRUD-сервис тренеров
/// </summary>
public interface ITrainerService : ICrudService<TrainerDto, TrainerEditDto>;