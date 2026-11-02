using FitnessClub.Application.Exceptions;
using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dtos.Edit;
using FitnessClub.Contracts.Dtos.Read;
using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>
/// Управление специализациями тренеров
/// </summary>
public class SpecializationService(
    IRepository<Specialization> specializationRepository,
    IRepository<Trainer> trainerRepository) : ICrudService<SpecializationDto, SpecializationEditDto>
{
    /// <summary>
    /// Получить список специализаций
    /// </summary>
    public async Task<IReadOnlyList<SpecializationDto>> GetAllAsync()
    {
        var specializations = await specializationRepository.GetAllAsync();
        return [.. specializations.Select(s => s.ToDto())];
    }
    /// <summary>
    /// Получить специализацию по идентификатору
    /// </summary>
    public async Task<SpecializationDto?> GetByIdAsync(int id)
    {
        var specialization = await specializationRepository.GetByIdAsync(id);
        return specialization?.ToDto();
    }
    /// <summary>
    /// Добавить специализацию
    /// </summary>
    public async Task<SpecializationDto> AddAsync(SpecializationEditDto editDto)
    {
        var specialization = await specializationRepository.AddAsync(editDto.ToEntity());
        return specialization.ToDto();
    }
    /// <summary>
    /// Обновить специализацию
    /// </summary>
    public async Task<SpecializationDto?> UpdateAsync(int id, SpecializationEditDto editDto)
    {
        var specialization = await specializationRepository.GetByIdAsync(id);
        if (specialization is null)
            return null;
        specialization.UpdateEntity(editDto);
        await specializationRepository.UpdateAsync(specialization);
        return specialization.ToDto();
    }
    /// <summary>
    /// Удалить специализацию при отсутствии связанных тренеров
    /// </summary>
    public async Task<bool> DeleteAsync(int id)
    {
        var allTrainers = await trainerRepository.GetAllAsync();
        if (allTrainers.Any(t => t.Specialization.Id == id))
        {
            throw new ConflictException("Невозможно удалить специализацию, так как она назначена тренерам.");
        }
        return await specializationRepository.DeleteAsync(id);
    }
}