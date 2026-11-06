using FoodDelivery.Application.Contracts.Common;
using FoodDelivery.Domain.Repositories;

namespace FoodDelivery.Application.Services;

/// <summary>
/// Реализует основные прикладные операции с ресурсом
/// </summary>
/// <typeparam name="TEntity">Тип доменной сущности</typeparam>
/// <typeparam name="TDto">Тип данных ответа</typeparam>
/// <typeparam name="TRequest">Тип данных запроса</typeparam>
public abstract class CrudServiceBase<TEntity, TDto, TRequest>
    : ICrudService<TDto, TRequest>
    where TEntity : class
    where TDto : class
    where TRequest : class
{
    /// <summary>
    /// Репозиторий сущностей
    /// </summary>
    protected abstract IRepository<TEntity> Repository { get; }

    /// <inheritdoc/>
    public IReadOnlyList<TDto> GetAll()
    {
        return Repository.Execute<IReadOnlyList<TDto>>(() =>
        {
            return Repository.GetAll()
                .Select(ToDto)
                .ToArray();
        });
    }

    /// <inheritdoc/>
    public TDto? GetById(int id)
    {
        return Repository.Execute<TDto?>(() =>
        {
            var entity = Repository.GetById(id);

            return entity is null ? null : ToDto(entity);
        });
    }

    /// <inheritdoc/>
    public TDto Create(TRequest request)
    {
        return Repository.Execute<TDto>(() =>
        {
            var entity = ToEntity(request, 0);
            var createdEntity = Repository.Add(entity);

            return ToDto(createdEntity);
        });
    }

    /// <inheritdoc/>
    public TDto? Update(int id, TRequest request)
    {
        return Repository.Execute<TDto?>(() =>
        {
            var entity = ToEntity(request, id);

            return Repository.Update(entity) ? ToDto(entity) : null;
        });
    }

    /// <inheritdoc/>
    public virtual void Delete(int id)
    {
        Repository.Execute(() =>
        {
            Repository.Delete(id);
        });
    }

    /// <summary>
    /// Преобразует сущность в данные ответа
    /// </summary>
    /// <param name="entity">Доменная сущность</param>
    /// <returns>Данные ответа</returns>
    protected abstract TDto ToDto(TEntity entity);

    /// <summary>
    /// Преобразует запрос в доменную сущность
    /// </summary>
    /// <param name="request">Данные запроса</param>
    /// <param name="id">Идентификатор сущности</param>
    /// <returns>Доменная сущность</returns>
    protected abstract TEntity ToEntity(TRequest request, int id);
}
