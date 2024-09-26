using StoreCashFlow.Domain;

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
        var expectedPrices = new[]
        {
            new { StoreId = 0, ProductGroupCode = "001", AvgPrice = 35.50 },
            new { StoreId = 0, ProductGroupCode = "002", AvgPrice = 70.99 },
            new { StoreId = 1, ProductGroupCode = "003", AvgPrice = 450.99 },
            new { StoreId = 1, ProductGroupCode = "004", AvgPrice = 120.50 },
            new { StoreId = 0, ProductGroupCode = "005", AvgPrice = 40.00 },
            new { StoreId = 0, ProductGroupCode = "006", AvgPrice = 55.99 },
            new { StoreId = 1, ProductGroupCode = "007", AvgPrice = 90.50 },
            new { StoreId = 1, ProductGroupCode = "008", AvgPrice = 320.75 },
            new { StoreId = 0, ProductGroupCode = "009", AvgPrice = 160.20 },
            new { StoreId = 1, ProductGroupCode = "010", AvgPrice = 20.00 },
            new { StoreId = 1, ProductGroupCode = "001", AvgPrice = 35.50 }
        };

        var avgPrices = _fixture.ProductAvailabilities
            .GroupBy(pa => new { pa.Store.StoreId, pa.Product.ProductGroupCode })
            .Select(group => new
            {
                group.Key.StoreId,
                group.Key.ProductGroupCode,
                AvgPrice = group.Average(pa => pa.Product.Price)
            })
            .ToList();

        Assert.Equal(expectedPrices, avgPrices);
    }

    [Fact]
    public void ReturnTop5SalesByTotalAmount()
    {
        var expectedSales = new[]
        {
            new { ProductName = "Масло", TotalAmount = Math.Round(160.20 * 1000, 2) },
            new { ProductName = "Молоко", TotalAmount = Math.Round(70.99 * 110, 2) },
            new { ProductName = "Сахар", TotalAmount = Math.Round(55.99 * 20, 2) },
            new { ProductName = "Яблоки", TotalAmount = Math.Round(120.50 * 5, 2) },   
            new { ProductName = "Хлеб", TotalAmount = Math.Round(35.50 * 2, 2) }       
        };

        var topSales = _fixture.Sales
            .GroupBy(s => s.Product.ProductGroupCode)
            .Select(group => new
            {
                ProductName = group.First().Product.Name,
                TotalAmount = Math.Round(group.Sum(s => s.Product.Price * s.Quantity), 2)
            })
            .OrderByDescending(g => g.TotalAmount)
            .Take(5)
            .ToList();

        Assert.Equal(expectedSales, topSales);
    }

    [Fact]
    public void ReturnExpiredProducts()
    {
        var today = new DateTime(2024, 9, 26);

        var expectedExpiredProducts = new[]
        {
             new { Product = _fixture.Products[0], Store = _fixture.Stores[0] },
             new { Product = _fixture.Products[0], Store = _fixture.Stores[1] }
        };

        var expiredProducts = _fixture.ProductAvailabilities
            .Where(pa => pa.Product.ExpirationDate < today)
            .Select(pa => new
            {
                pa.Product,
                pa.Store
            })
            .ToList();

        Assert.Equal(expectedExpiredProducts, expiredProducts);
    }

    [Fact]
    public void ReturnStoresWithSalesAboveAmount()
    {
        var threshold = 1000.0;
        var monthStart = new DateTime(2024, 8, 1);
        var monthEnd = new DateTime(2024, 8, 31);

        var expectedHighSales = new[]
        {
            new { StoreId = 1, TotalSales = 169128.70 }
        };

        var storesWithHighSales = _fixture.Sales
            .Where(s => s.SaleDate >= monthStart && s.SaleDate <= monthEnd)
            .GroupBy(s => s.Store.StoreId)
            .Select(group => new
            {
                StoreId = group.Key,
                TotalSales = Math.Round(group.Sum(s => s.Quantity * s.Product.Price), 2)
            })
            .Where(result => result.TotalSales > threshold)
            .ToList();

        Assert.Equal(expectedHighSales, storesWithHighSales);
    }
}