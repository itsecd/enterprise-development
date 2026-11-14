using FoodDelivery.Domain.Entities;

namespace FoodDelivery.Domain.Data;

/// <summary>
/// Заполняет контекст начальными тестовыми данными.
/// </summary>
public static class DataSeeder
{
    public static FoodDeliveryContext Seed()
    {
        var context = new FoodDeliveryContext();

        context.Clients.AddRange(
        [
            new()
            {
                Id = 1,
                FullName = "Зубенко Михаил Петрович",
                Phone = "+79277777777",
                DeliveryAddress = "г. Самара, ул. Ленина, д. 10"
            },
            new()
            {
                Id = 2,
                FullName = "Дылда Ольга Олеговна",
                Phone = "+79270001111",
                DeliveryAddress = "г. Самара, ул. Гагарина, д. 15"
            },
            new()
            {
                Id = 3,
                FullName = "Заболотный Антон Иванович",
                Phone = "+79273333333",
                DeliveryAddress = "г. Самара, ул. Гаражная, д. 21"
            },
            new()
            {
                Id = 4,
                FullName = "Скорлупа Иннокентий Сергеевич",
                Phone = "+79270101010",
                DeliveryAddress = "г. Самара, ул. Гая, д. 8"
            },
            new()
            {
                Id = 5,
                FullName = "Иванов Иван Иванович",
                Phone = "+79270202020",
                DeliveryAddress = "г. Самара, ул. Революционная, д. 32"
            },
            new()
            {
                Id = 6,
                FullName = "Петушков Илья Викторович",
                Phone = "+7270000001",
                DeliveryAddress = "г. Самара, ул. Ново-Садовая, д. 14"
            },
            new()
            {
                Id = 7,
                FullName = "Костыль Ирина Петровна",
                Phone = "+79371004554",
                DeliveryAddress = "г. Самара, ул. Стара-Загора, д. 5"
            },
            new()
            {
                Id = 8,
                FullName = "Петров Петр Петрович",
                Phone = "+7271232323",
                DeliveryAddress = "г. Самара, ул. Дыбенко, д. 19"
            },
            new()
            {
                Id = 9,
                FullName = "Плотников Иван Иванович",
                Phone = "+79276547656",
                DeliveryAddress = "г. Самара, ул. Гаражная, д. 27"
            },
            new()
            {
                Id = 10,
                FullName = "Колодин Алексей Сергеевич",
                Phone = "+7271044141",
                DeliveryAddress = "г. Самара, ул. Мяги, д. 11"
            }
        ]);

        context.Restaurants.AddRange(
        [
            new()
            {
                Id = 1,
                Name = "Ростикс",
                Address = "г. Самара, ул. Аэродромная, д. 25",
                Rating = 4.8,
                OpenTime = new TimeOnly(9, 0),
                CloseTime = new TimeOnly(23, 0)
            },
            new()
            {
                Id = 2,
                Name = "Вкусно и точка",
                Address = "г. Самара, ул. Дыбенко, д. 30",
                Rating = 4.5,
                OpenTime = new TimeOnly(8, 0),
                CloseTime = new TimeOnly(23, 0)
            },
            new()
            {
                Id = 3,
                Name = "Фуджи",
                Address = "г. Самара, ул. Гагарина, д. 42",
                Rating = 4.6,
                OpenTime = new TimeOnly(10, 0),
                CloseTime = new TimeOnly(23, 0)
            },
            new()
            {
                Id = 4,
                Name = "Тануки",
                Address = "г. Самара, ул. Стара-Загора, д. 35",
                Rating = 4.8,
                OpenTime = new TimeOnly(11, 0),
                CloseTime = new TimeOnly(23, 0)
            },
            new()
            {
                Id = 5,
                Name = "Комитет",
                Address = "г. Самара, ул. Дыбенко, д. 12",
                Rating = 4.9,
                OpenTime = new TimeOnly(10, 0),
                CloseTime = new TimeOnly(23, 15)
            },
            new()
            {
                Id = 6,
                Name = "Шашлычная",
                Address = "г. Самара, ул. Революционная, д. 51",
                Rating = 4.3,
                OpenTime = new TimeOnly(9, 0),
                CloseTime = new TimeOnly(22, 30)
            },
            new()
            {
                Id = 7,
                Name = "Перчини",
                Address = "г. Самара, ул. Ново-Садовая, д. 76",
                Rating = 4.6,
                OpenTime = new TimeOnly(8, 30),
                CloseTime = new TimeOnly(22, 0)
            },
            new()
            {
                Id = 8,
                Name = "Дом Нино",
                Address = "г. Самара, ул. Пушкина, д. 14",
                Rating = 4.9,
                OpenTime = new TimeOnly(11, 0),
                CloseTime = new TimeOnly(23, 0)
            },
            new()
            {
                Id = 9,
                Name = "Супра",
                Address = "г. Самара, ул. Мяги, д. 28",
                Rating = 4.9,
                OpenTime = new TimeOnly(10, 0),
                CloseTime = new TimeOnly(23, 0)
            },
            new()
            {
                Id = 10,
                Name = "Кафе Чаша",
                Address = "г. Самара, ул. Куйбышева, д. 10",
                Rating = 4.5,
                OpenTime = new TimeOnly(8, 0),
                CloseTime = new TimeOnly(18, 0)
            }
        ]);

        context.Categories.AddRange(
        [
            new() { Id = 1, Name = "Пицца" },
            new() { Id = 2, Name = "Суши" },
            new() { Id = 3, Name = "Бургеры" },
            new() { Id = 4, Name = "Салаты" },
            new() { Id = 5, Name = "Супы" },
            new() { Id = 6, Name = "Горячие блюда" },
            new() { Id = 7, Name = "Десерты" },
            new() { Id = 8, Name = "Напитки" },
            new() { Id = 9, Name = "Закуски" },
            new() { Id = 10, Name = "Соусы" }
        ]);

        context.Dishes.AddRange(
        [
            new()
            {
                Id = 1,
                Name = "Пицца Маргарита",
                Weight = 450,
                Price = 550m,
                CategoryId = 1,
                Category = context.Categories[0],
                RestaurantId = 1,
                Restaurant = context.Restaurants[0]
            },
            new()
            {
                Id = 2,
                Name = "Чикенбургер",
                Weight = 300,
                Price = 420m,
                CategoryId = 3,
                Category = context.Categories[2],
                RestaurantId = 1,
                Restaurant = context.Restaurants[0]
            },
            new()
            {
                Id = 3,
                Name = "Бургер классический",
                Weight = 320,
                Price = 390m,
                CategoryId = 3,
                Category = context.Categories[2],
                RestaurantId = 2,
                Restaurant = context.Restaurants[1]
            },
            new()
            {
                Id = 4,
                Name = "Картофель фри",
                Weight = 180,
                Price = 190m,
                CategoryId = 9,
                Category = context.Categories[8],
                RestaurantId = 2,
                Restaurant = context.Restaurants[1]
            },
            new()
            {
                Id = 5,
                Name = "Филадельфия",
                Weight = 280,
                Price = 650m,
                CategoryId = 2,
                Category = context.Categories[1],
                RestaurantId = 3,
                Restaurant = context.Restaurants[2]
            },
            new()
            {
                Id = 6,
                Name = "Калифорния",
                Weight = 260,
                Price = 590m,
                CategoryId = 2,
                Category = context.Categories[1],
                RestaurantId = 3,
                Restaurant = context.Restaurants[2]
            },
            new()
            {
                Id = 7,
                Name = "Том Ям",
                Weight = 400,
                Price = 720m,
                CategoryId = 5,
                Category = context.Categories[4],
                RestaurantId = 4,
                Restaurant = context.Restaurants[3]
            },
            new()
            {
                Id = 8,
                Name = "Лапша с курицей",
                Weight = 350,
                Price = 540m,
                CategoryId = 6,
                Category = context.Categories[5],
                RestaurantId = 4,
                Restaurant = context.Restaurants[3]
            },
            new()
            {
                Id = 9,
                Name = "Стейк из говядины",
                Weight = 350,
                Price = 1250m,
                CategoryId = 6,
                Category = context.Categories[5],
                RestaurantId = 5,
                Restaurant = context.Restaurants[4]
            },
            new()
            {
                Id = 10,
                Name = "Салат Цезарь",
                Weight = 250,
                Price = 490m,
                CategoryId = 4,
                Category = context.Categories[3],
                RestaurantId = 5,
                Restaurant = context.Restaurants[4]
            },
            new()
            {
                Id = 11,
                Name = "Шашлык из свинины",
                Weight = 400,
                Price = 850m,
                CategoryId = 6,
                Category = context.Categories[5],
                RestaurantId = 6,
                Restaurant = context.Restaurants[5]
            },
            new()
            {
                Id = 12,
                Name = "Паста Карбонара",
                Weight = 350,
                Price = 620m,
                CategoryId = 6,
                Category = context.Categories[5],
                RestaurantId = 7,
                Restaurant = context.Restaurants[6]
            },
            new()
            {
                Id = 13,
                Name = "Тирамису",
                Weight = 180,
                Price = 390m,
                CategoryId = 7,
                Category = context.Categories[6],
                RestaurantId = 8,
                Restaurant = context.Restaurants[7]
            },
            new()
            {
                Id = 14,
                Name = "Хачапури",
                Weight = 400,
                Price = 580m,
                CategoryId = 6,
                Category = context.Categories[5],
                RestaurantId = 9,
                Restaurant = context.Restaurants[8]
            },
            new()
            {
                Id = 15,
                Name = "Борщ",
                Weight = 350,
                Price = 360m,
                CategoryId = 5,
                Category = context.Categories[4],
                RestaurantId = 10,
                Restaurant = context.Restaurants[9]
            }
        ]);

        context.Orders.AddRange(
        [
            new()
            {
                Id = 1,
                ClientId = 1,
                Client = context.Clients[0],
                RestaurantId = 1,
                Restaurant = context.Restaurants[0],
                OrderDateTime = new DateTime(2026, 9, 1, 12, 10, 0),
                DeliveryDateTime = new DateTime(2026, 9, 1, 12, 45, 0),
                TotalPrice = 970m,
                Dishes = [context.Dishes[0], context.Dishes[1]]
            },
            new()
            {
                Id = 2,
                ClientId = 2,
                Client = context.Clients[1],
                RestaurantId = 2,
                Restaurant = context.Restaurants[1],
                OrderDateTime = new DateTime(2026, 9, 1, 13, 20, 0),
                DeliveryDateTime = new DateTime(2026, 9, 1, 13, 55, 0),
                TotalPrice = 580m,
                Dishes = [context.Dishes[2], context.Dishes[3]]
            },
            new()
            {
                Id = 3,
                ClientId = 3,
                Client = context.Clients[2],
                RestaurantId = 3,
                Restaurant = context.Restaurants[2],
                OrderDateTime = new DateTime(2026, 9, 1, 14, 0, 0),
                DeliveryDateTime = new DateTime(2026, 9, 1, 14, 40, 0),
                TotalPrice = 650m,
                Dishes = [context.Dishes[4]]
            },
            new()
            {
                Id = 4,
                ClientId = 4,
                Client = context.Clients[3],
                RestaurantId = 4,
                Restaurant = context.Restaurants[3],
                OrderDateTime = new DateTime(2026, 9, 1, 15, 15, 0),
                DeliveryDateTime = new DateTime(2026, 9, 1, 15, 50, 0),
                TotalPrice = 1260m,
                Dishes = [context.Dishes[6], context.Dishes[7]]
            },
            new()
            {
                Id = 5,
                ClientId = 5,
                Client = context.Clients[4],
                RestaurantId = 5,
                Restaurant = context.Restaurants[4],
                OrderDateTime = new DateTime(2026, 9, 2, 12, 30, 0),
                DeliveryDateTime = new DateTime(2026, 9, 2, 13, 20, 0),
                TotalPrice = 1740m,
                Dishes = [context.Dishes[8], context.Dishes[9]]
            },
            new()
            {
                Id = 6,
                ClientId = 6,
                Client = context.Clients[5],
                RestaurantId = 6,
                Restaurant = context.Restaurants[5],
                OrderDateTime = new DateTime(2026, 9, 2, 13, 0, 0),
                DeliveryDateTime = new DateTime(2026, 9, 2, 13, 45, 0),
                TotalPrice = 850m,
                Dishes = [context.Dishes[10]]
            },
            new()
            {
                Id = 7,
                ClientId = 7,
                Client = context.Clients[6],
                RestaurantId = 7,
                Restaurant = context.Restaurants[6],
                OrderDateTime = new DateTime(2026, 9, 2, 14, 10, 0),
                DeliveryDateTime = new DateTime(2026, 9, 2, 14, 50, 0),
                TotalPrice = 620m,
                Dishes = [context.Dishes[11]]
            },
            new()
            {
                Id = 8,
                ClientId = 8,
                Client = context.Clients[7],
                RestaurantId = 8,
                Restaurant = context.Restaurants[7],
                OrderDateTime = new DateTime(2026, 9, 2, 15, 0, 0),
                DeliveryDateTime = new DateTime(2026, 9, 2, 15, 35, 0),
                TotalPrice = 390m,
                Dishes = [context.Dishes[12]]
            },
            new()
            {
                Id = 9,
                ClientId = 9,
                Client = context.Clients[8],
                RestaurantId = 9,
                Restaurant = context.Restaurants[8],
                OrderDateTime = new DateTime(2026, 9, 3, 11, 30, 0),
                DeliveryDateTime = new DateTime(2026, 9, 3, 12, 10, 0),
                TotalPrice = 580m,
                Dishes = [context.Dishes[13]]
            },
            new()
            {
                Id = 10,
                ClientId = 10,
                Client = context.Clients[9],
                RestaurantId = 10,
                Restaurant = context.Restaurants[9],
                OrderDateTime = new DateTime(2026, 9, 3, 12, 0, 0),
                DeliveryDateTime = new DateTime(2026, 9, 3, 12, 30, 0),
                TotalPrice = 360m,
                Dishes = [context.Dishes[14]]
            },
            new()
            {
                Id = 11,
                ClientId = 1,
                Client = context.Clients[0],
                RestaurantId = 1,
                Restaurant = context.Restaurants[0],
                OrderDateTime = new DateTime(2026, 9, 3, 13, 0, 0),
                DeliveryDateTime = new DateTime(2026, 9, 3, 13, 25, 0),
                TotalPrice = 550m,
                Dishes = [context.Dishes[0]]
            },
            new()
            {
                Id = 12,
                ClientId = 2,
                Client = context.Clients[1],
                RestaurantId = 2,
                Restaurant = context.Restaurants[1],
                OrderDateTime = new DateTime(2026, 9, 3, 14, 0, 0),
                DeliveryDateTime = new DateTime(2026, 9, 3, 14, 30, 0),
                TotalPrice = 390m,
                Dishes = [context.Dishes[2]]
            },
            new()
            {
                Id = 13,
                ClientId = 3,
                Client = context.Clients[2],
                RestaurantId = 3,
                Restaurant = context.Restaurants[2],
                OrderDateTime = new DateTime(2026, 9, 4, 12, 15, 0),
                DeliveryDateTime = new DateTime(2026, 9, 4, 12, 50, 0),
                TotalPrice = 1240m,
                Dishes = [context.Dishes[4], context.Dishes[5]]
            },
            new()
            {
                Id = 14,
                ClientId = 4,
                Client = context.Clients[3],
                RestaurantId = 4,
                Restaurant = context.Restaurants[3],
                OrderDateTime = new DateTime(2026, 9, 4, 13, 30, 0),
                DeliveryDateTime = new DateTime(2026, 9, 4, 14, 20, 0),
                TotalPrice = 720m,
                Dishes = [context.Dishes[6]]
            },
            new()
            {
                Id = 15,
                ClientId = 5,
                Client = context.Clients[4],
                RestaurantId = 5,
                Restaurant = context.Restaurants[4],
                OrderDateTime = new DateTime(2026, 9, 4, 15, 0, 0),
                DeliveryDateTime = new DateTime(2026, 9, 4, 15, 45, 0),
                TotalPrice = 1250m,
                Dishes = [context.Dishes[8]]
            },
            new()
            {
                Id = 16,
                ClientId = 6,
                Client = context.Clients[5],
                RestaurantId = 1,
                Restaurant = context.Restaurants[0],
                OrderDateTime = new DateTime(2026, 9, 5, 12, 30, 0),
                DeliveryDateTime = new DateTime(2026, 9, 5, 13, 5, 0),
                TotalPrice = 970m,
                Dishes = [context.Dishes[0], context.Dishes[1]]
            },
            new()
            {
                Id = 17,
                ClientId = 7,
                Client = context.Clients[6],
                RestaurantId = 2,
                Restaurant = context.Restaurants[1],
                OrderDateTime = new DateTime(2026, 9, 5, 13, 15, 0),
                DeliveryDateTime = new DateTime(2026, 9, 5, 13, 50, 0),
                TotalPrice = 580m,
                Dishes = [context.Dishes[2], context.Dishes[3]]
            }
        ]);

        return context;
    }
}