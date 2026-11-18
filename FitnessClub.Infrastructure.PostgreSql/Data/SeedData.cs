using FitnessClub.Domain.Entities;
using FitnessClub.Domain.Enums;

namespace FitnessClub.Infrastructure.PostgreSql.Data;

/// <summary>
/// Первоначальное заполнение БД, включаемое в миграцию через HasData
/// </summary>
public static class SeedData
{
    /// <summary>
    /// Условная текущая дата, относительно которой заданы даты абонементов и занятий
    /// </summary>
    public static readonly DateTime SeedDate = new(2026, 11, 17);

    /// <summary>
    /// Специализации тренеров
    /// </summary>
    public static IEnumerable<object> Specializations =>
    [
        new { Id = 1, Name = "Йога" },
        new { Id = 2, Name = "Пилатес" },
        new { Id = 3, Name = "Бокс" },
        new { Id = 4, Name = "Плавание" },
        new { Id = 5, Name = "Кроссфит" },
        new { Id = 6, Name = "Теннис" },
        new { Id = 7, Name = "Лёгкая атлетика" },
        new { Id = 8, Name = "Тяжёлая атлетика" },
        new { Id = 9, Name = "Фитнес" },
        new { Id = 10, Name = "Реабилитация" },
    ];

    /// <summary>
    /// Тренеры
    /// </summary>
    public static IEnumerable<object> Trainers =>
    [
        new { Id = 1, PassportNumber = "4513 104010", FullName = "Иванов Пётр Иванович",       Gender = Gender.Male,   BirthDate = new DateTime(1980, 5, 12), SpecializationId = 1,  ExperienceYears = 3 },
        new { Id = 2, PassportNumber = "4532 100002", FullName = "Смирнов Алексей Петрович",   Gender = Gender.Male,   BirthDate = new DateTime(1975, 3, 8),  SpecializationId = 4,  ExperienceYears = 7 },
        new { Id = 3, PassportNumber = "4521 100003", FullName = "Кузнецова Мария Сергеевна",  Gender = Gender.Female, BirthDate = new DateTime(1988, 7, 21), SpecializationId = 1,  ExperienceYears = 10 },
        new { Id = 4, PassportNumber = "4567 102004", FullName = "Соколов Дмитрий Андреевич",  Gender = Gender.Male,   BirthDate = new DateTime(1990, 1, 30), SpecializationId = 3,  ExperienceYears = 2 },
        new { Id = 5, PassportNumber = "4589 100008", FullName = "Попова Елена Викторовна",    Gender = Gender.Female, BirthDate = new DateTime(1985, 9, 14), SpecializationId = 2,  ExperienceYears = 5 },
        new { Id = 6, PassportNumber = "4512 100006", FullName = "Лебедев Артём Олегович",     Gender = Gender.Male,   BirthDate = new DateTime(1978, 11, 2), SpecializationId = 5,  ExperienceYears = 12 },
        new { Id = 7, PassportNumber = "4522 101007", FullName = "Козлов Игорь Владимирович",  Gender = Gender.Male,   BirthDate = new DateTime(1995, 4, 17), SpecializationId = 8,  ExperienceYears = 1 },
        new { Id = 8, PassportNumber = "4577 100305", FullName = "Новикова Ольга Дмитриевна",  Gender = Gender.Female, BirthDate = new DateTime(1983, 6, 25), SpecializationId = 9,  ExperienceYears = 6 },
        new { Id = 9, PassportNumber = "4553 100009", FullName = "Морозов Павел Игоревич",     Gender = Gender.Male,   BirthDate = new DateTime(1992, 2, 9),  SpecializationId = 6,  ExperienceYears = 4 },
        new { Id = 10, PassportNumber = "4543 105001", FullName = "Волкова Анна Александровна", Gender = Gender.Female, BirthDate = new DateTime(1986, 4, 26), SpecializationId = 10, ExperienceYears = 8 },
    ];

    /// <summary>
    /// Клиенты
    /// </summary>
    public static IEnumerable<object> Clients =>
    [
        new { Id = 1, PassportNumber = "4501 200301", FullName = "Петров Пётр Сергеевич",       Gender = Gender.Male,   BirthDate = new DateTime(1991, 3, 15), Phone = "+7 911 100-11-11", SubscriptionStart = SeedDate.AddMonths(-13), SubscriptionEnd = SeedDate.AddMonths(-1) },
        new { Id = 2, PassportNumber = "4501 204002", FullName = "Орлова Марина Ивановна",       Gender = Gender.Female, BirthDate = new DateTime(1994, 5, 22), Phone = "+7 911 100-22-22", SubscriptionStart = SeedDate.AddMonths(-2),  SubscriptionEnd = SeedDate.AddDays(20) },
        new { Id = 3, PassportNumber = "4541 220003", FullName = "Сидоров Николай Петрович",     Gender = Gender.Male,   BirthDate = new DateTime(1987, 8, 3),  Phone = "+7 911 100-33-33", SubscriptionStart = SeedDate.AddMonths(-7),  SubscriptionEnd = SeedDate.AddDays(-10) },
        new { Id = 4, PassportNumber = "4531 200204", FullName = "Белова Ксения Павловна",       Gender = Gender.Female, BirthDate = new DateTime(1996, 10, 11), Phone = "+7 911 100-44-44", SubscriptionStart = SeedDate.AddMonths(-1), SubscriptionEnd = SeedDate.AddMonths(2) },
        new { Id = 5, PassportNumber = "4501 200005", FullName = "Гусев Андрей Юрьевич",         Gender = Gender.Male,   BirthDate = new DateTime(1989, 1, 27),  Phone = "+7 911 100-55-55", SubscriptionStart = SeedDate.AddMonths(-6),  SubscriptionEnd = SeedDate.AddDays(-5) },
        new { Id = 6, PassportNumber = "4521 200206", FullName = "Дмитриева Светлана Олеговна",  Gender = Gender.Female, BirthDate = new DateTime(1993, 4, 19),  Phone = "+7 911 100-66-66", SubscriptionStart = SeedDate.AddDays(-10),   SubscriptionEnd = SeedDate.AddDays(100) },
        new { Id = 7, PassportNumber = "4511 200307", FullName = "Зайцев Роман Викторович",      Gender = Gender.Male,   BirthDate = new DateTime(1985, 7, 7),   Phone = "+7 911 100-77-77", SubscriptionStart = SeedDate.AddMonths(-9),  SubscriptionEnd = SeedDate.AddDays(-45) },
        new { Id = 8, PassportNumber = "4541 205008", FullName = "Киселёва Дарья Максимовна",    Gender = Gender.Female, BirthDate = new DateTime(1998, 2, 14),  Phone = "+7 911 100-88-88", SubscriptionStart = SeedDate.AddMonths(-3),  SubscriptionEnd = SeedDate.AddDays(14) },
        new { Id = 9, PassportNumber = "4561 208009", FullName = "Львов Григорий Артёмович",     Gender = Gender.Male,   BirthDate = new DateTime(1990, 11, 29), Phone = "+7 911 100-99-99", SubscriptionStart = SeedDate.AddMonths(-12), SubscriptionEnd = SeedDate.AddDays(-1) },
        new { Id = 10, PassportNumber = "4511 200910", FullName = "Миронова Валерия Игоревна",    Gender = Gender.Female, BirthDate = new DateTime(1997, 9, 5),   Phone = "+7 911 100-00-00", SubscriptionStart = SeedDate.AddMonths(-4),  SubscriptionEnd = SeedDate.AddYears(1) },
    ];

    /// <summary>
    /// Занятия
    /// </summary>
    public static IEnumerable<object> Lessons =>
    [
        new { Id = 1,  ClientId = 1,  TrainerId = 3, DateStart = S(5, 10),             DateEnd = (DateTime?)null,          Hall = "Зал йоги",        IsTrial = true },
        new { Id = 2,  ClientId = 2,  TrainerId = 3, DateStart = S(10, 12),            DateEnd = (DateTime?)null,          Hall = "Зал йоги",        IsTrial = false },
        new { Id = 3,  ClientId = 3,  TrainerId = 5, DateStart = S(15, 18),            DateEnd = (DateTime?)null,          Hall = "Зал йоги",        IsTrial = false },
        new { Id = 4,  ClientId = 1,  TrainerId = 3, DateStart = S(5, 10).AddMonths(-1), DateEnd = (DateTime?)null,        Hall = "Зал йоги",        IsTrial = false },

        new { Id = 5,  ClientId = 4,  TrainerId = 1, DateStart = S(3, 9),              DateEnd = (DateTime?)null,          Hall = "Основной зал",    IsTrial = false },
        new { Id = 6,  ClientId = 5,  TrainerId = 1, DateStart = S(8, 11),             DateEnd = (DateTime?)null,          Hall = "Основной зал",    IsTrial = false },
        new { Id = 7,  ClientId = 6,  TrainerId = 1, DateStart = S(12, 16),            DateEnd = (DateTime?)null,          Hall = "Основной зал",    IsTrial = false },
        new { Id = 8,  ClientId = 7,  TrainerId = 1, DateStart = S(20, 19),            DateEnd = (DateTime?)null,          Hall = "Основной зал",    IsTrial = true },
        new { Id = 9,  ClientId = 8,  TrainerId = 1, DateStart = S(22, 8),             DateEnd = (DateTime?)S(22, 8).AddMinutes(30), Hall = "Основной зал", IsTrial = false },
        new { Id = 10, ClientId = 9,  TrainerId = 1, DateStart = S(5, 10).AddMonths(1), DateEnd = (DateTime?)null,         Hall = "Основной зал",    IsTrial = false },

        new { Id = 11, ClientId = 10, TrainerId = 2, DateStart = S(6, 10),             DateEnd = (DateTime?)null,          Hall = "Бассейн",         IsTrial = false },
        new { Id = 12, ClientId = 1,  TrainerId = 2, DateStart = S(13, 10),            DateEnd = (DateTime?)S(13, 10).AddHours(1), Hall = "Бассейн", IsTrial = true },
        new { Id = 13, ClientId = 2,  TrainerId = 2, DateStart = S(21, 10),            DateEnd = (DateTime?)null,          Hall = "Бассейн",         IsTrial = false },
        new { Id = 14, ClientId = 3,  TrainerId = 2, DateStart = S(27, 10),            DateEnd = (DateTime?)null,          Hall = "Бассейн",         IsTrial = false },

        new { Id = 15, ClientId = 4,  TrainerId = 4, DateStart = S(7, 10),             DateEnd = (DateTime?)S(7, 10).AddMinutes(90), Hall = "Зал единоборств", IsTrial = false },
        new { Id = 16, ClientId = 5,  TrainerId = 4, DateStart = S(14, 10),            DateEnd = (DateTime?)null,          Hall = "Зал единоборств", IsTrial = true },
    ];

    /// <summary>
    /// Занятие в текущем (относительно SeedDate) месяце в указанный день и час
    /// </summary>
    /// <param name="day">День месяца</param>
    /// <param name="hour">Час начала</param>
    /// <returns>Дата и время начала занятия</returns>
    private static DateTime S(int day, int hour) =>
        new(SeedDate.Year, SeedDate.Month, day, hour, 0, 0);
}
