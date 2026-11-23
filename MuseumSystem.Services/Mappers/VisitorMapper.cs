using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Entities;

namespace MuseumSystem.Services.Mappers;

public static class VisitorMapper
{
    public static VisitorResponse ToDto(this Visitor entity) => new()
    {
        Id = entity.Id,
        FullName = entity.FullName,
        Phone = entity.Phone,
        BirthDate = entity.BirthDate
    };

    public static Visitor ToEntity(this VisitorCreateRequest request) => new()
    {
        FullName = request.FullName,
        Phone = request.Phone,
        BirthDate = request.BirthDate
    };

    public static void UpdateEntity(this Visitor entity, VisitorUpdateRequest request)
    {
        entity.FullName = request.FullName;
        entity.Phone = request.Phone;
        entity.BirthDate = request.BirthDate;
    }
}