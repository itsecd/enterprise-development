using ShopSalesManagement.Domain;
using System;

namespace ShopSalesManagement.Tests
{
    public class ShopSalesManagementFixture : IDisposable
    {
        public Customer TestCustomer { get; private set; }
        public Product TestProduct { get; private set; }
        public Store TestStore { get; private set; }

        public ShopSalesManagementFixture()
        {
            // Инициализация тестовых данных
            TestCustomer = new Customer
            {
                Id = 1,
                CardNumber = "987654321",
                FullName = "Jane Doe"
            };

            TestProduct = new Product
            {
                Id = 1,
                Barcode = "123456789012",
                ProductGroupId = 1,
                Name = "Bread",
                Weight = 0.5m,
                Type = "piece",
                Price = 1.5m,
                ExpirationDate = DateTime.Today.AddDays(5)
            };


            TestStore = new Store
            {
                Id = 1,
                Name = "MiniMarket",
                Address = "456 High Street"
            };
        }

        public void Dispose()
        {
            // Очистка ресурсов, если необходимо
        }
    }

    public class ShopSalesManagementTestsWithFixture : IClassFixture<ShopSalesManagementFixture>
    {
        private readonly ShopSalesManagementFixture _fixture;

        public ShopSalesManagementTestsWithFixture(ShopSalesManagementFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public void CustomerFixtureDataIsCorrect()
        {
            // Используем данные фикстуры
            Assert.Equal("987654321", _fixture.TestCustomer.CardNumber);
            Assert.Equal("Jane Doe", _fixture.TestCustomer.FullName);
        }

        [Fact]
        public void ProductFixtureDataIsCorrect()
        {
            Assert.Equal("Bread", _fixture.TestProduct.Name);
            Assert.Equal(1.5m, _fixture.TestProduct.Price);
            Assert.Equal("piece", _fixture.TestProduct.Type);
        }

        [Fact]
        public void StoreFixtureDataIsCorrect()
        {
            Assert.Equal("MiniMarket", _fixture.TestStore.Name);
            Assert.Equal("456 High Street", _fixture.TestStore.Address);
        }
    }
}
