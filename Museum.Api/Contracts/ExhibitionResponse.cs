using Museum.Domain.Enums;

namespace Museum.Api.Contracts;

public record ExhibitionResponse(
    int Id,
    string Name,
    ExhibitionTheme Theme,
    int HallNumber,
    DateOnly StartDate,
    DateOnly EndDate
);
