using Vet.Domain.DataSeed;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Infrastructure.InMemory.Repositories;

/// <summary>
/// Класс репозитория для хранения данных в памяти
/// </summary>
/// <param name="data">Данные из датасида</param>
public class BreedRepository(VetClinicTestFixture data) : IRepository<Breed, int>
{
    private readonly VetClinicTestFixture _data = data;
    private readonly Lock _lock = new();

    /// <inheritdoc />
    public Task<Breed> AddAsync(Breed entity)
    {
        lock (_lock)
        {
            var maxId = _data.Breeds.Count > 0 ? _data.Breeds.Max(p => p.Id) : 0;
            entity.Id = maxId + 1;

            _data.Breeds.Add(entity);
            return Task.FromResult(entity);
        }
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int entityId)
    {
        lock (_lock)
        {
            var breed = _data.Breeds.FirstOrDefault(p => p.Id == entityId);
            if (breed is null)
            {
                return Task.FromResult(false);
            }

            _data.Breeds.Remove(breed);
            return Task.FromResult(true);
        }
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<Breed>> GetAllAsync()
    {
        lock (_lock)
        {
            IReadOnlyList<Breed> breeds = _data.Breeds.ToList().AsReadOnly();
            return Task.FromResult(breeds);
        }
    }

    /// <inheritdoc />
    public Task<Breed?> GetByIdAsync(int entityId)
    {
        lock (_lock)
        {
            var breed = _data.Breeds.FirstOrDefault(p => p.Id == entityId);
            return Task.FromResult(breed);
        }
    }

    /// <inheritdoc />
    public Task<bool> UpdateAsync(Breed entity)
    {
        lock (_lock)
        {
            var index = _data.Breeds.FindIndex(p => p.Id == entity.Id);
            if (index == -1)
            {
                return Task.FromResult(false);
            }

            _data.Breeds[index] = entity;
            return Task.FromResult(true);
        }
    }
}