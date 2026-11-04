using BeautySalon.Domain.Entities;

namespace BeautySalon.Domain.Repositories;

/// <summary>
/// Репозиторий для работы с мастерами.
/// </summary>
public interface IMasterRepository
{
    /// <summary>
    /// Получить всех клиентов.
    /// </summary>
    /// 
    public IEnumerable<Master> GetAll();
    
    /// <summary>
    /// Получить мастера по ID.
    /// </summary> 
    public Master? GetById(int id);
    
    /// <summary>
    /// Добавиьь мастера.
    /// </summary>
    public void Add(Master master);
    
    /// <summary>
    /// Обновить данные мастера.
    /// </summary>
    public void Update(Master master);
    
    /// <summary>
    /// Удалить мастера.
    /// </summary>
    public void Delete(Master master);
}