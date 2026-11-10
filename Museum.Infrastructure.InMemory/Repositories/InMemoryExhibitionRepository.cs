using Museum.Domain;
using Museum.Domain.Interfaces;

namespace Museum.Infrastructure.InMemory.Repositories;

/// <summary>
/// Работает с выставками музея
/// </summary>
public sealed class InMemoryExhibitionRepository(DataSeeder seeder)
    : InMemoryRepository<Exhibition>(
        seeder.Exhibitions,
        exhibition => exhibition.Id,
        (exhibition, id) => exhibition.Id = id),
    IExhibitionRepository;
