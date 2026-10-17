using FitnesClub.Domain.Data;
using FitnesClub.Domain.Entities;

namespace FitnesClub.Api.Storage;

/// <summary>
/// Репозиторий для хранения и управления специализациями тренеров в оперативной памяти.
/// </summary>
public class InMemorySpecializationRepo : IRepository<Specialization, Guid>
{
    private readonly List<Specialization> _specializations;

    /// <summary>
    /// Инициализирует репозиторий специализаций начальными данными из <see cref="DataSeeder"/>.
    /// </summary>
    public InMemorySpecializationRepo()
    {
        _specializations = [.. DataSeeder.Specializations];
    }

    /// <inheritdoc />
    public void Create(Specialization specialization)
    {
        if (specialization.Id == Guid.Empty)
        {
            specialization.Id = Guid.NewGuid();
        }

        _specializations.Add(specialization);
    }

    /// <inheritdoc />
    public Specialization? Read(Guid id)
    {
        return _specializations.FirstOrDefault(s => s.Id == id);
    }

    /// <inheritdoc />
    public List<Specialization> ReadAll()
    {
        return [.. _specializations];
    }

    /// <inheritdoc />
    public void Update(Specialization updatedSpecialization)
    {
        var index = _specializations.FindIndex(s => s.Id == updatedSpecialization.Id);
        if (index != -1)
        {
            _specializations[index] = updatedSpecialization;
        }
    }

    /// <inheritdoc />
    public void Delete(Guid id)
    {
        var item = Read(id);
        if (item is not null)
        {
            _specializations.Remove(item);
        }
    }
}