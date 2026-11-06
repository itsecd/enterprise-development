using System.ComponentModel.DataAnnotations;
using FoodDelivery.Application.Contracts.Dishes;

namespace FoodDelivery.Tests.Contracts;

/// <summary>
/// Проверяет ограничения входных данных блюда
/// </summary>
public sealed class DishRequestTests
{
    /// <summary>
    /// Проверяет запрет нулевой и отрицательной цены
    /// </summary>
    /// <param name="price">Проверяемая цена</param>
    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Validate_RejectsNonPositivePrice(int price)
    {
        var request = new DishRequest
        {
            Name = "Тестовое блюдо",
            Weight = 250,
            Price = price,
            CategoryId = 1,
            RestaurantId = 1
        };

        var results = new List<ValidationResult>();

        var isValid = Validator.TryValidateObject(
            request,
            new ValidationContext(request),
            results,
            validateAllProperties: true);

        Assert.False(isValid);
        Assert.Contains(
            results,
            result => result.MemberNames.Contains(nameof(DishRequest.Price)));
    }
}
