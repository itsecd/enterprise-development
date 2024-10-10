using ShopSalesManagement.Domain;

namespace ShopSalesManagement.Tests;

public class ShopSalesManagementFixture
{
    public List<Product> Products { get; private set; }
    public List<Store> Stores { get; private set; }
    public List<Customer> Customers { get; private set; }
    public List<Sale> Sales { get; private set; }
    public List<Purchase> Purchases { get; private set; }
    public List<Stock> Stocks { get; private set; }

    public ShopSalesManagementFixture()
    {
        Products = new List<Product>
        {
            new Product("123456789012", "Bread", "piece", 2.5m, DateTime.Today.AddDays(5)) { Id = 1, ProductGroupId = 1, Weight = 0.5m },
            new Product("234567890123", "Milk", "piece", 3.0m, DateTime.Today.AddDays(7)) { Id = 2, ProductGroupId = 1, Weight = 1.0m },
            new Product("345678901234", "Eggs", "piece", 1.5m, DateTime.Today.AddDays(3)) { Id = 3, ProductGroupId = 2, Weight = 0.2m },
            new Product("456789012345", "Cheese", "piece", 5.0m, DateTime.Today.AddDays(-1)) { Id = 4, ProductGroupId = 2, Weight = 0.5m }
        };
        Stores = new List<Store>
        {
            new Store("MiniMarket", "123 Main St") { Id = 1 },
            new Store("MegaMarket", "456 High St") { Id = 2 }
        };
        Customers = new List<Customer>
        {
            new Customer("987654321", "Jane Doe") { Id = 1 },
            new Customer("123456789", "John Smith") { Id = 2 }
        };
        Sales = new List<Sale>
        {
            new Sale(DateTime.Today.AddDays(-1), 1, 1, 20.0m) { Id = 1 },
            new Sale(DateTime.Today.AddDays(-2), 2, 2, 15.0m) { Id = 2 }
        };
        Purchases = new List<Purchase>
        {
            new Purchase { Id = 1, SaleId = 1, ProductId = 1, Quantity = 2, UnitPrice = 2.5m, TotalPrice = 5.0m },
            new Purchase { Id = 2, SaleId = 2, ProductId = 3, Quantity = 10, UnitPrice = 1.5m, TotalPrice = 15.0m }
        };
        Stocks = new List<Stock>
        {
            new Stock { Id = 1, ProductId = 1, StoreId = 1, Quantity = 10 },
            new Stock { Id = 2, ProductId = 2, StoreId = 1, Quantity = 0 },
            new Stock { Id = 3, ProductId = 3, StoreId = 2, Quantity = 5 },
            new Stock { Id = 4, ProductId = 4, StoreId = 1, Quantity = 3 }
        };
    }
}