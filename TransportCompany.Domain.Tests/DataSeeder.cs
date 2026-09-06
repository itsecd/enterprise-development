using System.ComponentModel;
using System.Runtime.CompilerServices;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Enums;

namespace TransportCompany.Domain.Tests;

public static class DataSeeder
{
    public static List<VehicleModel> GetVehicleModels() => new()
    {
        new VehicleModel { BodyType = BodyType.Sedan, BodyVolume = 82 },
        new VehicleModel { BodyType = BodyType.Hatchback, BodyVolume = 90 }
    };

    public static List<Client> GetClients() => new()
    {
        new Client { Name = "Donald Truck", Phone = "88005553535" },
        new Client { Name = "Cat Dog", Phone = "89991112233" }
    };

    public static List<Driver> GetDrivers() => new()
    {
        new Driver { PassportNumber = "1234 567890", FullName = "Khabib Nurmagomedov", Experience = 12, License = DrivingLicence.C },
        new Driver { PassportNumber = "1234 567890", FullName = "John Johnson", Experience = 5, License = DrivingLicence.C }
    };

    public static List<Vehicle> GetVehicles(List<VehicleModels> models) => new()
    {
        new Vehicle { LicensePlate = "X005XX05", LoadCapacity = 20.0, Model = models[0] },
        new Vehicle { LicensePlate = "E001MP777", LoadCapacity = 15.0, Model = models[1] }
    };

    public static List<Trip> GetTrips(List<VehicleModels> models, List<Driver> drivers, List<Client> clients) => new()
    {
        new Trip { Driver = drivers[0], Vehicle = vehicles[0], Client = clients[0], Date = DateTime.Now.AddDays(-20), CargoWeight = 15.0, TransportationCost = 15000m, Status = TripStatus.InTransit},
        new Trip { Driver = drivers[0], Vehicle = vehicles[0], Client = clients[0], Date = DateTime.Now.AddDays(-20), CargoWeight = 15.0, TransportationCost = 15000m, Status = TripStatus.InTransit}
    };
}
