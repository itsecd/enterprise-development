using System.Runtime.CompilerServices;
using TransportCompany.Domain.Enums;

namespace TransportCompany.Domain.Entitites;

public class Trip
{ 
    public int Id { get; set; }
    public int DriverId { get; set; }
    public Vehicleint VehicleId { get; set; }
    public Clientint ClientId { get; set; }
    public string Origin { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime Date { get; set; }
    public int CargoWeight { get; set; }
    public int TransportationCost { get; set; }
    public TripStatus Status { get; set; }
    public Driver? Driver { get; set; }
    public Vehicle? Vehicle { get; set; }
    public Client? Client { get; set; }
}