using AutoService.Contracts.Dtos;
using AutoService.Contracts.Interfaces;
using AutoService.Domain.Interfaces;
using AutoService.Domain.Models;

namespace AutoService.Application.Services;

/// <summary>
/// Сервис для управления автомобилями.
/// </summary>
public class CarService(ICarRepository repository, IClientRepository clientRepository) : ICarService
{
    /// <summary>
    /// Получить список всех автомобилей.
    /// </summary>
    public async Task<IEnumerable<CarDto>> GetAllAsync()
    {
        var cars = await repository.GetAllAsync();

        return cars.Select(c => new CarDto
        {
            Id = c.Id,
            LicensePlate = c.LicensePlate,
            Brand = c.Brand,
            Model = c.Model,
            Year = c.Year,
            ClientId = c.ClientId
        });
    }

    /// <summary>
    /// Получить автомобиль по идентификатору.
    /// </summary>
    public async Task<CarDto?> GetByIdAsync(Guid id)
    {
        var car = await repository.GetByIdAsync(id);
        if (car == null) return null;

        return new CarDto
        {
            Id = car.Id,
            LicensePlate = car.LicensePlate,
            Brand = car.Brand,
            Model = car.Model,
            Year = car.Year,
            ClientId = car.ClientId
        };
    }

    /// <summary>
    /// Добавить новый автомобиль.
    /// </summary>
    public async Task<CarDto> CreateAsync(CarDto carDto)
    {
        var client = await clientRepository.GetByIdAsync(carDto.ClientId)
            ?? throw new Exception($"Клиент с идентификатором {carDto.ClientId} не найден");

        var car = new Car
        {
            Id = Guid.NewGuid(),
            LicensePlate = carDto.LicensePlate,
            Brand = carDto.Brand,
            Model = carDto.Model,
            Year = carDto.Year,
            ClientId = client.Id,
            Client = client
        };

        await repository.AddAsync(car);

        carDto.Id = car.Id;
        return carDto;
    }

    /// <summary>
    /// Обновить существующий автомобиль.
    /// </summary>
    public async Task UpdateAsync(Guid id, CarUpdateDto carDto)
    {
        var car = await repository.GetByIdAsync(id)
            ?? throw new KeyNotFoundException($"Автомобиль с id {id} не найден");

        var client = await clientRepository.GetByIdAsync(carDto.ClientId)
            ?? throw new Exception($"Клиент с идентификатором {carDto.ClientId} не найден");

        car.LicensePlate = carDto.LicensePlate;
        car.Brand = carDto.Brand;
        car.Model = carDto.Model;
        car.Year = carDto.Year;
        car.ClientId = client.Id;
        car.Client = client;

        await repository.UpdateAsync(car);
    }

    /// <summary>
    /// Удалить автомобиль.
    /// </summary>
    public async Task DeleteAsync(Guid id)
    {
        var car = await repository.GetByIdAsync(id);
        if (car == null) return;
        await repository.DeleteAsync(car);
    }
}