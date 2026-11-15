using Microsoft.EntityFrameworkCore;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий записей на приём на базе Entity Framework Core
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class AppointmentRepository(VetDbContext context) : IAppointmentRepository
{
    /// <inheritdoc />
    /// <inheritdoc />
    public async Task<IReadOnlyList<Appointment>> GetAllAsync() =>
        await context.Appointments
            .Include(a => a.Pet)
                .ThenInclude(p => p.Breed)
            .Include(a => a.Pet)
                .ThenInclude(p => p.Owner)
            .Include(a => a.Veterinarian)
                .ThenInclude(v => v.Specialization)
                    .ThenInclude(s => s.Species)
            .ToListAsync();

    /// <inheritdoc />
    public async Task<Appointment?> GetByIdAsync(int entityId) =>
        await context.Appointments
            .Include(a => a.Pet)
                .ThenInclude(p => p.Breed)
            .Include(a => a.Pet)
                .ThenInclude(p => p.Owner)
            .Include(a => a.Veterinarian)
                .ThenInclude(v => v.Specialization)
                    .ThenInclude(s => s.Species)
            .FirstOrDefaultAsync(a => a.Id == entityId);

    /// <inheritdoc />
    public async Task<IReadOnlyList<Pet>> GetDistinctPetsByVeterinarianIdAsync(int veterinarianId) =>
        await context.Pets
            .AsNoTracking()
            .Include(p => p.Breed)
            .Include(p => p.Owner)
            .Where(p => p.Appointments.Any(a => a.Veterinarian.Id == veterinarianId))
            .OrderBy(p => p.Name)
            .ToListAsync();

    /// <inheritdoc />
    public async Task<int> GetRepeatCountByBreedIdAsync(int breedId) =>
        await context.Appointments
            .CountAsync(a => a.IsRepeat && a.Pet.Breed.Id == breedId);

    /// <inheritdoc />
    public async Task<IReadOnlyList<Appointment>> GetByRoomAndMonthAsync(int roomNumber, int year, int month)
    {
        var startDate = new DateTime(year, month, 1);
        var endDate = startDate.AddMonths(1);

        return await context.Appointments
            .AsNoTracking()
            .Include(a => a.Pet)
                .ThenInclude(p => p.Breed)
            .Include(a => a.Pet)
                .ThenInclude(p => p.Owner)
            .Include(a => a.Veterinarian)
                .ThenInclude(v => v.Specialization)
                    .ThenInclude(s => s.Species)
            .Where(a => a.RoomNumber == roomNumber
                     && a.AppointmentDateTime >= startDate
                     && a.AppointmentDateTime < endDate)
            .OrderBy(a => a.AppointmentDateTime)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Appointment> AddAsync(Appointment entity)
    {
        await context.Appointments.AddAsync(entity);
        await context.SaveChangesAsync();
        return entity;
    }

    /// <inheritdoc />
    public async Task<bool> UpdateAsync(Appointment entity)
    {
        if (context.Entry(entity).State == EntityState.Detached)
        {
            var existing = await context.Appointments.FindAsync(entity.Id);
            if (existing is null)
            {
                return false;
            }

            context.Entry(existing).CurrentValues.SetValues(entity);
        }

        await context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int entityId)
    {
        var appointment = await context.Appointments.FindAsync(entityId);
        if (appointment is null)
        {
            return false;
        }

        context.Appointments.Remove(appointment);
        await context.SaveChangesAsync();
        return true;
    }
}