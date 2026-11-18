using FitnessClub.Contracts.Dto;

namespace FitnessClub.Contracts.Interfaces;

/// <summary>
/// CRUD-сервис занятий
/// </summary>
public interface ILessonService : ICrudService<LessonDto, LessonEditDto>;