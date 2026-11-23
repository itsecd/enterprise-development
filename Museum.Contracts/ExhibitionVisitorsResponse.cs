namespace Museum.Contracts;

/// <summary>
/// Данные о числе посетителей выставки.
/// </summary>
/// <param name="ExhibitionId">Идентификатор выставки.</param>
/// <param name="Name">Название выставки.</param>
/// <param name="VisitorCount">Количество уникальных посетителей.</param>
public record ExhibitionVisitorsResponse(
    int ExhibitionId,
    string Name,
    int VisitorCount
);
