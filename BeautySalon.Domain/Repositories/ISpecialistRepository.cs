using BeautySalon.Domain.Entities;

namespace BeautySalon.Domain.Repositories;

/// <summary>
/// Хранение специалистов
/// </summary>
public interface ISpecialistRepository
{
    /// <summary>
    /// Получить все объекты
    /// </summary>
    IReadOnlyList<BeautySpecialist> GetAll();

    /// <summary>
    /// Найти объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <returns>Найденный объект или null</returns>
    BeautySpecialist? GetById(int id);

    /// <summary>
    /// Добавить объект и назначить ему идентификатор
    /// </summary>
    /// <param name="entity">Добавляемый объект</param>
    /// <returns>Сохранённый объект</returns>
    BeautySpecialist Add(BeautySpecialist entity);

    /// <summary>
    /// Обновить существующий объект
    /// </summary>
    /// <param name="entity">Новые данные с идентификатором объекта</param>
    /// <returns>Признак успешного обновления</returns>
    bool Update(BeautySpecialist entity);

    /// <summary>
    /// Удалить объект по идентификатору
    /// </summary>
    /// <param name="id">Идентификатор объекта</param>
    /// <returns>Признак успешного удаления</returns>
    bool Delete(int id);
}