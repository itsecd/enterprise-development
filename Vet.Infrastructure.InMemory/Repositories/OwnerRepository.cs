using Vet.Domain.DataSeed;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Infrastructure.InMemory.Repositories;

/// <summary>
/// Класс репозитория для хранения данных в памяти
/// </summary>
/// <param name="data">Данные из датасида</param>
public class OwnerRepository(VetClinicTestFixture data) : IRepository<Owner, int>
{
    private readonly VetClinicTestFixture _data = data;
    private readonly Lock _lock = new();

    /// <inheritdoc />
    public Task<Owner> AddAsync(Owner entity)
    {
        lock (_lock)
        {
            var maxId = _data.Owners.Count > 0 ? _data.Owners.Max(p => p.Id) : 0;
            entity.Id = maxId + 1;

            _data.Owners.Add(entity);
            return Task.FromResult(entity);
        }
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int entityId)
    {
        lock (_lock)
        {
            var owner = _data.Owners.FirstOrDefault(p => p.Id == entityId);
            if (owner is null)
            {
                return Task.FromResult(false);
            }

            _data.Owners.Remove(owner);
            return Task.FromResult(true);
        }
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<Owner>> GetAllAsync()
    {
        lock (_lock)
        {
            IReadOnlyList<Owner> owners = _data.Owners.ToList().AsReadOnly();
            return Task.FromResult(owners);
        }
    }

    /// <inheritdoc />
    public Task<Owner?> GetByIdAsync(int entityId)
    {
        lock (_lock)
        {
            var owner = _data.Owners.FirstOrDefault(p => p.Id == entityId);
            return Task.FromResult(owner);
        }
    }

    /// <inheritdoc />
    public Task<bool> UpdateAsync(Owner entity)
    {
        lock (_lock)
        {
            var index = _data.Owners.FindIndex(p => p.Id == entity.Id);
            if (index == -1)
            {
                return Task.FromResult(false);
            }

            _data.Owners[index] = entity;
            return Task.FromResult(true);
        }
    }
}