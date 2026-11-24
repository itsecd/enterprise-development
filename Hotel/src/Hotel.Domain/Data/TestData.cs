using Hotel.Domain.Enums;
using Hotel.Domain.Models;

namespace Hotel.Domain.Data;

/// <summary>
/// Генератор тестовых данных для гостиницы
/// </summary>
public class TestData
{
    /// <summary>
    /// Гости гостиницы
    /// </summary>
    public List<Client> Clients { get; } =
    [
        new() { Id = 1, PassportNumber = "4510 123456", FullName = "Алексеев Алексей Алексеевич", BirthDate = new DateTime(1980, 5, 12), Citizenship = "РФ" },
        new() { Id = 2, PassportNumber = "4511 234567", FullName = "Борисов Борис Борисович", BirthDate = new DateTime(1992, 8, 23), Citizenship = "РФ" },
        new() { Id = 3, PassportNumber = "4512 345678", FullName = "Волкова Виктория Владимировна", BirthDate = new DateTime(1988, 11, 3), Citizenship = "РФ" },
        new() { Id = 4, PassportNumber = "4513 456789", FullName = "Григорьев Григорий Григорьевич", BirthDate = new DateTime(1975, 2, 14), Citizenship = "РФ" },
        new() { Id = 5, PassportNumber = "4514 567890", FullName = "Дмитриева Дарья Дмитриевна", BirthDate = new DateTime(1995, 7, 30), Citizenship = "РФ" },
        new() { Id = 6, PassportNumber = "4515 678901", FullName = "Егоров Егор Егорович", BirthDate = new DateTime(1983, 9, 15), Citizenship = "РФ" },
        new() { Id = 7, PassportNumber = "4516 789012", FullName = "Жукова Жанна Жановна", BirthDate = new DateTime(1990, 12, 5), Citizenship = "РФ" },
        new() { Id = 8, PassportNumber = "4517 890123", FullName = "Зайцев Захар Захарович", BirthDate = new DateTime(1979, 4, 18), Citizenship = "РФ" },
        new() { Id = 9, PassportNumber = "4518 901234", FullName = "Иванова Ирина Игоревна", BirthDate = new DateTime(1998, 1, 22), Citizenship = "РФ" },
        new() { Id = 10, PassportNumber = "4519 012345", FullName = "Козлов Константин Кириллович", BirthDate = new DateTime(1986, 6, 9), Citizenship = "РФ" }
    ];

    /// <summary>
    /// Справочник типов номеров
    /// </summary>
    public List<RoomType> RoomTypes { get; } =
    [
        new() { Id = 1, Category = RoomCategory.Economy, Area = 14, BedCount = 1, HasBathroom = true, PricePerNight = 1200m },
        new() { Id = 2, Category = RoomCategory.Standard, Area = 20, BedCount = 2, HasBathroom = true, PricePerNight = 2200m },
        new() { Id = 3, Category = RoomCategory.Luxury, Area = 35, BedCount = 2, HasBathroom = true, PricePerNight = 4500m },
        new() { Id = 4, Category = RoomCategory.Apartment, Area = 55, BedCount = 3, HasBathroom = true, PricePerNight = 7500m },
        new() { Id = 5, Category = RoomCategory.Economy, Area = 10, BedCount = 1, HasBathroom = true, PricePerNight = 800m },
        new() { Id = 6, Category = RoomCategory.Standard, Area = 24, BedCount = 2, HasBathroom = true, PricePerNight = 2800m },
        new() { Id = 7, Category = RoomCategory.Luxury, Area = 45, BedCount = 3, HasBathroom = true, PricePerNight = 6000m },
        new() { Id = 8, Category = RoomCategory.Standard, Area = 18, BedCount = 1, HasBathroom = true, PricePerNight = 1900m },
        new() { Id = 9, Category = RoomCategory.Economy, Area = 16, BedCount = 2, HasBathroom = true, PricePerNight = 1600m },
        new() { Id = 10, Category = RoomCategory.Luxury, Area = 50, BedCount = 4, HasBathroom = true, PricePerNight = 8000m }
    ];

    /// <summary>
    /// Комнаты, принадлежащие предопределённым категориям
    /// </summary>
    public List<Room> Rooms { get; }

    /// <summary>
    /// Бронирования, связанные с клиентами и комнатами в этом наборе данных
    /// </summary>
    public List<Booking> Bookings { get; }

    /// <summary>
    /// Создаёт 10 клиентов, 10 типов, 10 номеров и 13 бронирований
    /// </summary>
    public TestData()
    {
        Rooms =
        [
            new() { Id = 1, RoomNumber = "101", Floor = 1, HasBalcony = false, Type = RoomTypes[0] },
            new() { Id = 2, RoomNumber = "102", Floor = 1, HasBalcony = true, Type = RoomTypes[1] },
            new() { Id = 3, RoomNumber = "201", Floor = 2, HasBalcony = false, Type = RoomTypes[2] },
            new() { Id = 4, RoomNumber = "202", Floor = 2, HasBalcony = true, Type = RoomTypes[3] },
            new() { Id = 5, RoomNumber = "301", Floor = 3, HasBalcony = false, Type = RoomTypes[4] },
            new() { Id = 6, RoomNumber = "302", Floor = 3, HasBalcony = true, Type = RoomTypes[5] },
            new() { Id = 7, RoomNumber = "401", Floor = 4, HasBalcony = false, Type = RoomTypes[6] },
            new() { Id = 8, RoomNumber = "402", Floor = 4, HasBalcony = true, Type = RoomTypes[7] },
            new() { Id = 9, RoomNumber = "501", Floor = 5, HasBalcony = false, Type = RoomTypes[8] },
            new() { Id = 10, RoomNumber = "502", Floor = 5, HasBalcony = true, Type = RoomTypes[9] }
        ];

        Bookings =
        [
            new() { Id = 1, Client = Clients[0], Room = Rooms[0], CheckInDate = new DateTime(2025, 3, 8), Nights = 5 },
            new() { Id = 2, Client = Clients[1], Room = Rooms[5], CheckInDate = new DateTime(2025, 3, 9), Nights = 3 },
            new() { Id = 3, Client = Clients[0], Room = Rooms[0], CheckInDate = new DateTime(2025, 1, 10), Nights = 2 },
            new() { Id = 4, Client = Clients[2], Room = Rooms[0], CheckInDate = new DateTime(2025, 2, 15), Nights = 4 },
            new() { Id = 5, Client = Clients[3], Room = Rooms[1], CheckInDate = new DateTime(2025, 1, 20), Nights = 3 },
            new() { Id = 6, Client = Clients[4], Room = Rooms[1], CheckInDate = new DateTime(2025, 2, 25), Nights = 1 },
            new() { Id = 7, Client = Clients[5], Room = Rooms[2], CheckInDate = new DateTime(2025, 1, 5), Nights = 7 },
            new() { Id = 8, Client = Clients[6], Room = Rooms[2], CheckInDate = new DateTime(2025, 2, 5), Nights = 2 },
            new() { Id = 9, Client = Clients[7], Room = Rooms[3], CheckInDate = new DateTime(2025, 1, 15), Nights = 1 },
            new() { Id = 10, Client = Clients[8], Room = Rooms[4], CheckInDate = new DateTime(2025, 2, 20), Nights = 6 },
            new() { Id = 11, Client = Clients[9], Room = Rooms[6], CheckInDate = new DateTime(2025, 1, 25), Nights = 10 },
            new() { Id = 12, Client = Clients[2], Room = Rooms[7], CheckInDate = new DateTime(2025, 2, 10), Nights = 4 },
            new() { Id = 13, Client = Clients[3], Room = Rooms[8], CheckInDate = new DateTime(2025, 1, 30), Nights = 2 }
        ];
    }
}