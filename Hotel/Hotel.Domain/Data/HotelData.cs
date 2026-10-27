using Hotel.Domain.Entities;
using Hotel.Domain.Shared.Enums;

namespace Hotel.Domain.Data;

/// <summary>
/// Тестовые данные гостиницы
/// </summary>
public static class HotelData
{
    /// <summary>
    /// Список типов номеров гостиницы
    /// </summary>
    public static IReadOnlyList<RoomType> RoomTypes { get; } =
    [
        new RoomType { Id = 1, Category = RoomCategory.Economy, Area = 18, BedCount = 1, HasBathroom = true, PricePerDay = 2500 },
        new RoomType { Id = 2, Category = RoomCategory.Economy, Area = 22, BedCount = 2, HasBathroom = false, PricePerDay = 3000 },
        new RoomType { Id = 3, Category = RoomCategory.Standard, Area = 25, BedCount = 2, HasBathroom = true, PricePerDay = 4000 },
        new RoomType { Id = 4, Category = RoomCategory.Standard, Area = 28, BedCount = 2, HasBathroom = true, PricePerDay = 4500 },
        new RoomType { Id = 5, Category = RoomCategory.Standard, Area = 30, BedCount = 3, HasBathroom = false, PricePerDay = 5000 },
        new RoomType { Id = 6, Category = RoomCategory.Comfort, Area = 32, BedCount = 2, HasBathroom = true, PricePerDay = 5500 },
        new RoomType { Id = 7, Category = RoomCategory.Comfort, Area = 38, BedCount = 3, HasBathroom = false, PricePerDay = 6500 },
        new RoomType { Id = 8, Category = RoomCategory.Comfort, Area = 40, BedCount = 4, HasBathroom = true, PricePerDay = 7000 },
        new RoomType { Id = 9, Category = RoomCategory.Luxury, Area = 45, BedCount = 2, HasBathroom = false, PricePerDay = 8000 },
        new RoomType { Id = 10, Category = RoomCategory.Luxury, Area = 55, BedCount = 2, HasBathroom = true, PricePerDay = 10000 }
    ];

    /// <summary>
    /// Список номеров гостиницы
    /// </summary>
    public static IReadOnlyList<Room> Rooms { get; } =
    [
        new Room { Id = 1, RoomNumber = 101, Floor = 1, HasBalcony = false, RoomType = RoomTypes[0] },
        new Room { Id = 2, RoomNumber = 102, Floor = 1, HasBalcony = true, RoomType = RoomTypes[1] },
        new Room { Id = 3, RoomNumber = 201, Floor = 2, HasBalcony = false, RoomType = RoomTypes[2] },
        new Room { Id = 4, RoomNumber = 202, Floor = 2, HasBalcony = true, RoomType = RoomTypes[3] },
        new Room { Id = 5, RoomNumber = 301, Floor = 3, HasBalcony = true, RoomType = RoomTypes[4] },
        new Room { Id = 6, RoomNumber = 302, Floor = 3, HasBalcony = false, RoomType = RoomTypes[5] },
        new Room { Id = 7, RoomNumber = 401, Floor = 4, HasBalcony = true, RoomType = RoomTypes[6] },
        new Room { Id = 8, RoomNumber = 402, Floor = 4, HasBalcony = true, RoomType = RoomTypes[7] },
        new Room { Id = 9, RoomNumber = 501, Floor = 5, HasBalcony = false, RoomType = RoomTypes[8] },
        new Room { Id = 10, RoomNumber = 502, Floor = 5, HasBalcony = true, RoomType = RoomTypes[9] }
    ];

    /// <summary>
    /// Список клиентов гостиницы
    /// </summary>
    public static IReadOnlyList<HotelClient> Clients { get; } =
    [
        new HotelClient { Id = 1, PassportNumber = "AA123456", FirstName = "Alexey", LastName = "Ivanov", BirthDate = new DateOnly(1990, 5, 12), Citizenship = "Russia" },
        new HotelClient { Id = 2, PassportNumber = "BB234567", FirstName = "Anna", LastName = "Petrova", BirthDate = new DateOnly(1988, 3, 20), Citizenship = "Russia" },
        new HotelClient { Id = 3, PassportNumber = "CC345678", FirstName = "Dmitry", LastName = "Smirnov", BirthDate = new DateOnly(1995, 7, 8), Citizenship = "Russia" },
        new HotelClient { Id = 4, PassportNumber = "DD456789", FirstName = "Elena", LastName = "Kuznetsova", BirthDate = new DateOnly(1992, 11, 15), Citizenship = "Russia" },
        new HotelClient { Id = 5, PassportNumber = "EE567890", FirstName = "Ivan", LastName = "Popov", BirthDate = new DateOnly(1985, 1, 25), Citizenship = "Russia" },
        new HotelClient { Id = 6, PassportNumber = "FF678901", FirstName = "Maria", LastName = "Sokolova", BirthDate = new DateOnly(1998, 6, 10), Citizenship = "Russia" },
        new HotelClient { Id = 7, PassportNumber = "GG789012", FirstName = "Nikolay", LastName = "Fedorov", BirthDate = new DateOnly(1987, 9, 3), Citizenship = "Russia" },
        new HotelClient { Id = 8, PassportNumber = "HH890123", FirstName = "Olga", LastName = "Morozova", BirthDate = new DateOnly(1993, 12, 18), Citizenship = "Russia" },
        new HotelClient { Id = 9, PassportNumber = "II901234", FirstName = "Pavel", LastName = "Volkov", BirthDate = new DateOnly(1991, 4, 7), Citizenship = "Russia" },
        new HotelClient { Id = 10, PassportNumber = "JJ012345", FirstName = "Sofia", LastName = "Orlova", BirthDate = new DateOnly(1996, 8, 22), Citizenship = "Russia" },
        new HotelClient { Id = 11, PassportNumber = "AB987654", FirstName = "John", LastName = "Smith", BirthDate = new DateOnly(1989, 2, 14), Citizenship = "USA" }
    ];

    /// <summary>
    /// Список бронирований номеров гостиницы
    /// </summary>
    public static IReadOnlyList<Booking> Bookings { get; } =
    [
        new Booking { Id = 1, Client = Clients[0], Room = Rooms[6], CheckInDate = new DateOnly(2026, 9, 1), DaysCount = 3 },
        new Booking { Id = 2, Client = Clients[1], Room = Rooms[6], CheckInDate = new DateOnly(2026, 9, 4), DaysCount = 5 },
        new Booking { Id = 3, Client = Clients[2], Room = Rooms[6], CheckInDate = new DateOnly(2026, 9, 9), DaysCount = 2 },
        new Booking { Id = 4, Client = Clients[3], Room = Rooms[6], CheckInDate = new DateOnly(2026, 9, 11), DaysCount = 4 },
        new Booking { Id = 5, Client = Clients[4], Room = Rooms[6], CheckInDate = new DateOnly(2026, 9, 15), DaysCount = 7 },
        new Booking { Id = 6, Client = Clients[5], Room = Rooms[2], CheckInDate = new DateOnly(2026, 8, 1), DaysCount = 3 },
        new Booking { Id = 7, Client = Clients[6], Room = Rooms[2], CheckInDate = new DateOnly(2026, 8, 4), DaysCount = 6 },
        new Booking { Id = 8, Client = Clients[7], Room = Rooms[2], CheckInDate = new DateOnly(2026, 8, 10), DaysCount = 2 },
        new Booking { Id = 9, Client = Clients[8], Room = Rooms[2], CheckInDate = new DateOnly(2026, 8, 12), DaysCount = 5 },
        new Booking { Id = 10, Client = Clients[9], Room = Rooms[9], CheckInDate = new DateOnly(2026, 7, 1), DaysCount = 4 },
        new Booking { Id = 11, Client = Clients[0], Room = Rooms[9], CheckInDate = new DateOnly(2026, 7, 5), DaysCount = 3 },
        new Booking { Id = 12, Client = Clients[1], Room = Rooms[9], CheckInDate = new DateOnly(2026, 7, 8), DaysCount = 2 },
        new Booking { Id = 13, Client = Clients[2], Room = Rooms[1], CheckInDate = new DateOnly(2026, 6, 1), DaysCount = 4 },
        new Booking { Id = 14, Client = Clients[3], Room = Rooms[1], CheckInDate = new DateOnly(2026, 6, 5), DaysCount = 3 },
        new Booking { Id = 15, Client = Clients[4], Room = Rooms[4], CheckInDate = new DateOnly(2026, 5, 1), DaysCount = 6 },
        new Booking { Id = 16, Client = Clients[5], Room = Rooms[4], CheckInDate = new DateOnly(2026, 5, 7), DaysCount = 3 },
        new Booking { Id = 17, Client = Clients[6], Room = Rooms[0], CheckInDate = new DateOnly(2026, 4, 1), DaysCount = 2 },
        new Booking { Id = 18, Client = Clients[7], Room = Rooms[3], CheckInDate = new DateOnly(2026, 4, 10), DaysCount = 4 },
        new Booking { Id = 19, Client = Clients[8], Room = Rooms[5], CheckInDate = new DateOnly(2026, 3, 1), DaysCount = 5 },
        new Booking { Id = 20, Client = Clients[9], Room = Rooms[7], CheckInDate = new DateOnly(2026, 3, 10), DaysCount = 2 },
        new Booking { Id = 21, Client = Clients[0], Room = Rooms[3], CheckInDate = new DateOnly(2026, 8, 5), DaysCount = 4 },
        new Booking { Id = 22, Client = Clients[1], Room = Rooms[5], CheckInDate = new DateOnly(2026, 8, 3), DaysCount = 7 }
    ];
}