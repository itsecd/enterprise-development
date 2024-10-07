using System;
using ShopSalesManagement.Domain;
using Xunit;

namespace ShopSalesManagement.Tests
{
    public class ShopSalesManagementTests
    {
        [Fact]
        public void CanCreateCustomer()
        {
            // Arrange
            var customer = new Customer
            {
                Id = 1,
                CardNumber = "123456789",
                FullName = "John Doe"
            };

            // Assert
            Assert.Equal(1, customer.Id);
            Assert.Equal("123456789", customer.CardNumber);
            Assert.Equal("John Doe", customer.FullName);
        }

        [Fact]
        public void CanCreateProduct()
        {
            // Arrange
            var product = new Product
            {
                Id = 1,
                Barcode = "0012345678905",
                ProductGroupId = 1,
                Name = "Milk",
                Weight = 1.0m,
                Type = "piece",
                Price = 2.99m,
                ExpirationDate = DateTime.Today.AddDays(10)
            };

            // Assert
            Assert.Equal(1, product.Id);
            Assert.Equal("0012345678905", product.Barcode);
            Assert.Equal(1, product.ProductGroupId);
            Assert.Equal("Milk", product.Name);
            Assert.Equal(1.0m, product.Weight);
            Assert.Equal("piece", product.Type);
            Assert.Equal(2.99m, product.Price);
            Assert.Equal(DateTime.Today.AddDays(10), product.ExpirationDate);
        }

        [Fact]
        public void CanCreateSale()
        {
            // Arrange
            var sale = new Sale
            {
                Id = 1,
                SaleDate = DateTime.Today,
                CustomerId = 1,
                StoreId = 1,
                TotalAmount = 59.99m
            };

            // Assert
            Assert.Equal(1, sale.Id);
            Assert.Equal(DateTime.Today, sale.SaleDate);
            Assert.Equal(1, sale.CustomerId);
            Assert.Equal(1, sale.StoreId);
            Assert.Equal(59.99m, sale.TotalAmount);
        }

        [Fact]
        public void CanCreateStore()
        {
            // Arrange
            var store = new Store
            {
                Id = 1,
                Name = "MegaMart",
                Address = "123 Main Street"
            };

            // Assert
            Assert.Equal(1, store.Id);
            Assert.Equal("MegaMart", store.Name);
            Assert.Equal("123 Main Street", store.Address);
        }
    }
}
