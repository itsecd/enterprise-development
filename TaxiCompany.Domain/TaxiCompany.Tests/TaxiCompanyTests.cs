using TaxiCompany.Domain;

namespace TaxiCompany.Tests;
/// <summary>
///  ласс дл€ проведени€ юнит-тестов
/// </summary>
/// <param name="fixture"></param>
public class TaxiCompanyTests(TaxiCompanyFixture fixture) : IClassFixture<TaxiCompanyFixture>
{
    private readonly TaxiCompanyFixture _fixture = fixture;
    /// <summary>
    /// ¬ыводит все сведени€ о конкретном водителе и его автомобиле
    /// </summary>
    [Fact]
    public void GetDriverAndCar()
    {
        var driverId = 5;
        var expectedDriver = _fixture.Drivers_list[4];
        var expectedCar = _fixture.Cars_list[4];

        var driver = _fixture.Drivers_list.First(d => d.Id == driverId);
        var car = _fixture.Cars_list.First(c => c.Id == driver.AssignedCarId);

        Assert.Equal(expectedDriver, driver);
        Assert.Equal(expectedCar, car);
    }
    /// <summary>
    /// ¬ыводит всех пассажиров, совершивших поездки за заданный период, упор€дочива€ по ‘»ќ
    /// </summary>
    [Fact]
    public void GetClientsByDate()
    {
        var expectedData = new List<Client>
        {
            _fixture.Clients_list[5],
            _fixture.Clients_list[10]
        };

        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 6, 30);

        var passengers = _fixture.Trips_list
            .Where(t => t.Date >= startDate && t.Date <= endDate)
            .Select(t => _fixture.Clients_list.First(c => c.Id == t.AssignedClientId))
            .Distinct()
            .OrderBy(c => c.FullName)
            .ToList();

        Assert.Equal(expectedData, passengers);
    }
    /// <summary>
    /// ¬ыводит количество поездок каждого пассажира
    /// </summary>
    [Fact]
    public void GetCountTrips()
    {
        var expectedData = new List<object>
        {
            new { Client = _fixture.Clients_list[0], TripCount = 2 },
            new { Client = _fixture.Clients_list[1], TripCount = 1 },
            new { Client = _fixture.Clients_list[2], TripCount = 1 },
            new { Client = _fixture.Clients_list[3], TripCount = 2 },
            new { Client = _fixture.Clients_list[4], TripCount = 1 },
            new { Client = _fixture.Clients_list[5], TripCount = 2 },
            new { Client = _fixture.Clients_list[6], TripCount = 1 },
            new { Client = _fixture.Clients_list[7], TripCount = 0 },
            new { Client = _fixture.Clients_list[8], TripCount = 0 },
            new { Client = _fixture.Clients_list[9], TripCount = 1 },
            new { Client = _fixture.Clients_list[10], TripCount = 2 }
        };

        var count = _fixture.Clients_list
            .Select(client => new
            {
                Client = client,
                TripCount = _fixture.Trips_list.Count(t => t.AssignedClientId == client.Id)
            })
            .ToList();

        Assert.Equal(expectedData, count);
    }
    /// <summary>
    /// ¬ыводит топ 5 водителей по совершенному количеству поездок
    /// </summary>
    [Fact]
    public void GetTopDrivers()
    {
        var expectedData = new List<(Driver Driver, int TripCount)>
        {
            (_fixture.Drivers_list[1], 3),
            (_fixture.Drivers_list[2], 2),
            (_fixture.Drivers_list[3], 2),
            (_fixture.Drivers_list[4], 2),
            (_fixture.Drivers_list[5], 2)
        };

        var drivers = _fixture.Drivers_list
            .Select(driver => new
            {
                Driver = driver,
                TripCount = _fixture.Trips_list.Count(t => t.AssignedCarId == driver.AssignedCarId)
            })
            .OrderByDescending(d => d.TripCount)
            .Take(5)
            .ToList();

        Assert.Equal(expectedData.Count, drivers.Count);
        for (int i = 0; i < expectedData.Count; i++)
        {
            Assert.Equal(expectedData[i].Driver, drivers[i].Driver);
            Assert.Equal(expectedData[i].TripCount, drivers[i].TripCount);
        }
    }
    /// <summary>
    ///  ¬ыводит информацию о количестве поездок, среднем времени и максимальном времени поездки дл€ каждого водител€
    /// </summary>
    [Fact]
    public void GetDriverTripStats()
    {
        var expectedData = new List<(Driver Driver, int TripCount, TimeOnly AvgDrivingTime, TimeOnly MaxDrivingTime)>
        {
            (_fixture.Drivers_list[0], 1, new TimeOnly(0, 23, 13), new TimeOnly(0, 23, 13)),
            (_fixture.Drivers_list[1], 3, new TimeOnly(0, 28, 2), new TimeOnly(0, 40, 45)),
            (_fixture.Drivers_list[2], 2, new TimeOnly(1, 05, 6, 500), new TimeOnly(1, 55, 1)),
            (_fixture.Drivers_list[3], 2, new TimeOnly(1, 4, 31), new TimeOnly(1, 28, 28)),
            (_fixture.Drivers_list[4], 2, new TimeOnly(0, 24, 22, 500), new TimeOnly(0, 25, 32)),
            (_fixture.Drivers_list[5], 2, new TimeOnly(0, 35, 30, 500), new TimeOnly(0, 35, 39)),
            (_fixture.Drivers_list[6], 1, new TimeOnly(0, 20, 2), new TimeOnly(0, 20, 2))
        };

        var drivers = _fixture.Drivers_list
        .Select(driver => (
            Driver: driver,
            TripCount: _fixture.Trips_list.Count(t => t.AssignedCarId == driver.AssignedCarId),
            AvgDrivingTime: new TimeOnly(0, 0).Add(
                _fixture.Trips_list
                .Where(t => t.AssignedCarId == driver.AssignedCarId)
                .Select(t => t.DrivingTime.ToTimeSpan())
                .DefaultIfEmpty(TimeSpan.Zero)
                .Aggregate(TimeSpan.Zero, (t1, t2) => t1 + t2)
                .Divide(_fixture.Trips_list.Count(t => t.AssignedCarId == driver.AssignedCarId) == 0
                    ? 1
                    : _fixture.Trips_list.Count(t => t.AssignedCarId == driver.AssignedCarId))),
            MaxDrivingTime: _fixture.Trips_list
                .Where(t => t.AssignedCarId == driver.AssignedCarId)
                .Select(t => t.DrivingTime)
                .DefaultIfEmpty(new TimeOnly(0, 0))
                .Max()
        ))
        .ToList();

        for (int i = 0; i < expectedData.Count; i++)
        {
            var expected = expectedData[i];
            var actual = drivers[i];

            Assert.Equal(expected.Driver.FullName, actual.Driver.FullName);
            Assert.Equal(expected.TripCount, actual.TripCount);
            Assert.Equal(expected.AvgDrivingTime.ToTimeSpan(), actual.AvgDrivingTime.ToTimeSpan());
            Assert.Equal(expected.MaxDrivingTime.ToTimeSpan(), actual.MaxDrivingTime.ToTimeSpan());
        }
    }
    /// <summary>
    /// ¬ыводит информацию о пассажирах, совершивших максимальное число поездок за указанный период.
    /// </summary>
    [Fact]
    public void GetClientsMaxTrips()
    {
        var expectedData = new List<Client> 
        {
            _fixture.Clients_list[0],
            _fixture.Clients_list[3],
            _fixture.Clients_list[5],
            _fixture.Clients_list[10]
        };

        var startDate = new DateTime(2024, 1, 1);
        var endDate = new DateTime(2024, 12, 31);

        var trips = _fixture.Trips_list
            .Where(t => t.Date >= startDate && t.Date <= endDate)
            .GroupBy(t => t.AssignedClientId)
            .Select(group => new
            {
                Client = _fixture.Clients_list.First(c => c.Id == group.Key),
                TripCount = group.Count()
            })
            .ToList();
        var maxTripCount = trips.Max(c => c.TripCount);
        var clients = trips
            .Where(c => c.TripCount == maxTripCount)
            .Select(c => c.Client)
            .ToList();

        Assert.Equal(expectedData, clients);
    }
}