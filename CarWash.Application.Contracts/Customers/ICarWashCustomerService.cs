using CarWash.Application.Contracts;
using CarWash.Application.Contracts.Cars;

namespace CarWash.Application.Contracts.Customers;

/// <summary>
/// Наследник службы аппликейшена для работы с клиентами
/// </summary>
public interface ICarWashCustomerService
    : IApplicationService<CarWashCustomerDto, CarWashCustomerCreateUpdateDto, int>
{
    /// <summary>
    /// Получает список автомобилей указанного клиента
    /// </summary>
    List<CarDto> GetCustomerCars(int customerId);
}
