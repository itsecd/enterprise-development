using DryCleaning.Domain.Models;

namespace DryCleaning.Domain.Data;

public static class DataSeed
{

    public static readonly DateTime BaseDate = new(2026, 10, 1);


    /// <summary>
    /// 10 Категорий
    /// </summary>
    public static List<Category> GetCategories() =>
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
    public static List<Client> GetClients() =>
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
    public static List<Item> GetItems()
    {

        var categories = GetCategories();

        return
        [
            new ()
            {
                Id = 1,
                Name = "Пальто демисезонное",
                Material = "Пух",
                CategoryId = 1,
                Category = categories[0]
            },
            new ()
            {
                Id = 2,
                Name = "Костюм",
                Material = "Шерсть",
                CategoryId = 2,
                Category = categories[1]
            },
            new ()
            {
                Id = 3,
                Name = "Вечернее платье",
                Material = "Шелк",
                CategoryId = 3,
                Category = categories[2]
            },
            new ()
            {
                Id = 4,
                Name = "Белая рубашка",
                Material = "Хлопок",
                CategoryId = 4,
                Category = categories[3]
            },
            new ()
            {
                Id = 5,
                Name = "Шерстяной свитер",
                Material = "Шерсть",
                CategoryId = 5,
                Category = categories[4]
            },
            new ()
            {
                Id = 6,
                Name = "Косуха",
                Material = "Натуральная кожа",
                CategoryId = 6,
                Category = categories[5]
            },
            new ()
            {
                Id = 7,
                Name = "Замшевые ботинки",
                Material = "Замша",
                CategoryId = 7,
                Category = categories[6]
            },
            new ()
            {
                Id = 8,
                Name = "Постельное бельё",
                Material = "Хлопок",
                CategoryId = 8,
                Category = categories[7]
            },
            new ()
            {
                Id = 9,
                Name = "Тюль",
                Material = "Лен",
                CategoryId = 9,
                Category = categories[8]
            },
            new ()
            {
                Id = 10,
                Name = "Зимний пуховик",
                Material = "Полиэстер/Пух",
                CategoryId = 10,
                Category = categories[9]
            }
        ];
    }


    /// <summary>
    /// 15 заказов
    /// </summary>
    public static List<Order> GetOrders()
    {

        var clients = GetClients();
        var items = GetItems();

        return
        [
            new ()
            {
                Id = 1,
                ClientId = 1,
                Client = clients[0],
                ItemId = 6,
                Item = items[5],
                AcceptDate = BaseDate.AddDays(-20),
                ExeDays = 7,
                Status = Status.Given
            },
            new ()
            {
                Id = 2,
                ClientId = 1,
                Client = clients[0],
                ItemId = 7,
                Item = items[6],
                AcceptDate = BaseDate.AddDays(-10),
                ExeDays = 5,
                Status = Status.Completed
            },
            new ()
            {
                Id = 3,
                ClientId = 1,
                Client = clients[0],
                ItemId = 1,
                Item = items[0],
                AcceptDate = BaseDate.AddDays(-2),
                ExeDays = 14,
                Status = Status.InProgress
            },
            new ()
            {
                Id = 4,
                ClientId = 2,
                Client = clients[1],
                ItemId = 2,
                Item = items[1],
                AcceptDate = BaseDate.AddDays(-5),
                ExeDays = 4,
                Status = Status.InProgress
            },
            new ()
            {
                Id = 5,
                ClientId = 2,
                Client = clients[1],
                ItemId = 4,
                Item = items[3],
                AcceptDate = BaseDate.AddDays(-1),
                ExeDays = 2,
                Status = Status.InProgress
            },
            new ()
            {
                Id = 6,
                ClientId = 3,
                Client = clients[2],
                ItemId = 3,
                Item = items[2],
                AcceptDate = BaseDate.AddDays(-15),
                ExeDays = 3,
                Status = Status.Given
            },
            new ()
            {
                Id = 7,
                ClientId = 3,
                Client = clients[2],
                ItemId = 8,
                Item = items[7],
                AcceptDate = BaseDate.AddDays(-14),
                ExeDays = 3,
                Status = Status.Given
            },
            new ()
            {
                Id = 8,
                ClientId = 4,
                Client = clients[3],
                ItemId = 10,
                Item = items[9],
                AcceptDate = BaseDate.AddDays(-30),
                ExeDays = 10,
                Status = Status.Given
            },
            new ()
            {
                Id = 9,
                ClientId = 5,
                Client = clients[4],
                ItemId = 5,
                Item = items[4],
                AcceptDate = BaseDate.AddDays(-8),
                ExeDays = 12,
                Status = Status.Completed
            },
            new ()
            {
                Id = 10,
                ClientId = 6,
                Client = clients[5],
                ItemId = 9,
                Item = items[8],
                AcceptDate = BaseDate.AddDays(-400),
                ExeDays = 4,
                Status = Status.Given
            },
            new ()
            {
                Id = 11,
                ClientId = 7,
                Client = clients[6],
                ItemId = 4,
                Item = items[3],
                AcceptDate = BaseDate.AddDays(-4),
                ExeDays = 1,
                Status = Status.InProgress
            },
            new ()
            {
                Id = 12,
                ClientId = 8,
                Client = clients[7],
                ItemId = 1,
                Item = items[0],
                AcceptDate = BaseDate.AddDays(-50),
                ExeDays = 6,
                Status = Status.Given
            },
            new()
            {
                Id = 13,
                ClientId = 10,
                Client = clients[9],
                ItemId = 6,
                Item = items[5],
                AcceptDate = BaseDate.AddDays(-450),
                ExeDays = 5,
                Status = Status.Given
            },
            new()
            {
                Id = 14,
                ClientId = 10,
                Client = clients[9],
                ItemId = 7,
                Item = items[6],
                AcceptDate = BaseDate.AddDays(-450),
                ExeDays = 5,
                Status = Status.Given
            },
            new()
            {
                Id = 15,
                ClientId = 10,
                Client = clients[9],
                ItemId = 1,
                Item = items[0],
                AcceptDate = BaseDate.AddDays(-450),
                ExeDays = 5,
                Status = Status.Given
            }
        ];
    }
}
