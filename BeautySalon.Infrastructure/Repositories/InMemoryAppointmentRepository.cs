using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.Repositories;

/// <summary>
/// In memory реализация репозитория оплаты.
/// </summary>
public class InMemoryAppointmentRepository : IAppointmentRepository
{
    public IEnumerable<Appointment> GetAll() => SeederData.Appointments;
    public Appointment? GetById(int id) => SeederData.Appointments.FirstOrDefault(a => a.Id == id);
    
    public void Add(Appointment appointment)
    {
        appointment.Id = SeederData.Appointments.Count > 0 ? SeederData.Appointments.Max(a => a.Id) + 1 : 1;
        SeederData.Appointments.Add(appointment);
    }
    
    public void Update(Appointment appointment) { }
    public void Delete(Appointment appointment) => SeederData.Appointments.Remove(appointment);
}