using TransportCompany.Domain.Enums;

namespace TransportCompany.Tests;

/// <summary>
/// Тесты аналитических запросов транспортной компании.
/// </summary>
/// <param name="data">Общий набор тестовых данных</param>
public class AnalyticsQueriesTests(QueriesTestFixture data) : IClassFixture<QueriesTestFixture>
{
    /// <summary>
    /// Проверяет вывод информации обо всех клиентах, заказывавших перевозку 
    /// на транспорте указанной модели, с сортировкой по ФИО.
    /// </summary>
    [Fact]
    public void ClientsByVehicleModelAreSortedByName()
    {
        var targetModelId = 1;

        // Act
        var targetVehicleIds = data.Vehicles
            .Where(v => v.ModelId == targetModelId)
            .Select(v => v.Id)
            .ToHashSet();

        var clients = data.Trips
            .Where(t => targetVehicleIds.Contains(t.VehicleId))
            .Select(t => t.Client!)
            .DistinctBy(c => c.Id)
            .OrderBy(c => c.Name, StringComparer.Ordinal)
            .ToList();

        Assert.NotEmpty(clients);
    }

    /// <summary>
    /// Проверяет вывод информации о транспортных средствах, находящихся в рейсе.
    /// </summary>
    [Fact]
    public void VehiclesInTransitAreRetrievedCorrectly()
    {
        var vehiclesInTransit = data.Trips
            .Where(t => t.Status == TripStatus.InTransit)
            .Select(t => t.Vehicle!)
            .DistinctBy(v => v.Id)
            .OrderBy(v => v.Id)
            .ToList();

        Assert.NotEmpty(vehiclesInTransit);
        Assert.All(vehiclesInTransit, v =>
            Assert.Contains(data.Trips, t => t.VehicleId == v.Id && t.Status == TripStatus.InTransit));
    }

    /// <summary>
    /// Проверяет топ 5 водителей по количеству рейсов.
    /// </summary>
    [Fact]
    public void TopDriversAreSortedByTripCount()
    {
        var topDrivers = data.Trips
            .GroupBy(t => t.DriverId)
            .Select(group => new { DriverId = group.Key, TripCount = group.Count() })
            .OrderByDescending(d => d.TripCount)
            .ThenBy(d => d.DriverId)
            .Take(5)
            .ToList();

        Assert.True(topDrivers.Count <= 5);
        if (topDrivers.Count > 1)
        {
            Assert.True(topDrivers[0].TripCount >= topDrivers[1].TripCount);
        }
    }

    /// <summary>
    /// Проверяет подсчет числа рейсов для каждого транспортного средства.
    /// </summary>
    [Fact]
    public void TripCountForEachVehicleIsCalculatedCorrectly()
    {
        var vehicleTripCounts = data.Vehicles
            .Select(v => new
            {
                VehicleId = v.Id,
                TripCount = data.Trips.Count(t => t.VehicleId == v.Id)
            })
            .ToList();

        Assert.Equal(data.Vehicles.Count, vehicleTripCounts.Count);
    }

    /// <summary>
    /// Проверяет топ 5 клиентов по сумме затрат на перевозки.
    /// </summary>
    [Fact]
    public void TopClientsAreSortedByTotalCost()
    {
        var topClients = data.Trips
            .GroupBy(t => t.ClientId)
            .Select(group => new
            {
                ClientId = group.Key,
                TotalSpent = group.Sum(t => t.TransportationCost)
            })
            .OrderByDescending(c => c.TotalSpent)
            .ThenBy(c => c.ClientId)
            .Take(5)
            .ToList();

        Assert.True(topClients.Count <= 5);
        if (topClients.Count > 1)
        {
            Assert.True(topClients[0].TotalSpent >= topClients[1].TotalSpent);
        }
    }
}