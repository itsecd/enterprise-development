using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий сущностей BeautySpecialist
/// </summary>
/// <param name="storage">Общее хранилище салона</param>
public sealed class SpecialistRepository(SalonStorage storage)
    : InMemoryRepository<BeautySpecialist>(storage, storage.Specialists, EntityCopies.Copy), ISpecialistRepository
{
    /// <inheritdoc />
    protected override void PrepareForSave(BeautySpecialist saved, BeautySpecialist? current)
    {
        saved.PassportNumber = string.IsNullOrWhiteSpace(saved.PassportNumber)
            ? null
            : saved.PassportNumber.Trim();

        if (saved.PassportNumber is not null &&
            Storage.Specialists.Any(specialist =>
                !ReferenceEquals(specialist, current) &&
                string.Equals(
                    specialist.PassportNumber?.Trim(),
                    saved.PassportNumber,
                    StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException(
                "Специалист с таким паспортом уже существует");
        }
    }

    /// <inheritdoc />
    protected override void ApplyChanges(BeautySpecialist current, BeautySpecialist saved)
    {
        current.LastName = saved.LastName;
        current.FirstName = saved.FirstName;
        current.Patronymic = saved.Patronymic;
        current.Gender = saved.Gender;
        current.DateOfBirth = saved.DateOfBirth;
        current.PhoneNumber = saved.PhoneNumber;
        current.PassportNumber = saved.PassportNumber;
        current.Specialization = saved.Specialization;
        current.WorkExperience = saved.WorkExperience;
    }

    /// <inheritdoc />
    protected override void CheckDeletion(BeautySpecialist entity)
    {
        if (Storage.Appointments.Any(item => item.Specialist.Id == entity.Id))
        {
            throw new InvalidOperationException("Объект используется в записях");
        }
    }

    /// <inheritdoc />
    public IReadOnlyList<BeautySpecialist> GetByMinimumExperience(int years)
    {
        lock (Storage.SyncRoot)
        {
            return Storage.Specialists
                .Where(specialist => specialist.WorkExperience >= years)
                .OrderBy(specialist => specialist.Id)
                .Select(EntityCopies.Copy)
                .ToList();
        }
    }
}
