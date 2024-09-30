using DispatchService.Model;

namespace DispatchService.Tests;

public class DispatchFixture
{
    public TestData TestData { get; set; }

    public DispatchFixture()
    {
        var drivers = new List<Driver>
        {
            new ()
            {
                Id = 1,
                FullName = "Иванов Иван Иванович",
                Passport = "1234 567890",
                DriverLicense = "AB1234567",
                Address = "Москва, ул. Ленина, д. 10",
                PhoneNumber = "+7 (912) 345-67-89"
            },
            new ()
            {
                Id = 2,
                FullName = "Петров Петр Петрович",
                Passport = "2345 678901",
                DriverLicense = "CD2345678",
                Address = "Москва, ул. Пушкина, д. 15",
                PhoneNumber = "+7 (912) 456-78-90"
            },
            new ()
            {
                Id = 3,
                FullName = "Сидоров Сидор Сидорович",
                Passport = "3456 789012",
                DriverLicense = "EF3456789",
                Address = "Санкт-Петербург, ул. Кирова, д. 20",
                PhoneNumber = "+7 (912) 567-89-01"
            },
            new ()
            {
                Id = 4,
                FullName = "Кузнецов Андрей Владимирович",
                Passport = "5678 123456",
                DriverLicense = "GH1234567",
                Address = "Москва, ул. Советская, д. 1",
                PhoneNumber = "+7 (903) 123-45-67"
            },
            new ()
            {
                Id = 5,
                FullName = "Васильев Алексей Павлович",
                Passport =  "6789 234567",
                DriverLicense = "IJ2345678",
                Address = "Санкт-Петербург, пр. Невский, д. 5",
                PhoneNumber = "+7 (921) 789-12-34"
            },
            new ()
            {
                Id = 6,
                FullName = "Смирнова Елена Ивановна",
                Passport = "7890 345678",
                DriverLicense = "KL3456789",
                Address = "Новосибирск, ул. Ленина, д. 10",
                PhoneNumber = "+7 (913) 345-67-89"
            },
            new ()
            {
                Id = 7,
                FullName = "Федоров Максим Сергеевич",
                Passport = "8901 456789",
                DriverLicense = "MN4567890",
                Address = "Екатеринбург, ул. Чапаева, д. 20",
                PhoneNumber = "+7 (922) 456-78-90"
            },
            new ()
            {
                Id = 8,
                FullName = "Александрова Мария Петровна",
                Passport = "9012 567890",
                DriverLicense = "OP5678901",
                Address = "Казань, ул. Кремлевская, д. 5",
                PhoneNumber = "+7 (917) 567-89-01"
            }
        };

        var transports = new List<Transport>
        {
            new ()
            {
                Id = 1,
                LicensePlate = "A123BC",
                Type = Transport.VehicleType.Bus,
                ModelName = "Volvo 7900",
                IsLowFloor = true,
                MaxCapacity = 100,
                YearOfManufacture = 2020
            },
            new ()
            {
                Id = 2,
                LicensePlate = "B456DE",
                Type = Transport.VehicleType.Trolleybus,
                ModelName = "Trolza",
                IsLowFloor = false,
                MaxCapacity = 80,
                YearOfManufacture = 2018
            },
            new ()
            {
                Id = 3,
                LicensePlate = "C789FG",
                Type = Transport.VehicleType.Tram,
                ModelName = "Stadler",
                IsLowFloor = true,
                MaxCapacity = 150,
                YearOfManufacture = 2019
            },
            new ()
            {
                Id = 4,
                LicensePlate = "D123HI",
                Type = Transport.VehicleType.Bus,
                ModelName = "Mercedes Citaro",
                IsLowFloor = true,
                MaxCapacity = 120,
                YearOfManufacture = 2017
            },
            new ()
            {
                Id = 5,
                LicensePlate = "E456JK",
                Type = Transport.VehicleType.Trolleybus,
                ModelName = "BKM 321",
                IsLowFloor = false,
                MaxCapacity = 90,
                YearOfManufacture = 2016
            },
            new ()
            {
                Id = 6,
                LicensePlate = "F789LM",
                Type = Transport.VehicleType.Tram,
                ModelName = "CAF Urbos",
                IsLowFloor = true,
                MaxCapacity = 160,
                YearOfManufacture = 2021
            },
            new ()
            {
                Id = 7,
                LicensePlate = "G123NO",
                Type = Transport.VehicleType.Bus,
                ModelName = "MAN Lion's City",
                IsLowFloor = true,
                MaxCapacity = 110,
                YearOfManufacture = 2020
            },
            new ()
            {
                Id = 8,
                LicensePlate = "H456PQ",
                Type = Transport.VehicleType.Trolleybus,
                ModelName = "ZIU-9",
                IsLowFloor = false,
                MaxCapacity = 70,
                YearOfManufacture = 2015
            }
        };

        var routes = new List<Route>
        {
            new ()
            {
                Id = 1,
                RouteNumber = "1",
                AssignedTransport = transports[0],
                AssignedDriver = drivers[0],
                StartTime = new DateTime(2024, 9, 14, 8, 0, 0),
                EndTime = new DateTime(2024, 9, 14, 10, 0, 0)
            },
            new ()
            {
                Id = 2,
                RouteNumber = "2",
                AssignedTransport = transports[1],
                AssignedDriver = drivers[1],
                StartTime = new DateTime(2024, 9, 11, 9, 0, 0),
                EndTime = new DateTime(2024, 9, 11, 11, 0, 0)
            },
            new ()
            {
                Id = 3,
                RouteNumber = "3",
                AssignedTransport = transports[2],
                AssignedDriver = drivers[2],
                StartTime = new DateTime(2024, 9, 14, 10, 0, 0),
                EndTime = new DateTime(2024, 9, 14, 12, 0, 0)
            },
            new ()
            {
                Id = 4,
                RouteNumber = "4",
                AssignedTransport = transports[3],
                AssignedDriver = drivers[3],
                StartTime = new DateTime(2024, 9, 13, 11, 0, 0),
                EndTime = new DateTime(2024, 9, 13, 13, 0, 0)
            },
            new ()
            {
                Id = 5,
                RouteNumber = "5",
                AssignedTransport = transports[4],
                AssignedDriver = drivers[4],
                StartTime = new DateTime(2024, 9, 15, 8, 0, 0),
                EndTime = new DateTime(2024, 9, 15, 12, 0, 0)
            },
            new ()
            {
                Id = 6,
                RouteNumber = "6",
                AssignedTransport = transports[5],
                AssignedDriver = drivers[5],
                StartTime = new DateTime(2024, 9, 16, 9, 0, 0),
                EndTime = new DateTime(2024, 9, 16, 16, 0, 0)
            },
            new ()
            {
                Id = 7,
                RouteNumber = "7",
                AssignedTransport = transports[6],
                AssignedDriver = drivers[6],
                StartTime = new DateTime(2024, 9, 12, 16, 0, 0),
                EndTime = new DateTime(2024, 9, 12, 19, 0, 0)
            },
            new ()
            {
                Id = 8,
                RouteNumber = "8",
                AssignedTransport = transports[7],
                AssignedDriver = drivers[7],
                StartTime = new DateTime(2024, 9, 17, 11, 0, 0),
                EndTime = new DateTime(2024, 9, 17, 20, 0, 0)
            },
            new ()
            {
                Id = 9,
                RouteNumber = "9",
                AssignedTransport = transports[4],
                AssignedDriver = drivers[4],
                StartTime = new DateTime(2024, 9, 15, 17, 0, 0),
                EndTime = new DateTime(2024, 9, 15, 20, 0, 0)
            },
        };

        TestData = new TestData
        {
            Drivers = drivers,
            Transports = transports,
            Routes = routes,
        };
    }

}
