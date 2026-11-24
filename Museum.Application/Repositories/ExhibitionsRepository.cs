using Museum.Contracts;
using Museum.Domain.Data;
using Museum.Domain.Entities;

namespace Museum.Application.Repositories;

/// <summary>
/// Работает с выставками в хранилище музея.
/// </summary>
public class ExhibitionsRepository(MuseumContext context)
{
    /// <summary>Возвращает все выставки.</summary>
    public IReadOnlyList<ExhibitionResponse> GetAll()
    {
        lock (context)
        {
            return context.Exhibitions.OrderBy(x => x.Id).Select(ToResponse).ToArray();
        }
    }

    /// <summary>Возвращает выставку по идентификатору.</summary>
    public ExhibitionResponse? GetById(int id)
    {
        lock (context)
        {
            var exhibition = context.Exhibitions.FirstOrDefault(x => x.Id == id);
            return exhibition is null ? null : ToResponse(exhibition);
        }
    }

    /// <summary>Создаёт выставку.</summary>
    public ExhibitionResponse Create(ExhibitionRequest request)
    {
        lock (context)
        {
            var nextId = context.Exhibitions.Count == 0 ? 1 : context.Exhibitions.Max(x => x.Id) + 1;
            var exhibition = new Exhibition
            {
                Id = nextId,
                Name = request.Name,
                Theme = request.Theme,
                HallNumber = request.HallNumber,
                StartDate = request.StartDate,
                EndDate = request.EndDate
            };
            context.Exhibitions.Add(exhibition);
            return ToResponse(exhibition);
        }
    }

    /// <summary>Изменяет выставку, если она существует.</summary>
    public bool Update(int id, ExhibitionRequest request)
    {
        lock (context)
        {
            var exhibition = context.Exhibitions.FirstOrDefault(x => x.Id == id);
            if (exhibition is null) return false;
            exhibition.Name = request.Name;
            exhibition.Theme = request.Theme;
            exhibition.HallNumber = request.HallNumber;
            exhibition.StartDate = request.StartDate;
            exhibition.EndDate = request.EndDate;
            return true;
        }
    }

    /// <summary>Удаляет выставку вместе с её связями с экскурсиями.</summary>
    public bool Delete(int id)
    {
        lock (context)
        {
            var exhibition = context.Exhibitions.FirstOrDefault(x => x.Id == id);
            if (exhibition is null) return false;
            foreach (var relation in context.TourExhibitions.Where(x => x.ExhibitionId == id).ToList())
            {
                relation.Tour.TourExhibitions.Remove(relation);
                context.TourExhibitions.Remove(relation);
            }
            context.Exhibitions.Remove(exhibition);
            return true;
        }
    }

    /// <summary>Преобразует доменную сущность в ответ API.</summary>
    private static ExhibitionResponse ToResponse(Exhibition exhibition) => new(
        exhibition.Id, exhibition.Name, exhibition.Theme,
        exhibition.HallNumber, exhibition.StartDate, exhibition.EndDate);
}
