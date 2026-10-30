using FitnessGym.Contracts.Dtos;
using FitnessGym.Contracts.Interfaces;
using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Enums;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Application.Services;

/// <summary>
/// Сервис для управления тренерами
/// </summary>
public class TrainerService(
    ITrainerRepository trainerRepository,
    ISpecializationRepository specRepository) : ITrainerService
{
    /// <summary>
    /// Получить список всех тренеров
    /// </summary>
    public async Task<IReadOnlyList<TrainerDto>> GetAllAsync()
    {
        var trainers = await trainerRepository.GetAllAsync();
        return trainers.Select(ToDto).ToList();
    }

    /// <summary>
    /// Получить тренера по идентификатору
    /// </summary>
    public async Task<TrainerDto?> GetByIdAsync(int id)
    {
        var trainer = await trainerRepository.GetByIdAsync(id);
        return trainer is null ? null : ToDto(trainer);
    }

    /// <summary>
    /// Создать нового тренера
    /// </summary>
    public async Task<TrainerDto> CreateAsync(TrainerEditDto dto)
    {
        var spec = await specRepository.GetByIdAsync(dto.SpecializationId);
        if (spec is null) throw new ArgumentException("Специализация не найдена");

        var trainer = new Trainer
        {
            PassportNumber = dto.PassportNumber,
            FullName = dto.FullName,
            Gender = Enum.Parse<Gender>(dto.Gender),
            BirthDate = dto.BirthDate,
            Specialization = spec,
            ExperienceYears = dto.ExperienceYears
        };

        var created = await trainerRepository.AddAsync(trainer);
        return ToDto(created);
    }

    /// <summary>
    /// Обновить данные тренера
    /// </summary>
    public async Task<bool> UpdateAsync(int id, TrainerEditDto dto)
    {
        var trainer = await trainerRepository.GetByIdAsync(id);
        if (trainer is null) return false;

        var spec = await specRepository.GetByIdAsync(dto.SpecializationId);
        if (spec is null) return false;

        trainer.PassportNumber = dto.PassportNumber;
        trainer.FullName = dto.FullName;
        trainer.Gender = Enum.Parse<Gender>(dto.Gender);
        trainer.BirthDate = dto.BirthDate;
        trainer.Specialization = spec;
        trainer.ExperienceYears = dto.ExperienceYears;

        return await trainerRepository.UpdateAsync(trainer);
    }

    /// <summary>
    /// Удалить тренера
    /// </summary>
    public Task<bool> DeleteAsync(int id) => trainerRepository.DeleteAsync(id);

    private static TrainerDto ToDto(Trainer t) => new(
        t.Id, 
        t.PassportNumber, 
        t.FullName, 
        t.Gender.ToString(),
        t.BirthDate, 
        t.Specialization.Id, 
        t.Specialization.Name, 
        t.ExperienceYears);
}