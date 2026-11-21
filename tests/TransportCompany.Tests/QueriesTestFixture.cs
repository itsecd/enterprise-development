using TransportCompany.Domain.Entities;
using TransportCompany.Domain.Seeds;

namespace TransportCompany.Tests;

/// <summary>
/// Тестовый набор данных для проверки запросов
/// </summary>
public class QueriesTestFixture
{
    /// <summary>
    /// Список моделей транспортных средств
    /// </summary>
    public readonly List<VehicleModel> Models;

    /// <summary>
    /// Список клиентов
    /// </summary>
    public readonly List<Client> Clients;

    /// <summary>
    /// Список водителей
    /// </summary>
    public readonly List<Driver> Drivers;

    /// <summary>
    /// Список транспортных средств
    /// </summary>
    public readonly List<Vehicle> Vehicles;

    /// <summary>
    /// Список рейсов
    /// </summary>
    public readonly List<Trip> Trips;

    /// <summary>
    /// Инициализирует тестовые данные из слоя Domain
    /// </summary>
    public QueriesTestFixture()
    {
        Models = TestData.GetVehicleModels();
        Clients = TestData.GetClients();
        Drivers = TestData.GetDrivers();
        Vehicles = TestData.GetVehicles(Models);
        Trips = TestData.GetTrips(Vehicles, Drivers, Clients);
    }
}