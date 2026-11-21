using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Tests;

/// <summary>
/// Набор тестовых данных для проверки LINQ-запросов.
/// </summary>
public class QueriesTestFixture
{
    /// <summary>
    /// Фиксированная дата и время для выполнения тестов.
    /// </summary>
    public DateTime TestDateTime { get; } =
        new(2026, 9, 23, 12, 0, 0);

    /// <summary>
    /// Список специализаций тренеров.
    /// </summary>
    public List<Specialization> Specializations { get; } =
    [
        new Specialization
        {
            Id = 1,
            Name = "Персональный тренинг",
            Description = "Индивидуальные тренировки с клиентами."
        },
        new Specialization
        {
            Id = 2,
            Name = "Функциональный тренинг",
            Description = "Тренировки для развития силы, выносливости и координации."
        },
        new Specialization
        {
            Id = 3,
            Name = "Силовой тренинг",
            Description = "Тренировки с использованием силовых упражнений."
        },
        new Specialization
        {
            Id = 4,
            Name = "Кардио",
            Description = "Тренировки для развития выносливости."
        },
        new Specialization
        {
            Id = 5,
            Name = "Йога",
            Description = "Индивидуальные занятия йогой."
        },
        new Specialization
        {
            Id = 6,
            Name = "Пилатес",
            Description = "Тренировки для укрепления мышц и улучшения гибкости."
        },
        new Specialization
        {
            Id = 7,
            Name = "Стретчинг",
            Description = "Занятия для развития гибкости."
        },
        new Specialization
        {
            Id = 8,
            Name = "Кроссфит",
            Description = "Высокоинтенсивные функциональные тренировки."
        },
        new Specialization
        {
            Id = 9,
            Name = "Бокс",
            Description = "Индивидуальные тренировки по боксу."
        },
        new Specialization
        {
            Id = 10,
            Name = "Лечебная физкультура",
            Description = "Тренировки с учётом индивидуальных особенностей клиента."
        }
    ];

    /// <summary>
    /// Список залов фитнес-клуба.
    /// </summary>
    public List<Hall> Halls { get; } =
    [
        new Hall
        {
            Id = 1,
            Name = "Тренажёрный зал №1",
            Capacity = 20
        },
        new Hall
        {
            Id = 2,
            Name = "Тренажёрный зал №2",
            Capacity = 15
        },
        new Hall
        {
            Id = 3,
            Name = "Зал функционального тренинга",
            Capacity = 25
        },
        new Hall
        {
            Id = 4,
            Name = "Кардио-зал",
            Capacity = 20
        },
        new Hall
        {
            Id = 5,
            Name = "Зал йоги",
            Capacity = 15
        },
        new Hall
        {
            Id = 6,
            Name = "Зал пилатеса",
            Capacity = 15
        },
        new Hall
        {
            Id = 7,
            Name = "Зал стретчинга",
            Capacity = 12
        },
        new Hall
        {
            Id = 8,
            Name = "Кроссфит-зал",
            Capacity = 20
        },
        new Hall
        {
            Id = 9,
            Name = "Боксёрский зал",
            Capacity = 10
        },
        new Hall
        {
            Id = 10,
            Name = "Малый тренировочный зал",
            Capacity = 10
        }
    ];

    /// <summary>
    /// Список клиентов фитнес-клуба.
    /// </summary>
    public List<Client> Clients { get; } =
    [
        new Client
        {
            PassportNumber = "1234 100001",
            FullName = "Иванов Иван Сергеевич",
            Gender = Gender.Male,
            BirthDate = new DateTime(1995, 3, 15),
            Phone = "+79990000001",
            SubscriptionStartDate = new DateTime(2026, 8, 1),
            SubscriptionEndDate = new DateTime(2026, 10, 1)
        },
        new Client
        {
            PassportNumber = "1234 100002",
            FullName = "Кузнецов Алексей Викторович",
            Gender = Gender.Male,
            BirthDate = new DateTime(1992, 7, 21),
            Phone = "+79990000002",
            SubscriptionStartDate = new DateTime(2025, 8, 1),
            SubscriptionEndDate = new DateTime(2026, 2, 15)
        },
        new Client
        {
            PassportNumber = "1234 100003",
            FullName = "Морозов Максим Николаевич",
            Gender = Gender.Male,
            BirthDate = new DateTime(1989, 11, 10),
            Phone = "+79990000003",
            SubscriptionStartDate = new DateTime(2025, 9, 1),
            SubscriptionEndDate = new DateTime(2026, 4, 20)
        },
        new Client
        {
            PassportNumber = "1234 100004",
            FullName = "Николаев Артём Дмитриевич",
            Gender = Gender.Male,
            BirthDate = new DateTime(1998, 1, 25),
            Phone = "+79990000004",
            SubscriptionStartDate = new DateTime(2025, 10, 1),
            SubscriptionEndDate = new DateTime(2026, 5, 10)
        },
        new Client
        {
            PassportNumber = "1234 100005",
            FullName = "Попов Дмитрий Олегович",
            Gender = Gender.Male,
            BirthDate = new DateTime(1994, 6, 12),
            Phone = "+79990000005",
            SubscriptionStartDate = new DateTime(2025, 11, 1),
            SubscriptionEndDate = new DateTime(2026, 6, 5)
        },
        new Client
        {
            PassportNumber = "1234 100006",
            FullName = "Сидорова Анна Сергеевна",
            Gender = Gender.Female,
            BirthDate = new DateTime(1996, 4, 18),
            Phone = "+79990000006",
            SubscriptionStartDate = new DateTime(2026, 1, 1),
            SubscriptionEndDate = new DateTime(2026, 7, 30)
        },
        new Client
        {
            PassportNumber = "1234 100007",
            FullName = "Петрова Мария Андреевна",
            Gender = Gender.Female,
            BirthDate = new DateTime(1997, 9, 5),
            Phone = "+79990000007",
            SubscriptionStartDate = new DateTime(2026, 8, 10),
            SubscriptionEndDate = new DateTime(2026, 11, 10)
        },
        new Client
        {
            PassportNumber = "1234 100008",
            FullName = "Соколова Елена Викторовна",
            Gender = Gender.Female,
            BirthDate = new DateTime(1993, 12, 2),
            Phone = "+79990000008",
            SubscriptionStartDate = new DateTime(2026, 7, 15),
            SubscriptionEndDate = new DateTime(2026, 10, 15)
        },
        new Client
        {
            PassportNumber = "1234 100009",
            FullName = "Фёдоров Михаил Александрович",
            Gender = Gender.Male,
            BirthDate = new DateTime(1990, 5, 27),
            Phone = "+79990000009",
            SubscriptionStartDate = new DateTime(2026, 9, 1),
            SubscriptionEndDate = new DateTime(2026, 12, 1)
        },
        new Client
        {
            PassportNumber = "1234 100010",
            FullName = "Волкова Ольга Дмитриевна",
            Gender = Gender.Female,
            BirthDate = new DateTime(1999, 2, 14),
            Phone = "+79990000010",
            SubscriptionStartDate = new DateTime(2026, 8, 20),
            SubscriptionEndDate = new DateTime(2026, 12, 20)
        }
    ];

    /// <summary>
    /// Список тренеров фитнес-клуба.
    /// </summary>
    public List<Trainer> Trainers { get; }

    /// <summary>
    /// Список записей клиентов на персональные тренировки.
    /// </summary>
    public List<Training> Trainings { get; }

    /// <summary>
    /// Создаёт набор тестовых тренеров и тренировок.
    /// </summary>
    public QueriesTestFixture()
    {
        Trainers =
        [
            new Trainer
            {
                PassportNumber = "4321 200001",
                FullName = "Алексеев Андрей Сергеевич",
                Gender = Gender.Male,
                BirthDate = new DateTime(1988, 2, 10),
                Specialization = Specializations[0],
                WorkExperience = 8
            },
            new Trainer
            {
                PassportNumber = "4321 200002",
                FullName = "Васильев Дмитрий Олегович",
                Gender = Gender.Male,
                BirthDate = new DateTime(1985, 6, 20),
                Specialization = Specializations[1],
                WorkExperience = 8
            },
            new Trainer
            {
                PassportNumber = "4321 200003",
                FullName = "Жуков Александр Михайлович",
                Gender = Gender.Male,
                BirthDate = new DateTime(1982, 9, 14),
                Specialization = Specializations[2],
                WorkExperience = 12
            },
            new Trainer
            {
                PassportNumber = "4321 200004",
                FullName = "Захарова Екатерина Романовна",
                Gender = Gender.Female,
                BirthDate = new DateTime(1990, 4, 3),
                Specialization = Specializations[3],
                WorkExperience = 6
            },
            new Trainer
            {
                PassportNumber = "4321 200005",
                FullName = "Ильин Роман Евгеньевич",
                Gender = Gender.Male,
                BirthDate = new DateTime(1987, 12, 8),
                Specialization = Specializations[4],
                WorkExperience = 9
            },
            new Trainer
            {
                PassportNumber = "4321 200006",
                FullName = "Кузнецов Алексей Николаевич",
                Gender = Gender.Male,
                BirthDate = new DateTime(1984, 5, 17),
                Specialization = Specializations[5],
                WorkExperience = 5
            },
            new Trainer
            {
                PassportNumber = "4321 200007",
                FullName = "Морозов Максим Викторович",
                Gender = Gender.Male,
                BirthDate = new DateTime(1986, 8, 22),
                Specialization = Specializations[6],
                WorkExperience = 10
            },
            new Trainer
            {
                PassportNumber = "4321 200008",
                FullName = "Смирнов Сергей Андреевич",
                Gender = Gender.Male,
                BirthDate = new DateTime(1989, 3, 11),
                Specialization = Specializations[7],
                WorkExperience = 7
            },
            new Trainer
            {
                PassportNumber = "4321 200009",
                FullName = "Орлов Николай Дмитриевич",
                Gender = Gender.Male,
                BirthDate = new DateTime(1991, 7, 19),
                Specialization = Specializations[8],
                WorkExperience = 3
            },
            new Trainer
            {
                PassportNumber = "4321 200010",
                FullName = "Павлова Анастасия Игоревна",
                Gender = Gender.Female,
                BirthDate = new DateTime(1993, 10, 27),
                Specialization = Specializations[9],
                WorkExperience = 2
            }
        ];

        Trainings =
        [
            new Training
            {
                Id = 1,
                Client = Clients[0],
                Trainer = Trainers[0],
                Hall = Halls[0],
                StartTime = new DateTime(2026, 9, 5, 10, 0, 0),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 2,
                Client = Clients[1],
                Trainer = Trainers[0],
                Hall = Halls[2],
                StartTime = TestDateTime.AddDays(-40),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 3,
                Client = Clients[2],
                Trainer = Trainers[0],
                Hall = Halls[3],
                StartTime = TestDateTime.AddDays(-50),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 4,
                Client = Clients[3],
                Trainer = Trainers[0],
                Hall = Halls[4],
                StartTime = TestDateTime.AddDays(-60),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 5,
                Client = Clients[4],
                Trainer = Trainers[1],
                Hall = Halls[0],
                StartTime = new DateTime(2026, 9, 10, 10, 0, 0),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 6,
                Client = Clients[5],
                Trainer = Trainers[1],
                Hall = Halls[2],
                StartTime = TestDateTime.AddDays(-45),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 7,
                Client = Clients[6],
                Trainer = Trainers[1],
                Hall = Halls[3],
                StartTime = TestDateTime.AddDays(-55),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 8,
                Client = Clients[7],
                Trainer = Trainers[1],
                Hall = Halls[4],
                StartTime = TestDateTime.AddDays(-65),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 9,
                Client = Clients[8],
                Trainer = Trainers[1],
                Hall = Halls[5],
                StartTime = TestDateTime.AddDays(-75),
                DurationMinutes = 60,
                IsTrial = true
            },

            new Training
            {
                Id = 10,
                Client = Clients[9],
                Trainer = Trainers[2],
                Hall = Halls[0],
                StartTime = new DateTime(2026, 9, 15, 10, 0, 0),
                DurationMinutes = 60,
                IsTrial = false
            },

            // 11
            new Training
            {
                Id = 11,
                Client = Clients[0],
                Trainer = Trainers[2],
                Hall = Halls[6],
                StartTime = TestDateTime.AddDays(-85),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 12,
                Client = Clients[1],
                Trainer = Trainers[2],
                Hall = Halls[7],
                StartTime = TestDateTime.AddDays(-90),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 13,
                Client = Clients[2],
                Trainer = Trainers[3],
                Hall = Halls[0],
                StartTime = new DateTime(2026, 9, 23, 11, 30, 0),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 14,
                Client = Clients[3],
                Trainer = Trainers[3],
                Hall = Halls[8],
                StartTime = TestDateTime.AddDays(-95),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 15,
                Client = Clients[4],
                Trainer = Trainers[3],
                Hall = Halls[9],
                StartTime = TestDateTime.AddDays(-100),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 16,
                Client = Clients[5],
                Trainer = Trainers[4],
                Hall = Halls[1],
                StartTime = TestDateTime.AddDays(-105),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 17,
                Client = Clients[6],
                Trainer = Trainers[4],
                Hall = Halls[2],
                StartTime = TestDateTime.AddDays(-110),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 18,
                Client = Clients[7],
                Trainer = Trainers[4],
                Hall = Halls[3],
                StartTime = TestDateTime.AddDays(-115),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 19,
                Client = Clients[8],
                Trainer = Trainers[5],
                Hall = Halls[4],
                StartTime = TestDateTime.AddDays(-120),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 20,
                Client = Clients[9],
                Trainer = Trainers[5],
                Hall = Halls[5],
                StartTime = TestDateTime.AddDays(-125),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 21,
                Client = Clients[0],
                Trainer = Trainers[6],
                Hall = Halls[6],
                StartTime = TestDateTime.AddDays(-130),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 22,
                Client = Clients[1],
                Trainer = Trainers[6],
                Hall = Halls[7],
                StartTime = TestDateTime.AddDays(-135),
                DurationMinutes = 60,
                IsTrial = false
            },

            // 23
            new Training
            {
                Id = 23,
                Client = Clients[2],
                Trainer = Trainers[7],
                Hall = Halls[8],
                StartTime = TestDateTime.AddDays(-140),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 24,
                Client = Clients[3],
                Trainer = Trainers[8],
                Hall = Halls[9],
                StartTime = TestDateTime.AddDays(-145),
                DurationMinutes = 60,
                IsTrial = false
            },

            new Training
            {
                Id = 25,
                Client = Clients[4],
                Trainer = Trainers[9],
                Hall = Halls[1],
                StartTime = TestDateTime.AddDays(-150),
                DurationMinutes = 60,
                IsTrial = true
            }
        ];
    }
}