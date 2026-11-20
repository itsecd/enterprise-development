using Microsoft.EntityFrameworkCore;
using VetClinic.Domain.Models;
using VetClinic.Domain.Repositories;

namespace VetClinic.Infrastructure.SqlServer.Repositories;

/// <summary>
/// Репозиторий записей на прием
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class AppointmentRepository(VetClinicDbContext context) : EfRepository<Appointment>(context), IAppointmentRepository
{
    /// <inheritdoc/>
    public async Task<int> CountRepeatByBreedAsync(int breedId, CancellationToken cancellationToken = default) =>
        await Entities.CountAsync(a => a.IsRepeat && a.Pet!.BreedId == breedId, cancellationToken);

    /// <inheritdoc/>
    public async Task<IReadOnlyList<Appointment>> GetByRoomAndMonthAsync(string roomNumber, int year, int month, CancellationToken cancellationToken = default) =>
        await Entities
            .AsNoTracking()
            .Where(a => a.RoomNumber == roomNumber && a.DateTime.Year == year && a.DateTime.Month == month)
            .OrderBy(a => a.Id)
            .ToListAsync(cancellationToken);
}
