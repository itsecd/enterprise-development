using DryCleaner.Domain.Entities;
using DryCleaner.Shared.Enums;

namespace DryCleaner.Domain.Data;

/// <summary>
/// Тестовые данные химчистки
/// </summary>
public static class DataSeed
{
    /// <summary>
    /// Категории изделий
    /// </summary>
    public static List<ItemCategory> Categories { get; } = new()
    {
        new ItemCategory { Id = 0, Name = "Верхняя одежда", RecommendedCleaning = CleaningType.DryCleaning, Price = 1500 },
        new ItemCategory { Id = 1, Name = "Пальто", RecommendedCleaning = CleaningType.DryCleaning, Price = 2000 },
        new ItemCategory { Id = 2, Name = "Платье", RecommendedCleaning = CleaningType.DryCleaning, Price = 1200 },
        new ItemCategory { Id = 3, Name = "Костюм", RecommendedCleaning = CleaningType.DryCleaning, Price = 1800 },
        new ItemCategory { Id = 4, Name = "Рубашка", RecommendedCleaning = CleaningType.Ironing, Price = 500  },
        new ItemCategory { Id = 5, Name = "Брюки", RecommendedCleaning = CleaningType.Ironing, Price = 800  },
        new ItemCategory { Id = 6, Name = "Постельное бельё", RecommendedCleaning = CleaningType.AquaCleaning, Price = 700  },
        new ItemCategory { Id = 7, Name = "Спортивная одежда", RecommendedCleaning = CleaningType.WetCleaning, Price = 600  },
        new ItemCategory { Id = 8, Name = "Куртка", RecommendedCleaning = CleaningType.DryCleaning, Price = 1700 },
        new ItemCategory { Id = 9, Name = "Шуба", RecommendedCleaning = CleaningType.DryCleaning, Price = 5000 },
    };

    /// <summary>
    /// Список изделий
    /// </summary>
    public static List<Item> Items { get; } = new()
    {
        new Item { Id = 0, Name = "Пальто", Category = Categories[1], CategoryId = 1, Material = "Шерсть" },
        new Item { Id = 1,Name = "Пиджак", Category = Categories[3], CategoryId = 3, Material = "Хлопок" },
        new Item { Id = 2, Name = "Платье свадебное", Category = Categories[2], CategoryId = 2, Material = "Шёлк" },
        new Item { Id = 3, Name = "Рубашка в клеточку", Category = Categories[4], CategoryId = 4, Material = "Хлопок" },
        new Item { Id = 4, Name = "Брюки", Category = Categories[5], CategoryId = 5, Material = "Шерсть" },
        new Item { Id = 5, Name = "Куртка", Category = Categories[8], CategoryId = 8, Material = "Кожа" },
        new Item { Id = 6, Name = "Шуба норковая", Category = Categories[9], CategoryId = 9, Material = "Мех" },
        new Item { Id = 7, Name = "Костюм тройка", Category = Categories[3], CategoryId = 3, Material = "Шерсть" },
        new Item { Id = 8, Name = "Футбольная форма", Category = Categories[7], CategoryId = 7, Material = "Синтетика" },
        new Item { Id = 9, Name = "Постельное бельё", Category = Categories[6], CategoryId = 6, Material = "Хлопок" },
    };

    /// <summary>
    /// Список клиентов
    /// </summary>
    public static List<Customer> Customers { get; } = new()
    {
        new Customer { Id = 0, LastName = "Евлампьев", FirstName = "Евлампий", Patronymic = "Евлампиевич", PhoneNumber = "+79001112233" },
        new Customer { Id = 1, LastName = "Сергеев", FirstName = "Александр", Patronymic = "Сильвестрович", PhoneNumber = "+79000000000" },
        new Customer { Id = 2, LastName = "Сергеев", FirstName = "Александр", Patronymic = "Александрович", PhoneNumber = "+79000000001" },
        new Customer { Id = 3, LastName = "Баринов", FirstName = "Виктор", Patronymic = "Петрович", PhoneNumber = "+78005553535" },
        new Customer { Id = 4, LastName = "Уайт", FirstName = "Уолтер", PhoneNumber = "+15430000567" },
        new Customer { Id = 5, LastName = "Вазовский", FirstName = "Майк", PhoneNumber = "+23334444555" },
        new Customer { Id = 6, LastName = "Гудман", FirstName = "Сол", PhoneNumber = "+77777777777" },
        new Customer { Id = 7, LastName = "Чигур", FirstName = "Антон", PhoneNumber = "+66666666666" },
        new Customer { Id = 8, LastName = "Пинкман", FirstName = "Джесси", PhoneNumber = "+977712935030" },
        new Customer { Id = 9, LastName = "Скалетта", FirstName = "Вито", PhoneNumber = "+79004301983" },
    };

    /// <summary>
    /// Список заказов
    /// </summary>
    public static List<Order> Orders { get; } = new()
    {
        new Order { Id = 0, Customer = Customers[0], CustomerId = 0, Item = Items[0], ItemId = 0, AcceptanceDate = new(2025, 4, 10), CompletionDays = 5, Status = OrderStatus.Issued },
        new Order { Id = 1, Customer = Customers[1], CustomerId = 1, Item = Items[1], ItemId = 1, AcceptanceDate = new(2025, 5, 15), CompletionDays = 7, Status = OrderStatus.Issued },
        new Order { Id = 2, Customer = Customers[2], CustomerId = 2, Item = Items[2], ItemId = 2, AcceptanceDate = new(2025, 6, 20), CompletionDays = 3, Status = OrderStatus.Issued },
        new Order { Id = 3, Customer = Customers[0], CustomerId = 0, Item = Items[1], ItemId = 1, AcceptanceDate = new(2025, 7, 25), CompletionDays = 2, Status = OrderStatus.Issued },
        new Order { Id = 4, Customer = Customers[3], CustomerId = 3, Item = Items[3], ItemId = 3, AcceptanceDate = new(2025, 8, 30), CompletionDays = 4, Status = OrderStatus.Issued },
        new Order { Id = 5, Customer = Customers[4], CustomerId = 4, Item = Items[4], ItemId = 4, AcceptanceDate = new(2025, 10, 5), CompletionDays = 6, Status = OrderStatus.Issued },
        new Order { Id = 6, Customer = Customers[5], CustomerId = 5, Item = Items[5], ItemId = 5, AcceptanceDate = new(2025, 11, 10), CompletionDays = 10, Status = OrderStatus.Issued },
        new Order { Id = 7, Customer = Customers[0], CustomerId = 0, Item = Items[7], ItemId = 7, AcceptanceDate = new(2025, 12, 15), CompletionDays = 3, Status = OrderStatus.Issued },
        new Order { Id = 8, Customer = Customers[6], CustomerId = 6, Item = Items[8], ItemId = 8, AcceptanceDate = new(2026, 1, 20), CompletionDays = 5, Status = OrderStatus.Issued },
        new Order { Id = 9, Customer = Customers[7], CustomerId = 7, Item = Items[9], ItemId = 9, AcceptanceDate = new(2026, 2, 1), CompletionDays = 4, Status = OrderStatus.Issued },
        new Order { Id = 10, Customer = Customers[8], CustomerId = 8, Item = Items[0], ItemId = 0, AcceptanceDate = new(2026, 2, 10), CompletionDays = 2, Status = OrderStatus.Issued },
        new Order { Id = 11, Customer = Customers[1], CustomerId = 1, Item = Items[3], ItemId = 3, AcceptanceDate = new(2026, 2, 20), CompletionDays = 6, Status = OrderStatus.Ready },
        new Order { Id = 12, Customer = Customers[2], CustomerId = 2, Item = Items[2], ItemId = 2, AcceptanceDate = new(2026, 3, 1), CompletionDays = 5, Status = OrderStatus.InProgress },
        new Order { Id = 13, Customer = Customers[9], CustomerId = 9, Item = Items[5], ItemId = 5, AcceptanceDate = new(2026, 3, 5), CompletionDays = 2, Status = OrderStatus.InProgress },
        new Order { Id = 14, Customer = Customers[0], CustomerId = 0, Item = Items[6], ItemId = 6, AcceptanceDate = new(2026, 3, 10), CompletionDays = 3, Status = OrderStatus.InProgress },
        new Order { Id = 15, Customer = Customers[3], CustomerId = 3, Item = Items[2], ItemId = 2, AcceptanceDate = new(2024, 8, 10), CompletionDays = 5, Status = OrderStatus.Issued },
        new Order { Id = 16, Customer = Customers[4], CustomerId = 4, Item = Items[4], ItemId = 4, AcceptanceDate = new(2024, 11, 20), CompletionDays = 4, Status = OrderStatus.Issued },
    };
}