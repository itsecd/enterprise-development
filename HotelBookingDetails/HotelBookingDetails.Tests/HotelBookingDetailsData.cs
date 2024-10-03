using HotelBookingDetails.Domain;


namespace HotelBookingDetails.Tests;

public class HotelBookingDetailsData
{
    public List<TypeRoom> TypesRoom =
    [
        new(){ Id = 1,  Name = "Luxary"},
        new(){ Id = 2,  Name = "Economy"},
        new(){ Id = 3,  Name = "Comfort"},
        new(){ Id = 4,  Name = "Comfort+"},
    ];

    public List<Passport> Passports =
    [
        new(){Id = 1, Number = 123421, Series = 6313},
        new(){Id = 2, Number = 567890, Series = 1234},
        new(){Id = 3, Number = 987654, Series = 5678},
        new(){Id = 4, Number = 234567, Series = 7890},
        new(){Id = 5, Number = 876543, Series = 3456},
        new(){Id = 6, Number = 345678, Series = 9012},
        new(){Id = 7, Number = 765432, Series = 2345},
        new(){Id = 8, Number = 654321, Series = 6789},
        new(){Id = 9, Number = 123456, Series = 4321},
        new(){Id = 10, Number = 456789, Series = 0987},
        new(){Id = 11, Number = 789012, Series = 8765},
        new(){Id = 12, Number = 345678, Series = 6543},
        new(){Id = 13, Number = 890123, Series = 2109},
        new(){Id = 14, Number = 456789, Series = 8765},
        new(){Id = 15, Number = 321098, Series = 5432},
        new(){Id = 16, Number = 901234, Series = 6789},
        new(){Id = 17, Number = 345678, Series = 1234},
        new(){Id = 18, Number = 567890, Series = 9876},
        new(){Id = 19, Number = 789012, Series = 3456},
        new(){Id = 20, Number = 890123, Series = 6789},

    ];


    public List<Room> Rooms;
    public List<Hotel> Hotels;
    public List<Client> Clients;
    public List<ReservedRooms> ReservedRooms;
    
    public HotelBookingDetailsData()
    {
        Clients =
        [
            new()
            {
                Id = 1,
                FullName = "Иванов Иван Иванович",
                PassportData = Passports[0],
                Birthday =  new DateOnly(2003, 1, 1)
            },
            new()
            {
                Id = 2,
                FullName = "Петров Петр Петрович",
                PassportData = Passports[1],
                Birthday =  new DateOnly(2002, 5, 15 )
            },
            new()
            {
                Id = 3,
                FullName = "Сидоров Сидор Сидорович",
                PassportData = Passports[2],
                Birthday =  new DateOnly(2001, 9, 20 )
            },
            new()
            {
                Id = 4,
                FullName = "Кузнецов Алексей Владимирович",
                PassportData = Passports[3],
                Birthday =  new DateOnly(2004, 3, 10)
            },
            new()
            {
                Id = 5,
                FullName = "Смирнов Дмитрий Александрович",
                PassportData = Passports[4],
                Birthday =  new DateOnly(2000, 8, 27)
            },
            new()
            {
                Id = 6,
                FullName = "Козлов Игорь Сергеевич",
                PassportData = Passports[5],
                Birthday =  new DateOnly(2005, 2, 18)
            },
            new()
            {
                Id = 7,
                FullName = "Лебедев Владислав Денисович",
                PassportData = Passports[6],
                Birthday =  new DateOnly(2003, 10, 05)
            },
            new()
            {
                Id = 8,
                FullName = "Никитин Андрей Алексеевич",
                PassportData = Passports[7],
                Birthday =  new DateOnly(2001, 12, 30)
            },
            new()
            {
                Id = 9,
                FullName = "Морозов Кирилл Васильевич",
                PassportData = Passports[8],
                Birthday =  new DateOnly(2000, 4, 09)
            },
            new()
            {
                Id = 10,
                FullName = "Андреев Евгений Дмитриевич",
                PassportData = Passports[9],
                Birthday =  new DateOnly(2002, 7, 14)
            },
            new()
            {
                Id = 11,
                FullName = "Богданова Елена Игоревна",
                PassportData = Passports[10],
                Birthday =  new DateOnly(2004, 11, 21)
            },
            new()
            {
                Id = 12,
                FullName = "Семенов Максим Павлович",
                PassportData = Passports[11],
                Birthday =  new DateOnly(2003, 6, 2)
            },
            new()
            {
                Id = 13,
                FullName = "Козлова Виктория Алексеевна",
                PassportData = Passports[12],
                Birthday =  new DateOnly(2000, 12, 15)
            },
            new()
            {
                Id = 14,
                FullName = "Новикова Анастасия Сергеевна",
                PassportData = Passports[13],
                Birthday =  new DateOnly(2001, 3, 25)
            },
            new()
            {
                Id = 15,
                FullName = "Гаврилова Ольга Владимировна",
                PassportData = Passports[14],
                Birthday =  new DateOnly(2002, 9, 12)
            },
            new()
            {
                Id = 16,
                FullName = "Белякова Светлана Ивановна",
                PassportData = Passports[15],
                Birthday =  new DateOnly(2004, 7, 3)
            },
            new()
            {
                Id = 17,
                FullName = "Федорова Екатерина Дмитриевна",
                PassportData = Passports[16],
                Birthday =  new DateOnly(2003, 4, 28)
            },
            new()
            {
                Id = 18,
                FullName = "Алексеева Ирина Александровна",
                PassportData = Passports[17],
                Birthday =  new DateOnly(2001, 10, 17)
            },
            new()
            {
                Id = 19,
                FullName = "Тихонова Татьяна Васильевна",
                PassportData = Passports[18],
                Birthday =  new DateOnly(2001, 10, 17)
            },
            new()
            {
                Id = 19,
                FullName = "Жмурова Мелания Максимова",
                PassportData = Passports[19],
                Birthday =  new DateOnly(2002, 02, 17)
            }
        ];
        Rooms =
        [
            new(){Id = 1, Capacity = 2, Cost = 3000, Type = TypesRoom[0], HotelId = 0},
            new(){Id = 2, Capacity = 1, Cost = 4000, Type = TypesRoom[1], HotelId = 0},
            new(){Id = 3, Capacity = 3, Cost = 5000, Type = TypesRoom[2], HotelId = 0},
            new(){Id = 4, Capacity = 2, Cost = 6000, Type = TypesRoom[3], HotelId = 0},
            new(){Id = 5, Capacity = 1, Cost = 7000, Type = TypesRoom[0], HotelId = 1},
            new(){Id = 6, Capacity = 5, Cost = 8000, Type = TypesRoom[1], HotelId = 1},
            new(){Id = 7, Capacity = 3, Cost = 9000, Type = TypesRoom[2], HotelId = 1},
            new(){Id = 8, Capacity = 2, Cost = 10000, Type = TypesRoom[3], HotelId = 1},
            new(){Id = 9, Capacity = 1, Cost = 11000, Type = TypesRoom[0], HotelId = 2},
            new(){Id = 10, Capacity = 4, Cost = 12000, Type = TypesRoom[1], HotelId = 2},
            new(){Id = 11, Capacity = 2, Cost = 13000, Type = TypesRoom[2], HotelId = 2},
            new(){Id = 12, Capacity = 1, Cost = 14000, Type = TypesRoom[3], HotelId = 2},
            new(){Id = 13, Capacity = 3, Cost = 15000, Type = TypesRoom[0], HotelId = 3},
            new(){Id = 14, Capacity = 2, Cost = 16000, Type = TypesRoom[1], HotelId = 3},
            new(){Id = 15, Capacity = 5, Cost = 17000, Type = TypesRoom[2], HotelId = 3},
            new(){Id = 16, Capacity = 1, Cost = 18000, Type = TypesRoom[3], HotelId = 3},
            new(){Id = 17, Capacity = 4, Cost = 19000, Type = TypesRoom[0], HotelId = 4},
            new(){Id = 18, Capacity = 3, Cost = 20000, Type = TypesRoom[1], HotelId = 4},
            new(){Id = 19, Capacity = 2, Cost = 21000, Type = TypesRoom[2], HotelId = 4},
            new(){Id = 20, Capacity = 1, Cost = 22000, Type = TypesRoom[3], HotelId = 4},
            new(){Id = 21, Capacity = 5, Cost = 23000, Type = TypesRoom[0], HotelId = 5},
        ];

        Hotels = 
        [
            new(){Id = 0, Name = "Гостиница Москва", Address = "Новосадовая 34Э", City = "Вашингтон"},
            new(){Id = 1, Name = "Grand Hotel", Address = "Park Avenue 123", City = "New York"},
            new(){Id = 2, Name = "Hilton", Address = "Main Street 55", City = "Chicago"},
            new(){Id = 3, Name = "Marriott", Address = "Sunset Boulevard 87", City = "Los Angeles"},
            new(){Id = 4, Name = "Hyatt Regency", Address = "Lake Shore Drive 100", City = "Chicago"},
            new(){Id = 5, Name = "Ritz-Carlton", Address = "Michigan Avenue 160", City = "Chicago"},
        ];
        ReservedRooms =
        [
            new(){ Client = Clients[0], DateArrival = new DateOnly(2024, 4, 28), Period = 10, Room = Rooms[0]},
            new(){ Client = Clients[11], DateArrival= new DateOnly(2024, 4, 28), Period= 10, Room= Rooms[11]},
            new(){ Client = Clients[1], DateArrival= new DateOnly(2024, 5, 5), Period= 5, Room= Rooms[1]},
            new(){ Client = Clients[2], DateArrival= new DateOnly(2024, 5, 15), Period= 7, Room= Rooms[2], DateDeparture =  new DateOnly(2024, 5, 22)},
            new(){ Client = Clients[3], DateArrival= new DateOnly(2024, 6, 1), Period= 3, Room= Rooms[3], DateDeparture =  new DateOnly(2024, 6, 4)},
            new(){ Client = Clients[4], DateArrival= new DateOnly(2024, 6, 10), Period= 14, Room= Rooms[4], DateDeparture =  new DateOnly(2024, 6, 24)},
            new(){ Client = Clients[5], DateArrival= new DateOnly(2024, 6, 20), Period= 9, Room= Rooms[5], DateDeparture =  new DateOnly(2024, 6, 29)},
            new(){ Client = Clients[6], DateArrival= new DateOnly(2024, 7, 1), Period= 6, Room= Rooms[6], DateDeparture =  new DateOnly(2024, 7, 7)},
            new(){ Client = Clients[7], DateArrival= new DateOnly(2024, 7, 10), Period= 11, Room= Rooms[7]},
            new(){ Client = Clients[8], DateArrival= new DateOnly(2024, 7, 20), Period= 8, Room= Rooms[8]},
            new(){ Client = Clients[9], DateArrival= new DateOnly(2024, 8, 1), Period= 4, Room= Rooms[9]},
            new(){ Client = Clients[10], DateArrival= new DateOnly(2024, 8, 10), Period= 12, Room= Rooms[10]},
            new(){ Client = Clients[11], DateArrival= new DateOnly(2024, 5, 15), Period= 7, Room= Rooms[3]},
            new(){ Client = Clients[12], DateArrival= new DateOnly(2024, 6, 1), Period= 3, Room= Rooms[14]},
            new(){ Client = Clients[13], DateArrival= new DateOnly(2024, 6 ,10), Period= 14, Room= Rooms[15]},
            new(){ Client = Clients[14], DateArrival= new DateOnly(2024, 6, 20), Period= 9, Room= Rooms[20], DateDeparture =  new DateOnly(2024, 6, 26)},
            new(){ Client = Clients[14], DateArrival= new DateOnly(2024 ,6, 27), Period= 9, Room= Rooms[20]},
            new(){ Client = Clients[15], DateArrival= new DateOnly(2024 ,7, 1), Period= 6, Room= Rooms[16],  DateDeparture =  new DateOnly(2024, 7, 7)},
            new(){ Client = Clients[15], DateArrival= new DateOnly(2024 ,7, 8), Period= 12, Room= Rooms[16]},
        ];
    }
}
