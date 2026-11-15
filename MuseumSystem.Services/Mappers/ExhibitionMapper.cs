using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Entities;

namespace MuseumSystem.Services.Mappers;

public static class ExhibitionMapper
{
    public static ExhibitionResponse ToDto(this Exhibition entity) => new()
    {
        Id = entity.Id,
        Name = entity.Name,
        Theme = entity.Theme,
        HallNumber = entity.HallNumber,
        StartDate = entity.StartDate,
        EndDate = entity.EndDate
    };

    public static Exhibition ToEntity(this ExhibitionCreateRequest request) => new()
    {
        Name = request.Name,
        Theme = request.Theme,
        HallNumber = request.HallNumber,
        StartDate = request.StartDate,
        EndDate = request.EndDate
    };

    public static void UpdateEntity(this Exhibition entity, ExhibitionUpdateRequest request)
    {
        entity.Name = request.Name;
        entity.Theme = request.Theme;
        entity.HallNumber = request.HallNumber;
        entity.StartDate = request.StartDate;
        entity.EndDate = request.EndDate;
    }
}