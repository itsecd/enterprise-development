using Hotel.Domain.Entities;
using Hotel.Domain.Enums;

namespace Hotel.Domain.Data;

/// <summary>
/// Тестовые данные для гостиницы
/// </summary>
public class HotelSeed
{
    /// <summary>
	/// Список комнат
	/// </summary>
	public List<Room> Rooms { get; set; } = [];

    /// <summary>
	/// Список типов комнат
	/// </summary>
    public List<RoomType> RoomTypes { get; set; } = [];

    /// <summary>
	/// Список клиентов
	/// </summary>
    public List<Client> Clients { get; set; } = [];

    /// <summary>
	/// Список бронирований
	/// </summary>
    public List<Booking> Bookings { get; set; } = [];

    /// <summary>
    /// Создание объектов классов и заполнение списков
    /// </summary>
    public HotelSeed()
    {

        // Типы комнат
        var type1 = new RoomType { Id = 1, Category = RoomCategory.Economy, RoomArea = 8, BedCount = 1, HasBathroom = false, Price = 2500 };
        var type2 = new RoomType { Id = 2, Category = RoomCategory.Economy, RoomArea = 10, BedCount = 1, HasBathroom = false, Price = 2700 };
        var type3 = new RoomType { Id = 3, Category = RoomCategory.Standard, RoomArea = 20, BedCount = 1, HasBathroom = false, Price = 5000 };
        var type4 = new RoomType { Id = 4, Category = RoomCategory.Standard, RoomArea = 15, BedCount = 1, HasBathroom = false, Price = 4000 };
        var type5 = new RoomType { Id = 5, Category = RoomCategory.Luxe, RoomArea = 25, BedCount = 2, HasBathroom = true, Price = 11000 };
        var type6 = new RoomType { Id = 6, Category = RoomCategory.Luxe, RoomArea = 30, BedCount = 2, HasBathroom = true, Price = 12000 };
        var type7 = new RoomType { Id = 7, Category = RoomCategory.Business, RoomArea = 45, BedCount = 3, HasBathroom = true, Price = 18000 };
        var type8 = new RoomType { Id = 8, Category = RoomCategory.Business, RoomArea = 50, BedCount = 4, HasBathroom = true, Price = 19500 };
        var type9 = new RoomType { Id = 9, Category = RoomCategory.President, RoomArea = 90, BedCount = 2, HasBathroom = true, Price = 750000 };
        var type10 = new RoomType { Id = 10, Category = RoomCategory.President, RoomArea = 100, BedCount = 6, HasBathroom = true, Price = 650000 };

        RoomTypes.AddRange([type1, type2, type3, type4, type5, type6, type7, type8, type9, type10]);

        // Комнаты
        var room1 = new Room { Id = 1, Number = 1, Floor = 1, HasBalcony = false, RoomType = type1 };
        var room2 = new Room { Id = 2, Number = 3, Floor = 1, HasBalcony = false, RoomType = type1 };
        var room3 = new Room { Id = 3, Number = 4, Floor = 1, HasBalcony = false, RoomType = type2 };
        var room4 = new Room { Id = 4, Number = 5, Floor = 1, HasBalcony = false, RoomType = type3 };
        var room5 = new Room { Id = 5, Number = 6, Floor = 1, HasBalcony = false, RoomType = type4 };
        var room6 = new Room { Id = 6, Number = 7, Floor = 2, HasBalcony = true, RoomType = type4 };
        var room7 = new Room { Id = 7, Number = 8, Floor = 2, HasBalcony = false, RoomType = type5 };
        var room8 = new Room { Id = 8, Number = 10, Floor = 2, HasBalcony = false, RoomType = type5 };
        var room9 = new Room { Id = 9, Number = 11, Floor = 2, HasBalcony = true, RoomType = type6 };
        var room10 = new Room { Id = 10, Number = 13, Floor = 2, HasBalcony = false, RoomType = type7 };
        var room11 = new Room { Id = 11, Number = 14, Floor = 2, HasBalcony = true, RoomType = type7 };
        var room12 = new Room { Id = 12, Number = 15, Floor = 3, HasBalcony = true, RoomType = type8 };
        var room13 = new Room { Id = 13, Number = 16, Floor = 3, HasBalcony = true, RoomType = type9 };
        var room14 = new Room { Id = 14, Number = 17, Floor = 3, HasBalcony = true, RoomType = type9 };
        var room15 = new Room { Id = 15, Number = 18, Floor = 3, HasBalcony = true, RoomType = type10 };

        Rooms.AddRange([room1, room2, room3, room4, room5, room6, room7, room8, room9, room10, room11, room12, room13, room14, room15]);

        // Клиенты
        var client1 = new Client { Id = 1, LastName = "Клюева", FirstName = "Мария", Patronymic = "Николаевна", BirthDate = new DateTime(1990, 04, 12), NumberPassport = "1234 678901", Citizenship = "РФ" };
        var client2 = new Client { Id = 2, LastName = "Волоконов", FirstName = "Петр", Patronymic = "Васильевич", BirthDate = new DateTime(1967, 05, 27), NumberPassport = "3421 576230", Citizenship = "РФ" };
        var client3 = new Client { Id = 3, LastName = "Валеев", FirstName = "Виталий", Patronymic = "Владимирович", BirthDate = new DateTime(1994, 06, 08), NumberPassport = "6234 817462", Citizenship = "РФ" };
        var client4 = new Client { Id = 4, LastName = "Jolie", FirstName = "Angelina", BirthDate = new DateTime(1975, 06, 04), NumberPassport = "654783710", Citizenship = "USA" };
        var client5 = new Client { Id = 5, LastName = "Carrey", FirstName = "Jim", BirthDate = new DateTime(1962, 01, 17), NumberPassport = "934801843", Citizenship = "USA" };
        var client6 = new Client { Id = 6, LastName = "Киркоров", FirstName = "Филипп", Patronymic = "Бедросович", BirthDate = new DateTime(1967, 04, 30), NumberPassport = "4932 104581", Citizenship = "РФ" };
        var client7 = new Client { Id = 7, LastName = "Билан", FirstName = "Дмитрий", Patronymic = "Николаевич", BirthDate = new DateTime(1981, 12, 24), NumberPassport = "5293 104585", Citizenship = "РФ" };
        var client8 = new Client { Id = 8, LastName = "Безруков", FirstName = "Сергей", Patronymic = "Витальевич", BirthDate = new DateTime(1973, 10, 18), NumberPassport = "4823 184751", Citizenship = "РФ" };
        var client9 = new Client { Id = 9, LastName = "Jinping", FirstName = "Xi", BirthDate = new DateTime(1953, 06, 15), NumberPassport = "EA3819831", Citizenship = "CN" };
        var client10 = new Client { Id = 10, LastName = "Yoon", FirstName = "Suk-Yeol", BirthDate = new DateTime(1960, 12, 18), NumberPassport = "M48129384", Citizenship = "KR" };

        Clients.AddRange([client1, client2, client3, client4, client5, client6, client7, client8, client9, client10]);

        // Бронирования
        var booking1 = new Booking { Id = 1, DateArrival = new DateTime(2021, 05, 09), DayCount = 101, Client = client1, Room = room2 };
        var booking2 = new Booking { Id = 2, DateArrival = new DateTime(2025, 01, 02), DayCount = 7, Client = client6, Room = room10 };
        var booking3 = new Booking { Id = 3, DateArrival = new DateTime(2015, 10, 25), DayCount = 15, Client = client8, Room = room4 };
        var booking4 = new Booking { Id = 4, DateArrival = new DateTime(2009, 08, 19), DayCount = 31, Client = client4, Room = room1 };
        var booking5 = new Booking { Id = 5, DateArrival = new DateTime(2026, 02, 23), DayCount = 2, Client = client7, Room = room5 };
        var booking6 = new Booking { Id = 6, DateArrival = new DateTime(2017, 09, 03), DayCount = 45, Client = client3, Room = room2 };
        var booking7 = new Booking { Id = 7, DateArrival = new DateTime(2022, 11, 04), DayCount = 5, Client = client10, Room = room8 };
        var booking8 = new Booking { Id = 8, DateArrival = new DateTime(2001, 12, 31), DayCount = 18, Client = client2, Room = room15 };
        var booking9 = new Booking { Id = 9, DateArrival = new DateTime(2007, 03, 14), DayCount = 60, Client = client5, Room = room6 };
        var booking10 = new Booking { Id = 10, DateArrival = new DateTime(2011, 04, 01), DayCount = 365, Client = client10, Room = room14 };
        var booking11 = new Booking { Id = 11, DateArrival = new DateTime(1972, 09, 05), DayCount = 28, Client = client9, Room = room13 };
        var booking12 = new Booking { Id = 12, DateArrival = new DateTime(2013, 10, 29), DayCount = 12, Client = client3, Room = room9 };
        var booking13 = new Booking { Id = 13, DateArrival = new DateTime(2019, 04, 13), DayCount = 1, Client = client3, Room = room5 };
        var booking14 = new Booking { Id = 14, DateArrival = new DateTime(2019, 03, 29), DayCount = 50, Client = client7, Room = room3 };
        var booking15 = new Booking { Id = 15, DateArrival = new DateTime(2024, 11, 03), DayCount = 4, Client = client6, Room = room7 };
        var booking16 = new Booking { Id = 16, DateArrival = new DateTime(2023, 05, 14), DayCount = 10, Client = client9, Room = room11 };
        var booking17 = new Booking { Id = 17, DateArrival = new DateTime(2021, 01, 26), DayCount = 2, Client = client10, Room = room12 };
        var booking18 = new Booking { Id = 18, DateArrival = new DateTime(2021, 09, 10), DayCount = 11, Client = client4, Room = room15 };
        var booking19 = new Booking { Id = 19, DateArrival = new DateTime(2021, 02, 15), DayCount = 21, Client = client1, Room = room6 };
        var booking20 = new Booking { Id = 20, DateArrival = new DateTime(2021, 12, 19), DayCount = 60, Client = client2, Room = room15 };

        Bookings.AddRange([booking1, booking2, booking3, booking4, booking5, booking6, booking7, booking8, booking9, booking10, booking11, booking12, booking13, booking14, booking15, booking16, booking17, booking18, booking19, booking20]);
    }
}
