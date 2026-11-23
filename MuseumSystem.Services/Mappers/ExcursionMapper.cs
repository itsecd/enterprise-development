using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Entities;

namespace MuseumSystem.Services.Mappers;

public static class ExcursionMapper
{
    public static ExcursionResponse ToDto(this Excursion entity) => new()
    {
        Id = entity.Id,
        StartsAt = entity.StartsAt,
        Duration = TimeSpan.FromMinutes(entity.DurationMinutes),
        ExhibitionIds = entity.Exhibitions.Select(e => e.Id).ToList()
    };

    public static Excursion ToEntity(this ExcursionCreateRequest request, List<Exhibition> exhibitions) => new()
    {
        StartsAt = request.StartsAt,
        DurationMinutes = (int)request.Duration.TotalMinutes,
        Exhibitions = exhibitions
    };

    public static void UpdateEntity(this Excursion entity, ExcursionUpdateRequest request, List<Exhibition> exhibitions)
    {
        entity.StartsAt = request.StartsAt;
        entity.DurationMinutes = (int)request.Duration.TotalMinutes;
        entity.Exhibitions = exhibitions;
    }
}