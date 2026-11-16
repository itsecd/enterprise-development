using CarWash.Application.Contracts;
using CarWash.Application.Contracts.Cars;
using CarWash.Domain;
using CarWash.Domain.Entities;

namespace CarWash.Application.Services;

/// <summary>
/// Служба для CRUD-операций над авто
/// </summary>
public class CarService(
    IRepository<Car, int> carRepository,
    IRepository<CarWashCustomer, int> customerRepository)
    : IApplicationService<CarDto, CarCreateUpdateDto, int>
{
    public async Task<CarDto> Create(CarCreateUpdateDto dto)
    {
        var customer = await customerRepository.Read(dto.CustomerId)
            ?? throw new KeyNotFoundException(
                $"Клиент с Id {dto.CustomerId} не найден");

        var newId = (await carRepository.ReadAll())
            .Select(car => car.Id)
            .DefaultIfEmpty(0)
            .Max() + 1;

        var car = new Car
        {
            Id = newId,
            CarCategory = dto.CarCategory,
            LicensePlate = dto.LicensePlate,
            Brand = dto.Brand,
            CustomerId = customer.Id,
            Customer = customer
        };

        await carRepository.Create(car);

        customer.Cars.Add(car);

        return MapToDto(car);
    }

    public async Task<CarDto?> Get(int dtoId)
    {
        var car = await carRepository.Read(dtoId);

        return car is null ? null : MapToDto(car);
    }

    public async Task<IList<CarDto>> GetAll()
    {
        var cars = await carRepository.ReadAll();

        return cars
            .Select(MapToDto)
            .ToList();
    }

    public async Task<CarDto> Update(
        CarCreateUpdateDto dto,
        int dtoId)
    {
        var car = await carRepository.Read(dtoId)
            ?? throw new KeyNotFoundException(
                $"Автомобиль с Id {dtoId} не найден");

        var customer = await customerRepository.Read(dto.CustomerId)
            ?? throw new KeyNotFoundException(
                $"Клиент с Id {dto.CustomerId} не найден");

        if (car.CustomerId != customer.Id)
        {
            car.Customer?.Cars.Remove(car);
            customer.Cars.Add(car);
        }

        car.CarCategory = dto.CarCategory;
        car.LicensePlate = dto.LicensePlate;
        car.Brand = dto.Brand;
        car.CustomerId = customer.Id;
        car.Customer = customer;

        var updatedCar = await carRepository.Update(car);

        return MapToDto(updatedCar);
    }

    public async Task<bool> Delete(int dtoId)
    {
        var car = await carRepository.Read(dtoId);

        if (car is null)
            return false;

        car.Customer?.Cars.Remove(car);

        return await carRepository.Delete(dtoId);
    }

    private static CarDto MapToDto(Car car)
    {
        return new CarDto(
            car.Id,
            car.CarCategory,
            car.LicensePlate,
            car.Brand,
            car.CustomerId);
    }
}