namespace Museum.Contracts;

/// <summary>
/// Данные о числе участников экскурсии.
/// </summary>
/// <param name="TourId">Идентификатор экскурсии.</param>
/// <param name="ParticipantCount">Количество участников.</param>
public record TourParticipantsResponse(
    int TourId,
    int ParticipantCount
);
