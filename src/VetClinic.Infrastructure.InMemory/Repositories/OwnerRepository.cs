using VetClinic.Domain.Entities;
using VetClinic.Domain.Exceptions;
using VetClinic.Domain.Interfaces;

namespace VetClinic.Infrastructure.InMemory.Repositories;

public sealed class OwnerRepository(InMemoryClinicStore store) : IOwnerRepository
{
    public Task<IReadOnlyList<Owner>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            return Task.FromResult<IReadOnlyList<Owner>>([.. store.Owners]);
        }
    }

    public Task<Owner?> FindByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            return Task.FromResult(store.Owners.FirstOrDefault(owner => owner.Id == id));
        }
    }

    public Task<IReadOnlyList<Owner>> GetWithSeveralPetsAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            return Task.FromResult<IReadOnlyList<Owner>>([.. store.Owners
                .Where(owner => owner.Pets.Count > 1)
                .OrderBy(owner => owner.FullName)]);
        }
    }

    public Task<Owner> AddAsync(string fullName, string phoneNumber, string? address, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            var owner = new Owner
            {
                Id = store.TakeOwnerId(),
                FullName = fullName,
                PhoneNumber = phoneNumber,
                Address = address
            };
            store.Owners.Add(owner);
            return Task.FromResult(owner);
        }
    }

    public Task<Owner> UpdateAsync(int id, string fullName, string phoneNumber, string? address, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            var owner = store.Owners.FirstOrDefault(item => item.Id == id)
                ?? throw new EntityNotFoundException("владелец", id);
            owner.FullName = fullName;
            owner.PhoneNumber = phoneNumber;
            owner.Address = address;
            return Task.FromResult(owner);
        }
    }

    public Task RemoveAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        lock (store.Sync)
        {
            var owner = store.Owners.FirstOrDefault(item => item.Id == id)
                ?? throw new EntityNotFoundException("владелец", id);

            if (owner.Pets.Count > 0)
            {
                throw new EntityInUseException($"У владельца «{owner.FullName}» есть питомцы, удаление невозможно.");
            }

            store.Owners.Remove(owner);
        }

        return Task.CompletedTask;
    }
}
