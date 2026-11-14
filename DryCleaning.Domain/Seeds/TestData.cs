using DryCleaning.Domain.Enum;

namespace DryCleaning.Domain.Seeds;

public class TestData
{
    public List<Client> Clients { get; } =
    [
        new Client { ClientId = 1,  FullName = "Александр Кузнецов", PhoneNumber = "+7 927 416 5823" },
        new Client { ClientId = 2,  FullName = "Антон Смирнов",      PhoneNumber = "+7 919 813 0876" },
        new Client { ClientId = 3,  FullName = "Данила Морозов",     PhoneNumber = "+7 918 191 4883" },
        new Client { ClientId = 4,  FullName = "Матвей Бобров",      PhoneNumber = "+7 902 815 7491" },
        new Client { ClientId = 5,  FullName = "Михаил Волков",      PhoneNumber = "+7 918 915 9235" },
        new Client { ClientId = 6,  FullName = "Дмитрий Морсов",     PhoneNumber = "+7 992 652 9123" },
        new Client { ClientId = 7,  FullName = "Александр Мельник",  PhoneNumber = "+7 924 639 8195" },
        new Client { ClientId = 8,  FullName = "Ольга Новикова",     PhoneNumber = "+7 987 429 3065" },
        new Client { ClientId = 9,  FullName = "Алексей Попов",      PhoneNumber = "+7 905 736 2184" },
        new Client { ClientId = 10, FullName = "Мария Васильева",    PhoneNumber = "+7 919 582 6473" }
    ];

    public List<ItemCategory> Categories { get; } =
    [
        new ItemCategory { CategoryId = 1,  CategoryName = "Одежда",          RecommendedCleaningType = CleaningType.DryCleaning,   Price = 1500m },
        new ItemCategory { CategoryId = 2,  CategoryName = "Обувь",           RecommendedCleaningType = CleaningType.WetCleaning,   Price = 1000m },
        new ItemCategory { CategoryId = 3,  CategoryName = "Аксессуары",      RecommendedCleaningType = CleaningType.SteamCleaning, Price = 2500m },
        new ItemCategory { CategoryId = 4,  CategoryName = "Ковры",           RecommendedCleaningType = CleaningType.WetCleaning,   Price = 3000m },
        new ItemCategory { CategoryId = 5,  CategoryName = "Шторы",           RecommendedCleaningType = CleaningType.SteamCleaning, Price = 4500m },
        new ItemCategory { CategoryId = 6,  CategoryName = "Постельное",      RecommendedCleaningType = CleaningType.WetCleaning,   Price = 3500m },
        new ItemCategory { CategoryId = 7,  CategoryName = "Вечерняя одежда", RecommendedCleaningType = CleaningType.DryCleaning,   Price = 4000m },
        new ItemCategory { CategoryId = 8,  CategoryName = "Мягкие игрушки",  RecommendedCleaningType = CleaningType.HandWash,      Price = 5000m },
        new ItemCategory { CategoryId = 9,  CategoryName = "Рубашки",         RecommendedCleaningType = CleaningType.Ironing,       Price = 900m  },
        new ItemCategory { CategoryId = 10, CategoryName = "Футболки",        RecommendedCleaningType = CleaningType.Ironing,       Price = 1200m }
    ];

    public List<Item> Items { get; }
    public List<Order> Orders { get; }

    public TestData()
    {
        Items =
        [
            new Item { ItemId = 1,  Name = "Пальто",               Material = "Шерсть",    Category = Categories[0] },
            new Item { ItemId = 2,  Name = "Кожаные ботинки",      Material = "Кожа",      Category = Categories[1] },
            new Item { ItemId = 3,  Name = "Кожаный ремень",       Material = "Кожа",      Category = Categories[2] },
            new Item { ItemId = 4,  Name = "Ковер в спальню",      Material = "Шерсть",    Category = Categories[3] },
            new Item { ItemId = 5,  Name = "Бархатные шторы",      Material = "Бархат",    Category = Categories[4] },
            new Item { ItemId = 6,  Name = "Наволочка на подушку", Material = "Шёлк",      Category = Categories[5] },
            new Item { ItemId = 7,  Name = "Вечернее платье",      Material = "Шёлк",      Category = Categories[6] },
            new Item { ItemId = 8,  Name = "Плюшевый мишка",       Material = "Плюш",      Category = Categories[7] },
            new Item { ItemId = 9,  Name = "Белая рубашка",        Material = "Хлопок",    Category = Categories[8] },
            new Item { ItemId = 10, Name = "Черная футболка",      Material = "Синтетика", Category = Categories[9] }
        ];

        Orders =
        [
            new Order { Id = 1,  Client = Clients[0], Item = Items[0], Status = OrderStatus.InProgress, AcceptDate = new DateTime(2026, 9, 1),  ProcessingDays = 10 },
            new Order { Id = 2,  Client = Clients[0], Item = Items[0], Status = OrderStatus.Issued,     AcceptDate = new DateTime(2026, 8, 1),  ProcessingDays = 5,  CompletedDate = new DateTime(2026, 8, 6) },
            new Order { Id = 3,  Client = Clients[0], Item = Items[5], Status = OrderStatus.Completed,  AcceptDate = new DateTime(2026, 7, 1),  ProcessingDays = 4,  CompletedDate = new DateTime(2026, 7, 5) },
            new Order { Id = 4,  Client = Clients[0], Item = Items[5], Status = OrderStatus.Issued,     AcceptDate = new DateTime(2026, 6, 1),  ProcessingDays = 6,  CompletedDate = new DateTime(2026, 6, 7) },
            new Order { Id = 5,  Client = Clients[1], Item = Items[0], Status = OrderStatus.Issued,     AcceptDate = new DateTime(2026, 5, 1),  ProcessingDays = 5,  CompletedDate = new DateTime(2026, 5, 6) },
            new Order { Id = 6,  Client = Clients[1], Item = Items[0], Status = OrderStatus.Issued,     AcceptDate = new DateTime(2026, 4, 1),  ProcessingDays = 6,  CompletedDate = new DateTime(2026, 4, 7) },
            new Order { Id = 7,  Client = Clients[1], Item = Items[5], Status = OrderStatus.InProgress, AcceptDate = new DateTime(2026, 3, 1),  ProcessingDays = 7  },
            new Order { Id = 8,  Client = Clients[2], Item = Items[3], Status = OrderStatus.Completed,  AcceptDate = new DateTime(2026, 9, 10), ProcessingDays = 3,  CompletedDate = new DateTime(2026, 9, 13) },
            new Order { Id = 9,  Client = Clients[2], Item = Items[5], Status = OrderStatus.InProgress, AcceptDate = new DateTime(2026, 1, 1),  ProcessingDays = 6  },
            new Order { Id = 10, Client = Clients[3], Item = Items[7], Status = OrderStatus.Issued,     AcceptDate = new DateTime(2026, 9, 5),  ProcessingDays = 20, CompletedDate = new DateTime(2026, 9, 25) },
            new Order { Id = 11, Client = Clients[4], Item = Items[6], Status = OrderStatus.Issued,     AcceptDate = new DateTime(2026, 2, 1),  ProcessingDays = 8,  CompletedDate = new DateTime(2026, 2, 9) },
            new Order { Id = 12, Client = Clients[5], Item = Items[1], Status = OrderStatus.Cancelled,  AcceptDate = new DateTime(2026, 5, 2),  ProcessingDays = 5  }
        ];
    }
}
