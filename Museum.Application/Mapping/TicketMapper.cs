using Museum.Contracts.Dto;
using Museum.Domain;

namespace Museum.Application.Mapping;

/// <summary>
/// Преобразует данные билетов в DTO и обратно
/// </summary>
public static class TicketMapper
{
    /// <summary>
    /// Преобразует билет в DTO
    /// </summary>
    public static TicketDto ToDto(Ticket ticket)
    {
        return new TicketDto
        {
            Id = ticket.Id,
            TourId = ticket.Tour.Id,
            VisitorId = ticket.Visitor.Id,
            Type = ticket.Type,
            Price = ticket.Price
        };
    }

    /// <summary>
    /// Создаёт билет с выбранными экскурсией и посетителем
    /// </summary>
    public static Ticket ToEntity(
        TicketEditDto dto,
        Tour tour,
        Visitor visitor)
    {
        return new Ticket
        {
            Tour = tour,
            Visitor = visitor,
            Type = dto.Type,
            Price = dto.Price
        };
    }
}
