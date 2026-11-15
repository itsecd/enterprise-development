using Microsoft.EntityFrameworkCore;
using Museum.Domain;
using Museum.Domain.Interfaces;

namespace Museum.Infrastructure.MySql.Repositories;

/// <summary>
/// Работает с экскурсиями в базе данных
/// </summary>
public sealed class EfTourRepository(MuseumDbContext context)
    : EfRepository<Tour>(context), ITourRepository
{
    /// <summary>
    /// Запрос экскурсий вместе с выставками
    /// </summary>
    protected override IQueryable<Tour> Query() =>
        Context.Tours.Include(tour => tour.Exhibitions);

    /// <summary>
    /// Добавляет экскурсию и связывает её с уже существующими выставками
    /// </summary>
    public override async Task<Tour> AddAsync(Tour entity)
    {
        var exhibitions = await FindExhibitionsAsync(entity);
        entity.Exhibitions = [.. exhibitions];

        Context.Tours.Add(entity);
        await Context.SaveChangesAsync();

        return entity;
    }

    /// <summary>
    /// Обновляет данные экскурсии и список её выставок
    /// </summary>
    public override async Task<bool> UpdateAsync(Tour entity)
    {
        var existing = await Context.Tours
            .Include(tour => tour.Exhibitions)
            .FirstOrDefaultAsync(tour => tour.Id == entity.Id);

        if (existing is null)
        {
            return false;
        }

        Context.Entry(existing).CurrentValues.SetValues(entity);
        existing.Exhibitions = [.. await FindExhibitionsAsync(entity)];

        await Context.SaveChangesAsync();

        return true;
    }

    private async Task<List<Exhibition>> FindExhibitionsAsync(Tour tour)
    {
        var ids = tour.Exhibitions.Select(exhibition => exhibition.Id).Distinct().ToList();

        return await Context.Exhibitions
            .Where(exhibition => ids.Contains(exhibition.Id))
            .ToListAsync();
    }
}
