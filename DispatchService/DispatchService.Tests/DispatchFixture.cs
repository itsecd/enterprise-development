using DispatchService.Model;

namespace DispatchService.Tests;

public class DispatchFixture
{
    public TestData TestData { get; set; }

    public DispatchFixture()
    {
        TestData = new TestData
        {
            Drivers = new List<Driver>
            {
                new Driver
                {
                    Id = 1,
                    FullName = "Иванов Иван Иванович",
                    Passport = "1234 567890",
                    DriverLicense = "AB1234567",
                    Address = "Москва, ул. Ленина, д. 10",
                    PhoneNumber = "+7 (912) 345-67-89"
                },
                new Driver
                {
                    Id = 2,
                    FullName = "Петров Петр Петрович",
                    Passport = "2345 678901",
                    DriverLicense = "CD2345678",
                    Address = "Москва, ул. Пушкина, д. 15",
                    PhoneNumber = "+7 (912) 456-78-90"
                },
                new Driver
                {
                    Id = 3,
                    FullName = "Сидоров Сидор Сидорович",
                    Passport = "3456 789012",
                    DriverLicense = "EF3456789",
                    Address = "Санкт-Петербург, ул. Кирова, д. 20",
                    PhoneNumber = "+7 (912) 567-89-01"
                },
                new Driver
                {
                    Id = 4,
                    FullName = "Кузнецов Андрей Владимирович",
                    Passport = "5678 123456",
                    DriverLicense = "GH1234567",
                    Address = "Москва, ул. Советская, д. 1",
                    PhoneNumber = "+7 (903) 123-45-67"
                },
                new Driver
                {
                    Id = 5,
                    FullName = "Васильев Алексей Павлович",
                    Passport =  "6789 234567",
                    DriverLicense = "IJ2345678",
                    Address = "Санкт-Петербург, пр. Невский, д. 5",
                    PhoneNumber = "+7 (921) 789-12-34"
                },
                new Driver
                {
                    Id = 6,
                    FullName = "Смирнова Елена Ивановна",
                    Passport= "7890 345678",
                    DriverLicense = "KL3456789",
                    Address = "Новосибирск, ул. Ленина, д. 10",
                    PhoneNumber= "+7 (913) 345-67-89"
                },
                new Driver
                {
                    Id = 7,
                    FullName ="Федоров Максим Сергеевич",
                    Passport ="8901 456789",
                    DriverLicense="MN4567890",
                    Address="Екатеринбург, ул. Чапаева, д. 20",
                    PhoneNumber="+7 (922) 456-78-90"
                },
                new Driver
                {
                    Id = 8,
                    FullName="Александрова Мария Петровна",
                    Passport= "9012 567890",
                    DriverLicense= "OP5678901",
                    Address= "Казань, ул. Кремлевская, д. 5",
                    PhoneNumber= "+7 (917) 567-89-01"
                }
            },

            Transports = new List<Transport>
            {
                new Transport
                {
                    Id = 1,
                    LicensePlate ="A123BC",
                    Type = Transport.VehicleType.Bus,
                    ModelName ="Volvo 7900",
                    IsLowFloor=true,
                    MaxCapacity=100,
                    YearOfManufacture=2020
                },
                new Transport
                {
                    Id=2,
                    LicensePlate ="B456DE",
                    Type =Transport.VehicleType.Trolleybus,
                    ModelName ="Trolza",
                    IsLowFloor=false,
                    MaxCapacity=80,
                    YearOfManufacture= 2018
                },
                new Transport
                {
                    Id=3,
                    LicensePlate ="C789FG",
                    Type = Transport.VehicleType.Tram,
                    ModelName ="Stadler",
                    IsLowFloor=true,
                    MaxCapacity=150,
                    YearOfManufacture=2019
                },
                new Transport
                {
                    Id=4,
                    LicensePlate ="D123HI",
                    Type =Transport.VehicleType.Bus,
                    ModelName ="Mercedes Citaro",
                    IsLowFloor=true,
                    MaxCapacity=120,
                    YearOfManufacture=2017
                },
                new Transport
                {
                    Id=5,
                    LicensePlate ="E456JK",
                    Type =Transport.VehicleType.Trolleybus,
                    ModelName ="BKM 321",
                    IsLowFloor=false,
                    MaxCapacity=90,
                    YearOfManufacture=2016
                },
                new Transport
                {
                    Id=6,
                    LicensePlate ="F789LM",
                    Type =Transport.VehicleType.Tram,
                    ModelName ="CAF Urbos",
                    IsLowFloor=true,
                    MaxCapacity=160,
                    YearOfManufacture=2021
                },
                new Transport
                {
                    Id=7,
                    LicensePlate ="G123NO",
                    Type =Transport.VehicleType.Bus,
                    ModelName ="MAN Lion's City",
                    IsLowFloor=true,
                    MaxCapacity=110,
                    YearOfManufacture=2020
                },
                new Transport
                {
                    Id=8,
                    LicensePlate ="H456PQ",
                    Type =Transport.VehicleType.Trolleybus,
                    ModelName ="ZIU-9",
                    IsLowFloor=false,
                    MaxCapacity=70,
                    YearOfManufacture=2015
                }
            },

            Routes = new List<Route>
            {
                new Route
                {
                    RouteNumber = "1",
                    AssignedTransport = new Transport
                    {
                        Id = 1,
                        LicensePlate ="A123BC",
                        Type = Transport.VehicleType.Bus,
                        ModelName ="Volvo 7900",
                        IsLowFloor=true,
                        MaxCapacity=100,
                        YearOfManufacture=2020
                    },
                    AssignedDriver = new Driver
                    {
                        Id = 1,
                        FullName = "Иванов Иван Иванович",
                        Passport = "1234 567890",
                        DriverLicense = "AB1234567",
                        Address = "Москва, ул. Ленина, д. 10",
                        PhoneNumber = "+7 (912) 345-67-89"
                    },
                    StartTime = new DateTime(2024, 9, 14, 8, 0, 0),
                    EndTime = new DateTime(2024, 9, 14, 10, 0, 0)
                },
                new Route
                {
                    RouteNumber = "2",
                    AssignedTransport = new Transport
                    {
                        Id=2,
                        LicensePlate ="B456DE",
                        Type =Transport.VehicleType.Trolleybus,
                        ModelName ="Trolza",
                        IsLowFloor=false,
                        MaxCapacity=80,
                        YearOfManufacture= 2018
                    },
                    AssignedDriver = new Driver
                    {
                        Id = 2,
                        FullName = "Петров Петр Петрович",
                        Passport = "2345 678901",
                        DriverLicense = "CD2345678",
                        Address = "Москва, ул. Пушкина, д. 15",
                        PhoneNumber = "+7 (912) 456-78-90"
                    },
                    StartTime = new DateTime(2024, 9, 11, 9, 0, 0),
                    EndTime = new DateTime(2024, 9, 11, 11, 0, 0)
                },
                new Route
                {
                    RouteNumber = "3",
                    AssignedTransport = new Transport
                    {
                        Id=3,
                        LicensePlate ="C789FG",
                        Type = Transport.VehicleType.Tram,
                        ModelName ="Stadler",
                        IsLowFloor=true,
                        MaxCapacity=150,
                        YearOfManufacture=2019
                    },
                    AssignedDriver = new Driver
                    {
                        Id = 3,
                        FullName = "Сидоров Сидор Сидорович",
                        Passport = "3456 789012",
                        DriverLicense = "EF3456789",
                        Address = "Санкт-Петербург, ул. Кирова, д. 20",
                        PhoneNumber = "+7 (912) 567-89-01"
                    },
                    StartTime = new DateTime(2024, 9, 14, 10, 0, 0),
                    EndTime = new DateTime(2024, 9, 14, 12, 0, 0)
                },
                new Route
                {
                    RouteNumber = "4",
                    AssignedTransport = new Transport
                    {
                        Id=4,
                        LicensePlate ="D123HI",
                        Type =Transport.VehicleType.Bus,
                        ModelName ="Mercedes Citaro",
                        IsLowFloor=true,
                        MaxCapacity=120,
                        YearOfManufacture=2017
                    },
                    AssignedDriver = new Driver
                    {
                        Id = 4,
                        FullName = "Кузнецов Андрей Владимирович",
                        Passport = "5678 123456",
                        DriverLicense = "GH1234567",
                        Address = "Москва, ул. Советская, д. 1",
                        PhoneNumber = "+7 (903) 123-45-67"
                    },
                    StartTime = new DateTime(2024, 9, 13, 11, 0, 0),
                    EndTime = new DateTime(2024, 9, 13, 13, 0, 0)
                },
                new Route
                {
                    RouteNumber = "5",
                    AssignedTransport = new Transport
                    {
                        Id=5,
                        LicensePlate ="E456JK",
                        Type =Transport.VehicleType.Trolleybus,
                        ModelName ="BKM 321",
                        IsLowFloor=false,
                        MaxCapacity=90,
                        YearOfManufacture=2016
                    },
                    AssignedDriver = new Driver
                    {
                        Id = 5,
                        FullName = "Васильев Алексей Павлович",
                        Passport =  "6789 234567",
                        DriverLicense = "IJ2345678",
                        Address = "Санкт-Петербург, пр. Невский, д. 5",
                        PhoneNumber = "+7 (921) 789-12-34"
                    },
                    StartTime = new DateTime(2024, 9, 15, 8, 0, 0),
                    EndTime = new DateTime(2024, 9, 15, 12, 0, 0)
                },
                new Route
                {
                    RouteNumber = "6",
                    AssignedTransport = new Transport
                    {
                        Id=6,
                        LicensePlate ="F789LM",
                        Type =Transport.VehicleType.Tram,
                        ModelName ="CAF Urbos",
                        IsLowFloor=true,
                        MaxCapacity=160,
                        YearOfManufacture=2021
                    },
                    AssignedDriver = new Driver
                    {
                        Id = 6,
                        FullName = "Смирнова Елена Ивановна",
                        Passport= "7890 345678",
                        DriverLicense = "KL3456789",
                        Address = "Новосибирск, ул. Ленина, д. 10",
                        PhoneNumber= "+7 (913) 345-67-89"
                    },
                    StartTime = new DateTime(2024, 9, 16, 9, 0, 0),
                    EndTime = new DateTime(2024, 9, 16, 16, 0, 0)
                },
                new Route
                {
                    RouteNumber = "7",
                    AssignedTransport = new Transport
                    {
                        Id=7,
                        LicensePlate ="G123NO",
                        Type =Transport.VehicleType.Bus,
                        ModelName ="MAN Lion's City",
                        IsLowFloor=true,
                        MaxCapacity=110,
                        YearOfManufacture=2020
                    },
                    AssignedDriver = new Driver
                    {
                        Id = 7,
                        FullName ="Федоров Максим Сергеевич",
                        Passport ="8901 456789",
                        DriverLicense="MN4567890",
                        Address="Екатеринбург, ул. Чапаева, д. 20",
                        PhoneNumber="+7 (922) 456-78-90"
                    },
                    StartTime = new DateTime(2024, 9, 12, 16, 0, 0),
                    EndTime = new DateTime(2024, 9, 12, 19, 0, 0)
                },
                new Route
                {
                    RouteNumber = "8",
                    AssignedTransport = new Transport
                    {
                        Id=8,
                        LicensePlate ="H456PQ",
                        Type =Transport.VehicleType.Trolleybus,
                        ModelName ="ZIU-9",
                        IsLowFloor=false,
                        MaxCapacity=70,
                        YearOfManufacture=2015
                    },
                    AssignedDriver = new Driver
                    {
                        Id = 8,
                        FullName="Александрова Мария Петровна",
                        Passport= "9012 567890",
                        DriverLicense= "OP5678901",
                        Address= "Казань, ул. Кремлевская, д. 5",
                        PhoneNumber= "+7 (917) 567-89-01"
                    },
                    StartTime = new DateTime(2024, 9, 17, 11, 0, 0),
                    EndTime = new DateTime(2024, 9, 17, 20, 0, 0)
                },
                new Route
                {
                    RouteNumber = "9",
                    AssignedTransport = new Transport
                    {
                        Id=5,
                        LicensePlate ="E456JK",
                        Type =Transport.VehicleType.Trolleybus,
                        ModelName ="BKM 321",
                        IsLowFloor=false,
                        MaxCapacity=90,
                        YearOfManufacture=2016
                    },
                    AssignedDriver = new Driver
                    {
                        Id = 5,
                        FullName = "Васильев Алексей Павлович",
                        Passport =  "6789 234567",
                        DriverLicense = "IJ2345678",
                        Address = "Санкт-Петербург, пр. Невский, д. 5",
                        PhoneNumber = "+7 (921) 789-12-34"
                    },
                    StartTime = new DateTime(2024, 9, 15, 17, 0, 0),
                    EndTime = new DateTime(2024, 9, 15, 20, 0, 0)
                },
            }
        };
    }

}
