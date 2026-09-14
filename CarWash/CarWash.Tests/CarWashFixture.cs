using CarWash.Domain;

namespace CarWash.Tests;

/// <summary>
/// Provides deterministic, fictional data for car wash analytics tests.
/// </summary>
/// <remarks>
/// Each collection contains ten entities. Orders reuse the clients, vehicles,
/// and services from these collections. Tests sharing this fixture should not
/// modify the entities or the clients' vehicle lists.
/// </remarks>
public class CarWashFixture
{
    /// <summary>
    /// Gets the fixed local time used by time-dependent test scenarios.
    /// </summary>
    public DateTime ReferenceTime { get; } = new(2026, 9, 14, 10, 0, 0);

    /// <summary>
    /// Gets the vehicles available in the test dataset.
    /// </summary>
    public IReadOnlyList<Vehicle> Vehicles { get; }

    /// <summary>
    /// Gets the clients and their associated vehicles.
    /// </summary>
    public IReadOnlyList<Client> Clients { get; }

    /// <summary>
    /// Gets the services, including services that have not been ordered.
    /// </summary>
    public IReadOnlyList<Service> Services { get; }

    /// <summary>
    /// Gets the orders, deliberately arranged out of chronological order.
    /// </summary>
    public IReadOnlyList<Order> Orders { get; }

    /// <summary>
    /// Creates ten vehicles, ten clients, ten services, and ten related orders.
    /// </summary>
    public CarWashFixture()
    {
        Vehicles =
        [
            new()
            {
                GovNumber = "А101АА163",
                Brand = "Lada",
                Category = VehicleCategory.Car
            },
            new()
            {
                GovNumber = "В202ВВ163",
                Brand = "Toyota",
                Category = VehicleCategory.Car
            },
            new()
            {
                GovNumber = "Е303ЕЕ163",
                Brand = "GAZ",
                Category = VehicleCategory.Truck
            },
            new()
            {
                GovNumber = "4004АВ163",
                Brand = "Honda",
                Category = VehicleCategory.Motorcycle
            },
            new()
            {
                GovNumber = "К505КК163",
                Brand = "Kia",
                Category = VehicleCategory.Car
            },
            new()
            {
                GovNumber = "М606ММ163",
                Brand = "Skoda",
                Category = VehicleCategory.Car
            },
            new()
            {
                GovNumber = "Н707НН163",
                Brand = "Kamaz",
                Category = VehicleCategory.Truck
            },
            new()
            {
                GovNumber = "8008АС163",
                Brand = "Yamaha",
                Category = VehicleCategory.Motorcycle
            },
            new()
            {
                GovNumber = "С909СС163",
                Brand = "Renault",
                Category = VehicleCategory.Car
            },
            new()
            {
                GovNumber = "Т010ТТ163",
                Brand = "Hyundai",
                Category = VehicleCategory.Car
            }
        ];

        Clients =
        [
            new()
            {
                FullName = "Иванов Алексей Сергеевич",
                PhoneNumber = "+79000000001",
                Vehicles = [Vehicles[0], Vehicles[1]]
            },
            new()
            {
                FullName = "Петрова Мария Андреевна",
                PhoneNumber = "+79000000002",
                Vehicles = [Vehicles[2]]
            },
            new()
            {
                FullName = "Сидоров Дмитрий Олегович",
                PhoneNumber = "+79000000003",
                Vehicles = [Vehicles[3]]
            },
            new()
            {
                FullName = "Смирнова Анна Игоревна",
                PhoneNumber = "+79000000004",
                Vehicles = [Vehicles[4]]
            },
            new()
            {
                FullName = "Кузнецов Павел Викторович",
                PhoneNumber = "+79000000005",
                Vehicles = [Vehicles[5], Vehicles[6]]
            },
            new()
            {
                FullName = "Попова Елена Михайловна",
                PhoneNumber = "+79000000006",
                Vehicles = [Vehicles[7]]
            },
            new()
            {
                FullName = "Васильев Артём Николаевич",
                PhoneNumber = "+79000000007",
                Vehicles = [Vehicles[8]]
            },
            new()
            {
                FullName = "Фёдорова Ольга Романовна",
                PhoneNumber = "+79000000008",
                Vehicles = [Vehicles[9]]
            },
            new()
            {
                FullName = "Михайлов Илья Денисович",
                PhoneNumber = "+79000000009",
                Vehicles = []
            },
            new()
            {
                FullName = "Новикова Дарья Алексеевна",
                PhoneNumber = "+79000000010",
                Vehicles = []
            }
        ];

        Services =
        [
            new()
            {
                Name = "Мойка кузова легкового автомобиля",
                Price = 600m,
                DurationMinutes = 30,
                Category = VehicleCategory.Car
            },
            new()
            {
                Name = "Уборка салона легкового автомобиля",
                Price = 800m,
                DurationMinutes = 20,
                Category = VehicleCategory.Car
            },
            new()
            {
                Name = "Комплексная мойка легкового автомобиля",
                Price = 1500m,
                DurationMinutes = 60,
                Category = VehicleCategory.Car
            },
            new()
            {
                Name = "Мойка кузова грузового автомобиля",
                Price = 1200m,
                DurationMinutes = 45,
                Category = VehicleCategory.Truck
            },
            new()
            {
                Name = "Комплексная мойка грузового автомобиля",
                Price = 2500m,
                DurationMinutes = 90,
                Category = VehicleCategory.Truck
            },
            new()
            {
                Name = "Мойка мотоцикла",
                Price = 400m,
                DurationMinutes = 20,
                Category = VehicleCategory.Motorcycle
            },
            new()
            {
                Name = "Мойка мотоцикла с защитным покрытием",
                Price = 700m,
                DurationMinutes = 30,
                Category = VehicleCategory.Motorcycle
            },
            new()
            {
                Name = "Нанесение воска на легковой автомобиль",
                Price = 500m,
                DurationMinutes = 15,
                Category = VehicleCategory.Car
            },
            new()
            {
                Name = "Мойка колёс легкового автомобиля",
                Price = 300m,
                DurationMinutes = 15,
                Category = VehicleCategory.Car
            },
            new()
            {
                Name = "Уборка кабины грузового автомобиля",
                Price = 900m,
                DurationMinutes = 30,
                Category = VehicleCategory.Truck
            }
        ];

        // These fixed local timestamps use the same time convention as ReferenceTime.
        // Bookings in the same box do not overlap; some meet at an exact boundary.
        Orders =
        [
            new()
            {
                Client = Clients[4],
                Vehicle = Vehicles[5],
                Service = Services[2],
                ServiceStartTime = new(2026, 9, 14, 10, 30, 0),
                BoxNumber = 1
            },
            new()
            {
                Client = Clients[2],
                Vehicle = Vehicles[3],
                Service = Services[5],
                ServiceStartTime = new(2026, 9, 14, 9, 0, 0),
                BoxNumber = 3
            },
            new()
            {
                Client = Clients[0],
                Vehicle = Vehicles[0],
                Service = Services[0],
                ServiceStartTime = new(2026, 9, 13, 9, 0, 0),
                BoxNumber = 1
            },
            new()
            {
                Client = Clients[5],
                Vehicle = Vehicles[7],
                Service = Services[6],
                ServiceStartTime = new(2026, 9, 14, 9, 50, 0),
                BoxNumber = 4
            },
            new()
            {
                Client = Clients[1],
                Vehicle = Vehicles[2],
                Service = Services[3],
                ServiceStartTime = new(2026, 9, 14, 8, 0, 0),
                BoxNumber = 2
            },
            new()
            {
                Client = Clients[3],
                Vehicle = Vehicles[4],
                Service = Services[1],
                ServiceStartTime = new(2026, 9, 14, 10, 0, 0),
                BoxNumber = 1
            },
            new()
            {
                Client = Clients[0],
                Vehicle = Vehicles[0],
                Service = Services[0],
                ServiceStartTime = new(2026, 9, 14, 9, 30, 0),
                BoxNumber = 1
            },
            new()
            {
                Client = Clients[1],
                Vehicle = Vehicles[2],
                Service = Services[3],
                ServiceStartTime = new(2026, 9, 14, 9, 40, 0),
                BoxNumber = 2
            },
            new()
            {
                Client = Clients[0],
                Vehicle = Vehicles[1],
                Service = Services[0],
                ServiceStartTime = new(2026, 9, 13, 10, 0, 0),
                BoxNumber = 1
            },
            new()
            {
                Client = Clients[2],
                Vehicle = Vehicles[3],
                Service = Services[5],
                ServiceStartTime = new(2026, 9, 14, 10, 0, 0),
                BoxNumber = 3
            }
        ];
    }
}
