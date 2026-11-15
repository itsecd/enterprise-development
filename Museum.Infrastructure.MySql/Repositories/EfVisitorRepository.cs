using Museum.Domain;
using Museum.Domain.Interfaces;

namespace Museum.Infrastructure.MySql.Repositories;

/// <summary>
/// Работает с посетителями в базе данных
/// </summary>
public sealed class EfVisitorRepository(MuseumDbContext context)
    : EfRepository<Visitor>(context), IVisitorRepository;
