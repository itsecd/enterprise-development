using BikeRent.Domain;
namespace BikeRent.Tests;
public class BikeRentFixture
{
    public List<Client> Clients =
    [
        new(){ Id = 0, FirstName = "Vasily", SecondName = "Denisov", Patronymic = "Nikolayevich", BirthDate = new DateTime(1970, 7, 20), PhoneNumber = "+7 908 216 48 93"},
        new(){ Id = 1, FirstName = "Alexandr", SecondName = "Katkov", Patronymic = "Dmitrievich", BirthDate = new DateTime(1984, 11, 14), PhoneNumber = "+7 908 156 38 26"},
        new(){ Id = 2, FirstName = "George", SecondName = "Granitov", Patronymic = "Albertovich", BirthDate = new DateTime(2001, 3, 30), PhoneNumber = "+7 907 511 63 89"},
        new(){ Id = 3, FirstName = "Alexey", SecondName = "Kuznetsov", Patronymic = "Vasilievich", BirthDate = new DateTime(2005, 5, 17), PhoneNumber = "+7 908 312 58 50"},
        new(){ Id = 4, FirstName = "Vyacheslav", SecondName = "Gvozdev", Patronymic = "Andreevich", BirthDate = new DateTime(1993, 1, 1), PhoneNumber = "+7 908 333 65 32"},
        new(){ Id = 5, FirstName = "Vsevolod", SecondName = "Arshinov", Patronymic = "Vladimirivoch", BirthDate = new DateTime(1999, 12, 21), PhoneNumber = "+7 905 041 32 61"},
        new(){ Id = 6, FirstName = "Ratibor", SecondName = "Zaharov", Patronymic = "Viktorovich", BirthDate = new DateTime(1975, 9, 18), PhoneNumber = "+7 908 216 18 97"}
    ];
    public List<BikeType> Types =
    [
        new(){ Id = 0, Price = 300, Name = "Sport"},
        new(){ Id = 1, Price = 350, Name = "Mountain"},
        new(){ Id = 2, Price = 250, Name = "Walking"}
    ];
    public List<Bike> Bikes =
    [
        new(){Id = 0, Color = "Blue", Model = "003v5", TypeId = 0 },
        new(){Id = 1, Color = "Red", Model = "003b", TypeId = 1 },
        new(){Id = 2, Color = "Black", Model = "004v8", TypeId = 2 },
        new(){Id = 3, Color = "Yellow", Model = "01006n", TypeId= 2 },
        new(){Id = 4, Color = "Green", Model = "331h", TypeId = 1},
        new(){Id = 5, Color = "Violet", Model = "34rf", TypeId = 1},
        new(){Id = 6, Color = "Orange", Model = "0004f", TypeId = 0}
    ];
    public List<Rent> Rents =
    [
        new(){ClientId = 0, BikeId = 4, Begin = new DateTime(2023, 9, 20, 18, 31, 0 ), End = new DateTime(2023, 9, 20, 20, 0, 0)},
        new(){ClientId = 0, BikeId = 3, Begin = new DateTime(2023, 9, 27, 17, 30, 0 ), End = new DateTime(2023, 9, 27, 19, 0, 0)},
        new(){ClientId = 0, BikeId = 3, Begin = new DateTime(2023, 9, 13, 17, 24, 0 ), End = new DateTime(2023, 9, 13, 19, 30, 0)},
        new(){ClientId = 0, BikeId = 3, Begin = new DateTime(2023, 9, 6, 18, 5, 0 ), End = new DateTime(2023, 9, 6, 19, 30, 0)},
        new(){ClientId = 0, BikeId = 4, Begin = new DateTime(2023, 9, 14, 18, 13, 0 ), End = new DateTime(2023, 9, 14, 19, 40, 0)},
        new(){ClientId = 0, BikeId = 2, Begin = new DateTime(2023, 9, 7, 17, 49, 0 ), End = new DateTime(2023, 9, 7, 19, 0, 0)},
        new(){ClientId = 1, BikeId = 2, Begin = new DateTime(2023, 9, 1, 18, 0, 0 ), End = new DateTime(2023, 9, 1, 20, 0, 0)},
        new(){ClientId = 1, BikeId = 2, Begin = new DateTime(2023, 9, 8, 16, 55, 0 ), End = new DateTime(2023, 9, 8, 20, 0, 0)},
        new(){ClientId = 1, BikeId = 4, Begin = new DateTime(2023, 9, 3, 18, 31, 0 ), End = new DateTime(2023, 9, 3, 20, 0, 0)},
        new(){ClientId = 1, BikeId = 1, Begin = new DateTime(2023, 9, 16, 14, 0, 0 ), End = new DateTime(2023, 9, 16, 16, 30, 0)},
        new(){ClientId = 1, BikeId = 1, Begin = new DateTime(2023, 9, 22, 15, 30, 0 ), End = new DateTime(2023, 9, 22, 16, 0, 0)},
        new(){ClientId = 1, BikeId = 1, Begin = new DateTime(2023, 9, 6, 18, 33, 0 ), End = new DateTime(2023, 9, 6, 20, 10, 0)},
        new(){ClientId = 2, BikeId = 4, Begin = new DateTime(2023, 9, 11, 18, 20, 0 ), End = new DateTime(2023, 9, 11, 20, 15, 0)},
        new(){ClientId = 2, BikeId = 5, Begin = new DateTime(2023, 9, 23, 13, 40, 0 ), End = new DateTime(2023, 9, 23, 15, 20, 0)},
        new(){ClientId = 3, BikeId = 5, Begin = new DateTime(2023, 9, 5, 14, 35, 0 ), End = new DateTime(2023, 9, 5, 15, 30, 0)},
        new(){ClientId = 3, BikeId = 5, Begin = new DateTime(2023, 9, 5, 19, 0, 0 ), End = new DateTime(2023, 9, 5, 21, 20, 0)},
        new(){ClientId = 3, BikeId = 4, Begin = new DateTime(2023, 9, 6, 9, 0, 0 ), End = new DateTime(2023, 9, 6, 11, 0, 0)},
        new(){ClientId = 3, BikeId = 6, Begin = new DateTime(2023, 9, 8, 8, 40, 0 ), End = new DateTime(2023, 9, 8, 10, 20, 0)},
        new(){ClientId = 4, BikeId = 6, Begin = new DateTime(2023, 9, 11, 11, 31, 0 ), End = new DateTime(2023, 9, 11, 13, 0, 0)},
        new(){ClientId = 4, BikeId = 6, Begin = new DateTime(2023, 9, 14, 10, 0, 0 ), End = new DateTime(2023, 9, 14, 12, 30, 0)},
        new(){ClientId = 5, BikeId = 4, Begin = new DateTime(2023, 9, 13, 13, 50, 0 ), End = new DateTime(2023, 9, 13, 16, 0, 0)},
        new(){ClientId = 4, BikeId = 1, Begin = new DateTime(2023, 9, 30, 15, 13, 0 ), End = new DateTime(2023, 9, 30, 18, 0, 0)},
        new(){ClientId = 5, BikeId = 1, Begin = new DateTime(2023, 9, 5, 16, 44, 0 ), End = new DateTime(2023, 9, 5, 18, 0, 0)},
        new(){ClientId = 6, BikeId = 1, Begin = new DateTime(2023, 9, 23, 12, 1, 0 ), End = new DateTime(2023, 9, 23, 14, 14, 0)},
        new(){ClientId = 5, BikeId = 4, Begin = new DateTime(2023, 9, 21, 15, 31, 0 ), End = new DateTime(2023, 9, 21, 16, 0, 0)},
        new(){ClientId = 6, BikeId = 3, Begin = new DateTime(2023, 9, 22, 13, 31, 0 ), End = new DateTime(2023, 9, 22, 15, 0, 0)},
        new(){ClientId = 5, BikeId = 3, Begin = new DateTime(2023, 9, 1, 12, 31, 0 ), End = new DateTime(2023, 9, 1, 13, 0, 0)},
    ];
}
