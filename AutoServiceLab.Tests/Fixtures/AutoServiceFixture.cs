using AutoServiceLab.Domain.Data;
using AutoServiceLab.Domain.Entities;

namespace AutoServiceLab.Tests.Fixtures;

/// <summary>
/// Тестовый набор данных автосервиса
/// </summary>
public class AutoServiceFixture
{
    /// <summary>
    /// Список клиентов
    /// </summary>
    public readonly List<Client> Clients;

    /// <summary>
    /// Список автомобилей
    /// </summary>
    public readonly List<Car> Cars;

    /// <summary>
    /// Список механиков
    /// </summary>
    public readonly List<Mechanic> Mechanics;

    /// <summary>
    /// Список видов работ
    /// </summary>
    public readonly List<WorkType> WorkTypes;

    /// <summary>
    /// Список заказов
    /// </summary>
    public readonly List<ServiceOrder> ServiceOrders;

    /// <summary>
    /// Список связей заказов и видов работ
    /// </summary>
    public readonly List<OrderWork> OrderWorks;

    /// <summary>
    /// Список связей заказов и механиков
    /// </summary>
    public readonly List<OrderMechanic> OrderMechanics;

    /// <summary>
    /// Создаёт тестовый набор данных
    /// </summary>
    public AutoServiceFixture()
    {
        Clients = DataSeeder.GetClients();
        Cars = DataSeeder.GetCars(Clients);
        Mechanics = DataSeeder.GetMechanics();
        WorkTypes = DataSeeder.GetWorkTypes();
        ServiceOrders = DataSeeder.GetServiceOrders(Clients, Cars);
        OrderWorks = DataSeeder.GetOrderWorks(ServiceOrders, WorkTypes);
        OrderMechanics = DataSeeder.GetOrderMechanics(ServiceOrders, Mechanics);
    }
}
