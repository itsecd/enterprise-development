using Museum.Domain.Enums;

namespace Museum.Api.Contracts;

public record ExhibitionVisitorsResponse(
    int ExhibitionId,
    string Name,
    int VisitorCount
);

public record TourParticipantsResponse(
    int TourId,
    int ParticipantCount
);

public record ThemeAttendanceResponse(
    ExhibitionTheme Theme,
    int TotalVisitors,
    decimal TotalTicketCost,
    double AverageVisitorsPerDay,
    int MinVisitorsPerDay,
    int MaxVisitorsPerDay
);

public record HallTourResponse(
    int TourId,
    DateTime StartDateTime,
    TimeSpan Duration
);

public record TourVisitorResponse(
    int VisitorId,
    string FullName,
    string Phone,
    DateOnly BirthDate
);
