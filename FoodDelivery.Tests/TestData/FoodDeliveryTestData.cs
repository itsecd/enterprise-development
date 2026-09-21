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
    }
}