namespace AutoService.Domain.Entities;

public class OrderWork
{
    public int Id { get; set; }


    public int RepairOrderId { get; set; }

    public RepairOrder RepairOrder { get; set; } = null!;


    public int WorkTypeId { get; set; }

    public WorkType WorkType { get; set; } = null!;
}