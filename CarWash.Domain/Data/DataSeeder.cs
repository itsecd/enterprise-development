using CarWash.Domain.Entities;

namespace CarWash.Domain.Data;

/// <summary>
/// Датасидер для первичного наполнения базы данных
/// </summary>
public class DataSeeder
{
    /// <summary>
    /// Список клиентов
    /// </summary>
    public List<CarWashCustomer> Customers { get; }

    /// <summary>
    /// Список автомобилей
    /// </summary>
    public List<Car> Cars { get; }

    /// <summary>
    /// Список процедур
    /// </summary>
    public List<CarWashProcedure> Procedures { get; }

    /// <summary>
    /// Список заказов
    /// </summary>
    public List<Order> Orders { get; }

    public DataSeeder()
    {
        var data = new CarWashData();

        Customers = data.Customers;
        Cars = data.Cars;
        Procedures = data.Procedures;
        Orders = data.Orders;
    }
}
