using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Infrastructure.PostgreSQL.Repositories;

/// <summary>
/// Репозиторий услуг поверх PostgreSQL.
/// </summary>
internal class ServiceRepository(SalonDbContext db) : PostgreSqlRepository<Service>(db), IServiceRepository
{
    /// <inheritdoc />
    protected override void ApplyChanges(Service current, Service updated)
    {
        current.Name = updated.Name;
        current.Category = updated.Category;
        current.Price = updated.Price;
        current.Duration = updated.Duration;
    }
}
