using DispatchService.Model;
namespace DispatchService.Tests;

public class TestData
{
    public required List<Driver> Drivers { get; set; }
    public required List<Transport> Transports { get; set; }
    public required List<Route> Routes { get; set; }
}
