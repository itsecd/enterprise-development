using System.ComponentModel;
using System.Runtime.CompilerServices;
using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Enums;

namespace TransportCompany.Domain.Tests;

public static class DataSeeder
{
    public static List<VehicleModel> GetVehicleModels() => new()
    {
        new VehicleModel { Id = 0, BodyType = BodyType.Sedan, BodyVolume = 82 },
        new VehicleModel { Id = 1, BodyType = BodyType.Hatchback, BodyVolume = 90 },
        new VehicleModel { Id = 2, BodyType = BodyType.Coupe, BodyVolume = 75 },
        new VehicleModel { Id = 3, BodyType = BodyType.Cabriolet, BodyVolume = 70 },
        new VehicleModel { Id = 4, BodyType = BodyType.SUV, BodyVolume = 150 },
        new VehicleModel { Id = 5, BodyType = BodyType.Crossover, BodyVolume = 120 },
        new VehicleModel { Id = 6, BodyType = BodyType.StationWagon, BodyVolume = 110 },
        new VehicleModel { Id = 7, BodyType = BodyType.Minivan, BodyVolume = 130 },
        new VehicleModel { Id = 8, BodyType = BodyType.Van, BodyVolume = 200 },
        new VehicleModel { Id = 9, BodyType = BodyType.PickupTruck, BodyVolume = 180 }
    };

    public static List<Client> GetClients() => new()
    {
        new Client { Id = 0, Name = "Donald Truck", Phone = "88005553535" },
        new Client { Id = 1, Name = "Cat Dog", Phone = "89991112233" },
        new Client { Id = 2, Name = "John Smith", Phone = "89001234567" },
        new Client { Id = 3, Name = "Emma Watson", Phone = "89112345678" },
        new Client { Id = 4, Name = "Michael Brown", Phone = "89223456789" },
        new Client { Id = 5, Name = "Sarah Johnson", Phone = "89334567890" },
        new Client { Id = 6, Name = "David Miller", Phone = "89445678901" },
        new Client { Id = 7, Name = "Lisa Davis", Phone = "89556789012" },
        new Client { Id = 8, Name = "James Wilson", Phone = "89667890123" },
        new Client { Id = 9, Name = "Maria Garcia", Phone = "89778901234" }
    };

    public static List<Driver> GetDrivers() => new()
    {
        new Driver { Id = 0, PassportNumber = "1234 567890", FullName = "John Johnson", Experience = 5, License = DrivingLicence.C },
        new Driver { Id = 1, PassportNumber = "2345 678901", FullName = "Mike Tyson", Experience = 8, License = DrivingLicence.B },
        new Driver { Id = 2, PassportNumber = "3456 789012", FullName = "Conor McGregor", Experience = 3, License = DrivingLicence.A },
        new Driver { Id = 3, PassportNumber = "4567 890123", FullName = "Anderson Silva", Experience = 15, License = DrivingLicence.CE },
        new Driver { Id = 4, PassportNumber = "5678 901234", FullName = "Georges St-Pierre", Experience = 10, License = DrivingLicence.D },
        new Driver { Id = 5, PassportNumber = "6789 012345", FullName = "Jon Jones", Experience = 7, License = DrivingLicence.BE },
        new Driver { Id = 6, PassportNumber = "7890 123456", FullName = "Daniel Cormier", Experience = 20, License = DrivingLicence.C1 },
        new Driver { Id = 7, PassportNumber = "8901 234567", FullName = "Stipe Miocic", Experience = 6, License = DrivingLicence.A1 },
        new Driver { Id = 8, PassportNumber = "9012 345678", FullName = "Israel Adesanya", Experience = 4, License = DrivingLicence.M },
        new Driver { Id = 9, PassportNumber = "1111 345678", FullName = "Aaaaaa Uuuuuuuu", Experience = 1, License = DrivingLicence.M }
    };

    public static List<Vehicle> GetVehicles(List<VehicleModels> models) => new()
    {
        new Vehicle { Id = 0, LicensePlate = "X005XX05", LoadCapacity = 20.0, ModelId = 0 },
        new Vehicle { Id = 1, LicensePlate = "E001MP777", LoadCapacity = 15.0, ModelId = 1 },
        new Vehicle { Id = 2, LicensePlate = "A123BC77", LoadCapacity = 25.5, ModelId = 2 },
        new Vehicle { Id = 3, LicensePlate = "B456DE99", LoadCapacity = 18.0, ModelId = 3 },
        new Vehicle { Id = 4, LicensePlate = "C789FG11", LoadCapacity = 30.0, ModelId = 4 },
        new Vehicle { Id = 5, LicensePlate = "D012HI33", LoadCapacity = 12.5, ModelId = 5 },
        new Vehicle { Id = 6, LicensePlate = "E345JK55", LoadCapacity = 22.0, ModelId = 6 },
        new Vehicle { Id = 7, LicensePlate = "F678LM77", LoadCapacity = 28.0, ModelId = 7 },
        new Vehicle { Id = 8, LicensePlate = "G901NO88", LoadCapacity = 16.0, ModelId = 8 },
        new Vehicle { Id = 9, LicensePlate = "H234PQ00", LoadCapacity = 35.0, ModelId = 9 }
    };

    public static List<Trip> GetTrips(List<VehicleModels> models, List<Driver> drivers, List<Client> clients) => new()
    {
        new Trip { DriverId = 0, VehicleId = 0, ClientId = 0, Date = DateTime.Now.AddDays(-20), CargoWeight = 15.0, TransportationCost = 15000m, Status = TripStatus.InTransit },
        new Trip { DriverId = 1, VehicleId = 1, ClientId = 1, Date = DateTime.Now.AddDays(-15), CargoWeight = 12.5, TransportationCost = 12000m, Status = TripStatus.Completed },
        new Trip { DriverId = 2, VehicleId = 2, ClientId = 2, Date = DateTime.Now.AddDays(-10), CargoWeight = 20.0, TransportationCost = 18000m, Status = TripStatus.Planned },
        new Trip { DriverId = 3, VehicleId = 3, ClientId = 3, Date = DateTime.Now.AddDays(-5), CargoWeight = 8.5, TransportationCost = 9000m, Status = TripStatus.Completed },
        new Trip { DriverId = 4, VehicleId = 4, ClientId = 4, Date = DateTime.Now.AddDays(-2), CargoWeight = 25.0, TransportationCost = 22000m, Status = TripStatus.InTransit },
        new Trip { DriverId = 5, VehicleId = 5, ClientId = 5, Date = DateTime.Now.AddDays(1), CargoWeight = 10.0, TransportationCost = 11000m, Status = TripStatus.Planned },
        new Trip { DriverId = 6, VehicleId = 6, ClientId = 6, Date = DateTime.Now.AddDays(3), CargoWeight = 18.5, TransportationCost = 16500m, Status = TripStatus.Planned },
        new Trip { DriverId = 7, VehicleId = 7, ClientId = 7, Date = DateTime.Now.AddDays(-8), CargoWeight = 22.0, TransportationCost = 20000m, Status = TripStatus.Completed },
        new Trip { DriverId = 8, VehicleId = 8, ClientId = 8, Date = DateTime.Now.AddDays(-1), CargoWeight = 14.0, TransportationCost = 13500m, Status = TripStatus.InTransit },
        new Trip { DriverId = 9, VehicleId = 9, ClientId = 9, Date = DateTime.Now.AddDays(-2), CargoWeight = 13.0, TransportationCost = 12500m, Status = TripStatus.InTransit }
    };
}
