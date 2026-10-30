using FitnessGym.Contracts.Dtos;
using FitnessGym.Contracts.Interfaces;
using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Enums;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Application.Services;

/// <summary>
/// Сервис для управления занятиями
/// </summary>
public class TrainingSessionService(
    ITrainingSessionRepository sessionRepository,
    IClientRepository clientRepository,
    ITrainerRepository trainerRepository) : ITrainingSessionService
{
    /// <summary>
    /// Получить список всех занятий
    /// </summary>
    public async Task<IReadOnlyList<TrainingSessionDto>> GetAllAsync()
    {
        var sessions = await sessionRepository.GetAllAsync();
        return sessions.Select(ToDto).ToList();
    }

    /// <summary>
    /// Получить занятие по идентификатору
    /// </summary>
    public async Task<TrainingSessionDto?> GetByIdAsync(int id)
    {
        var session = await sessionRepository.GetByIdAsync(id);
        return session is null ? null : ToDto(session);
    }

    /// <summary>
    /// Создать новую запись на занятие
    /// </summary>
    public async Task<TrainingSessionDto> CreateAsync(TrainingSessionEditDto dto)
    {
        var client = await clientRepository.GetByIdAsync(dto.ClientId);
        var trainer = await trainerRepository.GetByIdAsync(dto.TrainerId);

        if (client is null || trainer is null)
            throw new ArgumentException("Клиент или тренер не найдены");

        var session = new TrainingSession
        {
            Client = client,
            Trainer = trainer,
            StartsTraining = dto.StartsTraining,
            EndsTraining = dto.EndsTraining,
            HallName = Enum.Parse<Hall>(dto.Hall),
            IsTrial = dto.IsTrial
        };

        var created = await sessionRepository.AddAsync(session);
        return ToDto(created);
    }

    /// <summary>
    /// Удалить запись о занятии
    /// </summary>
    public Task<bool> DeleteAsync(int id) => sessionRepository.DeleteAsync(id);

    private static TrainingSessionDto ToDto(TrainingSession s) => new(
        s.Id, 
        s.Client.Id, 
        s.Client.FullName, 
        s.Trainer.Id,
        s.Trainer.FullName,
        s.StartsTraining, 
        s.EndsTraining, 
        s.HallName.ToString(), 
        s.IsTrial);
}