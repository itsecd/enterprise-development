using EnterpriseDevelopment.Domain.Entities;

namespace EnterpriseDevelopment.Domain;

/// <summary>
/// Набор данных для первой лабораторной работы.
/// </summary>
public class DataSeeder
{
    /// <summary>
    /// Категории блюд.
    /// </summary>
    public IReadOnlyList<DishCategory> DishCategories { get; } =
    [
        new() { Id = 1, Name = "Пицца" },
        new() { Id = 2, Name = "Суши" },
        new() { Id = 3, Name = "Бургеры" },
        new() { Id = 4, Name = "Салаты" },
        new() { Id = 5, Name = "Супы" },
        new() { Id = 6, Name = "Паста" },
        new() { Id = 7, Name = "Десерты" },
        new() { Id = 8, Name = "Напитки" },
        new() { Id = 9, Name = "Завтраки" },
        new() { Id = 10, Name = "Выпечка" }
    ];

    /// <summary>
    /// Рестораны.
    /// </summary>
    public IReadOnlyList<Restaurant> Restaurants { get; } =
    [
        new() { Id = 1, Name = "Пицца Хаус", Address = "ул. Ленина, 10", Rating = 4.8, OpeningTime = new TimeOnly(10, 0), ClosingTime = new TimeOnly(23, 0) },
        new() { Id = 2, Name = "Суши Мастер", Address = "ул. Гагарина, 25", Rating = 4.7, OpeningTime = new TimeOnly(11, 0), ClosingTime = new TimeOnly(0, 0) },
        new() { Id = 3, Name = "Бургер Лаб", Address = "пр. Мира, 7", Rating = 4.6, OpeningTime = new TimeOnly(10, 0), ClosingTime = new TimeOnly(2, 0) },
        new() { Id = 4, Name = "Зелёный Салат", Address = "ул. Советская, 14", Rating = 4.5, OpeningTime = new TimeOnly(9, 0), ClosingTime = new TimeOnly(22, 0) },
        new() { Id = 5, Name = "Восточный Двор", Address = "ул. Пушкина, 31", Rating = 4.4, OpeningTime = new TimeOnly(11, 0), ClosingTime = new TimeOnly(23, 0) },
        new() { Id = 6, Name = "Паста Бар", Address = "ул. Кирова, 8", Rating = 4.3, OpeningTime = new TimeOnly(10, 0), ClosingTime = new TimeOnly(23, 0) },
        new() { Id = 7, Name = "Сладкий Дом", Address = "ул. Молодёжная, 3", Rating = 4.2, OpeningTime = new TimeOnly(8, 0), ClosingTime = new TimeOnly(22, 0) },
        new() { Id = 8, Name = "Кофе Тайм", Address = "ул. Центральная, 18", Rating = 4.1, OpeningTime = new TimeOnly(7, 0), ClosingTime = new TimeOnly(21, 0) },
        new() { Id = 9, Name = "Утро", Address = "пр. Победы, 12", Rating = 4.0, OpeningTime = new TimeOnly(7, 0), ClosingTime = new TimeOnly(20, 0) },
        new() { Id = 10, Name = "Дом Выпечки", Address = "ул. Школьная, 6", Rating = 3.9, OpeningTime = new TimeOnly(8, 0), ClosingTime = new TimeOnly(21, 0) }
    ];

    /// <summary>
    /// Клиенты.
    /// </summary>
    public IReadOnlyList<Client> Clients { get; } =
    [
        new() { Id = 1, FullName = "Иванов Иван Иванович", PhoneNumber = "+7-900-000-0001", DeliveryAddress = "ул. Лесная, 1" },
        new() { Id = 2, FullName = "Петров Пётр Петрович", PhoneNumber = "+7-900-000-0002", DeliveryAddress = "ул. Лесная, 2" },
        new() { Id = 3, FullName = "Сидорова Анна Сергеевна", PhoneNumber = "+7-900-000-0003", DeliveryAddress = "ул. Лесная, 3" },
        new() { Id = 4, FullName = "Кузнецов Алексей Дмитриевич", PhoneNumber = "+7-900-000-0004", DeliveryAddress = "ул. Лесная, 4" },
        new() { Id = 5, FullName = "Смирнова Мария Андреевна", PhoneNumber = "+7-900-000-0005", DeliveryAddress = "ул. Лесная, 5" },
        new() { Id = 6, FullName = "Попов Дмитрий Олегович", PhoneNumber = "+7-900-000-0006", DeliveryAddress = "ул. Лесная, 6" },
        new() { Id = 7, FullName = "Васильева Елена Викторовна", PhoneNumber = "+7-900-000-0007", DeliveryAddress = "ул. Лесная, 7" },
        new() { Id = 8, FullName = "Морозов Николай Игоревич", PhoneNumber = "+7-900-000-0008", DeliveryAddress = "ул. Лесная, 8" },
        new() { Id = 9, FullName = "Волкова Ольга Романовна", PhoneNumber = "+7-900-000-0009", DeliveryAddress = "ул. Лесная, 9" },
        new() { Id = 10, FullName = "Фёдоров Сергей Максимович", PhoneNumber = "+7-900-000-0010", DeliveryAddress = "ул. Лесная, 10" }
    ];

    /// <summary>
    /// Блюда.
    /// </summary>
    public IReadOnlyList<Dish> Dishes { get; }

    /// <summary>
    /// Заказы.
    /// </summary>
    public IReadOnlyList<Order> Orders { get; }

    /// <summary>
    /// Позиции заказов.
    /// </summary>
    public IReadOnlyList<OrderItem> OrderItems { get; }

    /// <summary>
    /// Инициализирует набор тестовых данных.
    /// </summary>
    public DataSeeder()
    {
        Dishes =
        [
            new() { Id = 1, Name = "Маргарита", WeightInGrams = 450, Price = 550m, CategoryId = 1, Category = DishCategories[0], RestaurantId = 1, Restaurant = Restaurants[0] },
            new() { Id = 2, Name = "Пепперони", WeightInGrams = 470, Price = 650m, CategoryId = 1, Category = DishCategories[0], RestaurantId = 1, Restaurant = Restaurants[0] },
            new() { Id = 3, Name = "Филадельфия", WeightInGrams = 300, Price = 690m, CategoryId = 2, Category = DishCategories[1], RestaurantId = 2, Restaurant = Restaurants[1] },
            new() { Id = 4, Name = "Калифорния", WeightInGrams = 280, Price = 620m, CategoryId = 2, Category = DishCategories[1], RestaurantId = 2, Restaurant = Restaurants[1] },
            new() { Id = 5, Name = "Чизбургер", WeightInGrams = 320, Price = 490m, CategoryId = 3, Category = DishCategories[2], RestaurantId = 3, Restaurant = Restaurants[2] },
            new() { Id = 6, Name = "Двойной бургер", WeightInGrams = 420, Price = 690m, CategoryId = 3, Category = DishCategories[2], RestaurantId = 3, Restaurant = Restaurants[2] },
            new() { Id = 7, Name = "Цезарь", WeightInGrams = 250, Price = 420m, CategoryId = 4, Category = DishCategories[3], RestaurantId = 4, Restaurant = Restaurants[3] },
            new() { Id = 8, Name = "Том Ям", WeightInGrams = 380, Price = 590m, CategoryId = 5, Category = DishCategories[4], RestaurantId = 5, Restaurant = Restaurants[4] },
            new() { Id = 9, Name = "Карбонара", WeightInGrams = 360, Price = 520m, CategoryId = 6, Category = DishCategories[5], RestaurantId = 6, Restaurant = Restaurants[5] },
            new() { Id = 10, Name = "Чизкейк", WeightInGrams = 180, Price = 350m, CategoryId = 7, Category = DishCategories[6], RestaurantId = 7, Restaurant = Restaurants[6] },
            new() { Id = 11, Name = "Капучино", WeightInGrams = 300, Price = 220m, CategoryId = 8, Category = DishCategories[7], RestaurantId = 8, Restaurant = Restaurants[7] },
            new() { Id = 12, Name = "Омлет с овощами", WeightInGrams = 280, Price = 390m, CategoryId = 9, Category = DishCategories[8], RestaurantId = 9, Restaurant = Restaurants[8] },
            new() { Id = 13, Name = "Круассан", WeightInGrams = 120, Price = 190m, CategoryId = 10, Category = DishCategories[9], RestaurantId = 10, Restaurant = Restaurants[9] }
        ];

        Orders =
        [
            CreateOrder(1, Clients[0], Restaurants[0], new DateTimeOffset(2026, 2, 5, 12, 0, 0, TimeSpan.Zero), 30, 1800m),
            CreateOrder(2, Clients[0], Restaurants[0], new DateTimeOffset(2026, 2, 6, 13, 0, 0, TimeSpan.Zero), 45, 1600m),
            CreateOrder(3, Clients[0], Restaurants[0], new DateTimeOffset(2026, 2, 7, 18, 0, 0, TimeSpan.Zero), 60, 1950m),
            CreateOrder(4, Clients[0], Restaurants[0], new DateTimeOffset(2026, 2, 8, 19, 0, 0, TimeSpan.Zero), 15, 1950m),
            CreateOrder(5, Clients[1], Restaurants[1], new DateTimeOffset(2026, 2, 9, 12, 0, 0, TimeSpan.Zero), 40, 900m),
            CreateOrder(6, Clients[2], Restaurants[1], new DateTimeOffset(2026, 2, 10, 13, 0, 0, TimeSpan.Zero), 25, 1100m),
            CreateOrder(7, Clients[3], Restaurants[1], new DateTimeOffset(2026, 2, 11, 18, 0, 0, TimeSpan.Zero), 55, 980m),
            CreateOrder(8, Clients[4], Restaurants[2], new DateTimeOffset(2026, 2, 12, 19, 0, 0, TimeSpan.Zero), 20, 1200m),
            CreateOrder(9, Clients[5], Restaurants[2], new DateTimeOffset(2026, 2, 13, 19, 30, 0, TimeSpan.Zero), 35, 850m),
            CreateOrder(10, Clients[6], Restaurants[2], new DateTimeOffset(2026, 2, 14, 20, 0, 0, TimeSpan.Zero), 45, 1050m),
            CreateOrder(11, Clients[7], Restaurants[3], new DateTimeOffset(2026, 3, 1, 12, 0, 0, TimeSpan.Zero), 60, 700m),
            CreateOrder(12, Clients[8], Restaurants[4], new DateTimeOffset(2026, 3, 2, 13, 0, 0, TimeSpan.Zero), 30, 800m),
            CreateOrder(13, Clients[9], Restaurants[5], new DateTimeOffset(2026, 3, 3, 14, 0, 0, TimeSpan.Zero), 40, 650m),
            CreateOrder(14, Clients[1], Restaurants[6], new DateTimeOffset(2026, 3, 4, 15, 0, 0, TimeSpan.Zero), 20, 500m),
            CreateOrder(15, Clients[2], Restaurants[7], new DateTimeOffset(2026, 3, 5, 16, 0, 0, TimeSpan.Zero), 25, 450m),
            CreateOrder(16, Clients[3], Restaurants[8], new DateTimeOffset(2026, 3, 6, 9, 0, 0, TimeSpan.Zero), 15, 550m),
            CreateOrder(17, Clients[4], Restaurants[9], new DateTimeOffset(2026, 3, 7, 10, 0, 0, TimeSpan.Zero), 30, 600m),
            CreateOrder(18, Clients[5], Restaurants[0], new DateTimeOffset(2026, 4, 1, 18, 0, 0, TimeSpan.Zero), 35, 900m),
            CreateOrder(19, Clients[6], Restaurants[0], new DateTimeOffset(2026, 4, 2, 18, 30, 0, TimeSpan.Zero), 50, 1000m),
            CreateOrder(20, Clients[7], Restaurants[1], new DateTimeOffset(2026, 4, 3, 19, 0, 0, TimeSpan.Zero), 35, 750m)
        ];

        OrderItems =
        [
            CreateOrderItem(1, Orders[0], Dishes[0], 2),
            CreateOrderItem(2, Orders[1], Dishes[1], 2),
            CreateOrderItem(3, Orders[2], Dishes[0], 3),
            CreateOrderItem(4, Orders[3], Dishes[1], 3),
            CreateOrderItem(5, Orders[4], Dishes[2], 1),
            CreateOrderItem(6, Orders[5], Dishes[3], 1),
            CreateOrderItem(7, Orders[6], Dishes[2], 1),
            CreateOrderItem(8, Orders[7], Dishes[4], 2),
            CreateOrderItem(9, Orders[8], Dishes[5], 1),
            CreateOrderItem(10, Orders[9], Dishes[4], 2),
            CreateOrderItem(11, Orders[10], Dishes[6], 1),
            CreateOrderItem(12, Orders[11], Dishes[7], 1),
            CreateOrderItem(13, Orders[12], Dishes[8], 1),
            CreateOrderItem(14, Orders[13], Dishes[10], 1),
            CreateOrderItem(15, Orders[14], Dishes[11], 1),
            CreateOrderItem(16, Orders[15], Dishes[12], 1),
            CreateOrderItem(17, Orders[16], Dishes[4], 1),
            CreateOrderItem(18, Orders[17], Dishes[6], 1),
            CreateOrderItem(19, Orders[18], Dishes[7], 1),
            CreateOrderItem(20, Orders[19], Dishes[8], 1),
	    CreateOrderItem(21, Orders[19], Dishes[9], 1)
        ];
    }

    private static OrderItem CreateOrderItem(int id, Order order, Dish dish, int quantity)
    {
        var item = new OrderItem
        {
            Id = id,
            OrderId = order.Id,
            Order = order,
            DishId = dish.Id,
            Dish = dish,
            Quantity = quantity,
            UnitPrice = dish.Price
        };

        order.Items.Add(item);
        return item;
    }

    private static Order CreateOrder(
        int id,
        Client client,
        Restaurant restaurant,
        DateTimeOffset createdAt,
        int deliveryMinutes,
        decimal totalAmount)
    {
        return new Order
        {
            Id = id,
            ClientId = client.Id,
            Client = client,
            RestaurantId = restaurant.Id,
            Restaurant = restaurant,
            CreatedAt = createdAt,
            DeliveredAt = createdAt.AddMinutes(deliveryMinutes),
            TotalAmount = totalAmount
        };
    }
}
