using EnterpriseDevelopment.Contracts.DTOs.Dishes;
using EnterpriseDevelopment.Application.Services;
using EnterpriseDevelopment.Domain;
using Xunit;

namespace EnterpriseDevelopment.Application.Tests;

/// <summary>
/// Тесты сервиса управления блюдами ресторанов
/// </summary>
public class DishServiceTests
{
    private readonly DishService _service;

    /// <summary>
    /// Создаёт сервис блюд с тестовыми данными
    /// </summary>
    public DishServiceTests()
    {
        var repositories = new TestRepositories();
        _service = new DishService(
            repositories.Dishes, repositories.Categories,
            repositories.Restaurants, repositories.Orders);
    }

    /// <summary>
    /// Проверяет получение списка блюд
    /// </summary>
    [Fact]
    public void GetAll_ReturnsDishes()
    {
        Assert.NotEmpty(_service.GetAll());
    }

    /// <summary>
    /// Проверяет получение существующего блюда по идентификатору
    /// </summary>
    [Fact]
    public void GetById_ExistingDish_ReturnsDish()
    {
        Assert.NotNull(_service.GetById(1));
    }

    /// <summary>
    /// Проверяет получение null для несуществующего блюда
    /// </summary>
    [Fact]
    public void GetById_UnknownDish_ReturnsNull()
    {
        Assert.Null(_service.GetById(999));
    }

    /// <summary>
    /// Проверяет создание блюда и установку связей с рестораном и категорией
    /// </summary>
    [Fact]
    public void Create_ValidDish_AddsDish()
    {
        var created = _service.Create(new CreateDishDto("Тестовая пицца", 400, 500m, 1, 1));

        Assert.True(created.Id > 0);
        Assert.NotNull(_service.GetById(created.Id));
        Assert.Equal(1, created.CategoryId);
        Assert.Equal(1, created.RestaurantId);
    }

    /// <summary>
    /// Проверяет изменение названия существующего блюда
    /// </summary>
    [Fact]
    public void Update_ExistingDish_ChangesName()
    {
        var updated = new UpdateDishDto("Обновлённое блюдо", 350, 450m, 1, 1);

        Assert.True(_service.Update(1, updated));
        Assert.Equal("Обновлённое блюдо", _service.GetById(1)?.Name);
    }

    /// <summary>
    /// Проверяет запрет удаления блюда, входящего в заказ
    /// </summary>
    [Fact]
    public void Delete_DishUsedInOrder_ThrowsInvalidOperationException()
    {
        var repositories = new TestRepositories();
        var service = new DishService(
            repositories.Dishes, repositories.Categories,
            repositories.Restaurants, repositories.Orders);

        var dishId = repositories.Store.Orders
            .SelectMany(o => o.Dishes)
            .First().Id;

        Assert.Throws<InvalidOperationException>(() => service.Delete(dishId));
    }

    /// <summary>
    /// Проверяет удаление блюда, не используемого в заказах
    /// </summary>
    [Fact]
    public void Delete_NewDish_RemovesDish()
    {
        var created = _service.Create(new CreateDishDto("Блюдо для удаления", 200, 250m, 1, 1));

        Assert.True(_service.Delete(created.Id));
        Assert.Null(_service.GetById(created.Id));
    }
}
