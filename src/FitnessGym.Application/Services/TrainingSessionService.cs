using System.ComponentModel.DataAnnotations;
using FitnessGym.Contracts.Dto;
using FitnessGym.Contracts.Interfaces;
using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Application.Services;

/// <summary>
/// Операции с занятиями и преобразование данных в DTO.
/// </summary>
public class TrainingSessionService(
    ITrainingSessionRepository sessionRepository,
    IClientRepository clientRepository,
    ITrainerRepository trainerRepository,
    Func<DateTime>? clock = null) : ITrainingSessionService
{
    private readonly Func<DateTime> _clock = clock ?? (() => DateTime.Now);

    /// <inheritdoc/>
    public async Task<IReadOnlyList<TrainingSessionDto>> GetSessionsAsync() =>
        [.. (await sessionRepository.GetAllAsync()).Select(MapToDto)];

    /// <inheritdoc/>
    public async Task<TrainingSessionDto?> GetSessionAsync(int id)
    {
        var session = await sessionRepository.GetByIdAsync(id);
        return session is null ? null : MapToDto(session);
    }

    /// <inheritdoc/>
    public async Task<TrainingSessionDto> AddSessionAsync(UpsertTrainingSessionDto session) =>
        MapToDto(await sessionRepository.AddAsync(await MapToEntityAsync(session)));

    /// <inheritdoc/>
    public async Task<TrainingSessionDto?> UpdateSessionAsync(int id, UpsertTrainingSessionDto session)
    {
        if (await sessionRepository.GetByIdAsync(id) is null)
        {
            return null;
        }

        var entity = await MapToEntityAsync(session);
        if (!await sessionRepository.UpdateAsync(id, entity))
        {
            return null;
        }

        entity.Id = id;
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public Task<bool> DeleteSessionAsync(int id) => sessionRepository.DeleteAsync(id);

    /// <inheritdoc/>
    public async Task<IReadOnlyList<TrainingSessionDto>> GetSessionsForCurrentMonthAsync(string hallName)
    {
        if (string.IsNullOrWhiteSpace(hallName))
        {
            throw new ValidationException("Не указано название зала.");
        }

        return [.. (await sessionRepository.GetForMonthAsync(hallName, _clock())).Select(MapToDto)];
    }

    private static TrainingSessionDto MapToDto(TrainingSession session) => new()
    {
        Id = session.Id,
        ClientId = session.ClientId,
        TrainerId = session.TrainerId,
        StartsAt = session.StartsAt,
        EndsAt = session.EndsAt,
        HallName = session.HallName,
        IsTrial = session.IsTrial,
    };

    private async Task<TrainingSession> MapToEntityAsync(UpsertTrainingSessionDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto), validateAllProperties: true);
        return new TrainingSession
        {
            ClientId = dto.ClientId ?? throw new ValidationException("Не указан клиент."),
            TrainerId = dto.TrainerId ?? throw new ValidationException("Не указан тренер."),
            Client = await clientRepository.GetByIdAsync(
                    dto.ClientId ?? throw new ValidationException("Не указан клиент."))
                    ?? throw new ValidationException("Клиент не найден."),
            Trainer = await trainerRepository.GetByIdAsync(
                    dto.TrainerId ?? throw new ValidationException("Не указан тренер."))
                    ?? throw new ValidationException("Тренер не найден."),
            StartsAt = dto.StartsAt
                ?? throw new ValidationException("Время начала занятия обязательно."),
            EndsAt = dto.EndsAt,
            HallName = dto.HallName,
            IsTrial = dto.IsTrial,
        };
    }
}
