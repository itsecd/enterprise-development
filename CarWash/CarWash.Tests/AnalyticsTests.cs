using CarWash.Domain;

namespace CarWash.Tests;

/// <summary>
/// Verifies five analytical queries against the car wash data.
/// </summary>
/// <remarks>
/// Uses the shared data set provided by <see cref="CarWashFixture"/>.
/// Tests read the fixture data and compare the results with predefined expected values.
/// </remarks>
/// <param name="fixture">The shared fixture supplied by xUnit.</param>
public class AnalyticsTests(CarWashFixture fixture) : IClassFixture<CarWashFixture>
{
    /// <summary>
    /// Verifies the top five clients, their order, and their visit counts.
    /// </summary>
    /// <remarks>
    /// Each order counts as one visit; all orders in the fixture are included.
    /// Clients are sorted by visit count in descending order, then by full name
    /// using <see cref="StringComparer.Ordinal"/>.
    /// </remarks>
    [Fact]
    public void Top5ClientsShouldReturnClientsOrderedByVisitsThenName()
    {
        // Arrange
        Client[] expectedClients =
        [
            fixture.Clients[0],
            fixture.Clients[1],
            fixture.Clients[2],
            fixture.Clients[4],
            fixture.Clients[5]
        ];

        int[] expectedVisitCounts = [3, 2, 2, 1, 1];

        // Act
        var actual = fixture.Orders
            .GroupBy(order => order.Client)
            .Select(group => new
            {
                Client = group.Key,
                VisitCount = group.Count()
            })
            .OrderByDescending(item => item.VisitCount)
            .ThenBy(item => item.Client.FullName, StringComparer.Ordinal)
            .Take(5)
            .ToArray();

        // Assert
        Assert.Equal(expectedClients, actual.Select(item => item.Client).ToArray());
        Assert.Equal(expectedVisitCounts, actual.Select(item => item.VisitCount).ToArray());
    }

    /// <summary>
    /// Verifies which vehicles are being washed at a fixed point in time.
    /// </summary>
    /// <remarks>
    /// The time being checked is specified by <see cref="CarWashFixture.ReferenceTime"/>.
    /// The service interval includes its start time and excludes its end time.
    /// The result preserves the order of the matching orders in the fixture.
    /// </remarks>
    [Fact]
    public void VehiclesOnWashNowShouldReturnVehiclesBeingWashed()
    {
        // Arrange
        var timeNow = fixture.ReferenceTime;

        Vehicle[] expectedVehicles =
        [
            fixture.Vehicles[7],
            fixture.Vehicles[4],
            fixture.Vehicles[2],
            fixture.Vehicles[3]
        ];

        // Act
        var actual = fixture.Orders
            .Where(order => order.ServiceStartTime <= timeNow
                && order.ServiceStartTime.AddMinutes(order.Service.DurationMinutes) > timeNow)
            .Select(order => order.Vehicle)
            .ToArray();

        // Assert
        Assert.Equal(expectedVehicles, actual);
    }

    /// <summary>
    /// Verifies the top five services, their order, and their usage counts.
    /// </summary>
    /// <remarks>
    /// All orders in the fixture are included. Services are sorted by usage count
    /// in descending order, then by name using <see cref="StringComparer.Ordinal"/>.
    /// Only services with at least one order are included in the ranking.
    /// </remarks>
    [Fact]
    public void Top5ServicesShouldReturnServicesOrderedByUsageThenName()
    {
        // Arrange
        Service[] expectedServices =
        [
            fixture.Services[0],
            fixture.Services[3],
            fixture.Services[5],
            fixture.Services[2],
            fixture.Services[6]
        ];

        int[] expectedUsageCounts = [3, 2, 2, 1, 1];

        // Act
        var actual = fixture.Orders
            .GroupBy(order => order.Service)
            .Select(group => new
            {
                Service = group.Key,
                UsageCount = group.Count()
            })
            .OrderByDescending(item => item.UsageCount)
            .ThenBy(item => item.Service.Name, StringComparer.Ordinal)
            .Take(5)
            .ToArray();

        // Assert
        Assert.Equal(expectedServices, actual.Select(item => item.Service).ToArray());
        Assert.Equal(expectedUsageCounts, actual.Select(item => item.UsageCount).ToArray());
    }

    /// <summary>
    /// Verifies when a wash box becomes free after all scheduled orders.
    /// </summary>
    /// <remarks>
    /// The scenario uses a box with existing orders that do not overlap in time.
    /// With this schedule, the order with the latest start time also finishes last.
    /// The test checks that order's end time, taking future bookings into account.
    /// </remarks>
    [Fact]
    public void TimeWhenBoxBecomesFreeShouldReturnLastScheduledEndTime()
    {
        // Arrange
        var boxNumber = 1;
        DateTime expectedTime = new(2026, 9, 14, 11, 30, 0);

        // Act
        var actual = fixture.Orders
            .Where(order => order.BoxNumber == boxNumber)
            .OrderByDescending(order => order.ServiceStartTime)
            .Select(order => order.ServiceStartTime.AddMinutes(order.Service.DurationMinutes))
            .FirstOrDefault();

        // Assert
        Assert.Equal(expectedTime, actual);
    }

    /// <summary>
    /// Verifies the total revenue for each service, including services with no orders.
    /// </summary>
    /// <remarks>
    /// Revenue is calculated across all orders using the service prices in the fixture.
    /// Services with no orders are expected to have zero revenue. The totals follow
    /// the order of the items in <see cref="CarWashFixture.Services"/>.
    /// </remarks>
    [Fact]
    public void TotalRevenueForEachServiceShouldIncludeServicesWithoutOrders()
    {
        // Arrange
        decimal[] expectedRevenues =
        [
            1800m,
            800m,
            1500m,
            2400m,
            0m,
            800m,
            700m,
            0m,
            0m,
            0m
        ];

        // Act
        var actual = fixture.Services
            .Select(currentService => fixture.Orders
                .Where(order => order.Service == currentService)
                .Sum(order => order.Service.Price))
            .ToArray();

        // Assert
        Assert.Equal(expectedRevenues, actual);
    }
}
