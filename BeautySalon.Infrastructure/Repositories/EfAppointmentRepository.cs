using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;
using BeautySalon.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BeautySalon.Infrastructure.Repositories;

public class EfAppointmentRepository(BeautySalonDbContext context) : IAppointmentRepository
{
    /// <inheritdoc />
    public IEnumerable<Appointment> GetAll() => context.Appointments
        .Include(a => a.Client)
        .Include(a => a.Master)
        .Include(a => a.Service)
        .ToList();

    /// <inheritdoc />
    public Appointment? GetById(int id) => context.Appointments
        .Include(a => a.Client)
        .Include(a => a.Master)
        .Include(a => a.Service)
        .FirstOrDefault(a => a.Id == id);

    /// <inheritdoc />
    public void Add(Appointment appointment)
    {
        context.Appointments.Add(appointment);
        context.SaveChanges();
    }

    /// <inheritdoc />
    public void Update(Appointment appointment)
    {
        context.Appointments.Update(appointment);
        context.SaveChanges();
    }

    public void Delete(Appointment appointment)
    {
        context.Appointments.Remove(appointment);
        context.SaveChanges();
    }
}