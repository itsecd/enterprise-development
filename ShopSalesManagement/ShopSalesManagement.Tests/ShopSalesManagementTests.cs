using ShopSalesManagement.Domain;

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
        Assert.Equal(3.25m, averagePriceByGroup.First(g => g.ProductGroupId == 2).AveragePrice);
    }

    [Fact]
    public void CanRetrieveProductsInStore()
    {
        int storeId = 1;
        var storeProducts = _fixture.Stocks
            .Where(stock => stock.StoreId == storeId)
            .Join(_fixture.Products, stock => stock.ProductId, product => product.Id, (stock, product) => product)
            .ToList();
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

    [Fact]
    public void CanRetrieveStoresWithProductInStock()
    {
        int productId = 1;
        var storesWithProduct = _fixture.Stocks
            .Where(stock => stock.ProductId == productId && stock.Quantity > 0)
            .Join(_fixture.Stores, stock => stock.StoreId, store => store.Id, (stock, store) => store)
            .ToList();
        Assert.NotEmpty(storesWithProduct);
        Assert.Equal("MiniMarket", storesWithProduct.First().Name);
    }

    [Fact]
    public void CanRetrieveExpiredProductsWithStore()
    {
        var expiredProductsWithStore = (from product in _fixture.Products
                                        join stock in _fixture.Stocks on product.Id equals stock.ProductId
                                        join store in _fixture.Stores on stock.StoreId equals store.Id
                                        where product.ExpirationDate < DateTime.Today
                                        select new { Product = product, Store = store })
                                        .ToList();
        Assert.NotEmpty(expiredProductsWithStore);
        Assert.Equal("Cheese", expiredProductsWithStore.First().Product.Name);
        Assert.Equal("MiniMarket", expiredProductsWithStore.First().Store.Name);
    }
}
