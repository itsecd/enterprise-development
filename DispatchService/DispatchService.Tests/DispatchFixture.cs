using DispatchService.Model;

namespace DispatchService.Tests;

public class DispatchFixture
{
    public TestData TestData { get; set; }

    public DispatchFixture()
    {
        TestData = new TestData
        {
            Drivers = new List<Driver>
            {
                new Driver ( 1, "Иванов Иван Иванович", "1234 567890", "AB1234567", "Москва, ул. Ленина, д. 10", "+7 (912) 345-67-89" ),
                new Driver ( 2, "Петров Петр Петрович", "2345 678901", "CD2345678", "Москва, ул. Пушкина, д. 15", "+7 (912) 456-78-90" ),
                new Driver ( 3, "Сидоров Сидор Сидорович", "3456 789012", "EF3456789", "Санкт-Петербург, ул. Кирова, д. 20", "+7 (912) 567-89-01" ),
                new Driver ( 4, "Кузнецов Андрей Владимирович", "5678 123456", "GH1234567", "Москва, ул. Советская, д. 1", "+7 (903) 123-45-67" ),
                new Driver ( 5, "Васильев Алексей Павлович", "6789 234567", "IJ2345678", "Санкт-Петербург, пр. Невский, д. 5", "+7 (921) 789-12-34" ),
                new Driver ( 6, "Смирнова Елена Ивановна", "7890 345678", "KL3456789", "Новосибирск, ул. Ленина, д. 10", "+7 (913) 345-67-89" ),
                new Driver ( 7, "Федоров Максим Сергеевич", "8901 456789", "MN4567890", "Екатеринбург, ул. Чапаева, д. 20", "+7 (922) 456-78-90" ),
                new Driver ( 8, "Александрова Мария Петровна", "9012 567890", "OP5678901", "Казань, ул. Кремлевская, д. 5", "+7 (917) 567-89-01" )
            },

            Transports = new List<Transport>
            {
                new Transport ( 1, "A123BC", Transport.VehicleType.Bus, "Volvo 7900", true, 100, 2020),
                new Transport ( 2, "B456DE", Transport.VehicleType.Trolleybus, "Trolza", false, 80, 2018),
                new Transport ( 3, "C789FG", Transport.VehicleType.Tram, "Stadler", true, 150, 2019),
                new Transport ( 4, "D123HI", Transport.VehicleType.Bus, "Mercedes Citaro", true, 120, 2017),
                new Transport ( 5, "E456JK", Transport.VehicleType.Trolleybus, "BKM 321", false, 90, 2016),
                new Transport ( 6, "F789LM", Transport.VehicleType.Tram, "CAF Urbos", true, 160, 2021),
                new Transport ( 7, "G123NO", Transport.VehicleType.Bus, "MAN Lion's City", true, 110, 2020),
                new Transport ( 8, "H456PQ", Transport.VehicleType.Trolleybus, "ZIU-9", false, 70, 2015)
            },

            Routes = new List<Route>
            {
                new Route
                {
                    RouteNumber = "1",
                    AssignedTransport = new Transport ( 1, "A123BC", Transport.VehicleType.Bus, "Volvo 7900", true, 100, 2020),
                    AssignedDriver = new Driver ( 1,  "Иванов Иван Иванович", "1234 567890", "AB1234567", "Москва, ул. Ленина, д. 10", "+7 (912) 345-67-89" ),
                    StartTime = new DateTime(2024, 9, 14, 8, 0, 0), 
                    EndTime = new DateTime(2024, 9, 14, 10, 0, 0)  
                },
                new Route
                {
                    RouteNumber = "2",
                    AssignedTransport = new Transport ( 2, "B456DE", Transport.VehicleType.Trolleybus, "Trolza", false, 80, 2018),
                    AssignedDriver = new Driver ( 2, "Петров Петр Петрович", "2345 678901", "CD2345678", "Москва, ул. Пушкина, д. 15", "+7 (912) 456-78-90" ),
                    StartTime = new DateTime(2024, 9, 11, 9, 0, 0), 
                    EndTime = new DateTime(2024, 9, 11, 11, 0, 0)  
                },
                new Route
                {
                    RouteNumber = "3",
                    AssignedTransport = new Transport ( 3, "C789FG", Transport.VehicleType.Tram, "Stadler", true, 150, 2019),
                    AssignedDriver = new Driver ( 3, "Сидоров Сидор Сидорович", "3456 789012", "EF3456789", "Санкт-Петербург, ул. Кирова, д. 20", "+7 (912) 567-89-01" ),
                    StartTime = new DateTime(2024, 9, 14, 10, 0, 0), 
                    EndTime = new DateTime(2024, 9, 14, 12, 0, 0)   
                },
                new Route
                {
                    RouteNumber = "4",
                    AssignedTransport = new Transport ( 4, "D123HI", Transport.VehicleType.Bus, "Mercedes Citaro", true, 120, 2017),
                    AssignedDriver = new Driver ( 4, "Кузнецов Андрей Владимирович", "5678 123456", "GH1234567", "Москва, ул. Советская, д. 1", "+7 (903) 123-45-67" ),
                    StartTime = new DateTime(2024, 9, 13, 11, 0, 0), 
                    EndTime = new DateTime(2024, 9, 13, 13, 0, 0)  
                },
                new Route
                {
                    RouteNumber = "5",
                    AssignedTransport = new Transport ( 5, "E456JK", Transport.VehicleType.Trolleybus, "BKM 321", false, 90, 2016),
                    AssignedDriver = new Driver ( 5, "Васильев Алексей Павлович", "6789 234567", "IJ2345678", "Санкт-Петербург, пр. Невский, д. 5", "+7 (921) 789-12-34" ),
                    StartTime = new DateTime(2024, 9, 15, 8, 0, 0), 
                    EndTime = new DateTime(2024, 9, 15, 12, 0, 0)   
                },
                new Route
                {
                    RouteNumber = "6",
                    AssignedTransport = new Transport ( 6, "F789LM", Transport.VehicleType.Tram, "CAF Urbos", true, 160, 2021),
                    AssignedDriver = new Driver ( 6, "Смирнова Елена Ивановна", "7890 345678", "KL3456789", "Новосибирск, ул. Ленина, д. 10", "+7 (913) 345-67-89" ),
                    StartTime = new DateTime(2024, 9, 16, 9, 0, 0), 
                    EndTime = new DateTime(2024, 9, 16, 16, 0, 0)   
                },
                new Route
                {
                    RouteNumber = "7",
                    AssignedTransport = new Transport ( 7, "G123NO", Transport.VehicleType.Bus, "MAN Lion's City", true, 110, 2020),
                    AssignedDriver = new Driver ( 7, "Федоров Максим Сергеевич", "8901 456789", "MN4567890", "Екатеринбург, ул. Чапаева, д. 20", "+7 (922) 456-78-90" ),
                    StartTime = new DateTime(2024, 9, 12, 16, 0, 0), 
                    EndTime = new DateTime(2024, 9, 12, 19, 0, 0)   
                },
                new Route
                {
                    RouteNumber = "8",
                    AssignedTransport = new Transport ( 8, "H456PQ", Transport.VehicleType.Trolleybus, "ZIU-9", false, 70, 2015),
                    AssignedDriver = new Driver ( 8, "Александрова Мария Петровна", "9012 567890", "OP5678901", "Казань, ул. Кремлевская, д. 5", "+7 (917) 567-89-01" ),
                    StartTime = new DateTime(2024, 9, 17, 11, 0, 0),
                    EndTime = new DateTime(2024, 9, 17, 20, 0, 0)   
                },
                new Route
                {
                    RouteNumber = "9",
                    AssignedTransport = new Transport ( 5, "E456JK", Transport.VehicleType.Trolleybus, "BKM 321", false, 90, 2016),
                    AssignedDriver = new Driver ( 5, "Васильев Алексей Павлович", "6789 234567", "IJ2345678", "Санкт-Петербург, пр. Невский, д. 5", "+7 (921) 789-12-34" ),
                    StartTime = new DateTime(2024, 9, 15, 17, 0, 0),
                    EndTime = new DateTime(2024, 9, 15, 20, 0, 0)
                },
            }
        };
    }

}
