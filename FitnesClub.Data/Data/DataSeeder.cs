namespace FitnesClub.Data.Data;

using FitnesClub.Domain.Entities;
using FitnesClub.Domain.Enums;

/// <summary>
/// Предоставляет начальные (сидовые) данные для заполнения базы данных и репозиториев фитнес-клуба.
/// </summary>
public static class DataSeeder
{
    /// <summary>
    /// Опорная дата и время для генерации расписания и сроков действия абонементов.
    /// </summary>
    private static readonly DateTime _date = new(2026, 9, 28, 12, 0, 0);

    /// <summary>
    /// Начальный список специализаций тренеров.
    /// </summary>
    public static readonly List<Specialization> Specializations =
    [
        new Specialization { Id = Guid.Parse("11111111-0000-0000-0000-000000000001"), Name = "Фитнес" },
        new Specialization { Id = Guid.Parse("11111111-0000-0000-0000-000000000002"), Name = "Йога" },
        new Specialization { Id = Guid.Parse("11111111-0000-0000-0000-000000000003"), Name = "Силовые тренировки" },
        new Specialization { Id = Guid.Parse("11111111-0000-0000-0000-000000000004"), Name = "Пилатес" },
        new Specialization { Id = Guid.Parse("11111111-0000-0000-0000-000000000005"), Name = "Кардио" },
        new Specialization { Id = Guid.Parse("11111111-0000-0000-0000-000000000006"), Name = "Стретчинг" },
        new Specialization { Id = Guid.Parse("11111111-0000-0000-0000-000000000007"), Name = "Функциональный тренинг" },
        new Specialization { Id = Guid.Parse("11111111-0000-0000-0000-000000000008"), Name = "Кроссфит" },
        new Specialization { Id = Guid.Parse("11111111-0000-0000-0000-000000000009"), Name = "Бокс" },
        new Specialization { Id = Guid.Parse("11111111-0000-0000-0000-000000000010"), Name = "Танцевальные тренировки" }
    ];

    /// <summary>
    /// Начальный список тренеров клуба с заполненными профилями и внешними ключами специализаций.
    /// </summary>
    public static readonly List<Trainer> Trainers =
    [
        new Trainer
        {
            Id = Guid.Parse("22222222-0000-0000-0000-000000000001"),
            PassportNumber = "1000000001",
            FirstName = "Алексей",
            LastName = "Смирнов",
            Gender = Gender.Male,
            BirthDate = new(1985, 3, 15),
            SpecializationId = Specializations[0].Id,
            WorkExperienceYears = 8,
            PhoneNumber = "+79990000001"
        },
        new Trainer
        {
            Id = Guid.Parse("22222222-0000-0000-0000-000000000002"),
            PassportNumber = "1000000002",
            FirstName = "Елена",
            LastName = "Волкова",
            Gender = Gender.Female,
            BirthDate = new(1990, 7, 20),
            SpecializationId = Specializations[1].Id,
            WorkExperienceYears = 6,
            PhoneNumber = "+79990000002"
        },
        new Trainer
        {
            Id = Guid.Parse("22222222-0000-0000-0000-000000000003"),
            PassportNumber = "1000000003",
            FirstName = "Дмитрий",
            LastName = "Соколов",
            Gender = Gender.Male,
            BirthDate = new(1982, 11, 5),
            SpecializationId = Specializations[2].Id,
            WorkExperienceYears = 12,
            PhoneNumber = "+79990000003"
        },
        new Trainer
        {
            Id = Guid.Parse("22222222-0000-0000-0000-000000000004"),
            PassportNumber = "1000000004",
            FirstName = "Ольга",
            LastName = "Морозова",
            Gender = Gender.Female,
            BirthDate = new(1992, 2, 10),
            SpecializationId = Specializations[3].Id,
            WorkExperienceYears = 3,
            PhoneNumber = "+79990000004"
        },
        new Trainer
        {
            Id = Guid.Parse("22222222-0000-0000-0000-000000000005"),
            PassportNumber = "1000000005",
            FirstName = "Игорь",
            LastName = "Новиков",
            Gender = Gender.Male,
            BirthDate = new(1980, 9, 25),
            SpecializationId = Specializations[4].Id,
            WorkExperienceYears = 15,
            PhoneNumber = "+79990000005"
        },
        new Trainer
        {
            Id = Guid.Parse("22222222-0000-0000-0000-000000000006"),
            PassportNumber = "1000000006",
            FirstName = "Анна",
            LastName = "Федорова",
            Gender = Gender.Female,
            BirthDate = new(1995, 4, 18),
            SpecializationId = Specializations[5].Id,
            WorkExperienceYears = 2,
            PhoneNumber = "+79990000006"
        },
        new Trainer
        {
            Id = Guid.Parse("22222222-0000-0000-0000-000000000007"),
            PassportNumber = "1000000007",
            FirstName = "Максим",
            LastName = "Козлов",
            Gender = Gender.Male,
            BirthDate = new(1988, 6, 12),
            SpecializationId = Specializations[6].Id,
            WorkExperienceYears = 7,
            PhoneNumber = "+79990000007"
        },
        new Trainer
        {
            Id = Guid.Parse("22222222-0000-0000-0000-000000000008"),
            PassportNumber = "1000000008",
            FirstName = "Татьяна",
            LastName = "Лебедева",
            Gender = Gender.Female,
            BirthDate = new(1986, 12, 1),
            SpecializationId = Specializations[7].Id,
            WorkExperienceYears = 10,
            PhoneNumber = "+79990000008"
        },
        new Trainer
        {
            Id = Guid.Parse("22222222-0000-0000-0000-000000000009"),
            PassportNumber = "1000000009",
            FirstName = "Сергей",
            LastName = "Петров",
            Gender = Gender.Male,
            BirthDate = new(1984, 1, 30),
            SpecializationId = Specializations[8].Id,
            WorkExperienceYears = 9,
            PhoneNumber = "+79990000009"
        },
        new Trainer
        {
            Id = Guid.Parse("22222222-0000-0000-0000-000000000010"),
            PassportNumber = "1000000010",
            FirstName = "Мария",
            LastName = "Васильева",
            Gender = Gender.Female,
            BirthDate = new(1991, 8, 22),
            SpecializationId = Specializations[9].Id,
            WorkExperienceYears = 4,
            PhoneNumber = "+79990000010"
        }
    ];

    /// <summary>
    /// Начальный список клиентов клуба с активными и истекшими абонементов.
    /// </summary>
    public static readonly List<Member> Members =
    [
        new Member
        {
            Id = Guid.Parse("33333333-0000-0000-0000-000000000001"),
            PassportNumber = "2000000001",
            FirstName = "Иван",
            LastName = "Иванов",
            Gender = Gender.Male,
            BirthDate = new(1995, 5, 10),
            PhoneNumber = "+79991111111",
            MembershipStartDate = _date.AddMonths(-2),
            MembershipEndDate = _date.AddMonths(1)
        },
        new Member
        {
            Id = Guid.Parse("33333333-0000-0000-0000-000000000002"),
            PassportNumber = "2000000002",
            FirstName = "Анна",
            LastName = "Петрова",
            Gender = Gender.Female,
            BirthDate = new(1998, 8, 15),
            PhoneNumber = "+79991111112",
            MembershipStartDate = _date.AddMonths(-3),
            MembershipEndDate = _date.AddMonths(2)
        },
        new Member
        {
            Id = Guid.Parse("33333333-0000-0000-0000-000000000003"),
            PassportNumber = "2000000003",
            FirstName = "Алексей",
            LastName = "Сидоров",
            Gender = Gender.Male,
            BirthDate = new(1992, 1, 20),
            PhoneNumber = "+79991111113",
            MembershipStartDate = _date.AddMonths(-6),
            MembershipEndDate = _date.AddDays(-10)
        },
        new Member
        {
            Id = Guid.Parse("33333333-0000-0000-0000-000000000004"),
            PassportNumber = "2000000004",
            FirstName = "Екатерина",
            LastName = "Смирнова",
            Gender = Gender.Female,
            BirthDate = new(1997, 3, 12),
            PhoneNumber = "+79991111114",
            MembershipStartDate = _date.AddMonths(-1),
            MembershipEndDate = _date.AddMonths(3)
        },
        new Member
        {
            Id = Guid.Parse("33333333-0000-0000-0000-000000000005"),
            PassportNumber = "2000000005",
            FirstName = "Михаил",
            LastName = "Кузнецов",
            Gender = Gender.Male,
            BirthDate = new(1989, 10, 3),
            PhoneNumber = "+79991111115",
            MembershipStartDate = _date.AddMonths(-8),
            MembershipEndDate = _date.AddDays(-30)
        },
        new Member
        {
            Id = Guid.Parse("33333333-0000-0000-0000-000000000006"),
            PassportNumber = "2000000006",
            FirstName = "Ольга",
            LastName = "Попова",
            Gender = Gender.Female,
            BirthDate = new(1994, 6, 25),
            PhoneNumber = "+79991111116",
            MembershipStartDate = _date.AddMonths(-2),
            MembershipEndDate = _date.AddMonths(1)
        },
        new Member
        {
            Id = Guid.Parse("33333333-0000-0000-0000-000000000007"),
            PassportNumber = "2000000007",
            FirstName = "Артем",
            LastName = "Васильев",
            Gender = Gender.Male,
            BirthDate = new(1990, 9, 17),
            PhoneNumber = "+79991111117",
            MembershipStartDate = _date.AddMonths(-5),
            MembershipEndDate = _date.AddDays(-5)
        },
        new Member
        {
            Id = Guid.Parse("33333333-0000-0000-0000-000000000008"),
            PassportNumber = "2000000008",
            FirstName = "Наталья",
            LastName = "Соколова",
            Gender = Gender.Female,
            BirthDate = new(1996, 11, 8),
            PhoneNumber = "+79991111118",
            MembershipStartDate = _date.AddMonths(-1),
            MembershipEndDate = _date.AddMonths(2)
        },
        new Member
        {
            Id = Guid.Parse("33333333-0000-0000-0000-000000000009"),
            PassportNumber = "2000000009",
            FirstName = "Павел",
            LastName = "Михайлов",
            Gender = Gender.Male,
            BirthDate = new(1987, 4, 14),
            PhoneNumber = "+79991111119",
            MembershipStartDate = _date.AddMonths(-7),
            MembershipEndDate = _date.AddDays(-15)
        },
        new Member
        {
            Id = Guid.Parse("33333333-0000-0000-0000-000000000010"),
            PassportNumber = "2000000010",
            FirstName = "Ирина",
            LastName = "Новикова",
            Gender = Gender.Female,
            BirthDate = new(1993, 12, 30),
            PhoneNumber = "+79991111120",
            MembershipStartDate = _date.AddMonths(-2),
            MembershipEndDate = _date.AddMonths(1)
        }
    ];

    /// <summary>
    /// Начальный список записей на персональные тренировки.
    /// </summary>
    public static readonly List<Booking> Bookings =
    [
        new Booking
        {
            Id = Guid.Parse("44444444-0000-0000-0000-000000000001"),
            MemberId = Members[0].Id,
            TrainerId = Trainers[0].Id,
            LessonDateTime = _date.AddDays(1),
            Duration = TimeSpan.FromHours(1),
            RoomName = "Зал №1",
            IsTrial = false
        },
        new Booking
        {
            Id = Guid.Parse("44444444-0000-0000-0000-000000000002"),
            MemberId = Members[1].Id,
            TrainerId = Trainers[1].Id,
            LessonDateTime = _date.AddDays(2),
            Duration = TimeSpan.FromHours(2),
            RoomName = "Зал №2",
            IsTrial = true
        },
        new Booking
        {
            Id = Guid.Parse("44444444-0000-0000-0000-000000000003"),
            MemberId = Members[2].Id,
            TrainerId = Trainers[2].Id,
            LessonDateTime = _date.AddDays(-2),
            Duration = TimeSpan.FromHours(1),
            RoomName = "Зал №1",
            IsTrial = false
        },
        new Booking
        {
            Id = Guid.Parse("44444444-0000-0000-0000-000000000004"),
            MemberId = Members[3].Id,
            TrainerId = Trainers[0].Id,
            LessonDateTime = _date.AddDays(3),
            Duration = TimeSpan.FromHours(1),
            RoomName = "Зал №1",
            IsTrial = false
        },
        new Booking
        {
            Id = Guid.Parse("44444444-0000-0000-0000-000000000005"),
            MemberId = Members[4].Id,
            TrainerId = Trainers[2].Id,
            LessonDateTime = _date.AddDays(4),
            Duration = TimeSpan.FromHours(2),
            RoomName = "Зал №3",
            IsTrial = true
        },
        new Booking
        {
            Id = Guid.Parse("44444444-0000-0000-0000-000000000006"),
            MemberId = Members[5].Id,
            TrainerId = Trainers[0].Id,
            LessonDateTime = _date.AddDays(5),
            Duration = TimeSpan.FromHours(1),
            RoomName = "Зал №1",
            IsTrial = false
        },
        new Booking
        {
            Id = Guid.Parse("44444444-0000-0000-0000-000000000007"),
            MemberId = Members[6].Id,
            TrainerId = Trainers[3].Id,
            LessonDateTime = _date.AddDays(-5),
            Duration = TimeSpan.FromHours(1),
            RoomName = "Зал №2",
            IsTrial = false
        },
        new Booking
        {
            Id = Guid.Parse("44444444-0000-0000-0000-000000000008"),
            MemberId = Members[7].Id,
            TrainerId = Trainers[4].Id,
            LessonDateTime = _date.AddDays(6),
            Duration = TimeSpan.FromHours(2),
            RoomName = "Зал №3",
            IsTrial = false
        },
        new Booking
        {
            Id = Guid.Parse("44444444-0000-0000-0000-000000000009"),
            MemberId = Members[8].Id,
            TrainerId = Trainers[2].Id,
            LessonDateTime = _date.AddDays(7),
            Duration = TimeSpan.FromHours(1),
            RoomName = "Зал №1",
            IsTrial = true
        },
        new Booking
        {
            Id = Guid.Parse("44444444-0000-0000-0000-000000000010"),
            MemberId = Members[9].Id,
            TrainerId = Trainers[4].Id,
            LessonDateTime = _date.AddDays(8),
            Duration = TimeSpan.FromHours(1),
            RoomName = "Зал №3",
            IsTrial = false
        }
    ];
}