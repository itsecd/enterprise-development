using Museum.Domain;
using Museum.Domain.Interfaces;

namespace Museum.Infrastructure.InMemory.Repositories;

/// <summary>
/// Работает с экскурсиями музея
/// </summary>
public sealed class InMemoryTourRepository(DataSeeder seeder)
    : InMemoryRepository<Tour>(
        seeder.Tours,
        tour => tour.Id,
        (tour, id) => tour.Id = id),
    ITourRepository;
