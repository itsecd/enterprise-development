using Museum.Contracts;
using Museum.Domain.Data;
using Museum.Domain.Entities;

namespace Museum.Application.Repositories;

/// <summary>
/// Работает с экскурсиями в хранилище музея.
/// </summary>
public class ToursRepository(MuseumContext context)
{
    /// <summary>Возвращает все экскурсии.</summary>
    public IReadOnlyList<TourResponse> GetAll()
    {
        lock (context) return context.Tours.OrderBy(x => x.Id).Select(ToResponse).ToArray();
    }

    /// <summary>Возвращает экскурсию по идентификатору.</summary>
    public TourResponse? GetById(int id)
    {
        lock (context)
        {
            var tour = context.Tours.FirstOrDefault(x => x.Id == id);
            return tour is null ? null : ToResponse(tour);
        }
    }

    /// <summary>Создаёт экскурсию.</summary>
    public TourResponse Create(TourRequest request)
    {
        lock (context)
        {
            var nextId = context.Tours.Count == 0 ? 1 : context.Tours.Max(x => x.Id) + 1;
            var tour = new Tour
            {
                Id = nextId,
                StartDateTime = request.StartDateTime,
                Duration = request.Duration
            };
            context.Tours.Add(tour);
            return ToResponse(tour);
        }
    }

    /// <summary>Изменяет существующую экскурсию.</summary>
    public bool Update(int id, TourRequest request)
    {
        lock (context)
        {
            var tour = context.Tours.FirstOrDefault(x => x.Id == id);
            if (tour is null) return false;
            tour.StartDateTime = request.StartDateTime;
            tour.Duration = request.Duration;
            return true;
        }
    }

    /// <summary>Удаляет экскурсию вместе с билетами и связями с выставками.</summary>
    public bool Delete(int id)
    {
        lock (context)
        {
            var tour = context.Tours.FirstOrDefault(x => x.Id == id);
            if (tour is null) return false;
            foreach (var ticket in tour.Tickets.ToList())
            {
                ticket.Visitor.Tickets.Remove(ticket);
                tour.Tickets.Remove(ticket);
                context.Tickets.Remove(ticket);
            }
            foreach (var relation in tour.TourExhibitions.ToList())
            {
                relation.Exhibition.TourExhibitions.Remove(relation);
                tour.TourExhibitions.Remove(relation);
                context.TourExhibitions.Remove(relation);
            }
            context.Tours.Remove(tour);
            return true;
        }
    }

    /// <summary>Преобразует доменную сущность в ответ API.</summary>
    private static TourResponse ToResponse(Tour tour) => new(
        tour.Id, tour.StartDateTime, tour.Duration,
        tour.TourExhibitions.Select(x => x.ExhibitionId).OrderBy(id => id).ToArray());
}
