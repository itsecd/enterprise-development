using BeautySalon.Domain;
using BeautySalon.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.EfCore.Repositories;

/// <summary>
/// Репозиторий для записей с Entity Framework Core
/// </summary>
/// <param name="context">Контекст БД</param>
public class AppointmentEfCoreRepository(BeautySalonDbContext context) : IRepository<Appointment, int>
{
    /// <inheritdoc/>
    public async Task<Appointment> Create(Appointment entity)
    {
        var result = await context.Appointments.AddAsync(entity);
        await context.SaveChangesAsync();
        return result.Entity;
    }

    /// <inheritdoc/>
    public async Task<bool> Delete(int entityId)
    {
        var result = await context.Appointments.FirstOrDefaultAsync(a => a.Id == entityId);
        if (result == null)
            return false;

        context.Appointments.Remove(result);
        await context.SaveChangesAsync();
        return true;
    }

    /// <inheritdoc/>
    public async Task<Appointment?> Read(int entityId) => await context.Appointments.FirstOrDefaultAsync(a => a.Id == entityId);

    /// <inheritdoc/>
    public async Task<List<Appointment>> ReadAll() => await context.Appointments.ToListAsync();

    /// <inheritdoc/>
    public async Task<Appointment> Update(Appointment entity)
    {
        context.Appointments.Update(entity);
        await context.SaveChangesAsync();
        return (await Read(entity.Id))!;
    }
}
