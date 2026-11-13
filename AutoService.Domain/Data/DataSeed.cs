using AutoService.Domain.Models;

namespace AutoService.Domain.Data;

/// <summary>
/// Тестовые данные для юнит-тестов
/// </summary>
public static class DataSeed
{
    /// <summary>
    /// Возвращаем по 10 экземпляров каждого класса
    /// </summary>
    public static List<Client> Clients { get; set; } =
    [
        new()
        {
            FullName = "Иванов Иван Иванович",
            Phone = "+71234567890"
        },
        new()
        {
            FullName = "Симонов Илья Геогриевич",
            Phone = "+71234567891"
        },
        new()
        {
            FullName = "Кораблев Гриша Александрович",
            Phone = "+71234567892"
        },
        new()
        {
            FullName = "Толстой Илья Сергеевич",
            Phone = "+71234567893"
        },
        new()
        {
            FullName = "Кольцова Александра Тимофеевна",
            Phone = "+71234567894"
        },
        new()
        {
            FullName = "Кулова Диана Сергеевна",
            Phone = "+71234567895"
        },
        new()
        {
            FullName = "Кикиморов Петр Иванович",
            Phone = "+71234567896"
        },
        new()
        {
            FullName = "Кощеев Кощей Кощеевич",
            Phone = "+71234567897"
        },
        new()
        {
            FullName = "Кефирова Яга Сергеевна",
            Phone = "+71234567898"
        },
        new()
        {
            FullName = "Андреев Андрей Андреевич",
            Phone = "+71234567899"
        }
    ];

    public static List<Car> Cars { get; set; } =
    [
        new()
        {
            LicensePlate = "Т812КМ 78",
            Brand = "Skoda",
            Model = "Octavia",
            Year = 2000,
            ClientId = Clients[0].Id
        },
        new()
        {
            LicensePlate = "Н217УВ 47",
            Brand = "Kia",
            Model = "Rio",
            Year = 2001,
            ClientId = Clients[1].Id
        },
        new()
        {
            LicensePlate = "М903АС 99",
            Brand = "Lada",
            Model = "Vesta",
            Year = 2002,
            ClientId = Clients[2].Id
        },
        new()
        {
            LicensePlate = "Е441РТ 67",
            Brand = "Hyundai",
            Model = "Creta",
            Year = 2003,
            ClientId = Clients[3].Id
        },
        new()
        {
            LicensePlate = "В562НХ 52",
            Brand = "Mazda",
            Model = "CX-5",
            Year = 2004,
            ClientId = Clients[4].Id
        },
        new()
        {
            LicensePlate = "К119МУ 178",
            Brand = "Chery",
            Model = "Tiggo 7",
            Year = 2005,
            ClientId = Clients[5].Id
        },
        new()
        {
            LicensePlate = "О774ВС 23",
            Brand = "Toyota",
            Model = "Camry",
            Year = 2006,
            ClientId = Clients[6].Id
        },
        new()
        {
            LicensePlate = "С528ТЕ 64",
            Brand = "Renault",
            Model = "Duster",
            Year = 2007,
            ClientId = Clients[7].Id
        },
        new()
        {
            LicensePlate = "У391АК 16",
            Brand = "VW",
            Model = "Polo",
            Year = 2008,
            ClientId = Clients[8].Id
        },
        new()
        {
            LicensePlate = "Р527ОМ 34",
            Brand = "Nissan",
            Model = "Qashqai",
            Year = 2026,
            ClientId = Clients[9].Id
        }
    ];

    public static List<Mechanic> Mechanics { get; set; } =
    [
        new()
        {
            Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
            Passport = "8888 123345",
            FullName = "Кириллов Кирилл Кириллович",
            Specialization = Specialization.Engine,
            Experience = 11
        },
        new()
        {
            Passport = "7777 123243",
            FullName = "Романова Ольга Владимировна",
            Specialization = Specialization.Transmission,
            Experience = 8
        },
        new()
        {
            Passport = "6666 213123",
            FullName = "Гусеев Гусь Гусевич",
            Specialization = Specialization.Electrical,
            Experience = 6
        },
        new()
        {
            Passport = "5555 324255",
            FullName = "Комаров Комар Комарович",
            Specialization = Specialization.Body,
            Experience = 14
        },
        new()
        {
            Passport = "4444 543546",
            FullName = "Мошкова Мошка Мошковна",
            Specialization = Specialization.Diagnostics,
            Experience = 5
        },
        new()
        {
            Passport = "3333 456546",
            FullName = "Тараканов Таракан Тараканович",
            Specialization = Specialization.Suspension,
            Experience = 9
        },
        new()
        {
            Passport = "2222 879789",
            FullName = "Тараканов Торт Тортович",
            Specialization = Specialization.Brakes,
            Experience = 7
        },
        new()
        {
            Passport = "1111 674667",
            FullName = "Горева Зима Владимировна",
            Specialization = Specialization.Painting,
            Experience = 12
        },
        new()
        {
            Passport = "9999 812812",
            FullName = "Веснова Весна Владимировна",
            Specialization = Specialization.Engine,
            Experience = 17
        },
        new()
        {
            Passport = "0000 524267",
            FullName = "Родионов Михаил Петрович",
            Specialization = Specialization.Diagnostics,
            Experience = 3
        }
    ];

    public static List<WorkType> Works { get; set; } =
    [
        new()
        {
            Name = "Правка вмятины крыла",
            Category = "Body",
            Price = 6500,
            Duration = TimeSpan.FromHours(4)
        },
        new()
        {
            Name = "Диагностика ходовой",
            Category = "Diagnostics",
            Price = 3200,
            Duration = TimeSpan.FromHours(1)
        },
        new()
        {
            Name = "Замена тормозных дисков",
            Category = "Brakes",
            Price = 8900,
            Duration = TimeSpan.FromHours(2)
        },
        new()
        {
            Name = "Ремонт ГБЦ",
            Category = "Engine",
            Price = 42000,
            Duration = TimeSpan.FromHours(12)
        },
        new()
        {
            Name = "Замена ремня ГРМ",
            Category = "Engine",
            Price = 18500,
            Duration = TimeSpan.FromHours(5)
        },
        new()
        {
            Name = "Компьютерная диагностика",
            Category = "Diagnostics",
            Price = 2400,
            Duration = TimeSpan.FromMinutes(50)
        },
        new()
        {
            Name = "Замена АКБ",
            Category = "Electrical",
            Price = 1500,
            Duration = TimeSpan.FromMinutes(20)
        },
        new()
        {
            Name = "Покраска бампера",
            Category = "Painting",
            Price = 16000,
            Duration = TimeSpan.FromHours(8)
        },
        new()
        {
            Name = "Замена амортизаторов",
            Category = "Suspension",
            Price = 9500,
            Duration = TimeSpan.FromHours(3)
        },
        new()
        {
            Name = "Ремонт МКПП",
            Category = "Transmission",
            Price = 35000,
            Duration = TimeSpan.FromHours(10)
        }
    ];

    public static List<Order> Orders { get; } = CreateOrders();

    private static List<Order> CreateOrders()
    {
        var now = new DateTime(2026, 10, 12, 23, 5, 10, DateTimeKind.Utc);

        return
        [
            new()
            {
                Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                CarId = Cars[0].Id,
                ClientId = Clients[0].Id,
                MechanicId = Mechanics[0].Id,
                Works =
                [
                    Works[0],
                    Works[1]
                ],
                AcceptedAt = now.AddDays(-25),
                IssuedAt = now.AddDays(-25)
            },
            new()
            {
                CarId = Cars[1].Id,
                ClientId = Clients[1].Id,
                MechanicId = Mechanics[2].Id,
                Works =
                [
                    Works[6]
                ],
                AcceptedAt = now.AddDays(-22),
                IssuedAt = now.AddDays(-22)
            },
            new()
            {
                CarId = Cars[2].Id,
                ClientId = Clients[2].Id,
                MechanicId = Mechanics[5].Id,
                Works =
                [
                    Works[2]
                ],
                AcceptedAt = now.AddDays(-18),
                IssuedAt = now.AddDays(-18)
            },
            new()
            {
                CarId = Cars[3].Id,
                ClientId = Clients[3].Id,
                MechanicId = Mechanics[4].Id,
                Works =
                [
                    Works[5]
                ],
                AcceptedAt = now.AddDays(-15),
                IssuedAt = now.AddDays(-15)
            },
            new()
            {
                CarId = Cars[4].Id,
                ClientId = Clients[4].Id,
                MechanicId = Mechanics[1].Id,
                Works =
                [
                    Works[9]
                ],
                AcceptedAt = now.AddDays(-12),
                IssuedAt = now.AddDays(-11)
            },
            new()
            {
                CarId = Cars[5].Id,
                ClientId = Clients[5].Id,
                MechanicId = Mechanics[3].Id,
                Works =
                [
                    Works[7]
                ],
                AcceptedAt = now.AddDays(-9),
                IssuedAt = now.AddDays(-8)
            },
            new()
            {
                CarId = Cars[6].Id,
                ClientId = Clients[6].Id,
                MechanicId = Mechanics[8].Id,
                Works =
                [
                    Works[3]
                ],
                AcceptedAt = now.AddDays(-7),
                IssuedAt = now.AddDays(-5)
            },
            new()
            {
                CarId = Cars[7].Id,
                ClientId = Clients[7].Id,
                MechanicId = Mechanics[6].Id,
                Works =
                [
                    Works[2],
                    Works[6]
                ],
                AcceptedAt = now.AddDays(-4),
                IssuedAt = null
            },
            new()
            {
                CarId = Cars[8].Id,
                ClientId = Clients[8].Id,
                MechanicId = Mechanics[9].Id,
                Works =
                [
                    Works[1],
                    Works[5]
                ],
                AcceptedAt = now.AddDays(-2),
                IssuedAt = null
            },
            new()
            {
                CarId = Cars[9].Id,
                ClientId = Clients[9].Id,
                MechanicId = Mechanics[0].Id,
                Works =
                [
                    Works[4]
                ],
                AcceptedAt = now.AddDays(-1),
                IssuedAt = null
            },
            new()
            {
                CarId = Cars[0].Id,
                ClientId = Clients[0].Id,
                MechanicId = Mechanics[0].Id,
                Works =
                [
                    Works[0]
                ],
                AcceptedAt = now.AddDays(-3),
                IssuedAt = null
            },
            new()
            {
                CarId = Cars[0].Id,
                ClientId = Clients[0].Id,
                MechanicId = Mechanics[4].Id,
                Works =
                [
                    Works[1]
                ],
                AcceptedAt = now.AddDays(-2),
                IssuedAt = null
            }
        ];
    }
}