using BeautySalon.Domain.Entities;

namespace BeautySalon.Domain.Repositories;

/// <summary>
/// Репозиторий для работы с записями.
/// </summary>
public interface IAppointmentRepository
{
    /// <summary>
    /// Получить все записи.
    /// </summary>
    public IEnumerable<Appointment> GetAll();

    /// <summary>
    /// Получить запись по ID.
    /// </summary>
    public Appointment? GetById(int id);

    /// <summary>
    /// Добавить запись.
    /// </summary>
    public void Add(Appointment appointment);

    /// <summary>
    /// Обновить данные записи.
    /// </summary>
    public void Update(Appointment appointment);

    /// <summary>
    /// Удалить запись.
    /// </summary>
    public void Delete(Appointment appointment);
}