using BeautySalon.Domain.Data;
using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Repositories;

namespace BeautySalon.Infrastructure.Repositories;

/// <summary>
/// In memory реализация репозитория записей.
/// </summary>
public class InMemoryAppointmentRepository : IAppointmentRepository
{
    private readonly List<Appointment> _appointments;

    /// <summary>
    /// Инициализирует репозиторий, копируя данные из SeederData.
    /// </summary>
    public InMemoryAppointmentRepository()
    {
        _appointments = SeederData.Appointments.ToList();
    }

    /// <inheritdoc />
    public IEnumerable<Appointment> GetAll() => _appointments;

    /// <inheritdoc />
    public Appointment? GetById(int id) => _appointments.FirstOrDefault(a => a.Id == id);

    /// <inheritdoc />
    public void Add(Appointment appointment)
    {
        appointment.Id = _appointments.Count > 0 ? _appointments.Max(a => a.Id) + 1 : 1;
        _appointments.Add(appointment);
    }

    /// <inheritdoc />
    public void Update(Appointment appointment) { }

    /// <inheritdoc />
    public void Delete(Appointment appointment) => _appointments.Remove(appointment);
}