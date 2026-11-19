using FoodDelivery.Domain;

namespace FoodDelivery.Tests.Fixtures;

/// <summary>
/// Фикстура данных
/// </summary>
public class FoodDeliveryFixture
{
    /// <summary>
    /// Список категорий блюд
    /// </summary>
    public List<Category> Categories { get; }

    /// <summary>
    /// Список блюд
    /// </summary>
    public List<Dish> Dishes { get; }

    /// <summary>
    /// Список ресторанов
    /// </summary>
    public List<Restaurant> Restaurants { get; }

    /// <summary>
    /// Список клиентов
    /// </summary>
    public List<Client> Clients { get; }

    /// <summary>
    /// Список заказов
    /// </summary>
    public List<Order> Orders { get; }

    /// <summary>
    /// Инициализирует тестовые данные фикстуры
    /// </summary>
    public FoodDeliveryFixture()
    {
        // Последовательно вызываем методы наполнения структуры
        Categories = GetCategoriesList();
        Dishes = GetDishesList();
        Restaurants = GetRestaurantsList();
        Clients = GetClientsList();

        // Заказы наполняются в самом конце, так как они зависят от всех верхних списков
        Orders = GetOrdersList();
    }

    private static List<Category> GetCategoriesList() =>
    [
        new Category
        {
            Id = 1,
            Name = "Пицца"
        },
        new Category
        {
            Id = 2,
            Name = "Суши"
        },
        new Category
        {
            Id = 3,
            Name = "Бургеры"
        },
        new Category
        {
            Id = 4,
            Name = "Десерты"
        },
        new Category
        {
            Id = 5,
            Name = "Напитки"
        },
        new Category
        {
            Id = 6,
            Name = "Салаты"
        },
        new Category
        {
            Id = 7,
            Name = "Супы"
        },
        new Category
        {
            Id = 8,
            Name = "Выпечка"
        },
        new Category
        {
            Id = 9,
            Name = "Паста"
        },
        new Category
        {
            Id = 10,
            Name = "Шашлык"
        }
    ];

    private List<Dish> GetDishesList() =>
    [
        new Dish
        {
            Id = 1,
            Title = "Пепперони",
            Weight = 500,
            Price = 700m,
            CategoryId = 1,
            Category = Categories[0]
        },
        new Dish
        {
            Id = 2,
            Title = "Маргарита",
            Weight = 450,
            Price = 550m,
            CategoryId = 1,
            Category = Categories[0]
        },
        new Dish
        {
            Id = 3,
            Title = "Филадельфия",
            Weight = 220,
            Price = 450m,
            CategoryId = 2,
            Category = Categories[1]
        },
        new Dish
        {
            Id = 4,
            Title = "Калифорния",
            Weight = 210,
            Price = 400m,
            CategoryId = 2,
            Category = Categories[1]
        },
        new Dish
        {
            Id = 5,
            Title = "Чизбургер",
            Weight = 300,
            Price = 350m,
            CategoryId = 3,
            Category = Categories[2]
        },
        new Dish
        {
            Id = 6,
            Title = "Воппер",
            Weight = 350,
            Price = 400m,
            CategoryId = 3,
            Category = Categories[2]
        },
        new Dish
        {
            Id = 7,
            Title = "Чизкейк",
            Weight = 150,
            Price = 300m,
            CategoryId = 4,
            Category = Categories[3]
        },
        new Dish
        {
            Id = 8,
            Title = "Тирамису",
            Weight = 130,
            Price = 350m,
            CategoryId = 4,
            Category = Categories[3]
        },
        new Dish
        {
            Id = 9,
            Title = "Морс Клюквенный",
            Weight = 500,
            Price = 150m,
            CategoryId = 5,
            Category = Categories[4]
        },
        new Dish
        {
            Id = 10,
            Title = "Кока-Кола",
            Weight = 500,
            Price = 120m,
            CategoryId = 5,
            Category = Categories[4]
        },
        new Dish
        {
            Id = 11,
            Title = "Цезарь",
            Weight = 200,
            Price = 420m,
            CategoryId = 6,
            Category = Categories[5]
        },
        new Dish
        {
            Id = 12,
            Title = "Борщ",
            Weight = 400,
            Price = 380m,
            CategoryId = 7,
            Category = Categories[6]
        },
        new Dish
        {
            Id = 13,
            Title = "Круассан",
            Weight = 80,
            Price = 180m,
            CategoryId = 8,
            Category = Categories[7]
        },
        new Dish
        {
            Id = 14,
            Title = "Карбонара",
            Weight = 350,
            Price = 520m,
            CategoryId = 9,
            Category = Categories[8]
        },
        new Dish
        {
            Id = 15,
            Title = "Шашлык из свинины",
            Weight = 300,
            Price = 650m,
            CategoryId = 10,
            Category = Categories[9]
        }
    ];

    private static List<Restaurant> GetRestaurantsList() =>
    [
        new Restaurant
        {
            Id = 1,
            Name = "Додо Пицца",
            Address = "ул. Ленина, 5",
            Rating = 4.8,
            WorkingHours = "10:00-23:00"
        },
        new Restaurant
        {
            Id = 2,
            Name = "Тануки",
            Address = "ул. Садовая, 12",
            Rating = 4.5,
            WorkingHours = "11:00-23:00"
        },
        new Restaurant
        {
            Id = 3,
            Name = "Вкусно и точка",
            Address = "пр. Мира, 3",
            Rating = 4.2,
            WorkingHours = "06:00-00:00"
        },
        new Restaurant
        {
            Id = 4,
            Name = "Шоколадница",
            Address = "ул. Чехова, 8",
            Rating = 4.4,
            WorkingHours = "08:00-22:00"
        },
        new Restaurant
        {
            Id = 5,
            Name = "Папа Джонс",
            Address = "ул. Новая, 1",
            Rating = 4.6,
            WorkingHours = "10:00-23:00"
        },
        new Restaurant
        {
            Id = 6,
            Name = "Якитория",
            Address = "ул. Гагарина, 4",
            Rating = 4.3,
            WorkingHours = "11:00-23:00"
        },
        new Restaurant
        {
            Id = 7,
            Name = "Бургер Кинг",
            Address = "ул. Кирова, 17",
            Rating = 4.1,
            WorkingHours = "09:00-22:00"
        },
        new Restaurant
        {
            Id = 8,
            Name = "Теремок",
            Address = "ул. Полевая, 10",
            Rating = 4.4,
            WorkingHours = "10:00-21:00"
        },
        new Restaurant
        {
            Id = 9,
            Name = "Марчеллис",
            Address = "пр. Просвещения, 2",
            Rating = 4.7,
            WorkingHours = "12:00-23:00"
        },
        new Restaurant
        {
            Id = 10,
            Name = "Мясо и Вино",
            Address = "ул. Набережная, 15",
            Rating = 4.9,
            WorkingHours = "12:00-01:00"
        }
    ];

    private static List<Client> GetClientsList() =>
    [
        new Client
        {
            Id = 1,
            FullName = "Иванов Иван Иванович",
            Phone = "+79991112233",
            DeliveryAddress = "ул. Гагарина, 1"
        },
        new Client
        {
            Id = 2,
            FullName = "Петрова Анна Сергеевна",
            Phone = "+79994445566",
            DeliveryAddress = "ул. Полевая, 12"
        },
        new Client
        {
            Id = 3,
            FullName = "Смирнов Дмитрий Александрович",
            Phone = "+79997778899",
            DeliveryAddress = "ул. Лесная, 5"
        },
        new Client
        {
            Id = 4,
            FullName = "Алексеева Ольга Игоревна",
            Phone = "+79992223344",
            DeliveryAddress = "ул. Чехова, 3"
        },
        new Client
        {
            Id = 5,
            FullName = "Кузнецов Сергей Петрович",
            Phone = "+79995556677",
            DeliveryAddress = "ул. Новая, 8"
        },
        new Client
        {
            Id = 6,
            FullName = "Васильева Елена Николаевна",
            Phone = "+79998889900",
            DeliveryAddress = "ул. Пушкина, 15"
        },
        new Client
        {
            Id = 7,
            FullName = "Попов Игорь Сергеевич",
            Phone = "+79993334455",
            DeliveryAddress = "ул. Садовая, 2"
        },
        new Client
        {
            Id = 8,
            FullName = "Морозова Наталья Юрьевна",
            Phone = "+79996667788",
            DeliveryAddress = "ул. Полевая, 21"
        },
        new Client
        {
            Id = 9,
            FullName = "Федоров Андрей Олегович",
            Phone = "+79999990011",
            DeliveryAddress = "ул. Лесная, 4"
        },
        new Client
        {
            Id = 10,
            FullName = "Соколов Денис Витальевич",
            Phone = "+79991239876",
            DeliveryAddress = "ул. Набережная, 30"
        }
    ];

    private List<Order> GetOrdersList() =>
    [
        new Order
        {
            Id = 1,
            CookedAt = new DateTime(2026, 10, 1, 12, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 1, 12, 40, 0),
            ClientId = Clients[0].Id,
            Client = Clients[0],
            RestaurantId = Restaurants[0].Id,
            Restaurant = Restaurants[0],
            Items =
            [
                new OrderItem
                {
                    Id = 1,
                    OrderId = 1,
                    DishId = 1,
                    Dish = Dishes[0],
                    Quantity = 2
                }
            ]
        },
        new Order
        {
            Id = 2,
            CookedAt = new DateTime(2026, 10, 1, 13, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 1, 13, 35, 0),
            ClientId = Clients[1].Id,
            Client = Clients[1],
            RestaurantId = Restaurants[0].Id,
            Restaurant = Restaurants[0],
            Items =
            [
                new OrderItem
                {
                    Id = 2,
                    OrderId = 2,
                    DishId = 2,
                    Dish = Dishes[1],
                    Quantity = 1
                }
            ]
        },
        new Order
        {
            Id = 3,
            CookedAt = new DateTime(2026, 10, 2, 18, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 2, 18, 50, 0),
            ClientId = Clients[2].Id,
            Client = Clients[2],
            RestaurantId = Restaurants[0].Id,
            Restaurant = Restaurants[0],
            Items =
            [
                new OrderItem
                {
                    Id = 3,
                    OrderId = 3,
                    DishId = 1,
                    Dish = Dishes[0],
                    Quantity = 3
                }
            ]
        },
        new Order
        {
            Id = 4,
            CookedAt = new DateTime(2026, 10, 2, 19, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 2, 19, 25, 0),
            ClientId = Clients[0].Id,
            Client = Clients[0],
            RestaurantId = Restaurants[0].Id,
            Restaurant = Restaurants[0],
            Items =
            [
                new OrderItem
                {
                    Id = 4,
                    OrderId = 4,
                    DishId = 2,
                    Dish = Dishes[1],
                    Quantity = 2
                }
            ]
        },
        new Order
        {
            Id = 5,
            CookedAt = new DateTime(2026, 10, 3, 12, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 3, 13, 0, 0),
            ClientId = Clients[3].Id,
            Client = Clients[3],
            RestaurantId = Restaurants[1].Id,
            Restaurant = Restaurants[1],
            Items =
            [
                new OrderItem
                {
                    Id = 5,
                    OrderId = 5,
                    DishId = 3,
                    Dish = Dishes[2],
                    Quantity = 4
                }
            ]
        },
        new Order
        {
            Id = 6,
            CookedAt = new DateTime(2026, 10, 3, 14, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 3, 14, 45, 0),
            ClientId = Clients[4].Id,
            Client = Clients[4],
            RestaurantId = Restaurants[1].Id,
            Restaurant = Restaurants[1],
            Items =
            [
                new OrderItem
                {
                    Id = 6,
                    OrderId = 6,
                    DishId = 4,
                    Dish = Dishes[3],
                    Quantity = 2
                }
            ]
        },
        new Order
        {
            Id = 7,
            CookedAt = new DateTime(2026, 10, 4, 13, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 4, 13, 55, 0),
            ClientId = Clients[3].Id,
            Client = Clients[3],
            RestaurantId = Restaurants[1].Id,
            Restaurant = Restaurants[1],
            Items =
            [
                new OrderItem
                {
                    Id = 7,
                    OrderId = 7,
                    DishId = 3,
                    Dish = Dishes[2],
                    Quantity = 6
                }
            ]
        },
        new Order
        {
            Id = 8,
            CookedAt = new DateTime(2026, 10, 4, 19, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 4, 20, 10, 0),
            ClientId = Clients[5].Id,
            Client = Clients[5],
            RestaurantId = Restaurants[1].Id,
            Restaurant = Restaurants[1],
            Items =
            [
                new OrderItem
                {
                    Id = 8,
                    OrderId = 8,
                    DishId = 4,
                    Dish = Dishes[3],
                    Quantity = 3
                }
            ]
        },
        new Order
        {
            Id = 9,
            CookedAt = new DateTime(2026, 10, 5, 20, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 5, 20, 40, 0),
            ClientId = Clients[6].Id,
            Client = Clients[6],
            RestaurantId = Restaurants[1].Id,
            Restaurant = Restaurants[1],
            Items =
            [
                new OrderItem
                {
                    Id = 9,
                    OrderId = 9,
                    DishId = 3,
                    Dish = Dishes[2],
                    Quantity = 2
                }
            ]
        },
        // 20 мин - самый быстрый
        new Order
        {
            Id = 10,
            CookedAt = new DateTime(2026, 10, 1, 8, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 1, 8, 20, 0),
            ClientId = Clients[7].Id,
            Client = Clients[7],
            RestaurantId = Restaurants[2].Id,
            Restaurant = Restaurants[2],
            Items =
            [
                new OrderItem
                {
                    Id = 10,
                    OrderId = 10,
                    DishId = 5,
                    Dish = Dishes[4],
                    Quantity = 2
                }
            ]
        },
        // 20 мин - самый быстрый
        new Order
        {
            Id = 11,
            CookedAt = new DateTime(2026, 10, 2, 9, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 2, 9, 20, 0),
            ClientId = Clients[8].Id,
            Client = Clients[8],
            RestaurantId = Restaurants[2].Id,
            Restaurant = Restaurants[2],
            Items =
            [
                new OrderItem
                {
                    Id = 11,
                    OrderId = 11,
                    DishId = 6,
                    Dish = Dishes[5],
                    Quantity = 2
                }
            ]
        },
        new Order
        {
            Id = 12,
            CookedAt = new DateTime(2026, 10, 5, 15, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 5, 15, 30, 0),
            ClientId = Clients[9].Id,
            Client = Clients[9],
            RestaurantId = Restaurants[2].Id,
            Restaurant = Restaurants[2],
            Items =
            [
                new OrderItem
                {
                    Id = 12,
                    OrderId = 12,
                    DishId = 5,
                    Dish = Dishes[4],
                    Quantity = 3
                }
            ]
        },
        new Order
        {
            Id = 13,
            CookedAt = new DateTime(2026, 10, 1, 16, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 1, 16, 45, 0),
            ClientId = Clients[1].Id,
            Client = Clients[1],
            RestaurantId = Restaurants[3].Id,
            Restaurant = Restaurants[3],
            Items =
            [
                new OrderItem
                {
                    Id = 13,
                    OrderId = 13,
                    DishId = 7,
                    Dish = Dishes[6],
                    Quantity = 2
                }
            ]
        },
        new Order
        {
            Id = 14,
            CookedAt = new DateTime(2026, 10, 4, 11, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 4, 11, 35, 0),
            ClientId = Clients[2].Id,
            Client = Clients[2],
            RestaurantId = Restaurants[3].Id,
            Restaurant = Restaurants[3],
            Items =
            [
                new OrderItem
                {
                    Id = 14,
                    OrderId = 14,
                    DishId = 8,
                    Dish = Dishes[7],
                    Quantity = 3
                }
            ]
        },
        new Order
        {
            Id = 15,
            CookedAt = new DateTime(2026, 10, 2, 21, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 2, 21, 45, 0),
            ClientId = Clients[0].Id,
            Client = Clients[0],
            RestaurantId = Restaurants[4].Id,
            Restaurant = Restaurants[4],
            Items =
            [
                new OrderItem
                {
                    Id = 15,
                    OrderId = 15,
                    DishId = 1,
                    Dish = Dishes[0],
                    Quantity = 2
                }
            ]
        },
        new Order
        {
            Id = 16,
            CookedAt = new DateTime(2026, 10, 6, 18, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 6, 18, 55, 0),
            ClientId = Clients[4].Id,
            Client = Clients[4],
            RestaurantId = Restaurants[5].Id,
            Restaurant = Restaurants[5],
            Items =
            [
                new OrderItem
                {
                    Id = 16,
                    OrderId = 16,
                    DishId = 3,
                    Dish = Dishes[2],
                    Quantity = 3
                }
            ]
        },
        new Order
        {
            Id = 17,
            CookedAt = new DateTime(2026, 10, 6, 19, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 6, 19, 45, 0),
            ClientId = Clients[5].Id,
            Client = Clients[5],
            RestaurantId = Restaurants[5].Id,
            Restaurant = Restaurants[5],
            Items =
            [
                new OrderItem
                {
                    Id = 17,
                    OrderId = 17,
                    DishId = 9,
                    Dish = Dishes[8],
                    Quantity = 4
                }
            ]
        },
        new Order
        {
            Id = 18,
            CookedAt = new DateTime(2026, 10, 7, 13, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 7, 13, 35, 0),
            ClientId = Clients[6].Id,
            Client = Clients[6],
            RestaurantId = Restaurants[8].Id,
            Restaurant = Restaurants[8],
            Items =
            [
                new OrderItem
                {
                    Id = 18,
                    OrderId = 18,
                    DishId = 14,
                    Dish = Dishes[13],
                    Quantity = 2
                }
            ]
        },
        new Order
        {
            Id = 19,
            CookedAt = new DateTime(2026, 10, 7, 14, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 7, 14, 40, 0),
            ClientId = Clients[7].Id,
            Client = Clients[7],
            RestaurantId = Restaurants[8].Id,
            Restaurant = Restaurants[8],
            Items =
            [
                new OrderItem
                {
                    Id = 19,
                    OrderId = 19,
                    DishId = 14,
                    Dish = Dishes[13],
                    Quantity = 3
                }
            ]
        },
        // Ольга Алексеева тратит 6500р разом!
        new Order
        {
            Id = 20,
            CookedAt = new DateTime(2026, 10, 8, 21, 0, 0),
            DeliveredAt = new DateTime(2026, 10, 8, 22, 10, 0),
            ClientId = Clients[3].Id,
            Client = Clients[3],
            RestaurantId = Restaurants[9].Id,
            Restaurant = Restaurants[9],
            Items =
            [
                new OrderItem
                {
                    Id = 20,
                    OrderId = 20,
                    DishId = 15,
                    Dish = Dishes[14],
                    Quantity = 10
                }
            ]
        }
    ];
}
