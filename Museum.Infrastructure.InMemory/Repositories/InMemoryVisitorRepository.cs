using Museum.Domain;
using Museum.Domain.Interfaces;

namespace Museum.Infrastructure.InMemory.Repositories;

/// <summary>
/// Работает с посетителями музея
/// </summary>
public sealed class InMemoryVisitorRepository
    : InMemoryRepository<Visitor>, IVisitorRepository
{
    public InMemoryVisitorRepository(MuseumInMemoryStore store)
        : base(
            store.Visitors,
            visitor => visitor.Id,
            (visitor, id) => visitor.Id = id)
    {
    }
}
