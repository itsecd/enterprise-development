using CarWash.Domain.Entities;
using CarWash.Domain.Shared.Enums;

namespace CarWash.Domain.Data;

/// <summary>
/// Тестовые данные автомойки
/// </summary>
public class CarWashData
{
    /// <summary>
    /// Текущее время для тестовых данных
    /// </summary>
    public DateTime Now => DateTime.Now;

    /// <summary>
    /// Клиенты автомойки
    /// </summary>
    public List<CarWashCustomer> Customers { get; } = [];

    /// <summary>
    /// Автомобили клиентов
    /// </summary>
    public List<Car> Cars { get; } = [];

    /// <summary>
    /// Услуги автомойки
    /// </summary>
    public List<CarWashProcedure> Procedures { get; } = [];

    /// <summary>
    /// Заказы автомойки
    /// </summary>
    public List<Order> Orders { get; } = [];

    public CarWashData()
    {
        Customers.AddRange(
            [
                new CarWashCustomer
                {
                    Id = 1,
                    LastName = "Сопранов",
                    FirstName = "Антон",
                    Patronymic = null,
                    Phone = "+79010000001"
                },
                new CarWashCustomer
                {
                    Id = 2,
                    LastName = "Ошпаренный",
                    FirstName = "Дмитрий",
                    Patronymic = "Алексеевич",
                    Phone = "+79020000002"
                },
                new CarWashCustomer
                {
                    Id = 3,
                    LastName = "Папс",
                    FirstName = "Боб",
                    Patronymic = null,
                    Phone = "+79030000003"
                },
                new CarWashCustomer
                {
                    Id = 4,
                    LastName = "Быков",
                    FirstName = "Андрей",
                    Patronymic = "Евгеньевич",
                    Phone = "+79040000004"
                },
                new CarWashCustomer
                {
                    Id = 5,
                    LastName = "Бобков",
                    FirstName = "Геннадий",
                    Patronymic = "Андреевич",
                    Phone = "+79040000066",
                },
                new CarWashCustomer
                {
                    Id = 6,
                    LastName = "Афанасьев",
                    FirstName = "Федор",
                    Patronymic = "Иванович",
                    Phone = "+79060000006"
                },
                new CarWashCustomer
                {
                    Id = 7,
                    LastName = "Васильев",
                    FirstName = "Максим",
                    Patronymic = "Максимович",
                    Phone = "+79990000007"
                },
                new CarWashCustomer
                {
                    Id = 8,
                    LastName = "Новиков",
                    FirstName = "Николай",
                    Patronymic = null,
                    Phone = "+79990000008"
                },
                new CarWashCustomer
                {
                    Id = 9,
                    LastName = "Фёдоров",
                    FirstName = "Роман",
                    Patronymic = "Романович",
                    Phone = "+79990000009"
                },
                new CarWashCustomer
                {
                    Id = 10,
                    LastName = "Морозов",
                    FirstName = "Михаил",
                    Patronymic = "Михайлович",
                    Phone = "+79990000010"
                },
                new CarWashCustomer
                {
                    Id = 11,
                    LastName = "Лоренцо",
                    FirstName = "Анелло",
                    Patronymic = null,
                    Phone = "+79990000011"
                },
                new CarWashCustomer
                {
                    Id = 12,
                    LastName = "Алексеев",
                    FirstName = "Виктор",
                    Patronymic = "Викторович",
                    Phone = "+79990000012"
                }
            ]);

        Cars.AddRange(
            [
                new Car { Id = 1, CarCategory = CarCategory.Passenger, LicensePlate = "А111АА", Brand = "Chevrolet", CustomerId = 1, Customer = GetCustomer(1) },
                new Car { Id = 2, CarCategory = CarCategory.Passenger, LicensePlate = "А222АА", Brand = "Cadillac", CustomerId = 1, Customer = GetCustomer(1) },
                new Car { Id = 3, CarCategory = CarCategory.Passenger, LicensePlate = "О248ВН", Brand = "BMW", CustomerId = 2, Customer = GetCustomer(2) },
                new Car { Id = 4, CarCategory = CarCategory.Passenger, LicensePlate = "О249ВН", Brand = "BMW", CustomerId = 2, Customer = GetCustomer(2) },
                new Car { Id = 5, CarCategory = CarCategory.Motorcycle, LicensePlate = "С555СС", Brand = "Harley-Davidson", CustomerId = 3, Customer = GetCustomer(3) },
                new Car { Id = 6, CarCategory = CarCategory.Motorcycle, LicensePlate = "Е666ЕЕ", Brand = "Honda", CustomerId = 4, Customer = GetCustomer(4) },
                new Car { Id = 7, CarCategory = CarCategory.Passenger, LicensePlate = "К777КК", Brand = "Lada", CustomerId = 5, Customer = GetCustomer(5) },
                new Car { Id = 8, CarCategory = CarCategory.Truck, LicensePlate = "М571АК", Brand = "KamAZ", CustomerId = 6, Customer = GetCustomer(6) },
                new Car { Id = 9, CarCategory = CarCategory.Passenger, LicensePlate = "Н101НН", Brand = "Skoda", CustomerId = 7, Customer = GetCustomer(7) },
                new Car { Id = 10, CarCategory = CarCategory.Passenger, LicensePlate = "О202ОО", Brand = "Volvo", CustomerId = 8, Customer = GetCustomer(8) },
                new Car { Id = 11, CarCategory = CarCategory.Passenger, LicensePlate = "Р303РР", Brand = "Nissan", CustomerId = 9, Customer = GetCustomer(9) },
                new Car { Id = 12, CarCategory = CarCategory.Passenger, LicensePlate = "Т404ТТ", Brand = "Mazda", CustomerId = 10, Customer = GetCustomer(10) },
                new Car { Id = 13, CarCategory = CarCategory.Passenger, LicensePlate = "Т505ТТ", Brand = "Lexus", CustomerId = 10, Customer = GetCustomer(10) },
                new Car { Id = 14, CarCategory = CarCategory.Truck, LicensePlate = "У606УУ", Brand = "GMC", CustomerId = 11, Customer = GetCustomer(11) },
                new Car { Id = 15, CarCategory = CarCategory.Passenger, LicensePlate = "Х707ХХ", Brand = "Peugeot", CustomerId = 12, Customer = GetCustomer(12) }
            ]);

        Procedures.AddRange(
            [
                new CarWashProcedure
                {
                    Id = 1,
                    Name = "Комплексная мойка",
                    CarCategory = CarCategory.Passenger,
                    Price = 1500m,
                    Duration = TimeSpan.FromMinutes(60)
                },
                new CarWashProcedure
                {
                    Id = 2,
                    Name = "Мойка кузова",
                    CarCategory = CarCategory.Passenger,
                    Price = 800m,
                    Duration = TimeSpan.FromMinutes(30)
                },
                new CarWashProcedure
                {
                    Id = 3,
                    Name = "Мойка салона",
                    CarCategory = CarCategory.Passenger,
                    Price = 1000m,
                    Duration = TimeSpan.FromMinutes(45)
                },
                new CarWashProcedure
                {
                    Id = 4,
                    Name = "Мойка двигателя",
                    CarCategory = CarCategory.Passenger,
                    Price = 1200m,
                    Duration = TimeSpan.FromMinutes(40)
                },
                new CarWashProcedure
                {
                    Id = 5,
                    Name = "Полировка кузова",
                    CarCategory = CarCategory.Passenger,
                    Price = 3000m,
                    Duration = TimeSpan.FromMinutes(120)
                },
                new CarWashProcedure
                {
                    Id = 6,
                    Name = "Мойка внедорожника",
                    CarCategory = CarCategory.SUV,
                    Price = 2000m,
                    Duration = TimeSpan.FromMinutes(75)
                },
                new CarWashProcedure
                {
                    Id = 7,
                    Name = "Мойка микроавтобуса",
                    CarCategory = CarCategory.Minibus,
                    Price = 2500m,
                    Duration = TimeSpan.FromMinutes(90)
                },
                new CarWashProcedure
                {
                    Id = 8,
                    Name = "Мойка грузового автомобиля",
                    CarCategory = CarCategory.Truck,
                    Price = 4000m,
                    Duration = TimeSpan.FromMinutes(120)
                },
                new CarWashProcedure
                {
                    Id = 9,
                    Name = "Мойка автобуса",
                    CarCategory = CarCategory.Bus,
                    Price = 5000m,
                    Duration = TimeSpan.FromMinutes(150)
                },
                new CarWashProcedure
                {
                    Id = 10,
                    Name = "Мойка мотоцикла",
                    CarCategory = CarCategory.Motorcycle,
                    Price = 700m,
                    Duration = TimeSpan.FromMinutes(30)
                }
            ]);

        Orders.AddRange(
            [
                CreateOrder(1, 1, 1, new(2026, 9, 10, 9, 0, 0), 1),
                CreateOrder(2, 2, 2, new(2026, 9, 11, 10, 0, 0), 2),
                CreateOrder(3, 1, 1, new(2026, 9, 12, 11, 0, 0), 1),
                CreateOrder(4, 2, 3, new(2026, 9, 13, 12, 0, 0), 2),
                CreateOrder(26, 2, 3, new(2026, 9, 17, 11, 30, 0), 2),
                CreateOrder(5, 3, 1, new(2026, 9, 10, 10, 0, 0), 2),
                CreateOrder(6, 4, 2, new(2026, 9, 11, 11, 0, 0), 3),
                CreateOrder(7, 4, 1, new(2026, 9, 13, 13, 0, 0), 1),
                CreateOrder(27, 3, 1, new(2026, 9, 17, 11, 30, 0), 3),
                CreateOrder(8, 5, 10, new(2026, 9, 10, 12, 0, 0), 3),
                CreateOrder(9, 5, 10, new(2026, 9, 11, 13, 0, 0), 4),
                CreateOrder(10, 6, 10, new(2026, 9, 10, 13, 0, 0), 4),
                CreateOrder(11, 6, 10, new(2026, 9, 11, 14, 0, 0), 1),
                CreateOrder(12, 7, 1, new(2026, 9, 10, 14, 0, 0), 1),
                CreateOrder(13, 7, 2, new(2026, 9, 11, 15, 0, 0), 2),
                CreateOrder(14, 7, 1, new(2026, 9, 15, 11, 0, 0), 4),
                CreateOrder(28, 7, 2, new(2026, 9, 17, 11, 50, 0), 4),
                CreateOrder(15, 8, 8, new(2026, 9, 10, 15, 0, 0), 4),
                CreateOrder(16, 9, 2, new(2026, 9, 10, 16, 0, 0), 2),
                CreateOrder(17, 9, 3, new(2026, 9, 11, 16, 0, 0), 3),
                CreateOrder(18, 10, 3, new(2026, 9, 11, 17, 0, 0), 3),
                CreateOrder(19, 11, 4, new(2026, 9, 12, 10, 0, 0), 1),
                CreateOrder(20, 11, 5, new(2026, 9, 15, 10, 0, 0), 2),
                CreateOrder(21, 12, 5, new(2026, 9, 12, 11, 0, 0), 2),
                CreateOrder(22, 13, 1, new(2026, 9, 15, 10, 0, 0), 3),
                CreateOrder(23, 14, 8, new(2026, 9, 13, 11, 0, 0), 4),
                CreateOrder(24, 14, 8, new(2026, 9, 16, 14, 0, 0), 2),
                CreateOrder(25, 15, 2, new(2026, 9, 15, 12, 0, 0), 3),
            ]);

        foreach (var customer in Customers)
        {
            customer.Cars = Cars
                .Where(car => car.CustomerId == customer.Id)
                .ToList();

            customer.Orders = Orders
                .Where(order => order.Car.CustomerId == customer.Id)
                .ToList();
        }

        foreach (var car in Cars)
        {
            car.Orders = Orders
                .Where(order => order.CarId == car.Id)
                .ToList();
        }

        foreach (var procedure in Procedures)
        {
            procedure.Orders = Orders
                .Where(order => order.ProcedureId == procedure.Id)
                .ToList();
        }
    }

    private Order CreateOrder(
        int id,
        int carId,
        int procedureId,
        DateTime startTime,
        int washBoxNumber) =>
        new()
        {
            Id = id,
            CarId = carId,
            Car = GetCar(carId),
            ProcedureId = procedureId,
            Procedure = GetProcedure(procedureId),
            StartTime = startTime,
            WashBoxNumber = washBoxNumber
        };

    private Car GetCar(int carId)
    {
        try
        {
            return Cars.Single(car => car.Id == carId);
        }
        catch (InvalidOperationException exception)
        {
            throw new InvalidOperationException(
                $"Не удалось найти единственный автомобиль с ID = {carId}",
                exception);
        }
    }

    private CarWashCustomer GetCustomer(int customerId)
    {
        try
        {
            return Customers.Single(customer => customer.Id == customerId);
        }
        catch (InvalidOperationException exception)
        {
            throw new InvalidOperationException(
                $"Не удалось найти единственного клиента с ID = {customerId}",
                exception);
        }
    }

    private CarWashProcedure GetProcedure(int procedureId)
    {
        try
        {
            return Procedures.Single(procedure => procedure.Id == procedureId);
        }
        catch (InvalidOperationException exception)
        {
            throw new InvalidOperationException(
                $"Не удалось найти единственную услугу с ID = {procedureId}",
                exception);
        }
    }
}
