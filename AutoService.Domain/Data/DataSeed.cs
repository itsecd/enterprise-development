using AutoService.Domain.Models;

namespace AutoService.Domain.Data;

/// <summary>
/// Тестовые данные для юнит-тестов и начального заполнения базы данных
/// </summary>
public static class DataSeed
{
    /// <summary>
    /// Детерминированные идентификаторы, чтобы начальное заполнение базы данных было воспроизводимым
    /// </summary>
    private static Guid FixedId(int n) => new(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, (byte)n);

    /// <summary>
    /// Возвращаем по 10 экземпляров каждого класса
    /// </summary>
    public static List<Client> Clients { get; } =
    [
        new()
        {
            Id = FixedId(1),
            FullName = "Иванов Иван Иванович",
            Phone = "+71234567890"
        },
        new()
        {
            Id = FixedId(2),
            FullName = "Симонов Илья Геогриевич",
            Phone = "+71234567891"
        },
        new()
        {
            Id = FixedId(3),
            FullName = "Кораблев Гриша Александрович",
            Phone = "+71234567892"
        },
        new()
        {
            Id = FixedId(4),
            FullName = "Толстой Илья Сергеевич",
            Phone = "+71234567893"
        },
        new()
        {
            Id = FixedId(5),
            FullName = "Кольцова Александра Тимофеевна",
            Phone = "+71234567894"
        },
        new()
        {
            Id = FixedId(6),
            FullName = "Кулова Диана Сергеевна",
            Phone = "+71234567895"
        },
        new()
        {
            Id = FixedId(7),
            FullName = "Кикиморов Петр Иванович",
            Phone = "+71234567896"
        },
        new()
        {
            Id = FixedId(8),
            FullName = "Кощеев Кощей Кощеевич",
            Phone = "+71234567897"
        },
        new()
        {
            Id = FixedId(9),
            FullName = "Кефирова Яга Сергеевна",
            Phone = "+71234567898"
        },
        new()
        {
            Id = FixedId(10),
            FullName = "Андреев Андрей Андреевич",
            Phone = "+71234567899"
        }
    ];

    public static List<Car> Cars { get; } =
    [
        new()
        {
            Id = FixedId(11),
            LicensePlate = "Т812КМ 78",
            Brand = "Skoda",
            Model = "Octavia",
            Year = 2000,
            ClientId = Clients[0].Id,
            Client = Clients[0]
        },
        new()
        {
            Id = FixedId(12),
            LicensePlate = "Н217УВ 47",
            Brand = "Kia",
            Model = "Rio",
            Year = 2001,
            ClientId = Clients[1].Id,
            Client = Clients[1]
        },
        new()
        {
            Id = FixedId(13),
            LicensePlate = "М903АС 99",
            Brand = "Lada",
            Model = "Vesta",
            Year = 2002,
            ClientId = Clients[2].Id,
            Client = Clients[2]
        },
        new()
        {
            Id = FixedId(14),
            LicensePlate = "Е441РТ 67",
            Brand = "Hyundai",
            Model = "Creta",
            Year = 2003,
            ClientId = Clients[3].Id,
            Client = Clients[3]
        },
        new()
        {
            Id = FixedId(15),
            LicensePlate = "В562НХ 52",
            Brand = "Mazda",
            Model = "CX-5",
            Year = 2004,
            ClientId = Clients[4].Id,
            Client = Clients[4]
        },
        new()
        {
            Id = FixedId(16),
            LicensePlate = "К119МУ 178",
            Brand = "Chery",
            Model = "Tiggo 7",
            Year = 2005,
            ClientId = Clients[5].Id,
            Client = Clients[5]
        },
        new()
        {
            Id = FixedId(17),
            LicensePlate = "О774ВС 23",
            Brand = "Toyota",
            Model = "Camry",
            Year = 2006,
            ClientId = Clients[6].Id,
            Client = Clients[6]
        },
        new()
        {
            Id = FixedId(18),
            LicensePlate = "С528ТЕ 64",
            Brand = "Renault",
            Model = "Duster",
            Year = 2007,
            ClientId = Clients[7].Id,
            Client = Clients[7]
        },
        new()
        {
            Id = FixedId(19),
            LicensePlate = "У391АК 16",
            Brand = "VW",
            Model = "Polo",
            Year = 2008,
            ClientId = Clients[8].Id,
            Client = Clients[8]
        },
        new()
        {
            Id = FixedId(20),
            LicensePlate = "Р527ОМ 34",
            Brand = "Nissan",
            Model = "Qashqai",
            Year = 2026,
            ClientId = Clients[9].Id,
            Client = Clients[9]
        }
    ];

    public static List<Mechanic> Mechanics { get; } =
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
            Id = FixedId(22),
            Passport = "7777 123243",
            FullName = "Романова Ольга Владимировна",
            Specialization = Specialization.Transmission,
            Experience = 8
        },
        new()
        {
            Id = FixedId(23),
            Passport = "6666 213123",
            FullName = "Гусеев Гусь Гусевич",
            Specialization = Specialization.Electrical,
            Experience = 6
        },
        new()
        {
            Id = FixedId(24),
            Passport = "5555 324255",
            FullName = "Комаров Комар Комарович",
            Specialization = Specialization.Body,
            Experience = 14
        },
        new()
        {
            Id = FixedId(25),
            Passport = "4444 543546",
            FullName = "Мошкова Мошка Мошковна",
            Specialization = Specialization.Diagnostics,
            Experience = 5
        },
        new()
        {
            Id = FixedId(26),
            Passport = "3333 456546",
            FullName = "Тараканов Таракан Тараканович",
            Specialization = Specialization.Suspension,
            Experience = 9
        },
        new()
        {
            Id = FixedId(27),
            Passport = "2222 879789",
            FullName = "Тараканов Торт Тортович",
            Specialization = Specialization.Brakes,
            Experience = 7
        },
        new()
        {
            Id = FixedId(28),
            Passport = "1111 674667",
            FullName = "Горева Зима Владимировна",
            Specialization = Specialization.Painting,
            Experience = 12
        },
        new()
        {
            Id = FixedId(29),
            Passport = "9999 812812",
            FullName = "Веснова Весна Владимировна",
            Specialization = Specialization.Engine,
            Experience = 17
        },
        new()
        {
            Id = FixedId(30),
            Passport = "0000 524267",
            FullName = "Родионов Михаил Петрович",
            Specialization = Specialization.Diagnostics,
            Experience = 3
        }
    ];

    public static List<WorkType> Works { get; } =
    [
        new()
        {
            Id = FixedId(41),
            Name = "Правка вмятины крыла",
            Category = "Body",
            Price = 6500,
            Duration = TimeSpan.FromHours(4)
        },
        new()
        {
            Id = FixedId(42),
            Name = "Диагностика ходовой",
            Category = "Diagnostics",
            Price = 3200,
            Duration = TimeSpan.FromHours(1)
        },
        new()
        {
            Id = FixedId(43),
            Name = "Замена тормозных дисков",
            Category = "Brakes",
            Price = 8900,
            Duration = TimeSpan.FromHours(2)
        },
        new()
        {
            Id = FixedId(44),
            Name = "Ремонт ГБЦ",
            Category = "Engine",
            Price = 42000,
            Duration = TimeSpan.FromHours(12)
        },
        new()
        {
            Id = FixedId(45),
            Name = "Замена ремня ГРМ",
            Category = "Engine",
            Price = 18500,
            Duration = TimeSpan.FromHours(5)
        },
        new()
        {
            Id = FixedId(46),
            Name = "Компьютерная диагностика",
            Category = "Diagnostics",
            Price = 2400,
            Duration = TimeSpan.FromMinutes(50)
        },
        new()
        {
            Id = FixedId(47),
            Name = "Замена АКБ",
            Category = "Electrical",
            Price = 1500,
            Duration = TimeSpan.FromMinutes(20)
        },
        new()
        {
            Id = FixedId(48),
            Name = "Покраска бампера",
            Category = "Painting",
            Price = 16000,
            Duration = TimeSpan.FromHours(8)
        },
        new()
        {
            Id = FixedId(49),
            Name = "Замена амортизаторов",
            Category = "Suspension",
            Price = 9500,
            Duration = TimeSpan.FromHours(3)
        },
        new()
        {
            Id = FixedId(50),
            Name = "Ремонт МКПП",
            Category = "Transmission",
            Price = 35000,
            Duration = TimeSpan.FromHours(10)
        }
    ];

    /// <summary>
    /// Начальный пользователь для входа в API
    /// </summary>
    public static List<User> Users { get; } =
    [
        new()
        {
            Id = FixedId(81),
            Username = "admin",
            PasswordHash = "AQAAAAIAAYagAAAAEAABAgMEBQYHCAkKCwwNDg97z2IZyhqC3YaJEn8frGtw7fMb52Ky5VJvyBDCaUXU9w=="
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
                Car = Cars[0],
                ClientId = Clients[0].Id,
                Client = Clients[0],
                MechanicId = Mechanics[0].Id,
                Mechanic = Mechanics[0],
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
                Id = FixedId(62),
                CarId = Cars[1].Id,
                Car = Cars[1],
                ClientId = Clients[1].Id,
                Client = Clients[1],
                MechanicId = Mechanics[2].Id,
                Mechanic = Mechanics[2],
                Works =
                [
                    Works[6]
                ],
                AcceptedAt = now.AddDays(-22),
                IssuedAt = now.AddDays(-22)
            },
            new()
            {
                Id = FixedId(63),
                CarId = Cars[2].Id,
                Car = Cars[2],
                ClientId = Clients[2].Id,
                Client = Clients[2],
                MechanicId = Mechanics[5].Id,
                Mechanic = Mechanics[5],
                Works =
                [
                    Works[2]
                ],
                AcceptedAt = now.AddDays(-18),
                IssuedAt = now.AddDays(-18)
            },
            new()
            {
                Id = FixedId(64),
                CarId = Cars[3].Id,
                Car = Cars[3],
                ClientId = Clients[3].Id,
                Client = Clients[3],
                MechanicId = Mechanics[4].Id,
                Mechanic = Mechanics[4],
                Works =
                [
                    Works[5]
                ],
                AcceptedAt = now.AddDays(-15),
                IssuedAt = now.AddDays(-15)
            },
            new()
            {
                Id = FixedId(65),
                CarId = Cars[4].Id,
                Car = Cars[4],
                ClientId = Clients[4].Id,
                Client = Clients[4],
                MechanicId = Mechanics[1].Id,
                Mechanic = Mechanics[1],
                Works =
                [
                    Works[9]
                ],
                AcceptedAt = now.AddDays(-12),
                IssuedAt = now.AddDays(-11)
            },
            new()
            {
                Id = FixedId(66),
                CarId = Cars[5].Id,
                Car = Cars[5],
                ClientId = Clients[5].Id,
                Client = Clients[5],
                MechanicId = Mechanics[3].Id,
                Mechanic = Mechanics[3],
                Works =
                [
                    Works[7]
                ],
                AcceptedAt = now.AddDays(-9),
                IssuedAt = now.AddDays(-8)
            },
            new()
            {
                Id = FixedId(67),
                CarId = Cars[6].Id,
                Car = Cars[6],
                ClientId = Clients[6].Id,
                Client = Clients[6],
                MechanicId = Mechanics[8].Id,
                Mechanic = Mechanics[8],
                Works =
                [
                    Works[3]
                ],
                AcceptedAt = now.AddDays(-7),
                IssuedAt = now.AddDays(-5)
            },
            new()
            {
                Id = FixedId(68),
                CarId = Cars[7].Id,
                Car = Cars[7],
                ClientId = Clients[7].Id,
                Client = Clients[7],
                MechanicId = Mechanics[6].Id,
                Mechanic = Mechanics[6],
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
                Id = FixedId(69),
                CarId = Cars[8].Id,
                Car = Cars[8],
                ClientId = Clients[8].Id,
                Client = Clients[8],
                MechanicId = Mechanics[9].Id,
                Mechanic = Mechanics[9],
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
                Id = FixedId(70),
                CarId = Cars[9].Id,
                Car = Cars[9],
                ClientId = Clients[9].Id,
                Client = Clients[9],
                MechanicId = Mechanics[0].Id,
                Mechanic = Mechanics[0],
                Works =
                [
                    Works[4]
                ],
                AcceptedAt = now.AddDays(-1),
                IssuedAt = null
            },
            new()
            {
                Id = FixedId(71),
                CarId = Cars[0].Id,
                Car = Cars[0],
                ClientId = Clients[0].Id,
                Client = Clients[0],
                MechanicId = Mechanics[0].Id,
                Mechanic = Mechanics[0],
                Works =
                [
                    Works[0]
                ],
                AcceptedAt = now.AddDays(-3),
                IssuedAt = null
            },
            new()
            {
                Id = FixedId(72),
                CarId = Cars[0].Id,
                Car = Cars[0],
                ClientId = Clients[0].Id,
                Client = Clients[0],
                MechanicId = Mechanics[4].Id,
                Mechanic = Mechanics[4],
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
