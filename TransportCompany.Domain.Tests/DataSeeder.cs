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
        new VehicleModel { BodyType = BodyType.Hatchback, BodyVolume = 90 },
        new VehicleModel { BodyType = BodyType.Coupe, BodyVolume = 75 },
        new VehicleModel { BodyType = BodyType.Cabriolet, BodyVolume = 70 },
        new VehicleModel { BodyType = BodyType.SUV, BodyVolume = 150 },
        new VehicleModel { BodyType = BodyType.Crossover, BodyVolume = 120 },
        new VehicleModel { BodyType = BodyType.StationWagon, BodyVolume = 110 },
        new VehicleModel { BodyType = BodyType.Minivan, BodyVolume = 130 },
        new VehicleModel { BodyType = BodyType.Van, BodyVolume = 200 },
        new VehicleModel { BodyType = BodyType.PickupTruck, BodyVolume = 180 }
    };

    public static List<Client> GetClients() => new()
    {
        new Client { Name = "Donald Truck", Phone = "88005553535" },
        new Client { Name = "Cat Dog", Phone = "89991112233" },
        new Client { Name = "John Smith", Phone = "89001234567" },
        new Client { Name = "Emma Watson", Phone = "89112345678" },
        new Client { Name = "Michael Brown", Phone = "89223456789" },
        new Client { Name = "Sarah Johnson", Phone = "89334567890" },
        new Client { Name = "David Miller", Phone = "89445678901" },
        new Client { Name = "Lisa Davis", Phone = "89556789012" },
        new Client { Name = "James Wilson", Phone = "89667890123" },
        new Client { Name = "Maria Garcia", Phone = "89778901234" }
    };

    public static List<Driver> GetDrivers() => new()
    {
        new Driver { PassportNumber = "1234 567890", FullName = "John Johnson", Experience = 5, License = DrivingLicence.C },
        new Driver { PassportNumber = "2345 678901", FullName = "Mike Tyson", Experience = 8, License = DrivingLicence.B },
        new Driver { PassportNumber = "3456 789012", FullName = "Conor McGregor", Experience = 3, License = DrivingLicence.A },
        new Driver { PassportNumber = "4567 890123", FullName = "Anderson Silva", Experience = 15, License = DrivingLicence.CE },
        new Driver { PassportNumber = "5678 901234", FullName = "Georges St-Pierre", Experience = 10, License = DrivingLicence.D },
        new Driver { PassportNumber = "6789 012345", FullName = "Jon Jones", Experience = 7, License = DrivingLicence.BE },
        new Driver { PassportNumber = "7890 123456", FullName = "Daniel Cormier", Experience = 20, License = DrivingLicence.C1 },
        new Driver { PassportNumber = "8901 234567", FullName = "Stipe Miocic", Experience = 6, License = DrivingLicence.A1 },
        new Driver { PassportNumber = "9012 345678", FullName = "Israel Adesanya", Experience = 4, License = DrivingLicence.M }
    };

    public static List<Vehicle> GetVehicles(List<VehicleModels> models) => new()
    {
        new Vehicle { LicensePlate = "X005XX05", LoadCapacity = 20.0, Model = models[0] },
        new Vehicle { LicensePlate = "E001MP777", LoadCapacity = 15.0, Model = models[1] },
        new Vehicle { LicensePlate = "A123BC77", LoadCapacity = 25.5, Model = models[2] },
        new Vehicle { LicensePlate = "B456DE99", LoadCapacity = 18.0, Model = models[3] },
        new Vehicle { LicensePlate = "C789FG11", LoadCapacity = 30.0, Model = models[4] },
        new Vehicle { LicensePlate = "D012HI33", LoadCapacity = 12.5, Model = models[5] },
        new Vehicle { LicensePlate = "E345JK55", LoadCapacity = 22.0, Model = models[6] },
        new Vehicle { LicensePlate = "F678LM77", LoadCapacity = 28.0, Model = models[7] },
        new Vehicle { LicensePlate = "G901NO88", LoadCapacity = 16.0, Model = models[8] },
        new Vehicle { LicensePlate = "H234PQ00", LoadCapacity = 35.0, Model = models[9] }
    };

    public static List<Trip> GetTrips(List<VehicleModels> models, List<Driver> drivers, List<Client> clients) => new()
    {
        new Trip { Driver = drivers[0], Vehicle = vehicles[0], Client = clients[0], Date = DateTime.Now.AddDays(-20), CargoWeight = 15.0, TransportationCost = 15000m, Status = TripStatus.InTransit },
        new Trip { Driver = drivers[1], Vehicle = vehicles[1], Client = clients[1], Date = DateTime.Now.AddDays(-15), CargoWeight = 12.5, TransportationCost = 12000m, Status = TripStatus.Completed },
        new Trip { Driver = drivers[2], Vehicle = vehicles[2], Client = clients[2], Date = DateTime.Now.AddDays(-10), CargoWeight = 20.0, TransportationCost = 18000m, Status = TripStatus.Planned },
        new Trip { Driver = drivers[3], Vehicle = vehicles[3], Client = clients[3], Date = DateTime.Now.AddDays(-5), CargoWeight = 8.5, TransportationCost = 9000m, Status = TripStatus.Completed },
        new Trip { Driver = drivers[4], Vehicle = vehicles[4], Client = clients[4], Date = DateTime.Now.AddDays(-2), CargoWeight = 25.0, TransportationCost = 22000m, Status = TripStatus.InTransit },
        new Trip { Driver = drivers[5], Vehicle = vehicles[5], Client = clients[5], Date = DateTime.Now.AddDays(1), CargoWeight = 10.0, TransportationCost = 11000m, Status = TripStatus.Planned },
        new Trip { Driver = drivers[6], Vehicle = vehicles[6], Client = clients[6], Date = DateTime.Now.AddDays(3), CargoWeight = 18.5, TransportationCost = 16500m, Status = TripStatus.Planned },
        new Trip { Driver = drivers[7], Vehicle = vehicles[7], Client = clients[7], Date = DateTime.Now.AddDays(-8), CargoWeight = 22.0, TransportationCost = 20000m, Status = TripStatus.Completed },
        new Trip { Driver = drivers[8], Vehicle = vehicles[8], Client = clients[8], Date = DateTime.Now.AddDays(-1), CargoWeight = 14.0, TransportationCost = 13500m, Status = TripStatus.InTransit }
    };
}
