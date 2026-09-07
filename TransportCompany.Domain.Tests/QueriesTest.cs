using TransportCompany.Domain.Enums;
using TransportCompany.Domain.Entities;

using Xunit;
using System.IO.Compression;

namespace TransportCompany.Domain.Tests;

public class QueriesTest
{
    private readonly List<VehicleModel> _models;
    private readonly List<Client> _clients;
    private readonly List<Driver> _drivers;
    private readonly List<Vehicle> _vehicles;
    private readonly List<Trip> _trips;
    
    public QueriesTest()
    {
        _models = DataSeeder.GetVehicleModels();
        _clients = DataSeeder.GetClients();
        _drivers = DataSeeder.GetDrivers();
        _vehicles = DataSeeder.GetVehicles(_models);
        _trips = DataSeeder.GetTrips(_vehicles, _drivers, _clients);
    }

    [Fact]
    public void GetClientsByVehicleModel_OrderedByName()
    {
        /*
            1, 4, 6, 7 - Client IDs
            Cat Dog, Michael Brown, David Miller, Lisa Davis - Clients

            Sort by:
            1 - Cat Dog
            2 - David Miller
        */
        var targetModelId = 1;

        var resultClients = _trips
            .Where(t => t.Vehicle?.ModelId == targetModelId)
            .Select(t => t.Client)
            .Distinct()
            .OrderBy(c => c?.Name)
            .ToList();
    
        Assert.NotEmpty(resultClients);
        Assert.Equal("Cat Dog", resultClients[0]?.Name);
        Assert.Equal("David Miller", resultClients[1]?.Name);
    }
    
    [Fact]
    public void GetVehiclesByTripStatus()
    {
        var expectedIds = new HashSet<int> {0, 4, 8, 9};

        var tripStatus = TripStatus.InTransit;

        var resultVehicles = _trips
            .Where(t => t.Status == tripStatus)
            .Select(t => t.Id)
            .ToHashSet();
        
        Assert.All(expectedIds, (id) => resultVehicles.Contains(id));
    }

    [Fact]
    public void GetTripCountByVehicle()
    {
        var vehicleTripCounts = _vehicles
            .Select(v => new
            {
                Vehicle = v,
                TripCount = _trips.Count(t => t.VehicleId == v.Id)
            })
            .ToList();
        
        Console.WriteLine(vehicleTripCounts);

        Assert.Equal(10, vehicleTripCounts.Count);

        var vehicle6Stats = vehicleTripCounts.First(v => v.Vehicle.Id == 6);
        Assert.Equal(3, vehicle6Stats.TripCount);

        var vehicle8Stats = vehicleTripCounts.First(v => v.Vehicle.Id == 8);
        Assert.Equal(0, vehicle8Stats.TripCount);
    }

    [Fact]
    public void GetTopFiveDriversByTripCount()
    {
        var topDrivers = _trips
            .GroupBy(t => t.Driver)
            .OrderByDescending(g => g.Count())
            .Take(5)
            .Select(g => new { Driver = g.Key, TripCount = g.Count() })
            .ToList();
        
        Assert.True(topDrivers.Count() <= 5);

        Assert.Equal(3, topDrivers.First().Driver?.Id); // Водитель с ID 3 - топ
        Assert.Equal(3, topDrivers.First().TripCount); // и он сделал 3 рейса
    }

    [Fact]
    public void GetTopFiveClientsByTransportationCost()
    {
        var topClients = _trips
            .GroupBy(t => t.Client)
            .OrderByDescending(g => g.Sum(t => t.TransportationCost))
            .Take(5)
            .Select(g => new { Client = g.Key, TotalCost = g.Sum(t => t.TransportationCost) })
            .ToList();
        
        Assert.True(topClients.Count() <= 5);

        Assert.Equal(4, topClients.First().Client?.Id);
        Assert.Equal(22000m, topClients.First().TotalCost);
    }
}