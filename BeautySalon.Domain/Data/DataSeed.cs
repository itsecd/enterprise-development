using BeautySalon.Domain.Models;
using BeautySalon.Enums;

namespace BeautySalon.Domain.Data;

/// <summary>
/// Тестовые данные для предметной области салона красоты.
/// </summary>
public static class DataSeed
{
    /// <summary>
    /// Сегодняшняя дата без времени.
    /// </summary>
    private static DateTime Today => DateTime.Today;

    /// <summary>
    /// Время сегодня.
    /// </summary>
    private static DateTime At(int hour, int minute = 0) =>
        Today.AddHours(hour).AddMinutes(minute);

    /// <summary>
    /// Время дней назад.
    /// </summary>
    private static DateTime DaysAgo(int days, int hour, int minute = 0) =>
        Today.AddDays(-days).AddHours(hour).AddMinutes(minute);

    /// <summary>
    /// Список клиентов.
    /// </summary>
    public static List<Client> Clients { get; } = new()
    {
        new Client
        {
            LoyaltyPoints = 10,
            Id = 1,
            FullName = "Иванова Мария Сергеевна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1995, 5, 20),
            PhoneNumber = "+79991110001"
        },
        new Client
        {
            LoyaltyPoints = 11,
            Id = 2,
            FullName = "Петров Дмитрий Алексеевич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1988, 11, 2),
            PhoneNumber = "+79991110002"
        },
        new Client
        {
            LoyaltyPoints = 12,
            Id = 3,
            FullName = "Сидорова Анна Викторовна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(2000, 1, 15),
            PhoneNumber = "+79991110003"
        },
        new Client
        {
            LoyaltyPoints = 13,
            Id = 4,
            FullName = "Кузнецов Сергей Павлович",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1975, 7, 30),
            PhoneNumber = "+79991110004"
        },
        new Client
        {
            LoyaltyPoints = 14,
            Id = 5,
            FullName = "Морозова Елена Дмитриевна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1992, 3, 11),
            PhoneNumber = "+79991110005"
        },
        new Client
        {
            LoyaltyPoints = 15,
            Id = 6,
            FullName = "Волков Андрей Игоревич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1985, 9, 25),
            PhoneNumber = "+79991110006"
        },
        new Client
        {
            LoyaltyPoints = 16,
            Id = 7,
            FullName = "Новикова Ольга Николаевна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1998, 12, 5),
            PhoneNumber = "+79991110007"
        },
        new Client
        {
            LoyaltyPoints = 17,
            Id = 8,
            FullName = "Федоров Максим Юрьевич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1990, 6, 18),
            PhoneNumber = "+79991110008"
        },
        new Client
        {
            LoyaltyPoints = 18,
            Id = 9,
            FullName = "Захарова Ирина Александровна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1983, 4, 27),
            PhoneNumber = "+79991110009"
        },
        new Client
        {
            LoyaltyPoints = 19,
            Id = 10,
            FullName = "Белов Кирилл Романович",
            Gender = Gender.Male,
            BirthDate = new DateOnly(2001, 8, 9),
            PhoneNumber = "+79991110010"
        }
    };

    /// <summary>
    /// Список мастеров.
    /// </summary>
    public static List<Master> Masters { get; } = new()
    {
        new Master
        {
            Id = 11,
            FullName = "Петрова Анна Викторовна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1990, 3, 15),
            PhoneNumber = "+79992220001",
            PassportNumber = "1234 567890",
            ExperienceYears = 8,
            Specializations = { "Мастер маникюра", "Педикюр" }
        },
        new Master
        {
            Id = 12,
            FullName = "Соколов Игорь Михайлович",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1982, 5, 4),
            PhoneNumber = "+79992220002",
            PassportNumber = "1234 567891",
            ExperienceYears = 3,
            Specializations = { "Парикмахер" }
        },
        new Master
        {
            Id = 13,
            FullName = "Михайлова Татьяна Сергеевна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1978, 10, 20),
            PhoneNumber = "+79992220003",
            PassportNumber = "1234 567892",
            ExperienceYears = 12,
            Specializations = { "Косметолог", "Уход за лицом" }
        },
        new Master
        {
            Id = 14,
            FullName = "Козлов Дмитрий Андреевич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1991, 2, 14),
            PhoneNumber = "+79992220004",
            PassportNumber = "1234 567893",
            ExperienceYears = 5,
            Specializations = { "Парикмахер", "Барбер" }
        },
        new Master
        {
            Id = 15,
            FullName = "Лебедева Мария Ивановна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1999, 6, 30),
            PhoneNumber = "+79992220005",
            PassportNumber = "1234 567894",
            ExperienceYears = 1,
            Specializations = { "Визажист" }
        },
        new Master
        {
            Id = 16,
            FullName = "Орлов Алексей Петрович",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1980, 12, 1),
            PhoneNumber = "+79992220006",
            PassportNumber = "1234 567895",
            ExperienceYears = 7,
            Specializations = { "Массажист", "СПА-специалист" }
        },
        new Master
        {
            Id = 17,
            FullName = "Егорова Светлана Николаевна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1994, 9, 17),
            PhoneNumber = "+79992220007",
            PassportNumber = "1234 567896",
            ExperienceYears = 2,
            Specializations = { "Ногтевой сервис" }
        },
        new Master
        {
            Id = 18,
            FullName = "Павлов Роман Дмитриевич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1976, 1, 25),
            PhoneNumber = "+79992220008",
            PassportNumber = "1234 567897",
            ExperienceYears = 10,
            Specializations = { "Парикмахер", "Окрашивание волос" }
        },
        new Master
        {
            Id = 19,
            FullName = "Семенова Дарья Александровна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1997, 11, 11),
            PhoneNumber = "+79992220009",
            PassportNumber = "1234 567898",
            ExperienceYears = 4,
            Specializations = { "Эпиляция", "Уход за телом" }
        },
        new Master
        {
            Id = 20,
            FullName = "Громов Виктор Сергеевич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1987, 8, 8),
            PhoneNumber = "+79992220010",
            PassportNumber = "1234 567899",
            ExperienceYears = 6,
            Specializations = { "Массажист", "СПА-специалист" }
        }
    };

    /// <summary>
    /// Список услуг.
    /// </summary>
    public static List<Service> Services { get; } = new()
    {
        new Service
        {
            Id = 21,
            Name = "Маникюр классический",
            Category = ServiceCategory.NailCare,
            Price = 1500m,
            Duration = TimeSpan.FromMinutes(60)
        },
        new Service
        {
            Id = 22,
            Name ="Педикюр",
            Category = ServiceCategory.NailCare,
            Price = 2000m,
            Duration = TimeSpan.FromMinutes(90)
        },
        new Service
        {
            Id = 23,
            Name = "Женская стрижка",
            Category = ServiceCategory.HairCare,
            Price = 2500m,
            Duration = TimeSpan.FromMinutes(90)
        },
        new Service
        {
            Id = 24,
            Name = "Мужская стрижка",
            Category = ServiceCategory.HairCare,
            Price = 1800m,
            Duration = TimeSpan.FromMinutes(60)
        },
        new Service
        {
            Id = 25,
            Name = "Окрашивание волос",
            Category = ServiceCategory.HairCare,
            Price = 5000m,
            Duration = TimeSpan.FromMinutes(180)
        },
        new Service
        {
            Id = 26,
            Name = "Чистка лица",
            Category = ServiceCategory.Skincare,
            Price = 3500m,
            Duration = TimeSpan.FromMinutes(75)
        },
        new Service
        {
            Id = 27,
            Name = "Уходовая маска для лица",
            Category = ServiceCategory.Skincare,
            Price = 2200m,
            Duration = TimeSpan.FromMinutes(45)
        },
        new Service
        {
            Id = 28,
            Name = "Вечерний макияж",
            Category = ServiceCategory.Makeup,
            Price = 4000m,
            Duration = TimeSpan.FromMinutes(90)
        },
        new Service
        {
            Id = 29,
            Name = "Депиляция ног",
            Category = ServiceCategory.HairRemoval,
            Price = 3000m,
            Duration = TimeSpan.FromMinutes(60)
        },
        new Service
        {
            Id = 30,
            Name = "СПА-программа",
            Category = ServiceCategory.Spa,
            Price = 7000m,
            Duration = TimeSpan.FromMinutes(120)
        }
    };

    /// <summary>
    /// Список записей клиентов на услуги.
    /// </summary>
    public static List<Appointment> Appointments { get; } = new()
    {
        // Записи выбранного мастера Masters[0] для проверки окошек.
        // 09:00-10:00, потом окошко 10:00-11:30.
        new Appointment
        {
            Id = 31,
            ClientId = Clients[0].Id,
            MasterId = Masters[0].Id,
            ServiceId = Services[0].Id,
            StartDateTime = At(9, 0),
            IsRegularClient = true
        },

        // 11:30-13:00, потом окошко 13:00-13:30.
        new Appointment
        {
            Id = 32,
            ClientId = Clients[1].Id,
            MasterId = Masters[0].Id,
            ServiceId = Services[1].Id,
            StartDateTime = At(11, 30),
            IsRegularClient = false
        },

        // 13:30-14:30, потом окошко 14:30-16:00.
        new Appointment
        {
            Id = 33,
            ClientId = Clients[2].Id,
            MasterId = Masters[0].Id,
            ServiceId = Services[0].Id,
            StartDateTime = At(13, 30),
            IsRegularClient = true
        },

        // 16:00-17:30.
        new Appointment
        {
            Id = 34,
            ClientId = Clients[3].Id,
            MasterId = Masters[0].Id,
            ServiceId = Services[2].Id,
            StartDateTime = At(16, 0),
            IsRegularClient = false
        },

        // Повторная запись клиента Clients[0] к другому мастеру.
        new Appointment
        {
            Id = 35,
            ClientId = Clients[0].Id,
            MasterId = Masters[1].Id,
            ServiceId = Services[0].Id,
            StartDateTime = DaysAgo(1, 10, 0),
            IsRegularClient = true
        },

        // Повторная запись клиента Clients[1] к другому мастеру.
        new Appointment
        {
            Id = 36,
            ClientId = Clients[1].Id,
            MasterId = Masters[2].Id,
            ServiceId = Services[1].Id,
            StartDateTime = DaysAgo(1, 12, 0),
            IsRegularClient = false
        },

        // Повторная запись клиента Clients[2] к другому мастеру.
        new Appointment
        {
            Id = 37,
            ClientId = Clients[2].Id,
            MasterId = Masters[3].Id,
            ServiceId = Services[2].Id,
            StartDateTime = DaysAgo(2, 14, 0),
            IsRegularClient = true
        },

        // Повторная запись клиента Clients[3] к другому мастеру.
        new Appointment
        {
            Id = 38,
            ClientId = Clients[3].Id,
            MasterId = Masters[4].Id,
            ServiceId = Services[0].Id,
            StartDateTime = DaysAgo(2, 9, 30),
            IsRegularClient = false
        },

        // Еще записи для статистики по услугам.
        new Appointment
        {
            Id = 39,
            ClientId = Clients[4].Id,
            MasterId = Masters[5].Id,
            ServiceId = Services[1].Id,
            StartDateTime = DaysAgo(3, 11, 0),
            IsRegularClient = true
        },
        new Appointment
        {
            Id = 40,
            ClientId = Clients[5].Id,
            MasterId = Masters[6].Id,
            ServiceId = Services[3].Id,
            StartDateTime = DaysAgo(3, 13, 0),
            IsRegularClient = false
        },
        new Appointment
        {
            Id = 41,
            ClientId = Clients[6].Id,
            MasterId = Masters[7].Id,
            ServiceId = Services[3].Id,
            StartDateTime = DaysAgo(4, 15, 0),
            IsRegularClient = true
        },

        // Клиент Clients[7] записан к нескольким мастерам и имеет повторные записи за последний месяц.
        new Appointment
        {
            Id = 42,
            ClientId = Clients[7].Id,
            MasterId = Masters[8].Id,
            ServiceId = Services[4].Id,
            StartDateTime = DaysAgo(5, 10, 0),
            IsRegularClient = false
        },
        new Appointment
        {
            Id = 43,
            ClientId = Clients[7].Id,
            MasterId = Masters[9].Id,
            ServiceId = Services[5].Id,
            StartDateTime = DaysAgo(10, 12, 0),
            IsRegularClient = true
        },

        // Дополнительная запись для популярности услуги Services[4].
        new Appointment
        {
            Id = 44,
            ClientId = Clients[8].Id,
            MasterId = Masters[1].Id,
            ServiceId = Services[4].Id,
            StartDateTime = DaysAgo(12, 9, 0),
            IsRegularClient = false
        }
    };
}