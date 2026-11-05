using Microsoft.Extensions.Logging;
using VetClinic.Application.Mapping;
using VetClinic.Application.Validation;
using VetClinic.Contracts.Breeds;
using VetClinic.Domain.Enums;
using VetClinic.Domain.Exceptions;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Application.Services;

public sealed class BreedService(IBreedRepository breeds, ILogger<BreedService> logger) : IBreedService
{
    public async Task<IReadOnlyList<BreedDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение списка пород");
        var result = (await breeds.GetAllAsync(cancellationToken)).Select(ClinicMapper.ToBreed).ToArray();
        logger.LogInformation("Список пород получен, записей: {Count}", result.Length);
        return result;
    }

    public async Task<BreedDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение породы {BreedId}", id);
        var breed = await breeds.FindByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException("порода", id);
        logger.LogInformation("Порода {BreedId} получена", id);
        return ClinicMapper.ToBreed(breed);
    }

    public async Task<IReadOnlyList<BreedDto>> GetBySpeciesAsync(AnimalSpecies species, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение пород вида {Species}", species);
        var selected = DtoValidator.RequireSpecies(species);
        var result = (await breeds.GetBySpeciesAsync(selected, cancellationToken)).Select(ClinicMapper.ToBreed).ToArray();
        logger.LogInformation("Породы вида {Species} получены, записей: {Count}", selected, result.Length);
        return result;
    }

    public async Task<BreedDto> CreateAsync(BreedEditDto dto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Создание породы");
        DtoValidator.EnsureValid(dto);
        var created = await breeds.AddAsync(DtoValidator.RequireText(dto.Name, "Укажите название породы."), DtoValidator.RequireSpecies(dto.Species), cancellationToken);
        logger.LogInformation("Порода {BreedId} создана", created.Id);
        return ClinicMapper.ToBreed(created);
    }

    public async Task<BreedDto> UpdateAsync(int id, BreedEditDto dto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Обновление породы {BreedId}", id);
        DtoValidator.EnsureValid(dto);
        var updated = await breeds.UpdateAsync(
            id,
            DtoValidator.RequireText(dto.Name, "Укажите название породы."),
            DtoValidator.RequireSpecies(dto.Species), cancellationToken);
        logger.LogInformation("Порода {BreedId} обновлена", id);
        return ClinicMapper.ToBreed(updated);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Удаление породы {BreedId}", id);
        await breeds.RemoveAsync(id, cancellationToken);
        logger.LogInformation("Порода {BreedId} удалена", id);
    }
}
