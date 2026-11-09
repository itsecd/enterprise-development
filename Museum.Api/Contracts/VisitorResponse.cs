namespace Museum.Api.Contracts;

public record VisitorResponse(
    int Id,
    string FullName,
    string Phone,
    DateOnly BirthDate
);
