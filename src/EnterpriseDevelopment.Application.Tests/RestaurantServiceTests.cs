using EnterpriseDevelopment.Contracts.DTOs.Restaurants;
using EnterpriseDevelopment.Application.Services;
using EnterpriseDevelopment.Domain;
using Xunit;

namespace EnterpriseDevelopment.Application.Tests;

/// <summary>
/// Тесты сервиса управления ресторанами
/// </summary>
public class RestaurantServiceTests
{
    private readonly DataSeeder _data;
    private readonly RestaurantService _service;

    /// <summary>
    /// Создаёт тестовое хранилище данных и сервис ресторанов
    /// </summary>
    public RestaurantServiceTests()
    {
        _data = new DataSeeder();
        var repositories = new TestRepositories(_data);
        _service = new RestaurantService(
            repositories.Restaurants, repositories.Dishes, repositories.Orders);
    }

    /// <summary>
    /// Проверяет получение списка ресторанов
    /// </summary>
    [Fact]
    public void GetAll_ReturnsRestaurants()
    {
        var result = _service.GetAll();

        Assert.NotEmpty(result);
    }

    /// <summary>
    /// Проверяет получение существующего ресторана
    /// </summary>
    [Fact]
    public void GetById_ExistingRestaurant_ReturnsRestaurant()
    {
        var restaurant = _service.GetById(1);

        Assert.NotNull(restaurant);
        Assert.Equal(1, restaurant.Id);
    }

    /// <summary>
    /// Проверяет получение null для несуществующего ресторана
    /// </summary>
    [Fact]
    public void GetById_UnknownRestaurant_ReturnsNull()
    {
        var restaurant = _service.GetById(999);

        Assert.Null(restaurant);
    }

    /// <summary>
    /// Проверяет результат удаления несуществующего ресторана
    /// </summary>
    [Fact]
    public void Delete_UnknownRestaurant_ReturnsFalse()
    {
        var result = _service.Delete(999);

        Assert.False(result);
    }

    /// <summary>
    /// Проверяет запрет удаления ресторана с существующими заказами
    /// </summary>
    [Fact]
    public void Delete_RestaurantWithOrders_ReturnsFalse()
    {
        var result = _service.Delete(1);

        Assert.False(result);
        Assert.NotNull(_service.GetById(1));
    }

    /// <summary>
    /// Проверяет создание нового ресторана
    /// </summary>
    [Fact]
    public void Create_ValidRestaurant_AddsRestaurant()
    {
        var restaurant = new CreateRestaurantDto(
            "Новый ресторан", "Самара, ул. Тестовая, 1", 4.5,
            new TimeOnly(9, 0), new TimeOnly(22, 0));

        var created = _service.Create(restaurant);

        Assert.True(created.Id > 0);
        Assert.NotNull(_service.GetById(created.Id));
        Assert.Equal("Новый ресторан", created.Name);
    }

    /// <summary>
    /// Проверяет изменение названия существующего ресторана
    /// </summary>
    [Fact]
    public void Update_ExistingRestaurant_ChangesName()
    {
        var restaurant = _service.GetById(1);

        Assert.NotNull(restaurant);

        var updated = new UpdateRestaurantDto(
            "Обновлённый ресторан", restaurant.Address, restaurant.Rating,
            restaurant.OpeningTime, restaurant.ClosingTime);

        var result = _service.Update(1, updated);

        Assert.True(result);
        Assert.Equal(
            "Обновлённый ресторан",
            _service.GetById(1)?.Name);
    }

    /// <summary>
    /// Проверяет удаление ресторана без связанных заказов и блюд
    /// </summary>
    [Fact]
    public void Delete_NewRestaurant_RemovesRestaurant()
    {
        var restaurant = new CreateRestaurantDto(
            "Ресторан для удаления", "Самара, ул. Тестовая, 2", 4.0,
            new TimeOnly(9, 0), new TimeOnly(22, 0));

        var created = _service.Create(restaurant);

        var result = _service.Delete(created.Id);

        Assert.True(result);
        Assert.Null(_service.GetById(created.Id));
    }
}
