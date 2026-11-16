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
    public async Task<CarWashCustomerDto> Create(
        CarWashCustomerCreateUpdateDto dto)
    {
        var newId = (await customerRepository.ReadAll())
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

        await customerRepository.Create(customer);

        return MapToDto(customer);
    }

    public async Task<CarWashCustomerDto?> Get(int dtoId)
    {
        var customer = await customerRepository.Read(dtoId);

        return customer is null ? null : MapToDto(customer);
    }

    public async Task<IList<CarWashCustomerDto>> GetAll()
    {
        var customers = await customerRepository.ReadAll();

        return customers
            .Select(MapToDto)
            .ToList();
    }

    public async Task<CarWashCustomerDto> Update(
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

        var updatedCustomer = await customerRepository.Update(customer);

        return MapToDto(updatedCustomer);
    }

    public async Task<bool> Delete(int dtoId)
    {
        return await customerRepository.Delete(dtoId);
    }

    public async Task<IList<CarDto>> GetCustomerCars(int customerId)
    {
        var customer = await customerRepository.Read(customerId)
            ?? throw new KeyNotFoundException(
                $"Клиент с Id {customerId} не найден");

        return customer.Cars
            .Select(car => new CarDto(
                car.Id,
                car.CarCategory,
                car.LicensePlate,
                car.Brand,
                car.CustomerId))
            .ToList();
    }

    private static CarWashCustomerDto MapToDto(
        CarWashCustomer customer)
    {
        return new CarWashCustomerDto(
            customer.Id,
            customer.LastName,
            customer.FirstName,
            customer.Patronymic,
            customer.Phone);
    }
}