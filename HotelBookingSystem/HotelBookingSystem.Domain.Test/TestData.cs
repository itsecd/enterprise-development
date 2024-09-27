namespace HotelBookingSystem.Domain.Test;

/// <summary>
/// Responsible for providing test data
/// </summary>
public class TestData
{
    public List<Hotel> Hotels =
    [
        new(){ Id = 1, Name = "Alpha", City = "Moscow", Address = "Lenina St, 1" },
        new(){ Id = 2, Name = "Bravo", City = "St Petersburg", Address = "Nevsky Ave, 123" },
        new(){ Id = 3, Name = "Charlie", City = "St Petersburg", Address = "Nevskaya St, 45" },
        new(){ Id = 4, Name = "Delta", City = "Samara", Address = "Leninskaya St, 111" },
        new(){ Id = 5, Name = "Echo", City = "Samara", Address = "Panova St, 12" },
        new(){ Id = 6, Name = "Foxtrot", City = "Moscow", Address = "Samarskaya St, 34" },
        new(){ Id = 7, Name = "Golf", City = "Moscow", Address = "Pushkinskaya St, 56" },
    ];

    public List<Room> Rooms =
    [
        new(){ Id = 0, TypeRoom = "standard", Number = 10, Price = 1000, HotelId = 1 },
        new(){ Id = 1, TypeRoom = "superior", Number = 5, Price = 1700, HotelId = 2 },
        new(){ Id = 2, TypeRoom = "standard", Number = 2, Price = 1200, HotelId = 3 },
        new(){ Id = 3, TypeRoom = "superior", Number = 7, Price = 1900, HotelId = 1 },
        new(){ Id = 4, TypeRoom = "suite", Number = 3, Price = 2500, HotelId = 4 },
        new(){ Id = 5, TypeRoom = "standard", Number = 6, Price = 1300, HotelId = 2 },
        new(){ Id = 6, TypeRoom = "suite", Number = 2, Price = 2700, HotelId = 5 },
        new(){ Id = 7, TypeRoom = "superior", Number = 10, Price = 1800, HotelId = 6 },
        new(){ Id = 8, TypeRoom = "standard", Number = 8, Price = 1000, HotelId = 7 },
        new(){ Id = 9, TypeRoom = "suite", Number = 4, Price = 2600, HotelId = 5 },
        new(){ Id = 10, TypeRoom = "standard", Number = 3, Price = 1150, HotelId = 7 },
    ]; 

    public List<HotelClient> HotelClients =
    [
        new(){ Id = 0, Passport = 1111, Name = "John", Surname = "Sidorov", Patronymic = "Sergeevich", Birthdate = new DateOnly(1990, 01, 01) },
        new(){ Id = 1, Passport = 2222, Name = "Peter", Surname = "Chelaev", Patronymic = "Mikhailovich", Birthdate = new DateOnly(1992, 02, 02) },
        new(){ Id = 2, Passport = 3333, Name = "Sergey", Surname = "Morozov", Patronymic = "Ivanovich", Birthdate = new DateOnly(1995, 03, 03) },
        new(){ Id = 3, Passport = 4444, Name = "Anna", Surname = "Shcherbakova", Patronymic = "Nikitichna", Birthdate = new DateOnly(1998, 04, 04) },
        new(){ Id = 4, Passport = 5555, Name = "Michael", Surname = "Popov", Patronymic = "Andreevich", Birthdate = new DateOnly(2000, 05, 05) },
        new(){ Id = 5, Passport = 6666, Name = "Elena", Surname = "Vasina", Patronymic = "Aleksandrovna", Birthdate = new DateOnly(2002, 06, 06) },
    ];

    public List<BookedRoom> BookedRooms;

    public TestData()
    {
        BookedRooms =
        [
            new BookedRoom { Id = 0, ClientId = 0, RoomId = 0, EntryDate = new DateOnly(2024, 01, 01), 
                DepartureDate = new DateOnly(2024, 01, 03), BookingPeriod = new TimeSpan(2, 0, 0, 0) },
            new BookedRoom { Id = 1, ClientId = 1, RoomId = 1, EntryDate = new DateOnly(2024, 02, 05), 
                DepartureDate = new DateOnly(2024, 02, 10), BookingPeriod = new TimeSpan(5, 0, 0, 0) },
            new BookedRoom { Id = 2, ClientId = 2, RoomId = 2, EntryDate = new DateOnly(2024, 03, 15), 
                DepartureDate = new DateOnly(2024, 03, 20), BookingPeriod = new TimeSpan(5, 0, 0, 0) },
            new BookedRoom { Id = 3, ClientId = 3, RoomId = 3, EntryDate = new DateOnly(2024, 04, 01),
                DepartureDate = new DateOnly(2024, 04, 05), BookingPeriod = new TimeSpan(4, 0, 0, 0) },
            new BookedRoom { Id = 4, ClientId = 4, RoomId = 4, EntryDate = new DateOnly(2024, 05, 10), 
                DepartureDate = new DateOnly(2024, 05, 15), BookingPeriod = new TimeSpan(5, 0, 0, 0) },
            new BookedRoom { Id = 5, ClientId = 5, RoomId = 5, EntryDate = new DateOnly(2024, 06, 01), 
                DepartureDate = new DateOnly(2024, 06, 03), BookingPeriod = new TimeSpan(2, 0, 0, 0) },
            new BookedRoom { Id = 6, ClientId = 1, RoomId = 6, EntryDate = new DateOnly(2024, 06, 01),
                DepartureDate = new DateOnly(2024, 06, 03), BookingPeriod = new TimeSpan(2, 0, 0, 0) },
            new BookedRoom { Id = 7, ClientId = 3, RoomId = 7, EntryDate = new DateOnly(2024, 06, 01),
                DepartureDate = new DateOnly(2024, 06, 03), BookingPeriod = new TimeSpan(2, 0, 0, 0) },
        ];

        HotelClients[0].BookedRooms = [BookedRooms[0]];
        HotelClients[1].BookedRooms = [BookedRooms[1], BookedRooms[6]];
        HotelClients[2].BookedRooms = [BookedRooms[2]];
        HotelClients[3].BookedRooms = [BookedRooms[3], BookedRooms[7]];
        HotelClients[4].BookedRooms = [BookedRooms[4]];
        HotelClients[5].BookedRooms = [BookedRooms[5]];

        Rooms[0].BookedRooms = [BookedRooms[0]];
        Rooms[1].BookedRooms = [BookedRooms[1]];
        Rooms[2].BookedRooms = [BookedRooms[2]];
        Rooms[3].BookedRooms = [BookedRooms[3]];
        Rooms[4].BookedRooms = [BookedRooms[4]];
        Rooms[5].BookedRooms = [BookedRooms[5]];
        Rooms[6].BookedRooms = [BookedRooms[6]];
        Rooms[7].BookedRooms = [BookedRooms[7]];
    }
}