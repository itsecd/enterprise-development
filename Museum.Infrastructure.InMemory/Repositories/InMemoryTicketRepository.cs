using Museum.Domain;
using Museum.Domain.Interfaces;

namespace Museum.Infrastructure.InMemory.Repositories;

/// <summary>
/// Работает с билетами музея
/// </summary>
public sealed class InMemoryTicketRepository(DataSeeder seeder)
    : InMemoryRepository<Ticket>(
        seeder.Tickets,
        ticket => ticket.Id,
        (ticket, id) => ticket.Id = id),
    ITicketRepository;
