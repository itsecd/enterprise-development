using Vet.Application.Interfaces;
using Vet.Contracts.Dtos;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Application.Services;

/// <summary>
/// Сервис для управления специализациями
/// </summary>
/// <param name="specializationRepository">Репозиторий специализаций</param>
public class SpecializationService(IRepository<Specialization, int> specializationRepository)
    : IApplicationService<SpecializationDto, SpecializationCreateUpdateDto, int>
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<SpecializationDto>> GetAllAsync()
    {
        var specs = await specializationRepository.GetAllAsync();
        return [.. specs.Select(s => s.ToDto())];
    }

    /// <inheritdoc />
    public async Task<SpecializationDto?> GetByIdAsync(int id)
    {
        var spec = await specializationRepository.GetByIdAsync(id);
        return spec?.ToDto();
    }

    /// <inheritdoc />
    public async Task<SpecializationDto> CreateAsync(SpecializationCreateUpdateDto dto)
    {
        var spec = dto.ToEntity();
        var created = await specializationRepository.AddAsync(spec);
        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, SpecializationCreateUpdateDto dto)
    {
        var existing = await specializationRepository.GetByIdAsync(id);
        if (existing is null)
        {
            return false;
        }

        existing.Name = dto.Name;
        existing.Species = [.. dto.Species];

        return await specializationRepository.UpdateAsync(existing);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        return await specializationRepository.DeleteAsync(id);
    }
}