#pragma warning disable CS1591

using BeautySalon.Domain.Entities;
using BeautySalon.Domain.Enums;

namespace BeautySalon.Domain.Data;

/// <summary>
/// Фикстура с тестовыми данными для тестирования доменной модели.
/// </summary>
public class TestDataFixture
{
    /// <summary>
    /// Фиксированное время и дата, для детерминированности тестов.
    /// </summary>
    public DateTime BaseDate { get; } = new DateTime(2026, 11, 19, 14, 0, 0);

    public List<Client> Clients { get; }
    public List<Master> Masters { get; }
    public List<Service> Services { get; }
    public List<Appointment> Appointments { get; }

    public TestDataFixture()
    {


        Clients = new List<Client>
        {
            new Client {
                LastName = "Иванов",
                FirstName = "Иван",
                Patronymic = "Иванович",
                Gender = Gender.Male,
                BirthDate = new DateTime(1995, 5, 10),
                PhoneNumber = "111"
            },
            new Client {
                LastName = "Петрова",
                FirstName = "Анна",
                Patronymic = "Сергеевна",
                Gender = Gender.Female,
                BirthDate = new DateTime(1990, 8, 20),
                PhoneNumber = "222"
            },
            new Client
            {
                LastName = "Сидоров",
                FirstName = "Алексей",
                Patronymic = null,
                Gender = Gender.Male,
                BirthDate = new DateTime(1988, 2, 15),
                PhoneNumber = "333"
            },
            new Client
            {
                LastName = "Смирнова",
                FirstName = "Мария",
                Patronymic = "Игоревна",
                Gender = Gender.Female,
                BirthDate = new DateTime(2000, 11, 30),
                PhoneNumber = "444"
            },
            new Client
            {
                LastName = "Кузнецов",
                FirstName = "Дмитрий",
                Patronymic = "Петрович",
                Gender = Gender.Male,
                BirthDate = new DateTime(1992, 7, 5),
                PhoneNumber = "555"
            },
            new Client
            {
                LastName = "Соколова",
                FirstName = "Елена",
                Patronymic = "Валерьевна",
                Gender = Gender.Female,
                BirthDate = new DateTime(1985, 3, 12),
                PhoneNumber = "666"
            },
            new Client
            {
                LastName = "Попов",
                FirstName = "Андрей",
                Patronymic = "Николаевич",
                Gender = Gender.Male,
                BirthDate = new DateTime(1998, 9, 25),
                PhoneNumber = "777"
            },
            new Client
            {
                LastName = "Лебедева",
                FirstName = "Ольга",
                Patronymic = "Дмитриевна",
                Gender = Gender.Female,
                BirthDate = new DateTime(1993, 1, 18),
                PhoneNumber = "888"
            },
            new Client
            {
                LastName = "Козлов",
                FirstName = "Сергей",
                Patronymic = null,
                Gender = Gender.Male,
                BirthDate = new DateTime(1991, 6, 22),
                PhoneNumber = "999"
            },
            new Client
            {
                LastName = "Новикова",
                FirstName = "Татьяна",
                Patronymic = "Александровна",
                Gender = Gender.Female,
                BirthDate = new DateTime(1997, 4, 8),
                PhoneNumber = "000"
            }
        };

        Masters = new List<Master>
        {
            new Master
            {
                LastName = "Сидорова",
                FirstName = "Ольга",
                Patronymic = "Ивановна",
                Gender = Gender.Female,
                PassportNumber = "1001",
                Specialization = "Парикмахер",
                ExperienceYears = 10
            },
            new Master
            {
                LastName = "Кузнецов",
                FirstName = "Олег",
                Patronymic = null,
                Gender = Gender.Male,
                PassportNumber = "1002",
                Specialization = "Барбер",
                ExperienceYears = 3
            },
            new Master
            {
                LastName = "Смирнов",
                FirstName = "Павел",
                Patronymic = "Алексеевич",
                Gender = Gender.Male,
                PassportNumber = "1003",
                Specialization = "Мастер маникюра",
                ExperienceYears = 5
            },
            new Master
            {
                LastName = "Волкова",
                FirstName = "Наталья",
                Patronymic = "Владимировна",
                Gender = Gender.Female,
                PassportNumber = "1004",
                Specialization = "Косметолог",
                ExperienceYears = 7
            },
            new Master
            {
                LastName = "Морозов",
                FirstName = "Денис",
                Patronymic = "Сергеевич",
                Gender = Gender.Male,
                PassportNumber = "1005",
                Specialization = "Массажист",
                ExperienceYears = 2
            },
            new Master
            {
                LastName = "Павлова",
                FirstName = "Ирина",
                Patronymic = "Андреевна",
                Gender = Gender.Female,
                PassportNumber = "1006",
                Specialization = "Парикмахер",
                ExperienceYears = 12
            },
            new Master
            {
                LastName = "Семенов",
                FirstName = "Артем",
                Patronymic = null,
                Gender = Gender.Male,
                PassportNumber = "1007",
                Specialization = "Барбер",
                ExperienceYears = 4
            },
            new Master
            {
                LastName = "Григорьева",
                FirstName = "Светлана",
                Patronymic = "Петровна",
                Gender = Gender.Female,
                PassportNumber = "1008",
                Specialization = "Мастер маникюра",
                ExperienceYears = 6
            },
            new Master
            {
                LastName = "Николаев",
                FirstName = "Максим",
                Patronymic = "Игоревич",
                Gender = Gender.Male,
                PassportNumber = "1009",
                Specialization = "Косметолог",
                ExperienceYears = 8
            },
            new Master
            {
                LastName = "Алексеева",
                FirstName = "Виктория",
                Patronymic = "Сергеевна",
                Gender = Gender.Female,
                PassportNumber = "1010",
                Specialization = "Массажист",
                ExperienceYears = 1
            }
        };

        Services = new List<Service>
        {
            new Service
            {
                Name = "Стрижка",
                Category = "Волосы",
                Price = 1500,
                Duration = TimeSpan.FromMinutes(60)
            },
            new Service
            {
                Name = "Маникюр",
                Category = "Ногти",
                Price = 2000,
                Duration = TimeSpan.FromMinutes(90)
            },
            new Service
            {
                Name = "Педикюр",
                Category = "Ногти",
                Price = 2500,
                Duration = TimeSpan.FromMinutes(120)
            },
            new Service
            {
                Name = "Окрашивание",
                Category = "Волосы",
                Price = 4000,
                Duration = TimeSpan.FromMinutes(180)
            },
            new Service
            {
                Name = "Массаж лица",
                Category = "Косметология",
                Price = 3000,
                Duration = TimeSpan.FromMinutes(45)
            },
            new Service
            {
                Name = "Чистка лица",
                Category = "Косметология",
                Price = 3500,
                Duration = TimeSpan.FromMinutes(60)
            },
            new Service
            {
                Name = "Укладка",
                Category = "Волосы",
                Price = 1000,
                Duration = TimeSpan.FromMinutes(30)
            },
            new Service
            {
                Name = "Барбершоп-бритье",
                Category = "Волосы",
                Price = 1200,
                Duration = TimeSpan.FromMinutes(40)
            },
            new Service
            {
                Name = "Наращивание ресниц",
                Category = "Ресницы",
                Price = 2800,
                Duration = TimeSpan.FromMinutes(120)
            },
            new Service
            {
                Name = "Коррекция бровей",
                Category = "Брови",
                Price = 800,
                Duration = TimeSpan.FromMinutes(20)
            }
        };

        Appointments = new List<Appointment>
        {
            new Appointment
            {
                Client = Clients[0],
                Master = Masters[0],
                Service = Services[0],
                AppointmentDate = BaseDate.AddDays(-10).AddHours(10),
                IsRegularClient = true
            },
            new Appointment
            {
                Client = Clients[1],
                Master = Masters[0],
                Service = Services[6],
                AppointmentDate = BaseDate.AddDays(-10).AddHours(13),
                IsRegularClient = true
            },
            new Appointment
            {
                Client = Clients[2],
                Master = Masters[0],
                Service = Services[0],
                AppointmentDate = BaseDate.AddDays(-9).AddHours(9),
                IsRegularClient = false
            },

            new Appointment
            {
                Client = Clients[0],
                Master = Masters[2],
                Service = Services[1],
                AppointmentDate = BaseDate.AddDays(-5).AddHours(12),
                IsRegularClient = true
            },
            new Appointment
            {
                Client = Clients[0],
                Master = Masters[3],
                Service = Services[4],
                AppointmentDate = BaseDate.AddDays(-2).AddHours(15),
                IsRegularClient = true
            },
            new Appointment
            {
                Client = Clients[3],
                Master = Masters[2],
                Service = Services[2],
                AppointmentDate = BaseDate.AddDays(-8).AddHours(11),
                IsRegularClient = true
            },
            new Appointment
            {
                Client = Clients[1],
                Master = Masters[1],
                Service = Services[0],
                AppointmentDate = BaseDate.AddDays(-15).AddHours(14),
                IsRegularClient = true
            },
            new Appointment
            {
                Client = Clients[1],
                Master = Masters[2],
                Service = Services[1],
                AppointmentDate = BaseDate.AddDays(-32).AddHours(10),
                IsRegularClient = true
            },
            new Appointment
            {
                Client = Clients[4],
                Master = Masters[5],
                Service = Services[3],
                AppointmentDate = BaseDate.AddDays(-1).AddHours(16),
                IsRegularClient = true
            },
            new Appointment
            {
                Client = Clients[4],
                Master = Masters[5],
                Service = Services[6],
                AppointmentDate = BaseDate.AddHours(12),
                IsRegularClient = true
            },
            new Appointment
            {
                Client = Clients[5],
                Master = Masters[4],
                Service = Services[5],
                AppointmentDate = BaseDate.AddDays(-25).AddHours(10),
                IsRegularClient = false
            },
            new Appointment
            {
                Client = Clients[6],
                Master = Masters[0],
                Service = Services[0],
                AppointmentDate = BaseDate.AddDays(-2).AddHours(18),
                IsRegularClient = true
            },
            new Appointment
            {
                Client = Clients[7],
                Master = Masters[1],
                Service = Services[7],
                AppointmentDate = BaseDate.AddDays(-3).AddHours(11),
                IsRegularClient = false
            },
            new Appointment
            {
                Client = Clients[8],
                Master = Masters[2],
                Service = Services[1],
                AppointmentDate = BaseDate.AddDays(-4).AddHours(13),
                IsRegularClient = true
            },
            new Appointment
            {
                Client = Clients[9],
                Master = Masters[0],
                Service = Services[0],
                AppointmentDate = BaseDate.AddDays(-6).AddHours(12),
                IsRegularClient = false
            }
        };
    }
}