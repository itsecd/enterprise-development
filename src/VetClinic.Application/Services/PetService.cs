using Microsoft.Extensions.Logging;
using VetClinic.Application.Mapping;
using VetClinic.Application.Validation;
using VetClinic.Contracts.Pets;
using VetClinic.Domain.Exceptions;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Application.Services;

public sealed class PetService(IPetRepository pets, TimeProvider time, ILogger<PetService> logger) : IPetService
{
    public async Task<IReadOnlyList<PetDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение списка питомцев");
        var result = (await pets.GetAllAsync(cancellationToken)).Select(ClinicMapper.ToPet).ToArray();
        logger.LogInformation("Список питомцев получен, записей: {Count}", result.Length);
        return result;
    }

    public async Task<PetDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение питомца {PetId}", id);
        var pet = await pets.FindByIdAsync(id, cancellationToken) ?? throw new EntityNotFoundException("питомец", id);
        logger.LogInformation("Питомец {PetId} получен", id);
        return ClinicMapper.ToPet(pet);
    }

    public async Task<IReadOnlyList<PetDto>> GetByOwnerAsync(int ownerId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение питомцев владельца {OwnerId}", ownerId);
        var result = (await pets.GetByOwnerAsync(ownerId, cancellationToken)).Select(ClinicMapper.ToPet).ToArray();
        logger.LogInformation("Питомцы владельца {OwnerId} получены, записей: {Count}", ownerId, result.Length);
        return result;
    }

    public async Task<IReadOnlyList<PetDto>> GetByVeterinarianAsync(int veterinarianId, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Получение питомцев врача {VeterinarianId}", veterinarianId);
        var result = (await pets.GetByVeterinarianAsync(veterinarianId, cancellationToken)).Select(ClinicMapper.ToPet).ToArray();
        logger.LogInformation(
            "Питомцы врача {VeterinarianId} получены, записей: {Count}",
            veterinarianId,
            result.Length);
        return result;
    }

    public async Task<PetDto> CreateAsync(PetEditDto dto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Создание питомца");
        var (name, breedId, ownerId, birthDate, weight) = Read(dto);
        var created = await pets.AddAsync(name, breedId, ownerId, birthDate, weight, cancellationToken);
        logger.LogInformation("Питомец {PetId} создан", created.Id);
        return ClinicMapper.ToPet(created);
    }

    public async Task<PetDto> UpdateAsync(int id, PetEditDto dto, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Обновление питомца {PetId}", id);
        var (name, breedId, ownerId, birthDate, weight) = Read(dto);
        var updated = await pets.UpdateAsync(id, name, breedId, ownerId, birthDate, weight, cancellationToken);
        logger.LogInformation("Питомец {PetId} обновлён", id);
        return ClinicMapper.ToPet(updated);
    }

    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Удаление питомца {PetId}", id);
        await pets.RemoveAsync(id, cancellationToken);
        logger.LogInformation("Питомец {PetId} удалён", id);
    }

    private (string Name, int BreedId, int OwnerId, DateOnly BirthDate, double Weight) Read(PetEditDto dto)
    {
        DtoValidator.EnsureValid(dto);
        var birthDate = dto.BirthDate ?? throw new DomainRuleException("Укажите дату рождения.");
        var today = DateOnly.FromDateTime(time.GetLocalNow().DateTime);
        if (birthDate > today || birthDate.Year < 1980)
        {
            throw new DomainRuleException("Дата рождения питомца указана некорректно.");
        }

        return (
            DtoValidator.RequireText(dto.Name, "Укажите кличку."),
            DtoValidator.RequireId(dto.BreedId, "Укажите породу."),
            DtoValidator.RequireId(dto.OwnerId, "Укажите владельца."),
            birthDate,
            dto.Weight ?? throw new DomainRuleException("Укажите вес."));
    }
}
