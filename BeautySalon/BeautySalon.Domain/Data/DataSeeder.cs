using BeautySalon.Domain.Entities;
using BeautySalon.Shared.Enum;

namespace BeautySalon.Domain.Data;

/// <summary>
/// Тестовые данные
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// Мастера
    /// </summary>
    public static List<Master> Masters { get; } =
    [
        new Master
        {
            Id = 1,
            FullName = "Иванова Анна Сергеевна",
            Gender = Gender.Female,
            PassportNumber = "4501 123456",
            Specialization = "Парикмахер",
            WorkExperience = 10
        },
        new Master
        {
            Id = 2,
            FullName = "Петрова Мария Андреевна",
            Gender = Gender.Female,
            PassportNumber = "4502 234567",
            Specialization = "Визажист",
            WorkExperience = 7
        },
        new Master
        {
            Id = 3,
            FullName = "Соколова Елена Викторовна",
            Gender = Gender.Female,
            PassportNumber = "4503 345678",
            Specialization = "Мастер маникюра",
            WorkExperience = 5
        },
        new Master
        {
            Id = 4,
            FullName = "Кузнецов Дмитрий Олегович",
            Gender = Gender.Male,
            PassportNumber = "4504 456789",
            Specialization = "Парикмахер",
            WorkExperience = 6
        },
        new Master
        {
            Id = 5,
            FullName = "Орлова Екатерина Павловна",
            Gender = Gender.Female,
            PassportNumber = "4505 567890",
            Specialization = "Мастер маникюра",
            WorkExperience = 3
        },
        new Master
        {
            Id = 6,
            FullName = "Морозов Алексей Игоревич",
            Gender = Gender.Male,
            PassportNumber = "4506 678901",
            Specialization = "Барбер",
            WorkExperience = 2
        },
        new Master
        {
            Id = 7,
            FullName = "Волкова Ольга Максимовна",
            Gender = Gender.Female,
            PassportNumber = "4507 789012",
            Specialization = "Визажист",
            WorkExperience = 8
        },
        new Master
        {
            Id = 8,
            FullName = "Алексеева Наталья Романовна",
            Gender = Gender.Female,
            PassportNumber = "4508 890123",
            Specialization = "Косметолог",
            WorkExperience = 4
        },
        new Master
        {
            Id = 9,
            FullName = "Лебедев Артем Сергеевич",
            Gender = Gender.Male,
            PassportNumber = "4509 901234",
            Specialization = "Барбер",
            WorkExperience = 11
        },
        new Master
        {
            Id = 10,
            FullName = "Федорова Виктория Ильинична",
            Gender = Gender.Female,
            PassportNumber = "4510 012345",
            Specialization = "Косметолог",
            WorkExperience = 5
        }
    ];

    /// <summary>
    /// Клиенты
    /// </summary>
    public static List<Client> Clients { get; } =
    [
        new Client
        {
            Id = 1,
            FullName = "Смирнова Марина Алексеевна",
            Gender = Gender.Female,
            Birthday = new DateOnly(1990, 4, 15),
            PhoneNumber = "+7 900 000-00-01"
        },
        new Client
        {
            Id = 2,
            FullName = "Иванов Сергей Петрович",
            Gender = Gender.Male,
            Birthday = new DateOnly(1985, 8, 20),
            PhoneNumber = "+7 900 000-00-02"
        },
        new Client
        {
            Id = 3,
            FullName = "Кузнецова Ольга Дмитриевна",
            Gender = Gender.Female,
            Birthday = new DateOnly(1995, 2, 10),
            PhoneNumber = "+7 900 000-00-03"
        },
        new Client
        {
            Id = 4,
            FullName = "Попов Андрей Викторович",
            Gender = Gender.Male,
            Birthday = new DateOnly(1988, 11, 3),
            PhoneNumber = "+7 900 000-00-04"
        },
        new Client
        {
            Id = 5,
            FullName = "Васильева Екатерина Олеговна",
            Gender = Gender.Female,
            Birthday = new DateOnly(1992, 6, 25),
            PhoneNumber = "+7 900 000-00-05"
        },
        new Client
        {
            Id = 6,
            FullName = "Михайлов Денис Сергеевич",
            Gender = Gender.Male,
            Birthday = new DateOnly(1998, 1, 18),
            PhoneNumber = "+7 900 000-00-06"
        },
        new Client
        {
            Id = 7,
            FullName = "Николаева Алина Романовна",
            Gender = Gender.Female,
            Birthday = new DateOnly(1983, 9, 12),
            PhoneNumber = "+7 900 000-00-07"
        },
        new Client
        {
            Id = 8,
            FullName = "Федоров Максим Игоревич",
            Gender = Gender.Male,
            Birthday = new DateOnly(1991, 12, 7),
            PhoneNumber = "+7 900 000-00-08"
        },
        new Client
        {
            Id = 9,
            FullName = "Соколова Виктория Андреевна",
            Gender = Gender.Female,
            Birthday = new DateOnly(1996, 5, 30),
            PhoneNumber = "+7 900 000-00-09"
        },
        new Client
        {
            Id = 10,
            FullName = "Орлов Роман Александрович",
            Gender = Gender.Male,
            Birthday = new DateOnly(1987, 3, 22),
            PhoneNumber = "+7 900 000-00-10"
        }
    ];

    /// <summary>
    /// Услуги
    /// </summary>
    public static List<Service> Services { get; } =
    [
        new Service
        {
            Id = 1,
            Name = "Стрижка",
            Category = "Волосы",
            Price = 1800m,
            Duration = 60
        },
        new Service
        {
            Id = 2,
            Name = "Маникюр",
            Category = "Ногти",
            Price = 1500m,
            Duration = 90
        },
        new Service
        {
            Id = 3,
            Name = "Окрашивание",
            Category = "Волосы",
            Price = 4500m,
            Duration = 180
        },
        new Service
        {
            Id = 4,
            Name = "Укладка",
            Category = "Волосы",
            Price = 2000m,
            Duration = 60
        },
        new Service
        {
            Id = 5,
            Name = "Педикюр",
            Category = "Ногти",
            Price = 2200m,
            Duration = 120
        },
        new Service
        {
            Id = 6,
            Name = "Макияж",
            Category = "Макияж",
            Price = 2500m,
            Duration = 90
        },
        new Service
        {
            Id = 7,
            Name = "Коррекция бровей",
            Category = "Брови",
            Price = 1000m,
            Duration = 30
        },
        new Service
        {
            Id = 8,
            Name = "Уход за лицом",
            Category = "Косметология",
            Price = 3000m,
            Duration = 90
        },
        new Service
        {
            Id = 9,
            Name = "Массаж",
            Category = "Уход",
            Price = 2500m,
            Duration = 60
        },
        new Service
        {
            Id = 10,
            Name = "Ламинирование ресниц",
            Category = "Ресницы",
            Price = 2800m,
            Duration = 90
        }
    ];

    /// <summary>
    /// Записи
    /// </summary>
    public static List<Appointment> Appointments { get; } =
    [
        new Appointment
        {
            Id = 1,
            Client = Clients[0],
            Master = Masters[0],
            Service = Services[0],
            StartTime = new DateTime(2026, 9, 25, 9, 0, 0),
            IsRegular = false
        },
        new Appointment
        {
            Id = 2,
            Client = Clients[1],
            Master = Masters[0],
            Service = Services[1],
            StartTime = new DateTime(2026, 9, 25, 11, 0, 0),
            IsRegular = false
        },
        new Appointment
        {
            Id = 3,
            Client = Clients[2],
            Master = Masters[0],
            Service = Services[3],
            StartTime = new DateTime(2026, 9, 25, 14, 0, 0),
            IsRegular = false
        },

        new Appointment
        {
            Id = 4,
            Client = Clients[0],
            Master = Masters[1],
            Service = Services[0],
            StartTime = new DateTime(2026, 9, 1, 10, 0, 0),
            IsRegular = true
        },
        new Appointment
        {
            Id = 5,
            Client = Clients[0],
            Master = Masters[2],
            Service = Services[1],
            StartTime = new DateTime(2026, 9, 10, 12, 0, 0),
            IsRegular = true
        },
        new Appointment
        {
            Id = 6,
            Client = Clients[0],
            Master = Masters[3],
            Service = Services[3],
            StartTime = new DateTime(2026, 9, 20, 15, 0, 0),
            IsRegular = true
        },

        new Appointment
        {
            Id = 7,
            Client = Clients[1],
            Master = Masters[3],
            Service = Services[0],
            StartTime = new DateTime(2026, 9, 5, 10, 0, 0),
            IsRegular = true
        },
        new Appointment
        {
            Id = 8,
            Client = Clients[1],
            Master = Masters[4],
            Service = Services[2],
            StartTime = new DateTime(2026, 9, 18, 11, 0, 0),
            IsRegular = true
        },

        new Appointment
        {
            Id = 9,
            Client = Clients[2],
            Master = Masters[5],
            Service = Services[1],
            StartTime = new DateTime(2026, 8, 30, 10, 0, 0),
            IsRegular = true
        },
        new Appointment
        {
            Id = 10,
            Client = Clients[2],
            Master = Masters[6],
            Service = Services[4],
            StartTime = new DateTime(2026, 9, 8, 13, 0, 0),
            IsRegular = true
        },
        new Appointment
        {
            Id = 11,
            Client = Clients[2],
            Master = Masters[6],
            Service = Services[5],
            StartTime = new DateTime(2026, 9, 15, 16, 0, 0),
            IsRegular = true
        },

        new Appointment
        {
            Id = 12,
            Client = Clients[3],
            Master = Masters[3],
            Service = Services[0],
            StartTime = new DateTime(2026, 9, 12, 10, 0, 0),
            IsRegular = false
        },

        new Appointment
        {
            Id = 13,
            Client = Clients[4],
            Master = Masters[4],
            Service = Services[2],
            StartTime = new DateTime(2026, 7, 15, 11, 0, 0),
            IsRegular = false
        },
        new Appointment
        {
            Id = 14,
            Client = Clients[5],
            Master = Masters[5],
            Service = Services[4],
            StartTime = new DateTime(2026, 7, 20, 12, 0, 0),
            IsRegular = false
        }
    ];
}