using Vet.Domain.DataSeed;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Infrastructure.InMemory.Repositories;

/// <summary>
/// Класс репозитория для хранения данных в памяти
/// </summary>
/// <param name="data">Данные из датасида</param>
public class VeterinarianRepository(VetClinicTestFixture data) : IRepository<Veterinarian, int>
{
    private readonly VetClinicTestFixture _data = data;
    private readonly Lock _lock = new();

    /// <inheritdoc />
    public Task<Veterinarian> AddAsync(Veterinarian entity)
    {
        lock (_lock)
        {
            var maxId = _data.Veterinarians.Count > 0 ? _data.Veterinarians.Max(p => p.Id) : 0;
            entity.Id = maxId + 1;

            _data.Veterinarians.Add(entity);
            return Task.FromResult(entity);
        }
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int entityId)
    {
        lock (_lock)
        {
            var veterinarian = _data.Veterinarians.FirstOrDefault(p => p.Id == entityId);
            if (veterinarian is null)
            {
                return Task.FromResult(false);
            }

            _data.Veterinarians.Remove(veterinarian);
            return Task.FromResult(true);
        }
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<Veterinarian>> GetAllAsync()
    {
        lock (_lock)
        {
            IReadOnlyList<Veterinarian> veterinarians = _data.Veterinarians.ToList().AsReadOnly();
            return Task.FromResult(veterinarians);
        }
    }

    /// <inheritdoc />
    public Task<Veterinarian?> GetByIdAsync(int entityId)
    {
        lock (_lock)
        {
            var veterinarian = _data.Veterinarians.FirstOrDefault(p => p.Id == entityId);
            return Task.FromResult(veterinarian);
        }
    }

    /// <inheritdoc />
    public Task<bool> UpdateAsync(Veterinarian entity)
    {
        lock (_lock)
        {
            var index = _data.Veterinarians.FindIndex(p => p.Id == entity.Id);
            if (index == -1)
            {
                return Task.FromResult(false);
            }

            _data.Veterinarians[index] = entity;
            return Task.FromResult(true);
        }
    }
}