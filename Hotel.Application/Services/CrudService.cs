using Hotel.Application.Interfaces;
using Hotel.Domain.Interfaces;

namespace Hotel.Application.Services;

/// <summary>
/// Реализация CRUD-операций
/// </summary>
/// <typeparam name="TEntity">Инициализируемые сущности</typeparam>
/// <typeparam name="TRequest">Запрос</typeparam>
/// <typeparam name="TDto">Возвращаемые значения сущности</typeparam>
public abstract class CrudService<TEntity, TRequest, TDto> : ICrudService<TRequest, TDto>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Репозиторий для доступа к данным
    /// </summary>
    protected readonly IRepository<TEntity> Repository;

    /// <summary>
    /// Инициализация сущностей
    /// </summary>
    /// <param name="repository">Репозиторий</param>
    protected CrudService(IRepository<TEntity> repository)
    {
        Repository = repository;
    }

    /// <summary>
    /// Получение всех записей
    /// </summary>
    /// <returns>Список записей</returns>
    public virtual async Task<IReadOnlyList<TDto>> GetAllAsync()
    {
        var entities = await Repository.GetAllAsync();
        return entities.Select(ToDto).ToList();
    }

    /// <summary>
    /// Получение одной записи
    /// </summary>
    /// <param name="id">Id искомой записи</param>
    /// <returns>Запись</returns>
    /// <exception cref="KeyNotFoundException">Запись не найдена</exception>
    public virtual async Task<TDto> GetAsync(int id)
    {
        var entity = await Repository.GetByIdAsync(id);
        if (entity == null) throw new KeyNotFoundException($"Запись(Id={id}) не найдена");

        return ToDto(entity);
    }

    /// <summary>
    /// Создание записи
    /// </summary>
    /// <param name="request">Параметры создания записи</param>
    /// <returns>Созданная запись</returns>
    public virtual async Task<TDto> CreateAsync(TRequest request)
    {
        var entity = await BuildEntityAsync(request, 0);
        var created = await Repository.AddAsync(entity);
        return ToDto(created);
    }

    /// <summary>
    /// Обновить параметры записи
    /// </summary>
    /// <param name="id">Id записи</param>
    /// <param name="request">Обновляемые параметры</param>
    /// <exception cref="KeyNotFoundException">Запись не найдена</exception>
    public virtual async Task UpdateAsync(int id, TRequest request)
    {
        var updated = await BuildEntityAsync(request, id);

        var success = await Repository.UpdateAsync(id, entity => CopyValues(updated, entity));

        if (!success)
        {
            throw new KeyNotFoundException($"Запись(Id={id}) не найдена");
        }
    }

    /// <summary>
    /// Удаление записи
    /// </summary>
    /// <param name="id">Id записи</param>
    /// <exception cref="KeyNotFoundException">Запись не найдена</exception>
    public virtual async Task DeleteAsync(int id)
    {
        var existing = await Repository.GetByIdAsync(id);
        if (existing == null) throw new KeyNotFoundException($"Запись(Id={id}) не найдена");

        await CheckDeleteAsync(existing);
        await Repository.DeleteAsync(id);
    }

    /// <summary>Создание сущности из запроса</summary>
    protected abstract Task<TEntity> BuildEntityAsync(TRequest request, int id);

    /// <summary>Копирование значений</summary>
    protected abstract void CopyValues(TEntity source, TEntity target);

    /// <summary>Преобразование сущности в Dto</summary>
    protected abstract TDto ToDto(TEntity entity);

    /// <summary>Проверка существования перед удалением</summary>
    protected virtual Task CheckDeleteAsync(TEntity entity) => Task.CompletedTask;
}