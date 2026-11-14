using FoodDelivery.Contracts.Dishes;

namespace FoodDelivery.Application.Services;

public interface IDishService
{
    public IReadOnlyList<DishDto> GetAll();
    public DishDto? GetById(int id);
    public DishDto Create(DishCreateUpdateDto dto);
    public bool Update(int id, DishCreateUpdateDto dto);
    public void Delete(int id);
}