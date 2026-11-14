using FoodDelivery.Contracts.Categories;

namespace FoodDelivery.Application.Services;

public interface ICategoryService
{
    public IReadOnlyList<CategoryDto> GetAll();
    public CategoryDto? GetById(int id);
    public CategoryDto Create(CategoryCreateUpdateDto dto);
    public bool Update(int id, CategoryCreateUpdateDto dto);
    public void Delete(int id);
}