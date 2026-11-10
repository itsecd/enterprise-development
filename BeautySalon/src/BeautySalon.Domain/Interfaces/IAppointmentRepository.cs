using BeautySalon.Domain.Entities;

namespace BeautySalon.Domain.Interfaces;

/// <summary>
/// Репозиторий записей клиентов на приём
/// </summary>
public interface IAppointmentRepository : IRepository<Appointment>;