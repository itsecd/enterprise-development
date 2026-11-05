using VetClinic.Domain.Entities;
using VetClinic.Domain.Enums;
using VetClinic.Domain.Exceptions;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Infrastructure.InMemory.Repositories;

public sealed class BreedRepository(InMemoryClinicStore store) : IBreedRepository
{
    public Task<IReadOnlyList<Breed>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            return Task.FromResult<IReadOnlyList<Breed>>([.. store.Breeds]);
        }
    }

    public Task<Breed?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            return Task.FromResult(store.Breeds.FirstOrDefault(breed => breed.Id == id));
        }
    }

    public Task<IReadOnlyList<Breed>> GetBySpeciesAsync(AnimalSpecies species, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            return Task.FromResult<IReadOnlyList<Breed>>([.. store.Breeds.Where(breed => breed.Species == species)]);
        }
    }

    public Task<Breed> AddAsync(string name, AnimalSpecies species, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            var breed = new Breed
            {
                Id = store.TakeBreedId(),
                Name = name,
                Species = species
            };
            store.Breeds.Add(breed);
            return Task.FromResult(breed);
        }
    }

    public Task<Breed> UpdateAsync(int id, string name, AnimalSpecies species, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            var breed = store.Breeds.FirstOrDefault(item => item.Id == id)
                ?? throw new EntityNotFoundException("порода", id);

            if (breed.Species != species
                && store.Appointments.Any(appointment =>
                    appointment.Pet.Breed.Id == id
                    && appointment.Veterinarian.Specialization.Species != species))
            {
                throw new DomainRuleException(
                    "Нельзя сменить вид породы: у питомцев этой породы есть приёмы у врача другой специализации.");
            }

            breed.Name = name;
            breed.Species = species;
            return Task.FromResult(breed);
        }
    }

    public Task RemoveAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            var breed = store.Breeds.FirstOrDefault(item => item.Id == id)
                ?? throw new EntityNotFoundException("порода", id);

            if (store.Pets.Any(pet => pet.Breed.Id == id))
            {
                throw new EntityInUseException($"Порода «{breed.Name}» указана у питомцев, удаление невозможно.");
            }

            store.Breeds.Remove(breed);
        }

        return Task.CompletedTask;
    }
}
