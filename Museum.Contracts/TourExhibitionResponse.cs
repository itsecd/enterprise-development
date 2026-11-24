namespace Museum.Contracts;

/// <summary>
/// Ответ с данными связи экскурсии с выставкой.
/// </summary>
/// <param name="TourId">Идентификатор экскурсии.</param>
/// <param name="ExhibitionId">Идентификатор выставки.</param>
public record TourExhibitionResponse(
    int TourId,
    int ExhibitionId
);
