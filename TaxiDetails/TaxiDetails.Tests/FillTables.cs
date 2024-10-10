using System;
using System.Collections.Generic;
using TaxiDetailsClass.Domain;

namespace TaxiDetails.Tests
{
public class TaxiDetailsData
{
    public List<Driver> Drivers { get; set; }
    public List<Car> Cars { get; set; }
    public List<User> Users { get; set; }
    public List<Travel> Travels { get; set; }

    public TaxiDetailsData()
    {     
        Drivers =
        [
            new Driver(1, "Максим", "Смирнов", "Александрович", "1234567890", "ул. Пушкина, д. 1", "+7 123 456 78 90"),
            new Driver(2, "Артем", "Кузнецов", "Олегович", "0987654321", "ул. Лермонтова, д. 5", "+7 987 654 32 10"),
            new Driver(3, "Даниил", "Новиков", "Игоревич", "1522334455", "ул. Чехова, д. 10", "+7 111 222 33 44"),
            new Driver(4, "Кирилл", "Морозов", "Владимирович", "130587654", "ул. Лесная, д. 12", "+7 555 777 88 99"),
            new Driver(5, "Илья", "Соколов", "Андреевич", "4455665758", "ул. Цветочная, д. 8", "+7 123 789 45 67"),
            new Driver(6, "Никита", "Орлов", "Павлович", "9388775655", "ул. Южная, д. 3", "+7 333 444 55 66"),
            new Driver(7, "Матвей", "Попов", "Александрович", "2633443566", "ул. Центральная, д. 6", "+7 111 222 11 33"),
            new Driver(8, "Тимофей", "Васильев", "Владиславович", "6677439930", "ул. Полевая, д. 7", "+7 999 888 77 44"),
            new Driver(9, "Александр", "Зайцев", "Николаевич", "5566578899", "ул. Овражная, д. 4", "+7 222 333 44 11"),
            new Driver(10, "Егор", "Смирнов", "Сергеевич", "3344356611", "ул. Радужная, д. 9", "+7 777 666 55 99")
        ];            
        Cars =
        [
            new Car(1, "X123AB77", "Tesla Model S", "Черный", Drivers[0]),
            new Car(2, "B456CD99", "Hyundai Tucson", "Белый", Drivers[1]),
            new Car(3, "M789EF33", "BMW X5", "Серебристый", Drivers[2]),
            new Car(4, "N987GH22", "Toyota Prius", "Красный", Drivers[3]),
            new Car(5, "P111KL55", "Audi A4", "Синий", Drivers[4]),
            new Car(6, "C222MN66", "Mercedes-Benz GLC", "Зеленый", Drivers[5]),
            new Car(7, "L333OP77", "Ford Mustang", "Оранжевый", Drivers[6]),
            new Car(8, "V444QR88", "Volkswagen Passat", "Желтый", Drivers[7]),
            new Car(9, "T555ST99", "Honda Civic", "Серый", Drivers[8]),
            new Car(10, "R666UV00", "Kia Sportage", "Бежевый", Drivers[9])
        ];
        Users =
        [
            new(1,"+7 912 345 67 89", "Анна Сергеева"),
            new(2,"+7 923 456 78 90", "Елена Михайлова"),
            new(3,"+7 934 567 89 01", "Дмитрий Воронов"),
            new (4,"+7 945 678 90 12", "Марина Кузьмина"),
            new (5,"+7 956 789 01 23", "Алексей Петров"),
            new (6,"+7 967 890 12 34", "Ольга Лебедева"),
            new (7,"+7 978 901 23 45", "Юлия Павлова"),
            new (8,"+7 989 012 34 56", "Иван Иванов"),
            new (9,"+7 999 123 45 67", "Светлана Смирнова"),
            new (10,"+7 900 234 56 78", "Константин Волков")
        ];
        Travels =
        [
            new Travel(1, "ул. Новая, д. 5", "пр-т Мира, д. 10", new DateTime(2024, 10, 1), new TimeSpan(0, 20, 0), 400, Cars[0],Users[0]),
            new Travel(12, "пр-т Суворова, д. 6", "ул. Кирова, д. 9", new DateTime(2024, 10, 13), new TimeSpan(0, 26, 0), 500, Cars[0],Users[1]),
            new Travel(13, "ул. Орлова, д. 13", "ул. Цветочная, д. 2", new DateTime(2024, 10, 14), new TimeSpan(0, 50, 0), 550, Cars[0],Users[2]),
            new Travel(2, "ул. Лесная, д. 8", "ул. Рябиновая, д. 3", new DateTime(2024, 10,2), new TimeSpan(0, 16, 0), 300, Cars[1],Users[0]),
            new Travel(14, "ул. Колхозная, д. 7", "пр-т Победы, д. 4", new DateTime(2024, 10, 15), new TimeSpan(0, 30, 0), 600, Cars[1],Users[0]),
            new Travel(11, "ул. Речная, д. 18", "ул. Полевая, д. 5", new DateTime(2024, 10, 12), new TimeSpan(0, 20, 0), 400, Cars[2],Users[0]),
            new Travel(3, "ул. Школьная, д. 12", "ул. Солнечная, д. 4", new DateTime(2024, 10,3), new TimeSpan(0, 26, 0), 500, Cars[2],Users[2]),
            new Travel(4, "пр-т Ленина, д. 21", "ул. Клубничная, д. 7", new DateTime(2024, 10, 4), new TimeSpan(0, 30, 0), 600, Cars[3],Users[3]),
            new Travel(5, "ул. Парковая, д. 9", "пр-т Гагарина, д. 15", new DateTime(2024, 10, 5), new TimeSpan(0, 20, 0), 350, Cars[4],Users[4]),
            new Travel(6, "ул. Лермонтова, д. 2", "ул. Чехова, д. 10", new DateTime(2024, 10, 6), new TimeSpan(0, 26, 0), 450, Cars[5],Users[5]),
            new Travel(7, "ул. Речная, д. 18", "ул. Полевая, д. 5", new DateTime(2024, 10, 7), new TimeSpan(0, 20, 0), 400, Cars[6],Users[6]),
            new Travel(8, "пр-т Суворова, д. 6", "ул. Кирова, д. 9", new DateTime(2024, 10, 8), new TimeSpan(0, 26, 0), 500, Cars[7],Users[7]),
            new Travel(9, "ул. Орлова, д. 13", "ул. Цветочная, д. 2", new DateTime(2024, 10, 9), new TimeSpan(0, 30, 0), 550, Cars[8],Users[8]),
            new Travel(10, "ул. Колхозная, д. 7", "пр-т Победы, д. 4", new DateTime(2024, 10, 11), new TimeSpan(0, 30, 0), 600, Cars[9],Users[9])    
        ];
    }
}
}
