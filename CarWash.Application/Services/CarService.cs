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
    public CarDto Create(CarCreateUpdateDto dto)
    {
        var customer = customerRepository.Read(dto.CustomerId);

        var newId = carRepository.ReadAll()
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

        carRepository.Create(car);

        customer.Cars.Add(car);

        return MapToDto(car);
    }

    public CarDto Get(int dtoId)
    {
        return MapToDto(carRepository.Read(dtoId));
    }

    public List<CarDto> GetAll()
    {
        return carRepository.ReadAll()
            .Select(MapToDto)
            .ToList();
    }

    public CarDto Update(CarCreateUpdateDto dto, int dtoId)
    {
        var car = carRepository.Read(dtoId);
        var customer = customerRepository.Read(dto.CustomerId);

        if (car.CustomerId != customer.Id)
        {
            car.Customer.Cars.Remove(car);
            customer.Cars.Add(car);
        }

        car.CarCategory = dto.CarCategory;
        car.LicensePlate = dto.LicensePlate;
        car.Brand = dto.Brand;
        car.CustomerId = customer.Id;
        car.Customer = customer;

        carRepository.Update(car);

        return MapToDto(car);
    }

    public void Delete(int dtoId)
    {
        var car = carRepository.Read(dtoId);

        car.Customer.Cars.Remove(car);
        carRepository.Delete(dtoId);
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
