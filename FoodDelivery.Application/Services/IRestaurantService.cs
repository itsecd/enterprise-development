using FoodDelivery.Contracts.Restaurants;

namespace FoodDelivery.Application.Services;

public interface IRestaurantService
{
    public IReadOnlyList<RestaurantDto> GetAll();
    public RestaurantDto? GetById(int id);
    public RestaurantDto Create(RestaurantCreateUpdateDto dto);
    public bool Update(int id, RestaurantCreateUpdateDto dto);
    public void Delete(int id);
}