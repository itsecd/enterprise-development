using RealEstateAgency.Domain;
using Xunit;
using System.Linq;

namespace RealEstateAgency.Tests;

public class RealEstateAgencyQueryTests
{
    private List<Client> testClients = TestData.Clients;
    private List<Order> testOrders = TestData.Orders;
    private List<RealEstate> testRealEstates = TestData.RealEstates;

    [Fact]
    public void GetClientsSearchingForSpecificRealEstateType_ShouldReturnOrderedByFullName()
    {
        var realEstateType = RealEstate.PropertyType.Residential;

        var clients = testClients
            .Where(c => testOrders.Any(o => o.Client == c && o.Type == Order.PurchaseOrSale.Purchase && o.Item.Type == realEstateType))
            .OrderBy(c => c.FirstAndLastName)
            .ToList();

        Assert.NotEmpty(clients);
        Assert.Equal("Chukarev Michail", clients[0].FirstAndLastName);
    }

    [Fact]
    public void GetSellersWithinPeriod_ShouldReturnCorrectSellers()
    {
        var startDate = new DateTime(2024,1,1);
        var endDate = new DateTime(2024, 11, 1);

        var sellers = testClients
            .Where(c => testOrders.Any(o => o.Client == c && o.Type == Order.PurchaseOrSale.Sale && o.Time >= startDate && o.Time <= endDate))
            .ToList();

        Assert.NotEmpty(sellers);
        Assert.Contains(sellers, s => s.FirstAndLastName == "Chukarev Michail");
        Assert.Contains(sellers, s => s.FirstAndLastName == "Stepanov Dima");
    }

    //править
    [Fact]
    public void GetSellersForBuyerOrder_ShouldReturnSellersWithMatchingRealEstate()
    {
        var buyerOrder = testOrders
            .First(o => o.Type == Order.PurchaseOrSale.Purchase && o.Item.Type == RealEstate.PropertyType.Residential );

        var sellers = testClients
            .Join(testOrders.Where(o => o.Type == Order.PurchaseOrSale.Sale && o.Item.Type == buyerOrder.Item.Type && o.Price == buyerOrder.Price),
                  seller => seller.ClientId,
                  saleOrder => saleOrder.Client.ClientId,
                  (seller, saleOrder) => seller)
            .Distinct()
            .ToList();

        Assert.NotEmpty(sellers);
        Assert.Equal("Stepanov Dima", sellers[0].FirstAndLastName);
    }

    [Fact]
    public void GetOrderCountByRealEstateType_ShouldReturnCorrectCounts()
    {
        var orderCountByType = testOrders
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

        Assert.Equal(5, residentialCount?.OrderCount);
        Assert.Equal(1, commercialCount?.OrderCount);
    }

    [Fact]
    public void GetTop5ClientsByOrderCount_ShouldReturnTop5Clients()
    {
        var purchaseOrders = testClients
            .OrderByDescending(c => testOrders.Count(o => o.Client == c && o.Type == Order.PurchaseOrSale.Purchase))
            .Take(5)
            .ToList();

        var saleOrders = testClients
            .OrderByDescending(c => testOrders.Count(o => o.Client == c && o.Type == Order.PurchaseOrSale.Sale))
            .Take(5)
            .ToList();

        Assert.NotEmpty(purchaseOrders);
        Assert.True(purchaseOrders.Count <= 5);
        Assert.Equal("Chukarev Michail", purchaseOrders[0].FirstAndLastName);

        Assert.NotEmpty(saleOrders);
        Assert.True(saleOrders.Count <= 5);
        Assert.Equal("Chukarev Michail", saleOrders[0].FirstAndLastName);
    }

    [Fact]
    public void GetClientsWithMinOrderPrice_ShouldReturnClientsWithMinPrice()
    {
        var minPrice = testOrders.Min(o => o.Price);

        var clientsWithMinPrice = testClients
            .Where(c => testOrders.Any(o => o.Client == c && o.Price == minPrice))
            .ToList();

        Assert.NotEmpty(clientsWithMinPrice);
        Assert.Equal(500000, minPrice);
        Assert.Contains(clientsWithMinPrice, c => c.FirstAndLastName == "Ivanov Ivan");
    }
}
