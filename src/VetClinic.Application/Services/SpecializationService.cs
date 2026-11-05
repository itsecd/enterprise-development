using Microsoft.Extensions.Logging;
using VetClinic.Application.Mapping;
using VetClinic.Application.Validation;
using VetClinic.Contracts.Specializations;
using VetClinic.Domain.Enums;
using VetClinic.Domain.Exceptions;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Application.Services;

public sealed class SpecializationService(
    ISpecializationRepository specializations,
    ILogger<SpecializationService> logger) : ISpecializationService
{
    public async Task<IReadOnlyList<SpecializationDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение списка специализаций");
        var result = (await specializations.GetAllAsync(cancellationToken)).Select(ClinicMapper.ToSpecialization).ToArray();
        logger.LogInformation("Список специализаций получен, записей: {Count}", result.Length);
        return result;
    }

    public async Task<SpecializationDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение специализации {SpecializationId}", id);
        var specialization = await specializations.FindByIdAsync(id, cancellationToken)
            ?? throw new EntityNotFoundException("специализация", id);
        logger.LogInformation("Специализация {SpecializationId} получена", id);
        return ClinicMapper.ToSpecialization(specialization);
    }

    public async Task<IReadOnlyList<SpecializationDto>> GetBySpeciesAsync(AnimalSpecies species, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение специализаций вида {Species}", species);
        var selected = DtoValidator.RequireSpecies(species);
        var result = (await specializations.GetBySpeciesAsync(selected, cancellationToken)).Select(ClinicMapper.ToSpecialization).ToArray();
        logger.LogInformation("Специализации вида {Species} получены, записей: {Count}", selected, result.Length);
        return result;
    }

    public async Task<SpecializationDto> CreateAsync(SpecializationEditDto dto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Создание специализации");
        DtoValidator.EnsureValid(dto);
        var created = await specializations.AddAsync(
            DtoValidator.RequireText(dto.Name, "Укажите название специализации."),
            DtoValidator.RequireSpecies(dto.Species), cancellationToken);
        logger.LogInformation("Специализация {SpecializationId} создана", created.Id);
        return ClinicMapper.ToSpecialization(created);
    }

    public async Task<SpecializationDto> UpdateAsync(int id, SpecializationEditDto dto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Обновление специализации {SpecializationId}", id);
        DtoValidator.EnsureValid(dto);
        var updated = await specializations.UpdateAsync(
            id,
            DtoValidator.RequireText(dto.Name, "Укажите название специализации."),
            DtoValidator.RequireSpecies(dto.Species), cancellationToken);
        logger.LogInformation("Специализация {SpecializationId} обновлена", id);
        return ClinicMapper.ToSpecialization(updated);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Удаление специализации {SpecializationId}", id);
        await specializations.RemoveAsync(id, cancellationToken);
        logger.LogInformation("Специализация {SpecializationId} удалена", id);
    }
}
