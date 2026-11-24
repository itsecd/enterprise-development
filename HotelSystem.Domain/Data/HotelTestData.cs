using HotelSystem.Domain.Shared.Enums;
using HotelSystem.Domain.Models;

namespace HotelSystem.Domain.Data;

/// <summary>
/// Тестовые данные для гостиничной системы
/// </summary>
public static class HotelTestData
{
    /// <summary>
    /// Список типов гостиничных номеров
    /// </summary>
    public static List<RoomType> RoomTypes { get; } =
    [
        new()
        {
            Id = 1,
            Category = RoomCategory.Economy,
            Area = 18,
            BedCount = 1,
            HasBathOrShower = true,
            PricePerDay = 3000
        },
        new()
        {
            Id = 2,
            Category = RoomCategory.Standard,
            Area = 22,
            BedCount = 2,
            HasBathOrShower = true,
            PricePerDay = 4500
        },
        new()
        {
            Id = 3,
            Category = RoomCategory.Luxury,
            Area = 35,
            BedCount = 2,
            HasBathOrShower = true,
            PricePerDay = 8000
        },
        new()
        {
            Id = 4,
            Category = RoomCategory.Economy,
            Area = 20,
            BedCount = 1,
            HasBathOrShower = false,
            PricePerDay = 3200
        },
        new()
        {
            Id = 5,
            Category = RoomCategory.Standard,
            Area = 25,
            BedCount = 2,
            HasBathOrShower = true,
            PricePerDay = 5000
        },
        new()
        {
            Id = 6,
            Category = RoomCategory.Luxury,
            Area = 40,
            BedCount = 3,
            HasBathOrShower = true,
            PricePerDay = 9500
        },
        new()
        {
            Id = 7,
            Category = RoomCategory.Economy,
            Area = 19,
            BedCount = 1,
            HasBathOrShower = true,
            PricePerDay = 3100
        },
        new()
        {
            Id = 8,
            Category = RoomCategory.Standard,
            Area = 24,
            BedCount = 2,
            HasBathOrShower = false,
            PricePerDay = 4800
        },
        new()
        {
            Id = 9,
            Category = RoomCategory.Luxury,
            Area = 45,
            BedCount = 4,
            HasBathOrShower = true,
            PricePerDay = 11000
        },
        new()
        {
            Id = 10,
            Category = RoomCategory.Standard,
            Area = 28,
            BedCount = 2,
            HasBathOrShower = true,
            PricePerDay = 5500
        }
    ];

    /// <summary>
    /// Список гостиничных номеров
    /// </summary>
    public static List<Room> Rooms { get; } =
    [
        new() { Id = 1, RoomNumber = 101, Floor = 1, HasBalcony = false, RoomType = RoomTypes[0] },
        new() { Id = 2, RoomNumber = 102, Floor = 1, HasBalcony = true, RoomType = RoomTypes[1] },
        new() { Id = 3, RoomNumber = 103, Floor = 1, HasBalcony = true, RoomType = RoomTypes[2] },

        new() { Id = 4, RoomNumber = 201, Floor = 2, HasBalcony = false, RoomType = RoomTypes[0] },
        new() { Id = 5, RoomNumber = 202, Floor = 2, HasBalcony = true, RoomType = RoomTypes[1] },
        new() { Id = 6, RoomNumber = 203, Floor = 2, HasBalcony = true, RoomType = RoomTypes[2] },

        new() { Id = 7, RoomNumber = 301, Floor = 3, HasBalcony = false, RoomType = RoomTypes[3] },
        new() { Id = 8, RoomNumber = 302, Floor = 3, HasBalcony = true, RoomType = RoomTypes[4] },
        new() { Id = 9, RoomNumber = 303, Floor = 3, HasBalcony = true, RoomType = RoomTypes[5] },

        new() { Id = 10, RoomNumber = 401, Floor = 4, HasBalcony = false, RoomType = RoomTypes[3] }
    ];

    /// <summary>
    /// Список клиентов гостиницы
    /// </summary>
    public static List<Client> Clients { get; } =
    [
        new()
    {
        Id = 1,
        PassportNumber = "AA100001",
        LastName = "Александров",
        FirstName = "Алексей",
        Patronymic = "Петрович",
        BirthDate = new DateTime(1995, 3, 12),
        Citizenship = "Россия"
    },
    new()
    {
        Id = 2,
        PassportNumber = "AA100002",
        LastName = "Белов",
        FirstName = "Борис",
        Patronymic = "Сергеевич",
        BirthDate = new DateTime(1988, 7, 24),
        Citizenship = "Россия"
    },
    new()
    {
        Id = 3,
        PassportNumber = "AA100003",
        LastName = "Васильева",
        FirstName = "Виктория",
        Patronymic = "Андреевна",
        BirthDate = new DateTime(1999, 11, 5),
        Citizenship = "Россия"
    },
    new()
    {
        Id = 4,
        PassportNumber = "AA100004",
        LastName = "Громов",
        FirstName = "Георгий",
        Patronymic = "Иванович",
        BirthDate = new DateTime(1992, 1, 18),
        Citizenship = "Беларусь"
    },
    new()
    {
        Id = 5,
        PassportNumber = "AA100005",
        LastName = "Дмитриева",
        FirstName = "Дарья",
        Patronymic = "Олеговна",
        BirthDate = new DateTime(2001, 6, 30),
        Citizenship = "Россия"
    },
    new()
    {
        Id = 6,
        PassportNumber = "AA100006",
        LastName = "Егоров",
        FirstName = "Евгений",
        Patronymic = "Максимович",
        BirthDate = new DateTime(1985, 9, 14),
        Citizenship = "Казахстан"
    },
    new()
    {
        Id = 7,
        PassportNumber = "AA100007",
        LastName = "Жукова",
        FirstName = "Жанна",
        Patronymic = "Романовна",
        BirthDate = new DateTime(1997, 2, 22),
        Citizenship = "Россия"
    },
    new()
    {
        Id = 8,
        PassportNumber = "AA100008",
        LastName = "Захаров",
        FirstName = "Захар",
        Patronymic = "Денисович",
        BirthDate = new DateTime(1990, 12, 8),
        Citizenship = "Армения"
    },
    new()
    {
        Id = 9,
        PassportNumber = "AA100009",
        LastName = "Ван Дам",
        FirstName = "Ирина",
        Patronymic = "Павловна",
        BirthDate = new DateTime(1996, 4, 17),
        Citizenship = "Россия"
    },
    new()
    {
        Id = 10,
        PassportNumber = "AA100010",
        LastName = "Кузнецов",
        FirstName = "Кирилл",
        Patronymic = null,
        BirthDate = new DateTime(1993, 8, 27),
        Citizenship = "Россия"
    }
    ];

    /// <summary>
    /// Список бронирований гостиничных номеров
    /// </summary>
    public static List<Booking> Bookings { get; } =
    [
        new()
        {
            Id = 1,
            Client = Clients[0],
            Room = Rooms[0],
            CheckInDate = new DateTime(2026, 9, 1),
            NumberOfDays = 5
        },
        new()
        {
            Id = 2,
            Client = Clients[1],
            Room = Rooms[1],
            CheckInDate = new DateTime(2026, 9, 2),
            NumberOfDays = 3
        },
        new()
        {
            Id = 3,
            Client = Clients[2],
            Room = Rooms[1],
            CheckInDate = new DateTime(2026, 8, 20),
            NumberOfDays = 4
        },
        new()
        {
            Id = 4,
            Client = Clients[3],
            Room = Rooms[2],
            CheckInDate = new DateTime(2026, 9, 5),
            NumberOfDays = 7
        },
        new()
        {
            Id = 5,
            Client = Clients[4],
            Room = Rooms[3],
            CheckInDate = new DateTime(2026, 8, 15),
            NumberOfDays = 2
        },
        new()
        {
            Id = 6,
            Client = Clients[5],
            Room = Rooms[4],
            CheckInDate = new DateTime(2026, 8, 25),
            NumberOfDays = 6
        },
        new()
        {
            Id = 7,
            Client = Clients[6],
            Room = Rooms[4],
            CheckInDate = new DateTime(2026, 9, 3),
            NumberOfDays = 4
        },
        new()
        {
            Id = 8,
            Client = Clients[7],
            Room = Rooms[5],
            CheckInDate = new DateTime(2026, 8, 10),
            NumberOfDays = 10
        },
        new()
        {
            Id = 9,
            Client = Clients[8],
            Room = Rooms[5],
            CheckInDate = new DateTime(2026, 9, 4),
            NumberOfDays = 5
        },
        new()
        {
            Id = 10,
            Client = Clients[9],
            Room = Rooms[5],
            CheckInDate = new DateTime(2026, 9, 8),
            NumberOfDays = 3
        },
        new()
        {
            Id = 11,
            Client = Clients[0],
            Room = Rooms[6],
            CheckInDate = new DateTime(2026, 8, 1),
            NumberOfDays = 5
        },
        new()
        {
            Id = 12,
            Client = Clients[1],
            Room = Rooms[7],
            CheckInDate = new DateTime(2026, 8, 5),
            NumberOfDays = 4
        },
        new()
        {
            Id = 13,
            Client = Clients[2],
            Room = Rooms[8],
            CheckInDate = new DateTime(2026, 7, 20),
            NumberOfDays = 8
        },
        new()
        {
            Id = 14,
            Client = Clients[3],
            Room = Rooms[9],
            CheckInDate = new DateTime(2026, 9, 7),
            NumberOfDays = 6
        }
    ];
}
