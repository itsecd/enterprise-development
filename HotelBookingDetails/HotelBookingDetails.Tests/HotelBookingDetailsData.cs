using HotelBookingDetails;
using HotelBookingDetails.Domain;

namespace HotelBookingDetails.Tests;

public class HotelBookingDetailsData
{
    public List<TypeRoom> TypesRoom =
    [
        new(){ id = 1,  NameType = "Luxary"},
        new(){ id = 2,  NameType = "Economy"},
        new(){ id = 3,  NameType = "Comfort"},
        new(){ id = 4,  NameType = "Comfort+"},
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
        Clients = [
            new(){
            id = 1,
            FullName = "Иванов Иван Иванович",
            PassportData = Passports[0],
            Birthday =  DateOnly.ParseExact("2003-01-01", "yyyy-mm-dd")
                },
            new(){
            id = 2,
            FullName = "Петров Петр Петрович",
            PassportData = Passports[1],
            Birthday =  DateOnly.ParseExact("2002-05-15", "yyyy-mm-dd")
            },
            new(){
            id = 3,
            FullName = "Сидоров Сидор Сидорович",
            PassportData = Passports[2],
            Birthday =  DateOnly.ParseExact("2001-09-20", "yyyy-mm-dd")
            },
            new(){
            id = 4,
            FullName = "Кузнецов Алексей Владимирович",
            PassportData = Passports[3],
            Birthday =  DateOnly.ParseExact("2004-03-10", "yyyy-mm-dd")
            },
            new(){
            id = 5,
            FullName = "Смирнов Дмитрий Александрович",
            PassportData = Passports[4],
            Birthday =  DateOnly.ParseExact("2000-08-27", "yyyy-mm-dd")
            },
            new(){
            id = 6,
            FullName = "Козлов Игорь Сергеевич",
            PassportData = Passports[5],
            Birthday =  DateOnly.ParseExact("2005-02-18", "yyyy-mm-dd")
            },
            new(){
            id = 7,
            FullName = "Лебедев Владислав Денисович",
            PassportData = Passports[6],
            Birthday =  DateOnly.ParseExact("2003-10-05", "yyyy-mm-dd")
            },
            new(){
            id = 8,
            FullName = "Никитин Андрей Алексеевич",
            PassportData = Passports[7],
            Birthday =  DateOnly.ParseExact("2001-12-30", "yyyy-mm-dd")
            },
            new(){
            id = 9,
            FullName = "Морозов Кирилл Васильевич",
            PassportData = Passports[8],
            Birthday =  DateOnly.ParseExact("2000-04-09", "yyyy-mm-dd")
            },
            new(){
            id = 10,
            FullName = "Андреев Евгений Дмитриевич",
            PassportData = Passports[9],
            Birthday =  DateOnly.ParseExact("2002-07-14", "yyyy-mm-dd")
            },
            new(){
            id = 11,
            FullName = "Богданова Елена Игоревна",
            PassportData = Passports[10],
            Birthday =  DateOnly.ParseExact("2004-11-21", "yyyy-mm-dd")
            },
            new(){
            id = 12,
            FullName = "Семенов Максим Павлович",
            PassportData = Passports[11],
            Birthday =  DateOnly.ParseExact("2003-06-02", "yyyy-mm-dd")
            },
            new(){
            id = 13,
            FullName = "Козлова Виктория Алексеевна",
            PassportData = Passports[12],
            Birthday =  DateOnly.ParseExact("2000-12-15", "yyyy-mm-dd")
            },
            new(){
            id = 14,
            FullName = "Новикова Анастасия Сергеевна",
            PassportData = Passports[13],
            Birthday =  DateOnly.ParseExact("2001-03-25", "yyyy-mm-dd")
            },
            new(){
            id = 15,
            FullName = "Гаврилова Ольга Владимировна",
            PassportData = Passports[14],
            Birthday =  DateOnly.ParseExact("2002-09-12", "yyyy-mm-dd")
            },
            new(){
            id = 16,
            FullName = "Белякова Светлана Ивановна",
            PassportData = Passports[15],
            Birthday =  DateOnly.ParseExact("2004-07-03", "yyyy-mm-dd")
            },
            new(){
            id = 17,
            FullName = "Федорова Екатерина Дмитриевна",
            PassportData = Passports[16],
            Birthday =  DateOnly.ParseExact("2003-04-28", "yyyy-mm-dd")
            },
            new(){
            id = 18,
            FullName = "Алексеева Ирина Александровна",
            PassportData = Passports[17],
            Birthday =  DateOnly.ParseExact("2001-10-17", "yyyy-mm-dd")
            },
            new(){
            id = 19,
            FullName = "Тихонова Татьяна Васильевна",
            PassportData = Passports[18],
            Birthday =  DateOnly.ParseExact("2001-10-17", "yyyy-mm-dd")
            },
            new(){
            id = 19,
            FullName = "Жмурова Мелания Максимова",
            PassportData = Passports[19],
            Birthday =  DateOnly.ParseExact("2002-02-17", "yyyy-mm-dd")
            }
            ];
        Rooms = [
            new(){Id = 1, Capacity = 2, Cost = 3000, Type = TypesRoom[0]},
            new(){Id = 2, Capacity = 1, Cost = 4000, Type = TypesRoom[1]},
            new(){Id = 3, Capacity = 3, Cost = 5000, Type = TypesRoom[2]},
            new(){Id = 4, Capacity = 2, Cost = 6000, Type = TypesRoom[3]},
            new(){Id = 5, Capacity = 1, Cost = 7000, Type = TypesRoom[0]},
            new(){Id = 6, Capacity = 5, Cost = 8000, Type = TypesRoom[1]},
            new(){Id = 7, Capacity = 3, Cost = 9000, Type = TypesRoom[2]},
            new(){Id = 8, Capacity = 2, Cost = 10000, Type = TypesRoom[3]},
            new(){Id = 9, Capacity = 1, Cost = 11000, Type = TypesRoom[0]},
            new(){Id = 10, Capacity = 4, Cost = 12000, Type = TypesRoom[1]},
            new(){Id = 11, Capacity = 2, Cost = 13000, Type = TypesRoom[2]},
            new(){Id = 12, Capacity = 1, Cost = 14000, Type = TypesRoom[3]},
            new(){Id = 13, Capacity = 3, Cost = 15000, Type = TypesRoom[0]},
            new(){Id = 14, Capacity = 2, Cost = 16000, Type = TypesRoom[1]},
            new(){Id = 15, Capacity = 5, Cost = 17000, Type = TypesRoom[2]},
            new(){Id = 16, Capacity = 1, Cost = 18000, Type = TypesRoom[3]},
            new(){Id = 17, Capacity = 4, Cost = 19000, Type = TypesRoom[0]},
            new(){Id = 18, Capacity = 3, Cost = 20000, Type = TypesRoom[1]},
            new(){Id = 19, Capacity = 2, Cost = 21000, Type = TypesRoom[2]},
            new(){Id = 20, Capacity = 1, Cost = 22000, Type = TypesRoom[3]},
            new(){Id = 21, Capacity = 5, Cost = 23000, Type = TypesRoom[0]},
            ];

        Hotels = [
            new(){Name = "Гостиница Москва", Address = "Новосадовая 34Э", City = "Вашингтон", Rooms = Rooms.Slice(0, 4)},
            new() { Name = "Grand Hotel", Address = "Park Avenue 123", City = "New York", Rooms = Rooms.Slice(4, 4)},
            new(){ Name = "Hilton", Address = "Main Street 55", City = "Chicago", Rooms = Rooms.Slice(8, 4)},
            new(){ Name = "Marriott", Address = "Sunset Boulevard 87", City = "Los Angeles", Rooms = Rooms.Slice(12, 4)},
            new(){ Name = "Hyatt Regency", Address = "Lake Shore Drive 100", City = "Chicago", Rooms = Rooms.Slice(16, 4)},
            new(){ Name = "Ritz-Carlton", Address = "Michigan Avenue 160", City = "Chicago", Rooms = Rooms.Slice(20, 1)},
            ];
        ReservedRooms = [
            new(){ client = Clients[0], DateArrival = DateOnly.ParseExact("2024-04-28", "yyyy-mm-dd"), Period = 10, room = Rooms[0]},
            new(){ client = Clients[0], DateArrival= DateOnly.ParseExact("2024-04-28", "yyyy-mm-dd"), Period= 10, room= Rooms[0]},
            new(){ client = Clients[1], DateArrival= DateOnly.ParseExact("2024-05-05", "yyyy-mm-dd"), Period= 5, room= Rooms[1]},
            new(){ client = Clients[2], DateArrival= DateOnly.ParseExact("2024-05-15", "yyyy-mm-dd"), Period= 7, room= Rooms[2], DateDeparture =  DateOnly.ParseExact("2024-05-22", "yyyy-mm-dd")},
            new(){ client = Clients[3], DateArrival= DateOnly.ParseExact("2024-06-01", "yyyy-mm-dd"), Period= 3, room= Rooms[3], DateDeparture =  DateOnly.ParseExact("2024-06-04", "yyyy-mm-dd")},
            new(){ client = Clients[4], DateArrival= DateOnly.ParseExact("2024-06-10", "yyyy-mm-dd"), Period= 14, room= Rooms[4], DateDeparture =  DateOnly.ParseExact("2024-06-24", "yyyy-mm-dd")},
            new(){ client = Clients[5], DateArrival= DateOnly.ParseExact("2024-06-20", "yyyy-mm-dd"), Period= 9, room= Rooms[5], DateDeparture =  DateOnly.ParseExact("2024-06-29", "yyyy-mm-dd")},
            new(){ client = Clients[6], DateArrival= DateOnly.ParseExact("2024-07-01", "yyyy-mm-dd"), Period= 6, room= Rooms[6], DateDeparture =  DateOnly.ParseExact("2024-07-07", "yyyy-mm-dd")},
            new(){ client = Clients[7], DateArrival= DateOnly.ParseExact("2024-07-10", "yyyy-mm-dd"), Period= 11, room= Rooms[7]},
            new(){ client = Clients[8], DateArrival= DateOnly.ParseExact("2024-07-20", "yyyy-mm-dd"), Period= 8, room= Rooms[8]},
            new(){ client = Clients[9], DateArrival= DateOnly.ParseExact("2024-08-01", "yyyy-mm-dd"), Period= 4, room= Rooms[9]},
            new(){ client = Clients[10], DateArrival= DateOnly.ParseExact("2024-08-10", "yyyy-mm-dd"), Period= 12, room= Rooms[10]},


            ];
    }
}
