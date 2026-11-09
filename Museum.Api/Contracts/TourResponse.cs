namespace Museum.Api.Contracts;

public record TourResponse(
    int Id,
    DateTime StartDateTime,
    TimeSpan Duration,
    int[] ExhibitionIds
);
