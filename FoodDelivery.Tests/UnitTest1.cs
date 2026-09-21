using FoodDelivery.Tests.TestData;

namespace FoodDelivery.Tests;

public class TestDataTests
{
    [Fact]
    public void TestData_ShouldContainRequiredNumberOfEntities()
    {
        var data = new FoodDeliveryTestData();

        Assert.True(data.Categories.Count >= 10);
        Assert.True(data.Dishes.Count >= 10);
        Assert.True(data.Restaurants.Count >= 10);
        Assert.True(data.Customers.Count >= 10);
        Assert.True(data.Orders.Count >= 10);
        Assert.True(data.OrderItems.Count >= 10);
    }
}