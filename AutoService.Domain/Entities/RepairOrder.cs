namespace AutoService.Domain.Entities;

public class RepairOrder
{
    public int Id { get; set; }


    public int ClientId { get; set; }

    public Client Client { get; set; } = null!;


    public int CarId { get; set; }

    public Car Car { get; set; } = null!;


    public DateTime AdmissionDate { get; set; }

    public DateTime? ReleaseDate { get; set; }


    public List<OrderMechanic> Mechanics { get; set; } = [];

    public List<OrderWork> Works { get; set; } = [];
}