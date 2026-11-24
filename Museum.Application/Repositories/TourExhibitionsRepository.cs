using Museum.Contracts;
using Museum.Domain.Data;
using Museum.Domain.Entities;

namespace Museum.Application.Repositories;

/// <summary>
/// Работает со связями экскурсий и выставок в хранилище музея.
/// </summary>
public class TourExhibitionsRepository(MuseumContext context)
{
    /// <summary>Возвращает все связи экскурсий и выставок.</summary>
    public IReadOnlyList<TourExhibitionResponse> GetAll()
    {
        lock (context)
        {
            return context.TourExhibitions.OrderBy(x => x.TourId)
                .ThenBy(x => x.ExhibitionId).Select(ToResponse).ToArray();
        }
    }

    /// <summary>Возвращает связь по составному ключу.</summary>
    public TourExhibitionResponse? GetById(int tourId, int exhibitionId)
    {
        lock (context)
        {
            var relation = context.TourExhibitions.FirstOrDefault(x =>
                x.TourId == tourId && x.ExhibitionId == exhibitionId);
            return relation is null ? null : ToResponse(relation);
        }
    }

    /// <summary>Создаёт связь между экскурсией и выставкой.</summary>
    public (RepositoryChangeResult Status, TourExhibitionResponse? Response) Create(TourExhibitionRequest request)
    {
        lock (context)
        {
            var tour = context.Tours.FirstOrDefault(x => x.Id == request.TourId);
            var exhibition = context.Exhibitions.FirstOrDefault(x => x.Id == request.ExhibitionId);
            if (tour is null || exhibition is null) return (RepositoryChangeResult.InvalidReference, null);
            if (context.TourExhibitions.Any(x => x.TourId == request.TourId && x.ExhibitionId == request.ExhibitionId))
                return (RepositoryChangeResult.Conflict, null);
            var relation = new TourExhibition
            {
                TourId = tour.Id,
                ExhibitionId = exhibition.Id,
                Tour = tour,
                Exhibition = exhibition
            };
            context.TourExhibitions.Add(relation);
            tour.TourExhibitions.Add(relation);
            exhibition.TourExhibitions.Add(relation);
            return (RepositoryChangeResult.Success, ToResponse(relation));
        }
    }

    /// <summary>Изменяет связь с проверкой дублирования.</summary>
    public RepositoryChangeResult Update(int tourId, int exhibitionId, TourExhibitionRequest request)
    {
        lock (context)
        {
            var relation = context.TourExhibitions.FirstOrDefault(x =>
                x.TourId == tourId && x.ExhibitionId == exhibitionId);
            if (relation is null) return RepositoryChangeResult.NotFound;
            var newTour = context.Tours.FirstOrDefault(x => x.Id == request.TourId);
            var newExhibition = context.Exhibitions.FirstOrDefault(x => x.Id == request.ExhibitionId);
            if (newTour is null || newExhibition is null) return RepositoryChangeResult.InvalidReference;
            if (context.TourExhibitions.Any(x => !ReferenceEquals(x, relation) &&
                x.TourId == request.TourId && x.ExhibitionId == request.ExhibitionId))
                return RepositoryChangeResult.Conflict;
            relation.Tour.TourExhibitions.Remove(relation);
            relation.Exhibition.TourExhibitions.Remove(relation);
            relation.TourId = newTour.Id;
            relation.ExhibitionId = newExhibition.Id;
            relation.Tour = newTour;
            relation.Exhibition = newExhibition;
            newTour.TourExhibitions.Add(relation);
            newExhibition.TourExhibitions.Add(relation);
            return RepositoryChangeResult.Success;
        }
    }

    /// <summary>Удаляет связь экскурсии и выставки.</summary>
    public bool Delete(int tourId, int exhibitionId)
    {
        lock (context)
        {
            var relation = context.TourExhibitions.FirstOrDefault(x =>
                x.TourId == tourId && x.ExhibitionId == exhibitionId);
            if (relation is null) return false;
            relation.Tour.TourExhibitions.Remove(relation);
            relation.Exhibition.TourExhibitions.Remove(relation);
            context.TourExhibitions.Remove(relation);
            return true;
        }
    }

    /// <summary>Преобразует доменную сущность в ответ API.</summary>
    private static TourExhibitionResponse ToResponse(TourExhibition relation) => new(
        relation.TourId, relation.ExhibitionId);
}
