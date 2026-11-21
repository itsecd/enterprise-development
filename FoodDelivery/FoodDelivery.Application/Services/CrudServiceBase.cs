using FoodDelivery.Application.Contracts.Common;
using FoodDelivery.Application.Exceptions;
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
    public async Task<IReadOnlyList<TDto>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return (await Repository.GetAllAsync(cancellationToken))
            .Select(ToDto)
            .ToArray();
    }

    /// <inheritdoc/>
    public async Task<TDto> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await Repository.GetByIdAsync(id, cancellationToken) ?? throw MissingResource(id);

        return ToDto(entity);
    }

    /// <inheritdoc/>
    public async Task<TDto> CreateAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        var entity = await ToEntityAsync(request, 0, null, cancellationToken);
        var createdEntity = await Repository.AddAsync(entity, cancellationToken);

        return ToDto(createdEntity);
    }

    /// <inheritdoc/>
    public virtual async Task<TDto> UpdateAsync(int id, TRequest request, CancellationToken cancellationToken = default)
    {
        var existing = await Repository.GetByIdAsync(id, cancellationToken)
            ?? throw MissingResource(id);

        var entity = await ToEntityAsync(request, id, existing, cancellationToken);

        if (!await Repository.UpdateAsync(entity, cancellationToken))
        {
            throw MissingResource(id);
        }

        return ToDto(entity);
    }

    /// <inheritdoc/>
    public virtual async Task DeleteAsync(int id, CancellationToken cancellationToken = default)
    {
        await Repository.DeleteAsync(id, cancellationToken);
    }

    private static ResourceNotFoundException MissingResource(int id)
    {
        return new ResourceNotFoundException($"Ресурс с идентификатором {id} не найден");
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
    /// <param name="existing">Текущая сущность при обновлении</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    protected abstract Task<TEntity> ToEntityAsync(
        TRequest request,
        int id,
        TEntity? existing,
        CancellationToken cancellationToken);
}
