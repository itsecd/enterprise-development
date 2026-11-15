using Museum.Domain;
using Museum.Domain.Interfaces;

namespace Museum.Infrastructure.MySql.Repositories;

/// <summary>
/// Работает с выставками в базе данных
/// </summary>
public sealed class EfExhibitionRepository(MuseumDbContext context)
    : EfRepository<Exhibition>(context), IExhibitionRepository;
