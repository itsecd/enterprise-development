using DryCleaning.Tests.Services;
using DryCleaning.Domain.Seeds;

namespace DryCleaning.Tests;

/// <summary>
/// Тесты запросов по клиентам
/// </summary>
public class ClientQueriesTests(TestData data) : IClassFixture<TestData>
{
    /// <summary>
    /// Топ-5 клиентов по количеству сданных изделий за 2026 год
    /// </summary>
    [Fact]
    public void Top5ClientsAreSortedByItemCount()
    {
        var from = new DateTime(2026, 1, 1);
        var to = new DateTime(2027, 1, 1);

        int[] expectedIds = [1, 2, 3, 4, 5];

        var clients = OrderQueries.GetTop5Clients(data.Orders, from, to);

        Assert.Equal(expectedIds, clients.Select(c => c.ClientId));
    }

    /// <summary>
    /// Клиенты с самой долгой обработкой, упорядоченные по ФИО
    /// </summary>
    [Fact]
    public void ClientsWithLongestProcessingAreSortedByName()
    {
        int[] expectedIds = [4];
        
        var clients = OrderQueries.GetClientsWithLongestProcessing(data.Orders);

        Assert.Equal(expectedIds, clients.Select(c => c.ClientId));
    }

    /// <summary>
    /// Клиент с наибольшей суммой за весь период
    /// </summary>
    [Fact]
    public void TopSpendingClientIsCalculatedFromIssuedOrders()
    {
        const int expectedId = 1;
        
        var client = OrderQueries.GetTopSpendingClient(data.Orders);

        Assert.NotNull(client);
        Assert.Equal(expectedId, client.ClientId);
    }
}