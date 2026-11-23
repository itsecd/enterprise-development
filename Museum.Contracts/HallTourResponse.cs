namespace Museum.Contracts;

/// <summary>
/// Данные об экскурсии в выбранном зале.
/// </summary>
/// <param name="TourId">Идентификатор экскурсии.</param>
/// <param name="StartDateTime">Дата и время начала экскурсии.</param>
/// <param name="Duration">Продолжительность экскурсии.</param>
public record HallTourResponse(
    int TourId,
    DateTime StartDateTime,
    TimeSpan Duration
);
