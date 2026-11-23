using AutoServiceLab.Application.Contracts.Dtos.Cars;
using AutoServiceLab.Application.Contracts.Interfaces;
using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Repositories;
using Microsoft.Extensions.Logging;

namespace AutoServiceLab.Application.Services;

/// <summary>
/// Сервис для работы с автомобилями
/// </summary>
public class CarService(
    ICarRepository repository,
    IClientRepository clientRepo,
    ILogger<CarService> logger) : ICarService
{
    /// <inheritdoc />
    public async Task<IList<CarDto>> GetAllAsync()
    {
        logger.LogInformation("Получение списка всех автомобилей");
        return (await repository.GetAllAsync())
            .Select(c => new CarDto(c.Id, c.LicensePlate, c.Brand, c.Model, c.Year, c.ClientId))
            .ToList();
    }

    /// <inheritdoc />
    public async Task<CarDto?> GetAsync(int id)
    {
        logger.LogInformation("Получение автомобиля с ID: {Id}", id);
        var car = await repository.GetAsync(id);
        return car == null ? null : new CarDto(car.Id, car.LicensePlate, car.Brand, car.Model, car.Year, car.ClientId);
    }

    /// <inheritdoc />
    public async Task<CarDto> CreateAsync(CarCreateUpdateDto dto)
    {
        logger.LogInformation("Создание автомобиля {Brand} {Model}", dto.Brand, dto.Model);
        var car = new Car
        {
            LicensePlate = dto.LicensePlate,
            Brand = dto.Brand,
            Model = dto.Model,
            Year = dto.Year,
            ClientId = dto.ClientId,
            Client = await clientRepo.GetAsync(dto.ClientId) ?? throw new KeyNotFoundException("Клиент не найден")
        };
        await repository.AddAsync(car);
        return new CarDto(car.Id, car.LicensePlate, car.Brand, car.Model, car.Year, car.ClientId);
    }

    /// <inheritdoc />
    public async Task<CarDto> UpdateAsync(CarCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление автомобиля с ID: {Id}", id);
        var car = await repository.GetAsync(id) ?? throw new KeyNotFoundException();
        car.LicensePlate = dto.LicensePlate;
        car.Brand = dto.Brand;
        car.Model = dto.Model;
        car.Year = dto.Year;
        car.ClientId = dto.ClientId;
        car.Client = await clientRepo.GetAsync(dto.ClientId) ?? throw new KeyNotFoundException("Клиент не найден");
        await repository.UpdateAsync(car);
        return new CarDto(car.Id, car.LicensePlate, car.Brand, car.Model, car.Year, car.ClientId);
    }

    /// <inheritdoc />
    public async Task<bool> DeleteAsync(int id)
    {
        logger.LogInformation("Удаление автомобиля с ID: {Id}", id);
        return await repository.DeleteAsync(id);
    }

    /// <inheritdoc />
    public async Task<IList<CarDto>> GetCarsByClientId(int clientId)
    {
        logger.LogInformation("Получение автомобилей клиента с ID: {ClientId}", clientId);
        return (await repository.GetAllAsync())
            .Where(c => c.ClientId == clientId)
            .Select(c => new CarDto(c.Id, c.LicensePlate, c.Brand, c.Model, c.Year, c.ClientId))
            .ToList();
    }
}
