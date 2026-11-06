using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Domain.Data;

/// <summary>
/// Подготавливает данные фитнес-клуба для тестов.
/// </summary>
public class FitnessClubDataSeeder
{
    /// <summary>
    /// Текущий момент времени.
    /// </summary>
    public DateTime Now { get; }

    /// <summary>
    /// Текущая дата.
    /// </summary>
    public DateOnly Today { get; }

    /// <summary>
    /// Специализации тренеров.
    /// </summary>
    public List<Specialization> Specializations { get; }

    /// <summary>
    /// Залы фитнес-клуба.
    /// </summary>
    public List<Gym> Gyms { get; }

    /// <summary>
    /// Клиенты фитнес-клуба.
    /// </summary>
    public List<Client> Clients { get; } = [];

    /// <summary>
    /// Тренеры фитнес-клуба.
    /// </summary>
    public List<Trainer> Trainers { get; } = [];

    /// <summary>
    /// Записи клиентов на занятия.
    /// </summary>
    public List<TrainingSession> TrainingSessions { get; } = [];

    /// <summary>
    /// Создаёт набор данных фитнес-клуба.
    /// </summary>
    public FitnessClubDataSeeder(DateTime referenceTime)
    {
        Now = referenceTime;
        Today = DateOnly.FromDateTime(Now);

        Specializations = CreateSpecializations();
        Gyms = CreateGyms();

        Clients.AddRange(CreateClients(Today));
        Trainers.AddRange(CreateTrainers(Specializations));
        TrainingSessions.AddRange(CreateTrainingSessions(Now, Clients, Trainers, Gyms));
    }

    /// <summary>
    /// Создаёт список специализаций тренеров.
    /// </summary>
    /// <returns>Список специализаций.</returns>
    private static List<Specialization> CreateSpecializations() =>
    [
        new() { Id = 1, Name = "Силовой тренинг" },
        new() { Id = 2, Name = "Йога" },
        new() { Id = 3, Name = "Функциональный тренинг" },
        new() { Id = 4, Name = "Кардиотренировки" },
        new() { Id = 5, Name = "Пилатес" },
        new() { Id = 6, Name = "Стретчинг" },
        new() { Id = 7, Name = "Кроссфит" },
        new() { Id = 8, Name = "Бокс" },
        new() { Id = 9, Name = "Реабилитационный фитнес" },
        new() { Id = 10, Name = "Тяжёлая атлетика" }
    ];

    /// <summary>
    /// Создаёт список залов фитнес-клуба.
    /// </summary>
    /// <returns>Список залов.</returns>
    private static List<Gym> CreateGyms() =>
    [
        new() { Id = 1, Name = "Силовой зал" },
        new() { Id = 2, Name = "Зал йоги" },
        new() { Id = 3, Name = "Зал функционального тренинга" },
        new() { Id = 4, Name = "Зал для кардио" },
        new() { Id = 5, Name = "Зал пилатеса" },
        new() { Id = 6, Name = "Зал растяжки" },
        new() { Id = 7, Name = "Зал для кроссфита" },
        new() { Id = 8, Name = "Зал для бокса" },
        new() { Id = 9, Name = "Зал для реабилитации" },
        new() { Id = 10, Name = "Зал тяжёлой атлетики" }
    ];

    /// <summary>
    /// Создаёт список клиентов фитнес-клуба.
    /// </summary>
    /// <param name="today">Дата, относительно которой формируются сроки действия абонементов.</param>
    /// <returns>Список клиентов.</returns>
    private static List<Client> CreateClients(DateOnly today) =>
    [
        new Client
        {
            Id = 5,
            PassportNumber = "4505 100005",
            FullName = "Денисов Павел Андреевич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1997, 9, 9),
            PhoneNumber = "+7 900 100-00-05",
            SubscriptionStartDate = today.AddDays(-75),
            SubscriptionEndDate = today.AddDays(-5)
        },
        new Client
        {
            Id = 1,
            PassportNumber = "4501 100001",
            FullName = "Андреев Алексей Сергеевич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1995, 4, 12),
            PhoneNumber = "+7 900 100-00-01",
            SubscriptionStartDate = today.AddDays(-90),
            SubscriptionEndDate = today.AddDays(-30)
        },
        new Client
        {
            Id = 4,
            PassportNumber = "4504 100004",
            FullName = "Громова Елена Павловна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1988, 2, 17),
            PhoneNumber = "+7 900 100-00-04",
            SubscriptionStartDate = today.AddDays(-180),
            SubscriptionEndDate = today.AddDays(-45)
        },
        new Client
        {
            Id = 2,
            PassportNumber = "4502 100002",
            FullName = "Беляева Мария Олеговна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1999, 7, 5),
            PhoneNumber = "+7 900 100-00-02",
            SubscriptionStartDate = today.AddDays(-60),
            SubscriptionEndDate = today.AddDays(-10)
        },
        new Client
        {
            Id = 3,
            PassportNumber = "4503 100003",
            FullName = "Васильев Дмитрий Игоревич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1992, 11, 21),
            PhoneNumber = "+7 900 100-00-03",
            SubscriptionStartDate = today.AddDays(-120),
            SubscriptionEndDate = today.AddDays(-1)
        },

        new Client
        {
            Id = 6,
            PassportNumber = "4506 100006",
            FullName = "Егорова Ольга Викторовна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1994, 6, 25),
            PhoneNumber = "+7 900 100-00-06",
            SubscriptionStartDate = today.AddDays(-40),
            SubscriptionEndDate = today.AddDays(50)
        },
        new Client
        {
            Id = 7,
            PassportNumber = "4507 100007",
            FullName = "Жуков Игорь Романович",
            Gender = Gender.Male,
            BirthDate = new DateOnly(2000, 1, 13),
            PhoneNumber = "+7 900 100-00-07",
            SubscriptionStartDate = today.AddDays(-20),
            SubscriptionEndDate = today.AddDays(70)
        },
        new Client
        {
            Id = 8,
            PassportNumber = "4508 100008",
            FullName = "Зайцева Анна Максимовна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(2001, 8, 30),
            PhoneNumber = "+7 900 100-00-08",
            SubscriptionStartDate = today.AddDays(-10),
            SubscriptionEndDate = today.AddDays(80)
        },
        new Client
        {
            Id = 9,
            PassportNumber = "4509 100009",
            FullName = "Иванов Сергей Петрович",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1986, 12, 3),
            PhoneNumber = "+7 900 100-00-09",
            SubscriptionStartDate = today.AddDays(-100),
            SubscriptionEndDate = today.AddDays(20)
        },
        new Client
        {
            Id = 10,
            PassportNumber = "4510 100010",
            FullName = "Козлова Мария Дмитриевна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1998, 3, 28),
            PhoneNumber = "+7 900 100-00-10",
            SubscriptionStartDate = today.AddDays(-1),
            SubscriptionEndDate = today.AddDays(364)
        }
    ];

    /// <summary>
    /// Создаёт список тренеров фитнес-клуба.
    /// </summary>
    /// <param name="specializations">Список специализаций тренеров.</param>
    /// <returns>Список тренеров.</returns>
    private static List<Trainer> CreateTrainers(List<Specialization> specializations) =>
    [
        new Trainer
        {
            Id = 11,
            PassportNumber = "4601 200001",
            FullName = "Соколов Артём Викторович",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1991, 6, 15),
            Specialization = specializations[0],
            WorkExperience = 12
        },
        new Trainer
        {
            Id = 12,
            PassportNumber = "4602 200002",
            FullName = "Крылова Наталья Сергеевна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1989, 3, 24),
            Specialization = specializations[1],
            WorkExperience = 9
        },
        new Trainer
        {
            Id = 13,
            PassportNumber = "4603 200003",
            FullName = "Попов Максим Ильич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1996, 11, 2),
            Specialization = specializations[2],
            WorkExperience = 7
        },
        new Trainer
        {
            Id = 14,
            PassportNumber = "4604 200004",
            FullName = "Лебедева Дарья Александровна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1987, 7, 19),
            Specialization = specializations[3],
            WorkExperience = 6
        },
        new Trainer
        {
            Id = 15,
            PassportNumber = "4605 200005",
            FullName = "Новиков Артур Валерьевич",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1983, 1, 30),
            Specialization = specializations[4],
            WorkExperience = 5
        },
        new Trainer
        {
            Id = 16,
            PassportNumber = "4606 200006",
            FullName = "Фёдорова Ксения Павловна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1995, 9, 8),
            Specialization = specializations[5],
            WorkExperience = 4
        },
        new Trainer
        {
            Id = 17,
            PassportNumber = "4607 200007",
            FullName = "Морозов Георгий Львович",
            Gender = Gender.Male,
            BirthDate = new DateOnly(1993, 12, 11),
            Specialization = specializations[6],
            WorkExperience = 3
        },
        new Trainer
        {
            Id = 18,
            PassportNumber = "4608 200008",
            FullName = "Волкова Ирина Дмитриевна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1998, 5, 27),
            Specialization = specializations[7],
            WorkExperience = 2
        },
        new Trainer
        {
            Id = 19,
            PassportNumber = "4609 200009",
            FullName = "Зайцев Роман Олегович",
            Gender = Gender.Male,
            BirthDate = new DateOnly(2000, 2, 14),
            Specialization = specializations[8],
            WorkExperience = 1
        },
        new Trainer
        {
            Id = 20,
            PassportNumber = "4610 200010",
            FullName = "Павлова Светлана Юрьевна",
            Gender = Gender.Female,
            BirthDate = new DateOnly(1992, 8, 3),
            Specialization = specializations[9],
            WorkExperience = 5
        }
    ];

    /// <summary>
    /// Создаёт список записей клиентов на занятия.
    /// </summary>
    /// <param name="now">Текущий момент времени.</param>
    /// <param name="clients">Список клиентов.</param>
    /// <param name="trainers">Список тренеров.</param>
    /// <param name="gyms">Список залов.</param>
    /// <returns>Список записей на занятия.</returns>
    private static List<TrainingSession> CreateTrainingSessions(
        DateTime now,
        List<Client> clients,
        List<Trainer> trainers,
        List<Gym> gyms)
    {
        var currentMonth = new DateTime(now.Year, now.Month, 1);
        var previousMonth = currentMonth.AddMonths(-1);
        var nextMonth = currentMonth.AddMonths(1);

        static DateTime At(DateTime month, int day, int hour) => new(month.Year, month.Month, day, hour, 0, 0);
        static DateTime End(DateTime start, int hours) => start.AddHours(hours);

        return
        [
            new TrainingSession
            {
                Id = 1,
                Client = clients[5],
                Trainer = trainers[1],
                StartsAt = At(currentMonth, 5, 10),
                EndsAt = End(At(currentMonth, 5, 10), 1),
                Gym = gyms[0],
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 2,
                Client = clients[6],
                Trainer = trainers[0],
                StartsAt = At(currentMonth, 12, 12),
                EndsAt = End(At(currentMonth, 12, 12), 2),
                Gym = gyms[0],
                IsTrial = true
            },
            new TrainingSession
            {
                Id = 3,
                Client = clients[7],
                Trainer = trainers[0],
                StartsAt = At(currentMonth, 20, 18),
                EndsAt = End(At(currentMonth, 20, 18), 1),
                Gym = gyms[0],
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 4,
                Client = clients[8],
                Trainer = trainers[0],
                StartsAt = At(previousMonth, 8, 11),
                EndsAt = End(At(previousMonth, 8, 11), 2),
                Gym = gyms[0],
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 5,
                Client = clients[9],
                Trainer = trainers[0],
                StartsAt = At(nextMonth, 10, 15),
                EndsAt = End(At(nextMonth, 10, 15), 1),
                Gym = gyms[0],
                IsTrial = true
            },
            new TrainingSession
            {
                Id = 6,
                Client = clients[5],
                Trainer = trainers[0],
                StartsAt = now.AddMinutes(-30),
                EndsAt = now.AddMinutes(30),
                Gym = gyms[1],
                IsTrial = false
            },

            new TrainingSession
            {
                Id = 7,
                Client = clients[6],
                Trainer = trainers[0],
                StartsAt = At(currentMonth, 6, 11),
                EndsAt = End(At(currentMonth, 6, 11), 1),
                Gym = gyms[1],
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 8,
                Client = clients[7],
                Trainer = trainers[1],
                StartsAt = At(currentMonth, 9, 13),
                EndsAt = End(At(currentMonth, 9, 13), 2),
                Gym = gyms[2],
                IsTrial = true
            },
            new TrainingSession
            {
                Id = 9,
                Client = clients[8],
                Trainer = trainers[1],
                StartsAt = At(currentMonth, 14, 17),
                EndsAt = End(At(currentMonth, 14, 17), 1),
                Gym = gyms[3],
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 10,
                Client = clients[9],
                Trainer = trainers[1],
                StartsAt = At(previousMonth, 16, 18),
                EndsAt = End(At(previousMonth, 16, 18), 2),
                Gym = gyms[4],
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 11,
                Client = clients[5],
                Trainer = trainers[1],
                StartsAt = At(nextMonth, 18, 19),
                EndsAt = End(At(nextMonth, 18, 19), 1),
                Gym = gyms[5],
                IsTrial = false
            },

            new TrainingSession
            {
                Id = 12,
                Client = clients[6],
                Trainer = trainers[2],
                StartsAt = At(currentMonth, 8, 10),
                EndsAt = End(At(currentMonth, 8, 10), 2),
                Gym = gyms[2],
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 13,
                Client = clients[7],
                Trainer = trainers[2],
                StartsAt = At(currentMonth, 15, 16),
                EndsAt = End(At(currentMonth, 15, 16), 1),
                Gym = gyms[3],
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 14,
                Client = clients[8],
                Trainer = trainers[2],
                StartsAt = At(previousMonth, 19, 12),
                EndsAt = End(At(previousMonth, 19, 12), 2),
                Gym = gyms[4],
                IsTrial = true
            },
            new TrainingSession
            {
                Id = 15,
                Client = clients[9],
                Trainer = trainers[2],
                StartsAt = At(nextMonth, 21, 20),
                EndsAt = End(At(nextMonth, 21, 20), 1),
                Gym = gyms[5],
                IsTrial = false
            },

            new TrainingSession
            {
                Id = 16,
                Client = clients[5],
                Trainer = trainers[3],
                StartsAt = At(currentMonth, 11, 14),
                EndsAt = End(At(currentMonth, 11, 14), 2),
                Gym = gyms[3],
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 17,
                Client = clients[6],
                Trainer = trainers[3],
                StartsAt = At(currentMonth, 17, 19),
                EndsAt = End(At(currentMonth, 17, 19), 1),
                Gym = gyms[4],
                IsTrial = true
            },
            new TrainingSession
            {
                Id = 18,
                Client = clients[7],
                Trainer = trainers[3],
                StartsAt = At(previousMonth, 22, 10),
                EndsAt = End(At(previousMonth, 22, 10), 2),
                Gym = gyms[5],
                IsTrial = false
            },

            new TrainingSession
            {
                Id = 19,
                Client = clients[8],
                Trainer = trainers[4],
                StartsAt = At(currentMonth, 13, 8),
                EndsAt = End(At(currentMonth, 13, 8), 1),
                Gym = gyms[4],
                IsTrial = false
            },
            new TrainingSession
            {
                Id = 20,
                Client = clients[9],
                Trainer = trainers[4],
                StartsAt = At(nextMonth, 24, 18),
                EndsAt = End(At(nextMonth, 24, 18), 2),
                Gym = gyms[5],
                IsTrial = true
            }
        ];
    }
}
