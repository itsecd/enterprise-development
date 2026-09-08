namespace AutoService.Domain.Entities;

public class OrderMechanic
{
    public int Id { get; set; }


    public int RepairOrderId { get; set; }

    public RepairOrder RepairOrder { get; set; } = null!;


    public int MechanicId { get; set; }

    public Mechanic Mechanic { get; set; } = null!;
}