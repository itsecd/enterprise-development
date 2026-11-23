using Hotel.Domain.Entities;
using Hotel.Domain.Shared.Enums;

namespace Hotel.Domain.Data;

/// <summary>
/// 
/// </summary>
public static class TestData
{
    /// <summary>
    /// Типы гостиничных номеров
    /// </summary>
    public static List<RoomType> RoomTypes { get; } =
    [
        new()
        {
            Id = 1,
            Category = RoomCategory.Economy,
            Area = 15,
            Beds = 1,
            HasBath = false,
            HasShower = false,
            DayPrice = 2000
        },
        new()
        {
            Id = 2,
            Category = RoomCategory.Economy,
            Area = 19,
            Beds = 1,
            HasBath = false,
            HasShower = true,
            DayPrice = 2600
        },
        new()
        {
            Id = 3,
            Category = RoomCategory.Standart,
            Area = 22,
            Beds = 1,
            HasBath = false,
            HasShower = true,
            DayPrice = 3000
        },
        new()
        {
            Id = 4,
            Category = RoomCategory.Standart,
            Area = 25,
            Beds = 1,
            HasBath = true,
            HasShower = true,
            DayPrice = 3200
        },
        new()
        {
            Id = 5,
            Category = RoomCategory.Standart,
            Area = 28,
            Beds = 2,
            HasBath = true,
            HasShower = true,
            DayPrice = 3600
        },
        new()
        {
            Id = 6,
            Category = RoomCategory.Delux,
            Area = 32,
            Beds = 1,
            HasBath = true,
            HasShower = true,
            DayPrice = 4000
        },
        new()
        {
            Id = 7,
            Category = RoomCategory.Delux,
            Area = 35,
            Beds = 2,
            HasBath = true,
            HasShower = true,
            DayPrice = 4800
        },
        new()
        {
            Id = 8,
            Category = RoomCategory.Suite,
            Area = 40,
            Beds = 2,
            HasBath = true,
            HasShower = true,
            DayPrice = 6000
        },
        new()
        {
            Id = 9,
            Category = RoomCategory.Suite,
            Area = 50,
            Beds = 3,
            HasBath = true,
            HasShower = true,
            DayPrice = 8000
        },
        new()
        {
            Id = 10,
            Category = RoomCategory.Residence,
            Area = 65,
            Beds = 3,
            HasBath = true,
            HasShower = true,
            DayPrice = 12000
        }
    ];

    /// <summary>
    /// Список номеров
    /// </summary>
    public static List<Room> Rooms { get; } =
    [
        new() { Id = 1, Number = 11, Floor = 1, HasBalcony = false, Type = RoomTypes[0] },
        new() { Id = 2, Number = 21, Floor = 2, HasBalcony = true, Type = RoomTypes[1] },
        new() { Id = 3, Number = 12, Floor = 1, HasBalcony = false, Type = RoomTypes[2] },
        new() { Id = 4, Number = 13, Floor = 1, HasBalcony = false, Type = RoomTypes[3] },
        new() { Id = 5, Number = 22, Floor = 2, HasBalcony = true, Type = RoomTypes[4] },
        new() { Id = 6, Number = 23, Floor = 3, HasBalcony = true, Type = RoomTypes[5] },
        new() { Id = 7, Number = 31, Floor = 3, HasBalcony = true, Type = RoomTypes[6] },
        new() { Id = 8, Number = 11, Floor = 2, HasBalcony = true, Type = RoomTypes[7] },
        new() { Id = 9, Number = 41, Floor = 4, HasBalcony = true, Type = RoomTypes[8] },
        new() { Id = 10, Number = 42, Floor = 4, HasBalcony = true, Type = RoomTypes[9] }
    ];

    /// <summary>
    /// Список клиентов
    /// </summary>
    public static List<Client> Clients { get; } =
    [
        new()
        {
            Id = 1,
            PassportNumber = "40 10 200123",
            Name = "Николай",
            Surname = "Степанов",
            Patronymic = "Васильевич",
            BirthDate = new DateOnly(1965, 2, 15),
            Citizenship = "Российская Федерация"
        },
        new()
        {
            Id = 2,
            PassportNumber = "45 25 564209",
            Name = "София",
            Surname = "Оленцова",
            Patronymic = "Сергеевна",
            BirthDate = new DateOnly(2005, 1, 17),
            Citizenship = "Российская Федерация"
        },
        new()
        {
            Id = 3,
            PassportNumber = "60 23 123765",
            Name = "Сергей",
            Surname = "Соколов",
            Patronymic = "Олегович",
            BirthDate = new DateOnly(1978, 5, 14),
            Citizenship = "Российская Федерация"
        },
        new()
        {
            Id = 4,
            PassportNumber = "69 11 142836",
            Name = "Генадий",
            Surname = "Воробьёв",
            Patronymic = "Петрович",
            BirthDate = new DateOnly(1991, 8, 26),
            Citizenship = "Российская Федерация"
        },
        new()
        {
            Id = 5,
            PassportNumber = "67 20 133765",
            Name = "Ираида",
            Surname = "Коновалова",
            Patronymic = "Владимировна",
            BirthDate = new DateOnly(1975, 12, 12),
            Citizenship = "Российская Федерация"
        },
        new()
        {
            Id = 6,
            PassportNumber = "N81245210",
            Name = "Бахыт",
            Surname = "Мустафин",
            Patronymic = "Омарулы",
            BirthDate = new DateOnly(2001, 10, 4),
            Citizenship = "Республика Казахстан"
        },
        new()
        {
            Id = 7,
            PassportNumber = "N24715589",
            Name = "Айару",
            Surname = "Богенбай",
            Patronymic = "Мадикызы",
            BirthDate = new DateOnly(1987, 9, 8),
            Citizenship = "Республика Казахстан"
        },
        new()
        {
            Id = 8,
            PassportNumber = "N37281641",
            Name = "Нурмагамбет",
            Surname = "Шопак",
            BirthDate = new DateOnly(2004, 5, 29),
            Citizenship = "Республика Казахстан"
        },
        new()
        {
            Id = 9,
            PassportNumber = "AE574690",
            Name = "Виталий",
            Surname = "Кожемятько",
            Patronymic = "Тарасович",
            BirthDate = new DateOnly(1994, 2, 1),
            Citizenship = "Украина"
        },
        new()
        {
            Id = 10,
            PassportNumber = "BB591645",
            Name = "Аксана",
            Surname = "Коваленко",
            Patronymic = "Петровна",
            BirthDate = new DateOnly(2000, 5, 30),
            Citizenship = "Украина"
        }
    ];

    /// <summary>
    /// Список бронирований номеров
    /// </summary>
    public static List<Booking> Bookings { get; } =
    [
        new()
        {
            Id = 1,
            Client = Clients[1],
            Room = Rooms[0],
            CheckInDate = new DateOnly(2026, 2, 13),
            DaysOfStay = 2
        },
        new()
        {
            Id = 2,
            Client = Clients[0],
            Room = Rooms[6],
            CheckInDate = new DateOnly(2026, 1, 25),
            DaysOfStay = 5
        },
        new()
        {
            Id = 3,
            Client = Clients[7],
            Room = Rooms[3],
            CheckInDate = new DateOnly(2026, 1, 22),
            DaysOfStay = 4
        },
        new()
        {
            Id = 4,
            Client = Clients[2],
            Room = Rooms[2],
            CheckInDate = new DateOnly(2026, 3, 10),
            DaysOfStay = 3
        },
        new()
        {
            Id = 5,
            Client = Clients[8],
            Room = Rooms[0],
            CheckInDate = new DateOnly(2026, 4, 15),
            DaysOfStay = 1
        },
        new()
        {
            Id = 6,
            Client = Clients[4],
            Room = Rooms[9],
            CheckInDate = new DateOnly(2025, 12, 28),
            DaysOfStay = 7
        },
        new()
        {
            Id = 7,
            Client = Clients[9],
            Room = Rooms[1],
            CheckInDate = new DateOnly(2025, 6, 10),
            DaysOfStay = 2
        },
        new()
        {
            Id = 8,
            Client = Clients[3],
            Room = Rooms[2],
            CheckInDate = new DateOnly(2026, 4, 14),
            DaysOfStay = 3
        },
        new()
        {
            Id = 9,
            Client = Clients[6],
            Room = Rooms[4],
            CheckInDate = new DateOnly(2025, 11, 25),
            DaysOfStay = 5
        },
        new()
        {
            Id = 10,
            Client = Clients[5],
            Room = Rooms[3],
            CheckInDate = new DateOnly(2026, 9, 1),
            DaysOfStay = 4
        },
        new()
        {
            Id = 11,
            Client = Clients[0],
            Room = Rooms[6],
            CheckInDate = new DateOnly(2026, 7, 21),
            DaysOfStay = 3
        },
        new()
        {
            Id = 12,
            Client = Clients[4],
            Room = Rooms[6],
            CheckInDate = new DateOnly(2026, 3, 31),
            DaysOfStay = 4
        },
        new()
        {
            Id = 13,
            Client = Clients[7],
            Room = Rooms[5],
            CheckInDate = new DateOnly(2026, 2, 12),
            DaysOfStay = 5
        },
        new()
        {
            Id = 14,
            Client = Clients[5],
            Room = Rooms[5],
            CheckInDate = new DateOnly(2026, 3, 17),
            DaysOfStay = 5
        },
        new()
        {
            Id = 15,
            Client = Clients[2],
            Room = Rooms[5],
            CheckInDate = new DateOnly(2026, 8, 3),
            DaysOfStay = 5
        }
    ];
}
