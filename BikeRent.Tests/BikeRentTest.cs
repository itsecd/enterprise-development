using Xunit.Sdk;
namespace BikeRent.Tests;
public class BikeRentTest(BikeRentFixture fixture) : IClassFixture<BikeRentFixture>
{
    private readonly BikeRentFixture _fixture = fixture;
    [Fact]
    /// <summary>
    /// Info about all sport bikes
    /// </summary>
    public void TestSelectSportBikes()
    {
        var Bikes = _fixture.Bikes;
        var Types = _fixture.Types;
        var SportBikes =
        (from Type in Types
         where Type.Name == "Sport"
         join Bike in Bikes on Type.Id equals Bike.TypeId
         select Bike).ToList();
        Assert.Equal(2, SportBikes.Count);
    }
    /// <summary>
    /// Info about all clients who have ever took mountain bike ordered by fullname
    /// </summary>
    [Fact]
    public void TestMountainBikesClients()
    {
        var Bikes = _fixture.Bikes;
        var Types = _fixture.Types;
        var Rents = _fixture.Rents;
        var Clients = _fixture.Clients;
        var mountain_clients = 
            (from type in Types
             where type.Name == "Mountain"
             join bike in Bikes on type.Id equals bike.TypeId
             join rent in Rents on bike.TypeId equals rent.BikeId
             join Client in Clients on rent.ClientId equals Client.Id
             orderby Client.Patronymic
             orderby Client.FirstName
             orderby Client.SecondName
             select Client).Distinct().ToList();
        Assert.Equal(4, mountain_clients.Count());
        Assert.Equal("Arshinov", mountain_clients.First().SecondName);
    }
    /// <summary>
    /// Summed rental time for all bike types
    /// </summary>
    [Fact]
    public void TestBikeTimeRents()
    {
        var Bikes = _fixture.Bikes;
        var Types = _fixture.Types;
        var Rents = _fixture.Rents;
        var TypeRentTime =
            (from Type in Types
             join Bike in Bikes on Type.Id equals Bike.TypeId
             join Rent in Rents on Bike.Id equals Rent.BikeId
             group new {Rent, Type} by Type.Id into NewRents
             from Rnt in NewRents
             select new
             {
                 Rnt.Type.Id,
                 Time = TimeSpan.FromSeconds(NewRents.Sum(rn => rn.Rent.End.Subtract(rn.Rent.Begin).TotalSeconds))
             }).Distinct().ToList();
        Assert.Equal(3, TypeRentTime.Count());
    }
    /// <summary>
    /// Info about clients who have took rent most times
    /// </summary>
    [Fact]
    public void TestClientMaxRents()
    {
        var Rents = _fixture.Rents;
        var Clients = _fixture.Clients;
        var MostRentClients =
            (from Rent in Rents
             group Rent by Rent.ClientId into NewRents
             from Rent in NewRents
             join Client in Clients on Rent.ClientId equals Client.Id
             select new
             {
                 Client.Id,
                 Client.FirstName,
                 Client.SecondName,
                 Client.Patronymic,
                 Client.PhoneNumber,
                 Client.BirthDate,
                 times = NewRents.Count()
             }).Distinct().ToList();
        Assert.Equal(7, MostRentClients.Count());
    }
    /// <summary>
    /// Info about five most rented bikes
    /// </summary>
    [Fact]
    public void TestTopFiveBikes()
    {
        var Rents = _fixture.Rents;
        var Bikes = _fixture.Bikes;
        var BikeRent =
            (from Rent in Rents
             join Bike in Bikes on Rent.BikeId equals Bike.Id
             group new {Rent, Bike} by new {Rent.BikeId, Bike.Model, Bike.Color} into NewRents
             orderby NewRents.Count() descending
             select new
             {
                 NewRents.Key.BikeId,
                 NewRents.Key.Model,
                 NewRents.Key.Color,
                 Count = NewRents.Count(),
             }).Take(5).ToList();
        Assert.Equal(4, BikeRent.First().BikeId);
    }
    /// <summary>
    /// Info about max, min and average rental time
    /// </summary>
    [Fact]
    public void TestRentTime()
    {
        var Rents = _fixture.Rents;
        var Max = Rents.Max(r => r.End - r.Begin).TotalSeconds;
        var Min = Rents.Min(r => r.End - r.Begin).TotalSeconds;
        var Avg = Rents.Average(r => (r.End - r.Begin).TotalSeconds);
        Assert.True(Max > 7200);
        Assert.True(Min < 3600);
        Assert.True(Avg > 3600);
    }
}