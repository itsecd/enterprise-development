using Museum.Domain;

namespace Museum.Domain.Interfaces;

/// <summary>
/// Описывает работу с билетами музея
/// </summary>
public interface ITicketRepository : IRepository<Ticket>
{
}
