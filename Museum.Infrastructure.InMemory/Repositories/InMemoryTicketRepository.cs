using Museum.Domain;
using Museum.Domain.Interfaces;

namespace Museum.Infrastructure.InMemory.Repositories;

/// <summary>
/// Работает с билетами музея
/// </summary>
public sealed class InMemoryTicketRepository
    : InMemoryRepository<Ticket>, ITicketRepository
{
    public InMemoryTicketRepository(MuseumInMemoryStore store)
        : base(
            store.Tickets,
            ticket => ticket.Id,
            (ticket, id) => ticket.Id = id)
    {
    }
}
