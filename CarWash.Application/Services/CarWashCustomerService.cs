using CarWash.Application.Contracts.Cars;
using CarWash.Application.Contracts.Customers;
using CarWash.Domain;
using CarWash.Domain.Entities;

namespace CarWash.Application.Services;

/// <summary>
/// Служба для CRUD-операций над клиентами
/// </summary>
public class CarWashCustomerService(
    IRepository<CarWashCustomer, int> customerRepository)
    : ICarWashCustomerService
{
    public CarWashCustomerDto Create(CarWashCustomerCreateUpdateDto dto)
    {
        var newId = customerRepository.ReadAll()
            .Select(customer => customer.Id)
            .DefaultIfEmpty(0)
            .Max() + 1;

        var customer = new CarWashCustomer
        {
            Id = newId,
            LastName = dto.LastName,
            FirstName = dto.FirstName,
            Patronymic = dto.Patronymic,
            Phone = dto.Phone
        };

        customerRepository.Create(customer);

        return MapToDto(customer);
    }

    public CarWashCustomerDto Get(int dtoId)
    {
        return MapToDto(customerRepository.Read(dtoId));
    }

    public List<CarWashCustomerDto> GetAll()
    {
        return customerRepository.ReadAll()
            .Select(MapToDto)
            .ToList();
    }

    public CarWashCustomerDto Update(
        CarWashCustomerCreateUpdateDto dto,
        int dtoId)
    {
        var customer = new CarWashCustomer
        {
            Id = dtoId,
            LastName = dto.LastName,
            FirstName = dto.FirstName,
            Patronymic = dto.Patronymic,
            Phone = dto.Phone
        };

        customerRepository.Update(customer);

        return MapToDto(customer);
    }

    public void Delete(int dtoId)
    {
        customerRepository.Delete(dtoId);
    }

    public List<CarDto> GetCustomerCars(int customerId)
    {
        var customer = customerRepository.Read(customerId);

        return customer.Cars
            .Select(car => new CarDto(
                car.Id,
                car.CarCategory,
                car.LicensePlate,
                car.Brand,
                car.CustomerId))
            .ToList();
    }

    private static CarWashCustomerDto MapToDto(CarWashCustomer customer)
    {
        return new CarWashCustomerDto(
            customer.Id,
            customer.LastName,
            customer.FirstName,
            customer.Patronymic,
            customer.Phone);
    }
}
