using CarWash.Application.Mapping;
using CarWash.Contracts.Dto;
using CarWash.Contracts.Interfaces;
using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;

namespace CarWash.Application.Services;

/// <summary>
/// Сервис для работы с автомобилями
/// </summary>
/// <param name="repository">Репозиторий автомобилей</param>
/// <param name="clientRepository">Репозиторий клиентов</param>
/// <param name="orderRepository">Репозиторий заказов</param>
public class CarService(
    ICarRepository repository,
    IClientRepository clientRepository,
    IOrderRepository orderRepository) : ICarService
{
    /// <inheritdoc />
    public async Task<IEnumerable<CarDto>> GetAllAsync()
    {
        var cars = await repository.GetAllAsync();
        return cars.Select(c => c.ToDto());
    }

    /// <inheritdoc />
    public async Task<CarDto?> GetByIdAsync(int id)
    {
        var car = await repository.GetByIdAsync(id);
        return car?.ToDto();
    }

    /// <inheritdoc />
    public async Task<CarDto> CreateAsync(CarEditDto editDto)
    {
        var car = new Car
        {
            LicensePlate = editDto.LicensePlate,
            Brand = editDto.Brand
        };

        var created = await repository.AddAsync(car);
        return created.ToDto();
    }

    /// <inheritdoc />
    public async Task UpdateAsync(int id, CarEditDto editDto)
    {
        var car = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Автомобиль не найден");

        car.LicensePlate = editDto.LicensePlate;
        car.Brand = editDto.Brand;
        await repository.UpdateAsync(car);
    }

    /// <inheritdoc />
    public async Task DeleteAsync(int id)
    {
        _ = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException("Автомобиль не найден");

        var orders = await orderRepository.GetAllAsync();
        if (orders.Any(o => o.Car.Id == id))
        {
            throw new InvalidOperationException("Нельзя удалить автомобиль: по нему есть заказы");
        }

        // Убираем автомобиль из списка автомобилей его владельца
        var clients = await clientRepository.GetAllAsync();
        foreach (var client in clients)
        {
            client.Cars.RemoveAll(c => c.Id == id);
        }

        await repository.DeleteAsync(id);
    }
}
