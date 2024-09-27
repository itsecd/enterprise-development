using DispatchService.Model;

namespace DispatchService.Tests;

public class DispatchServiceTests(DispatchFixture fixture) : IClassFixture<DispatchFixture>
{
    private readonly DispatchFixture _fixture = fixture;

    [Fact]
    public void TestSpecificTransport()
    {
        var id = 1;
        var specificTransport = _fixture.TestData.Transports.FirstOrDefault(t => t.Id == id);
        Assert.NotNull(specificTransport);
        Assert.Equal("Volvo 7900", specificTransport.ModelName);
    }

    [Fact]
    public void TestDriversInPeriod()
    {
        var startDate = new DateTime(2024, 9, 14, 8, 0, 0);
        var endDate = new DateTime(2024, 9, 14, 12, 0, 0);

        var driversInPeriod = _fixture.TestData.Routes
            .Where(r => r.StartTime >= startDate && r.EndTime <= endDate)
            .Select(r => r.AssignedDriver)
            .Distinct()
            .OrderBy(d => d.FullName)
            .ToList();

        Assert.NotEmpty(driversInPeriod);
        Assert.Equal(2, driversInPeriod.Count);
    }

    [Fact]
    public void TestTotalTripTimesByTransport()
    {
        var expectedTripTimes = new List<(Transport.VehicleType TransportType, string Model, TimeSpan TotalTime)>
        {
            (Transport.VehicleType.Bus, "Volvo 7900", TimeSpan.FromSeconds((new TimeSpan(0, 2, 0, 0)).TotalSeconds)),
            (Transport.VehicleType.Trolleybus, "Trolza", TimeSpan.FromSeconds((new TimeSpan(0, 2, 0, 0)).TotalSeconds)),
            (Transport.VehicleType.Tram, "Stadler", TimeSpan.FromSeconds((new TimeSpan(0, 2, 0, 0)).TotalSeconds)),
            (Transport.VehicleType.Bus, "Mercedes Citaro", TimeSpan.FromSeconds((new TimeSpan(0, 2, 0, 0)).TotalSeconds)),
            (Transport.VehicleType.Trolleybus, "BKM 321", TimeSpan.FromSeconds((new TimeSpan(0, 7, 0, 0)).TotalSeconds)),
            (Transport.VehicleType.Tram, "CAF Urbos", TimeSpan.FromSeconds((new TimeSpan(0, 7, 0, 0)).TotalSeconds)),
            (Transport.VehicleType.Bus, "MAN Lion's City", TimeSpan.FromSeconds((new TimeSpan(0, 3, 0, 0)).TotalSeconds)),
            (Transport.VehicleType.Trolleybus, "ZIU-9", TimeSpan.FromSeconds((new TimeSpan(0, 9, 0, 0)).TotalSeconds))
        };

        var totalTripTimes = _fixture.TestData.Routes
            .GroupBy(r => new { r.AssignedTransport.Type, r.AssignedTransport.ModelName })
            .Select(g => new
            {
                TransportType = g.Key.Type,
                Model = g.Key.ModelName,
                TotalTime = TimeSpan.FromSeconds(g.Sum(r => (r.EndTime - r.StartTime).TotalSeconds))
            })
            .ToList();

        var actualTripTimes = totalTripTimes
            .Select(t => (t.TransportType, t.Model, t.TotalTime))
            .ToList();

        Assert.Equal(expectedTripTimes, actualTripTimes);
    }

    [Fact]
    public void TestTopDriversByTripCount()
    {
        var expectedTopDrivers = new List<(Driver Driver, int TripCount)>
        {
            (new Driver
            {
                Id = 5,
                FullName = "Васильев Алексей Павлович",
                Passport =  "6789 234567",
                DriverLicense = "IJ2345678",
                Address = "Санкт-Петербург, пр. Невский, д. 5",
                PhoneNumber = "+7 (921) 789-12-34"
            }, 2),
            (new Driver
            {
                Id = 1,
                FullName = "Иванов Иван Иванович",
                Passport = "1234 567890",
                DriverLicense = "AB1234567",
                Address = "Москва, ул. Ленина, д. 10",
                PhoneNumber = "+7 (912) 345-67-89"
            }, 1),
            (new Driver
            {
                Id = 2,
                FullName = "Петров Петр Петрович",
                Passport = "2345 678901",
                DriverLicense = "CD2345678",
                Address = "Москва, ул. Пушкина, д. 15",
                PhoneNumber = "+7 (912) 456-78-90"
            }, 1),
            (new Driver
            {
                Id = 3,
                FullName = "Сидоров Сидор Сидорович",
                Passport = "3456 789012",
                DriverLicense = "EF3456789",
                Address = "Санкт-Петербург, ул. Кирова, д. 20",
                PhoneNumber = "+7 (912) 567-89-01"
            }, 1),
            (new Driver
            {
                Id = 4,
                FullName = "Кузнецов Андрей Владимирович",
                Passport = "5678 123456",
                DriverLicense = "GH1234567",
                Address = "Москва, ул. Советская, д. 1",
                PhoneNumber = "+7 (903) 123-45-67"
            }, 1)
        };

        var topDrivers = _fixture.TestData.Routes
            .GroupBy(r => r.AssignedDriver)
            .Select(g => new
            {
                Driver = g.Key,
                TripCount = g.Count()
            })
            .OrderByDescending(d => d.TripCount)
            .Take(5)
            .ToList();

        var actualTopDrivers = topDrivers
            .Select(d => (d.Driver, d.TripCount))
            .ToList();

        for (int i = 0; i < expectedTopDrivers.Count; i++)
        {
            var expected = expectedTopDrivers[i];
            var actual = actualTopDrivers[i];

            Assert.Equal(expected.Driver.Id, actual.Driver.Id);
            Assert.Equal(expected.TripCount, actual.TripCount);
        }
    }

    [Fact]
    public void TestDriverTripStats()
    {
        var expectedDriverTripStats = new List<(Driver Driver, int TripCount, TimeSpan AverageTime, TimeSpan MaxTime)>
        {
            (new Driver
            {
                Id = 5,
                FullName = "Васильев Алексей Павлович",
                Passport =  "6789 234567",
                DriverLicense = "IJ2345678",
                Address = "Санкт-Петербург, пр. Невский, д. 5",
                PhoneNumber = "+7 (921) 789-12-34"
            }, 2, TimeSpan.FromSeconds(12600), TimeSpan.FromSeconds(14400)),
            (new Driver
            {
                Id = 1,
                FullName = "Иванов Иван Иванович",
                Passport = "1234 567890",
                DriverLicense = "AB1234567",
                Address = "Москва, ул. Ленина, д. 10",
                PhoneNumber = "+7 (912) 345-67-89"
            }, 1, TimeSpan.FromSeconds(7200), TimeSpan.FromSeconds(7200)),
            (new Driver
            {
                Id = 2,
                FullName = "Петров Петр Петрович",
                Passport = "2345 678901",
                DriverLicense = "CD2345678",
                Address = "Москва, ул. Пушкина, д. 15",
                PhoneNumber = "+7 (912) 456-78-90"
            }, 1, TimeSpan.FromSeconds(7200), TimeSpan.FromSeconds(7200)),

        };
        var driverTripStats = _fixture.TestData.Routes
        .GroupBy(r => r.AssignedDriver)
        .Select(g => new
        {
            Driver = g.Key,
            TripCount = g.Count(),
            AverageTime = TimeSpan.FromSeconds(g.Average(r => (r.EndTime - r.StartTime).TotalSeconds)),
            MaxTime = TimeSpan.FromSeconds(g.Max(r => (r.EndTime - r.StartTime).TotalSeconds))
        })
        .ToList();

        Assert.NotEmpty(driverTripStats);

        for (int i = 0; i < expectedDriverTripStats.Count; i++)
        {
            var expected = expectedDriverTripStats[i];
            var actual = driverTripStats.FirstOrDefault(d => d.Driver.Id == expected.Driver.Id);

            Assert.NotNull(actual);

            Assert.Equal(expected.TripCount, actual.TripCount);

            Assert.Equal(expected.AverageTime, actual.AverageTime);

            Assert.Equal(expected.MaxTime, actual.MaxTime);
        }
    }

    [Fact]
    public void TestTopTransportsByTripCount()
    {
        var startDate = new DateTime(2024, 9, 15, 7, 0, 0);
        var endDate = new DateTime(2024, 9, 15, 23, 0, 0);

        var expectedTopTransports = new List<(Transport Transport, int TripCount)>
        {
            (new Transport{Id = 5, LicensePlate = "C789GH", Type = Transport.VehicleType.Bus, ModelName = "Mercedes Sprinter", IsLowFloor = true, MaxCapacity = 70, YearOfManufacture = 2021 }, 2)
        };

        var transportTripCounts = _fixture.TestData.Routes
            .Where(r => r.StartTime >= startDate && r.EndTime <= endDate)
            .GroupBy(r => r.AssignedTransport)
            .Select(g => new
            {
                Transport = g.Key,
                TripCount = g.Count()
            })
            .ToList();

        Assert.NotEmpty(transportTripCounts);

        var maxTripCount = transportTripCounts.Max(t => t.TripCount);

        var topTransports = transportTripCounts
            .Where(t => t.TripCount == maxTripCount)
            .ToList();

        Assert.NotEmpty(topTransports);

        for (int i = 0; i < topTransports.Count; i++)
        {
            var expected = expectedTopTransports[i];
            var actual = topTransports.FirstOrDefault(t => t.Transport.Id == expected.Transport.Id);

            Assert.NotNull(actual);
            Assert.Equal(expected.TripCount, actual.TripCount);
        }
    }
}