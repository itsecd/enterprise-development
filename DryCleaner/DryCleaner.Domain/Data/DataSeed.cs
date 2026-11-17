
using DryCleaner.Domain.Entities;
using DryCleaner.Shared.Enums;

namespace DryCleaner.Domain.Data;

/// <summary>
/// Набор тестовых данных 
/// </summary>
public static class DataSeed
{
    public static List<ProductCategory> Categories { get; } =
    [
        new ProductCategory
        {
            Id = 1,
            Name = "Верхняя одежда",
            RecommendedCleaningType = "Химчистка",
            CleaningPrice = 1500m
        },
        new ProductCategory
        {
            Id = 2,
            Name = "Костюмы",
            RecommendedCleaningType = "Химчистка",
            CleaningPrice = 1200m
        },
        new ProductCategory
        {
            Id = 3,
            Name = "Платья",
            RecommendedCleaningType = "Деликатная чистка",
            CleaningPrice = 1000m
        },
        new ProductCategory
        {
            Id = 4,
            Name = "Рубашки",
            RecommendedCleaningType = "Стирка",
            CleaningPrice = 500m
        },
        new ProductCategory
        {
            Id = 5,
            Name = "Брюки",
            RecommendedCleaningType = "Химчистка",
            CleaningPrice = 700m
        },
        new ProductCategory
        {
            Id = 6,
            Name = "Пальто",
            RecommendedCleaningType = "Химчистка",
            CleaningPrice = 1800m
        },
        new ProductCategory
        {
            Id = 7,
            Name = "Куртки",
            RecommendedCleaningType = "Химчистка",
            CleaningPrice = 1300m
        },
        new ProductCategory
        {
            Id = 8,
            Name = "Шторы",
            RecommendedCleaningType = "Деликатная чистка",
            CleaningPrice = 2000m
        },
        new ProductCategory
        {
            Id = 9,
            Name = "Одеяла",
            RecommendedCleaningType = "Стирка",
            CleaningPrice = 1700m
        },
        new ProductCategory
        {
            Id = 10,
            Name = "Обувь",
            RecommendedCleaningType = "Специальная чистка",
            CleaningPrice = 900m
        }
    ];

    public static List<Client> Clients { get; } =
    [
        new Client { Id = 1, Name = "Иванов Иван Иванович", Phone = "+79990000001" },
        new Client { Id = 2, Name = "Петров Петр Петрович", Phone = "+79990000002" },
        new Client { Id = 3, Name = "Сидорова Анна Сергеевна", Phone = "+79990000003" },
        new Client { Id = 4, Name = "Кузнецов Алексей Владимирович", Phone = "+79990000004" },
        new Client { Id = 5, Name = "Смирнова Елена Андреевна", Phone = "+79990000005" },
        new Client { Id = 6, Name = "Попов Дмитрий Сергеевич", Phone = "+79990000006" },
        new Client { Id = 7, Name = "Васильева Ольга Николаевна", Phone = "+79990000007" },
        new Client { Id = 8, Name = "Новиков Андрей Павлович", Phone = "+79990000008" },
        new Client { Id = 9, Name = "Морозова Мария Игоревна", Phone = "+79990000009" },
        new Client { Id = 10, Name = "Волков Сергей Александрович", Phone = "+79990000010" }
    ];

    public static List<Product> Products { get; } =
    [
        new Product
        {
            Id = 1,
            Name = "Пальто классическое",
            Material = "Шерсть",
            Category = Categories[0]
        },

        new Product
        {
            Id = 2,
            Name = "Костюм мужской",
            Material = "Шерсть",
            Category = Categories[1]
        },

        new Product
        {
            Id = 3,
            Name = "Пальто женское",
            Material = "Кашемир",
            Category = Categories[0]
        },

        new Product
        {
            Id = 4,
            Name = "Пальто осеннее",
            Material = "Шерсть",
            Category = Categories[0]
        },

        new Product
        {
            Id = 5,
            Name = "Костюм женский",
            Material = "Шерсть",
            Category = Categories[1]
        },

        new Product
        {
            Id = 6,
            Name = "Костюм деловой",
            Material = "Шерсть",
            Category = Categories[1]
        },

        new Product
        {
            Id = 7,
            Name = "Платье вечернее",
            Material = "Шёлк",
            Category = Categories[2]
        },

        new Product
        {
            Id = 8,
            Name = "Рубашка белая",
            Material = "Хлопок",
            Category = Categories[3]
        },

        new Product
        {
            Id = 9,
            Name = "Брюки классические",
            Material = "Шерсть",
            Category = Categories[4]
        },

        new Product
        {
            Id = 10,
            Name = "Пальто мужское",
            Material = "Кашемир",
            Category = Categories[0]
        }
    ];

    public static List<Order> Orders { get; } =
    [
        new Order
        {
            Id = 1,
            Client = Clients[0],
            Product = Products[0],
            AcceptedAt = new DateTime(2025, 1, 10, 0, 0, 0, DateTimeKind.Utc),
            ProcessingDays = 5,
            Status = OrderStatus.Issued
        },

        new Order
        {
            Id = 2,
            Client = Clients[0],
            Product = Products[1],
            AcceptedAt = new DateTime(2025, 3, 10, 0, 0, 0, DateTimeKind.Utc),
            ProcessingDays = 7,
            Status = OrderStatus.Issued
        },

        new Order
        {
            Id = 3,
            Client = Clients[1],
            Product = Products[2],
            AcceptedAt = new DateTime(2025, 6, 10, 0, 0, 0, DateTimeKind.Utc),
            ProcessingDays = 3,
            Status = OrderStatus.Issued
        },

        new Order
        {
            Id = 4,
            Client = Clients[1],
            Product = Products[3],
            AcceptedAt = new DateTime(2025, 7, 25, 0, 0, 0, DateTimeKind.Utc),
            ProcessingDays = 10,
            Status = OrderStatus.InProgress
        },

        new Order
        {
            Id = 5,
            Client = Clients[2],
            Product = Products[4],
            AcceptedAt = new DateTime(2025, 8, 10, 0, 0, 0, DateTimeKind.Utc),
            ProcessingDays = 6,
            Status = OrderStatus.Issued
        },

        new Order
        {
            Id = 6,
            Client = Clients[2],
            Product = Products[5],
            AcceptedAt = new DateTime(2025, 9, 10, 0, 0, 0, DateTimeKind.Utc),
            ProcessingDays = 8,
            Status = OrderStatus.Issued
        },

        new Order
        {
            Id = 7,
            Client = Clients[3],
            Product = Products[6],
            AcceptedAt = new DateTime(2025, 10, 10, 0, 0, 0, DateTimeKind.Utc),
            ProcessingDays = 15,
            Status = OrderStatus.Issued
        },

        new Order
        {
            Id = 8,
            Client = Clients[3],
            Product = Products[7],
            AcceptedAt = new DateTime(2025, 11, 10, 0, 0, 0, DateTimeKind.Utc),
            ProcessingDays = 20,
            Status = OrderStatus.InProgress
        },

        new Order
        {
            Id = 9,
            Client = Clients[4],
            Product = Products[8],
            AcceptedAt = new DateTime(2025, 12, 10, 0, 0, 0, DateTimeKind.Utc),
            ProcessingDays = 4,
            Status = OrderStatus.Issued
        },

        new Order
        {
            Id = 10,
            Client = Clients[4],
            Product = Products[9],
            AcceptedAt = new DateTime(2026, 5, 10, 0, 0, 0, DateTimeKind.Utc),
            ProcessingDays = 12,
            Status = OrderStatus.InProgress
        }
    ];
}
