using DispatchTransportControl.Domain.Entity;
using DispatchTransportControl.Domain.Repository;

namespace DispatchTransportControl.Domain.Test;

public class DispatchTransportControlTest(TestFixture testFixture) : IClassFixture<TestFixture>
{
    private readonly DriverRepository _driverRepository = testFixture.DriverRepository;

    private readonly VehicleRepository _vehicleRepository = testFixture.VehicleRepository;

    [Fact]
    public void GetInfoAboutVehicleById()
    {
        const int vehicleId = 1;
        var vehicle = _vehicleRepository.GetById(vehicleId);

        Assert.NotNull(vehicle);
        Assert.Equal(vehicleId, vehicle.Id);
        Assert.Equal("registrationNumber1", vehicle.RegistrationNumber);
        Assert.Equal(VehicleType.Bus, vehicle.VehicleType);
        Assert.Equal(testFixture.TestDataProvider.VehicleModels[0], vehicle.VehicleModel);
        Assert.Equal(1993, vehicle.YearOfManufacture);
    }

    [Fact]
    public void GetDriversByPeriod()
    {
        var startDate = new DateTime(2024, 9, 20, 10, 0, 0);
        startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
        var endDate = new DateTime(2024, 9, 20, 19, 0, 0);
        endDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

        var drivers = _driverRepository.GetAllByPeriod(startDate, endDate).ToList();

        Assert.Equal(4, drivers.Count);
        Assert.Equal("driverSurname1", testFixture.TestDataProvider.Drivers[0].Surname);
        Assert.Equal("driverSurname2", testFixture.TestDataProvider.Drivers[1].Surname);
        Assert.Equal("driverSurname3", testFixture.TestDataProvider.Drivers[2].Surname);
        Assert.Equal("driverSurname4", testFixture.TestDataProvider.Drivers[3].Surname);
    }

    [Fact]
    public void GetTotalTripTimeByVehicleTypeAndModel()
    {
        var tripDurationsForVehicleTypeAndModel =
            _vehicleRepository.GetTotalTripTimeForEveryVehicleTypeAndModel();

        Assert.Equal(1,
            tripDurationsForVehicleTypeAndModel[(VehicleType.Bus, testFixture.TestDataProvider.VehicleModels[0])]);
        Assert.Equal(1,
            tripDurationsForVehicleTypeAndModel[(VehicleType.Trolley, testFixture.TestDataProvider.VehicleModels[1])]);
        Assert.Equal(2,
            tripDurationsForVehicleTypeAndModel[(VehicleType.Bus, testFixture.TestDataProvider.VehicleModels[2])]);
        Assert.Equal(3,
            tripDurationsForVehicleTypeAndModel[(VehicleType.Bus, testFixture.TestDataProvider.VehicleModels[3])]);
        Assert.Equal(2,
            tripDurationsForVehicleTypeAndModel[(VehicleType.Tram, testFixture.TestDataProvider.VehicleModels[4])]);
    }

    [Fact]
    public void GetTop5DriversByTripCount()
    {
        var topDrivers = _driverRepository.GetTop5DriversByTripCount().ToList();

        Assert.Equal(5, topDrivers.Count);
        Assert.Equal(testFixture.TestDataProvider.Drivers[3], topDrivers[0].Driver);
    }

    [Fact]
    public void GetDriverTripStats()
    {
        var driverStats = _driverRepository.GetDriverTripStats().ToList();

        Assert.Equal(5, driverStats.Count);
        Assert.Equal(2, driverStats[3].Value.TripCount);
        Assert.Equal(1.5, driverStats[3].Value.AvgTime);
        Assert.Equal(2, driverStats[3].Value.MaxTime);
    }

    [Fact]
    public void GetVehiclesWithMaxTripsForPeriod()
    {
        var startDate = new DateTime(2024, 9, 19, 00, 0, 0);
        startDate = DateTime.SpecifyKind(startDate, DateTimeKind.Utc);
        var endDate = new DateTime(2024, 9, 22, 00, 0, 0);
        endDate = DateTime.SpecifyKind(endDate, DateTimeKind.Utc);

        var vehiclesWithMaxTrips =
            _vehicleRepository.GetVehiclesWithMaxTripsForPeriod(startDate, endDate).ToList();

        Assert.Single(vehiclesWithMaxTrips);
        Assert.Equal(testFixture.TestDataProvider.Vehicles[4], vehiclesWithMaxTrips[0].Vehicle);
        Assert.Equal(2, vehiclesWithMaxTrips[0].TripCount);
    }
}