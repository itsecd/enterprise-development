using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Infrastructure.PostgreSQL.Repositories;

/// <summary>
/// Репозиторий записей на приём поверх PostgreSQL.
/// </summary>
internal class AppointmentRepository(SalonDbContext db)
    : PostgreSqlRepository<Appointment>(db), IAppointmentRepository
{
    /// <inheritdoc />
    protected override void ApplyChanges(Appointment current, Appointment updated)
    {
        current.Client = updated.Client;
        current.Master = updated.Master;
        current.Service = updated.Service;
        current.DateTime = updated.DateTime;
        current.IsRegularClient = updated.IsRegularClient;
    }
}
