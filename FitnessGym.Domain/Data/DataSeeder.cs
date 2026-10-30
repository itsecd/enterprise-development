using FitnessGym.Domain.Entities;
using FitnessGym.Domain.Enums;

namespace FitnessGym.Domain.Data;

/// <summary>
/// Класс для генерации тестовых данных в памяти
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// Зафиксированный момент времени, относительно которого строятся тестовые данные
    /// </summary>
    public static readonly DateTime Now = new(2026, 5, 15, 12, 0, 0, DateTimeKind.Utc);

    /// <summary>
    /// Дата, соответствующая зафиксированному моменту времени
    /// </summary>
    public static readonly DateOnly Today = DateOnly.FromDateTime(Now);

    public static readonly List<Specialization> Specializations;
    public static readonly List<Client> Clients;
    public static readonly List<Trainer> Trainers;
    public static readonly List<TrainingSession> Sessions;

    // Статический конструктор выполнится один раз при первом обращении к классу
    static DataSeeder()
    {
        Specializations = BuildSpecializations();
        Clients = BuildClients();
        Trainers = BuildTrainers();
        Sessions = BuildSessions();
    }

    private static List<Specialization> BuildSpecializations() =>
    [
        new Specialization { Id = 0, Name = "Аэробика" },
        new Specialization { Id = 1, Name = "Аквааэробика" },
        new Specialization { Id = 2, Name = "Бодибилдинг" },
        new Specialization { Id = 3, Name = "Пауэрлифтинг" },
        new Specialization { Id = 4, Name = "Кроссфит" },
        new Specialization { Id = 5, Name = "Йога" },
        new Specialization { Id = 6, Name = "Пилатес" },
        new Specialization { Id = 7, Name = "Стретчинг" },
        new Specialization { Id = 8, Name = "Единоборства" },
        new Specialization { Id = 9, Name = "Танцы" },
    ];

    private static List<Client> BuildClients() =>
    [
        // Клиенты с истёкшим абонементом — упорядочены по ФИО для удобства проверки
        new Client
        {
            Id = 0, PassportNumber = "5101 111001",
            FullName = "Гусев Роман Валерьевич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1988, 3, 14),
            Phone = "+7 (900) 200-10-01",
            SubscriptionStart = Today.AddDays(-90),
            SubscriptionEnd = Today.AddDays(-15),
        },
        new Client
        {
            Id = 1, PassportNumber = "5102 111002",
            FullName = "Ершова Полина Дмитриевна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1995, 7, 22),
            Phone = "+7 (900) 200-10-02",
            SubscriptionStart = Today.AddDays(-60),
            SubscriptionEnd = Today.AddDays(-3),
        },
        new Client
        {
            Id = 2, PassportNumber = "5103 111003",
            FullName = "Медведев Артур Игоревич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1979, 11, 5),
            Phone = "+7 (900) 200-10-03",
            SubscriptionStart = Today.AddDays(-180),
            SubscriptionEnd = Today.AddDays(-40),
        },
        new Client
        {
            Id = 3, PassportNumber = "5104 111004",
            FullName = "Соколова Виктория Андреевна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1992, 1, 30),
            Phone = "+7 (900) 200-10-04",
            SubscriptionStart = Today.AddDays(-120),
            SubscriptionEnd = Today.AddDays(-7),
        },
        new Client
        {
            Id = 4, PassportNumber = "5105 111005",
            FullName = "Фролов Никита Сергеевич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(2000, 9, 9),
            Phone = "+7 (900) 200-10-05",
            SubscriptionStart = Today.AddDays(-45),
            SubscriptionEnd = Today.AddDays(-1),
        },

        // Клиенты с действующим абонементом
        new Client
        {
            Id = 5, PassportNumber = "5106 111006",
            FullName = "Афанасьев Владислав Игоревич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1985, 5, 18),
            Phone = "+7 (900) 200-10-06",
            SubscriptionStart = Today.AddDays(-30),
            SubscriptionEnd = Today.AddDays(60),
        },
        new Client
        {
            Id = 6, PassportNumber = "5107 111007",
            FullName = "Григорьева Валерия Павловна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1998, 12, 3),
            Phone = "+7 (900) 200-10-07",
            SubscriptionStart = Today.AddDays(-15),
            SubscriptionEnd = Today.AddDays(75),
        },
        new Client
        {
            Id = 7, PassportNumber = "5108 111008",
            FullName = "Комарова Дарья Алексеевна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1990, 6, 25),
            Phone = "+7 (900) 200-10-08",
            SubscriptionStart = Today.AddDays(-90),
            SubscriptionEnd = Today.AddDays(30),
        },
        new Client
        {
            Id = 8, PassportNumber = "5109 111009",
            FullName = "Степанов Егор Олегович",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1983, 8, 11),
            Phone = "+7 (900) 200-10-09",
            SubscriptionStart = Today.AddDays(-60),
            SubscriptionEnd = Today.AddDays(120),
        },
        new Client
        {
            Id = 9, PassportNumber = "5110 111010",
            FullName = "Филиппова Алёна Максимовна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(2002, 2, 17),
            Phone = "+7 (900) 200-10-10",
            SubscriptionStart = Today.AddDays(-1),
            SubscriptionEnd = Today.AddDays(180),
        },

        // Клиенты, абонемент которых ещё не начался
        new Client
        {
            Id = 10, PassportNumber = "5111 111011",
            FullName = "Данилов Кирилл Андреевич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(2003, 4, 20),
            Phone = "+7 (900) 200-10-11",
            SubscriptionStart = Today.AddDays(10),
            SubscriptionEnd = Today.AddDays(100),
        },
        new Client
        {
            Id = 11, PassportNumber = "5112 111012",
            FullName = "Никитина София Романовна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1996, 10, 8),
            Phone = "+7 (900) 200-10-12",
            SubscriptionStart = Today.AddDays(20),
            SubscriptionEnd = Today.AddDays(110),
        },
    ];

    private static List<Trainer> BuildTrainers() =>
    [
        new Trainer
        {
            Id = 0,
            PassportNumber = "5201 222001",
            FullName = "Тарасов Андрей Викторович",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1991, 4, 2),
            Specialization = Specializations[4],
            ExperienceYears = 4
        },
        new Trainer
        {
            Id = 1,
            PassportNumber = "5202 222002",
            FullName = "Громова Елена Сергеевна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1987, 9, 19),
            Specialization = Specializations[5],
            ExperienceYears = 6
        },
        new Trainer
        {
            Id = 2,
            PassportNumber = "5203 222003",
            FullName = "Белов Максим Олегович",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1999, 12, 14),
            Specialization = Specializations[8],
            ExperienceYears = 3
        },
        new Trainer
        {
            Id = 3,
            PassportNumber = "5204 222004",
            FullName = "Орлова Наталья Ивановна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1982, 2, 8),
            Specialization = Specializations[6],
            ExperienceYears = 9
        },
        new Trainer
        {
            Id = 4,
            PassportNumber = "5205 222005",
            FullName = "Крылов Дмитрий Андреевич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1978, 7, 21),
            Specialization = Specializations[3],
            ExperienceYears = 11
        },
        new Trainer
        {
            Id = 5,
            PassportNumber = "5206 222006",
            FullName = "Соловьёва Анна Михайловна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1993, 5, 16),
            Specialization = Specializations[7],
            ExperienceYears = 5
        },
        new Trainer
        {
            Id = 6,
            PassportNumber = "5207 222007",
            FullName = "Николаев Артём Павлович",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1986, 11, 30),
            Specialization = Specializations[8],
            ExperienceYears = 8
        },
        new Trainer
        {
            Id = 7,
            PassportNumber = "5208 222008",
            FullName = "Романова Екатерина Дмитриевна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1989, 8, 25),
            Specialization = Specializations[9],
            ExperienceYears = 7
        },
        new Trainer
        {
            Id = 8,
            PassportNumber = "5209 222009",
            FullName = "Виноградов Сергей Николаевич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(2001, 1, 7),
            Specialization = Specializations[1],
            ExperienceYears = 2
        },
        new Trainer
        {
            Id = 9,
            PassportNumber = "5210 222010",
            FullName = "Полякова Марина Владимировна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1994, 6, 12),
            Specialization = Specializations[0],
            ExperienceYears = 5
        },
        new Trainer
        {
            Id = 10,
            PassportNumber = "5211 222011",
            FullName = "Тихонов Игорь Алексеевич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1975, 3, 4),
            Specialization = Specializations[2],
            ExperienceYears = 13
        },
        new Trainer
        {
            Id = 11,
            PassportNumber = "5212 222012",
            FullName = "Киселёва Ольга Андреевна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1984, 10, 23),
            Specialization = Specializations[4],
            ExperienceYears = 6
        },
    ];

    private static List<TrainingSession> BuildSessions()
    {
        var currentMonth = new DateTime(Now.Year, Now.Month, 1);
        var previousMonth = currentMonth.AddMonths(-1);
        var nextMonth = currentMonth.AddMonths(1);

        return
        [
            // Занятия в «Силовом зале» в текущем месяце — для проверки фильтрации по залу
            new TrainingSession
            {
                Id = 0,
                Client = Clients[5],
                Trainer = Trainers[0],
                StartsTraining = DateInMonth(currentMonth, 6, 10),
                EndsTraining = DateInMonth(currentMonth, 6, 11),
                HallName = Hall.Gym,
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 1,
                Client = Clients[6],
                Trainer = Trainers[1],
                StartsTraining = DateInMonth(currentMonth, 13, 12),
                EndsTraining = DateInMonth(currentMonth, 13, 13),
                HallName = Hall.Gym,
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 2,
                Client = Clients[7],
                Trainer = Trainers[2],
                StartsTraining = DateInMonth(currentMonth, 21, 18),
                EndsTraining = DateInMonth(currentMonth, 21, 19),
                HallName = Hall.Gym,
                IsTrial = true
            },

            // Занятия в «Силовом зале» в других месяцах — не должны попасть в выборку
            new TrainingSession
            {
                Id = 3,
                Client = Clients[8],
                Trainer = Trainers[0],
                StartsTraining = DateInMonth(previousMonth, 7, 11),
                EndsTraining = DateInMonth(previousMonth, 7, 12),
                HallName = Hall.Gym,
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 4,
                Client = Clients[9],
                Trainer = Trainers[0],
                StartsTraining = DateInMonth(nextMonth, 8, 15),
                EndsTraining = DateInMonth(nextMonth, 8, 16),
                HallName = Hall.Gym,
                IsTrial = false
            },

            // Идущее занятие в «Зале йоги» и завершившееся занятие на «Ринге» — для проверки доступности залов
            new TrainingSession
            {
                Id = 5,
                Client = Clients[8],
                Trainer = Trainers[3],
                StartsTraining = Now.AddMinutes(-40),
                EndsTraining = Now.AddMinutes(20),
                HallName = Hall.YogaRoom,
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 6,
                Client = Clients[0],
                Trainer = Trainers[4],
                StartsTraining = Now.AddMinutes(-90),
                EndsTraining = Now.AddMinutes(-15),
                HallName = Hall.Ring,
                IsTrial = false
            },

            // Остальные занятия — для статистики популярности тренеров
            new TrainingSession
            {
                Id = 7,
                Client = Clients[5],
                Trainer = Trainers[0],
                StartsTraining = DateInMonth(currentMonth, 4, 9),
                EndsTraining = DateInMonth(currentMonth, 4, 10),
                HallName = Hall.Pool,
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 8,
                Client = Clients[6],
                Trainer = Trainers[1],
                StartsTraining = DateInMonth(currentMonth, 9, 14),
                EndsTraining = DateInMonth(currentMonth, 9, 15),
                HallName = Hall.YogaRoom,
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 9,
                Client = Clients[7],
                Trainer = Trainers[2],
                StartsTraining = DateInMonth(previousMonth, 12, 16),
                EndsTraining = DateInMonth(previousMonth, 12, 17),
                HallName = Hall.DanceHall,
                IsTrial = true
            },
            new TrainingSession
            {
                Id = 10,
                Client = Clients[8],
                Trainer = Trainers[0],
                StartsTraining = DateInMonth(currentMonth, 18, 19),
                EndsTraining = DateInMonth(currentMonth, 18, 20),
                HallName = Hall.Pool,
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 11,
                Client = Clients[9],
                Trainer = Trainers[1],
                StartsTraining = DateInMonth(previousMonth, 23, 7),
                EndsTraining = DateInMonth(previousMonth, 23, 8),
                HallName = Hall.Ring,
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 12,
                Client = Clients[5],
                Trainer = Trainers[2],
                StartsTraining = DateInMonth(currentMonth, 25, 11),
                EndsTraining = DateInMonth(currentMonth, 25, 12),
                HallName = Hall.Pool,
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 13,
                Client = Clients[6],
                Trainer = Trainers[0],
                StartsTraining = DateInMonth(previousMonth, 15, 20),
                EndsTraining = DateInMonth(previousMonth, 15, 21),
                HallName = Hall.CardioZone,
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 14,
                Client = Clients[7],
                Trainer = Trainers[1],
                StartsTraining = DateInMonth(nextMonth, 5, 12),
                EndsTraining = DateInMonth(nextMonth, 5, 13),
                HallName = Hall.Pool,
                IsTrial = true
            },
            new TrainingSession
            {
                Id = 15,
                Client = Clients[8],
                Trainer = Trainers[2],
                StartsTraining = DateInMonth(nextMonth, 11, 17),
                EndsTraining = DateInMonth(nextMonth, 11, 18),
                HallName = Hall.Ring,
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 16,
                Client = Clients[9],
                Trainer = Trainers[3],
                StartsTraining = DateInMonth(currentMonth, 20, 8),
                EndsTraining = DateInMonth(currentMonth, 20, 9),
                HallName = Hall.CardioZone,
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 17,
                Client = Clients[5],
                Trainer = Trainers[0],
                StartsTraining = DateInMonth(currentMonth, 27, 13),
                EndsTraining = DateInMonth(currentMonth, 27, 14),
                HallName = Hall.DanceHall,
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 18,
                Client = Clients[6],
                Trainer = Trainers[1],
                StartsTraining = DateInMonth(currentMonth, 3, 10),
                EndsTraining = DateInMonth(currentMonth, 3, 11),
                HallName = Hall.YogaRoom,
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 19,
                Client = Clients[7],
                Trainer = Trainers[3],
                StartsTraining = DateInMonth(previousMonth, 19, 15),
                EndsTraining = DateInMonth(previousMonth, 19, 16),
                HallName = Hall.Gym,
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 20,
                Client = Clients[8],
                Trainer = Trainers[4],
                StartsTraining = DateInMonth(nextMonth, 14, 18),
                EndsTraining = DateInMonth(nextMonth, 14, 19),
                HallName = Hall.YogaRoom,
                IsTrial = true
            },
            new TrainingSession
            {
                Id = 21,
                Client = Clients[9],
                Trainer = Trainers[5],
                StartsTraining = DateInMonth(currentMonth, 22, 7),
                EndsTraining = DateInMonth(currentMonth, 22, 8),
                HallName = Hall.Pool,
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 22,
                Client = Clients[5],
                Trainer = Trainers[6],
                StartsTraining = DateInMonth(previousMonth, 9, 16),
                EndsTraining = DateInMonth(previousMonth, 9, 17),
                HallName = Hall.DanceHall,
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 23,
                Client = Clients[6],
                Trainer = Trainers[7],
                StartsTraining = DateInMonth(currentMonth, 15, 20),
                EndsTraining = DateInMonth(currentMonth, 15, 21),
                HallName = Hall.CardioZone,
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 24,
                Client = Clients[7],
                Trainer = Trainers[8],
                StartsTraining = DateInMonth(nextMonth, 25, 9),
                EndsTraining = DateInMonth(nextMonth, 25, 10),
                HallName = Hall.Gym,
                IsTrial = true
            },
            new TrainingSession
            {
                Id = 25,
                Client = Clients[8],
                Trainer = Trainers[9],
                StartsTraining = DateInMonth(previousMonth, 28, 18),
                EndsTraining = DateInMonth(previousMonth, 28, 19),
                HallName = Hall.Pool,
                IsTrial = false
            },
        ];
    }

    private static DateTime DateInMonth(DateTime month, int day, int hour) =>
        new(month.Year, month.Month, day, hour, 0, 0);
}