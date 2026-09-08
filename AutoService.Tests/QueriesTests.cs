using AutoService.Domain.Enums;
using AutoService.Tests.Fixtures;
using Xunit;


namespace AutoService.Tests;


public class QueriesTests
    : IClassFixture<AutoServiceFixture>
{
    private readonly AutoServiceFixture _fixture;


    public QueriesTests(
        AutoServiceFixture fixture)
    {
        _fixture = fixture;
    }


    [Fact]
    public void Return_Mechanics_By_Work_Type()
    {
        var context = _fixture.Context;


        var specialization =
            MechanicSpecialization.Engine;


        var mechanics =
            context.Mechanics
                .Where(mechanic =>
                    mechanic.Specialization == specialization)
                .ToList();


        Assert.NotEmpty(mechanics);


        Assert.All(
            mechanics,
            mechanic =>
                Assert.Equal(
                    specialization,
                    mechanic.Specialization));
    }


    [Fact]
    public void Return_Clients_By_Mechanic()
    {
        var context = _fixture.Context;


        var mechanic =
            context.Mechanics
                .First();


        var clients =
            context.RepairOrders
                .Where(order =>
                    order.Mechanics
                        .Any(orderMechanic =>
                            orderMechanic.MechanicId == mechanic.Id))
                .Select(order =>
                    order.Client)
                .Distinct()
                .OrderBy(client =>
                    client.FullName)
                .ToList();


        Assert.NotEmpty(clients);


        Assert.True(
            clients
                .SequenceEqual(
                    clients.OrderBy(x => x.FullName)));
    }


    [Fact]
    public void Count_Repeated_Client_Requests_Last_Month()
    {
        var context = _fixture.Context;


        var monthAgo =
            DateTime.Now.AddMonths(-1);


        var repeatedClients =
            context.RepairOrders
                .Where(order =>
                    order.AdmissionDate >= monthAgo)
                .GroupBy(order =>
                    order.ClientId)
                .Where(group =>
                    group.Count() > 1)
                .Select(group =>
                    new
                    {
                        ClientId = group.Key,
                        RequestsCount = group.Count()
                    })
                .ToList();


        Assert.NotEmpty(repeatedClients);


        Assert.All(
            repeatedClients,
            client =>
                Assert.True(client.RequestsCount > 1));
    }


    [Fact]
    public void Calculate_Total_Cost_For_Order()
    {
        var context = _fixture.Context;


        var order =
            context.RepairOrders
                .First();


        var totalCost =
            order.Works
                .Sum(work =>
                    work.WorkType.Cost);


        var expectedCost =
            order.Works
                .Select(work =>
                    work.WorkType.Cost)
                .Sum();


        Assert.Equal(
            expectedCost,
            totalCost);


        Assert.True(
            totalCost > 0);
    }


    [Fact]
    public void Return_Top_5_Most_Frequent_Work_Types()
    {
        var context = _fixture.Context;


        var topWorks =
            context.RepairOrders
                .SelectMany(order =>
                    order.Works)
                .GroupBy(orderWork =>
                    orderWork.WorkType)
                .Select(group =>
                    new
                    {
                        WorkType = group.Key,
                        Count = group.Count()
                    })
                .OrderByDescending(x =>
                    x.Count)
                .Take(5)
                .ToList();


        Assert.Equal(
            5,
            topWorks.Count);


        Assert.All(
            topWorks,
            work =>
                Assert.True(work.Count > 0));


        Assert.True(
            topWorks
                .SequenceEqual(
                    topWorks
                        .OrderByDescending(x =>
                            x.Count)));
    }
}