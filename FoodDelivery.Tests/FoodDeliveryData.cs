using FoodDelivery.Domain.Model;
using FoodDelivery.Domain.Enums;
using FoodDelivery.Domain.Services;

namespace FoodDelivery.Tests;

/// <summary>
/// Тестовый набор данных службы доставки еды
/// </summary>
public static class FoodDeliveryData
{
    public static List<Customer> Customers { get; }
    public static List<Restaurant> Restaurants { get; }
    public static List<Courier> Couriers { get; }
    public static List<Dish> Dishes { get; }
    public static List<Order> Orders { get; }
    public static List<OrderItem> OrderItems { get; }
    public static List<DishCategory> Categories { get; }

    static FoodDeliveryData()
    {
        Customers =
        [
            new Customer { Id = 1, FullName = "Иванов А.", PhoneNumber = "+79170000001", Address = "ул. Ленина 1" },
            new Customer { Id = 2, FullName = "Иванов Б.", PhoneNumber = "+79170000002", Address = "ул. Ленина 2" },
            new Customer { Id = 3, FullName = "Иванов В.", PhoneNumber = "+79170000003", Address = "ул. Ленина 3" },
            new Customer { Id = 4, FullName = "Иванов Г.", PhoneNumber = "+79170000004", Address = "ул. Ленина 4" },
            new Customer { Id = 5, FullName = "Иванов Д.", PhoneNumber = "+79170000005", Address = "ул. Ленина 5" },
            new Customer { Id = 6, FullName = "Иванов Е.", PhoneNumber = "+79170000006", Address = "ул. Ленина 6" },
            new Customer { Id = 7, FullName = "Иванов Ё.", PhoneNumber = "+79170000007", Address = "ул. Ленина 7" },
            new Customer { Id = 8, FullName = "Иванов Ж.", PhoneNumber = "+79170000008", Address = "ул. Ленина 8" },
            new Customer { Id = 9, FullName = "Иванов З.", PhoneNumber = "+79170000009", Address = "ул. Ленина 9" },
            new Customer { Id = 10, FullName = "Иванов И.", PhoneNumber = "+79170000010", Address = "ул. Ленина 10" }
        ];

        Restaurants =
        [
            new Restaurant { Id = 1, Name = "Пиццерия Pizza Tower", Address = "ул. Мира 25", Rating = 4.8, WorkingHours = "10:00 - 23:00" },
            new Restaurant { Id = 2, Name = "Суши-бар DANGANRONPA", Address = "ул. Мира 100", Rating = 4.9, WorkingHours = "11:00 - 00:00" },
            new Restaurant { Id = 3, Name = "Бургерная Machine Party", Address = "ул. Мира 3", Rating = 4.6, WorkingHours = "09:00 - 22:00" },
            new Restaurant { Id = 4, Name = "Кафе YAPYAP", Address = "ул. Мира 40", Rating = 4.5, WorkingHours = "08:00 - 21:00" },
            new Restaurant { Id = 5, Name = "Кофейня Leblanc", Address = "ул. Мира 7", Rating = 4.9, WorkingHours = "08:00 - 22:00" },
            new Restaurant { Id = 6, Name = "Бургерная REPO", Address = "ул. Мира 2", Rating = 4.4, WorkingHours = "10:00 - 22:00" },
            new Restaurant { Id = 7, Name = "Чайхана ZAVOD", Address = "ул. Мира 15", Rating = 4.3, WorkingHours = "11:00 - 23:00" },
            new Restaurant { Id = 8, Name = "Пиццерия Noita", Address = "ул. Мира 33", Rating = 4.7, WorkingHours = "10:00 - 23:00" },
            new Restaurant { Id = 9, Name = "Пельменная Ъ", Address = "ул. Мира 21", Rating = 4.2, WorkingHours = "09:00 - 21:00" },
            new Restaurant { Id = 10, Name = "Блинная Sandrock", Address = "ул. Мира 8", Rating = 4.5, WorkingHours = "08:00 - 20:00" }
        ];

        Couriers =
        [
            new Courier { Id = 1, FullName = "Петров А.", PhoneNumber = "+79171000001", Transport = TransportType.Bicycle },
            new Courier { Id = 2, FullName = "Петров Б.", PhoneNumber = "+79171000002", Transport = TransportType.Car },
            new Courier { Id = 3, FullName = "Петров В.", PhoneNumber = "+79171000003", Transport = TransportType.Foot },
            new Courier { Id = 4, FullName = "Петров Г.", PhoneNumber = "+79171000004", Transport = TransportType.Scooter },
            new Courier { Id = 5, FullName = "Петров Д.", PhoneNumber = "+79171000005", Transport = TransportType.Bicycle },
            new Courier { Id = 6, FullName = "Петров Е.", PhoneNumber = "+79171000006", Transport = TransportType.Car },
            new Courier { Id = 7, FullName = "Петров Ё.", PhoneNumber = "+79171000007", Transport = TransportType.Scooter },
            new Courier { Id = 8, FullName = "Петров Ж.", PhoneNumber = "+79171000008", Transport = TransportType.Foot },
            new Courier { Id = 9, FullName = "Петров З.", PhoneNumber = "+79171000009", Transport = TransportType.Bicycle },
            new Courier { Id = 10, FullName = "Петров И.", PhoneNumber = "+79171000010", Transport = TransportType.Car }
        ];

        Categories =
        [
            new DishCategory { Id = 1, Name = "Пицца" },
            new DishCategory { Id = 2, Name = "Суши" },
            new DishCategory { Id = 3, Name = "Бургеры" },
            new DishCategory { Id = 4, Name = "Салаты" },
            new DishCategory { Id = 5, Name = "Супы" },
            new DishCategory { Id = 6, Name = "Паста" },
            new DishCategory { Id = 7, Name = "Закуски" },
            new DishCategory { Id = 8, Name = "Напитки" },
            new DishCategory { Id = 9, Name = "Десерты" },
            new DishCategory { Id = 10, Name = "Завтраки" }
        ];

        Dishes =
        [
            new Dish { Id = 1, Name = "Маргарита", Weight = 450, Price = 450, CategoryId = 1, Category = Categories[0], RestaurantId = 1, Restaurant = Restaurants[0] },
            new Dish { Id = 2, Name = "Пепперони", Weight = 500, Price = 550, CategoryId = 1, Category = Categories[0], RestaurantId = 1, Restaurant = Restaurants[0] },
            new Dish { Id = 3, Name = "Карбонара", Weight = 480, Price = 420, CategoryId = 1, Category = Categories[0], RestaurantId = 1, Restaurant = Restaurants[0] },
            new Dish { Id = 4, Name = "Филадельфия", Weight = 280, Price = 890, CategoryId = 2, Category = Categories[1], RestaurantId = 2, Restaurant = Restaurants[1] },
            new Dish { Id = 5, Name = "Калифорния", Weight = 250, Price = 320, CategoryId = 2, Category = Categories[1], RestaurantId = 2, Restaurant = Restaurants[1] },
            new Dish { Id = 6, Name = "Суши с лососем", Weight = 70, Price = 180, CategoryId = 2, Category = Categories[1], RestaurantId = 2, Restaurant = Restaurants[1] },
            new Dish { Id = 7, Name = "Шефбургер", Weight = 320, Price = 350, CategoryId = 3, Category = Categories[2], RestaurantId = 3, Restaurant = Restaurants[2] },
            new Dish { Id = 8, Name = "Чизбургер", Weight = 290, Price = 470, CategoryId = 3, Category = Categories[2], RestaurantId = 3, Restaurant = Restaurants[2] },
            new Dish { Id = 9, Name = "Картофель фри", Weight = 150, Price = 150, CategoryId = 3, Category = Categories[2], RestaurantId = 3, Restaurant = Restaurants[2] },
            new Dish { Id = 10, Name = "Цезарь", Weight = 220, Price = 320, CategoryId = 4, Category = Categories[3], RestaurantId = 4, Restaurant = Restaurants[3] }
        ];

        Categories[0].Dishes.AddRange([Dishes[0], Dishes[1], Dishes[2]]);
        Categories[1].Dishes.AddRange([Dishes[3], Dishes[4], Dishes[5]]);
        Categories[2].Dishes.AddRange([Dishes[6], Dishes[7], Dishes[8]]);
        Categories[3].Dishes.Add(Dishes[9]);

        Restaurants[0].Dishes.AddRange([Dishes[0], Dishes[1], Dishes[2]]);
        Restaurants[1].Dishes.AddRange([Dishes[3], Dishes[4], Dishes[5]]);
        Restaurants[2].Dishes.AddRange([Dishes[6], Dishes[7], Dishes[8]]);
        Restaurants[3].Dishes.Add(Dishes[9]);

        Orders =
        [
            new Order
            {
                Id = 1,
                CustomerId = 1,
                Customer = Customers[0],
                RestaurantId = 1,
                Restaurant = Restaurants[0],
                CourierId = 1,
                Courier = Couriers[0],
                CreatedAt = new DateTime(2024, 5, 1, 12, 0, 0),
                DeliveredAt = new DateTime(2024, 5, 1, 12, 35, 0),
                Status = OrderStatus.Delivered,
                DeliveryFee = 150,
                DeliveryAddress = "ул. Ленина 1",
                PaymentMethod = PaymentMethod.Card
            },

            new Order
            {
                Id = 2,
                CustomerId = 2,
                Customer = Customers[1],
                RestaurantId = 1,
                Restaurant = Restaurants[0],
                CourierId = 2,
                Courier = Couriers[1],
                CreatedAt = new DateTime(2024, 5, 2, 18, 30, 0),
                DeliveredAt = new DateTime(2024, 5, 2, 18, 50, 0),
                Status = OrderStatus.Delivered,
                DeliveryFee = 150,
                DeliveryAddress = "ул. Ленина 2",
                PaymentMethod = PaymentMethod.Cash
            },

            new Order
            {
                Id = 3,
                CustomerId = 3,
                Customer = Customers[2],
                RestaurantId = 2,
                Restaurant = Restaurants[1],
                CourierId = 3,
                Courier = Couriers[2],
                CreatedAt = new DateTime(2024, 5, 3, 19, 0, 0),
                DeliveredAt = new DateTime(2024, 5, 3, 19, 45, 0),
                Status = OrderStatus.Delivered,
                DeliveryFee = 200,
                DeliveryAddress = "ул. Ленина 3",
                PaymentMethod = PaymentMethod.Online
            },

            new Order
            {
                Id = 4,
                CustomerId = 4,
                Customer = Customers[3],
                RestaurantId = 2,
                Restaurant = Restaurants[1],
                CourierId = 1,
                Courier = Couriers[0],
                CreatedAt = new DateTime(2024, 5, 5, 13, 15, 0),
                DeliveredAt = null,
                Status = OrderStatus.Delivering,
                DeliveryFee = 200,
                DeliveryAddress = "ул. Ленина 4",
                PaymentMethod = PaymentMethod.Card
            },

            new Order
            {
                Id = 5,
                CustomerId = 5,
                Customer = Customers[4],
                RestaurantId = 3,
                Restaurant = Restaurants[2],
                CourierId = 4,
                Courier = Couriers[3],
                CreatedAt = new DateTime(2024, 5, 6, 20, 45, 0),
                DeliveredAt = null,
                Status = OrderStatus.Preparing,
                DeliveryFee = 100,
                DeliveryAddress = "ул. Ленина 5",
                PaymentMethod = PaymentMethod.Cash
            },

            new Order
            {
                Id = 6,
                CustomerId = 6,
                Customer = Customers[5],
                RestaurantId = 4,
                Restaurant = Restaurants[3],
                CourierId = 5,
                Courier = Couriers[4],
                CreatedAt = new DateTime(2024, 5, 8, 11, 0, 0),
                DeliveredAt = new DateTime(2024, 5, 8, 11, 40, 0),
                Status = OrderStatus.Delivered,
                DeliveryFee = 120,
                DeliveryAddress = "ул. Ленина 6",
                PaymentMethod = PaymentMethod.Online
            },

            new Order
            {
                Id = 7,
                CustomerId = 7,
                Customer = Customers[6],
                RestaurantId = 5,
                Restaurant = Restaurants[4],
                CourierId = 6,
                Courier = Couriers[5],
                CreatedAt = new DateTime(2024, 5, 10, 14, 20, 0),
                DeliveredAt = null,
                Status = OrderStatus.Cancelled,
                DeliveryFee = 90,
                DeliveryAddress = "ул. Ленина 7",
                PaymentMethod = PaymentMethod.Card
            },

            new Order
            {
                Id = 11,
                CustomerId = 1,
                Customer = Customers[0],
                RestaurantId = 1,
                Restaurant = Restaurants[0],
                CourierId = 10,
                Courier = Couriers[9],
                CreatedAt = new DateTime(2024, 5, 20, 12, 50, 0),
                DeliveredAt = new DateTime(2024, 5, 20, 13, 20, 0),
                Status = OrderStatus.Delivered,
                DeliveryFee = 80,
                DeliveryAddress = "ул. Ленина 1",
                PaymentMethod = PaymentMethod.Online
            },

            new Order
            {
                Id = 12,
                CustomerId = 2,
                Customer = Customers[1],
                RestaurantId = 1,
                Restaurant = Restaurants[0],
                CourierId = 2,
                Courier = Couriers[1],
                CreatedAt = new DateTime(2024, 5, 22, 19, 30, 0),
                DeliveredAt = new DateTime(2024, 5, 22, 20, 05, 0),
                Status = OrderStatus.Delivered,
                DeliveryFee = 110,
                DeliveryAddress = "ул. Ленина 2",
                PaymentMethod = PaymentMethod.Cash
            },

            new Order
            {
                Id = 13,
                CustomerId = 3,
                Customer = Customers[2],
                RestaurantId = 2,
                Restaurant = Restaurants[1],
                CourierId = 3,
                Courier = Couriers[2],
                CreatedAt = new DateTime(2024, 5, 24, 15, 0, 0),
                DeliveredAt = new DateTime(2024, 5, 24, 15, 30, 0),
                Status = OrderStatus.Delivered,
                DeliveryFee = 250,
                DeliveryAddress = "ул. Ленина 3",
                PaymentMethod = PaymentMethod.Card
            },

            new Order
            {
                Id = 14,
                CustomerId = 3,
                Customer = Customers[2],
                RestaurantId = 3,
                Restaurant = Restaurants[2],
                CourierId = 1,
                Courier = Couriers[0],
                CreatedAt = new DateTime(2024, 5, 25, 14, 0, 0),
                DeliveredAt = new DateTime(2024, 5, 25, 14, 30, 0),
                Status = OrderStatus.Delivered,
                DeliveryFee = 190,
                DeliveryAddress = "ул. Ленина 3",
                PaymentMethod = PaymentMethod.Online
            },

            new Order
            {
                Id = 15,
                CustomerId = 8,
                Customer = Customers[7],
                RestaurantId = 4,
                Restaurant = Restaurants[3],
                CourierId = 7,
                Courier = Couriers[6],
                CreatedAt = new DateTime(2024, 5, 26, 17, 0, 0),
                DeliveredAt = new DateTime(2024, 5, 26, 17, 35, 0),
                Status = OrderStatus.Delivered,
                DeliveryFee = 130,
                DeliveryAddress = "ул. Ленина 8",
                PaymentMethod = PaymentMethod.Cash
            }
        ];

        OrderItems =
        [
            new OrderItem { Id = 1, OrderId = 1, Order = Orders[0], DishId = 1, Dish = Dishes[0], Quantity = 1, UnitPrice = 450 },
            new OrderItem { Id = 2, OrderId = 1, Order = Orders[0], DishId = 2, Dish = Dishes[1], Quantity = 2, UnitPrice = 550 },
            new OrderItem { Id = 3, OrderId = 2, Order = Orders[1], DishId = 2, Dish = Dishes[1], Quantity = 1, UnitPrice = 550 },
            new OrderItem { Id = 4, OrderId = 3, Order = Orders[2], DishId = 4, Dish = Dishes[3], Quantity = 2, UnitPrice = 890 },
            new OrderItem { Id = 5, OrderId = 3, Order = Orders[2], DishId = 5, Dish = Dishes[4], Quantity = 1, UnitPrice = 320 },
            new OrderItem { Id = 6, OrderId = 4, Order = Orders[3], DishId = 5, Dish = Dishes[4], Quantity = 1, UnitPrice = 320 },
            new OrderItem { Id = 7, OrderId = 4, Order = Orders[3], DishId = 6, Dish = Dishes[5], Quantity = 3, UnitPrice = 180 },
            new OrderItem { Id = 8, OrderId = 5, Order = Orders[4], DishId = 7, Dish = Dishes[6], Quantity = 2, UnitPrice = 350 },
            new OrderItem { Id = 9, OrderId = 5, Order = Orders[4], DishId = 8, Dish = Dishes[7], Quantity = 1, UnitPrice = 470 },
            new OrderItem { Id = 10, OrderId = 6, Order = Orders[5], DishId = 10, Dish = Dishes[9], Quantity = 2, UnitPrice = 320 },
            new OrderItem { Id = 11, OrderId = 7, Order = Orders[6], DishId = 3, Dish = Dishes[2], Quantity = 1, UnitPrice = 420 },
            new OrderItem { Id = 12, OrderId = 11, Order = Orders[7], DishId = 3, Dish = Dishes[2], Quantity = 1, UnitPrice = 420 },
            new OrderItem { Id = 13, OrderId = 12, Order = Orders[8], DishId = 1, Dish = Dishes[0], Quantity = 2, UnitPrice = 450 },
            new OrderItem { Id = 14, OrderId = 13, Order = Orders[9], DishId = 4, Dish = Dishes[3], Quantity = 2, UnitPrice = 890 },
            new OrderItem { Id = 15, OrderId = 13, Order = Orders[9], DishId = 6, Dish = Dishes[5], Quantity = 2, UnitPrice = 180 },
            new OrderItem { Id = 16, OrderId = 14, Order = Orders[10], DishId = 7, Dish = Dishes[6], Quantity = 3, UnitPrice = 350 },
            new OrderItem { Id = 17, OrderId = 15, Order = Orders[11], DishId = 10, Dish = Dishes[9], Quantity = 1, UnitPrice = 320 }
        ];

         foreach (var order in Orders)
        {
            order.Items = [.. OrderItems.Where(i => i.OrderId == order.Id)];
            order.Total = OrderCostCalculator.CalculateTotal(order);
        }

        foreach (var customer in Customers)
        {
            customer.Orders = [.. Orders.Where(o => o.CustomerId == customer.Id)];
        }

        foreach (var restaurant in Restaurants)
        {
            restaurant.Orders = [.. Orders.Where(o => o.RestaurantId == restaurant.Id)];
        }

        foreach (var courier in Couriers)
        {
            courier.Orders = [.. Orders.Where(o => o.CourierId == courier.Id)];
        }
    }
}
