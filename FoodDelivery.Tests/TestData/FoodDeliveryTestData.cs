using FoodDelivery.Domain;

namespace FoodDelivery.Tests.TestData;

public class FoodDeliveryTestData
{
    public List<Category> Categories { get; } = [];

    public List<Dish> Dishes { get; } = [];

    public List<Restaurant> Restaurants { get; } = [];

    public List<Customer> Customers { get; } = [];

    public List<Order> Orders { get; } = [];

    public List<OrderItem> OrderItems { get; } = [];

    public FoodDeliveryTestData()
    {
        CreateCategories();
        CreateDishes();
        CreateRestaurants();
        CreateCustomers();
        CreateOrders();
    }

    private void CreateCategories()
    {
          Categories.AddRange(
    [
        new Category { Id = 1, Name = "Pizza" },
        new Category { Id = 2, Name = "Burgers" },
        new Category { Id = 3, Name = "Sushi" },
        new Category { Id = 4, Name = "Soups" },
        new Category { Id = 5, Name = "Salads" },
        new Category { Id = 6, Name = "Pasta" },
        new Category { Id = 7, Name = "Desserts" },
        new Category { Id = 8, Name = "Drinks" },
        new Category { Id = 9, Name = "Breakfast" },
        new Category { Id = 10, Name = "Snacks" }
    ]);
    }

    private void CreateDishes()
{
    Dishes.AddRange(
    [
        new Dish
        {
            Id = 1,
            Name = "Margherita",
            Weight = 450,
            Price = 650m,
            CategoryId = 1,
            Category = Categories[0]
        },
        new Dish
        {
            Id = 2,
            Name = "Pepperoni",
            Weight = 500,
            Price = 750m,
            CategoryId = 1,
            Category = Categories[0]
        },
        new Dish
        {
            Id = 3,
            Name = "Classic Burger",
            Weight = 350,
            Price = 520m,
            CategoryId = 2,
            Category = Categories[1]
        },
        new Dish
        {
            Id = 4,
            Name = "Cheeseburger",
            Weight = 380,
            Price = 580m,
            CategoryId = 2,
            Category = Categories[1]
        },
        new Dish
        {
            Id = 5,
            Name = "Philadelphia Roll",
            Weight = 300,
            Price = 690m,
            CategoryId = 3,
            Category = Categories[2]
        },
        new Dish
        {
            Id = 6,
            Name = "Tom Yum",
            Weight = 400,
            Price = 620m,
            CategoryId = 4,
            Category = Categories[3]
        },
        new Dish
        {
            Id = 7,
            Name = "Caesar Salad",
            Weight = 280,
            Price = 450m,
            CategoryId = 5,
            Category = Categories[4]
        },
        new Dish
        {
            Id = 8,
            Name = "Carbonara",
            Weight = 350,
            Price = 590m,
            CategoryId = 6,
            Category = Categories[5]
        },
        new Dish
        {
            Id = 9,
            Name = "Cheesecake",
            Weight = 180,
            Price = 390m,
            CategoryId = 7,
            Category = Categories[6]
        },
        new Dish
        {
            Id = 10,
            Name = "Cola",
            Weight = 500,
            Price = 150m,
            CategoryId = 8,
            Category = Categories[7]
        },
        new Dish
        {
            Id = 11,
            Name = "Pancakes",
            Weight = 250,
            Price = 350m,
            CategoryId = 9,
            Category = Categories[8]
        },
        new Dish
        {
            Id = 12,
            Name = "Mozzarella Sticks",
            Weight = 220,
            Price = 420m,
            CategoryId = 10,
            Category = Categories[9]
        }
    ]);
}

    private void CreateRestaurants()
{
    Restaurants.AddRange(
    [
        new Restaurant
        {
            Id = 1,
            Name = "Pizza House",
            Address = "Lenina Street, 10",
            Rating = 4.8m,
            OpeningTime = new TimeOnly(10, 0),
            ClosingTime = new TimeOnly(23, 0)
        },
        new Restaurant
        {
            Id = 2,
            Name = "Burger Point",
            Address = "Moskovskoe Highway, 25",
            Rating = 4.6m,
            OpeningTime = new TimeOnly(9, 0),
            ClosingTime = new TimeOnly(23, 0)
        },
        new Restaurant
        {
            Id = 3,
            Name = "Sushi Time",
            Address = "Gagarina Street, 15",
            Rating = 4.7m,
            OpeningTime = new TimeOnly(11, 0),
            ClosingTime = new TimeOnly(23, 30)
        },
        new Restaurant
        {
            Id = 4,
            Name = "Pasta Place",
            Address = "Novo-Sadovaya Street, 40",
            Rating = 4.5m,
            OpeningTime = new TimeOnly(10, 0),
            ClosingTime = new TimeOnly(22, 0)
        },
        new Restaurant
        {
            Id = 5,
            Name = "Food Corner",
            Address = "Revolutsii Street, 8",
            Rating = 4.3m,
            OpeningTime = new TimeOnly(8, 0),
            ClosingTime = new TimeOnly(22, 0)
        },
        new Restaurant
        {
            Id = 6,
            Name = "Family Kitchen",
            Address = "Michurina Street, 30",
            Rating = 4.4m,
            OpeningTime = new TimeOnly(9, 0),
            ClosingTime = new TimeOnly(22, 0)
        },
        new Restaurant
        {
            Id = 7,
            Name = "Daily Meal",
            Address = "Sovetskoy Armii Street, 12",
            Rating = 4.2m,
            OpeningTime = new TimeOnly(8, 0),
            ClosingTime = new TimeOnly(21, 0)
        },
        new Restaurant
        {
            Id = 8,
            Name = "Street Food",
            Address = "Aerodromnaya Street, 5",
            Rating = 4.1m,
            OpeningTime = new TimeOnly(10, 0),
            ClosingTime = new TimeOnly(23, 0)
        },
        new Restaurant
        {
            Id = 9,
            Name = "Tasty Place",
            Address = "Osipenko Street, 18",
            Rating = 4.6m,
            OpeningTime = new TimeOnly(10, 0),
            ClosingTime = new TimeOnly(22, 30)
        },
        new Restaurant
        {
            Id = 10,
            Name = "Home Food",
            Address = "Polevaya Street, 20",
            Rating = 4.0m,
            OpeningTime = new TimeOnly(9, 0),
            ClosingTime = new TimeOnly(21, 0)
        }
    ]);
}

    private void CreateCustomers()
{
    Customers.AddRange(
    [
        new Customer
        {
            Id = 1,
            LastName = "Ivanov",
            FirstName = "Ivan",
            Patronymic = "Petrovich",
            Phone = "+79000000001",
            DeliveryAddress = "Lenina Street, 1"
        },
        new Customer
        {
            Id = 2,
            LastName = "Petrov",
            FirstName = "Petr",
            Patronymic = "Ivanovich",
            Phone = "+79000000002",
            DeliveryAddress = "Gagarina Street, 2"
        },
        new Customer
        {
            Id = 3,
            LastName = "Sidorov",
            FirstName = "Sergey",
            Patronymic = "Alexandrovich",
            Phone = "+79000000003",
            DeliveryAddress = "Michurina Street, 3"
        },
        new Customer
        {
            Id = 4,
            LastName = "Smirnov",
            FirstName = "Alexey",
            Patronymic = "Dmitrievich",
            Phone = "+79000000004",
            DeliveryAddress = "Polevaya Street, 4"
        },
        new Customer
        {
            Id = 5,
            LastName = "Kuznetsov",
            FirstName = "Dmitry",
            Patronymic = "Sergeevich",
            Phone = "+79000000005",
            DeliveryAddress = "Osipenko Street, 5"
        },
        new Customer
        {
            Id = 6,
            LastName = "Popov",
            FirstName = "Nikolay",
            Patronymic = "Andreevich",
            Phone = "+79000000006",
            DeliveryAddress = "Revolutsii Street, 6"
        },
        new Customer
        {
            Id = 7,
            LastName = "Volkov",
            FirstName = "Andrey",
            Patronymic = "Vladimirovich",
            Phone = "+79000000007",
            DeliveryAddress = "Sovetskoy Armii Street, 7"
        },
        new Customer
        {
            Id = 8,
            LastName = "Orlov",
            FirstName = "Mikhail",
            Patronymic = "Olegovich",
            Phone = "+79000000008",
            DeliveryAddress = "Aerodromnaya Street, 8"
        },
        new Customer
        {
            Id = 9,
            LastName = "Fedorov",
            FirstName = "Roman",
            Patronymic = "Igorevich",
            Phone = "+79000000009",
            DeliveryAddress = "Novo-Sadovaya Street, 9"
        },
        new Customer
        {
            Id = 10,
            LastName = "Morozov",
            FirstName = "Kirill",
            Patronymic = "Alexeevich",
            Phone = "+79000000010",
            DeliveryAddress = "Moskovskoe Highway, 10"
        }
    ]);
}

    private void CreateOrders()
{
    Orders.AddRange(
    [
        new Order
        {
            Id = 1,
            CustomerId = 1,
            RestaurantId = 1,
            OrderTime = new DateTime(2026, 9, 1, 12, 0, 0),
            DeliveryTime = new DateTime(2026, 9, 1, 12, 35, 0),
            TotalAmount = 1450m,
            Customer = Customers[0],
            Restaurant = Restaurants[0]
        },
        new Order
        {
            Id = 2,
            CustomerId = 2,
            RestaurantId = 1,
            OrderTime = new DateTime(2026, 9, 2, 13, 0, 0),
            DeliveryTime = new DateTime(2026, 9, 2, 13, 50, 0),
            TotalAmount = 750m,
            Customer = Customers[1],
            Restaurant = Restaurants[0]
        },
        new Order
        {
            Id = 3,
            CustomerId = 1,
            RestaurantId = 1,
            OrderTime = new DateTime(2026, 9, 3, 18, 0, 0),
            DeliveryTime = new DateTime(2026, 9, 3, 18, 30, 0),
            TotalAmount = 1550m,
            Customer = Customers[0],
            Restaurant = Restaurants[0]
        },
        new Order
        {
            Id = 4,
            CustomerId = 3,
            RestaurantId = 1,
            OrderTime = new DateTime(2026, 9, 5, 14, 0, 0),
            DeliveryTime = new DateTime(2026, 9, 5, 14, 40, 0),
            TotalAmount = 970m,
            Customer = Customers[2],
            Restaurant = Restaurants[0]
        },
        new Order
        {
            Id = 5,
            CustomerId = 1,
            RestaurantId = 1,
            OrderTime = new DateTime(2026, 9, 7, 19, 0, 0),
            DeliveryTime = new DateTime(2026, 9, 7, 19, 55, 0),
            TotalAmount = 1840m,
            Customer = Customers[0],
            Restaurant = Restaurants[0]
        },
        new Order
        {
            Id = 6,
            CustomerId = 4,
            RestaurantId = 2,
            OrderTime = new DateTime(2026, 9, 3, 11, 30, 0),
            DeliveryTime = new DateTime(2026, 9, 3, 12, 15, 0),
            TotalAmount = 1100m,
            Customer = Customers[3],
            Restaurant = Restaurants[1]
        },
        new Order
        {
            Id = 7,
            CustomerId = 5,
            RestaurantId = 2,
            OrderTime = new DateTime(2026, 9, 4, 17, 0, 0),
            DeliveryTime = new DateTime(2026, 9, 4, 17, 35, 0),
            TotalAmount = 1340m,
            Customer = Customers[4],
            Restaurant = Restaurants[1]
        },
        new Order
        {
            Id = 8,
            CustomerId = 1,
            RestaurantId = 2,
            OrderTime = new DateTime(2026, 9, 6, 16, 0, 0),
            DeliveryTime = new DateTime(2026, 9, 6, 16, 45, 0),
            TotalAmount = 1260m,
            Customer = Customers[0],
            Restaurant = Restaurants[1]
        },
        new Order
        {
            Id = 9,
            CustomerId = 6,
            RestaurantId = 2,
            OrderTime = new DateTime(2026, 9, 8, 15, 0, 0),
            DeliveryTime = new DateTime(2026, 9, 8, 15, 50, 0),
            TotalAmount = 940m,
            Customer = Customers[5],
            Restaurant = Restaurants[1]
        },
        new Order
        {
            Id = 10,
            CustomerId = 7,
            RestaurantId = 3,
            OrderTime = new DateTime(2026, 9, 4, 12, 0, 0),
            DeliveryTime = new DateTime(2026, 9, 4, 12, 30, 0),
            TotalAmount = 1380m,
            Customer = Customers[6],
            Restaurant = Restaurants[2]
        },
        new Order
        {
            Id = 11,
            CustomerId = 8,
            RestaurantId = 3,
            OrderTime = new DateTime(2026, 9, 5, 18, 0, 0),
            DeliveryTime = new DateTime(2026, 9, 5, 18, 40, 0),
            TotalAmount = 1080m,
            Customer = Customers[7],
            Restaurant = Restaurants[2]
        },
        new Order
        {
            Id = 12,
            CustomerId = 9,
            RestaurantId = 3,
            OrderTime = new DateTime(2026, 9, 9, 13, 0, 0),
            DeliveryTime = new DateTime(2026, 9, 9, 13, 55, 0),
            TotalAmount = 970m,
            Customer = Customers[8],
            Restaurant = Restaurants[2]
        },
        new Order
        {
            Id = 13,
            CustomerId = 10,
            RestaurantId = 4,
            OrderTime = new DateTime(2026, 9, 6, 14, 0, 0),
            DeliveryTime = new DateTime(2026, 9, 6, 14, 45, 0),
            TotalAmount = 1010m,
            Customer = Customers[9],
            Restaurant = Restaurants[3]
        },
        new Order
        {
            Id = 14,
            CustomerId = 5,
            RestaurantId = 4,
            OrderTime = new DateTime(2026, 9, 8, 19, 0, 0),
            DeliveryTime = new DateTime(2026, 9, 8, 19, 40, 0),
            TotalAmount = 1180m,
            Customer = Customers[4],
            Restaurant = Restaurants[3]
        },
        new Order
        {
            Id = 15,
            CustomerId = 2,
            RestaurantId = 5,
            OrderTime = new DateTime(2026, 9, 10, 12, 0, 0),
            DeliveryTime = new DateTime(2026, 9, 10, 12, 50, 0),
            TotalAmount = 870m,
            Customer = Customers[1],
            Restaurant = Restaurants[4]
        }
    ]);

    CreateOrderItems();
}
    private void CreateOrderItems()
{
    OrderItems.AddRange(
    [
        new OrderItem
        {
            Id = 1,
            OrderId = 1,
            DishId = 1,
            Quantity = 1,
            UnitPrice = 650m,
            Order = Orders[0],
            Dish = Dishes[0]
        },
        new OrderItem
        {
            Id = 2,
            OrderId = 1,
            DishId = 2,
            Quantity = 1,
            UnitPrice = 750m,
            Order = Orders[0],
            Dish = Dishes[1]
        },
        new OrderItem
        {
            Id = 3,
            OrderId = 1,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[0],
            Dish = Dishes[9]
        },

        new OrderItem
        {
            Id = 4,
            OrderId = 2,
            DishId = 2,
            Quantity = 1,
            UnitPrice = 750m,
            Order = Orders[1],
            Dish = Dishes[1]
        },

        new OrderItem
        {
            Id = 5,
            OrderId = 3,
            DishId = 1,
            Quantity = 1,
            UnitPrice = 650m,
            Order = Orders[2],
            Dish = Dishes[0]
        },
        new OrderItem
        {
            Id = 6,
            OrderId = 3,
            DishId = 3,
            Quantity = 1,
            UnitPrice = 520m,
            Order = Orders[2],
            Dish = Dishes[2]
        },
        new OrderItem
        {
            Id = 7,
            OrderId = 3,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[2],
            Dish = Dishes[9]
        },
        new OrderItem
        {
            Id = 8,
            OrderId = 3,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[2],
            Dish = Dishes[9]
        },
        new OrderItem
        {
            Id = 9,
            OrderId = 3,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[2],
            Dish = Dishes[9]
        },
        new OrderItem
        {
            Id = 10,
            OrderId = 3,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[2],
            Dish = Dishes[9]
        },
        new OrderItem
        {
            Id = 11,
            OrderId = 3,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[2],
            Dish = Dishes[9]
        },

        new OrderItem
        {
            Id = 12,
            OrderId = 4,
            DishId = 3,
            Quantity = 1,
            UnitPrice = 520m,
            Order = Orders[3],
            Dish = Dishes[2]
        },
        new OrderItem
        {
            Id = 13,
            OrderId = 4,
            DishId = 7,
            Quantity = 1,
            UnitPrice = 450m,
            Order = Orders[3],
            Dish = Dishes[6]
        },

        new OrderItem
        {
            Id = 14,
            OrderId = 5,
            DishId = 1,
            Quantity = 2,
            UnitPrice = 650m,
            Order = Orders[4],
            Dish = Dishes[0]
        },
        new OrderItem
        {
            Id = 15,
            OrderId = 5,
            DishId = 7,
            Quantity = 1,
            UnitPrice = 450m,
            Order = Orders[4],
            Dish = Dishes[6]
        },
        new OrderItem
        {
            Id = 16,
            OrderId = 5,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[4],
            Dish = Dishes[9]
        },

        new OrderItem
        {
            Id = 17,
            OrderId = 6,
            DishId = 3,
            Quantity = 1,
            UnitPrice = 520m,
            Order = Orders[5],
            Dish = Dishes[2]
        },
        new OrderItem
        {
            Id = 18,
            OrderId = 6,
            DishId = 4,
            Quantity = 1,
            UnitPrice = 580m,
            Order = Orders[5],
            Dish = Dishes[3]
        },

        new OrderItem
        {
            Id = 19,
            OrderId = 7,
            DishId = 4,
            Quantity = 1,
            UnitPrice = 580m,
            Order = Orders[6],
            Dish = Dishes[3]
        },
        new OrderItem
        {
            Id = 20,
            OrderId = 7,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[6],
            Dish = Dishes[9]
        },
        new OrderItem
        {
            Id = 21,
            OrderId = 7,
            DishId = 9,
            Quantity = 1,
            UnitPrice = 390m,
            Order = Orders[6],
            Dish = Dishes[8]
        },
        new OrderItem
        {
            Id = 22,
            OrderId = 7,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[6],
            Dish = Dishes[9]
        },

        new OrderItem
        {
            Id = 23,
            OrderId = 8,
            DishId = 3,
            Quantity = 1,
            UnitPrice = 520m,
            Order = Orders[7],
            Dish = Dishes[2]
        },
        new OrderItem
        {
            Id = 24,
            OrderId = 8,
            DishId = 4,
            Quantity = 1,
            UnitPrice = 580m,
            Order = Orders[7],
            Dish = Dishes[3]
        },
        new OrderItem
        {
            Id = 25,
            OrderId = 8,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[7],
            Dish = Dishes[9]
        },
        new OrderItem
        {
            Id = 26,
            OrderId = 8,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[7],
            Dish = Dishes[9]
        },
        new OrderItem
        {
            Id = 27,
            OrderId = 8,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[7],
            Dish = Dishes[9]
        },
        new OrderItem
        {
            Id = 28,
            OrderId = 8,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[7],
            Dish = Dishes[9]
        },
        new OrderItem
        {
            Id = 29,
            OrderId = 8,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[7],
            Dish = Dishes[9]
        },

        new OrderItem
        {
            Id = 30,
            OrderId = 9,
            DishId = 4,
            Quantity = 1,
            UnitPrice = 580m,
            Order = Orders[8],
            Dish = Dishes[3]
        },
        new OrderItem
        {
            Id = 31,
            OrderId = 9,
            DishId = 7,
            Quantity = 1,
            UnitPrice = 450m,
            Order = Orders[8],
            Dish = Dishes[6]
        },
        new OrderItem
        {
            Id = 32,
            OrderId = 9,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[8],
            Dish = Dishes[9]
        },

        new OrderItem
        {
            Id = 33,
            OrderId = 10,
            DishId = 5,
            Quantity = 2,
            UnitPrice = 690m,
            Order = Orders[9],
            Dish = Dishes[4]
        },

        new OrderItem
        {
            Id = 34,
            OrderId = 11,
            DishId = 5,
            Quantity = 1,
            UnitPrice = 690m,
            Order = Orders[10],
            Dish = Dishes[4]
        },
        new OrderItem
        {
            Id = 35,
            OrderId = 11,
            DishId = 9,
            Quantity = 1,
            UnitPrice = 390m,
            Order = Orders[10],
            Dish = Dishes[8]
        },

        new OrderItem
        {
            Id = 36,
            OrderId = 12,
            DishId = 5,
            Quantity = 1,
            UnitPrice = 690m,
            Order = Orders[11],
            Dish = Dishes[4]
        },
        new OrderItem
        {
            Id = 37,
            OrderId = 12,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[11],
            Dish = Dishes[9]
        },
        new OrderItem
        {
            Id = 38,
            OrderId = 12,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[11],
            Dish = Dishes[9]
        },

        new OrderItem
        {
            Id = 39,
            OrderId = 13,
            DishId = 8,
            Quantity = 1,
            UnitPrice = 590m,
            Order = Orders[12],
            Dish = Dishes[7]
        },
        new OrderItem
        {
            Id = 40,
            OrderId = 13,
            DishId = 9,
            Quantity = 1,
            UnitPrice = 390m,
            Order = Orders[12],
            Dish = Dishes[8]
        },
        new OrderItem
        {
            Id = 41,
            OrderId = 13,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[12],
            Dish = Dishes[9]
        },

        new OrderItem
        {
            Id = 42,
            OrderId = 14,
            DishId = 8,
            Quantity = 1,
            UnitPrice = 590m,
            Order = Orders[13],
            Dish = Dishes[7]
        },
        new OrderItem
        {
            Id = 43,
            OrderId = 14,
            DishId = 11,
            Quantity = 1,
            UnitPrice = 350m,
            Order = Orders[13],
            Dish = Dishes[10]
        },
        new OrderItem
        {
            Id = 44,
            OrderId = 14,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[13],
            Dish = Dishes[9]
        },
        new OrderItem
        {
            Id = 45,
            OrderId = 14,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[13],
            Dish = Dishes[9]
        },

        new OrderItem
        {
            Id = 46,
            OrderId = 15,
            DishId = 11,
            Quantity = 1,
            UnitPrice = 350m,
            Order = Orders[14],
            Dish = Dishes[10]
        },
        new OrderItem
        {
            Id = 47,
            OrderId = 15,
            DishId = 12,
            Quantity = 1,
            UnitPrice = 420m,
            Order = Orders[14],
            Dish = Dishes[11]
        },
        new OrderItem
        {
            Id = 48,
            OrderId = 15,
            DishId = 10,
            Quantity = 1,
            UnitPrice = 150m,
            Order = Orders[14],
            Dish = Dishes[9]
        }
    ]);
    foreach (var orderItem in OrderItems)
{
    orderItem.Order.Items.Add(orderItem);
}
}
}