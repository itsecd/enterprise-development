using CarWash.Domain.Models;

namespace CarWash.Domain.Data;

/// <summary>
/// Общие начальные данные сервера и тестов
/// </summary>
public class CarWashData
{
    /// <summary>
    /// Постоянная опорная дата начальных данных для миграций базы
    /// </summary>
    public static DateTime MigrationReferenceTime { get; } = new(2026, 1, 1, 12, 0, 0);

    /// <summary>
    /// Автомобили
    /// </summary>
    public IReadOnlyList<Car> Cars { get; }

    /// <summary>
    /// Клиенты
    /// </summary>
    public IReadOnlyList<Customer> Customers { get; }

    /// <summary>
    /// Услуги
    /// </summary>
    public IReadOnlyList<WashService> WashServices { get; }

    /// <summary>
    /// Заказы
    /// </summary>
    public IReadOnlyList<Order> Orders { get; }

    /// <summary>
    /// Создать набор данных с одним выполняющимся заказом относительно опорного времени
    /// </summary>
    /// <param name="referenceTime">Опорное время; для тестов по умолчанию используется текущее время</param>
    public CarWashData(DateTime? referenceTime = null)
    {
        Customers =
        [
            new() { Id = 1, FullName = "Иванов Иван Иванович", Phone = "79000000001" },
            new() { Id = 2, FullName = "Петров Петр Петрович", Phone = "79000000002" },
            new() { Id = 3, FullName = "Сидоров Алексей Олегович", Phone = "79000000003" },
            new() { Id = 4, FullName = "Смирнов Максим Игоревич", Phone = "79000000004" },
            new() { Id = 5, FullName = "Кузнецов Андрей Сергеевич", Phone = "79000000005" },
            new() { Id = 6, FullName = "Попов Дмитрий Андреевич", Phone = "79000000006" },
            new() { Id = 7, FullName = "Волков Роман Ильич", Phone = "79000000007" },
            new() { Id = 8, FullName = "Соколов Павел Андреевич", Phone = "79000000008" },
            new() { Id = 9, FullName = "Орлов Олег Сергеевич", Phone = "79000000009" },
            new() { Id = 10, FullName = "Морозов Антон Петрович", Phone = "79000000010" }
        ];

        Cars =
        [
            new() { Id = 1, LicensePlate = "A001AA", Brand = "Toyota", Customer = Customers[0] },
            new() { Id = 2, LicensePlate = "A002AA", Brand = "Kia", Customer = Customers[1] },
            new() { Id = 3, LicensePlate = "A003AA", Brand = "Lada", Customer = Customers[2] },
            new() { Id = 4, LicensePlate = "A004AA", Brand = "BMW", Customer = Customers[3] },
            new() { Id = 5, LicensePlate = "A005AA", Brand = "Audi", Customer = Customers[4] },
            new() { Id = 6, LicensePlate = "A006AA", Brand = "Ford", Customer = Customers[5] },
            new() { Id = 7, LicensePlate = "A007AA", Brand = "Skoda", Customer = Customers[6] },
            new() { Id = 8, LicensePlate = "A008AA", Brand = "Renault", Customer = Customers[7] },
            new() { Id = 9, LicensePlate = "A009AA", Brand = "Hyundai", Customer = Customers[8] },
            new() { Id = 10, LicensePlate = "A010AA", Brand = "Volkswagen", Customer = Customers[9] }
        ];

        foreach (var car in Cars)
        {
            car.CustomerId = car.Customer.Id;
            car.Customer.Cars.Add(car);
        }

        WashServices =
        [
            new() { Id = 1, Name = "Стандартная мойка", Category = CarCategory.Sedan, Cost = 500m, DurationMinutes = 30 },
            new() { Id = 2, Name = "Мойка кузова", Category = CarCategory.Sedan, Cost = 400m, DurationMinutes = 20 },
            new() { Id = 3, Name = "Комплексная мойка", Category = CarCategory.Sedan, Cost = 800m, DurationMinutes = 40 },
            new() { Id = 4, Name = "Мойка внедорожника", Category = CarCategory.SUV, Cost = 1000m, DurationMinutes = 45 },
            new() { Id = 5, Name = "Мойка грузовика", Category = CarCategory.Truck, Cost = 1500m, DurationMinutes = 60 },
            new() { Id = 6, Name = "Мойка мотоцикла", Category = CarCategory.Motorcycle, Cost = 300m, DurationMinutes = 15 },
            new() { Id = 7, Name = "Мойка микроавтобуса", Category = CarCategory.Minibus, Cost = 900m, DurationMinutes = 35 },
            new() { Id = 8, Name = "Уборка салона", Category = CarCategory.Sedan, Cost = 600m, DurationMinutes = 30 },
            new() { Id = 9, Name = "Мойка колёс", Category = CarCategory.Sedan, Cost = 200m, DurationMinutes = 10 },
            new() { Id = 10, Name = "Нанесение воска", Category = CarCategory.Sedan, Cost = 700m, DurationMinutes = 25 }
        ];

        var now = referenceTime ?? DateTime.Now;
        var orders = new List<Order>();
        for (var customerIndex = 0; customerIndex < 5; customerIndex++)
        {
            for (var visit = 0; visit < 5 - customerIndex; visit++)
            {
                orders.Add(new Order
                {
                    Id = orders.Count + 1,
                    Customer = Customers[customerIndex],
                    Car = Cars[customerIndex],
                    WashService = WashServices[customerIndex],
                    StartTime = now.AddDays(-1),
                    BoxNumber = customerIndex + 1
                });
            }
        }

        orders.Add(new Order
        {
            Id = 16,
            Customer = Customers[0],
            Car = Cars[0],
            WashService = WashServices[0],
            StartTime = now.AddMinutes(-5),
            BoxNumber = 1
        });
        foreach (var order in orders)
        {
            order.CustomerId = order.Customer.Id;
            order.CarId = order.Car.Id;
            order.WashServiceId = order.WashService.Id;
        }
        Orders = orders;
    }
}
