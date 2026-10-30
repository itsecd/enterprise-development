using FitnessGym.Domain.Data;
using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Interfaces;

namespace FitnessGym.Infrastructure.InMemory.Repositories;

/// <summary>
/// Реализация репозитория специализаций в памяти
/// </summary>
public class SpecializationRepository : ISpecializationRepository
{
    private readonly List<Specialization> _specializations = new(DataSeeder.Specializations);

    /// <summary>
    /// Получить список всех специализаций
    /// </summary>
    public Task<IReadOnlyList<Specialization>> GetAllAsync()
    {
        return Task.FromResult<IReadOnlyList<Specialization>>(_specializations);
    }

    /// <summary>
    /// Получить специализацию по идентификатору
    /// </summary>
    public Task<Specialization?> GetByIdAsync(int id)
    {
        return Task.FromResult(_specializations.FirstOrDefault(s => s.Id == id));
    }

    /// <summary>
    /// Добавить новую специализацию
    /// </summary>
    public Task<Specialization> AddAsync(Specialization specialization)
    {
        var newId = _specializations.Count > 0 ? _specializations.Max(s => s.Id) + 1 : 0;
        specialization.Id = newId;
        _specializations.Add(specialization);
        return Task.FromResult(specialization);
    }

    /// <summary>
    /// Удалить специализацию
    /// </summary>
    public Task<bool> DeleteAsync(int id)
    {
        var spec = _specializations.FirstOrDefault(s => s.Id == id);
        if (spec is null) return Task.FromResult(false);

        _specializations.Remove(spec);
        return Task.FromResult(true);
    }
}