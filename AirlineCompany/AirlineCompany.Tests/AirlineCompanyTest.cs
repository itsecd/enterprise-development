using AirlineCompany.Domain;
using System.Globalization;

namespace AirlineCompany.Tests;
public class AirlineCompanyTest(AirlineCompanyFixture fixture): IClassFixture<AirlineCompanyFixture>
{
    private AirlineCompanyFixture _fixture = fixture;

    /// <summary>
    ///  1) Вывести сведения о всех авиарейсах, вылетевших из указанного пункта отправления
    ///  в указанный пункт прибытия.
    /// </summary>
    [Fact]
    public void TestFlyightDA()
    {
        var fixture = new AirlineCompanyFixture();
        var flyFixture = fixture.GetFlights();
        var flyightDA =
            (from fly in flyFixture
             where fly.DeparturePoint == "Tokio" && fly.ArrivalPoint == "Dublin"
            select fly).ToList();

        Assert.True(flyightDA.Any());
        Assert.Equal(flyFixture[0], flyightDA[0]);
    }


    /// <summary>
    /// 2) Вывести сведения обо всех пассажирах, летящих данным рейсом,
    /// вес багажа которых равен нулю, упорядочить по ФИО.
    /// </summary>
    [Fact]
    public void TestPassWF()
    {
        var fixture = new AirlineCompanyFixture();
        var passFixture = fixture.GetPassenegers();

        var passWF =
            (from pass in passFixture
             orderby pass.FIO descending
             where pass.сodeFlight == "5000" && pass.baggageWeight == 0
             select pass).ToList();

        var res = new List<Passeneger>() { passFixture[8], passFixture[0]};

        Assert.True(passWF.Any());
        Assert.Equal(res, passWF);
    }


    /// <summary>
    /// 3) Вывести сводную информацию обо всех полетах самолетов данного типа
    /// в указанный период времени.
    /// </summary>
    [Fact]
    public void TestFlyightPT()
    {
        var fixture = new AirlineCompanyFixture();
        var flyFixture = fixture.GetFlights();

        var flyightPT =
            from fly in flyFixture
            where fly.PlaneType == "Panda 202208" &&
            fly.Departure >= DateTime.Parse("2024-09-01") &&
            fly.Departure <= DateTime.Parse("2024-11-30")
            select fly;

        var res = new List<AirFlight>() { flyFixture[1], flyFixture[2] };
        Assert.True(flyFixture.Any());
        Assert.Equal(res, flyightPT);
    }


    /// <summary>
    /// 4) Вывести топ 5 авиарейсов по количеству перевезённых пассажиров.
    /// </summary>
    [Fact]
    public void TestFlyightTop()
    {
        var fixture = new AirlineCompanyFixture();
        var passFixture = fixture.GetPassenegers();
        var flyFixture = fixture.GetFlights();

        var flyightTop =
            (from fly in flyFixture
             let c = passFixture.Count(pass => pass.codeFlight == fly.CodeNumber)
             orderby c descending
             select new { fly, c }).ToList();

        var res = new List<(AirFlight, int)>()
        {
            (flyFixture[0], 3),
            (flyFixture[2], 3),
            (flyFixture[4], 3),
            (flyFixture[6], 3),
            (flyFixture[8], 3),
        };

        Assert.True(flyightTop.Any());
        for (var i = 0; i < 5; i++)
        {
            Assert.Equal(res[0], (flyightTop[0].fly, flyightTop[0].c));
        }
       
    }


    /// <summary>
    /// 5) Вывести список рейсов с минимальным временем в пути.
    /// </summary>
    [Fact]
    public void TestFlyightTime()
    {
        var fixture = new AirlineCompanyFixture();
        var flyFixture = fixture.GetFlights();

        var flyightTime =
            (from fly in flyFixture
            let minTime = flyFixture.Min(pass => pass.FlyingTime)
            where fly.FlyingTime == minTime
            select fly).ToList();

        Assert.True(flyightTime.Any());
        Assert.Equal(flyFixture[4], flyightTime[0]);
    }


    /// <summary>
    /// 6) Вывести информацию о средней и максимальной загрузке авиарейсов
    /// из заданного пункта отправления.
    /// </summary>
    [Fact]
    public void TestFlyightWeight() 
    {
        var fixture = new AirlineCompanyFixture();
        var passFixture = fixture.GetPassenegers();
        var flyFixture = fixture.GetFlights();

        var flyightWeight =
            from fly in flyFixture
            join pass in passFixture on fly.CodeNumber equals pass.codeFlight
            where fly.DeparturePoint == "Rome"
            select new
            {
                id = fly.Idflight,
                code = fly.CodeNumber,
                depar = fly.DeparturePoint,
                arrive = fly.ArrivalPoint,
                bag = pass.baggageWeight,
            };

        var flyightWMA =
            (from fly in flyightWeight
             let maxW = (from f in flyightWeight
                         select f.bag).Max()
             let avgW = (from f in flyightWeight
                         select f.bag).Average()
             select new{ maxW, avgW }).Distinct().ToList();

        Assert.True(flyightWMA.Any());
        Assert.Equal("8,4", flyightWMA[0].maxW.ToString());
        Assert.Equal("5,8857145", flyightWMA[0].avgW.ToString());
    }
}
