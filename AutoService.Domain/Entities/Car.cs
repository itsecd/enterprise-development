namespace AutoService.Domain.Entities;

public class Car
{
    public int Id { get; set; }

    public string LicensePlate { get; set; } = string.Empty;

    public string Brand { get; set; } = string.Empty;

    public string Model { get; set; } = string.Empty;

    public int Year { get; set; }


    public int ClientId { get; set; }

    public Client Client { get; set; } = null!;


    public List<RepairOrder> Orders { get; set; } = [];
}