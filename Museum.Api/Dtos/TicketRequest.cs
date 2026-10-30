using Museum.Domain;

namespace Museum.Api.Dtos;

public record TicketRequest(int TourId, int VisitorId, TicketType Type, decimal Price);
