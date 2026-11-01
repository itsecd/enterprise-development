using AutoServiceLab.Domain.Data;
using AutoServiceLab.Domain.Entities;

namespace AutoServiceLab.Infrastructure.InMemory.Data;

/// <summary>
/// Статическое хранилище данных в памяти
/// </summary>
public static class AutoServiceData
{
    public static List<Client> Clients { get; } = DataSeeder.GetClients();
    public static List<Mechanic> Mechanics { get; } = DataSeeder.GetMechanics();
    public static List<WorkType> WorkTypes { get; } = DataSeeder.GetWorkTypes();
    public static List<Car> Cars { get; } = DataSeeder.GetCars(Clients);
    public static List<ServiceOrder> ServiceOrders { get; } = DataSeeder.GetServiceOrders(Clients, Cars);
    public static List<OrderWork> OrderWorks { get; } = DataSeeder.GetOrderWorks(ServiceOrders, WorkTypes);
    public static List<OrderMechanic> OrderMechanics { get; } = DataSeeder.GetOrderMechanics(ServiceOrders, Mechanics);
}
