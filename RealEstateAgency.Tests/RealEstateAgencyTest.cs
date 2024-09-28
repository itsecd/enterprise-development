using RealEstateAgency.Domain;

public class RealEstateAgencyQueryTests
{
    private List<Client> _testClients = TestData.GetTestClients();

    [Fact]
    public void GetClientsSearchingForSpecificRealEstateType_ShouldReturnOrderedByFullName()
    {
        var realEstateType = RealEstate.PropertyType.Residential;

        var clients = _testClients
            .Where(c => c.Orders.Any(o => o.Type == Order.PurchaseOrSale.Purchase && o.Item.Type == realEstateType))
            .OrderBy(c => c.FirstAndLastName)
            .ToList();

        Assert.NotEmpty(clients);
        Assert.Equal("Chukarev Michail", clients[0].FirstAndLastName);
    }

    [Fact]
    public void GetSellersWithinPeriod_ShouldReturnCorrectSellers()
    {
        var startDate = DateTime.Now.AddMonths(-6);
        var endDate = DateTime.Now;

        var sellers = _testClients
            .Where(c => c.Orders.Any(o => o.Type == Order.PurchaseOrSale.Sale && o.Time >= startDate && o.Time <= endDate))
            .ToList();

        Assert.NotEmpty(sellers);
        Assert.Contains(sellers, s => s.FirstAndLastName == "Chukarev Michail");
        Assert.Contains(sellers, s => s.FirstAndLastName == "Stepanov Dima");
    }

    [Fact]
    public void GetSellersForBuyerOrder_ShouldReturnSellersWithMatchingRealEstate()
    {
        var buyerOrder = _testClients
            .SelectMany(c => c.Orders)
            .First(o => o.Type == Order.PurchaseOrSale.Purchase && o.Item.Type == RealEstate.PropertyType.Residential);

        var sellers = _testClients
            .Where(c => c.Orders.Any(o => o.Type == Order.PurchaseOrSale.Sale && o.Item.Type == buyerOrder.Item.Type))
            .ToList();

        Assert.NotEmpty(sellers);
        Assert.Equal("Stepanov Dima", sellers[0].FirstAndLastName);
    }

    [Fact]
    public void GetOrderCountByRealEstateType_ShouldReturnCorrectCounts()
    {
        var orderCountByType = _testClients
            .SelectMany(c => c.Orders)
            .GroupBy(o => o.Item.Type)
            .Select(g => new
            {
                RealEstateType = g.Key,
                OrderCount = g.Count()
            })
            .ToList();

        Assert.NotEmpty(orderCountByType);
        var residentialCount = orderCountByType.FirstOrDefault(t => t.RealEstateType == RealEstate.PropertyType.Residential);
        var commercialCount = orderCountByType.FirstOrDefault(t => t.RealEstateType == RealEstate.PropertyType.Uninhabitable);

        Assert.Equal(5, residentialCount.OrderCount);
        Assert.Equal(1, commercialCount.OrderCount);
    }

    [Fact]
    public void GetTop5ClientsByOrderCount_ShouldReturnTop5Clients()
    {
        var topClients = _testClients
            .OrderByDescending(c => c.Orders.Count)
            .Take(5)
            .ToList();

        Assert.NotEmpty(topClients);
        Assert.True(topClients.Count <= 5);
        Assert.Equal("Chukarev Michail", topClients[0].FirstAndLastName);
    }

    [Fact]
    public void GetClientsWithMinOrderPrice_ShouldReturnClientsWithMinPrice()
    {
        var minPrice = _testClients.SelectMany(c => c.Orders).Min(o => o.Price);

        var clientsWithMinPrice = _testClients
            .Where(c => c.Orders.Any(o => o.Price == minPrice))
            .ToList();

        Assert.NotEmpty(clientsWithMinPrice);
        Assert.Equal(450000, minPrice);
        Assert.Contains(clientsWithMinPrice, c => c.FirstAndLastName == "Chukarev Michail");
    }
}
