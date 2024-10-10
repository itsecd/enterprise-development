using ShopSalesManagement.Domain;
using System;
using System.Collections.Generic;

namespace ShopSalesManagement.Tests;

public class ShopSalesManagementFixture
{
    public List<Product> Products { get; private set; }
    public List<Store> Stores { get; private set; }
    public List<Customer> Customers { get; private set; }
    public List<Sale> Sales { get; private set; }
    public List<Purchase> Purchases { get; private set; }

    public ShopSalesManagementFixture()
    {
        /// <summary>
        // Initialization of test data for goods
        /// </summary>
        Products = new List<Product>
        {
            new Product { Id = 1, Barcode = "123456789012", ProductGroupId = 1, Name = "Bread", Weight = 0.5m, Type = "piece", Price = 2.5m, ExpirationDate = DateTime.Today.AddDays(5) },
            new Product { Id = 2, Barcode = "234567890123", ProductGroupId = 1, Name = "Milk", Weight = 1.0m, Type = "piece", Price = 3.0m, ExpirationDate = DateTime.Today.AddDays(7) },
            new Product { Id = 3, Barcode = "345678901234", ProductGroupId = 2, Name = "Eggs", Weight = 0.2m, Type = "piece", Price = 1.5m, ExpirationDate = DateTime.Today.AddDays(3) }
        };
        /// <summary>
        // Initialization of test data for stores
        /// </summary>
        Stores = new List<Store>
        {
            new Store { Id = 1, Name = "MiniMarket", Address = "123 Main St" },
            new Store { Id = 2, Name = "MegaMarket", Address = "456 High St" }
        };
        /// <summary>
        // Initialization of test data for customers
        /// </summary>
        Customers = new List<Customer>
        {
            new Customer { Id = 1, CardNumber = "987654321", FullName = "Jane Doe" },
            new Customer { Id = 2, CardNumber = "123456789", FullName = "John Smith" }
        };
        /// <summary>
        // Initialization of test data for sales
        /// </summary>
        Sales = new List<Sale>
        {
            new Sale { Id = 1, SaleDate = DateTime.Today.AddDays(-1), CustomerId = 1, StoreId = 1, TotalAmount = 20.0m },
            new Sale { Id = 2, SaleDate = DateTime.Today.AddDays(-2), CustomerId = 2, StoreId = 2, TotalAmount = 15.0m }
        };
        /// <summary>
        // Initialization of test data for purchases
        /// </summary>
        Purchases = new List<Purchase>
        {
            new Purchase { Id = 1, SaleId = 1, ProductId = 1, Quantity = 2, UnitPrice = 2.5m, TotalPrice = 5.0m },
            new Purchase { Id = 2, SaleId = 2, ProductId = 3, Quantity = 10, UnitPrice = 1.5m, TotalPrice = 15.0m }
        };
    }
}
