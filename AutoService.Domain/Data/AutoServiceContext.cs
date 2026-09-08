using AutoService.Domain.Entities;

namespace AutoService.Domain.Data;

public class AutoServiceContext
{
    public List<Client> Clients { get; set; } = [];

    public List<Car> Cars { get; set; } = [];

    public List<Mechanic> Mechanics { get; set; } = [];

    public List<WorkType> WorkTypes { get; set; } = [];

    public List<RepairOrder> RepairOrders { get; set; } = [];
}