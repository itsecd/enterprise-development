using AirlineCompany.Domain;

namespace AirlineCompany.Tests;
/// <summary>
/// Класс для тестирования запросов
/// </summary>
/// <param name="fixture"></param>
public class AirlineCompanyTest(AirlineCompanyFixture fixture): IClassFixture<AirlineCompanyFixture>
{
    private AirlineCompanyFixture _fixture = fixture;

    /// <summary>
    ///  1) Вывести сведения о всех авиарейсах, вылетевших из указанного пункта отправления
    ///  в указанный пункт прибытия.
    /// </summary>
    [Fact]
    public void TestFlyightDepartureArrive()
    {
        var fixture = new AirlineCompanyFixture();
        var flyFixture = fixture.GetFlights();

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
        var fixture = new AirlineCompanyFixture();
        var passFixture = fixture.GetPassenegers();

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
        var fixture = new AirlineCompanyFixture();
        var flyFixture = fixture.GetFlights();

        var expected = new List<AirFlight>() { flyFixture[1], flyFixture[2] };
        var planeModel = "Panda 202208";
        var departure = DateTime.Parse("2024-09-01");
        var arrive = DateTime.Parse("2024-11-30");

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
        var fixture = new AirlineCompanyFixture();
        var flyFixture = fixture.GetFlights();
        var passFixture = fixture.GetPassenegers(); 

        var exprcted = new List<(AirFlight, int)>()
        {
            (flyFixture[0], 3),
            (flyFixture[2], 3),
            (flyFixture[4], 3),
            (flyFixture[6], 3),
            (flyFixture[8], 3),
        };

        var flyightTopPassengers =
            (from fly in flyFixture
             let c = passFixture.Count(pass => pass.IdFlight == fly.Idflight)
             orderby c descending
             select new { fly, c }).Take(5).ToList();


        Assert.True(flyightTopPassengers.Count() != 0);
        for (var i = 0; i < 5; i++)
        {
            Assert.Equal(exprcted[0], (flyightTopPassengers[0].fly, flyightTopPassengers[0].c));
        }
       
    }


    /// <summary>
    /// 5) Вывести список рейсов с минимальным временем в пути.
    /// </summary>
    [Fact]
    public void TestFlyightMinTime()
    {
        var fixture = new AirlineCompanyFixture();
        var flyFixture = fixture.GetFlights();

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
        var fixture = new AirlineCompanyFixture();
        var flyFixture = fixture.GetFlights();
        var passFixture = fixture.GetPassenegers();

        var departure = "Rome";

        var flightWeight =
            from fly in flyFixture
            join pass in passFixture on fly.Idflight equals pass.IdFlight
            where fly.DeparturePoint == departure
            select new
            {
                fly,
                bag = pass.BaggageWeight,
            };

        var maxW = flightWeight.Max(f => f.bag);
        var avgW = flightWeight.Average(f => f.bag);

        Assert.True(maxW.Equals((double)8.4));
        Assert.True(avgW.Equals((double)5.8857142857142852));

    }
}
