using Museum.Domain;
using Museum.Domain.Interfaces;

namespace Museum.Infrastructure.InMemory.Repositories;

/// <summary>
/// Работает с экскурсиями музея
/// </summary>
public sealed class InMemoryTourRepository
    : InMemoryRepository<Tour>, ITourRepository
{
    public InMemoryTourRepository(MuseumInMemoryStore store)
        : base(
            store.Tours,
            tour => tour.Id,
            (tour, id) => tour.Id = id)
    {
    }
}
