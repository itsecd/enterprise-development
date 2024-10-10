namespace TaxiDetails.Tests;

using System.Diagnostics.CodeAnalysis;
using TaxiDetailsClass.Domain;

/// <summary>
/// class for test requests
/// </summary>
public class UnitTest1(TaxiDetailsData dataProvider) : IClassFixture<TaxiDetailsData>
{
    private readonly TaxiDetailsData _dataProvider = dataProvider;
    /// <summary>
    /// info about driver of car
    /// </summary>
    [Fact]
    public void ReturnCarDriverInfo()
    {
        string name = "Максим";
        var expectedData = _dataProvider.Cars[0];
        var getData = _dataProvider.Cars.Where(c => c.AssignedDriver.Name == name).Select(c => c).First();
        Assert.Equal(expectedData, getData);
    }
    /// <summary>
    /// info about passenger of dateinterval
    /// </summary>
    [Fact]
    public void ReturnPassengetInfo()
    {
        DateTime startDate = new(2024, 10, 1);
        DateTime endDate = new(2024, 10, 4);

        var expectedUsersName = new List<string>
        {
            _dataProvider.Users[0].FullName,
            _dataProvider.Users[2].FullName,
            _dataProvider.Users[3].FullName
        };
        var getData = _dataProvider.Travels
            .Where(t => t.TripDate >= startDate && t.TripDate <= endDate)
            .Select(t => t.Client.FullName) 
            .Distinct()
            .OrderBy(u => u) 
            .ToList();
        Assert.Equal(expectedUsersName, getData);
    }
    /// <summary>
    /// info about passenger and count of trips
    /// </summary>
    [Fact]
    public void ReturnPassengetTravelsCount()
    {
        var expectedUsersName = new Dictionary<string, int>
{
    { _dataProvider.Users[0].FullName, 4 },
    { _dataProvider.Users[1].FullName, 1 },
    { _dataProvider.Users[2].FullName, 2 }, 
    { _dataProvider.Users[3].FullName, 1 }, 
    { _dataProvider.Users[4].FullName, 1 }, 
    { _dataProvider.Users[5].FullName, 1 },
    { _dataProvider.Users[6].FullName, 1 }, 
    { _dataProvider.Users[7].FullName, 1 }, 
    { _dataProvider.Users[8].FullName, 1 }, 
    { _dataProvider.Users[9].FullName, 1 }  
};
        var getPassengerTripCounts = _dataProvider.Travels
         .GroupBy(t => t.Client) // Ãðóïïèðóåì ïî êëèåíòàì
         .Select(g => new
         {
             Passenger = g.Key,
             TripCount = g.Count() // Ñ÷èòàåì êîëè÷åñòâî ïîåçäîê
         }).OrderBy(p => p.Passenger.Id)
         .ToDictionary(x => x.Passenger.FullName, x => x.TripCount); // Ïðåîáðàçóåì â ñëîâàðü
        Assert.Equal(expectedUsersName,getPassengerTripCounts );
    }
    /// <summary>
    /// top 5 drivers
    /// </summary>
    [Fact]
    public void ReturnTopFiveDrives()
    {
        var expectedUsersName1 = new Dictionary<string, int> {
        { _dataProvider.Drivers[0].Name, 3 },
        { _dataProvider.Drivers[1].Name, 2 },
        { _dataProvider.Drivers[2].Name, 2 },
        { _dataProvider.Drivers[3].Name, 1 },
        { _dataProvider.Drivers[4].Name, 1 },
    };

        var driverTripCounts1 = _dataProvider.Travels
            .GroupBy(t => t.AssignedCar.AssignedDriver)
            .Select(g => new
            {
                Driver = g.Key,
                TripCount = g.Count()
            })
            .OrderByDescending(x => x.TripCount) // Ñîðòèðóåì ïî êîëè÷åñòâó ïîåçäîê
            .Take(5) 
            .ToList();
        var actualUsersName1 = driverTripCounts1.ToDictionary(x => x.Driver.Name, x => x.TripCount);
        Assert.Equal(expectedUsersName1, actualUsersName1);
    }
    /// <summary>
    /// info about avg and max time of drivers
    /// </summary>
    [Fact]
    public void ReturnStatisticTime()
    {
        var expectedData = new List<(string Name, int TripCount, TimeOnly AvgDrivingTime, TimeOnly MaxDrivingTime)>
        {
            (_dataProvider.Drivers[0].Name, 3, new TimeOnly(0, 32), new TimeOnly(0, 50)),
            (_dataProvider.Drivers[1].Name, 2, new TimeOnly(0, 23), new TimeOnly(0, 30)),
            (_dataProvider.Drivers[2].Name, 2, new TimeOnly(0, 23), new TimeOnly(0, 26)),
            (_dataProvider.Drivers[3].Name, 1, new TimeOnly(0, 30), new TimeOnly(0, 30)),
            (_dataProvider.Drivers[4].Name, 1, new TimeOnly(0, 20), new TimeOnly(0, 20)),
            (_dataProvider.Drivers[5].Name, 1, new TimeOnly(0, 26), new TimeOnly(0, 26)),
            (_dataProvider.Drivers[6].Name, 1, new TimeOnly(0, 20), new TimeOnly(0, 20)),
            (_dataProvider.Drivers[7].Name, 1, new TimeOnly(0, 26), new TimeOnly(0, 26)),
            (_dataProvider.Drivers[8].Name, 1, new TimeOnly(0, 30), new TimeOnly(0, 30)),
            (_dataProvider.Drivers[9].Name, 1, new TimeOnly(0, 30), new TimeOnly(0, 30))
        };
        var driverTripStats = _dataProvider.Travels
    .GroupBy(t => t.AssignedCar.AssignedDriver)
    .Select(g => (
        Driver: g.Key.Name,
        TripCount: g.Count(),
        AvgDrivingTime: TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(g.Average(t => t.TravelTime.TotalMinutes))),
        MaxDrivingTime: TimeOnly.FromTimeSpan(TimeSpan.FromMinutes(g.Max(t => t.TravelTime.TotalMinutes)))
    ))
    .ToList();
        Assert.Equal(expectedData, driverTripStats);
    }
    /// <summary>
    /// info of client max trips in dateinterval 
    /// </summary>
    [Fact]
    public void GetClientsMaxTrips()
    {
        var startDate = new DateTime(2024, 10, 1); 
        var endDate = new DateTime(2024, 10, 11); 
        var expectedData =new List<User>
        { _dataProvider.Users[0]};
        var topPassengers = _dataProvider.Travels
        .Where(t => t.TripDate >= startDate && t.TripDate <= endDate) 
        .GroupBy(t => t.Client)
        .Select(g => new
        {
            Passenger = g.Key,
            TripCount = g.Count()
        })
        .Where(x => x.TripCount ==
                    _dataProvider.Travels
                        .Where(t => t.TripDate >= startDate && t.TripDate <= endDate)
                        .GroupBy(t => t.Client)
                        .Max(g => g.Count()))
        .Select(x => x.Passenger)
        .ToList();



        Assert.Equal(expectedData, topPassengers);
    }
}

