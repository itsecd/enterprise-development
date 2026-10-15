using FitnesClub.Domain.Entities;
using FitnesClub.Domain.Enums;

namespace FitnesClub.Domain.Data;

public static class DataSeeder
{
    private static readonly DateTime _date = new (2026, 9, 28, 12, 0, 0);
    public static readonly List<Specialization> Specializations =
    [
        new Specialization { Name = "Фитнес" },
        new Specialization { Name = "Йога" },
        new Specialization { Name = "Силовые тренировки" },
        new Specialization { Name = "Пилатес" },
        new Specialization { Name = "Кардио" },
        new Specialization { Name = "Стретчинг" },
        new Specialization { Name = "Функциональный тренинг" },
        new Specialization { Name = "Кроссфит" },
        new Specialization { Name = "Бокс" },
        new Specialization { Name = "Танцевальные тренировки" }
     ];

    public static readonly List<Trainer> Trainers =
    [
        new Trainer
        {
            PassportNumber = "1000000001",
            FirstName = "Алексей",
            LastName = "Смирнов",
            Gender = Gender.Male,
            BirthDate = new(1985, 3, 15),
            SpecializationId = Specializations[0].Id,
            Specialization = Specializations[0],
            WorkExperienceYears = 8,
            PhoneNumber = "+79990000001"
        },
        new Trainer
        {
            PassportNumber = "1000000002",
            FirstName = "Елена",
            LastName = "Волкова",
            Gender = Gender.Female,
            BirthDate = new(1990, 7, 20),
            SpecializationId = Specializations[1].Id,
            Specialization = Specializations[1],
            WorkExperienceYears = 6,
            PhoneNumber = "+79990000002"
        },
        new Trainer
        {
            PassportNumber = "1000000003",
            FirstName = "Дмитрий",
            LastName = "Соколов",
            Gender = Gender.Male,
            BirthDate = new(1982, 11, 5),
            SpecializationId = Specializations[2].Id,
            Specialization = Specializations[2],
            WorkExperienceYears = 12,
            PhoneNumber = "+79990000003"
        },
        new Trainer
        {
            PassportNumber = "1000000004",
            FirstName = "Ольга",
            LastName = "Морозова",
            Gender = Gender.Female,
            BirthDate = new(1992, 2, 10),
            SpecializationId = Specializations[3].Id,
            Specialization = Specializations[3],
            WorkExperienceYears = 3,
            PhoneNumber = "+79990000004"
        },
        new Trainer
        {
            PassportNumber = "1000000005",
            FirstName = "Игорь",
            LastName = "Новиков",
            Gender = Gender.Male,
            BirthDate = new(1980, 9, 25),
            SpecializationId = Specializations[4].Id,
            Specialization = Specializations[4],
            WorkExperienceYears = 15,
            PhoneNumber = "+79990000005"
        },
        new Trainer
        {
            PassportNumber = "1000000006",
            FirstName = "Анна",
            LastName = "Федорова",
            Gender = Gender.Female,
            BirthDate = new(1995, 4, 18),
            SpecializationId = Specializations[5].Id,
            Specialization = Specializations[5],
            WorkExperienceYears = 2,
            PhoneNumber = "+79990000006"
        },
        new Trainer
        {
            PassportNumber = "1000000007",
            FirstName = "Максим",
            LastName = "Козлов",
            Gender = Gender.Male,
            BirthDate = new(1988, 6, 12),
            SpecializationId = Specializations[6].Id,
            Specialization = Specializations[6],
            WorkExperienceYears = 7,
            PhoneNumber = "+79990000007"
        },
        new Trainer
        {
            PassportNumber = "1000000008",
            FirstName = "Татьяна",
            LastName = "Лебедева",
            Gender = Gender.Female,
            BirthDate = new(1986, 12, 1),
            SpecializationId = Specializations[7].Id,
            Specialization = Specializations[7],
            WorkExperienceYears = 10,
            PhoneNumber = "+79990000008"
        },
        new Trainer
        {
            PassportNumber = "1000000009",
            FirstName = "Сергей",
            LastName = "Петров",
            Gender = Gender.Male,
            BirthDate = new(1984, 1, 30),
            SpecializationId = Specializations[8].Id,
            Specialization = Specializations[8],
            WorkExperienceYears = 9,
            PhoneNumber = "+79990000009"
        },
        new Trainer
        {
            PassportNumber = "1000000010",
            FirstName = "Мария",
            LastName = "Васильева",
            Gender = Gender.Female,
            BirthDate = new(1991, 8, 22),
            SpecializationId = Specializations[9].Id,
            Specialization = Specializations[9],
            WorkExperienceYears = 4,
            PhoneNumber = "+79990000010"
        }

    ];

    public static readonly List<Member> Members =
    [
        new Member
        {
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

    public static readonly List<Booking> Bookings =
    [
        new Booking
        {
            MemberId = Members[0].Id,
            Member = Members[0],
            TrainerId = Trainers[0].Id,
            Trainer = Trainers[0],
            LessonDateTime = _date.AddDays(1),
            Duration = TimeSpan.FromHours(1),
            RoomName = "Зал №1",
            IsTrial = false
        },
        new Booking
        {
            MemberId = Members[1].Id,
            Member = Members[1],
            TrainerId = Trainers[1].Id,
            Trainer = Trainers[1],
            LessonDateTime = _date.AddDays(2),
            Duration = TimeSpan.FromHours(2),
            RoomName = "Зал №2",
            IsTrial = true
        },
        new Booking
        {
            MemberId = Members[2].Id,
            Member = Members[2],
            TrainerId = Trainers[2].Id,
            Trainer = Trainers[2],
            LessonDateTime = _date.AddDays(-2),
            Duration = TimeSpan.FromHours(1),
            RoomName = "Зал №1",
            IsTrial = false
        },
        new Booking
        {
            MemberId = Members[3].Id,
            Member = Members[3],
            TrainerId = Trainers[0].Id,
            Trainer = Trainers[0],
            LessonDateTime = _date.AddDays(3),
            Duration = TimeSpan.FromHours(1),
            RoomName = "Зал №1",
            IsTrial = false
        },
        new Booking
        {
            MemberId = Members[4].Id,
            Member = Members[4],
            TrainerId = Trainers[2].Id,
            Trainer = Trainers[2],
            LessonDateTime = _date.AddDays(4),
            Duration = TimeSpan.FromHours(2),
            RoomName = "Зал №3",
            IsTrial = true
        },
        new Booking
        {
            MemberId = Members[5].Id,
            Member = Members[5],
            TrainerId = Trainers[0].Id,
            Trainer = Trainers[0],
            LessonDateTime = _date.AddDays(5),
            Duration = TimeSpan.FromHours(1),
            RoomName = "Зал №1",
            IsTrial = false
        },
        new Booking
        {
            MemberId = Members[6].Id,
            Member = Members[6],
            TrainerId = Trainers[3].Id,
            Trainer = Trainers[3],
            LessonDateTime = _date.AddDays(-5),
            Duration = TimeSpan.FromHours(1),
            RoomName = "Зал №2",
            IsTrial = false
        },
        new Booking
        {
            MemberId = Members[7].Id,
            Member = Members[7],
            TrainerId = Trainers[4].Id,
            Trainer = Trainers[4],
            LessonDateTime = _date.AddDays(6),
            Duration = TimeSpan.FromHours(2),
            RoomName = "Зал №3",
            IsTrial = false
        },
        new Booking
        {
            MemberId = Members[8].Id,
            Member = Members[8],
            TrainerId = Trainers[2].Id,
            Trainer = Trainers[2],
            LessonDateTime = _date.AddDays(7),
            Duration = TimeSpan.FromHours(1),
            RoomName = "Зал №1",
            IsTrial = true
        },
        new Booking
        {
            MemberId = Members[9].Id,
            Member = Members[9],
            TrainerId = Trainers[4].Id,
            Trainer = Trainers[4],
            LessonDateTime = _date.AddDays(8),
            Duration = TimeSpan.FromHours(1),
            RoomName = "Зал №3",
            IsTrial = false
        }
    ];
}
