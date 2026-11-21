using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Seeds;

namespace TransportCompany.Infrastructure.InMemory.Data;

/// <summary>
/// Имитация контекста базы данных в памяти
/// </summary>
public class InMemoryDbContext
{
    public List<VehicleModel> Models { get; set; }
    public List<Client> Clients { get; set; }
    public List<Driver> Drivers { get; set; }
    public List<Vehicle> Vehicles { get; set; }
    public List<Trip> Trips { get; set; }

    /// <summary>
    /// Инициализируем БД  стартовыми данными из Domain
    /// </summary>
    public InMemoryDbContext()
    {
        Models = TestData.GetVehicleModels();
        Clients = TestData.GetClients();
        Drivers = TestData.GetDrivers();
        Vehicles = TestData.GetVehicles(Models);
        Trips = TestData.GetTrips(Vehicles, Drivers, Clients);
    }
}