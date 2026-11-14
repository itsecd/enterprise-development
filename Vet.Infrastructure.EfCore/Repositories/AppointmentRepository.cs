using Microsoft.EntityFrameworkCore;
using Vet.Domain.Entities;
using Vet.Domain.Interfaces;

namespace Vet.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий записей на приём на базе Entity Framework Core
/// </summary>
/// <param name="context">Контекст базы данных</param>
public class AppointmentRepository(VetDbContext context) : IRepository<Appointment, int>
{
    /// <inheritdoc />
    public async Task<IReadOnlyList<Appointment>> GetAllAsync()
    {
        return await context.Appointments
            .Include(a => a.Pet)
                .ThenInclude(p => p.Breed)
            .Include(a => a.Pet)
                .ThenInclude(p => p.Owner)
            .Include(a => a.Veterinarian)
                .ThenInclude(v => v.Specialization)
            .ToListAsync();
    }

    /// <inheritdoc />
    public async Task<Appointment?> GetByIdAsync(int entityId)
    {
        return await context.Appointments
            .Include(a => a.Pet)
                .ThenInclude(p => p.Breed)
            .Include(a => a.Pet)
                .ThenInclude(p => p.Owner)
            .Include(a => a.Veterinarian)
                .ThenInclude(v => v.Specialization)
            .FirstOrDefaultAsync(a => a.Id == entityId);
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