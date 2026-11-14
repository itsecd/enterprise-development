using FoodDelivery.Contracts.Analytics;

namespace FoodDelivery.Application.Services;

public interface IAnalyticsService
{
    public IReadOnlyList<TopRestaurantDto> GetTop5Restaurants();
    public IReadOnlyList<MinDeliveryOrderDto> GetOrdersWithMinDeliveryTime();
    public IReadOnlyList<ClientSummaryDto> GetClientsByRestaurant(int restaurantId);
    public IReadOnlyList<CategorySummaryDto> GetCategorySummary(DateTime start, DateTime end);
    public IReadOnlyList<TopSpenderDto> GetTopSpenders();
}