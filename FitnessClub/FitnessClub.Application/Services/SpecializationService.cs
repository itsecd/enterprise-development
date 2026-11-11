using FitnessClub.Application.Mapping;
using FitnessClub.Contracts.Dtos;
using FitnessClub.Contracts.Services;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>
/// Реализует операции для работы со специализациями тренеров.
/// </summary>
/// <param name="repository">Репозиторий специализаций.</param>
public class SpecializationService(ISpecializationRepository repository) : ISpecializationService
{
    /// <inheritdoc />
    public async Task<IReadOnlyCollection<SpecializationDto>> GetAllAsync()
    {
        var items = await repository.GetAllAsync();

        return [.. items.Select(item => item.ToDto())];
    }

    /// <inheritdoc />
    public async Task<SpecializationDto?> GetByIdAsync(int id)
    {
        var item = await repository.GetByIdAsync(id);

        return item?.ToDto();
    }

    /// <inheritdoc />
    public async Task<SpecializationDto> CreateAsync(
        CreateSpecializationDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var specialization = new Specialization
        {
            Id = 0,
            Name = dto.Name,
            Description = dto.Description
        };

        var created = await repository.AddAsync(specialization);

        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(
        int id,
        UpdateSpecializationDto dto)
    {
        ArgumentNullException.ThrowIfNull(dto);

        var specialization = await repository.GetByIdAsync(id);

        if (specialization is null)
        {
            return false;
        }

        specialization.Name = dto.Name;
        specialization.Description = dto.Description;

        return await repository.UpdateAsync(id, specialization);
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int id)
    {
        return repository.DeleteAsync(id);
    }
}