using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dtos;
using FitnessClub.Contracts.Services;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>
/// Реализует операции для работы с записями на тренировки.
/// </summary>
public class TrainingService(
    ITrainingRepository trainingRepository,
    IClientRepository clientRepository,
    ITrainerRepository trainerRepository,
    IHallRepository hallRepository) : ITrainingService
{
    /// <inheritdoc />
    public async Task<IReadOnlyCollection<TrainingDto>> GetAllAsync()
    {
        var trainings = await trainingRepository.GetAllAsync();

        return [.. trainings.Select(training => training.ToDto())];
    }

    /// <inheritdoc />
    public async Task<TrainingDto?> GetByIdAsync(int id)
    {
        var training = await trainingRepository.GetByIdAsync(id);

        return training?.ToDto();
    }

    /// <inheritdoc />
    public async Task<TrainingDto> CreateAsync(CreateTrainingDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var client = await clientRepository.GetByIdAsync(dto.ClientId);
        var trainer = await trainerRepository.GetByIdAsync(dto.TrainerId);
        var hall = await hallRepository.GetByIdAsync(dto.HallId);

        if (client is null)
        {
            throw new ArgumentException("Клиент не найден.", nameof(dto));
        }

        if (trainer is null)
        {
            throw new ArgumentException("Тренер не найден.", nameof(dto));
        }

        if (hall is null)
        {
            throw new ArgumentException("Зал не найден.", nameof(dto));
        }

        var training = new Training
        {
            Id = 0,
            Client = client,
            Trainer = trainer,
            Hall = hall,
            StartTime = dto.StartTime,
            DurationMinutes = dto.DurationMinutes,
            IsTrial = dto.IsTrial
        };

        var created = await trainingRepository.AddAsync(training);

        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, UpdateTrainingDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var training = await trainingRepository.GetByIdAsync(id);

        if (training is null)
        {
            return false;
        }

        var client = await clientRepository.GetByIdAsync(dto.ClientId);
        var trainer = await trainerRepository.GetByIdAsync(dto.TrainerId);
        var hall = await hallRepository.GetByIdAsync(dto.HallId);

        if (client is null || trainer is null || hall is null)
        {
            throw new ArgumentException(
                "Клиент, тренер или зал не найдены.",
                nameof(dto));
        }

        training.Client = client;
        training.Trainer = trainer;
        training.Hall = hall;
        training.StartTime = dto.StartTime;
        training.DurationMinutes = dto.DurationMinutes;
        training.IsTrial = dto.IsTrial;

        return await trainingRepository.UpdateAsync(id, training);
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int id)
    {
        return trainingRepository.DeleteAsync(id);
    }
}