using Vet.Domain.DataSeed;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Infrastructure.InMemory.Repositories;

/// <summary>
/// Класс репозитория для хранения данных в памяти
/// </summary>
/// <param name="data">Данные из датасида</param>
public class PetRepository(VetClinicTestFixture data) : IRepository<Pet, int>
{
    private readonly VetClinicTestFixture _data = data;
    private readonly Lock _lock = new();

    /// <inheritdoc />
    public Task<Pet> AddAsync(Pet entity)
    {
        lock (_lock)
        {
            var maxId = _data.Pets.Count > 0 ? _data.Pets.Max(p => p.Id) : 0;
            entity.Id = maxId + 1;

            _data.Pets.Add(entity);
            return Task.FromResult(entity);
        }
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int entityId)
    {
        lock (_lock)
        {
            var pet = _data.Pets.FirstOrDefault(p => p.Id == entityId);
            if (pet is null)
            {
                return Task.FromResult(false);
            }

            _data.Pets.Remove(pet);
            return Task.FromResult(true);
        }
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<Pet>> GetAllAsync()
    {
        lock (_lock)
        {
            IReadOnlyList<Pet> pets = _data.Pets.ToList().AsReadOnly();
            return Task.FromResult(pets);
        }
    }

    /// <inheritdoc />
    public Task<Pet?> GetByIdAsync(int entityId)
    {
        lock (_lock)
        {
            var pet = _data.Pets.FirstOrDefault(p => p.Id == entityId);
            return Task.FromResult(pet);
        }
    }

    /// <inheritdoc />
    public Task<bool> UpdateAsync(Pet entity)
    {
        lock (_lock)
        {
            var index = _data.Pets.FindIndex(p => p.Id == entity.Id);
            if (index == -1)
            {
                return Task.FromResult(false);
            }

            _data.Pets[index] = entity;
            return Task.FromResult(true);
        }
    }
}