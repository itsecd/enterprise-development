
using FitnessClub.Api.Dtos.TrainingSessions;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Api.Services;

/// <summary>
/// Сервис для работы с записями на занятия.
/// </summary>
public class TrainingSessionService(
    IRepository<TrainingSession> sessionRepository,
    IRepository<Client> clientRepository,
    IRepository<Trainer> trainerRepository,
    IRepository<Gym> gymRepository)
{
    /// <summary>
    /// Получить все записи на занятия.
    /// </summary>
    public IReadOnlyList<TrainingSessionDto> GetAll() =>
        [.. sessionRepository.GetAll().Select(ToDto)];

    /// <summary>
    /// Получить запись по идентификатору.
    /// </summary>
    public TrainingSessionDto? GetById(int id)
    {
        var session = sessionRepository.GetById(id);

        return session is null ? null : ToDto(session);
    }

    /// <summary>
    /// Получить занятия за текущий месяц в выбранном зале.
    /// </summary>
    public IReadOnlyList<TrainingSessionDto>? GetCurrentMonthByGym(int gymId)
    {
        if (gymRepository.GetById(gymId) is null)
        {
            return null;
        }

        var now = DateTime.Now;

        return [.. sessionRepository.GetAll()
            .Where(session =>
                session.Gym.Id == gymId &&
                session.StartsAt.Year == now.Year &&
                session.StartsAt.Month == now.Month)
            .OrderBy(session => session.StartsAt)
            .Select(ToDto)];
    }

    /// <summary>
    /// Создать запись на занятие.
    /// </summary>
    public TrainingSessionDto Create(CreateTrainingSessionDto dto)
    {
        Validate(dto);

        var session = new TrainingSession
        {
            Client = GetClient(dto.ClientId),
            Trainer = GetTrainer(dto.TrainerId),
            Gym = GetGym(dto.GymId),
            StartsAt = dto.StartsAt,
            EndsAt = dto.EndsAt,
            IsTrial = dto.IsTrial
        };

        return ToDto(sessionRepository.Add(session));
    }

    /// <summary>
    /// Обновить запись на занятие.
    /// </summary>
    public bool Update(int id, CreateTrainingSessionDto dto)
    {
        if (sessionRepository.GetById(id) is null)
        {
            return false;
        }

        Validate(dto);

        var session = new TrainingSession
        {
            Client = GetClient(dto.ClientId),
            Trainer = GetTrainer(dto.TrainerId),
            Gym = GetGym(dto.GymId),
            StartsAt = dto.StartsAt,
            EndsAt = dto.EndsAt,
            IsTrial = dto.IsTrial
        };

        return sessionRepository.Update(id, session);
    }

    /// <summary>
    /// Удалить запись на занятие.
    /// </summary>
    public bool Delete(int id) => sessionRepository.Delete(id);

    private Client GetClient(int id) =>
        clientRepository.GetById(id)
        ?? throw new ArgumentException(
            "Клиент с указанным ID не найден.");

    private Trainer GetTrainer(int id) =>
        trainerRepository.GetById(id)
        ?? throw new ArgumentException(
            "Тренер с указанным ID не найден.");

    private Gym GetGym(int id) =>
        gymRepository.GetById(id)
        ?? throw new ArgumentException(
            "Зал с указанным ID не найден.");

    private static void Validate(CreateTrainingSessionDto dto)
    {
        if (dto.StartsAt == default)
        {
            throw new ArgumentException(
                "Необходимо указать дату и время начала занятия.");
        }

        if (dto.EndsAt.HasValue &&
            dto.EndsAt.Value <= dto.StartsAt)
        {
            throw new ArgumentException(
                "Время окончания должно быть позже времени начала.");
        }
    }

    private static TrainingSessionDto ToDto(
        TrainingSession session) => new(
            session.Id,
            session.Client.Id,
            session.Client.FullName,
            session.Trainer.Id,
            session.Trainer.FullName,
            session.StartsAt,
            session.EndsAt,
            session.Gym.Id,
            session.Gym.Name,
            session.IsTrial);
}
