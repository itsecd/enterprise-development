using Museum.Domain;
using Museum.Domain.Interfaces;

namespace Museum.Infrastructure.InMemory.Repositories;

/// <summary>
/// Работает с посетителями музея
/// </summary>
public sealed class InMemoryVisitorRepository(DataSeeder seeder)
    : InMemoryRepository<Visitor>(
        seeder.Visitors,
        visitor => visitor.Id,
        (visitor, id) => visitor.Id = id),
    IVisitorRepository;
