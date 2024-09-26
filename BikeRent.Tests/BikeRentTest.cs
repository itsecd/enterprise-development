
namespace BikeRent.Tests;
public class BikeRentTest(BikeRentFixture fixture) : IClassFixture<BikeRentFixture>
{
    private readonly BikeRentFixture _fixture = fixture;
    /// <summary>
    /// Info about all sport bikes
    /// </summary>
    [Fact]
    public void TestSelectSportBikes()
    {
        var bikes = _fixture.Bikes;
        var types = _fixture.Types;
        var sportBikes =
            (from type in types
            where type.Name == "Sport"
            join bike in bikes on type.Id equals bike.TypeId
            select bike).ToList();
        Assert.Equal(2, sportBikes.Count);
    }
    /// <summary>
    /// Info about all clients who have ever took mountain bike ordered by fullname
    /// </summary>
    [Fact]
    public void TestMountainBikesClients()
    {
        var bikes = _fixture.Bikes;
        var types = _fixture.Types;
        var rents = _fixture.Rents;
        var clients = _fixture.Clients;
        var mountain_clients = 
            (from type in types
             where type.Name == "Mountain"
             join bike in bikes on type.Id equals bike.TypeId
             join rent in rents on bike.Id equals rent.BikeId
             join client in clients on rent.ClientId equals client.Id
             orderby client.SecondName, client.FirstName, client.Patronymic
             select client).Distinct().ToList();
        Assert.Equal(7, mountain_clients.Count);
        Assert.Equal("Arshinov", mountain_clients.First().SecondName);
    }
    /// <summary>
    /// Summed rental time for all bike types
    /// </summary>
    [Fact]
    public void TestBikeTimeRents()
    {
        var bikes = _fixture.Bikes;
        var types = _fixture.Types;
        var rents = _fixture.Rents;
        var typeRentTime =
            (from type in types
             join bike in bikes on type.Id equals bike.TypeId
             join rent in rents on bike.Id equals rent.BikeId
             group new {rent, type} by type.Id into newRents
             select new
             {
                 newRents.Key,
                 Time = TimeSpan.FromSeconds(newRents.Sum(rn => rn.rent.End.Subtract(rn.rent.Begin).TotalSeconds))
             }).ToList();
        Assert.Equal(20340, typeRentTime.First().Time.TotalSeconds, tolerance: 10);
    }
    /// <summary>
    /// Info about clients who have took rent most times
    /// </summary>
    [Fact]
    public void TestClientMaxRents()
    {
        var rents = _fixture.Rents;
        var clients = _fixture.Clients;
        var rentCountedClients =
            (from rent in rents
             group rent by rent.ClientId into newRents
             from rent in newRents
             join client in clients on rent.ClientId equals client.Id
             select new
             {
                 client.Id,
                 client.FirstName,
                 client.SecondName,
                 client.Patronymic,
                 client.PhoneNumber,
                 client.BirthDate,
                 times = newRents.Count()
             }).Distinct().ToList();
        var mostRentClients = 
            (from client in rentCountedClients
             where client.times == rentCountedClients.Max(rt => rt.times)
             select client).ToList();
        Assert.Equal(6, rentCountedClients.First().times);
    }
    /// <summary>
    /// Info about five most rented bikes
    /// </summary>
    [Fact]
    public void TestTopFiveBikes()
    {
        var rents = _fixture.Rents;
        var bikes = _fixture.Bikes;
        var bikeRent =
            (from rent in rents
             join bike in bikes on rent.BikeId equals bike.Id
             group new {rent, bike} by new {rent.BikeId, bike.Model, bike.Color} into newRents
             orderby newRents.Count() descending
             select new
             {
                 newRents.Key.BikeId,
                 newRents.Key.Model,
                 newRents.Key.Color,
                 Count = newRents.Count(),
             }).Take(5).ToList();
        Assert.Equal(4, bikeRent.First().BikeId);
    }
    /// <summary>
    /// Info about max, min and average rental time
    /// </summary>
    [Fact]
    public void TestRentTime()
    {
        var rents = _fixture.Rents;
        var max = rents.Max(r => r.End - r.Begin).TotalSeconds;
        var min = rents.Min(r => r.End - r.Begin).TotalSeconds;
        var avg = rents.Average(r => (r.End - r.Begin).TotalSeconds);
        Assert.Equal(11100, max, tolerance: 1);
        Assert.Equal(1740, min, tolerance: 1);
        Assert.Equal(6091, avg, tolerance: 1);
    }
}