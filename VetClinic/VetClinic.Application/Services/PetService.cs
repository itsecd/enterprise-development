using VetClinic.Application.Exceptions;
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
/// <param name="transaction">Общая транзакция проверки породы и сохранения питомца</param>
public class PetService(
    IPetRepository pets,
    IOwnerRepository owners,
    IBreedRepository breeds,
    IClinicTransaction transaction) : IPetService
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
    public Task<PetDto> CreateAsync(PetEditDto dto, CancellationToken cancellationToken = default) =>
        transaction.ExecuteSerializableAsync(async token =>
        {
            var entity = dto.ToEntity();
            entity.Owner = await GetOwnerAsync(dto.OwnerId, token);
            entity.Breed = await GetBreedAsync(dto.BreedId, dto.Species, token);
            var created = await pets.CreateAsync(entity, token);
            return created.ToDto();
        }, cancellationToken);

    /// <inheritdoc/>
    public Task<PetDto?> UpdateAsync(int id, PetEditDto dto, CancellationToken cancellationToken = default) =>
        transaction.ExecuteSerializableAsync<PetDto?>(async token =>
        {
            var entity = await pets.GetByIdAsync(id, token);
            if (entity is null)
            {
                return null;
            }

            var owner = await GetOwnerAsync(dto.OwnerId, token);
            var breed = await GetBreedAsync(dto.BreedId, dto.Species, token);
            entity.Owner = owner;
            entity.Breed = breed;
            entity.Apply(dto);
            await pets.SaveChangesAsync(token);
            return entity.ToDto();
        }, cancellationToken);

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
    /// <exception cref="InvalidPetBreedException">Порода относится к другому виду</exception>
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
            throw new InvalidPetBreedException($"Порода «{breed.Name}» относится к другому виду питомца");
        }

        return breed;
    }
}
