using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.InMemory.Repositories;

/// <summary>
/// Хранение сущностей Service в памяти
/// </summary>
/// <param name="storage">Общее хранилище салона</param>
public sealed class ServiceRepository(SalonStorage storage) : IServiceRepository
{
    /// <inheritdoc />
    public IReadOnlyList<Service> GetAll()
    {
        lock (storage.SyncRoot)
        {
            return storage.Services.Select(EntityCopies.Copy).ToList();
        }
    }

    /// <inheritdoc />
    public Service? GetById(int id)
    {
        lock (storage.SyncRoot)
        {
            var entity = storage.Services.FirstOrDefault(item => item.Id == id);
            return entity is null ? null : EntityCopies.Copy(entity);
        }
    }

    /// <inheritdoc />
    public Service Add(Service entity)
    {
        lock (storage.SyncRoot)
        {
            var saved = EntityCopies.Copy(entity);

            saved.Id = storage.NextId<Service>();
            storage.Services.Add(saved);
            return EntityCopies.Copy(saved);
        }
    }

    /// <inheritdoc />
    public bool Update(Service entity)
    {
        lock (storage.SyncRoot)
        {
            var current = storage.Services.FirstOrDefault(item => item.Id == entity.Id);
            if (current is null)
            {
                return false;
            }

            var saved = EntityCopies.Copy(entity);

            if (current.DurationMinutes != saved.DurationMinutes &&
                storage.Appointments.Any(item => item.Service.Id == current.Id))
            {
                throw new InvalidOperationException(
                    "Нельзя менять длительность услуги с существующими записями");
            }

            current.Name = saved.Name;
            current.Category = saved.Category;
            current.Price = saved.Price;
            current.DurationMinutes = saved.DurationMinutes;
            return true;
        }
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        lock (storage.SyncRoot)
        {
            if (storage.Appointments.Any(item => item.Service.Id == id))
            {
                throw new InvalidOperationException("Объект используется в записях");
            }

            return storage.Services.RemoveAll(item => item.Id == id) > 0;
        }
    }
}