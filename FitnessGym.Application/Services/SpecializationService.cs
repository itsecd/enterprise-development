using FitnessGym.Contracts.Dtos;
using FitnessGym.Contracts.Interfaces;
using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Application.Services;

/// <summary>
/// Сервис для управления специализациями
/// </summary>
public class SpecializationService(ISpecializationRepository repository) : ISpecializationService
{
    /// <summary>
    /// Получить список всех специализаций
    /// </summary>
    public async Task<IReadOnlyList<SpecializationDto>> GetAllAsync()
    {
        var specs = await repository.GetAllAsync();
        return specs.Select(s => new SpecializationDto(s.Id, s.Name)).ToList();
    }

    /// <summary>
    /// Получить специализацию по идентификатору
    /// </summary>
    public async Task<SpecializationDto?> GetByIdAsync(int id)
    {
        var spec = await repository.GetByIdAsync(id);
        return spec is null ? null : new SpecializationDto(spec.Id, spec.Name);
    }

    /// <summary>
    /// Создать новую специализацию
    /// </summary>
    public async Task<SpecializationDto> CreateAsync(SpecializationEditDto dto)
    {
        var spec = new Specialization { Name = dto.Name };
        var created = await repository.AddAsync(spec);
        return new SpecializationDto(created.Id, created.Name);
    }

    /// <summary>
    /// Удалить специализацию
    /// </summary>
    public Task<bool> DeleteAsync(int id) => repository.DeleteAsync(id);
}