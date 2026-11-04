using Vet.Application.Interfaces;
using Vet.Contracts.Dtos;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Application.Services;

/// <summary>
/// Сервис для управления ветеринарами
/// </summary>
/// <param name="veterinarianRepository">Репозиторий ветеринаров</param>
/// <param name="specializationRepository">Репозиторий специализаций</param>
public class VeterinarianService(
    IRepository<Veterinarian, int> veterinarianRepository,
    IRepository<Specialization, int> specializationRepository)
    : IApplicationService<VeterinarianDto, VeterinarianCreateUpdateDto, int>
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<VeterinarianDto>> GetAllAsync()
    {
        var vets = await veterinarianRepository.GetAllAsync();
        return [.. vets.Select(v => v.ToDto())];
    }

    /// <inheritdoc />
    public async Task<VeterinarianDto?> GetByIdAsync(int id)
    {
        var vet = await veterinarianRepository.GetByIdAsync(id);
        return vet?.ToDto();
    }

    /// <inheritdoc />
    public async Task<VeterinarianDto> CreateAsync(VeterinarianCreateUpdateDto dto)
    {
        var spec = await specializationRepository.GetByIdAsync(dto.SpecializationId)
            ?? throw new KeyNotFoundException($"Специализация с Id {dto.SpecializationId} не найдена");

        var vet = dto.ToEntity(spec);
        var created = await veterinarianRepository.AddAsync(vet);

        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, VeterinarianCreateUpdateDto dto)
    {
        var existing = await veterinarianRepository.GetByIdAsync(id);
        if (existing is null)
        {
            return false;
        }

        var spec = await specializationRepository.GetByIdAsync(dto.SpecializationId)
            ?? throw new KeyNotFoundException($"Специализация с Id {dto.SpecializationId} не найдена");

        existing.PassportNumber = dto.PassportNumber;
        existing.FullName = dto.FullName;
        existing.Specialization = spec;
        existing.BirthYear = dto.BirthYear;
        existing.WorkExperience = dto.WorkExperience;

        return await veterinarianRepository.UpdateAsync(existing);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        return await veterinarianRepository.DeleteAsync(id);
    }
}