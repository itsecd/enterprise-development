using BeautySalon.Domain.Entities;

namespace BeautySalon.Domain.Repositories;

/// <summary>
/// Репозиторий для работы с услугами.
/// </summary>
public interface IServiceRepository
{
    /// <summary>
    /// Получить все услуги.
    /// </summary>
    public IEnumerable<Service> GetAll();

    /// <summary>
    /// Получить услугу по ID.
    /// </summary>
    public Service? GetById(int id);

    /// <summary>
    /// Добавить услугу.
    /// </summary>
    public void Add(Service service);

    /// <summary>
    /// Обновить данные услуги.
    /// </summary>
    public void Update(Service service);

    /// <summary>
    /// Удалить услугу.
    /// </summary>
    public void Delete(Service service);
}