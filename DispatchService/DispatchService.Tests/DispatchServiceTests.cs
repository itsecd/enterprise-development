namespace DispatchService.Tests;

public class DispatchServiceTests(DispatchFixture fixture) : IClassFixture<DispatchFixture>
{
    private readonly DispatchFixture _fixture = fixture;

    [Fact]
    public void TestSpecificTransport()
    {
        var specificTransport = _fixture.TestData.Transports.FirstOrDefault(t => t.LicensePlate == "A123BC");
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
        var totalTripTimes = _fixture.TestData.Routes
            .GroupBy(r => new { r.AssignedTransport.Type, r.AssignedTransport.ModelName })
            .Select(g => new
            {
                TransportType = g.Key.Type,
                Model = g.Key.ModelName,
                TotalTime = g.Sum(r => (r.EndTime - r.StartTime).TotalHours)
            })
            .ToList();

        Assert.NotEmpty(totalTripTimes);
    }

    [Fact]
    public void TestTopDriversByTripCount()
    {
        var topDrivers = _fixture.TestData.Routes
            .GroupBy(r => r.AssignedDriver)
            .Select(g => new
            {
                Driver = g.Key,
                TripCount = g.Count()
            })
            .OrderByDescending(d => d.TripCount)
            .Take(5)
            .ToList();

        Assert.Equal(5, topDrivers.Count);
    }

    [Fact]
    public void TestDriverTripStats()
    {
        var driverTripStats = _fixture.TestData.Routes
            .GroupBy(r => r.AssignedDriver)
            .Select(g => new
            {
                Driver = g.Key,
                TripCount = g.Count(),
                AverageTime = g.Average(r => (r.EndTime - r.StartTime).TotalHours),
                MaxTime = g.Max(r => (r.EndTime - r.StartTime).TotalHours)
            })
            .ToList();

        Assert.NotEmpty(driverTripStats);
    }

    [Fact]
    public void TestTopTransportsByTripCount()
    {
        var startDate = new DateTime(2024, 9, 15, 8, 0, 0);
        var endDate = new DateTime(2024, 9, 15, 23, 0, 0);

        var topTransports = _fixture.TestData.Routes
            .Where(r => r.StartTime >= startDate && r.EndTime <= endDate)
            .GroupBy(r => r.AssignedTransport)
            .Select(g => new
            {
                Transport = g.Key,
                TripCount = g.Count()
            })
            .OrderByDescending(t => t.TripCount)
            .Take(1)
            .ToList();

        Assert.Single(topTransports);
    }
}