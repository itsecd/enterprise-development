using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// CRUD-сервис специализаций
/// </summary>
public interface ISpecializationService : ICrudService<SpecializationDto, SpecializationEditDto>;