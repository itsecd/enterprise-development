namespace Museum.Api.Contracts;

public record TourExhibitionRequest(
    int TourId,
    int ExhibitionId
);
