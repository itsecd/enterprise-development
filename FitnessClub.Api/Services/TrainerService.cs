
using FitnessClub.Api.Dtos.Trainers;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Api.Services;

/// <summary>
/// Сервис для работы с тренерами.
/// </summary>
public class TrainerService(
    IRepository<Trainer> trainerRepository,
    IRepository<Specialization> specializationRepository,
    IRepository<TrainingSession> sessionRepository)
{
    /// <summary>
    /// Получить всех тренеров.
    /// </summary>
    public IReadOnlyList<TrainerDto> GetAll() =>
        [.. trainerRepository.GetAll().Select(ToDto)];

    /// <summary>
    /// Получить тренера по идентификатору.
    /// </summary>
    public TrainerDto? GetById(int id)
    {
        var trainer = trainerRepository.GetById(id);
        return trainer is null ? null : ToDto(trainer);
    }

    /// <summary>
    /// Получить тренеров со стажем не менее 5 лет.
    /// </summary>
    public IReadOnlyList<TrainerDto> GetExperienced()
    {
        return [.. trainerRepository.GetAll()
            .Where(trainer => trainer.WorkExperience >= 5)
            .Select(ToDto)];
    }

    /// <summary>
    /// Получить пять самых популярных тренеров.
    /// </summary>
    public IReadOnlyList<TrainerDto> GetTopFive()
    {
        var trainerIds = sessionRepository.GetAll()
            .GroupBy(session => session.Trainer.Id)
            .OrderByDescending(group => group.Count())
            .ThenBy(group => group.Key)
            .Take(5)
            .Select(group => group.Key);

        return [.. trainerIds
            .Select(trainerRepository.GetById)
            .Where(trainer => trainer is not null)
            .Select(trainer => ToDto(trainer!))];
    }

    /// <summary>
    /// Создать нового тренера.
    /// </summary>
    public TrainerDto Create(CreateTrainerDto dto)
    {
        Validate(dto);
        CheckPassport(dto.PassportNumber);

        var specialization = GetSpecialization(dto.SpecializationId);
        var trainer = ToEntity(dto, specialization);

        return ToDto(trainerRepository.Add(trainer));
    }

    /// <summary>
    /// Обновить информацию о тренере.
    /// </summary>
    public bool Update(int id, CreateTrainerDto dto)
    {
        if (trainerRepository.GetById(id) is null)
        {
            return false;
        }

        Validate(dto);
        CheckPassport(dto.PassportNumber, id);

        var specialization = GetSpecialization(dto.SpecializationId);
        var trainer = ToEntity(dto, specialization);

        return trainerRepository.Update(id, trainer);
    }

    /// <summary>
    /// Удалить тренера.
    /// </summary>
    public bool Delete(int id) => trainerRepository.Delete(id);

    /// <summary>
    /// Проверить данные тренера.
    /// </summary>
    private static void Validate(CreateTrainerDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.PassportNumber) ||
            string.IsNullOrWhiteSpace(dto.FullName))
        {
            throw new ArgumentException(
                "Паспорт и ФИО тренера обязательны.");
        }

        if (!Enum.IsDefined(dto.Gender))
        {
            throw new ArgumentException(
                "Некорректное значение пола.");
        }

        if (dto.BirthDate == default ||
            dto.BirthDate >= DateOnly.FromDateTime(DateTime.Today))
        {
            throw new ArgumentException(
                "Некорректная дата рождения.");
        }

        if (dto.WorkExperience < 0)
        {
            throw new ArgumentException(
                "Стаж работы не может быть отрицательным.");
        }
    }

    /// <summary>
    /// Проверить уникальность номера паспорта.
    /// </summary>
    private void CheckPassport(string passportNumber, int? currentId = null)
    {
        if (trainerRepository.GetAll().Any(trainer =>
            trainer.PassportNumber == passportNumber &&
            trainer.Id != currentId))
        {
            throw new InvalidOperationException(
                "Тренер с таким номером паспорта уже существует.");
        }
    }

    /// <summary>
    /// Найти специализацию по идентификатору.
    /// </summary>
    private Specialization GetSpecialization(int id)
    {
        return specializationRepository.GetById(id)
            ?? throw new ArgumentException(
                "Указанная специализация не найдена.");
    }

    /// <summary>
    /// Преобразовать DTO в сущность.
    /// </summary>
    private static Trainer ToEntity(
        CreateTrainerDto dto,
        Specialization specialization) => new()
        {
            PassportNumber = dto.PassportNumber,
            FullName = dto.FullName,
            Gender = dto.Gender,
            BirthDate = dto.BirthDate,
            Specialization = specialization,
            WorkExperience = dto.WorkExperience
        };

    /// <summary>
    /// Преобразовать сущность в DTO.
    /// </summary>
    private static TrainerDto ToDto(Trainer trainer) => new(
        trainer.Id,
        trainer.PassportNumber,
        trainer.FullName,
        trainer.Gender,
        trainer.BirthDate,
        trainer.Specialization.Id,
        trainer.Specialization.Name,
        trainer.WorkExperience);
}
