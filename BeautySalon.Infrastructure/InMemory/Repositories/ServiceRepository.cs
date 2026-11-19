using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.InMemory.Repositories;

/// <summary>
/// Репозиторий сущностей Service
/// </summary>
/// <param name="storage">Общее хранилище салона</param>
public sealed class ServiceRepository(SalonStorage storage)
    : InMemoryRepository<Service>(storage, storage.Services, EntityCopies.Copy), IServiceRepository
{
    /// <inheritdoc />
    protected override void PrepareForSave(Service saved, Service? current)
    {
        if (current is not null &&
            current.DurationMinutes != saved.DurationMinutes &&
            Storage.Appointments.Any(item => item.Service.Id == current.Id))
        {
            throw new InvalidOperationException(
                "Нельзя менять длительность услуги с существующими записями");
        }
    }

    /// <inheritdoc />
    protected override void ApplyChanges(Service current, Service saved)
    {
        current.Name = saved.Name;
        current.Category = saved.Category;
        current.Price = saved.Price;
        current.DurationMinutes = saved.DurationMinutes;
    }

    /// <inheritdoc />
    protected override void CheckDeletion(Service entity)
    {
        if (Storage.Appointments.Any(item => item.Service.Id == entity.Id))
        {
            throw new InvalidOperationException("Объект используется в записях");
        }
    }
}
