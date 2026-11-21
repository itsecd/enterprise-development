using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Enums;

namespace TransportCompany.Tests;

/// <summary>
/// Тесты запросов к данным транспортной компании
/// </summary>
/// <param name="fixture">Тестовый набор данных</param>
public class QueriesTest(QueriesTestFixture fixture) : IClassFixture<QueriesTestFixture>
{
    private readonly List<Vehicle> _vehicles = fixture.Vehicles;
    private readonly List<Trip> _trips = fixture.Trips;

    /// <summary>
    /// Проверяет получение клиентов по модели транспортного средства с сортировкой по имени
    /// </summary>
    [Fact]
    public void GetClientsByVehicleModel_OrderedByName()
    {
        var targetModelId = 2;
        var expectedClientNames = new[] { "Cat Dog", "David Miller" };

        var resultClients = _trips
            .Where(t => t.Vehicle?.ModelId == targetModelId)
            .Select(t => t.Client)
            .Distinct()
            .OrderBy(c => c?.Name)
            .ToList();

        Assert.NotEmpty(resultClients);
        Assert.Equal(expectedClientNames[0], resultClients[0]?.Name);
        Assert.Equal(expectedClientNames[1], resultClients[1]?.Name);
    }

    /// <summary>
    /// Проверяет получение идентификаторов транспортных средств по статусу рейса
    /// </summary>
    [Fact]
    public void GetVehiclesByTripStatus()
    {
        var tripStatus = TripStatus.InTransit;
        var expectedVehicleIds = new HashSet<int> { 1, 5, 9, 10 };

        var resultVehicleIds = _trips
            .Where(t => t.Status == tripStatus)
            .Select(t => t.Id)
            .ToHashSet();

        Assert.Equal(expectedVehicleIds, resultVehicleIds);
    }

    /// <summary>
    /// Проверяет подсчёт количества рейсов для каждого транспортного средства
    /// </summary>
    [Fact]
    public void GetTripCountByVehicle()
    {
        var expectedVehicleCount = 10;
        var vehicle6Id = 6;
        var expectedVehicle6TripCount = 1;
        var vehicle8Id = 8;
        var expectedVehicle8TripCount = 0;

        var vehicleTripCounts = _vehicles
            .Select(v => new
            {
                Vehicle = v,
                TripCount = _trips.Count(t => t.VehicleId == v.Id)
            })
            .ToList();

        Assert.Equal(expectedVehicleCount, vehicleTripCounts.Count);

        var vehicle6Stats = vehicleTripCounts.First(v => v.Vehicle.Id == vehicle6Id);
        Assert.Equal(expectedVehicle6TripCount, vehicle6Stats.TripCount);

        var vehicle8Stats = vehicleTripCounts.First(v => v.Vehicle.Id == vehicle8Id);
        Assert.Equal(expectedVehicle8TripCount, vehicle8Stats.TripCount);
    }

    /// <summary>
    /// Проверяет получение топ-5 водителей по количеству рейсов
    /// </summary>
    [Fact]
    public void GetTopFiveDriversByTripCount()
    {
        var expectedTopDriverId = 4;
        var expectedTopDriverTripCount = 3;

        var topDrivers = _trips
            .GroupBy(t => t.Driver)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => new { Driver = g.Key, TripCount = g.Count() })
            .ToList();

        Assert.Equal(expectedTopDriverId, topDrivers.First().Driver?.Id);
        Assert.Equal(expectedTopDriverTripCount, topDrivers.First().TripCount);
    }

    /// <summary>
    /// Проверяет получение топ-5 клиентов по суммарной стоимости перевозок
    /// </summary>
    [Fact]
    public void GetTopFiveClientsByTransportationCost()
    {
        var expectedTopClientId = 5;
        var expectedTopClientTotalCost = 22000m;

        var topClients = _trips
            .GroupBy(t => t.Client)
            .OrderByDescending(g => g.Sum(t => t.TransportationCost))
            .Take(5)
            .Select(g => new { Client = g.Key, TotalCost = g.Sum(t => t.TransportationCost) })
            .ToList();

        Assert.Equal(expectedTopClientId, topClients.First().Client?.Id);
        Assert.Equal(expectedTopClientTotalCost, topClients.First().TotalCost);
    }
}