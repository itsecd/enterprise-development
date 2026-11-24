using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;
using BeautySalon.Infrastructure.EfCore.Data;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.EfCore.Repositories;

public class EfAppointmentRepository(BeautySalonDbContext context) : IAppointmentRepository
{
    /// <inheritdoc />
    public async Task<IEnumerable<Appointment>> GetAllAsync() => await context.Appointments
        .Include(a => a.Client)
        .Include(a => a.Master)
        .Include(a => a.Service)
        .ToListAsync();

    /// <inheritdoc />
    public async Task<Appointment?> GetByIdAsync(int id) => await context.Appointments
        .Include(a => a.Client)
        .Include(a => a.Master)
        .Include(a => a.Service)
        .FirstOrDefaultAsync(a => a.Id == id);

    /// <inheritdoc />
    public async Task AddAsync(Appointment entity) 
    { 
        context.Appointments.Add(entity); 
        await context.SaveChangesAsync(); 
    }

    /// <inheritdoc />
    public async Task UpdateAsync(Appointment entity) 
    { 
        context.Appointments.Update(entity); 
        await context.SaveChangesAsync(); 
    }

    public async Task DeleteAsync(Appointment entity) 
    { 
        context.Appointments.Remove(entity); 
        await context.SaveChangesAsync(); 
    }
}