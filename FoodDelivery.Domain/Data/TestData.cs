using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Domain.Data;

public static class TestData
{
    public static List<Category> Categories { get; } = new()
    {
        new Category
        {
            Id = 1,
            Name = "Пицца"
        },

        new Category
        {
            Id = 2,
            Name = "Бургеры"
        },

        new Category
        {
            Id = 3,
            Name = "Закуски"
        },

        new Category
        {
            Id = 4,
            Name = "Салаты"
        },

        new Category
        {
            Id = 5,
            Name = "Супы"
        },

        new Category
        {
            Id = 6,
            Name = "Паста"
        },

        new Category
        {
            Id = 7,
            Name = "Роллы"
        },

        new Category
        {
            Id = 8,
            Name = "Десерты"
        },

        new Category
        {
            Id = 9,
            Name = "Напитки"
        },

        new Category
        {
            Id = 10,
            Name = "Завтраки"
        }
    };

    public static List<Restaurant> Restaurants { get; } = new()
    {
        new Restaurant
        {
            Id = 1,
            Name = "Додо Пицца",
            Address = "просп. Ленина, 1",
            Rating = 4.8,
            OpeningTime = new TimeOnly(10, 0),
            ClosingTime = new TimeOnly(23, 0)
        },

        new Restaurant
        {
            Id = 2,
            Name = "Burger King",
            Address = "ул. Тухачевского, 80",
            Rating = 4.6,
            OpeningTime = new TimeOnly(9, 0),
            ClosingTime = new TimeOnly(22, 0)
        },

        new Restaurant
        {
            Id = 3,
            Name = "Tanuki",
            Address = "ул. Коммунистическая, 17",
            Rating = 4.9,
            OpeningTime = new TimeOnly(11, 0),
            ClosingTime = new TimeOnly(23, 30)
        },

        new Restaurant
        {
            Id = 4,
            Name = "Salad Bar",
            Address = "ул. Гагарина, 66",
            Rating = 4.3,
            OpeningTime = new TimeOnly(9, 0),
            ClosingTime = new TimeOnly(21, 0)
        },

        new Restaurant
        {
            Id = 5,
            Name = "Мушмула",
            Address = "ул. Галактионовская, 133",
            Rating = 4.6,
            OpeningTime = new TimeOnly(10, 0),
            ClosingTime = new TimeOnly(22, 0)
        },

        new Restaurant
        {
            Id = 6,
            Name = "Дари Дури",
            Address = "ул. Дачная, 12",
            Rating = 4.4,
            OpeningTime = new TimeOnly(8, 0),
            ClosingTime = new TimeOnly(22, 0)
        },

        new Restaurant
        {
            Id = 7,
            Name = "Surf Coffee",
            Address = "ул. Полевая, 28",
            Rating = 4.8,
            OpeningTime = new TimeOnly(8, 0),
            ClosingTime = new TimeOnly(21, 0)
        },

        new Restaurant
        {
            Id = 8,
            Name = "Чипа Лучо",
            Address = "ул. Ново-Садовая, 228",
            Rating = 4.2,
            OpeningTime = new TimeOnly(10, 0),
            ClosingTime = new TimeOnly(23, 0)
        },

        new Restaurant
        {
            Id = 9,
            Name = "Белотурка",
            Address = "ул. Молодогвардейская,127",
            Rating = 4.8,
            OpeningTime = new TimeOnly(7, 0),
            ClosingTime = new TimeOnly(20, 0)
        },

        new Restaurant
        {
            Id = 10,
            Name = "Gellert",
            Address = "ул. Владимирская, 38",
            Rating = 4.0,
            OpeningTime = new TimeOnly(10, 0),
            ClosingTime = new TimeOnly(22, 0)
        }
    };

    public static List<Customer> Customers { get; } = new()
    {
        new Customer
        {
            Id = 1,
            LastName = "Мэдисон",
            FirstName = "Илья",
            Patronymic = "Сергеевич",
            Phone = "+79270000228",
            DeliveryAddress = "Самара, ул. Ленина, д. 1"
        },

        new Customer
        {
            Id = 2,
            LastName = "Зырянов",
            FirstName = "Кирилл",
            Patronymic = null,
            Phone = "+79270000666",
            DeliveryAddress = "Самара, ул. Пушкина, д. 10"
        },

        new Customer
        {
            Id = 3,
            LastName = "Купитман",
            FirstName = "Иван",
            Patronymic = "Натанович",
            Phone = "+79080001337",
            DeliveryAddress = "Самара, ул. Гагарина, д. 15"
        },

        new Customer
        {
            Id = 4,
            LastName = "Бобков",
            FirstName = "Геннадий",
            Patronymic = "Андреевич",
            Phone = "+79040000066",
            DeliveryAddress = "Самара, ул. Мира, д. 20"
        },

        new Customer
        {
            Id = 5,
            LastName = "Джопс",
            FirstName = "Степан",
            Patronymic = "Алексеевич",
            Phone = "+79270000077",
            DeliveryAddress = "Самара, ул. Советская, д. 5"
        },

        new Customer
        {
            Id = 6,
            LastName = "Гриффин",
            FirstName = "Пётр",
            Patronymic = null,
            Phone = "+79080001109",
            DeliveryAddress = "Самара, ул. Центральная, д. 8"
        },

        new Customer
        {
            Id = 7,
            LastName = "Дупло",
            FirstName = "Ольга",
            Patronymic = "Викторовна",
            Phone = "+79270000777",
            DeliveryAddress = "Самара, ул. Школьная, д. 12"
        },

        new Customer
        {
            Id = 8,
            LastName = "Петров",
            FirstName = "Денис",
            Patronymic = "Игоревич",
            Phone = "+79270000888",
            DeliveryAddress = "Самара, ул. Молодёжная, д. 7"
        },

        new Customer
        {
            Id = 9,
            LastName = "Морозова",
            FirstName = "Екатерина",
            Patronymic = "Сергеевна",
            Phone = "+79270000089",
            DeliveryAddress = "Самара, ул. Новая, д. 18"
        },

        new Customer
        {
            Id = 10,
            LastName = "Сопло",
            FirstName = "Никита",
            Patronymic = "Сергеевич",
            Phone = "+79270001010",
            DeliveryAddress = "Самара, ул. Садовая, д. 25"
        }
    };

    public static List<Dish> Dishes { get; } = new()
    {
        new Dish
        {
            Id = 1,
            Name = "Пицца Песто",
            WeightGrams = 500,
            Price = 750m,
            CategoryId = 1,
            Category = Categories[0],
            RestaurantId = 1,
            Restaurant = Restaurants[0]
        },

        new Dish
        {
            Id = 2,
            Name = "Пицца 4 сыра",
            WeightGrams = 550,
            Price = 650m,
            CategoryId = 1,
            Category = Categories[0],
            RestaurantId = 1,
            Restaurant = Restaurants[0]
        },

        new Dish
        {
            Id = 3,
            Name = "Бургер с рваной говядиной",
            WeightGrams = 300,
            Price = 650m,
            CategoryId = 2,
            Category = Categories[1],
            RestaurantId = 2,
            Restaurant = Restaurants[1]
        },

        new Dish
        {
            Id = 4,
            Name = "Чизбушка",
            WeightGrams = 320,
            Price = 500m,
            CategoryId = 2,
            Category = Categories[1],
            RestaurantId = 2,
            Restaurant = Restaurants[1]
        },

        new Dish
        {
            Id = 5,
            Name = "Луковые кольца",
            WeightGrams = 150,
            Price = 220m,
            CategoryId = 3,
            Category = Categories[2],
            RestaurantId = 2,
            Restaurant = Restaurants[1]
        },

        new Dish
        {
            Id = 6,
            Name = "Греческий салат",
            WeightGrams = 280,
            Price = 420m,
            CategoryId = 4,
            Category = Categories[3],
            RestaurantId = 4,
            Restaurant = Restaurants[3]
        },

        new Dish
        {
            Id = 7,
            Name = "Солянка",
            WeightGrams = 350,
            Price = 380m,
            CategoryId = 5,
            Category = Categories[4],
            RestaurantId = 5,
            Restaurant = Restaurants[4]
        },

        new Dish
        {
            Id = 8,
            Name = "Паста Аррабиата",
            WeightGrams = 400,
            Price = 590m,
            CategoryId = 6,
            Category = Categories[5],
            RestaurantId = 8,
            Restaurant = Restaurants[7]
        },

        new Dish
        {
            Id = 9,
            Name = "Роллы с тунцом",
            WeightGrams = 250,
            Price = 680m,
            CategoryId = 7,
            Category = Categories[6],
            RestaurantId = 3,
            Restaurant = Restaurants[2]
        },

        new Dish
        {
            Id = 10,
            Name = "Торт медовик",
            WeightGrams = 180,
            Price = 300m,
            CategoryId = 8,
            Category = Categories[7],
            RestaurantId = 9,
            Restaurant = Restaurants[8]
        },

        new Dish
        {
            Id = 11,
            Name = "Капучино",
            WeightGrams = 300,
            Price = 450m,
            CategoryId = 9,
            Category = Categories[8],
            RestaurantId = 7,
            Restaurant = Restaurants[6]
        },

        new Dish
        {
            Id = 12,
            Name = "Сырники",
            WeightGrams = 250,
            Price = 350m,
            CategoryId = 10,
            Category = Categories[9],
            RestaurantId = 9,
            Restaurant = Restaurants[8]
        }
    };

    public static List<Order> Orders { get; } = new()
    {
        new Order
        {
            Id = 1,
            CustomerId = 1,
            Customer = Customers[0],
            RestaurantId = 1,
            Restaurant = Restaurants[0],
            CreatedAt = new DateTime(2026, 9, 1, 12, 0, 0),
            DeliveredAt = new DateTime(2026, 9, 1, 12, 35, 0),
        },

        new Order
        {
            Id = 2,
            CustomerId = 2,
            Customer = Customers[1],
            RestaurantId = 1,
            Restaurant = Restaurants[0],
            CreatedAt = new DateTime(2026, 9, 2, 13, 0, 0),
            DeliveredAt = new DateTime(2026, 9, 2, 13, 50, 0),
        },

        new Order
        {
            Id = 3,
            CustomerId = 3,
            Customer = Customers[2],
            RestaurantId = 2,
            Restaurant = Restaurants[1],
            CreatedAt = new DateTime(2026, 9, 3, 14, 0, 0),
            DeliveredAt = new DateTime(2026, 9, 3, 14, 25, 0),
        },

        new Order
        {
            Id = 4,
            CustomerId = 4,
            Customer = Customers[3],
            RestaurantId = 2,
            Restaurant = Restaurants[1],
            CreatedAt = new DateTime(2026, 9, 4, 15, 0, 0),
            DeliveredAt = new DateTime(2026, 9, 4, 15, 45, 0),
        },

        new Order
        {
            Id = 5,
            CustomerId = 5,
            Customer = Customers[4],
            RestaurantId = 3,
            Restaurant = Restaurants[2],
            CreatedAt = new DateTime(2026, 9, 5, 16, 0, 0),
            DeliveredAt = new DateTime(2026, 9, 5, 16, 30, 0),
        },

        new Order
        {
            Id = 6,
            CustomerId = 6,
            Customer = Customers[5],
            RestaurantId = 3,
            Restaurant = Restaurants[2],
            CreatedAt = new DateTime(2026, 9, 6, 17, 0, 0),
            DeliveredAt = new DateTime(2026, 9, 6, 17, 40, 0),
        },

        new Order
        {
            Id = 7,
            CustomerId = 7,
            Customer = Customers[6],
            RestaurantId = 4,
            Restaurant = Restaurants[3],
            CreatedAt = new DateTime(2026, 9, 7, 18, 0, 0),
            DeliveredAt = new DateTime(2026, 9, 7, 18, 20, 0),
        },

        new Order
        {
            Id = 8,
            CustomerId = 8,
            Customer = Customers[7],
            RestaurantId = 5,
            Restaurant = Restaurants[4],
            CreatedAt = new DateTime(2026, 9, 8, 19, 45, 0),
            DeliveredAt = new DateTime(2026, 9, 8, 19, 55, 0),
        },

        new Order
        {
            Id = 9,
            CustomerId = 9,
            Customer = Customers[8],
            RestaurantId = 8,
            Restaurant = Restaurants[7],
            CreatedAt = new DateTime(2026, 9, 9, 10, 0, 0),
            DeliveredAt = new DateTime(2026, 9, 9, 10, 10, 0),
        },

        new Order
        {
            Id = 10,
            CustomerId = 10,
            Customer = Customers[9],
            RestaurantId = 7,
            Restaurant = Restaurants[6],
            CreatedAt = new DateTime(2026, 9, 10, 11, 0, 0),
            DeliveredAt = new DateTime(2026, 9, 10, 11, 30, 0),
        },

        new Order
        {
            Id = 11,
            CustomerId = 1,
            Customer = Customers[0],
            RestaurantId = 7,
            Restaurant = Restaurants[6],
            CreatedAt = new DateTime(2026, 9, 11, 12, 0, 0),
            DeliveredAt = new DateTime(2026, 9, 11, 12, 25, 0),
        },

        new Order
        {
            Id = 12,
            CustomerId = 3,
            Customer = Customers[2],
            RestaurantId = 7,
            Restaurant = Restaurants[6],
            CreatedAt = new DateTime(2026, 9, 12, 13, 0, 0),
            DeliveredAt = new DateTime(2026, 9, 12, 13, 30, 0),
        },

        new Order
        {
            Id = 13,
            CustomerId = 4,
            Customer = Customers[3],
            RestaurantId = 7,
            Restaurant = Restaurants[6],
            CreatedAt = new DateTime(2026, 9, 13, 14, 0, 0),
            DeliveredAt = new DateTime(2026, 9, 13, 14, 20, 0),
        },

        new Order
        {
            Id = 14,
            CustomerId = 5,
            Customer = Customers[4],
            RestaurantId = 2,
            Restaurant = Restaurants[1],
            CreatedAt = new DateTime(2026, 9, 14, 15, 0, 0),
            DeliveredAt = new DateTime(2026, 9, 14, 15, 35, 0),
        },

        new Order
        {
            Id = 15,
            CustomerId = 6,
            Customer = Customers[5],
            RestaurantId = 8,
            Restaurant = Restaurants[7],
            CreatedAt = new DateTime(2026, 9, 15, 16, 0, 0),
            DeliveredAt = new DateTime(2026, 9, 15, 16, 40, 0),
        }
    };

    public static List<OrderItem> OrderItems { get; } = new()
    {
        new OrderItem
        {
            Id = 1,
            OrderId = 1,
            DishId = 1,
            Dish = Dishes[0],
            Quantity = 1,
        },

        new OrderItem
        {
            Id = 2,
            OrderId = 1,
            DishId = 2,
            Dish = Dishes[1],
            Quantity = 1,
        },

        new OrderItem
        {
            Id = 3,
            OrderId = 2,
            DishId = 1,
            Dish = Dishes[0],
            Quantity = 1,
        },

        new OrderItem
        {
            Id = 4,
            OrderId = 2,
            DishId = 2,
            Dish = Dishes[1],
            Quantity = 2,
        },

        new OrderItem
        {
            Id = 5,
            OrderId = 3,
            DishId = 3,
            Dish = Dishes[2],
            Quantity = 1,
        },

        new OrderItem
        {
            Id = 6,
            OrderId = 3,
            DishId = 5,
            Dish = Dishes[4],
            Quantity = 1,
        },

        new OrderItem
        {
            Id = 7,
            OrderId = 4,
            DishId = 4,
            Dish = Dishes[3],
            Quantity = 2,
        },

        new OrderItem
        {
            Id = 8,
            OrderId = 4,
            DishId = 5,
            Dish = Dishes[4],
            Quantity = 1,
        },

        new OrderItem
        {
            Id = 9,
            OrderId = 5,
            DishId = 9,
            Dish = Dishes[8],
            Quantity = 1,
        },

        new OrderItem
        {
            Id = 10,
            OrderId = 6,
            DishId = 9,
            Dish = Dishes[8],
            Quantity = 1,
        },

        new OrderItem
        {
            Id = 11,
            OrderId = 7,
            DishId = 6,
            Dish = Dishes[5],
            Quantity = 1,
        },

        new OrderItem
        {
            Id = 12,
            OrderId = 8,
            DishId = 7,
            Dish = Dishes[6],
            Quantity = 1,
        },

        new OrderItem
        {
            Id = 13,
            OrderId = 9,
            DishId = 8,
            Dish = Dishes[7],
            Quantity = 1,
        },

        new OrderItem
        {
            Id = 14,
            OrderId = 10,
            DishId = 11,
            Dish = Dishes[10],
            Quantity = 1,
        },

        new OrderItem
        {
            Id = 15,
            OrderId = 11,
            DishId = 11,
            Dish = Dishes[10],
            Quantity = 1,
        },

        new OrderItem
        {
            Id = 16,
            OrderId = 12,
            DishId = 11,
            Dish = Dishes[10],
            Quantity = 2,
        },

        new OrderItem
        {
            Id = 17,
            OrderId = 13,
            DishId = 11,
            Dish = Dishes[10],
            Quantity = 1,
        },

        new OrderItem
        {
            Id = 18,
            OrderId = 14,
            DishId = 3,
            Dish = Dishes[2],
            Quantity = 1,
        },

        new OrderItem
        {
            Id = 19,
            OrderId = 15,
            DishId = 8,
            Dish = Dishes[7],
            Quantity = 1,
        }
    };

    // связка orderitems с orders
    static TestData()
    {
        foreach (var restaurant in Restaurants)
        {
            restaurant.Dishes = Dishes
                .Where(dish => dish.RestaurantId == restaurant.Id)
                .ToList();
        }

        foreach (var order in Orders)
        {
            order.Items = OrderItems
                .Where(item => item.OrderId == order.Id)
                .ToList();
        }
    }
}

