using TransportCompany.Domain.Enums;

namespace TransportCompany.Domain.Entities;

public class Trip
{ 
    public int Id { get; set; }
    public int DriverId { get; set; }
    public int VehicleId { get; set; }
    public int ClientId { get; set; }
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public double CargoWeight { get; set; }
    public decimal TransportationCost { get; set; }
    public TripStatus Status { get; set; }
    public Driver? Driver { get; set; }
    public Vehicle? Vehicle { get; set; }
    public Client? Client { get; set; }
}