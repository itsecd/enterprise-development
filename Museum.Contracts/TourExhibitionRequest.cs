namespace Museum.Contracts;

/// <summary>
/// Данные для создания или изменения связи экскурсии с выставкой.
/// </summary>
/// <param name="TourId">Идентификатор экскурсии.</param>
/// <param name="ExhibitionId">Идентификатор выставки.</param>
public record TourExhibitionRequest(
    int TourId,
    int ExhibitionId
);
