using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Domain.Seeds;

/// <summary>
/// Начальные данные для фитнес-клуба
/// </summary>
public class SeedData
{
    /// <summary>
    /// Список специализаций
    /// </summary>
    public List<Specialization> Specializations { get; }
    /// <summary>
    /// Список клиентов
    /// </summary>
    public List<Client> Clients { get; }
    /// <summary>
    /// Список тренеров
    /// </summary>
    public List<Trainer> Trainers { get; }
    /// <summary>
    /// Список залов для тренировок
    /// </summary>
    public List<Hall> Halls { get; }
    /// <summary>
    /// Список броней на тренировки
    /// </summary>
    public List<Booking> Bookings { get; }
    public SeedData()
    {
        Specializations =
        [
            new Specialization { Name = "Йога" },
            new Specialization { Name = "Пилатес" },
            new Specialization { Name = "Силовые тренировки" },
            new Specialization { Name = "Кардиотренировки" },
            new Specialization { Name = "Функциональные тренировки" },
            new Specialization { Name = "Группа здоровья" },
            new Specialization { Name = "Кросс-фитнес" },
            new Specialization { Name = "Фитнес для начинающих" },
            new Specialization { Name = "Боевые искусства" },
            new Specialization { Name = "Плавание" }
        ];
        Clients =
        [
            new Client
            {
                PassportNumber = "2222 222222",
                FullName = "Петрова Мария Сергеевна",
                Gender = Gender.Female,
                BirthDate = new DateOnly(1995, 3, 20),
                PhoneNumber = "+7 900 111-22-33",
                SubscriptionStart = new DateOnly(2026, 1, 1),
                SubscriptionEnd = new DateOnly(2026, 12, 31)
            },
            new Client
            {
                PassportNumber = "3333 333333",
                FullName = "Сидоров Алексей Иванович",
                Gender = Gender.Male,
                BirthDate = new DateOnly(1988, 7, 15),
                PhoneNumber = "+7 900 222-33-44",
                SubscriptionStart = new DateOnly(2026, 3, 19),
                SubscriptionEnd = new DateOnly(2026, 9, 19)
            },
            new Client
            {
                PassportNumber = "4444 444444",
                FullName = "Кузнецова Елена Владимировна",
                Gender = Gender.Female,
                BirthDate = new DateOnly(1992, 11, 5),
                PhoneNumber = "+7 900 333-44-55",
                SubscriptionStart = new DateOnly(2025, 9, 11),
                SubscriptionEnd = new DateOnly(2026, 1, 1)
            },
            new Client
            {
                PassportNumber = "5555 555555",
                FullName = "Иванов Иван Иванович",
                Gender = Gender.Male,
                BirthDate = new DateOnly(1985, 12, 10),
                PhoneNumber = "+7 900 444-55-66",
                SubscriptionStart = new DateOnly(2026, 1, 1),
                SubscriptionEnd = new DateOnly(2027, 1, 1)
            },
            new Client
            {
                PassportNumber = "6666 666666",
                FullName = "Смирнова Анна Пампамовна",
                Gender = Gender.Female,
                BirthDate = new DateOnly(1990, 5, 25),
                PhoneNumber = "+7 900 555-66-77",
                SubscriptionStart = new DateOnly(2025, 1, 1),
                SubscriptionEnd = new DateOnly(2025, 6, 30)
            },
            new Client
            {
                PassportNumber = "7777 777777",
                FullName = "Козлов Дмитрий Сергеевич",
                Gender = Gender.Male,
                BirthDate = new DateOnly(2010, 8, 30),
                PhoneNumber = "+7 900 666-77-88",
                SubscriptionStart = new DateOnly(2026, 6, 1),
                SubscriptionEnd = new DateOnly(2026, 12, 31)
            },
            new Client
            {
                PassportNumber = "8888 888888",
                FullName = "Васильева Ольга Николаевна",
                Gender = Gender.Female,
                BirthDate = new DateOnly(1968, 2, 14),
                PhoneNumber = "+7 900 777-88-99",
                SubscriptionStart = new DateOnly(2026, 5, 29),
                SubscriptionEnd = new DateOnly(2026, 12, 21)
            },
            new Client
            {
                PassportNumber = "9999 999999",
                FullName = "Морозов Сергей Владимирович",
                Gender = Gender.Male,
                BirthDate = new DateOnly(1975, 9, 3),
                PhoneNumber = "+7 900 888-99-00",
                SubscriptionStart = new DateOnly(2026, 10, 13),
                SubscriptionEnd = new DateOnly(2029, 4, 5)
            },
            new Client
            {
                PassportNumber = "1010 101010",
                FullName = "Криштиану Роналду Алексеевич",
                Gender = Gender.Male,
                BirthDate = new DateOnly(1985, 2, 14),
                PhoneNumber = "+7 900 999-00-11",
                SubscriptionStart = new DateOnly(2020, 1, 1),
                SubscriptionEnd = new DateOnly(2026, 10, 22)
            },
            new Client
            {
                PassportNumber = "1111 111111",
                FullName = "Месси Лионель Андреевич",
                Gender = Gender.Male,
                BirthDate = new DateOnly(1987, 6, 24),
                PhoneNumber = "+7 900 000-11-22",
                SubscriptionStart = new DateOnly(2024, 8, 13),
                SubscriptionEnd = new DateOnly(2026, 11, 9)
            },
            new Client
            {
                PassportNumber = "1212 121212",
                FullName = "Бурдюк Артем Сергеевич",
                Gender = Gender.Male,
                BirthDate = new DateOnly(1992, 2, 5),
                PhoneNumber = "+7 900 111-22-33",
                SubscriptionStart = new DateOnly(2025, 9, 15),
                SubscriptionEnd = new DateOnly(2025, 9, 16)
            },
            new Client
            {
                PassportNumber = "1313 131313",
                FullName = "А Анастасия Владимировна",
                Gender = Gender.Female,
                BirthDate = new DateOnly(1995, 12, 10),
                PhoneNumber = "+7 900 222-33-44",
                SubscriptionStart = new DateOnly(2024, 9, 15),
                SubscriptionEnd = new DateOnly(2025, 3, 27)
            }
        ];
        Trainers =
        [
            new Trainer
            {
                PassportNumber = "1234 567890",
                FullName = "Золотов Иван Сергеевич",
                Specialization = Specializations[0],
                Experience = 5,
                Gender = Gender.Male,
                BirthDate = new DateOnly(1985, 12, 10)
            },
            new Trainer
            {
                PassportNumber = "2345 678901",
                FullName = "Кондратьева Екатерина Алексеевна",
                Specialization = Specializations[1],
                Experience = 3,
                Gender = Gender.Female,
                BirthDate = new DateOnly(1996, 2, 14)
            },
            new Trainer
            {
                PassportNumber = "3456 789012",
                FullName = "Смирнов Алексей Владимирович",
                Specialization = Specializations[2],
                Experience = 7,
                Gender = Gender.Male,
                BirthDate = new DateOnly(1988, 7, 16)
            },
            new Trainer
            {
                PassportNumber = "4567 890123",
                FullName = "Голубочкин Дмитрий Олегович",
                Specialization = Specializations[3],
                Experience = 35,
                Gender = Gender.Male,
                BirthDate = new DateOnly(1969, 9, 26)
            },
            new Trainer
            {
                PassportNumber = "5678 901234",
                FullName = "Кузнецова Анастасия Сергеевна",
                Specialization = Specializations[4],
                Experience = 2,
                Gender = Gender.Female,
                BirthDate = new DateOnly(1995, 11, 12)
            },
            new Trainer
            {
                PassportNumber = "6789 012345",
                FullName = "Петрова Мария Владимировна",
                Specialization = Specializations[5],
                Experience = 14,
                Gender = Gender.Female,
                BirthDate = new DateOnly(1980, 12, 15)
            },
            new Trainer
            {
                PassportNumber = "7890 123456",
                FullName = "Иванов Евстафий Алексеевич",
                Specialization = Specializations[6],
                Experience = 10,
                Gender = Gender.Male,
                BirthDate = new DateOnly(2000, 7, 10)
            },
            new Trainer
            {
                PassportNumber = "8901 234567",
                FullName = "Ярослав Юрьевич Дронов", //шаман
                Specialization = Specializations[7],
                Experience = 5,
                Gender = Gender.Male,
                BirthDate = new DateOnly(1991, 11, 22)
            },
            new Trainer
            {
                PassportNumber = "9012 345678",
                FullName = "Сергеев Алексей Владимирович",
                Specialization = Specializations[8],
                Experience = 12,
                Gender = Gender.Male,
                BirthDate = new DateOnly(1990, 3, 5)
            },
            new Trainer
            {
                PassportNumber = "0123 456789",
                FullName = "Александрова Мария Сергеевна",
                Specialization = Specializations[9],
                Experience = 6,
                Gender = Gender.Female,
                BirthDate = new DateOnly(1991, 12, 26)
            }
        ];
        Halls =
        [
            new Hall { Name = "Зал бокса" },
            new Hall { Name = "Зал фитнеса" },
            new Hall { Name = "Зал йоги" },
            new Hall { Name = "Зал пилатеса" },
            new Hall { Name = "Кросс-фитнес студия" },
            new Hall { Name = "Зал кардиотренировок" },
            new Hall { Name = "Зал функциональных тренировок" },
            new Hall { Name = "Зал групповых тренировок" },
            new Hall { Name = "Зал боевых искусств" },
            new Hall { Name = "Бассейн" }
        ];
        Bookings =
        [
            new Booking
            {
                Client = Clients[0],
                Trainer = Trainers[0],
                Hall = Halls[0],
                DateTime = new DateTime(2026, 1, 1, 10, 0, 0),
                IsTrial = false,
                Duration = TimeSpan.FromMinutes(60)
            },
            new Booking
            {
                Client = Clients[1],
                Trainer = Trainers[1],
                Hall = Halls[1],
                DateTime = new DateTime(2026, 3, 19, 14, 30, 0),
                IsTrial = false,
                Duration = TimeSpan.FromMinutes(90)
            },
            new Booking
            {
                Client = Clients[2],
                Trainer = Trainers[2],
                Hall = Halls[2],
                DateTime = new DateTime(2025, 9, 11, 18, 15, 0),
                IsTrial = false,
                Duration = TimeSpan.FromMinutes(45)
            },
            new Booking
            {
                Client = Clients[3],
                Trainer = Trainers[3],
                Hall = Halls[3],
                DateTime = new DateTime(2026, 1, 1, 9, 45, 0),
                IsTrial = false,
                Duration = TimeSpan.FromMinutes(120)
            },
            new Booking
            {
                Client = Clients[4],
                Trainer = Trainers[4],
                Hall = Halls[4],
                DateTime = new DateTime(2025, 9, 1, 16, 0, 0),
                IsTrial = true,
                Duration = TimeSpan.FromMinutes(30)
            },
            new Booking
            {
                Client = Clients[5],
                Trainer = Trainers[5],
                Hall = Halls[5],
                DateTime = new DateTime(2026, 6, 1, 11, 30, 0),
                IsTrial = false,
                Duration = TimeSpan.FromMinutes(60)
            },
            new Booking
            {
                Client = Clients[6],
                Trainer = Trainers[6],
                Hall = Halls[0],
                DateTime = new DateTime(2026, 9, 29, 13, 15, 0),
                IsTrial = false,
                Duration = TimeSpan.FromMinutes(180)
            },
            new Booking
            {
                Client = Clients[7],
                Trainer = Trainers[7],
                Hall = Halls[1],
                DateTime = new DateTime(2026, 9, 13, 15, 45, 0),
                IsTrial = true,
                Duration = TimeSpan.FromMinutes(45)
            },
            new Booking
            {
                Client = Clients[8],
                Trainer = Trainers[8],
                Hall = Halls[2],
                DateTime = new DateTime(2020, 1, 1, 12, 30, 0),
                IsTrial = false,
                Duration = TimeSpan.FromMinutes(90)
            },
            new Booking
            {
                Client = Clients[9],
                Trainer = Trainers[9],
                Hall = Halls[3],
                DateTime = new DateTime(2024, 8, 13, 17, 0, 0),
                IsTrial = false,
                Duration = TimeSpan.FromMinutes(60)
            },
            new Booking
            {
                Client = Clients[0],
                Trainer = Trainers[0],
                Hall = Halls[4],
                DateTime = new DateTime(2026, 9, 15, 12, 0, 0),
                IsTrial = false,
                Duration = TimeSpan.FromMinutes(60)
            },
            new Booking
            {
                Client = Clients[1],
                Trainer = Trainers[1],
                Hall = Halls[4],
                DateTime = new DateTime(2026, 9, 15, 10, 0, 0),
                IsTrial = false,
                Duration = TimeSpan.FromMinutes(60)
            },
            new Booking
            {
                Client = Clients[2],
                Trainer = Trainers[2],
                Hall = Halls[4],
                DateTime = new DateTime(2026, 9, 15, 11, 0, 0),
                IsTrial = true,
                Duration = TimeSpan.FromMinutes(60)
            },
            new Booking
            {
                Client = Clients[1],
                Trainer = Trainers[1],
                Hall = Halls[5],
                DateTime = new DateTime(2026, 9, 15, 12, 30, 0),
                IsTrial = false,
                Duration = TimeSpan.FromMinutes(60)
            },
            new Booking
            {
                Client = Clients[2],
                Trainer = Trainers[2],
                Hall = Halls[6],
                DateTime = new DateTime(2026, 9, 15, 13, 0, 0),
                IsTrial = false,
                Duration = TimeSpan.FromMinutes(60)
            },
            new Booking
            {
                Client = Clients[0],
                Trainer = Trainers[2],
                Hall = Halls[2],
                DateTime = new DateTime(2026, 2, 10, 9, 0, 0),
                IsTrial = false,
                Duration = TimeSpan.FromMinutes(60)
            },
            new Booking
            {
                Client = Clients[1],
                Trainer = Trainers[2],
                Hall = Halls[2],
                DateTime = new DateTime(2026, 3, 5, 11, 0, 0),
                IsTrial = false,
                Duration = TimeSpan.FromMinutes(90)
            },
            new Booking
            {
                Client = Clients[3],
                Trainer = Trainers[6],
                Hall = Halls[6],
                DateTime = new DateTime(2026, 4, 1, 15, 0, 0),
                IsTrial = false,
                Duration = TimeSpan.FromMinutes(120)
            },
            new Booking
            {
                Client = Clients[4],
                Trainer = Trainers[3],
                Hall = Halls[6],
                DateTime = new DateTime(2026, 5, 10, 16, 0, 0),
                IsTrial = false,
                Duration = TimeSpan.FromMinutes(180)
            },
            new Booking
            {
                Client = Clients[5],
                Trainer = Trainers[3],
                Hall = Halls[6],
                DateTime = new DateTime(2026, 6, 15, 17, 0, 0),
                IsTrial = false,
                Duration = TimeSpan.FromMinutes(240)
            },
            new Booking
            {
                Client = Clients[6],
                Trainer = Trainers[3],
                Hall = Halls[6],
                DateTime = new DateTime(2026, 7, 20, 18, 0, 0),
                IsTrial = false,
                Duration = TimeSpan.FromMinutes(360)
            },
        ];
    }
}
