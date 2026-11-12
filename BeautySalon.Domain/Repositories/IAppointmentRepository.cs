using BeautySalon.Domain.Entities;

namespace BeautySalon.Domain.Repositories;

/// <summary>
/// Хранение записей
/// </summary>
public interface IAppointmentRepository
{
    /// <summary>
    /// Получить все объекты
    /// </summary>
    IReadOnlyList<Appointment> GetAll();

    /// <summary>
    /// Найти объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <returns>Найденный объект или null</returns>
    Appointment? GetById(int id);

    /// <summary>
    /// Добавить объект и назначить ему идентификатор
    /// </summary>
    /// <param name="entity">Добавляемый объект</param>
    /// <returns>Сохранённый объект</returns>
    Appointment Add(Appointment entity);

    /// <summary>
    /// Обновить существующий объект
    /// </summary>
    /// <param name="entity">Новые данные с идентификатором объекта</param>
    /// <returns>Признак успешного обновления</returns>
    bool Update(Appointment entity);

    /// <summary>
    /// Удалить объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <returns>Признак успешного удаления</returns>
    bool Delete(int id);
}