using StoreCashFlow.Domain;

namespace StoreCashFlow.Tests;

public class StoreCashFixture
{
    public List<Product> Products;
    public List<Store> Stores =
    [
        new() { StoreId = 0, Location = "Москва, ул. Тверская, д. 10" },
        new() { StoreId = 1, Location = "Санкт-Петербург, Невский проспект, д. 45" }
    ];
    public List<ProductAvailability> ProductAvailabilities;
    public List<Sale> Sales;
    public List<Customer> Customers =
    [
        new() { CustomerId = 0, CardNumber = "12345", FirstName = "Иван", LastName = "Иванов", Potronimic = "Иванович" },
        new() { CustomerId = 1, CardNumber = "54321",  FirstName = "Владимир", LastName = "Иванов", Potronimic = "Иванович" },
        new() { CustomerId = 2, CardNumber = "54431",  FirstName = "Сергей", LastName = "Иванов", Potronimic = "Владимирович" }
    ];
    public List<ProductType> ProductTypes =
    [
        new() { Id = 0, Name = "штучный"},
        new() { Id = 1, Name = "развесной"}
    ];

    public StoreCashFixture()
    {
        Products =
        [
            new() { Barcode = "1111111111", ProductGroupCode = "001", Name = "Хлеб", Weight = 0.5, ProductType = ProductTypes[0], Price = 35.50, ExpirationDate = new DateTime(2024, 9, 1) },
            new() { Barcode = "2222222222", ProductGroupCode = "002", Name = "Молоко", Weight = 1.0, ProductType = ProductTypes[0], Price = 70.99, ExpirationDate = new DateTime(2024, 10, 5) },
            new() { Barcode = "3333333333", ProductGroupCode = "003", Name = "Сыр", Weight = 0.2, ProductType = ProductTypes[1], Price = 450.99, ExpirationDate = new DateTime(2024, 11, 15) },
            new() { Barcode = "4444444444", ProductGroupCode = "004", Name = "Яблоки", Weight = 1.0, ProductType = ProductTypes[1], Price = 120.50, ExpirationDate = new DateTime(2024, 9, 30) },
            new() { Barcode = "5555555555", ProductGroupCode = "005", Name = "Картофель", Weight = 2.0, ProductType = ProductTypes[1], Price = 40.00, ExpirationDate = new DateTime(2024, 11, 20) },
            new() { Barcode = "6666666666", ProductGroupCode = "006", Name = "Сахар", Weight = 1.0, ProductType = ProductTypes[0], Price = 55.99, ExpirationDate = new DateTime(2025, 3, 1) },
            new() { Barcode = "7777777777", ProductGroupCode = "007", Name = "Мука", Weight = 2.0, ProductType = ProductTypes[0], Price = 90.50, ExpirationDate = new DateTime(2025, 1, 15) },
            new() { Barcode = "8888888888", ProductGroupCode = "008", Name = "Колбаса", Weight = 0.3, ProductType = ProductTypes[1], Price = 320.75, ExpirationDate = new DateTime(2024, 12, 5) },
            new() { Barcode = "9999999999", ProductGroupCode = "009", Name = "Масло", Weight = 0.2, ProductType = ProductTypes[0], Price = 160.20, ExpirationDate = new DateTime(2024, 10, 10) },
            new() { Barcode = "1010101010", ProductGroupCode = "010", Name = "Яйца", Weight = 0.5, ProductType = ProductTypes[0], Price = 20.00, ExpirationDate = new DateTime(2024, 10, 3) }
        ];

        ProductAvailabilities =
        [
            new() { Id = 0, Product = Products[0], Quantity = 100, Store = Stores[0] },
            new() { Id = 1, Product = Products[1], Quantity = 50, Store = Stores[0] },
            new() { Id = 2, Product = Products[2], Quantity = 30, Store = Stores[1] },
            new() { Id = 3, Product = Products[3], Quantity = 200, Store = Stores[1] },
            new() { Id = 4, Product = Products[4], Quantity = 150, Store = Stores[0] },
            new() { Id = 5, Product = Products[5], Quantity = 100, Store = Stores[0] },
            new() { Id = 6, Product = Products[6], Quantity = 120, Store = Stores[1] },
            new() { Id = 7, Product = Products[7], Quantity = 80, Store = Stores[1] },
            new() { Id = 8, Product = Products[8], Quantity = 60, Store = Stores[0] },
            new() { Id = 9, Product = Products[9], Quantity = 90, Store = Stores[1] },
            new() { Id = 10, Product = Products[0], Quantity = 120, Store = Stores[1] },
        ];

        Sales =
        [
            new() { SaleId = 0, Customer = Customers[0], Product = Products[0], Quantity = 2, SaleDate = new DateTime(2024, 9, 25), Store = Stores[0] },
            new() { SaleId = 1, Customer = Customers[1], Product = Products[3], Quantity = 5, SaleDate = new DateTime(2024, 9, 24), Store = Stores[1] },
            new() { SaleId = 2, Customer = Customers[2], Product = Products[8], Quantity = 1000, SaleDate = new DateTime(2024, 8, 24), Store = Stores[1] },
            new() { SaleId = 3, Customer = Customers[0], Product = Products[1], Quantity = 100, SaleDate = new DateTime(2024, 8, 24), Store = Stores[1] },
            new() { SaleId = 4, Customer = Customers[0], Product = Products[1], Quantity = 10, SaleDate = new DateTime(2024, 8, 24), Store = Stores[1] },
            new() { SaleId = 5, Customer = Customers[0], Product = Products[5], Quantity = 20, SaleDate = new DateTime(2024, 8, 24), Store = Stores[1] }
        ];
    }
}
