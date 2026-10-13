using CarWash.Domain.Entities;
using CarWash.Domain.Enums;

namespace CarWash.Tests;

/// <summary>
/// Набор тестовых данных для проведения юнит-тестирования автомойки
/// </summary>
public class CarWashFixture
{
    private static readonly DateTime _baseTime = new(2026, 6, 1, 10, 0, 0);

    public static List<Car> Cars { get; set; } =
        [
        new Car 
        { 
            Id = 1,
            LicensePlate = "А101АА663", 
            Brand = "Lada Ballada" 
        },
        new Car 
        { 
            Id = 2, 
            LicensePlate = "В206ВВ663", 
            Brand = "Toyota Yenota" 
        },
        new Car 
        { 
            Id = 3, 
            LicensePlate = "С302СС363", 
            Brand = "CUMAZ 67" 
        },
        new Car 
        { 
            Id = 4, 
            LicensePlate = "Е456ЕЕ163", 
            Brand = "BMW X5group" 
        },
        new Car 
        { 
            Id = 5, 
            LicensePlate = "К545КК067", 
            Brand = "Kia Rio" 
        },
        new Car 
        { 
            Id = 6, 
            LicensePlate = "М611ММ663", 
            Brand = "Hyundai Slesaris" 
        },
        new Car 
        { 
            Id = 7, 
            LicensePlate = "Х701ТР167", 
            Brand = "Ssania R500" 
        },
        new Car 
        { 
            Id = 8, 
            LicensePlate = "О893ОО557", 
            Brand = "GAZelle Prev" 
        },
        new Car 
        { 
            Id = 9, 
            LicensePlate = "Р999РР347", 
            Brand = "Mercedes Splinter" 
        },
        new Car 
        { 
            Id = 10, 
            LicensePlate = "Т042ТТ127", 
            Brand = "Geely Monjaro" 
        }
        ];

    public static List<Client> Clients { get; set; } =
        [
        new Client 
        { 
            Id = 1, 
            FullName = "Иванов Иван Иванович", 
            Phone = "+78461112233", 
            Cars = [Cars[0]] 
        },
        new Client 
        { 
            Id = 2, 
            FullName = "Петров Сергей Алексеевич", 
            Phone = "+78462223344", 
            Cars = [Cars[1], Cars[4]] 
        },
        new Client 
        { 
            Id = 3, 
            FullName = "Сидоров Алексей Михайлович", 
            Phone = "+78463334455", 
            Cars = [Cars[2]] 
        },
        new Client 
        { 
            Id = 4, 
            FullName = "Смирнова Елена Павловна", 
            Phone = "+78464445566", 
            Cars = [Cars[3]] 
        },
        new Client 
        { 
            Id = 5, 
            FullName = "Кузнецов Дмитрий Николаевич", 
            Phone = "+78465556677", 
            Cars = [Cars[5]] 
        },
        new Client 
        { 
            Id = 6, 
            FullName = "Васильев Андрей Игоревич", 
            Phone = "+78466667788", 
            Cars = [Cars[6]] 
        },
        new Client 
        { 
            Id = 7, 
            FullName = "Попов Михаил Сергеевич", 
            Phone = "+78467778899", 
            Cars = [Cars[7]] 
        },
        new Client 
        { 
            Id = 8, 
            FullName = "Соколова Анна Владимировна", 
            Phone = "+78468889900", 
            Cars = [Cars[8]] 
        },
        new Client 
        { 
            Id = 9, 
            FullName = "Михайлов Денис Олегович", 
            Phone = "+78469990011", 
            Cars = [Cars[9]] 
        },
        new Client 
        { 
            Id = 10, 
            FullName = "Федоров Роман Денисович", 
            Phone = "+78460001122", 
            Cars = [Cars[0]] 
        }
        ];

    public static List<Service> Services { get; set; } =
        [
        new Service 
        { Id = 1, 
            Name = "Экспресс-мойка", 
            Category = CarCategory.Passenger, 
            Price = 600m, 
            DurationMinutes = 20 
        },
        new Service 
        { Id = 2, 
            Name = "Комплексная мойка", 
            Category = CarCategory.Passenger, 
            Price = 1400m, 
            DurationMinutes = 45 
        },
        new Service 
        { Id = 3, 
            Name = "Стандартная мойка SUV", 
            Category = CarCategory.Suv, 
            Price = 1100m, 
            DurationMinutes = 35 
        },
        new Service 
        { Id = 4, 
            Name = "Химчистка салона", 
            Category = CarCategory.Passenger, 
            Price = 4500m, 
            DurationMinutes = 90 
        },
        new Service 
        { Id = 5, 
            Name = "Мойка грузовика", 
            Category = CarCategory.Truck, 
            Price = 2800m, 
            DurationMinutes = 60 
        },
        new Service 
        { Id = 6, 
            Name = "Полировка кузова", 
            Category = CarCategory.Suv, 
            Price = 3500m, 
            DurationMinutes = 75 
        },
        new Service 
        { Id = 7, 
            Name = "Мойка двигателя", 
            Category = CarCategory.Suv, 
            Price = 1200m, 
            DurationMinutes = 30 
        },
        new Service 
        { Id = 8, 
            Name = "Обработка воском", 
            Category = CarCategory.Passenger, 
            Price = 500m, 
            DurationMinutes = 15 
        },
        new Service 
        { Id = 9, 
            Name = "Мойка днища", 
            Category = CarCategory.Suv, 
            Price = 900m, 
            DurationMinutes = 25 
        },
        new Service 
        { Id = 10, 
            Name = "Мойка минивэна", 
            Category = CarCategory.Minivan, 
            Price = 1800m, 
            DurationMinutes = 50 
        }
        ];

    public List<Order> Orders { get; set; } =
    [
        new Order 
        { 
            Id = 1, 
            Client = Clients[0], 
            Car = Cars[0],
            Service = Services[0],
            StartTime = _baseTime, 
            BoxNumber = 1
        },
        new Order 
        {
            Id = 2, 
            Client = Clients[0], 
            Car = Cars[0], 
            Service = Services[0], 
            StartTime = _baseTime.AddHours(2),
            BoxNumber = 1
        },
        new Order 
        { 
            Id = 3, 
            Client = Clients[0],
            Car = Cars[0], 
            Service = Services[1],
            StartTime = _baseTime.AddDays(1),
            BoxNumber = 2
        },
        new Order 
        { 
            Id = 4, 
            Client = Clients[0], 
            Car = Cars[0],
            Service = Services[7], 
            StartTime = _baseTime.AddDays(2), 
            BoxNumber = 1
        },
        new Order 
        { 
            Id = 5, 
            Client = Clients[0], 
            Car = Cars[0], 
            Service = Services[0],
            StartTime = _baseTime.AddDays(3),
            BoxNumber = 1
        },

        new Order 
        { 
            Id = 6, 
            Client = Clients[1], 
            Car = Cars[1], 
            Service = Services[1], 
            StartTime = _baseTime, 
            BoxNumber = 2 
        },
        new Order 
        { 
            Id = 7, 
            Client = Clients[1], 
            Car = Cars[1], 
            Service = Services[1], 
            StartTime = _baseTime.AddHours(3), 
            BoxNumber = 1 
        },
        new Order 
        { 
            Id = 8, 
            Client = Clients[1],
            Car = Cars[4],
            Service = Services[2], 
            StartTime = _baseTime.AddDays(1),
            BoxNumber = 3 
        },
        new Order 
        { 
            Id = 9, 
            Client = Clients[1], 
            Car = Cars[4], 
            Service = Services[0], 
            StartTime = _baseTime.AddDays(2), 
            BoxNumber = 2 
        },

        new Order 
        { 
            Id = 10, 
            Client = Clients[2],
            Car = Cars[2], 
            Service = Services[4], 
            StartTime = _baseTime,
            BoxNumber = 3 
        },
        new Order 
        { 
            Id = 11, 
            Client = Clients[2], 
            Car = Cars[2],
            Service = Services[4], 
            StartTime = _baseTime.AddDays(2),
            BoxNumber = 3 
        },
        new Order 
        { 
            Id = 12, 
            Client = Clients[2], 
            Car = Cars[2], 
            Service = Services[2], 
            StartTime = _baseTime.AddDays(4), 
            BoxNumber = 3 
        },

        new Order 
        { 
            Id = 13, 
            Client = Clients[3],
            Car = Cars[3], 
            Service = Services[2],
            StartTime = _baseTime.AddMinutes(10),
            BoxNumber = 4 
        },
        new Order 
        { 
            Id = 14, 
            Client = Clients[3], 
            Car = Cars[3], 
            Service = Services[0], 
            StartTime = _baseTime.AddDays(1), 
            BoxNumber = 1
        },

        new Order 
        { 
            Id = 15, 
            Client = Clients[4],
            Car = Cars[5],
            Service = Services[3],
            StartTime = _baseTime.AddHours(5),
            BoxNumber = 2 
        },
        new Order 
        { 
            Id = 16, 
            Client = Clients[4],
            Car = Cars[5], 
            Service = Services[5],
            StartTime = _baseTime.AddDays(3),
            BoxNumber = 2
        },

        new Order 
        { 
            Id = 17, 
            Client = Clients[5], 
            Car = Cars[6], 
            Service = Services[6], 
            StartTime = _baseTime.AddHours(4), 
            BoxNumber = 4
        },
        new Order 
        { 
            Id = 18, 
            Client = Clients[6],
            Car = Cars[7], 
            Service = Services[9], 
            StartTime = _baseTime.AddHours(1), 
            BoxNumber = 5 
        },
        new Order 
        { 
            Id = 19, 
            Client = Clients[7], 
            Car = Cars[8], 
            Service = Services[8], 
            StartTime = _baseTime.AddHours(2),
            BoxNumber = 5
        },
        new Order 
        { 
            Id = 20, 
            Client = Clients[8], 
            Car = Cars[9], 
            Service = Services[1],
            StartTime = _baseTime.AddHours(6), 
            BoxNumber = 2 
        },
        new Order 
        { 
            Id = 21, 
            Client = Clients[9], 
            Car = Cars[0], 
            Service = Services[3], 
            StartTime = _baseTime.AddDays(5), 
            BoxNumber = 2 
        }
    ];
}