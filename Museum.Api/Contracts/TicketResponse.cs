using Museum.Domain.Enums;

namespace Museum.Api.Contracts;

public record TicketResponse(
    int Id,
    int TourId,
    int VisitorId,
    TicketType Type,
    decimal Price
);
