using Museum.Domain.Enums;

namespace Museum.Contracts;

/// <summary>
/// Данные для создания или изменения билета.
/// </summary>
/// <param name="TourId">Идентификатор экскурсии.</param>
/// <param name="VisitorId">Идентификатор посетителя.</param>
/// <param name="Type">Тип билета.</param>
/// <param name="Price">Стоимость билета.</param>
public record TicketRequest(
    int TourId,
    int VisitorId,
    TicketType Type,
    decimal Price
);
