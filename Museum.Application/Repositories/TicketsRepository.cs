using Museum.Contracts;
using Museum.Domain.Data;
using Museum.Domain.Entities;

namespace Museum.Application.Repositories;

/// <summary>
/// Работает с билетами в хранилище музея.
/// </summary>
public class TicketsRepository(MuseumContext context)
{
    /// <summary>Возвращает все билеты.</summary>
    public IReadOnlyList<TicketResponse> GetAll()
    {
        lock (context) return context.Tickets.OrderBy(x => x.Id).Select(ToResponse).ToArray();
    }

    /// <summary>Возвращает билет по идентификатору.</summary>
    public TicketResponse? GetById(int id)
    {
        lock (context)
        {
            var ticket = context.Tickets.FirstOrDefault(x => x.Id == id);
            return ticket is null ? null : ToResponse(ticket);
        }
    }

    /// <summary>Создаёт билет при наличии экскурсии и посетителя.</summary>
    public TicketResponse? Create(TicketRequest request)
    {
        lock (context)
        {
            var tour = context.Tours.FirstOrDefault(x => x.Id == request.TourId);
            var visitor = context.Visitors.FirstOrDefault(x => x.Id == request.VisitorId);
            if (tour is null || visitor is null) return null;
            var nextId = context.Tickets.Count == 0 ? 1 : context.Tickets.Max(x => x.Id) + 1;
            var ticket = new Ticket
            {
                Id = nextId,
                TourId = tour.Id,
                VisitorId = visitor.Id,
                Type = request.Type,
                Price = request.Price,
                Tour = tour,
                Visitor = visitor
            };
            context.Tickets.Add(ticket);
            tour.Tickets.Add(ticket);
            visitor.Tickets.Add(ticket);
            return ToResponse(ticket);
        }
    }

    /// <summary>Изменяет билет и при необходимости обновляет связи.</summary>
    public RepositoryChangeResult Update(int id, TicketRequest request)
    {
        lock (context)
        {
            var ticket = context.Tickets.FirstOrDefault(x => x.Id == id);
            if (ticket is null) return RepositoryChangeResult.NotFound;
            var tour = context.Tours.FirstOrDefault(x => x.Id == request.TourId);
            var visitor = context.Visitors.FirstOrDefault(x => x.Id == request.VisitorId);
            if (tour is null || visitor is null) return RepositoryChangeResult.InvalidReference;
            ticket.Tour.Tickets.Remove(ticket);
            ticket.Visitor.Tickets.Remove(ticket);
            ticket.TourId = tour.Id;
            ticket.VisitorId = visitor.Id;
            ticket.Type = request.Type;
            ticket.Price = request.Price;
            ticket.Tour = tour;
            ticket.Visitor = visitor;
            tour.Tickets.Add(ticket);
            visitor.Tickets.Add(ticket);
            return RepositoryChangeResult.Success;
        }
    }

    /// <summary>Удаляет билет и очищает его связи.</summary>
    public bool Delete(int id)
    {
        lock (context)
        {
            var ticket = context.Tickets.FirstOrDefault(x => x.Id == id);
            if (ticket is null) return false;
            ticket.Tour.Tickets.Remove(ticket);
            ticket.Visitor.Tickets.Remove(ticket);
            context.Tickets.Remove(ticket);
            return true;
        }
    }

    /// <summary>Преобразует доменную сущность в ответ API.</summary>
    private static TicketResponse ToResponse(Ticket ticket) => new(
        ticket.Id, ticket.TourId, ticket.VisitorId, ticket.Type, ticket.Price);
}
