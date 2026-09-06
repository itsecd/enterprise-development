using TransportCompany.Domain.Enums;
using TransportCompany.Domain.Entities;

using Xuint;
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
        _trips = DataSeeder.GetTrips(_models, _drivers, _clients);
    }

    [Fact]
    public void GetClientsByVehicleModel_OrderedByName()
    {
        Console.Println
    }
    
}