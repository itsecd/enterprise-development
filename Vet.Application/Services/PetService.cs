using Microsoft.Extensions.Logging;
using Vet.Application.Interfaces;
using Vet.Contracts.Dtos;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Application.Services;

/// <summary>
/// Сервис для управления питомцами
/// </summary>
/// <param name="petRepository">Репозиторий питомцев</param>
/// <param name="ownerRepository">Репозиторий владельцев</param>
/// <param name="breedRepository">Репозиторий пород</param>
/// <param name="appointmentRepository">Репозиторий приемов</param>
/// <param name="logger">Логгер сервиса</param>
public class PetService(
    IRepository<Pet, int> petRepository,
    IRepository<Owner, int> ownerRepository,
    IRepository<Breed, int> breedRepository,
    IRepository<Appointment, int> appointmentRepository,
    ILogger<PetService> logger)
    : IApplicationService<PetDto, PetCreateUpdateDto, int>
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<PetDto>> GetAllAsync()
    {
        var pets = await petRepository.GetAllAsync();
        return [.. pets.Select(p => p.ToDto())];
    }

    /// <inheritdoc />
    public async Task<PetDto?> GetByIdAsync(int id)
    {
        var pet = await petRepository.GetByIdAsync(id);
        return pet?.ToDto();
    }

    /// <inheritdoc />
    public async Task<PetDto> CreateAsync(PetCreateUpdateDto dto)
    {
        var owner = await ownerRepository.GetByIdAsync(dto.OwnerId);
        if (owner is null)
        {
            logger.LogWarning("Ошибка создания питомца: Владелец с Id {OwnerId} не найден", dto.OwnerId);
            throw new KeyNotFoundException($"Владелец с Id {dto.OwnerId} не найден");
        }

        var breed = await breedRepository.GetByIdAsync(dto.BreedId);
        if (breed is null)
        {
            logger.LogWarning("Ошибка создания питомца: Порода с Id {BreedId} не найдена", dto.BreedId);
            throw new KeyNotFoundException($"Порода с Id {dto.BreedId} не найдена");
        }

        if (breed.Species != dto.Species)
        {
            logger.LogWarning("Несоответствие вида животного: передано {DtoSpecies}, а у породы {BreedSpecies}", dto.Species, breed.Species);
            throw new ArgumentException($"Вид животного '{dto.Species}' не соответствует виду выбранной породы '{breed.Species}'");
        }

        var pet = dto.ToEntity(breed, owner);
        var created = await petRepository.AddAsync(pet);

        owner.Pets.Add(created);
        logger.LogInformation("Создан новый питомец с Id {Id} ({Name})", created.Id, created.Name);

        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, PetCreateUpdateDto dto)
    {
        var existing = await petRepository.GetByIdAsync(id);
        if (existing is null)
        {
            logger.LogWarning("Попытка обновления несуществующего питомца с Id {Id}", id);
            return false;
        }

        var owner = await ownerRepository.GetByIdAsync(dto.OwnerId);
        if (owner is null)
        {
            logger.LogWarning("Ошибка обновления питомца {Id}: Владелец с Id {OwnerId} не найден", id, dto.OwnerId);
            throw new KeyNotFoundException($"Владелец с Id {dto.OwnerId} не найден");
        }

        var breed = await breedRepository.GetByIdAsync(dto.BreedId);
        if (breed is null)
        {
            logger.LogWarning("Ошибка обновления питомца {Id}: Порода с Id {BreedId} не найдена", id, dto.BreedId);
            throw new KeyNotFoundException($"Порода с Id {dto.BreedId} не найдена");
        }

        if (breed.Species != dto.Species)
        {
            logger.LogWarning("Несоответствие вида при обновлении питомца {Id}: {DtoSpecies} != {BreedSpecies}", id, dto.Species, breed.Species);
            throw new ArgumentException($"Вид животного '{dto.Species}' не соответствует виду выбранной породы '{breed.Species}'");
        }

        if (existing.Owner.Id != owner.Id)
        {
            existing.Owner.Pets.Remove(existing);
            owner.Pets.Add(existing);
        }

        existing.Name = dto.Name;
        existing.Species = dto.Species;
        existing.Breed = breed;
        existing.BirthDate = dto.BirthDate;
        existing.BodyWeight = dto.BodyWeight;
        existing.Owner = owner;

        var result = await petRepository.UpdateAsync(existing);
        if (result)
        {
            logger.LogInformation("Питомец с Id {Id} успешно обновлен", id);
        }
        return result;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var pet = await petRepository.GetByIdAsync(id);
        if (pet is null)
        {
            logger.LogWarning("Попытка удаления несуществующего питомца с Id {Id}", id);
            return false;
        }

        pet.Owner.Pets.Remove(pet);

        var allAppointments = await appointmentRepository.GetAllAsync();
        var petAppointments = allAppointments.Where(a => a.Pet.Id == id).ToList();
        foreach (var appt in petAppointments)
        {
            appt.Veterinarian.Appointments.Remove(appt);
            await appointmentRepository.DeleteAsync(appt.Id);
        }

        var result = await petRepository.DeleteAsync(id);
        if (result)
        {
            logger.LogInformation("Питомец с Id {Id} и все связанные приемы успешно удалены", id);
        }
        return result;
    }
}