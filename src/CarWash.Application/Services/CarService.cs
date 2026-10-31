using CarWash.Contracts.Dto;
using CarWash.Contracts.Interfaces;
using CarWash.Domain.Entities;
using CarWash.Domain.Interfaces;

namespace CarWash.Application.Services;

public class CarService(ICarRepository repository) : ICarService
{
    public async Task<IEnumerable<CarDto>> GetAllAsync()
    {
        var cars = await repository.GetAllAsync();
        return cars.Select(c => new CarDto { Id = c.Id, LicensePlate = c.LicensePlate, Brand = c.Brand });
    }

    public async Task<CarDto?> GetByIdAsync(int id)
    {
        var car = await repository.GetByIdAsync(id);
        if (car == null) return null;
        return new CarDto { Id = car.Id, LicensePlate = car.LicensePlate, Brand = car.Brand };
    }

    public async Task<CarDto> CreateAsync(CarEditDto editDto)
    {
        var car = new Car { LicensePlate = editDto.LicensePlate, Brand = editDto.Brand };
        var created = await repository.AddAsync(car);
        return new CarDto { Id = created.Id, LicensePlate = created.LicensePlate, Brand = created.Brand };
    }

    public async Task UpdateAsync(int id, CarEditDto editDto)
    {
        var car = await repository.GetByIdAsync(id);
        if (car == null) throw new KeyNotFoundException("Автомобиль не найден");

        car.LicensePlate = editDto.LicensePlate;
        car.Brand = editDto.Brand;

        await repository.UpdateAsync(car);
    }

    public async Task DeleteAsync(int id)
    {
        await repository.DeleteAsync(id);
    }
}