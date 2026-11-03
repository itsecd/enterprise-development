using HotelSystem.Application.Contracts;

namespace HotelSystem.Application.Services;

/// <summary>
/// Определяет общие CRUD-операции для сервисов приложения
/// </summary>
public interface ICrudService<TRequest, TResponse>
    where TResponse : IEntityResponse
{
    /// <summary>
    /// Возвращает все сущности
    /// </summary>
    public List<TResponse> GetAll();

    /// <summary>
    /// Возвращает сущность по идентификатору
    /// </summary>
    public TResponse? GetById(int id);

    /// <summary>
    /// Создаёт сущность
    /// </summary>
    /// <exception cref="Exceptions.ReferenceNotFoundException">
    /// Связанная сущность из запроса не найдена
    /// </exception>
    public TResponse Add(TRequest request);

    /// <summary>
    /// Обновляет сущность
    /// </summary>
    /// <exception cref="Exceptions.ReferenceNotFoundException">
    /// Связанная сущность из запроса не найдена
    /// </exception>
    public bool Update(int id, TRequest request);

    /// <summary>
    /// Удаляет сущность
    /// </summary>
    public bool Delete(int id);
}