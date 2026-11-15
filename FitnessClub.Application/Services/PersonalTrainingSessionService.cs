using FitnessClub.Application.Contracts;
using FitnessClub.Application.Contracts.PersonalTrainingSessions;
using FitnessClub.Domain.Repositories;
using FitnessClub.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace FitnessClub.Application.Services;

/// <summary>
/// Cервис для работы с персональными тренировками
/// </summary>
public class PersonalTrainingSessionService(
    IPersonalTrainingSessionRepository sessionRepository,
    IClientRepository clientRepository,
    ITrainerRepository trainerRepository,
    ILogger<PersonalTrainingSessionService> logger)
    : IApplicationService<
        PersonalTrainingSessionDto,
        PersonalTrainingSessionCreateUpdateDto,
        int>
{
    /// <inheritdoc/>
    public async Task<PersonalTrainingSessionDto> Create(PersonalTrainingSessionCreateUpdateDto dto)
    {
        logger.LogInformation("Создание персональной тренировки");
        var client = await clientRepository.Get(dto.ClientId);
        if (client is null)
        {
            logger.LogWarning("Клиент c Id: {Id} не найден",dto.ClientId);
            throw new KeyNotFoundException($"Клиент с Id: {dto.ClientId} не найден");
        }
        var trainer = await trainerRepository.Get(dto.TrainerId);
        if (trainer is null)
        {
            logger.LogWarning("Тренер c Id: {Id} не найден",dto.TrainerId);
            throw new KeyNotFoundException($"Тренер с Id: {dto.TrainerId} не найден");
        }
        var personalTrainingSession = new PersonalTrainingSession
        {
            Id = 0,
            Client = client,
            Trainer = trainer,
            TrainDay = dto.TrainDay,
            HallName = dto.HallName,
            IsTrial = dto.IsTrial
        };
        await sessionRepository.Add(personalTrainingSession);
        var result = new PersonalTrainingSessionDto(
            personalTrainingSession.Id,
            client.Id,
            trainer.Id,
            personalTrainingSession.TrainDay,
            personalTrainingSession.HallName,
            personalTrainingSession.IsTrial);
        logger.LogInformation("Id созданного персонального занятия: {Id}",personalTrainingSession.Id);
        return result;
    }
    /// <inheritdoc/>
    public async Task<PersonalTrainingSessionDto?> Get(int id)
    {
        logger.LogInformation("Получение персональной тренировки c Id: {Id}",id);
        var personalTrainingSession = await sessionRepository.Get(id);
        if (personalTrainingSession is null)
        {
            logger.LogWarning("Персональная тренировка c Id: {Id} не найдена",id);
            return null;
        }
        var result = new PersonalTrainingSessionDto(
            personalTrainingSession.Id,
            personalTrainingSession.Client.Id,
            personalTrainingSession.Trainer.Id,
            personalTrainingSession.TrainDay,
            personalTrainingSession.HallName,
            personalTrainingSession.IsTrial);
        logger.LogInformation("Персональная тренировка c Id: {Id} найдена",id);
        return result;
    }
    /// <inheritdoc/>
    public async Task<IList<PersonalTrainingSessionDto>> GetAll()
    {
        logger.LogInformation("Получение списка персональных тренировок");
        var sessions = await sessionRepository.GetAll();
        var result = sessions.Select(session=>new PersonalTrainingSessionDto
            (
                session.Id,
                session.Client.Id,
                session.Trainer.Id,
                session.TrainDay,
                session.HallName,
                session.IsTrial))
            .ToList();
        logger.LogInformation("Получено персональных тренировок: {Count}",result.Count);
        return result;
    }
    /// <inheritdoc/>
    public async Task<PersonalTrainingSessionDto> Update(PersonalTrainingSessionCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление персональной тренировки с Id: {Id}",id);
        var personalTrainingSession = await sessionRepository.Get(id);
        if (personalTrainingSession is null)
        {
            logger.LogWarning("Персональная тренировка c Id: {Id} не найдена",id);
            throw new KeyNotFoundException($"Персональная тренировка с Id: {id} не найдена");
        }
        var client = await clientRepository.Get(dto.ClientId);
        if (client is null)
        {
            logger.LogWarning("Клиент c Id: {Id} не найден",dto.ClientId);
            throw new KeyNotFoundException($"Клиент с Id: {dto.ClientId} не найден");
        }
        var trainer = await trainerRepository.Get(dto.TrainerId);
        if (trainer is null)
        {
            logger.LogWarning("Тренер c Id: {Id} не найден",dto.TrainerId);
            throw new KeyNotFoundException($"Тренер с Id: {dto.TrainerId} не найден");
        }
        personalTrainingSession.Client = client;
        personalTrainingSession.Trainer = trainer;
        personalTrainingSession.TrainDay = dto.TrainDay;
        personalTrainingSession.HallName = dto.HallName;
        personalTrainingSession.IsTrial = dto.IsTrial;
        await sessionRepository.Update(personalTrainingSession);
        var result = new PersonalTrainingSessionDto(
            personalTrainingSession.Id,
            personalTrainingSession.Client.Id,
            personalTrainingSession.Trainer.Id,
            personalTrainingSession.TrainDay,
            personalTrainingSession.HallName,
            personalTrainingSession.IsTrial);
        logger.LogInformation("Персональная тренировка с Id: {Id} обновлена",id);
        return result;
    }
    /// <inheritdoc/>
    public async Task<bool> Delete(int id)
    {
        logger.LogInformation("Удаление персональной тренировки с Id: {Id}",id);
        var personalTrainingSession = await sessionRepository.Get(id);
        if (personalTrainingSession is null)
        {
            logger.LogWarning("Персональная тренировка c Id: {Id} не найдена",id);
            return false;
        }
        var result = await sessionRepository.Delete(id);
        if (result)
        {
            logger.LogInformation("Персональная тренировка с Id: {Id} удалена",id);
        }
        return result;
    }
}