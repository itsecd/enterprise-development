using DispatchService.Model;

namespace DispatchService.Tests;

public class DispatchServiceTests(DispatchFixture fixture) : IClassFixture<DispatchFixture>
{
    private readonly DispatchFixture _fixture = fixture;

    [Fact]
    public void TestSpecificTransport()
    {
        var id = 1;
        var specificTransport = _fixture.TestData.Transports.FirstOrDefault(t => t.Id == id);
        Assert.NotNull(specificTransport);
        Assert.Equal("Volvo 7900", specificTransport.ModelName);
    }

    [Fact]
    public void TestDriversInPeriod()
    {
        var startDate = new DateTime(2024, 9, 14, 8, 0, 0);
        var endDate = new DateTime(2024, 9, 14, 12, 0, 0);

        var driversInPeriod = _fixture.TestData.Routes
            .Where(r => r.StartTime >= startDate && r.EndTime <= endDate)
            .Select(r => r.AssignedDriver)
            .Distinct()
            .OrderBy(d => d.FullName)
            .ToList();

        Assert.NotEmpty(driversInPeriod);
        Assert.Equal(2, driversInPeriod.Count);
    }

    [Fact]
    public void TestTotalTripTimesByTransport()
    {
        var expectedTripTimes = new []
        {
            new { TransportType = Transport.VehicleType.Bus, Model = "Volvo 7900", TotalTime = new TimeSpan(0, 2, 0, 0) },
            new { TransportType = Transport.VehicleType.Trolleybus, Model = "Trolza", TotalTime = new TimeSpan(0, 2, 0, 0) },
            new { TransportType = Transport.VehicleType.Tram, Model = "Stadler", TotalTime = new TimeSpan(0, 2, 0, 0) },
            new { TransportType = Transport.VehicleType.Bus, Model = "Mercedes Citaro", TotalTime = new TimeSpan(0, 2, 0, 0) },
            new { TransportType = Transport.VehicleType.Trolleybus, Model = "BKM 321", TotalTime = new TimeSpan(0, 7, 0, 0) },
            new { TransportType = Transport.VehicleType.Tram, Model = "CAF Urbos", TotalTime = new TimeSpan(0, 7, 0, 0) },
            new { TransportType = Transport.VehicleType.Bus, Model = "MAN Lion's City", TotalTime = new TimeSpan(0, 3, 0, 0) },
            new { TransportType = Transport.VehicleType.Trolleybus, Model = "ZIU-9", TotalTime = new TimeSpan(0, 9, 0, 0) }
        };

        var totalTripTimes = _fixture.TestData.Routes
            .GroupBy(r => new { r.AssignedTransport.Type, r.AssignedTransport.ModelName })
            .Select(g => new
            {
                TransportType = g.Key.Type,
                Model = g.Key.ModelName,
                TotalTime = TimeSpan.FromSeconds(g.Sum(r => (r.EndTime - r.StartTime).TotalSeconds))
            })
            .ToList();

        Assert.Equal(expectedTripTimes, totalTripTimes);
    }

    [Fact]
    public void TestTopDriversByTripCount()
    {
        var expectedId = 5;
        var topDrivers = _fixture.TestData.Routes
            .GroupBy(r => r.AssignedDriver)
            .Select(g => new
            {
                Driver = g.Key,
                TripCount = g.Count()
            })
            .OrderByDescending(d => d.TripCount)
            .Take(1)
            .ToList();

        Assert.Equal(expectedId, topDrivers[0].Driver.Id);
    }

    [Fact]
    public void TestDriverTripStats()
    {
        var expectedDriverTripStats = new List<(Driver Driver, int TripCount, TimeSpan AverageTime, TimeSpan MaxTime)>
        {
            (_fixture.TestData.Drivers[4], 2, TimeSpan.FromSeconds(12600), TimeSpan.FromSeconds(14400)),
            (_fixture.TestData.Drivers[0], 1, TimeSpan.FromSeconds(7200), TimeSpan.FromSeconds(7200)),
            (_fixture.TestData.Drivers[1], 1, TimeSpan.FromSeconds(7200), TimeSpan.FromSeconds(7200))
        };
        var driverTripStats = _fixture.TestData.Routes
        .GroupBy(r => r.AssignedDriver)
        .Select(g => new
        {
            Driver = g.Key,
            TripCount = g.Count(),
            AverageTime = TimeSpan.FromSeconds(g.Average(r => (r.EndTime - r.StartTime).TotalSeconds)),
            MaxTime = TimeSpan.FromSeconds(g.Max(r => (r.EndTime - r.StartTime).TotalSeconds))
        })
        .ToList();

        Assert.NotEmpty(driverTripStats);
        Assert.Equal(expectedDriverTripStats, expectedDriverTripStats.ToArray());
    }

    [Fact]
    public void TestTopTransportsByTripCount()
    {
        var startDate = new DateTime(2024, 9, 15, 7, 0, 0);
        var endDate = new DateTime(2024, 9, 15, 23, 0, 0);

        var expectedTopTransports = new[] 
        {
            new { Transport = _fixture.TestData.Transports[4], TripCount = 2 }
        };

        var transportTripCounts = _fixture.TestData.Routes
            .Where(r => r.StartTime >= startDate && r.EndTime <= endDate)
            .GroupBy(r => r.AssignedTransport)
            .Select(g => new
            {
                Transport = g.Key,
                TripCount = g.Count()
            })
            .ToList();

        var maxTripCount = transportTripCounts.Max(t => t.TripCount);

        var topTransports = transportTripCounts
            .Where(t => t.TripCount == maxTripCount)
            .ToList();

        Assert.NotEmpty(topTransports);
        Assert.Equal(expectedTopTransports, topTransports);    
    }
}