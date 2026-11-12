using System.ComponentModel.DataAnnotations;
using FitnessGym.Application.Exceptions;
using FitnessGym.Contracts.Dto;
using FitnessGym.Contracts.Interfaces;
using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Application.Services;

/// <summary>
/// Операции с специализациями и преобразование данных в DTO.
/// </summary>
public class SpecializationService(
    ISpecializationRepository specializationRepository,
    ITrainerRepository trainerRepository) : ISpecializationService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<SpecializationDto>> GetSpecializationsAsync() =>
        [.. (await specializationRepository.GetAllAsync()).Select(MapToDto)];

    /// <inheritdoc/>
    public async Task<SpecializationDto?> GetSpecializationAsync(int id)
    {
        var specialization = await specializationRepository.GetByIdAsync(id);
        return specialization is null ? null : MapToDto(specialization);
    }

    /// <inheritdoc/>
    public async Task<SpecializationDto> AddSpecializationAsync(UpsertSpecializationDto specialization) =>
        MapToDto(await specializationRepository.AddAsync(MapToEntity(specialization)));

    /// <inheritdoc/>
    public async Task<SpecializationDto?> UpdateSpecializationAsync(int id, UpsertSpecializationDto specialization)
    {
        if (await specializationRepository.GetByIdAsync(id) is null)
        {
            return null;
        }

        var entity = MapToEntity(specialization);
        if (!await specializationRepository.UpdateAsync(id, entity))
        {
            return null;
        }

        entity.Id = id;
        return MapToDto(entity);
    }

    /// <inheritdoc/>
    public async Task<bool> DeleteSpecializationAsync(int id)
    {
        if (await trainerRepository.HasSpecializationAsync(id))
        {
            throw new EntityInUseException("Специализация назначена тренеру — сначала измените его специализацию.");
        }

        return await specializationRepository.DeleteAsync(id);
    }

    private static SpecializationDto MapToDto(Specialization specialization) => new()
    {
        Id = specialization.Id,
        Name = specialization.Name,
    };

    private static Specialization MapToEntity(UpsertSpecializationDto dto)
    {
        Validator.ValidateObject(dto, new ValidationContext(dto), validateAllProperties: true);
        return new Specialization
        {
            Name = dto.Name,
        };
    }
}
