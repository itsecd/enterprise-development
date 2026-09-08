using AutoService.Tests.Fixtures;
using Xunit;

namespace AutoService.Tests;

public class DomainTests
    : IClassFixture<AutoServiceFixture>
{
    private readonly AutoServiceFixture _fixture;


    public DomainTests(
        AutoServiceFixture fixture)
    {
        _fixture = fixture;
    }


    [Fact]
    public void Seeder_Should_Create_Enough_Data()
    {
        var context = _fixture.Context;


        Assert.True(context.Clients.Count >= 10);

        Assert.True(context.Cars.Count >= 10);

        Assert.True(context.Mechanics.Count >= 10);

        Assert.True(context.WorkTypes.Count >= 10);

        Assert.True(context.RepairOrders.Count >= 10);
    }

    [Fact]
    public void Seeder_Should_Create_Cars_With_Clients()
    {
        var context = _fixture.Context;

        var carsWithoutClient =
            context.Cars
                .Count(car => car.Client == null);


        Assert.Equal(0, carsWithoutClient);
    }

    [Fact]
    public void Seeder_Should_Create_RepairOrders_With_Relations()
    {
        var context = _fixture.Context;


        var invalidOrders =
            context.RepairOrders
                .Count(order =>
                    order.Car == null ||
                    order.Client == null);


        Assert.Equal(0, invalidOrders);
    }

    [Fact]
    public void RepairOrders_Should_Have_Mechanics()
    {
        var context = _fixture.Context;


        var ordersWithoutMechanics =
            context.RepairOrders
                .Count(order =>
                    order.Mechanics.Count == 0);


        Assert.Equal(0, ordersWithoutMechanics);
    }

    [Fact]
    public void RepairOrders_Should_Have_Works()
    {
        var context = _fixture.Context;


        var ordersWithoutWorks =
            context.RepairOrders
                .Count(order =>
                    order.Works.Count == 0);


        Assert.Equal(0, ordersWithoutWorks);
    }
}