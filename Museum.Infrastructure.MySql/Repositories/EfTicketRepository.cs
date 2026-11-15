using Microsoft.EntityFrameworkCore;
using Museum.Domain;
using Museum.Domain.Interfaces;

namespace Museum.Infrastructure.MySql.Repositories;

/// <summary>
/// Работает с билетами в базе данных
/// </summary>
public sealed class EfTicketRepository(MuseumDbContext context)
    : EfRepository<Ticket>(context), ITicketRepository
{
    /// <summary>
    /// Запрос билетов вместе с экскурсиями, их выставками и посетителями
    /// </summary>
    protected override IQueryable<Ticket> Query() =>
        Context.Tickets
            .Include(ticket => ticket.Tour)
                .ThenInclude(tour => tour.Exhibitions)
            .Include(ticket => ticket.Visitor);

    /// <summary>
    /// Добавляет билет, связывая его с уже существующими экскурсией и посетителем
    /// </summary>
    public override async Task<Ticket> AddAsync(Ticket entity)
    {
        entity.Tour = await FindTourAsync(entity);
        entity.Visitor = await FindVisitorAsync(entity);

        Context.Tickets.Add(entity);
        await Context.SaveChangesAsync();

        return entity;
    }

    /// <summary>
    /// Обновляет билет и его связи
    /// </summary>
    public override async Task<bool> UpdateAsync(Ticket entity)
    {
        var existing = await Context.Tickets.FindAsync(entity.Id);

        if (existing is null)
        {
            return false;
        }

        existing.Tour = await FindTourAsync(entity);
        existing.Visitor = await FindVisitorAsync(entity);
        existing.Type = entity.Type;
        existing.Price = entity.Price;

        await Context.SaveChangesAsync();

        return true;
    }

    private async Task<Tour> FindTourAsync(Ticket ticket) =>
        await Context.Tours.FindAsync(ticket.Tour.Id)
        ?? throw new InvalidOperationException($"Экскурсия {ticket.Tour.Id} не найдена");

    private async Task<Visitor> FindVisitorAsync(Ticket ticket) =>
        await Context.Visitors.FindAsync(ticket.Visitor.Id)
        ?? throw new InvalidOperationException($"Посетитель {ticket.Visitor.Id} не найден");
}
