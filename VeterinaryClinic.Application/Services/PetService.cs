using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Выполняет операции с ресурсом «питомец»
/// </summary>
public sealed class PetService(IRepository<Pet> pets, IRepository<Breed> breeds, IRepository<Owner> owners, IRepository<Appointment> appointments) : IPetService
{
    /// <summary>
    /// Возвращает всех питомцев
    /// </summary>
    public IReadOnlyList<PetDto> Pets() => pets.GetAll().Select(DtoMapper.Map).ToArray();

    /// <summary>
    /// Находит питомца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public PetDto? Get(int id) => pets.Get(id) is { } p ? DtoMapper.Map(p) : null;

    /// <summary>
    /// Создаёт питомца
    /// </summary>
    /// <param name="x">Данные для сохранения</param>
    public PetDto Create(SavePetRequest x)
    {
        Validate(x);
        var breed = breeds.Get(x.BreedId) ?? throw new ArgumentException("Breed not found");
        var owner = ResolveOwner(x.OwnerId);
        var saved = pets.Create(id =>
        {
            var p = new Pet
            {
                Id = id,
                Name = x.Name,
                Breed = breed,
                BirthDate = x.BirthDate,
                Weight = x.Weight
            }; p.ChangeOwner(owner); return p;
        });
        return DtoMapper.Map(saved);
    }

    /// <summary>
    /// Изменяет питомца с проверкой существующих приёмов
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    /// <param name="x">Данные для сохранения</param>
    public PetDto Update(int id, SavePetRequest x)
    {
        Validate(x);
        var breed = breeds.Get(x.BreedId) ?? throw new ArgumentException("Breed not found");
        var owner = ResolveOwner(x.OwnerId);
        if (appointments.Any(a => a.Pet.Id == id && a.Veterinarian.Specialization.AnimalType != breed.AnimalType))
            throw new InvalidOperationException("Breed is incompatible with existing appointments");
        return DtoMapper.Map(pets.Update(id, p =>
        {
            p.Name = x.Name; p.Breed = breed;
            p.Weight = x.Weight; p.BirthDate = x.BirthDate; p.ChangeOwner(owner);
        }));
    }

    /// <summary>
    /// Удаляет питомца, у которого нет приёмов
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public void DeletePet(int id)
    {
        var pet = pets.Get(id);
        if (pet is null) return;
        if (appointments.Any(a => a.Pet.Id == id)) throw new InvalidOperationException("Pet has appointments");
        pet.ChangeOwner(null);
        pets.Remove(id);
    }

    /// <summary>
    /// Возвращает приёмы питомца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public IReadOnlyList<AppointmentDto> AppointmentsByPet(int id)
    {
        if (pets.Get(id) is null) throw new KeyNotFoundException($"Pet {id} not found");
        return appointments.Find(a => a.Pet.Id == id).OrderBy(a => a.AppointmentDate)
            .Select(DtoMapper.Map).ToArray();
    }

    /// <summary>
    /// Возвращает владельца питомца
    /// </summary>
    /// <param name="id">Идентификатор записи</param>
    public OwnerDto? OwnerByPet(int id)
    {
        var pet = pets.Get(id) ?? throw new KeyNotFoundException($"Pet {id} not found");
        return pet.Owner is { } o ? DtoMapper.Map(o) : null;
    }
    private Owner? ResolveOwner(int? id) => id is int ownerId
        ? owners.Get(ownerId) ?? throw new ArgumentException("Owner not found") : null;
    private static void Validate(SavePetRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.Name) || x.Weight <= 0 || !double.IsFinite(x.Weight) ||
            x.BirthDate > DateOnly.FromDateTime(DateTime.Today))
            throw new ArgumentException("Invalid pet");
    }
}
