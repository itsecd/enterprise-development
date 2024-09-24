using BikeRent.Tests;
namespace BikeRent.Tests;
/// <summary>
/// Юнит тест
/// </summary>
/// <param name="fixture"></param>
public class BikeRentTest(BikeRentFixture fixture) : IClassFixture<BikeRentFixture>
{
    private BikeRentFixture _fixture = fixture;
    /// <summary>
    /// Информация обо всех спортивных велосипедах
    /// </summary>
    [Fact]
    public void TestSelectSportBikes()
    {
        var bikes = _fixture.Bikes.ToList();
        var types = _fixture.Types.ToList();
        var sport_bikes =
        (from type in types
         where type.Name.Equals("Спортивный")
         join bike in bikes on type.TypeId equals bike.TypeId
         select bike).ToList();
        Assert.Equal(2, sport_bikes.Count);
    }
    /// <summary>
    /// Упорядоченая по ФИО информация обо всех клиентах, которые брали в аренду горные велосипеды
    /// </summary>
    [Fact]
    public void TestMountainBikesClients()
    {
        var bikes = _fixture.Bikes.ToList();
        var types = _fixture.Types.ToList();
        var rents = _fixture.Rents.ToList();
        var clients = _fixture.Clients.ToList();
        var mountain_clients = 
            (from type in types
             where type.Name.Equals("Горный")
             join bike in bikes on type.TypeId equals bike.TypeId
             join rent in rents on bike.TypeId equals rent.BikeId
             join client in clients on rent.ClientId equals client.ClientId
             orderby client.ClientSecondName
             select client).Distinct().ToList();
        Assert.Equal(4, mountain_clients.Count);
        Assert.Equal("Аршинов", mountain_clients.First().ClientSecondName);
    }
    /// <summary>
    /// Суммарное время аренды для велосипеда каждого типа
    /// </summary>
    [Fact]
    public void TestBikeTimeRents()
    {
        var bikes = _fixture.Bikes.ToList();
        var types = _fixture.Types.ToList();
        var rents = _fixture.Rents.ToList();
        foreach( var type in types)
        {
            var type_time =
                (from bike in bikes
                where bike.TypeId == type.TypeId
                join rent in rents on bike.BikeId equals rent.BikeId
                select new
                {
                    type = type.Name,
                    type_id = bike.TypeId,
                    time = rent.End - rent.Begin
                }).ToList();
            var single_type_time = new TimeSpan();
            foreach(var tm in type_time)
            {
                single_type_time += tm.time;
            }
            Assert.True(single_type_time > new TimeSpan(1, 0, 0));
        }
    }
    /// <summary>
    /// Информация о клиентах, бравших велосипеды на прокат больше всего раз
    /// </summary>
    [Fact]
    public void TestClientMaxRents()
    {
        var rents = _fixture.Rents.ToList();
        var clients = _fixture.Clients.ToList();
        foreach (var client in clients)
        {
            var client_rent_count =
                (from rent in rents
                where rent.ClientId == client.ClientId
                select rent).Count();
            Assert.True(client_rent_count > 1);
        }
    }
    /// <summary>
    /// Информация о пяти наиболее често арендуемых велосипедах
    /// </summary>
    [Fact]
    public void TestTopFiveBikes()
    {
        var rents = _fixture.Rents.ToList();
        var bike_rent =
            (from rent in rents
             group rent by rent.BikeId into new_rents
             from new_rent in new_rents
             select new
             {
                 new_rent.BikeId,
                 cnt = new_rents.Count(),
             }).Distinct().ToList().Slice(0, 5);
        Assert.Equal(4, bike_rent.First().BikeId);
    }
    /// <summary>
    /// Информация о максимальном, среднем и минимальном времени аренды
    /// </summary>
    [Fact]
    public void TestRentTime()
    {
        var rents = _fixture.Rents.ToList();
        var rent_time_max =
            (from rent in rents
             select new
             {
                 time = (rent.End - rent.Begin).TotalSeconds
             }).ToList().Max(tm => tm.time);
        var rent_time_avg =
            (from rent in rents
             select new
             {
                 time = (rent.End - rent.Begin).TotalSeconds
             }).ToList().Average(tm => tm.time);
        var rent_time_min =
            (from rent in rents
             select new
             {
                 time = (rent.End - rent.Begin).TotalSeconds
             }).ToList().Min(tm => tm.time);
        Assert.True(rent_time_max > 7200);
        Assert.True(rent_time_min < 3600);
        Assert.True(rent_time_avg > 3600);
    }
}