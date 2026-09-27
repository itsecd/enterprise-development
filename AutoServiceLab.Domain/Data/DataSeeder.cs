using AutoServiceLab.Domain.Entities;
using AutoServiceLab.Domain.Enums;

namespace AutoServiceLab.Domain.Data;

/// <summary>
/// Создаёт тестовые данные автосервиса
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// Возвращает список клиентов автосервиса
    /// </summary>
    /// <returns>Список клиентов</returns>
    public static List<Client> GetClients()
    {
        return
        [
            new Client()
            {
                Id = 1,
                FullName = "Иванов Иван Иванович",
                Phone = "+7-927-111-11-11"
            },

            new Client()
            {
                Id = 2,
                FullName = "Петрова Мария Сергеевна",
                Phone = "+7-927-222-22-22"
            },

            new Client()
            {
                Id = 3,
                FullName = "Сидоров Алексей Петрович",
                Phone = "+7-927-333-33-33"
            },

            new Client()
            {
                Id = 4,
                FullName = "Козлова Елена Дмитриевна",
                Phone = "+7-927-444-44-44"
            },

            new Client()
            {
                Id = 5,
                FullName = "Морозов Дмитрий Андреевич",
                Phone = "+7-927-555-55-55"
            },

            new Client()
            {
                Id = 6,
                FullName = "Волкова Анна Владимировна",
                Phone = "+7-927-666-66-66"
            },

            new Client()
            {
                Id = 7,
                FullName = "Зайцев Артем Олегович",
                Phone = "+7-927-777-77-77"
            },

            new Client()
            {
                Id = 8,
                FullName = "Соколова Наталья Игоревна",
                Phone = "+7-927-888-88-88"
            },

            new Client()
            {
                Id = 9,
                FullName = "Кузнецов Максим Романович",
                Phone = "+7-927-999-99-99"
            },

            new Client()
            {
                Id = 10,
                FullName = "Смирнова Ольга Павловна",
                Phone = "+7-927-000-00-00"
            },
        ];
    }

    /// <summary>
    /// Возвращает список автомобилей клиентов
    /// </summary>
    /// <param name="clients">Список клиентов</param>
    /// <returns>Список автомобилей</returns>
    public static List<Car> GetCars(List<Client> clients)
    {
        var cars = new List<Car>
        {
            new()
            {
                Id = 1,
                LicensePlate = "Х011ХХ163",
                Brand = "Lada",
                Model = "Granta",
                Year = 2020,
                ClientId = clients[0].Id,
                Client = clients[0]
            },

            new()
            {
                Id = 2,
                LicensePlate = "А001АА163",
                Brand = "Toyota",
                Model = "Camry",
                Year = 2020,
                ClientId = clients[1].Id,
                Client = clients[1]
            },

            new()
            {
                Id = 3,
                LicensePlate = "В002ВВ163",
                Brand = "BMW",
                Model = "X5",
                Year = 2022,
                ClientId = clients[1].Id,
                Client = clients[1]
            },

            new()
            {
                Id = 4,
                LicensePlate = "С003СС163",
                Brand = "Mercedes",
                Model = "E-Class",
                Year = 2021,
                ClientId = clients[2].Id,
                Client = clients[2]
            },

            new()
            {
                Id = 5,
                LicensePlate = "Е004ЕЕ163",
                Brand = "Audi",
                Model = "Q7",
                Year = 2020,
                ClientId = clients[3].Id,
                Client = clients[3]
            },

            new()
            {
                Id = 6,
                LicensePlate = "К005КК163",
                Brand = "Volkswagen",
                Model = "Passat",
                Year = 2019,
                ClientId = clients[4].Id,
                Client = clients[4]
            },

            new()
            {
                Id = 7,
                LicensePlate = "М006ММ163",
                Brand = "Ford",
                Model = "Focus",
                Year = 2021,
                ClientId = clients[5].Id,
                Client = clients[5]
            },

            new()
            {
                Id = 8,
                LicensePlate = "Н007НН163",
                Brand = "Toyota",
                Model = "Land Cruiser",
                Year = 2022,
                ClientId = clients[6].Id,
                Client = clients[6]
            },

            new()
            {
                Id = 9,
                LicensePlate = "О008ОО163",
                Brand = "Hyundai",
                Model = "Sonata",
                Year = 2020,
                ClientId = clients[7].Id,
                Client = clients[7]
            },

            new()
            {
                Id = 10,
                LicensePlate = "Р009РР163",
                Brand = "Nissan",
                Model = "X-Trail",
                Year = 2021,
                ClientId = clients[8].Id,
                Client = clients[8]
            },

            new()
            {
                Id = 11,
                LicensePlate = "Т010ТТ163",
                Brand = "BMW",
                Model = "3 Series",
                Year = 2020,
                ClientId = clients[9].Id,
                Client = clients[9]
            },

        };

        cars.ForEach(car => clients.FirstOrDefault(client => client.Id == car.ClientId)?.Cars.Add(car));

        return cars;
    }

    /// <summary>
    /// Возвращает список механиков автосервиса
    /// </summary>
    /// <returns>Список механиков</returns>
    public static List<Mechanic> GetMechanics()
    {
        return
        [
            new Mechanic()
            {
                Id = 1,
                PassportNumber = "1111 111111",
                FullName = "Сидоров Петр Иванович",
                Phone = "+7-937-111-11-11",
                Specialization = MechanicSpecialization.Engine,
                ExperienceYears = 4
            },

            new Mechanic()
            {
                Id = 2,
                PassportNumber = "2222 222222",
                FullName = "Иванов Александр Петрович",
                Phone = "+7-937-111-11-12",
                Specialization = MechanicSpecialization.Electrical,
                ExperienceYears = 6
            },

            new Mechanic()
            {
                Id = 3,
                PassportNumber = "3333 333333",
                FullName = "Петрова Екатерина Сергеевна",
                Phone = "+7-937-111-11-13",
                Specialization = MechanicSpecialization.Body,
                ExperienceYears = 8
            },

            new Mechanic()
            {
                Id = 4,
                PassportNumber = "4444 444444",
                FullName = "Смирнов Денис Андреевич",
                Phone = "+7-937-111-11-14",
                Specialization = MechanicSpecialization.Transmission,
                ExperienceYears = 5
            },

            new Mechanic()
            {
                Id = 5,
                PassportNumber = "5555 555555",
                FullName = "Козлова Ольга Дмитриевна",
                Phone = "+7-937-111-11-15",
                Specialization = MechanicSpecialization.Engine,
                ExperienceYears = 7
            },

            new Mechanic()
            {
                Id = 6,
                PassportNumber = "6666 666666",
                FullName = "Морозов Владимир Игоревич",
                Phone = "+7-937-111-11-16",
                Specialization = MechanicSpecialization.Electrical,
                ExperienceYears = 10
            },

            new Mechanic()
            {
                Id = 7,
                PassportNumber = "7777 777777",
                FullName = "Новикова Мария Олеговна",
                Phone = "+7-937-111-11-17",
                Specialization = MechanicSpecialization.Body,
                ExperienceYears = 3
            },

            new Mechanic()
            {
                Id = 8,
                PassportNumber = "8888 888888",
                FullName = "Федоров Алексей Романович",
                Phone = "+7-937-111-11-18",
                Specialization = MechanicSpecialization.Transmission,
                ExperienceYears = 9
            },

            new Mechanic()
            {
                Id = 9,
                PassportNumber = "9999 999999",
                FullName = "Егоров Михаил Валерьевич",
                Phone = "+7-937-111-11-19",
                Specialization = MechanicSpecialization.Suspension,
                ExperienceYears = 12
            },

            new Mechanic()
            {
                Id = 10,
                PassportNumber = "1010 101010",
                FullName = "Тимофеев Илья Андреевич",
                Phone = "+7-937-111-11-10",
                Specialization = MechanicSpecialization.Brakes,
                ExperienceYears = 2
            },
        ];
    }

    /// <summary>
    /// Возвращает список видов работ
    /// </summary>
    /// <returns>Список видов работ</returns>
    public static List<WorkType> GetWorkTypes()
    {
        return
        [
            new WorkType()
            {
                Id = 1,
                Name = "Замена масла",
                Category = WorkCategory.Maintenance,
                Duration = 1.0m,
                LaborCost = 3750
            },

            new WorkType()
            {
                Id = 2,
                Name = "Диагностика двигателя",
                Category = WorkCategory.Diagnostics,
                Duration = 1.5m,
                LaborCost = 2500
            },

            new WorkType()
            {
                Id = 3,
                Name = "Ремонт двигателя",
                Category = WorkCategory.Engine,
                Duration = 8.0m,
                LaborCost = 20000
            },

            new WorkType()
            {
                Id = 4,
                Name = "Замена тормозных колодок",
                Category = WorkCategory.Brakes,
                Duration = 2.0m,
                LaborCost = 5000
            },

            new WorkType()
            {
                Id = 5,
                Name = "Замена шин",
                Category = WorkCategory.Suspension,
                Duration = 1.0m,
                LaborCost = 2400
            },

            new WorkType()
            {
                Id = 6,
                Name = "Ремонт проводки",
                Category = WorkCategory.Electrical,
                Duration = 3.0m,
                LaborCost = 6600
            },

            new WorkType()
            {
                Id = 7,
                Name = "Ремонт АКПП",
                Category = WorkCategory.Transmission,
                Duration = 10.0m,
                LaborCost = 24000
            },

            new WorkType()
            {
                Id = 8,
                Name = "Кузовной ремонт",
                Category = WorkCategory.Body,
                Duration = 6.0m,
                LaborCost = 15000
            },

            new WorkType()
            {
                Id = 9,
                Name = "Замена ремня ГРМ",
                Category = WorkCategory.Engine,
                Duration = 4.0m,
                LaborCost = 8000
            },

            new WorkType()
            {
                Id = 10,
                Name = "Замена амортизаторов",
                Category = WorkCategory.Suspension,
                Duration = 3.0m,
                LaborCost = 6000
            },
        ];
    }

    /// <summary>
    /// Возвращает список заказов на обслуживание
    /// </summary>
    /// <param name="clients">Список клиентов</param>
    /// <param name="cars">Список автомобилей</param>
    /// <returns>Список заказов</returns>
    public static List<ServiceOrder> GetServiceOrders(List<Client> clients, List<Car> cars)
    {
        var orders = new List<ServiceOrder>
        {
            new()
            {
                Id = 1,
                ReceptionDate = new DateTime(2026, 9, 1),
                IssueDate = new DateTime(2026, 9, 3),
                ClientId = clients[0].Id,
                Client = clients[0],
                CarId = cars[0].Id,
                Car = cars[0]
            },

            new()
            {
                Id = 2,
                ReceptionDate = new DateTime(2026, 9, 5),
                IssueDate = new DateTime(2026, 9, 7),
                ClientId = clients[1].Id,
                Client = clients[1],
                CarId = cars[1].Id,
                Car = cars[1]
            },

            new()
            {
                Id = 3,
                ReceptionDate = new DateTime(2026, 9, 8),
                IssueDate = null,
                ClientId = clients[1].Id,
                Client = clients[1],
                CarId = cars[2].Id,
                Car = cars[2]
            },

            new()
            {
                Id = 4,
                ReceptionDate = new DateTime(2026, 9, 10),
                IssueDate = new DateTime(2026, 9, 12),
                ClientId = clients[2].Id,
                Client = clients[2],
                CarId = cars[3].Id,
                Car = cars[3]
            },

            new()
            {
                Id = 5,
                ReceptionDate = new DateTime(2026, 9, 12),
                IssueDate = new DateTime(2026, 9, 15),
                ClientId = clients[3].Id,
                Client = clients[3],
                CarId = cars[4].Id,
                Car = cars[4]
            },

            new()
            {
                Id = 6,
                ReceptionDate = new DateTime(2026, 9, 15),
                IssueDate = null,
                ClientId = clients[3].Id,
                Client = clients[3],
                CarId = cars[4].Id,
                Car = cars[4]
            },

            new()
            {
                Id = 7,
                ReceptionDate = new DateTime(2026, 9, 18),
                IssueDate = new DateTime(2026, 9, 20),
                ClientId = clients[4].Id,
                Client = clients[4],
                CarId = cars[5].Id,
                Car = cars[5]
            },

            new()
            {
                Id = 8,
                ReceptionDate = new DateTime(2026, 9, 20),
                IssueDate = new DateTime(2026, 9, 23),
                ClientId = clients[5].Id,
                Client = clients[5],
                CarId = cars[6].Id,
                Car = cars[6]
            },

            new()
            {
                Id = 9,
                ReceptionDate = new DateTime(2026, 9, 21),
                IssueDate = new DateTime(2026, 9, 22),
                ClientId = clients[6].Id,
                Client = clients[6],
                CarId = cars[7].Id,
                Car = cars[7]
            },

            new()
            {
                Id = 10,
                ReceptionDate = new DateTime(2026, 9, 22),
                IssueDate = null,
                ClientId = clients[7].Id,
                Client = clients[7],
                CarId = cars[8].Id,
                Car = cars[8]
            },

            new()
            {
                Id = 11,
                ReceptionDate = new DateTime(2026, 9, 25),
                IssueDate = new DateTime(2026, 9, 27),
                ClientId = clients[8].Id,
                Client = clients[8],
                CarId = cars[9].Id,
                Car = cars[9]
            },

            new()
            {
                Id = 12,
                ReceptionDate = new DateTime(2026, 9, 26),
                IssueDate = null,
                ClientId = clients[9].Id,
                Client = clients[9],
                CarId = cars[10].Id,
                Car = cars[10]
            },

        };

        orders.ForEach(order => clients.FirstOrDefault(client => client.Id == order.ClientId)?.Orders.Add(order));
        orders.ForEach(order => cars.FirstOrDefault(car => car.Id == order.CarId)?.Orders.Add(order));

        return orders;
    }

    /// <summary>
    /// Возвращает список связей заказов и видов работ
    /// </summary>
    /// <param name="orders">Список заказов</param>
    /// <param name="workTypes">Список видов работ</param>
    /// <returns>Список связей заказов и видов работ</returns>
    public static List<OrderWork> GetOrderWorks(List<ServiceOrder> orders, List<WorkType> workTypes)
    {
        var orderWorks = new List<OrderWork>
        {
            new()
            {
                Id = 1,
                ServiceOrderId = orders[0].Id,
                ServiceOrder = orders[0],
                WorkTypeId = workTypes[0].Id,
                WorkType = workTypes[0]
            },

            new()
            {
                Id = 2,
                ServiceOrderId = orders[0].Id,
                ServiceOrder = orders[0],
                WorkTypeId = workTypes[1].Id,
                WorkType = workTypes[1]
            },

            new()
            {
                Id = 3,
                ServiceOrderId = orders[1].Id,
                ServiceOrder = orders[1],
                WorkTypeId = workTypes[2].Id,
                WorkType = workTypes[2]
            },

            new()
            {
                Id = 4,
                ServiceOrderId = orders[2].Id,
                ServiceOrder = orders[2],
                WorkTypeId = workTypes[3].Id,
                WorkType = workTypes[3]
            },

            new()
            {
                Id = 5,
                ServiceOrderId = orders[3].Id,
                ServiceOrder = orders[3],
                WorkTypeId = workTypes[4].Id,
                WorkType = workTypes[4]
            },

            new()
            {
                Id = 6,
                ServiceOrderId = orders[4].Id,
                ServiceOrder = orders[4],
                WorkTypeId = workTypes[5].Id,
                WorkType = workTypes[5]
            },

            new()
            {
                Id = 7,
                ServiceOrderId = orders[5].Id,
                ServiceOrder = orders[5],
                WorkTypeId = workTypes[6].Id,
                WorkType = workTypes[6]
            },

            new()
            {
                Id = 8,
                ServiceOrderId = orders[6].Id,
                ServiceOrder = orders[6],
                WorkTypeId = workTypes[7].Id,
                WorkType = workTypes[7]
            },

            new()
            {
                Id = 9,
                ServiceOrderId = orders[7].Id,
                ServiceOrder = orders[7],
                WorkTypeId = workTypes[8].Id,
                WorkType = workTypes[8]
            },

            new()
            {
                Id = 10,
                ServiceOrderId = orders[8].Id,
                ServiceOrder = orders[8],
                WorkTypeId = workTypes[9].Id,
                WorkType = workTypes[9]
            },

            new()
            {
                Id = 11,
                ServiceOrderId = orders[9].Id,
                ServiceOrder = orders[9],
                WorkTypeId = workTypes[0].Id,
                WorkType = workTypes[0]
            },

            new()
            {
                Id = 12,
                ServiceOrderId = orders[10].Id,
                ServiceOrder = orders[10],
                WorkTypeId = workTypes[2].Id,
                WorkType = workTypes[2]
            },
        };

        orderWorks.ForEach(ow => ow.ServiceOrder.OrderWorks.Add(ow));
        orderWorks.ForEach(ow => ow.WorkType.OrderWorks.Add(ow));

        return orderWorks;
    }

    /// <summary>
    /// Возвращает список связей заказов и механиков
    /// </summary>
    /// <param name="orders">Список заказов</param>
    /// <param name="mechanics">Список механиков</param>
    /// <returns>Список связей заказов и механиков</returns>
    public static List<OrderMechanic> GetOrderMechanics(List<ServiceOrder> orders, List<Mechanic> mechanics)
    {
        var orderMechanics = new List<OrderMechanic>
        {
            new()
            {
                Id = 1,
                ServiceOrderId = orders[0].Id,
                ServiceOrder = orders[0],
                MechanicId = mechanics[0].Id,
                Mechanic = mechanics[0]
            },

            new()
            {
                Id = 2,
                ServiceOrderId = orders[1].Id,
                ServiceOrder = orders[1],
                MechanicId = mechanics[4].Id,
                Mechanic = mechanics[4]
            },

            new()
            {
                Id = 3,
                ServiceOrderId = orders[2].Id,
                ServiceOrder = orders[2],
                MechanicId = mechanics[9].Id,
                Mechanic = mechanics[9]
            },

            new()
            {
                Id = 4,
                ServiceOrderId = orders[3].Id,
                ServiceOrder = orders[3],
                MechanicId = mechanics[8].Id,
                Mechanic = mechanics[8]
            },

            new()
            {
                Id = 5,
                ServiceOrderId = orders[4].Id,
                ServiceOrder = orders[4],
                MechanicId = mechanics[1].Id,
                Mechanic = mechanics[1]
            },

            new()
            {
                Id = 6,
                ServiceOrderId = orders[5].Id,
                ServiceOrder = orders[5],
                MechanicId = mechanics[3].Id,
                Mechanic = mechanics[3]
            },

            new()
            {
                Id = 7,
                ServiceOrderId = orders[6].Id,
                ServiceOrder = orders[6],
                MechanicId = mechanics[2].Id,
                Mechanic = mechanics[2]
            },

            new()
            {
                Id = 8,
                ServiceOrderId = orders[7].Id,
                ServiceOrder = orders[7],
                MechanicId = mechanics[0].Id,
                Mechanic = mechanics[0]
            },

            new()
            {
                Id = 9,
                ServiceOrderId = orders[8].Id,
                ServiceOrder = orders[8],
                MechanicId = mechanics[8].Id,
                Mechanic = mechanics[8]
            },

            new()
            {
                Id = 10,
                ServiceOrderId = orders[9].Id,
                ServiceOrder = orders[9],
                MechanicId = mechanics[0].Id,
                Mechanic = mechanics[0]
            },

            new()
            {
                Id = 11,
                ServiceOrderId = orders[10].Id,
                ServiceOrder = orders[10],
                MechanicId = mechanics[4].Id,
                Mechanic = mechanics[4]
            },

            new()
            {
                Id = 12,
                ServiceOrderId = orders[11].Id,
                ServiceOrder = orders[11],
                MechanicId = mechanics[4].Id,
                Mechanic = mechanics[4]
            },
        };

        orderMechanics.ForEach(om => om.ServiceOrder.OrderMechanics.Add(om));
        orderMechanics.ForEach(om => om.Mechanic.OrderMechanics.Add(om));

        return orderMechanics;
    }
}
