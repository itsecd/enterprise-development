using AutoService.Domain.Data;
using AutoService.Domain.Models;

namespace AutoService.Tests;

public class AutoServiceFixture
{
    public List<Client> Clients { get; } = DataSeed.Clients;
    public List<Car> Cars { get; } = DataSeed.Cars;
    public List<Mechanic> Mechanics { get; } = DataSeed.Mechanics;
    public List<WorkType> Works { get; } = DataSeed.Works;
    public List<Order> Orders { get; } = DataSeed.Orders;
}