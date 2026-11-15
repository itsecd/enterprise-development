using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Infrastructure.PostgreSQL.Repositories;

/// <summary>
/// Репозиторий клиентов поверх PostgreSQL.
/// </summary>
internal class ClientRepository(SalonDbContext db) : PostgreSqlRepository<Client>(db), IClientRepository
{
    /// <inheritdoc />
    protected override void ApplyChanges(Client current, Client updated)
    {
        current.FullName = updated.FullName;
        current.Gender = updated.Gender;
        current.PhoneNumber = updated.PhoneNumber;
        current.BirthDate = updated.BirthDate;
    }
}
