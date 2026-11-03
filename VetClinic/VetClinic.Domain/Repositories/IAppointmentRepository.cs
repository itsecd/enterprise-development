using VetClinic.Domain.Models;

namespace VetClinic.Domain.Repositories;

/// <summary>
/// Репозиторий записей на прием
/// </summary>
public interface IAppointmentRepository : IRepository<Appointment>;
