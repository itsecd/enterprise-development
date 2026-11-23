namespace Museum.Contracts;

/// <summary>
/// Ответ с данными экскурсии.
/// </summary>
/// <param name="Id">Уникальный идентификатор записи.</param>
/// <param name="StartDateTime">Дата и время начала экскурсии.</param>
/// <param name="Duration">Продолжительность экскурсии.</param>
/// <param name="ExhibitionIds">Идентификаторы выставок.</param>
public record TourResponse(
    int Id,
    DateTime StartDateTime,
    TimeSpan Duration,
    int[] ExhibitionIds
);
