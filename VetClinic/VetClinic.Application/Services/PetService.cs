using VetClinic.Application.Mapping;
using VetClinic.Contracts.Dto.Pets;
using VetClinic.Contracts.Services;
using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;
using VetClinic.Shared;

namespace VetClinic.Application.Services;

/// <summary>
/// Сервис питомцев, записи на прием удаляются вместе с питомцем каскадом в базе
/// </summary>
/// <param name="pets">Репозиторий питомцев</param>
/// <param name="owners">Репозиторий владельцев</param>
/// <param name="breeds">Репозиторий пород</param>
public class PetService(
    IPetRepository pets,
    IOwnerRepository owners,
    IBreedRepository breeds) : IPetService
{
    /// <inheritdoc/>
    public async Task<IReadOnlyList<PetDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var all = await pets.GetAllAsync(cancellationToken);
        return [.. all.Select(p => p.ToDto())];
    }

    /// <inheritdoc/>
    public async Task<PetDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await pets.GetByIdAsync(id, cancellationToken);
        return entity?.ToDto();
    }

    /// <inheritdoc/>
    public async Task<PetDto> CreateAsync(PetEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = dto.ToEntity();
        entity.Owner = await GetOwnerAsync(dto.OwnerId, cancellationToken);
        entity.Breed = await GetBreedAsync(dto.BreedId, dto.Species, cancellationToken);
        var created = await pets.CreateAsync(entity, cancellationToken);
        return created.ToDto();
    }

    /// <inheritdoc/>
    public async Task<PetDto?> UpdateAsync(int id, PetEditDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await pets.GetByIdAsync(id, cancellationToken);
        if (entity is null)
        {
            return null;
        }

        entity.Owner = await GetOwnerAsync(dto.OwnerId, cancellationToken);
        entity.Breed = await GetBreedAsync(dto.BreedId, dto.Species, cancellationToken);
        entity.Apply(dto);
        await pets.UpdateAsync(entity, cancellationToken);
        return entity.ToDto();
    }

    /// <inheritdoc/>
    public async Task DeleteAsync(int id, CancellationToken cancellationToken = default) =>
        await pets.DeleteAsync(id, cancellationToken);

    /// <summary>
    /// Найти владельца или сообщить, что его нет
    /// </summary>
    /// <param name="ownerId">Идентификатор владельца</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <exception cref="KeyNotFoundException">Владелец не найден</exception>
    private async Task<Owner> GetOwnerAsync(int ownerId, CancellationToken cancellationToken) =>
        await owners.GetByIdAsync(ownerId, cancellationToken)
        ?? throw new KeyNotFoundException($"Владелец с идентификатором {ownerId} не найден");

    /// <summary>
    /// Найти породу, если она указана, и проверить, что она относится к виду питомца
    /// </summary>
    /// <param name="breedId">Идентификатор породы или null</param>
    /// <param name="species">Вид питомца</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <exception cref="KeyNotFoundException">Порода указана, но не найдена</exception>
    /// <exception cref="ArgumentException">Порода относится к другому виду</exception>
    private async Task<Breed?> GetBreedAsync(int? breedId, AnimalSpecies species, CancellationToken cancellationToken)
    {
        if (breedId is null)
        {
            return null;
        }

        var breed = await breeds.GetByIdAsync(breedId.Value, cancellationToken)
            ?? throw new KeyNotFoundException($"Порода с идентификатором {breedId} не найдена");

        if (breed.Species != species)
        {
            throw new ArgumentException($"Порода «{breed.Name}» относится к другому виду питомца");
        }

        return breed;
    }
}
