using FitnessClub.Domain.Interfaces;

namespace FitnessClub.Infrastructure.InMemory.Repositories.Base;

/// <summary>
/// Базовая реализация репозитория для хранения сущностей в памяти.
/// </summary>
/// <typeparam name="TEntity">Тип сущности.</typeparam>
public class InMemoryRepository<TEntity> : IRepository<TEntity>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Коллекция сущностей, переданная из общего хранилища.
    /// </summary>
    protected readonly List<TEntity> Entities;

    /// <summary>
    /// Создаёт репозиторий с указанной коллекцией.
    /// </summary>
    /// <param name="entities">Общая коллекция сущностей.</param>
    public InMemoryRepository(List<TEntity> entities)
    {
        ArgumentNullException.ThrowIfNull(entities);

        Entities = entities;
    }

    /// <summary>
    /// Получает все сущности.
    /// </summary>
    /// <returns>Коллекция сущностей.</returns>
    public Task<IReadOnlyCollection<TEntity>> GetAllAsync()
    {
        IReadOnlyCollection<TEntity> result = [.. Entities];

        return Task.FromResult(result);
    }

    /// <summary>
    /// Получает сущность по IDу.
    /// </summary>
    /// <param name="id">ID сущности.</param>
    /// <returns>Сущность или null, если она не найдена.</returns>
    public Task<TEntity?> GetByIdAsync(int id)
    {
        var entity = Entities.FirstOrDefault(item => item.Id == id);

        return Task.FromResult(entity);
    }

    /// <summary>
    /// Добавляет новую сущность.
    /// </summary>
    /// <param name="entity">Добавляемая сущность.</param>
    /// <returns>Добавленная сущность.</returns>
    /// <exception cref="ArgumentNullException">
    /// Выбрасывается, если передана null.
    /// </exception>
    /// <exception cref="InvalidOperationException">
    /// Выбрасывается, если сущность с таким ID уже существует.
    /// </exception>
    public Task<TEntity> AddAsync(TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        if (entity.Id <= 0)
        {
            entity.Id = Entities.Count == 0
                ? 1
                : Entities.Max(item => item.Id) + 1;
        }

        if (Entities.Any(item => item.Id == entity.Id))
        {
            throw new InvalidOperationException(
                $"Сущность с ID {entity.Id} уже существует.");
        }

        Entities.Add(entity);

        return Task.FromResult(entity);
    }

    /// <summary>
    /// Обновляет существующую сущность.
    /// </summary>
    /// <param name="id">ID обновляемой сущности.</param>
    /// <param name="entity">Новые данные сущности.</param>
    /// <returns>true, если сущность обновлена; иначе false.</returns>
    public Task<bool> UpdateAsync(int id, TEntity entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var index = Entities.FindIndex(item => item.Id == id);

        if (index < 0)
        {
            return Task.FromResult(false);
        }

        var duplicateIdExists = Entities.Any(
            item => item.Id == entity.Id && item.Id != id);

        if (duplicateIdExists)
        {
            return Task.FromResult(false);
        }

        entity.Id = id;
        Entities[index] = entity;

        return Task.FromResult(true);
    }

    /// <summary>
    /// Удаляет сущность по IDу.
    /// </summary>
    /// <param name="id">ID удаляемой сущности.</param>
    /// <returns>true, если сущность удалена; иначе false.</returns>
    public Task<bool> DeleteAsync(int id)
    {
        var entity = Entities.FirstOrDefault(item => item.Id == id);

        if (entity is null)
        {
            return Task.FromResult(false);
        }

        Entities.Remove(entity);

        return Task.FromResult(true);
    }
}