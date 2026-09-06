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
        int targetModelId = 1;

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
    
}