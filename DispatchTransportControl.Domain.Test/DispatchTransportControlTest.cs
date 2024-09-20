namespace DispatchTransportControl.Domain.Test;

public class DispatchTransportControlTest(TestDataProvider testDataProvider) : IClassFixture<TestDataProvider>
{
    [Fact]
    public void GetInfoAboutVehicleById()
    {
        const int vehicleId = 1;
        var vehicle = testDataProvider.Vehicles.FirstOrDefault(v => v.Id == vehicleId);

        Assert.NotNull(vehicle);
        Assert.Equal(vehicleId, vehicle.Id);
        Assert.Equal("registrationNumber1", vehicle.RegistrationNumber);
        Assert.Equal(VehicleType.Bus, vehicle.VehicleType);
        Assert.Equal(testDataProvider.VehicleModels[0], vehicle.VehicleModel);
        Assert.Equal(1993, vehicle.YearOfManufacture);
    }

    [Fact]
    public void GetDriversByPeriod()
    {
        var startDate = new DateTime(2024, 9, 20, 10, 0, 0);
        var endDate = new DateTime(2024, 9, 20, 19, 0, 0);

        var drivers = testDataProvider.RouteAssignments
            .Where(ra => startDate <= ra.StartTime && ra.EndTime <= endDate)
            .Select(ra => ra.Driver)
            .Distinct()
            .OrderBy(d => d.Surname).ThenBy(d => d.Name).ThenBy(d => d.Patronymic)
            .ToList();

        Assert.Equal(4, drivers.Count);
        Assert.Equal("driverSurname1", testDataProvider.Drivers[0].Surname);
        Assert.Equal("driverSurname2", testDataProvider.Drivers[1].Surname);
        Assert.Equal("driverSurname3", testDataProvider.Drivers[2].Surname);
        Assert.Equal("driverSurname4", testDataProvider.Drivers[3].Surname);
    }

    [Fact]
    public void GetTotalTripTimeByVehicleTypeAndModel()
    {
        var tripDurationsForVehicleTypeAndModel = testDataProvider.RouteAssignments
            .GroupBy(ra => new { ra.Vehicle.VehicleType, ra.Vehicle.VehicleModel.Id })
            .ToDictionary
            (
                group => new { group.Key.VehicleType, group.Key.Id },
                group => group.Sum(ra => (ra.EndTime - ra.StartTime).TotalHours)
            );

        Assert.Equal(1, tripDurationsForVehicleTypeAndModel[new { VehicleType = VehicleType.Bus, Id = 1 }]);
        Assert.Equal(1, tripDurationsForVehicleTypeAndModel[new { VehicleType = VehicleType.Trolley, Id = 2 }]);
        Assert.Equal(2, tripDurationsForVehicleTypeAndModel[new { VehicleType = VehicleType.Bus, Id = 3 }]);
        Assert.Equal(3, tripDurationsForVehicleTypeAndModel[new { VehicleType = VehicleType.Bus, Id = 4 }]);
        Assert.Equal(2, tripDurationsForVehicleTypeAndModel[new { VehicleType = VehicleType.Tram, Id = 5 }]);
    }

    [Fact]
    public void GetTop5DriversByTripCount()
    {
        var topDrivers = testDataProvider.RouteAssignments
            .GroupBy(ra => ra.Driver)
            .Select(group => new
            {
                Driver = group.Key,
                TripCount = group.Count()
            })
            .OrderByDescending(g => g.TripCount)
            .Take(5)
            .ToList();

        Assert.Equal(5, topDrivers.Count);
        Assert.Equal(testDataProvider.Drivers[3], topDrivers[0].Driver);
    }

    [Fact]
    public void GetDriverTripStats()
    {
        var driverStats = testDataProvider.RouteAssignments
            .GroupBy(ra => ra.Driver)
            .ToDictionary(
                group => group.Key,
                group => new
                {
                    TripCount = group.Count(),
                    AvgTime = group.Average(ra => (ra.EndTime - ra.StartTime).TotalHours),
                    MaxTime = group.Max(ra => (ra.EndTime - ra.StartTime).TotalHours)
                }
            );

        Assert.Equal(5, driverStats.Count);
        Assert.Equal(2, driverStats[testDataProvider.Drivers[3]].TripCount);
        Assert.Equal(1.5, driverStats[testDataProvider.Drivers[3]].AvgTime);
        Assert.Equal(2, driverStats[testDataProvider.Drivers[3]].MaxTime);
    }

    [Fact]
    public void GetVehiclesWithMaxTripsForPeriod()
    {
        var startDate = new DateTime(2024, 9, 19, 00, 0, 0);
        var endDate = new DateTime(2024, 9, 22, 00, 0, 0);

        var maxTripCount = testDataProvider.RouteAssignments.Where(ra => ra.StartTime >= startDate && ra.EndTime <= endDate)
            .GroupBy(ra => ra.Vehicle)
            .Select(group => group.Count())
            .Max();

        var vehiclesWithMaxTrips = testDataProvider.RouteAssignments.Where(ra => ra.StartTime >= startDate && ra.EndTime <= endDate)
            .GroupBy(ra => ra.Vehicle)
            .Select(group => new { Vehicle = group.Key, TripCount = group.Count() })
            .Where(g => g.TripCount == maxTripCount)
            .ToList();

        Assert.Single(vehiclesWithMaxTrips);
        Assert.Equal(testDataProvider.Vehicles[4], vehiclesWithMaxTrips[0].Vehicle);
        Assert.Equal(2, vehiclesWithMaxTrips[0].TripCount);
    }

}