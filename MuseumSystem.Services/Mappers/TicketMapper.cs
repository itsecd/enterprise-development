using MuseumSystem.Contracts.Models;
using MuseumSystem.Domain.Entities;

namespace MuseumSystem.Services.Mappers;

public static class TicketMapper
{
    public static TicketResponse ToDto(this Ticket entity) => new()
    {
        Id = entity.Id,
        ExcursionId = entity.Excursion.Id,
        VisitorId = entity.Visitor.Id,
        Type = entity.Type,
        Price = entity.Price
    };

    public static Ticket ToEntity(this TicketCreateRequest request, Excursion excursion, Visitor visitor) => new()
    {
        Excursion = excursion,
        Visitor = visitor,
        Type = request.Type,
        Price = request.Price
    };

    public static void UpdateEntity(this Ticket entity, TicketUpdateRequest request, Excursion excursion, Visitor visitor)
    {
        entity.Excursion = excursion;
        entity.Visitor = visitor;
        entity.Type = request.Type;
        entity.Price = request.Price;
    }
}