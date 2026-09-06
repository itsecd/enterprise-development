namespace TransportCompany.Domain.Entitites;

public class Vehicle
{ 
    public int Id { get; set; }
    public string LicensePlate { get; set; }
    public double LoadCapacity { get; set; }
    public int ModelId { get; set; }
    public VehicleModel? Model { get; set; }
}