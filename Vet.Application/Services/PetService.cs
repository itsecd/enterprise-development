using Vet.Application.Interfaces;
using Vet.Contracts.Dtos;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Application.Services;

/// <summary>
/// Сервис для управления питомцами
/// </summary>
/// <param name="petRepository">репозиторий питомцев</param>
/// <param name="ownerRepository">Репозиторий владельцев</param>
/// <param name="breedRepository">Репозиторий пород</param>
/// <param name="appointmentRepository">Репозиторий приемов</param>
public class PetService(
    IRepository<Pet, int> petRepository,
    IRepository<Owner, int> ownerRepository,
    IRepository<Breed, int> breedRepository,
    IRepository<Appointment, int> appointmentRepository)
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
        var owner = await ownerRepository.GetByIdAsync(dto.OwnerId)
            ?? throw new KeyNotFoundException($"Владелец с Id {dto.OwnerId} не найден");

        var breed = await breedRepository.GetByIdAsync(dto.BreedId)
            ?? throw new KeyNotFoundException($"Порода с Id {dto.BreedId} не найдена");

        if (breed.Species != dto.Species)
        {
            throw new ArgumentException($"Вид животного '{dto.Species}' не соответствует виду выбранной породы '{breed.Species}'");
        }

        var pet = dto.ToEntity(breed, owner);
        var created = await petRepository.AddAsync(pet);

        owner.Pets.Add(created);

        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(int id, PetCreateUpdateDto dto)
    {
        var existing = await petRepository.GetByIdAsync(id);
        if (existing is null)
        {
            return false;
        }

        var owner = await ownerRepository.GetByIdAsync(dto.OwnerId)
            ?? throw new KeyNotFoundException($"Владелец с Id {dto.OwnerId} не найден");

        var breed = await breedRepository.GetByIdAsync(dto.BreedId)
            ?? throw new KeyNotFoundException($"Порода с Id {dto.BreedId} не найдена");

        if (breed.Species != dto.Species)
        {
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

        return await petRepository.UpdateAsync(existing);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        var pet = await petRepository.GetByIdAsync(id);
        if (pet is null)
        {
            return false;
        }

        pet.Owner.Pets.Remove(pet);

        var allAppointments = await appointmentRepository.GetAllAsync();
        var petAppointments = allAppointments.Where(a => a.Pet.Id == id).ToList();
        foreach (var appt in petAppointments)
        {
            await appointmentRepository.DeleteAsync(appt.Id);
        }

        return await petRepository.DeleteAsync(id);
    }
}