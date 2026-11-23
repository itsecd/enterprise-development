using Museum.Contracts;
using Museum.Domain.Data;
using Museum.Domain.Entities;

namespace Museum.Application.Repositories;

/// <summary>
/// Работает с посетителями в хранилище музея.
/// </summary>
public class VisitorsRepository(MuseumContext context)
{
    /// <summary>Возвращает всех посетителей.</summary>
    public IReadOnlyList<VisitorResponse> GetAll()
    {
        lock (context) return context.Visitors.OrderBy(x => x.Id).Select(ToResponse).ToArray();
    }

    /// <summary>Возвращает посетителя по идентификатору.</summary>
    public VisitorResponse? GetById(int id)
    {
        lock (context)
        {
            var visitor = context.Visitors.FirstOrDefault(x => x.Id == id);
            return visitor is null ? null : ToResponse(visitor);
        }
    }

    /// <summary>Создаёт посетителя.</summary>
    public VisitorResponse Create(VisitorRequest request)
    {
        lock (context)
        {
            var nextId = context.Visitors.Count == 0 ? 1 : context.Visitors.Max(x => x.Id) + 1;
            var visitor = new Visitor
            {
                Id = nextId,
                FullName = request.FullName,
                Phone = request.Phone,
                BirthDate = request.BirthDate
            };
            context.Visitors.Add(visitor);
            return ToResponse(visitor);
        }
    }

    /// <summary>Изменяет существующего посетителя.</summary>
    public bool Update(int id, VisitorRequest request)
    {
        lock (context)
        {
            var visitor = context.Visitors.FirstOrDefault(x => x.Id == id);
            if (visitor is null) return false;
            visitor.FullName = request.FullName;
            visitor.Phone = request.Phone;
            visitor.BirthDate = request.BirthDate;
            return true;
        }
    }

    /// <summary>Удаляет посетителя вместе с его билетами.</summary>
    public bool Delete(int id)
    {
        lock (context)
        {
            var visitor = context.Visitors.FirstOrDefault(x => x.Id == id);
            if (visitor is null) return false;
            foreach (var ticket in context.Tickets.Where(x => x.VisitorId == id).ToList())
            {
                ticket.Tour.Tickets.Remove(ticket);
                visitor.Tickets.Remove(ticket);
                context.Tickets.Remove(ticket);
            }
            context.Visitors.Remove(visitor);
            return true;
        }
    }

    /// <summary>Преобразует доменную сущность в ответ API.</summary>
    private static VisitorResponse ToResponse(Visitor visitor) => new(
        visitor.Id, visitor.FullName, visitor.Phone, visitor.BirthDate);
}
