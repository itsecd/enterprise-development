using DryCleaning.Domain.Models;

namespace DryCleaning.Tests;

/// <summary>
/// Тестовый набор данных
/// </summary>
public static class DataTest
{
    public static readonly DateTime BaseDate = new(2026, 10, 1);

    /// <summary>
    /// 10 Категорий
    /// </summary>
    public static readonly List<Category> Categories =
    [
        new () 
        { 
            Id = 1,
            Name = "Верхняя одежда",
            CleaningType = "Сухая химчистка",
            Price = 1500m 
        },
        new () 
        { 
            Id = 2,
            Name = "Деловые костюмы",
            CleaningType = "Сухая чистка",
            Price = 1800m
        },
        new () 
        {
            Id = 3,
            Name = "Платья и юбки",
            CleaningType = "Деликатная чистка",
            Price = 1100m
        },
        new () 
        {
            Id = 4,
            Name = "Рубашки и блузы",
            CleaningType = "Аквачистка",
            Price = 600m
        },
        new () 
        {
            Id = 5,
            Name = "Трикотаж и свитера",
            CleaningType = "Бережная стирка",
            Price = 750m
        },
        new () 
        {
            Id = 6,
            Name = "Кожаные изделия",
            CleaningType = "Чистка с пропиткой",
            Price = 3500m
        },
        new () 
        {
            Id = 7,
            Name = "Обувь",
            CleaningType = "Комплексная чистка",
            Price = 2500m
        },
        new () 
        {
            Id = 8,
            Name = "Постельное белье",
            CleaningType = "Стирка и глажка",
            Price = 500m
        },
        new () 
        {
            Id = 9,
            Name = "Шторы и гардины",
            CleaningType = "Паровая обработка",
            Price = 950m
        },
        new ()
        {
            Id = 10,
            Name = "Пуховики",
            CleaningType = "Аквачистка с сушкой пера",
            Price = 2200m
        }
    ];

    /// <summary>
    /// 10 Клиентов
    /// </summary>
    public static readonly List<Client> Clients =
    [
        new ()
        {
            Id = 1,
            FullName = "Иванов Иван Иванович",
            PhoneNumber = "+79231112233"
        },
        new ()
        {
            Id = 2,
            FullName = "Петров Петр Петрович",
            PhoneNumber = "+79071023344"
        },
        new ()
        {
            Id = 3,
            FullName = "Сидорова Анна Сергеевна",
            PhoneNumber = "+79103334455"
        },
        new ()
        {
            Id = 4,
            FullName = "Смирнов Алексей Дмитриевич",
            PhoneNumber = "+79004445566"
        },
        new ()
        {
            Id = 5,
            FullName = "Кузнецова Елена Владимировна",
            PhoneNumber = "+79005556677"
        },
        new ()
        {
            Id = 6,
            FullName = "Попов Максим Олегович",
            PhoneNumber = "+79056612788"
        },
        new ()
        {
            Id = 7,
            FullName = "Васильева Ольга Николаевна",
            PhoneNumber = "+79007778899"
        },
        new ()
        {
            Id = 8,
            FullName = "Новиков Артем Игоревич",
            PhoneNumber = "+79278889900"
        },
        new ()
        {
            Id = 9,
            FullName = "Морозова Екатерина Павловна",
            PhoneNumber = "+79889990011"
        },
        new ()
        {
            Id = 10,
            FullName = "Федоров Сергей Александрович",
            PhoneNumber = "+79140001122"
        }
    ];

    /// <summary>
    /// 10 Изделий
    /// </summary>
    public static readonly List<Item> Items =
    [
        new ()
        {
            Id = 1,
            Name = "Пальто демисезонное",
            Material = "Пух",
            CategoryId = 1,
            Category = Categories[0]
        },
        new ()
        {
            Id = 2,
            Name = "Костюм",
            Material = "Шерсть",
            CategoryId = 2,
            Category = Categories[1]
        },
        new ()
        {
            Id = 3,
            Name = "Вечернее платье",
            Material = "Шелк",
            CategoryId = 3,
            Category = Categories[2]
        },
        new ()
        {
            Id = 4,
            Name = "Белая рубашка",
            Material = "Хлопок",
            CategoryId = 4,
            Category = Categories[3]
        },
        new ()
        {
            Id = 5,
            Name = "Шерстяной свитер",
            Material = "Шерсть",
            CategoryId = 5,
            Category = Categories[4]
        },
        new ()
        {
            Id = 6,
            Name = "Косуха",
            Material = "Натуральная кожа",
            CategoryId = 6,
            Category = Categories[5]
        },
        new ()
        {
            Id = 7,
            Name = "Замшевые ботинки",
            Material = "Замша",
            CategoryId = 7,
            Category = Categories[6]
        },
        new ()
        {
            Id = 8,
            Name = "Постельное бельё",
            Material = "Хлопок",
            CategoryId = 8,
            Category = Categories[7]
        },
        new ()
        {
            Id = 9,
            Name = "Тюль",
            Material = "Лен",
            CategoryId = 9,
            Category = Categories[8]
        },
        new ()
        {
            Id = 10,
            Name = "Зимний пуховик",
            Material = "Полиэстер/Пух",
            CategoryId = 10,
            Category = Categories[9]
        }
    ];

    /// <summary>
    /// 15 Заказов
    /// </summary>
    public static readonly List<Order> Orders =
    [
        new ()
        {
            Id = 1,
            ClientId = 1,
            Client = Clients[0],
            ItemId = 6,
            Item = Items[5],
            AcceptDate = BaseDate.AddDays(-20),
            ExeDays = 7,
            Status = Status.Given
        },
        new ()
        {
            Id = 2,
            ClientId = 1,
            Client = Clients[0],
            ItemId = 7,
            Item = Items[6],
            AcceptDate = BaseDate.AddDays(-10),
            ExeDays = 5,
            Status = Status.Completed
        },
        new ()
        {
            Id = 3,
            ClientId = 1,
            Client = Clients[0],
            ItemId = 1,
            Item = Items[0],
            AcceptDate = BaseDate.AddDays(-2),
            ExeDays = 14,
            Status = Status.InProgress
        },
        new ()
        {
            Id = 4,
            ClientId = 2,
            Client = Clients[1],
            ItemId = 2,
            Item = Items[1],
            AcceptDate = BaseDate.AddDays(-5),
            ExeDays = 4,
            Status = Status.InProgress
        },
        new ()
        {
            Id = 5,
            ClientId = 2,
            Client = Clients[1],
            ItemId = 4,
            Item = Items[3],
            AcceptDate = BaseDate.AddDays(-1),
            ExeDays = 2,
            Status = Status.InProgress
        },
        new ()
        {
            Id = 6,
            ClientId = 3,
            Client = Clients[2],
            ItemId = 3,
            Item = Items[2],
            AcceptDate = BaseDate.AddDays(-15),
            ExeDays = 3,
            Status = Status.Given
        },
        new ()
        {
            Id = 7,
            ClientId = 3,
            Client = Clients[2],
            ItemId = 8,
            Item = Items[7],
            AcceptDate = BaseDate.AddDays(-14),
            ExeDays = 3,
            Status = Status.Given
        },
        new ()
        {
            Id = 8,
            ClientId = 4,
            Client = Clients[3],
            ItemId = 10,
            Item = Items[9],
            AcceptDate = BaseDate.AddDays(-30),
            ExeDays = 10,
            Status = Status.Given
        },
        new ()
        {
            Id = 9,
            ClientId = 5,
            Client = Clients[4],
            ItemId = 5,
            Item = Items[4],
            AcceptDate = BaseDate.AddDays(-8),
            ExeDays = 12,
            Status = Status.Completed
        },
        new ()
        {
            Id = 10,
            ClientId = 6,
            Client = Clients[5],
            ItemId = 9,
            Item = Items[8],
            AcceptDate = BaseDate.AddDays(-400),
            ExeDays = 4,
            Status = Status.Given
        },
        new ()
        {
            Id = 11,
            ClientId = 7,
            Client = Clients[6],
            ItemId = 4,
            Item = Items[3],
            AcceptDate = BaseDate.AddDays(-4),
            ExeDays = 1,
            Status = Status.InProgress
        },
        new ()
        {
            Id = 12,
            ClientId = 8,
            Client = Clients[7],
            ItemId = 1,
            Item = Items[0],
            AcceptDate = BaseDate.AddDays(-50),
            ExeDays = 6,
            Status = Status.Given
        },
        new()
        {
            Id = 13,
            ClientId = 10,
            Client = Clients[9],
            ItemId = 6,
            Item = Items[5],
            AcceptDate = BaseDate.AddDays(-450),
            ExeDays = 5,
            Status = Status.Given
        },
        new()
        {
            Id = 14,
            ClientId = 10,
            Client = Clients[9],
            ItemId = 7,
            Item = Items[6],
            AcceptDate = BaseDate.AddDays(-450),
            ExeDays = 5,
            Status = Status.Given
        },
        new()
        {
            Id = 15,
            ClientId = 10,
            Client = Clients[9],
            ItemId = 1,
            Item = Items[0],
            AcceptDate = BaseDate.AddDays(-450),
            ExeDays = 5,
            Status = Status.Given
        }
    ];
}
