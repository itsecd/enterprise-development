using FitnessClub.Contracts.Interfaces;
using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Application.Services;

/// <summary>
/// Базовая реализация CRUD-сервиса над репозиторием
/// </summary>
/// <typeparam name="TEntity">Тип доменной сущности</typeparam>
/// <typeparam name="TDto">Read-DTO</typeparam>
/// <typeparam name="TEditDto">Edit-DTO</typeparam>
/// <param name="repository">Репозиторий сущности</param>
public abstract class CrudServiceBase<TEntity, TDto, TEditDto>(IRepository<TEntity> repository): ICrudService<TDto, TEditDto> where TEntity : class, IEntity where TDto : class where TEditDto : class
{
    /// <summary>
    /// Создаёт запись и возвращает её
    /// </summary>
    /// <param name="editDto"></param>
    public virtual async Task<TDto> Create(TEditDto editDto)
    {
        await ValidateAsync(editDto);

        var entity = await MapToEntityAsync(editDto);
        var created = await repository.Create(entity);

        return MapToDto(created);
    }

    /// <summary>
    /// Возвращает все записи
    /// </summary>
    public async Task<IReadOnlyList<TDto>> ReadAll()
    {
        var entities = await repository.ReadAll();
        return [.. entities.Select(MapToDto)];
    }

    /// <summary>
    /// Возвращает запись по идентификатору
    /// </summary>
    /// <param name="id">Индентификатор</param>
    /// <exception cref="KeyNotFoundException">Запись не найдена</exception>
    public async Task<TDto> Read(int id)
    {
        var entity = await repository.Read(id)
            ?? throw new KeyNotFoundException($"Сущность {typeof(TEntity).Name} с идентификатором {id} не найдена");

        return MapToDto(entity);
    }

    /// <summary>
    /// Обновляет запись и возвращает её
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <param name="editDto"></param>
    /// <exception cref="KeyNotFoundException">Запись не найдена</exception>
    public virtual async Task<TDto> Update(int id, TEditDto editDto)
    {
        await ValidateAsync(editDto);

        _ = await repository.Read(id)
            ?? throw new KeyNotFoundException($"Сущность {typeof(TEntity).Name} с идентификатором {id} не найдена");

        var entity = await MapToEntityAsync(editDto);
        entity.Id = id;

        await repository.Update(entity);
        return MapToDto(entity);
    }

    /// <summary>
    /// Удаляет запись
    /// </summary>
    /// <param name="id">Идентификатор</param>
    /// <exception cref="KeyNotFoundException">Запись не найдена</exception>
    public async Task Delete(int id)
    {
        var deleted = await repository.Delete(id);
        if (!deleted)
            throw new KeyNotFoundException($"Сущность {typeof(TEntity).Name} с идентификатором {id} не найдена");
    }

    /// <summary>
    /// Маппинг сущности в read-DTO
    /// </summary>
    protected abstract TDto MapToDto(TEntity entity);

    /// <summary>
    /// Собирает сущность из edit-DTO
    /// </summary>
    protected abstract Task<TEntity> MapToEntityAsync(TEditDto editDto);

    /// <summary>
    /// Бизнес-валидация edit-DTO
    /// </summary>
    protected virtual Task ValidateAsync(TEditDto editDto) => Task.CompletedTask;
}