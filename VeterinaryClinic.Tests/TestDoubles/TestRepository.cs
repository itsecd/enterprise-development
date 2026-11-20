using VeterinaryClinic.Domain.Models;
using System.Linq.Expressions;
using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Tests.TestDoubles;

/// <summary>Тестовая подмена репозитория для проверки сервисов без базы данных.</summary>
public sealed class TestRepository<T>(IEnumerable<T> initial, Func<T, int> idSelector) : IRepository<T> where T : class, IEntity
{
    private readonly List<T> _items = initial.ToList();
    public Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult<IReadOnlyList<T>>(_items.ToArray());
    }
    public Task<IReadOnlyList<T>> FindAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult<IReadOnlyList<T>>(_items.Where(predicate.Compile()).ToArray());
    }
    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(_items.Any(predicate.Compile()));
    }
    public Task<int> CountAsync(Expression<Func<T, bool>> predicate, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(_items.Count(predicate.Compile()));
    }
    public Task<T?> GetAsync(int id, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        return Task.FromResult(_items.FirstOrDefault(x => idSelector(x) == id));
    }
    public Task<T> CreateAsync(T entity, CancellationToken cancellationToken = default)
    {
        cancellationToken.ThrowIfCancellationRequested();
        if (entity.Id == 0) entity.Id = _items.Count == 0 ? 1 : _items.Max(x => x.Id) + 1;
        _items.Add(entity);
        return Task.FromResult(entity);
    }
    public async Task<T> UpdateAsync(int id, Action<T> apply, CancellationToken cancellationToken = default)
    {
        var entity = await GetAsync(id, cancellationToken) ?? throw new KeyNotFoundException($"Сущность с идентификатором {id} не найдена");
        apply(entity);
        return entity;
    }
    public async Task<bool> RemoveAsync(int id, CancellationToken cancellationToken = default)
    {
        var entity = await GetAsync(id, cancellationToken);
        return entity is not null && _items.Remove(entity);
    }
}
