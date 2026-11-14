using Vet.Domain.DataSeed;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Infrastructure.InMemory.Repositories;

/// <summary>
/// Класс репозитория для хранения данных в памяти
/// </summary>
/// <param name="data">Данные из датасида</param>
public class SpecializationRepository(VetClinicTestFixture data) : IRepository<Specialization, int>
{
    private readonly VetClinicTestFixture _data = data;
    private readonly Lock _lock = new();

    /// <inheritdoc />
    public Task<Specialization> AddAsync(Specialization entity)
    {
        lock (_lock)
        {
            var maxId = _data.Specializations.Count > 0 ? _data.Specializations.Max(p => p.Id) : 0;
            entity.Id = maxId + 1;

            _data.Specializations.Add(entity);
            return Task.FromResult(entity);
        }
    }

    /// <inheritdoc />
    public Task<bool> DeleteAsync(int entityId)
    {
        lock (_lock)
        {
            var specialization = _data.Specializations.FirstOrDefault(p => p.Id == entityId);
            if (specialization is null)
            {
                return Task.FromResult(false);
            }

            _data.Specializations.Remove(specialization);
            return Task.FromResult(true);
        }
    }

    /// <inheritdoc />
    public Task<IReadOnlyList<Specialization>> GetAllAsync()
    {
        lock (_lock)
        {
            IReadOnlyList<Specialization> specializations = _data.Specializations.ToList().AsReadOnly();
            return Task.FromResult(specializations);
        }
    }

    /// <inheritdoc />
    public Task<Specialization?> GetByIdAsync(int entityId)
    {
        lock (_lock)
        {
            var specialization = _data.Specializations.FirstOrDefault(p => p.Id == entityId);
            return Task.FromResult(specialization);
        }
    }

    /// <inheritdoc />
    public Task<bool> UpdateAsync(Specialization entity)
    {
        lock (_lock)
        {
            var index = _data.Specializations.FindIndex(p => p.Id == entity.Id);
            if (index == -1)
            {
                return Task.FromResult(false);
            }

            _data.Specializations[index] = entity;
            return Task.FromResult(true);
        }
    }
}