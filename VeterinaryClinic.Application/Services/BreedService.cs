using VeterinaryClinic.Application.Contracts.Appointments;
using VeterinaryClinic.Application.Contracts.Breeds;
using VeterinaryClinic.Application.Contracts.Owners;
using VeterinaryClinic.Application.Contracts.Pets;
using VeterinaryClinic.Application.Contracts.Specializations;
using VeterinaryClinic.Application.Contracts.Veterinarians;
using VeterinaryClinic.Domain.Models;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Application.Services;

/// <summary>
/// Выполняет операции с ресурсом «порода»
/// </summary>
public sealed class BreedService(IRepository<Breed> breeds, IRepository<Pet> pets, IRepository<Appointment> appointments) : IBreedService
{
    private static void Validate(SaveBreedRequest x)
    {
        if (string.IsNullOrWhiteSpace(x.Name) || !Enum.IsDefined(x.AnimalType))
            throw new ArgumentException("Некорректные данные породы");
    }

    public async Task<IReadOnlyList<BreedDto>> BreedsAsync(CancellationToken cancellationToken = default) => (await breeds.GetAllAsync(cancellationToken)).Select(DtoMapper.Map).ToArray();

    public async Task<BreedDto?> GetAsync(int id, CancellationToken cancellationToken = default) => (await breeds.GetAsync(id, cancellationToken)) is { } b ? DtoMapper.Map(b) : null;

    public async Task<BreedDto> CreateAsync(SaveBreedRequest x, CancellationToken cancellationToken = default)
    {

        Validate(x);
        return DtoMapper.Map((await breeds.CreateAsync(new Breed { Name = x.Name.Trim(), AnimalType = x.AnimalType }, cancellationToken)));
    }


    public async Task<BreedDto> UpdateAsync(int id, SaveBreedRequest x, CancellationToken cancellationToken = default)
    {

        Validate(x);
        var current = (await breeds.GetAsync(id, cancellationToken)) ?? throw new KeyNotFoundException($"Порода {id} не найдена");
        if (current.AnimalType != x.AnimalType && (await pets.AnyAsync(p => p.Breed.Id == id, cancellationToken)))
            throw new InvalidOperationException("Нельзя изменить вид породы, к которой привязаны питомцы");
        return DtoMapper.Map((await breeds.UpdateAsync(id, b => { b.Name = x.Name.Trim(); b.AnimalType = x.AnimalType; }, cancellationToken)));
    }


    public async Task DeleteBreedAsync(int id, CancellationToken cancellationToken = default)
    {

        if ((await pets.AnyAsync(p => p.Breed.Id == id, cancellationToken))) throw new InvalidOperationException("Порода используется");
        await breeds.RemoveAsync(id, cancellationToken);
    }


    public async Task<int> RepeatedAppointmentsAsync(int breedId, CancellationToken cancellationToken = default)
    {

        if ((await breeds.GetAsync(breedId, cancellationToken)) is null) throw new KeyNotFoundException($"Порода {breedId} не найдена");
        return (await appointments.CountAsync(a => a.IsRepeated && a.Pet.Breed.Id == breedId, cancellationToken));
    }


    public async Task<IReadOnlyList<PetDto>> PetsByBreedAsync(int id, CancellationToken cancellationToken = default)
    {

        if ((await breeds.GetAsync(id, cancellationToken)) is null) throw new KeyNotFoundException($"Порода {id} не найдена");
        return (await pets.FindAsync(p => p.Breed.Id == id, cancellationToken)).Select(DtoMapper.Map).ToArray();
    }
}
