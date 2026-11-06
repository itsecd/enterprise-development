
using FitnessClub.Api.Dtos.Gyms;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Api.Services;

/// <summary>
/// Сервис для работы с залами фитнес-клуба.
/// </summary>
public class GymService(
    IRepository<Gym> gymRepository,
    IRepository<TrainingSession> sessionRepository)
{
    /// <summary>
    /// Получить все залы.
    /// </summary>
    public IReadOnlyList<GymDto> GetAll() => [.. gymRepository.GetAll().Select(ToDto)];

    /// <summary>
    /// Получить зал по идентификатору.
    /// </summary>
    public GymDto? GetById(int id)
    {
        var gym = gymRepository.GetById(id);

        return gym is null ? null : ToDto(gym);
    }

    /// <summary>
    /// Проверить доступность зала.
    /// </summary>
    public bool? IsAvailable(int gymId)
    {
        if (gymRepository.GetById(gymId) is null)
        {
            return null;
        }

        var now = DateTime.Now;

        return !sessionRepository.GetAll().Any(session =>
            session.Gym.Id == gymId &&
            session.EndsAt.HasValue &&
            session.StartsAt <= now &&
            now < session.EndsAt.Value);
    }

    /// <summary>
    /// Создать зал.
    /// </summary>
    public GymDto Create(CreateGymDto dto)
    {
        Validate(dto);

        var gym = new Gym
        {
            Name = dto.Name.Trim()
        };

        return ToDto(gymRepository.Add(gym));
    }

    /// <summary>
    /// Обновить зал.
    /// </summary>
    public bool Update(int id, CreateGymDto dto)
    {
        if (gymRepository.GetById(id) is null)
        {
            return false;
        }

        Validate(dto);

        var gym = new Gym
        {
            Name = dto.Name.Trim()
        };

        return gymRepository.Update(id, gym);
    }

    /// <summary>
    /// Удалить зал.
    /// </summary>
    public bool Delete(int id) => gymRepository.Delete(id);

    /// <summary>
    /// Проверить данные зала.
    /// </summary>
    private static void Validate(CreateGymDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Name))
        {
            throw new ArgumentException(
                "Название зала не может быть пустым.");
        }
    }

    /// <summary>
    /// Преобразовать сущность в DTO.
    /// </summary>
    private static GymDto ToDto(Gym gym) =>
        new(gym.Id, gym.Name);
}
