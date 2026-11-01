using AutoServiceLab.Application.Contracts.DTOs.Cars;
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
    public IList<CarDto> GetAll()
    {
        logger.LogInformation("Получение списка всех автомобилей");
        return repository.GetAll()
            .Select(c => new CarDto(c.Id, c.LicensePlate, c.Brand, c.Model, c.Year, c.ClientId))
            .ToList();
    }

    /// <inheritdoc />
    public CarDto? Get(int id)
    {
        logger.LogInformation("Получение автомобиля с ID: {Id}", id);
        var car = repository.Get(id);
        return car == null ? null : new CarDto(car.Id, car.LicensePlate, car.Brand, car.Model, car.Year, car.ClientId);
    }

    /// <inheritdoc />
    public CarDto Create(CarCreateUpdateDto dto)
    {
        logger.LogInformation("Создание автомобиля {Brand} {Model}", dto.Brand, dto.Model);
        var car = new Car
        {
            LicensePlate = dto.LicensePlate,
            Brand = dto.Brand,
            Model = dto.Model,
            Year = dto.Year,
            ClientId = dto.ClientId,
            Client = clientRepo.Get(dto.ClientId) ?? throw new Exception("Клиент не найден")
        };
        repository.Add(car);
        return new CarDto(car.Id, car.LicensePlate, car.Brand, car.Model, car.Year, car.ClientId);
    }

    /// <inheritdoc />
    public CarDto Update(CarCreateUpdateDto dto, int id)
    {
        logger.LogInformation("Обновление автомобиля с ID: {Id}", id);
        var car = repository.Get(id) ?? throw new KeyNotFoundException();
        car.LicensePlate = dto.LicensePlate;
        car.Brand = dto.Brand;
        car.Model = dto.Model;
        car.Year = dto.Year;
        car.ClientId = dto.ClientId;
        car.Client = clientRepo.Get(dto.ClientId) ?? throw new Exception("Клиент не найден");
        repository.Update(car);
        return new CarDto(car.Id, car.LicensePlate, car.Brand, car.Model, car.Year, car.ClientId);
    }

    /// <inheritdoc />
    public bool Delete(int id)
    {
        logger.LogInformation("Удаление автомобиля с ID: {Id}", id);
        return repository.Delete(id);
    }

    /// <inheritdoc />
    public IList<CarDto> GetCarsByClientId(int clientId)
    {
        logger.LogInformation("Получение автомобилей клиента с ID: {ClientId}", clientId);
        return repository.GetAll()
            .Where(c => c.ClientId == clientId)
            .Select(c => new CarDto(c.Id, c.LicensePlate, c.Brand, c.Model, c.Year, c.ClientId))
            .ToList();
    }
}
