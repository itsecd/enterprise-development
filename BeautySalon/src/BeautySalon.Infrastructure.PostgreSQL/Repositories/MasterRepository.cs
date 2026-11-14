using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Interfaces;

namespace BeautySalon.Infrastructure.PostgreSQL.Repositories;

/// <summary>
/// Репозиторий мастеров поверх PostgreSQL.
/// </summary>
internal class MasterRepository(SalonDbContext db) : PostgreSqlRepository<Master>(db), IMasterRepository
{
    /// <inheritdoc />
    protected override void ApplyChanges(Master current, Master updated)
    {
        current.FullName = updated.FullName;
        current.Gender = updated.Gender;
        current.PassportNumber = updated.PassportNumber;
        current.Specialization = updated.Specialization;
        current.WorkExperience = updated.WorkExperience;
    }
}
