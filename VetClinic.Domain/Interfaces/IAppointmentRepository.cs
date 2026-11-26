using VetClinic.Domain.Entities;

namespace VetClinic.Domain.Interfaces;

/// <summary>
/// Интерфейс репозитория для приёмов
/// </summary>
public interface IAppointmentRepository : IRepository<Appointment>
{
    /// <summary>
    /// Поиск записей приёмов по id врача
    /// </summary>
    /// <param name="petid">id интересуемого ветеринара</param>
    /// <returns>Список приёмов</returns>
    public Task<List<Appointment>> GetAppointmentsByPetId(int petid);
}
