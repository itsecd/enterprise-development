namespace Museum.Api.Contracts;

public class TourRequest
{
    public DateTime StartDateTime { get; set; }

    public TimeSpan Duration { get; set; }
}
