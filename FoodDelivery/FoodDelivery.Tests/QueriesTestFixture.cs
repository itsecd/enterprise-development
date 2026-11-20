using FoodDelivery.Domain.Entities;
using FoodDelivery.Domain.Enums;

namespace FoodDelivery.Tests;

/// <summary>
/// Тестовый набор данных для проверки запросов службы доставки еды
/// </summary>
public class QueriesTestFixture
{
    /// <summary>
    /// Список клиентов
    /// </summary>
    public readonly List<Customer> Customers;

    /// <summary>
    /// Список ресторанов
    /// </summary>
    public readonly List<Restaurant> Restaurants;

    /// <summary>
    /// Список категорий блюд
    /// </summary>
    public readonly List<DishCategory> DishCategories;

    /// <summary>
    /// Список блюд
    /// </summary>
    public readonly List<Dish> Dishes;

    /// <summary>
    /// Список курьеров
    /// </summary>
    public readonly List<Courier> Couriers;

    /// <summary>
    /// Список заказов
    /// </summary>
    public readonly List<Order> Orders;

    /// <summary>
    /// Инициализация тестовых данных
    /// </summary>
    public QueriesTestFixture()
    {
        Customers = GetCustomers();
        Restaurants = GetRestaurants();
        DishCategories = GetDishCategories();
        Dishes = GetDishes(DishCategories);
        Couriers = GetCouriers();
        Orders = GetOrders(Customers, Restaurants, Couriers);
    }

    /// <summary>
    /// Создание списка клиентов
    /// </summary>
    /// <returns>Список клиентов</returns>
    private static List<Customer> GetCustomers() =>
    [
        new Customer { Id = 1, Name = "Вадим Владимиров", Phone = "+79990000000", DeliveryAddress = "ул. Ленина, д. 10" },
        new Customer { Id = 2, Name = "Иван Лапшин", Phone = "+79991112233", DeliveryAddress = "ул. Пушкина, д. 4" },
        new Customer { Id = 3, Name = "Максим Громов", Phone = "+79992223344", DeliveryAddress = "ул. Гагарина, д. 15" },
        new Customer { Id = 4, Name = "Анна Иванова", Phone = "+79993334455", DeliveryAddress = "ул. Мира, д. 8" },
        new Customer { Id = 5, Name = "Иван Петров", Phone = "+79994445566", DeliveryAddress = "ул. Советская, д. 22" },
        new Customer { Id = 6, Name = "Елена Сидорова", Phone = "+79995556677", DeliveryAddress = "ул. Кирова, д. 3" },
        new Customer { Id = 7, Name = "Дмитрий Волков", Phone = "+79996667788", DeliveryAddress = "ул. Лесная, д. 11" },
        new Customer { Id = 8, Name = "Ольга Морозова", Phone = "+79997778899", DeliveryAddress = "ул. Новая, д. 7" },
        new Customer { Id = 9, Name = "Сергей Васильев", Phone = "+79998889900", DeliveryAddress = "ул. Парковая, д. 5" },
        new Customer { Id = 10, Name = "Мария Смирнова", Phone = "+79999990011", DeliveryAddress = "ул. Полевая, д. 20" }
    ];

    /// <summary>
    /// Создаёт список ресторанов
    /// </summary>
    /// <returns>Список ресторанов</returns>
    private static List<Restaurant> GetRestaurants() =>
    [
        new Restaurant { Id = 1, Name = "Вкусно — и точка", Address = "ул. Полевая, д. 15", Rating = 4.7, WorkingHours = "08:00 - 23:00", IsActive = true },
        new Restaurant { Id = 2, Name = "Крошка картошка", Address = "ул. Молодогвардейская, д. 135", Rating = 4.5, WorkingHours = "09:00 - 22:00", IsActive = true },
        new Restaurant { Id = 3, Name = "Dodo Pizza", Address = "ул. Ново-Садовая, д. 8А", Rating = 4.8, WorkingHours = "10:00 - 00:00", IsActive = true },
        new Restaurant { Id = 4, Name = "Бургер Кинг", Address = "ул. Дыбенко, д. 30", Rating = 4.4, WorkingHours = "10:00 - 23:00", IsActive = true },
        new Restaurant { Id = 5, Name = "Rostic's", Address = "ул. Авроры, д. 209", Rating = 4.2, WorkingHours = "10:00 - 22:00", IsActive = false },
        new Restaurant { Id = 6, Name = "Перчини", Address = "Московское ш., д. 252", Rating = 4.9, WorkingHours = "12:00 - 00:00", IsActive = true },
        new Restaurant { Id = 7, Name = "Суши Wok", Address = "ул. 6-я просека, д. 149", Rating = 4.3, WorkingHours = "10:00 - 23:00", IsActive = true },
        new Restaurant { Id = 8, Name = "Папа Джонс", Address = "ул. Георгия Димитрова, д. 110", Rating = 4.6, WorkingHours = "10:00 - 23:00", IsActive = true },
        new Restaurant { Id = 9, Name = "Старик Хинкалыч", Address = "Московское ш., д. 205", Rating = 4.8, WorkingHours = "11:00 - 23:00", IsActive = true },
        new Restaurant { Id = 10, Name = "Тануки", Address = "ул. Дачная, д. 2", Rating = 4.7, WorkingHours = "11:00 - 01:00", IsActive = true }
    ];

    /// <summary>
    /// Создаёт справочник категорий блюд
    /// </summary>
    /// <returns>Список категорий блюд</returns>
    private static List<DishCategory> GetDishCategories() =>
    [
        new DishCategory { Id = 1, Name = "Фастфуд" },
        new DishCategory { Id = 2, Name = "Основные блюда" },
        new DishCategory { Id = 3, Name = "Напитки" },
        new DishCategory { Id = 4, Name = "Пицца" },
        new DishCategory { Id = 5, Name = "Суши и роллы" },
        new DishCategory { Id = 6, Name = "Супы" },
        new DishCategory { Id = 7, Name = "Салаты" },
        new DishCategory { Id = 8, Name = "Десерты" },
        new DishCategory { Id = 9, Name = "Завтраки" },
        new DishCategory { Id = 10, Name = "Закуски" }
    ];

    /// <summary>
    /// Создаёт список блюд
    /// </summary>
    /// <param name="categories">Категории блюд</param>
    /// <returns>Список блюд</returns>
    private static List<Dish> GetDishes(List<DishCategory> categories)
    {
        var fastFood = categories[0];
        var mainCourse = categories[1];
        var drink = categories[2];
        var pizza = categories[3];
        var sushiAndRolls = categories[4];

        return
        [
            new Dish { Id = 1, RestaurantId = 1, Name = "Гранд", Weight = 250, Price = 190, CategoryId = fastFood.Id, Category = fastFood },
            new Dish { Id = 2, RestaurantId = 1, Name = "Картофель фри", Weight = 100, Price = 100, CategoryId = fastFood.Id, Category = fastFood },
            new Dish { Id = 3, RestaurantId = 2, Name = "Печень жареная с луком", Weight = 300, Price = 350, CategoryId = mainCourse.Id, Category = mainCourse },
            new Dish { Id = 4, RestaurantId = 3, Name = "Маргарита", Weight = 450, Price = 490, CategoryId = pizza.Id, Category = pizza },
            new Dish { Id = 5, RestaurantId = 4, Name = "Воппер", Weight = 280, Price = 250, CategoryId = fastFood.Id, Category = fastFood },
            new Dish { Id = 6, RestaurantId = 6, Name = "Блин с грибами", Weight = 200, Price = 220, CategoryId = mainCourse.Id, Category = mainCourse },
            new Dish { Id = 7, RestaurantId = 7, Name = "Филадельфия", Weight = 270, Price = 550, CategoryId = sushiAndRolls.Id, Category = sushiAndRolls },
            new Dish { Id = 8, RestaurantId = 8, Name = "Пепперони", Weight = 500, Price = 600, CategoryId = pizza.Id, Category = pizza },
            new Dish { Id = 9, RestaurantId = 9, Name = "Капучино", Weight = 300, Price = 180, CategoryId = drink.Id, Category = drink },
            new Dish { Id = 10, RestaurantId = 10, Name = "Удон с курицей", Weight = 350, Price = 420, CategoryId = mainCourse.Id, Category = mainCourse }
        ];
    }

    /// <summary>
    /// Создаёт список курьеров
    /// </summary>
    /// <returns>Список курьеров</returns>
    private static List<Courier> GetCouriers() =>
    [
        new Courier { Id = 1, FullName = "Максим Краснов", Phone = "+79991000001", Vehicle = "Lada Granta", IsActive = true },
        new Courier { Id = 2, FullName = "Алексей Смирнов", Phone = "+79991000002", Vehicle = "Велосипед", IsActive = true },
        new Courier { Id = 3, FullName = "Евгений Попов", Phone = "+79991000003", Vehicle = "Электросамокат", IsActive = true },
        new Courier { Id = 4, FullName = "Владимир Лебедев", Phone = "+79991000004", Vehicle = "Kia Rio", IsActive = true },
        new Courier { Id = 5, FullName = "Артем Козлов", Phone = "+79991000005", Vehicle = "Пешком", IsActive = true },
        new Courier { Id = 6, FullName = "Роман Новиков", Phone = "+79991000006", Vehicle = "Hyundai Solaris", IsActive = true },
        new Courier { Id = 7, FullName = "Денис Морозов", Phone = "+79991000007", Vehicle = "Велосипед", IsActive = true },
        new Courier { Id = 8, FullName = "Павел Соколов", Phone = "+79991000008", Vehicle = "Электросамокат", IsActive = true },
        new Courier { Id = 9, FullName = "Игорь Медведев", Phone = "+79991000009", Vehicle = "Пешком", IsActive = true },
        new Courier { Id = 10, FullName = "Александр Григорьев", Phone = "+79991000010", Vehicle = "Велосипед", IsActive = true }
    ];

    /// <summary>
    /// Создаёт список заказов
    /// </summary>
    /// <param name="customers">Список клиентов</param>
    /// <param name="restaurants">Список ресторанов</param>
    /// <param name="couriers">Список курьеров</param>
    /// <returns>Список заказов</returns>
    private static List<Order> GetOrders(List<Customer> customers, List<Restaurant> restaurants, List<Courier> couriers) =>
    [
        new Order { Id = 1, CustomerId = 2, RestaurantId = 1, CourierId = 1, Customer = customers[1], Restaurant = restaurants[0], Courier = couriers[0], DeliveryAddress = "ул. Пушкина, д. 4", OrderTime = new DateTime(2026, 5, 10, 12, 0, 0), DeliveryTime = new DateTime(2026, 5, 10, 12, 40, 0), TotalAmount = 1500m, Status = OrderStatus.Completed, PaymentMethod = PaymentMethod.Online },
        new Order { Id = 2, CustomerId = 3, RestaurantId = 1, CourierId = 1, Customer = customers[2], Restaurant = restaurants[0], Courier = couriers[0], DeliveryAddress = "ул. Гагарина, д. 15", OrderTime = new DateTime(2026, 5, 10, 13, 0, 0), DeliveryTime = new DateTime(2026, 5, 10, 13, 30, 0), TotalAmount = 800m, Status = OrderStatus.Delivering, PaymentMethod = PaymentMethod.CardOnDelivery },
        new Order { Id = 3, CustomerId = 4, RestaurantId = 1, CourierId = 1, Customer = customers[3], Restaurant = restaurants[0], Courier = couriers[0], DeliveryAddress = "ул. Мира, д. 8", OrderTime = new DateTime(2026, 5, 11, 14, 0, 0), DeliveryTime = new DateTime(2026, 5, 11, 14, 45, 0), TotalAmount = 2500m, Status = OrderStatus.Completed, PaymentMethod = PaymentMethod.Online },
        new Order { Id = 4, CustomerId = 5, RestaurantId = 2, CourierId = 2, Customer = customers[4], Restaurant = restaurants[1], Courier = couriers[1], DeliveryAddress = "ул. Советская, д. 22", OrderTime = new DateTime(2026, 5, 11, 15, 0, 0), DeliveryTime = new DateTime(2026, 5, 11, 15, 35, 0), TotalAmount = 3000m, Status = OrderStatus.Delivering, PaymentMethod = PaymentMethod.Cash },
        new Order { Id = 5, CustomerId = 5, RestaurantId = 3, CourierId = 3, Customer = customers[4], Restaurant = restaurants[2], Courier = couriers[2], DeliveryAddress = "ул. Советская, д. 22", OrderTime = new DateTime(2026, 5, 12, 10, 0, 0), DeliveryTime = new DateTime(2026, 5, 12, 10, 30, 0), TotalAmount = 1200m, Status = OrderStatus.Completed, PaymentMethod = PaymentMethod.Online },
        new Order { Id = 6, CustomerId = 6, RestaurantId = 4, CourierId = 4, Customer = customers[5], Restaurant = restaurants[3], Courier = couriers[3], DeliveryAddress = "ул. Кирова, д. 3", OrderTime = new DateTime(2026, 5, 12, 11, 0, 0), DeliveryTime = new DateTime(2026, 5, 12, 11, 50, 0), TotalAmount = 500m, Status = OrderStatus.Cancelled, PaymentMethod = PaymentMethod.CardOnDelivery },
        new Order { Id = 7, CustomerId = 7, RestaurantId = 2, CourierId = 2, Customer = customers[6], Restaurant = restaurants[1], Courier = couriers[1], DeliveryAddress = "ул. Лесная, д. 11", OrderTime = new DateTime(2026, 5, 13, 16, 0, 0), DeliveryTime = new DateTime(2026, 5, 13, 16, 30, 0), TotalAmount = 1800m, Status = OrderStatus.Delivering, PaymentMethod = PaymentMethod.Online },
        new Order { Id = 8, CustomerId = 8, RestaurantId = 6, CourierId = 5, Customer = customers[7], Restaurant = restaurants[5], Courier = couriers[4], DeliveryAddress = "ул. Новая, д. 7", OrderTime = new DateTime(2026, 5, 14, 17, 0, 0), DeliveryTime = new DateTime(2026, 5, 14, 17, 40, 0), TotalAmount = 900m, Status = OrderStatus.Completed, PaymentMethod = PaymentMethod.Cash },
        new Order { Id = 9, CustomerId = 9, RestaurantId = 2, CourierId = 2, Customer = customers[8], Restaurant = restaurants[1], Courier = couriers[1], DeliveryAddress = "ул. Парковая, д. 5", OrderTime = new DateTime(2026, 5, 15, 18, 0, 0), DeliveryTime = new DateTime(2026, 5, 15, 18, 25, 0), TotalAmount = 2100m, Status = OrderStatus.Delivering, PaymentMethod = PaymentMethod.Online },
        new Order { Id = 10, CustomerId = 10, RestaurantId = 10, CourierId = 6, Customer = customers[9], Restaurant = restaurants[9], Courier = couriers[5], DeliveryAddress = "ул. Полевая, д. 20", OrderTime = new DateTime(2026, 5, 16, 19, 0, 0), DeliveryTime = new DateTime(2026, 5, 16, 19, 45, 0), TotalAmount = 3500m, Status = OrderStatus.Completed, PaymentMethod = PaymentMethod.Online }
    ];
}