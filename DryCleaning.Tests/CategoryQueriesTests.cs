using DryCleaning.Domain.Seeds;
using DryCleaning.Tests.Services;

namespace DryCleaning.Tests;

public class CategoryQueriesTests(TestData data) : IClassFixture<TestData>
{
    [Fact]
    public void MostPopularCategoriesAreCalculated()
    {
        var from = new DateTime(2026, 1, 1);
        var to = new DateTime(2027, 1, 1);

        int[] expectedIds = [1, 6, 7, 4, 8];

        var categories = OrderQueries.GetCategoryPopularity(
            data.Orders, data.Categories, from, to, descending: true);

        Assert.Equal(expectedIds, categories.Select(c => c.CategoryId));
    }

    [Fact]
    public void LeastPopularCategoriesAreCalculated()
    {
        var from = new DateTime(2026, 1, 1);
        var to = new DateTime(2027, 1, 1);

        int[] expectedIds = [3, 2, 9, 10, 5];

        var categories = OrderQueries.GetCategoryPopularity(
            data.Orders, data.Categories, from, to, descending: false);

        Assert.Equal(expectedIds, categories.Select(c => c.CategoryId));
    }
}
