namespace TaxiCompany.Domain;

public class Car
{
    public required int Id { get; set; }

    public required string Colour { get; set; }

    public required string Model { get; set; }

    public required string SerialNumber { get; set; }

    public required DateTime RealeseYear { get; set; }

    public required int AssignedDriverId { get; set; }
}
