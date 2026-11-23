using Museum.Domain.Enums;

namespace Museum.Contracts;

/// <summary>
/// Ответ с данными билета.
/// </summary>
/// <param name="Id">Уникальный идентификатор записи.</param>
/// <param name="TourId">Идентификатор экскурсии.</param>
/// <param name="VisitorId">Идентификатор посетителя.</param>
/// <param name="Type">Тип билета.</param>
/// <param name="Price">Стоимость билета.</param>
public record TicketResponse(
    int Id,
    int TourId,
    int VisitorId,
    TicketType Type,
    decimal Price
);
