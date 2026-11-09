using Museum.Domain.Enums;

namespace Museum.Api.Contracts;

public record TicketRequest(
    int TourId,
    int VisitorId,
    TicketType Type,
    decimal Price
);
