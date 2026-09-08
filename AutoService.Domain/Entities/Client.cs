using System.Runtime.ConstrainedExecution;

namespace AutoService.Domain.Entities;

public class Client
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Phone { get; set; } = string.Empty;

    public List<Car> Cars { get; set; } = [];

    public List<RepairOrder> Orders { get; set; } = [];
}