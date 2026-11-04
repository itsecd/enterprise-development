using Museum.Domain;
using Museum.Domain.Interfaces;

namespace Museum.Infrastructure.InMemory.Repositories;

/// <summary>
/// Работает с выставками музея
/// </summary>
public sealed class InMemoryExhibitionRepository
    : InMemoryRepository<Exhibition>, IExhibitionRepository
{
    public InMemoryExhibitionRepository(MuseumInMemoryStore store)
        : base(
            store.Exhibitions,
            exhibition => exhibition.Id,
            (exhibition, id) => exhibition.Id = id)
    {
    }
}
