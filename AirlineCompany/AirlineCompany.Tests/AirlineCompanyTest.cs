using AirlineCompany.Domain;

namespace AirlineCompany.Tests;
/// <summary>
/// Класс для тестирования запросов
/// </summary>
/// <param name="fixture"></param>
public class AirlineCompanyTest(AirlineCompanyFixture fixture): IClassFixture<AirlineCompanyFixture>
{
    private readonly AirlineCompanyFixture _fixture = fixture;

    /// <summary>
    ///  1) Вывести сведения о всех авиарейсах, вылетевших из указанного пункта отправления
    ///  в указанный пункт прибытия.
    /// </summary>
    [Fact]
    public void TestFlyightDepartureArrive()
    {
        var flyFixture = _fixture.AirFlights;

        var departure = "Tokio";
        var arrive = "Dublin";

        var flyightDepartureArrive =
            (from fly in flyFixture
             where fly.DeparturePoint == departure && fly.ArrivalPoint == arrive
             select fly).ToList();

        Assert.True(flyightDepartureArrive.Count() != 0);
        Assert.Equal(flyFixture[0], flyightDepartureArrive[0]);
    }


    /// <summary>
    /// 2) Вывести сведения обо всех пассажирах, летящих данным рейсом,
    /// вес багажа которых равен нулю, упорядочить по ФИО.
    /// </summary>
    [Fact]
    public void TestPassenegersWeightFlight()
    {
        var passFixture = _fixture.Passengers;

        var expected = new List<Passeneger>() { passFixture[8], passFixture[0]};
        var idFlight = 4;

        var passenegersWeightFlight =
            (from pass in passFixture
             orderby pass.FullName descending
             where pass.IdFlight == idFlight && pass.BaggageWeight == 0
             select pass).ToList();

        Assert.True(passenegersWeightFlight.Count() != 0);
        Assert.Equal(expected, passenegersWeightFlight);
    }


    /// <summary>
    /// 3) Вывести сводную информацию обо всех полетах самолетов данного типа
    /// в указанный период времени.
    /// </summary>
    [Fact]
    public void TestFlyightPassengersDate()
    {
        var flyFixture = _fixture.AirFlights;

        var expected = new List<AirFlight>() { flyFixture[1], flyFixture[2] };
        var planeModel = "Panda 202208";
        var departure = new DateTime(2024, 9, 1);
        var arrive = new DateTime(2024, 11, 30);

        var flyightPassengersDate =
            from fly in flyFixture
            where fly.PlaneType == planeModel &&
            fly.Departure >= departure &&
            fly.Departure <= arrive
            select fly;

        Assert.True(flyightPassengersDate.Count() != 0);
        Assert.Equal(expected, flyightPassengersDate);
    }


    /// <summary>
    /// 4) Вывести топ 5 авиарейсов по количеству перевезённых пассажиров.
    /// </summary>
    [Fact]
    public void TestFlyightTopPassengers()
    {
        var flyFixture = _fixture.AirFlights;
        var passFixture = _fixture.Passengers;

        var expected = new[]
        {
            new { Fly = flyFixture[0], Count = 3 },
            new { Fly = flyFixture[2], Count = 3 },
            new { Fly = flyFixture[4], Count = 3 },
            new { Fly = flyFixture[6], Count = 3 },
            new { Fly = flyFixture[8], Count = 3 },
        };

        var flyightTopPassengers =
            (from fly in flyFixture
             let c = passFixture.Count(pass => pass.IdFlight == fly.Idflight)
             orderby c descending
             select new 
             { 
                 Fly = fly, 
                 Count = c 
             
             }).Take(5).ToList();


        Assert.True(flyightTopPassengers.Count() != 0);
        Assert.Equal(expected, flyightTopPassengers);
    }


    /// <summary>
    /// 5) Вывести список рейсов с минимальным временем в пути.
    /// </summary>
    [Fact]
    public void TestFlyightMinTime()
    {
        var flyFixture = _fixture.AirFlights;

        var flyightMinTime =
            (from fly in flyFixture
             let minTime = flyFixture.Min(pass => pass.FlyingTime)
            where fly.FlyingTime == minTime
            select fly).ToList();

        Assert.True(flyightMinTime.Count() != 0);
        Assert.Equal(flyFixture[4], flyightMinTime[0]);
    }


    /// <summary>
    /// 6) Вывести информацию о средней и максимальной загрузке авиарейсов
    /// из заданного пункта отправления.
    /// </summary>
    [Fact]
    public void TestFlyightMaxAvrWeight() 
    {
        var flyFixture = _fixture.AirFlights;
        var passFixture = _fixture.Passengers;

        var departure = "Rome";

        var flightWeight =
            from fly in flyFixture
            join pass in passFixture on fly.Idflight equals pass.IdFlight
            where fly.DeparturePoint == departure
            select new
            {
                Fly = fly,
                Bag = pass.BaggageWeight,
            };

        var maxW = flightWeight.Max(f => f.Bag);
        var avgW = flightWeight.Average(f => f.Bag);

        Assert.Equal(8.4, maxW, 1e4);
        Assert.Equal(5.8857, avgW, 1e4);

    }
}
