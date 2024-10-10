using ShopSalesManagement.Domain;
using System.Linq;
using Xunit;

namespace ShopSalesManagement.Tests;

public class ShopSalesManagementTests : IClassFixture<ShopSalesManagementFixture>
{
    private readonly ShopSalesManagementFixture _fixture;

    public ShopSalesManagementTests(ShopSalesManagementFixture fixture)
    {
        _fixture = fixture;
    }

    [Fact]
    public void CanRetrieveAveragePriceByProductGroupForEachStore()
    {
        var averagePriceByGroup = _fixture.Products
            .GroupBy(p => p.ProductGroupId)
            .Select(g => new
            {
                ProductGroupId = g.Key,
                AveragePrice = g.Average(p => p.Price)
            })
            .ToList();
        Assert.Equal(2.75m, averagePriceByGroup.First(g => g.ProductGroupId == 1).AveragePrice);
        Assert.Equal(1.5m, averagePriceByGroup.First(g => g.ProductGroupId == 2).AveragePrice);
    }

    [Fact]
    public void CanRetrieveProductsInStore()
    {
        var storeProducts = _fixture.Products.Where(p => _fixture.Stores.Any(s => s.Id == 1)).ToList();
        Assert.NotEmpty(storeProducts);
    }

    [Fact]
    public void CanRetrieveStoresWithTotalSalesAboveThreshold()
    {
        decimal threshold = 15.0m;
        var storesAboveThreshold = _fixture.Sales
            .GroupBy(s => s.StoreId)
            .Where(g => g.Sum(s => s.TotalAmount) > threshold)
            .Select(g => g.Key)
            .ToList();
        Assert.Contains(1, storesAboveThreshold);
    }

    [Fact]
    public void CanRetrieveTop5Sales()
    {
        var topSales = _fixture.Sales
            .OrderByDescending(s => s.TotalAmount)
            .Take(5)
            .ToList();
        Assert.NotEmpty(topSales);
        Assert.Equal(20.0m, topSales.First().TotalAmount);
    }
}