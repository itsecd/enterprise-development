using VeterinaryClinic.Domain.Repositories;

namespace VeterinaryClinic.Infrastructure;

/// <summary>
/// Хранит записи в памяти приложения
/// </summary>
public sealed class InMemoryRepository<T>(IEnumerable<T> initial, Func<T, int> idSelector) : IRepository<T> where T : class
{
    private readonly List<T> _items = initial.ToList();

    /// <inheritdoc />
    public IReadOnlyList<T> GetAll() => _items.ToArray();

    /// <inheritdoc />
    public IReadOnlyList<T> Find(Func<T, bool> predicate) => _items.Where(predicate).ToArray();

    /// <inheritdoc />
    public bool Any(Func<T, bool> predicate) => _items.Any(predicate);

    /// <inheritdoc />
    public int Count(Func<T, bool> predicate) => _items.Count(predicate);

    /// <inheritdoc />
    public T? Get(int id) => _items.FirstOrDefault(x => idSelector(x) == id);

    /// <inheritdoc />
    public int NextId() => _items.Count == 0 ? 1 : _items.Max(idSelector) + 1;

    /// <inheritdoc />
    public T Create(Func<int, T> factory)
    {
        var entity = factory(NextId());
        _items.Add(entity);
        return entity;
    }

    /// <inheritdoc />
    public T Update(int id, Action<T> apply)
    {
        var entity = Get(id) ?? throw new KeyNotFoundException($"Entity with ID {id} was not found");
        apply(entity);
        return entity;
    }

    /// <inheritdoc />
    public bool Remove(int id)
    {
        var entity = Get(id);
        return entity is not null && _items.Remove(entity);
    }
}
