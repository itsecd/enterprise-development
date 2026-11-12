using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.InMemory.Repositories;

/// <summary>
/// Хранение сущностей BeautySpecialist в памяти
/// </summary>
/// <param name="storage">Общее хранилище салона</param>
public sealed class SpecialistRepository(SalonStorage storage) : ISpecialistRepository
{
    /// <inheritdoc />
    public IReadOnlyList<BeautySpecialist> GetAll()
    {
        lock (storage.SyncRoot)
        {
            return storage.Specialists.Select(EntityCopies.Copy).ToList();
        }
    }

    /// <inheritdoc />
    public BeautySpecialist? GetById(int id)
    {
        lock (storage.SyncRoot)
        {
            var entity = storage.Specialists.FirstOrDefault(item => item.Id == id);
            return entity is null ? null : EntityCopies.Copy(entity);
        }
    }

    /// <inheritdoc />
    public BeautySpecialist Add(BeautySpecialist entity)
    {
        lock (storage.SyncRoot)
        {
            var saved = EntityCopies.Copy(entity);
            saved.PassportNumber = string.IsNullOrWhiteSpace(saved.PassportNumber)
                ? null
                : saved.PassportNumber.Trim();

            if (saved.PassportNumber is not null &&
                storage.Specialists.Any(specialist =>
                    !ReferenceEquals(specialist, null) &&
                    string.Equals(
                        specialist.PassportNumber?.Trim(),
                        saved.PassportNumber,
                        StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException(
                    "Специалист с таким паспортом уже существует");
            }

            saved.Id = storage.NextId<BeautySpecialist>();
            storage.Specialists.Add(saved);
            return EntityCopies.Copy(saved);
        }
    }

    /// <inheritdoc />
    public bool Update(BeautySpecialist entity)
    {
        lock (storage.SyncRoot)
        {
            var current = storage.Specialists.FirstOrDefault(item => item.Id == entity.Id);
            if (current is null)
            {
                return false;
            }

            var saved = EntityCopies.Copy(entity);
            saved.PassportNumber = string.IsNullOrWhiteSpace(saved.PassportNumber)
                ? null
                : saved.PassportNumber.Trim();

            if (saved.PassportNumber is not null &&
                storage.Specialists.Any(specialist =>
                    !ReferenceEquals(specialist, current) &&
                    string.Equals(
                        specialist.PassportNumber?.Trim(),
                        saved.PassportNumber,
                        StringComparison.OrdinalIgnoreCase)))
            {
                throw new InvalidOperationException(
                    "Специалист с таким паспортом уже существует");
            }

            current.LastName = saved.LastName;
            current.FirstName = saved.FirstName;
            current.Patronymic = saved.Patronymic;
            current.Gender = saved.Gender;
            current.DateOfBirth = saved.DateOfBirth;
            current.PhoneNumber = saved.PhoneNumber;
            current.PassportNumber = saved.PassportNumber;
            current.Specialization = saved.Specialization;
            current.WorkExperience = saved.WorkExperience;
            return true;
        }
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        lock (storage.SyncRoot)
        {
            if (storage.Appointments.Any(item => item.Specialist.Id == id))
            {
                throw new InvalidOperationException("Объект используется в записях");
            }

            return storage.Specialists.RemoveAll(item => item.Id == id) > 0;
        }
    }
}