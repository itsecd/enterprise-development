namespace DispatchTransportControl.Domain.Test;

public class DispatchTransportControlTest(TestDataProvider testDataProvider) : IClassFixture<TestDataProvider>
{
    [Fact]
    public void GetInfoAboutVehicleById()
    {
        int vehicleId = 1;
        Vehicle? vehicle = testDataProvider.Vehicles.FirstOrDefault(v => v.Id == vehicleId);

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
        DateTime startDate = testDataProvider.DatetimeNow.AddHours(-5);
        DateTime endDate = testDataProvider.DatetimeNow.AddHours(4);

        List<Driver> drivers = testDataProvider.RouteAssignments
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
    public void GetTotalTripTimeByVehicleType()
    {
        Dictionary<VehicleType, double> tripDurationsForVehicleType = testDataProvider.RouteAssignments
            .GroupBy(ra => new { ra.Vehicle.VehicleType })
            .ToDictionary
            (
                group => group.Key.VehicleType,
                group => group.Sum(ra => (ra.EndTime - ra.StartTime).TotalHours)
            );

        Assert.Equal(6, tripDurationsForVehicleType[VehicleType.Bus]);
        Assert.Equal(1, tripDurationsForVehicleType[VehicleType.Trolley]);
        Assert.Equal(2, tripDurationsForVehicleType[VehicleType.Tram]);
    }

    [Fact]
    public void GetTotalTripTimeByVehicleModel()
    {
        Dictionary<int, double> tripDurationsForVehicleModel = testDataProvider.RouteAssignments
            .GroupBy(ra => new { ra.Vehicle.VehicleModel.Id })
            .ToDictionary
            (
                group => group.Key.Id,
                group => group.Sum(ra => (ra.EndTime - ra.StartTime).TotalHours)
            );

        Assert.Equal(1, tripDurationsForVehicleModel[1]);
        Assert.Equal(1, tripDurationsForVehicleModel[2]);
        Assert.Equal(2, tripDurationsForVehicleModel[3]);
        Assert.Equal(3, tripDurationsForVehicleModel[4]);
        Assert.Equal(2, tripDurationsForVehicleModel[5]);
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
        DateTime startDate = testDataProvider.DatetimeNow.AddHours(-5);
        DateTime endDate = testDataProvider.DatetimeNow.AddHours(10);

        int maxTripCount = testDataProvider.RouteAssignments.Where(ra => ra.StartTime >= startDate && ra.EndTime <= endDate)
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