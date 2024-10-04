namespace TaxiCompany.Domain;

public class Trip
{
    public required int Id { get; set; }

    public required string Departure { get; set; }

    public required string Destination { get; set; }

    public required DateTime Date { get; set; }

    public TimeOnly DrivingTime { get; set; }

    public required decimal Cost { get; set; }

    public required int AssignedClientId { get; set; }

    public required int AssignedCarId { get; set; }
}
