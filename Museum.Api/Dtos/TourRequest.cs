namespace Museum.Api.Dtos;

public record TourRequest(DateTime Date, TimeSpan StartTime, int DurationMinutes, List<int> ExhibitionIds);
