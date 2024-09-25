using StoreCashFlow.Domain;
using Xunit;

namespace StoreCashFlow.Tests;

public class StoreCashTests(StoreCashFixture fixture) : IClassFixture<StoreCashFixture>
{
    private readonly StoreCashFixture _fixture = fixture;
   
    [Fact]
    public void ReturnAllProductsInStore()
    {
        var storeId = 1;
        var expectedProducts = new List<Product>
        {
            _fixture.Products[2],
            _fixture.Products[3],
            _fixture.Products[6],
            _fixture.Products[7],
            _fixture.Products[9],
            _fixture.Products[0]
        };

        var resultProducts = _fixture.ProductAvailabilities
            .Where(pa => pa.Store.StoreId == storeId)
            .Select(pa => pa.Product)
            .ToList();

        Assert.Equal(expectedProducts, resultProducts);
    }

    [Fact]
    public void ReturnStoresWithProductInStock()
    {
        var productId = _fixture.Products[0].Barcode;
        var expectedStores = new List<Store>
        {
            _fixture.Stores[0],
            _fixture.Stores[1]
        };

        var resultStores = _fixture.ProductAvailabilities
            .Where(pa => pa.Product.Barcode == productId)
            .Select(pa => pa.Store)
            .ToList();

        Assert.Equal(expectedStores, resultStores);
    }

    [Fact]
    public void ReturnAveragePriceByGroupAndStore()
    {
        var avgPrices = _fixture.ProductAvailabilities
            .GroupBy(pa => new { pa.Store.StoreId, pa.Product.ProductGroupCode })
            .Select(group => new
            {
                StoreId = group.Key.StoreId,
                ProductGroupCode = group.Key.ProductGroupCode,
                AvgPrice = group.Average(pa => pa.Product.Price)
            })
            .ToList();

        Assert.NotEmpty(avgPrices);
        Assert.All(avgPrices, item => Assert.True(item.AvgPrice > 0));
    }

    [Fact]
    public void ReturnTop5SalesByTotalAmount()
    {
        var topSales = _fixture.Sales
                .OrderByDescending(s => s.Product.Price * s.Quantity)
                .Take(5)
                .ToList();

        Assert.NotEmpty(topSales);
        Assert.True(topSales.Count <= 5);
        Assert.All(topSales, sale => Assert.True(sale.Quantity > 0));
    }

    [Fact]
    public void ReturnExpiredProducts()
    {
        var today = DateTime.Today;

        var expiredProducts = _fixture.ProductAvailabilities
            .Where(pa => pa.Product.ExpirationDate < today)
            .Select(pa => new
            {
                pa.Product,
                pa.Store
            })
            .ToList();

        Assert.NotEmpty(expiredProducts);
        Assert.All(expiredProducts, item => Assert.True(item.Product.ExpirationDate < today));
    }

    [Fact]
    public void ReturnStoresWithSalesAboveAmount()
    {
        var threshold = 1000.0;
        var monthStart = new DateTime(2024, 8, 1);
        var monthEnd = new DateTime(2024, 8, 31);

        var storesWithHighSales = _fixture.Sales
            .Where(s => s.SaleDate >= monthStart && s.SaleDate <= monthEnd)
            .GroupBy(s => s.Store.StoreId)
            .Select(group => new
            {
                StoreId = group.Key,
                TotalSales = group.Sum(s => s.Quantity * s.Product.Price)
            })
            .Where(result => result.TotalSales > threshold)
            .ToList();

        Assert.NotEmpty(storesWithHighSales);
        Assert.All(storesWithHighSales, store => Assert.True(store.TotalSales > threshold));
    }
}