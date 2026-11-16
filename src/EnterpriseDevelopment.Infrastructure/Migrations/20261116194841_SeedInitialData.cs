using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EnterpriseDevelopment.Infrastructure.Migrations;

/// <summary>
/// Миграция первоначального заполнения базы данных
/// </summary>
public partial class SeedInitialData : Migration
{
    /// <summary>
    /// Добавляет первоначальные данные
    /// </summary>
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Категории блюд
        migrationBuilder.InsertData(
            table: "DishCategories",
            columns: ["Id", "Name"],
            values: new object[,]
            {
                { 1, "Пицца" },
                { 2, "Суши" },
                { 3, "Бургеры" },
                { 4, "Супы" },
                { 5, "Салаты" },
                { 6, "Десерты" },
                { 7, "Напитки" },
                { 8, "Закуски" },
                { 9, "Паста" },
                { 10, "Горячие блюда" }
            });

        // Рестораны
        migrationBuilder.InsertData(
            table: "Restaurants",
            columns:
            [
                "Id", "Name", "Address", "Rating",
                "OpeningTime", "ClosingTime"
            ],
            values: new object[,]
            {
                { 1, "Сицилия", "ул. Гагарина, 10", 4.7, new TimeOnly(10, 0), new TimeOnly(23, 0) },
                { 2, "Токио", "ул. Московская, 25", 4.6, new TimeOnly(11, 0), new TimeOnly(23, 30) },
                { 3, "Burger House", "ул. Победы, 15", 4.4, new TimeOnly(9, 0), new TimeOnly(22, 0) },
                { 4, "Хачапури", "ул. Ленинградская, 40", 4.8, new TimeOnly(10, 0), new TimeOnly(23, 0) },
                { 5, "Вилка", "ул. Советской Армии, 30", 4.3, new TimeOnly(8, 0), new TimeOnly(21, 0) },
                { 6, "Паста Бар", "ул. Самарская, 81", 4.5, new TimeOnly(11, 0), new TimeOnly(23, 0) },
                { 7, "Шашлык №1", "ул. Победы, 90", 4.2, new TimeOnly(10, 0), new TimeOnly(22, 0) },
                { 8, "Green Food", "ул. Осипенко, 12", 4.6, new TimeOnly(9, 0), new TimeOnly(21, 0) },
                { 9, "Sweet Home", "ул. Полевая, 55", 4.9, new TimeOnly(9, 0), new TimeOnly(22, 0) },
                { 10, "Горячий двор", "ул. Авроры, 100", 4.1, new TimeOnly(10, 0), new TimeOnly(23, 0) }
            });

        // Клиенты
        migrationBuilder.InsertData(
            table: "Clients",
            columns:
            [
                "Id", "FullName", "PhoneNumber",
                "DeliveryAddress"
            ],
            values: new object[,]
            {
                { 1, "Иванов Иван Иванович", "+79991234501", "ул. Гагарина, 10" },
                { 2, "Петров Пётр Сергеевич", "+79991234502", "ул. Московская, 25" },
                { 3, "Сидорова Анна Андреевна", "+79991234503", "ул. Победы, 15" },
                { 4, "Кузнецов Алексей Олегович", "+79991234504", "ул. Ленинградская, 40" },
                { 5, "Смирнова Екатерина Павловна", "+79991234505", "ул. Советской Армии, 30" },
                { 6, "Попов Дмитрий Игоревич", "+79991234506", "ул. Самарская, 81" },
                { 7, "Соколова Мария Олеговна", "+79991234507", "ул. Осипенко, 12" },
                { 8, "Морозов Артём Сергеевич", "+79991234508", "ул. Полевая, 55" },
                { 9, "Волкова Алина Романовна", "+79991234509", "ул. Авроры, 100" },
                { 10, "Орлов Максим Евгеньевич", "+79991234510", "Московское шоссе, 20" }
            });

        // Блюда
        migrationBuilder.InsertData(
            table: "Dishes",
            columns:
            [
                "Id", "Name", "Weight", "Price",
                "CategoryId", "RestaurantId"
            ],
            values: new object[,]
            {
                { 1, "Пепперони", 450, 520m, 1, 1 },
                { 2, "Маргарита", 420, 450m, 1, 1 },
                { 3, "Филадельфия", 260, 650m, 2, 2 },
                { 4, "Калифорния", 240, 590m, 2, 2 },
                { 5, "Чизбургер", 320, 390m, 3, 3 },
                { 6, "Хачапури по-аджарски", 400, 480m, 10, 4 },
                { 7, "Борщ", 350, 300m, 4, 5 },
                { 8, "Карбонара", 330, 510m, 9, 6 },
                { 9, "Шашлык из свинины", 300, 560m, 10, 7 },
                { 10, "Цезарь", 250, 420m, 5, 8 },
                { 11, "Чизкейк", 180, 350m, 6, 9 },
                { 12, "Лимонад", 500, 230m, 7, 10 }
            });

        // Заказы
        migrationBuilder.InsertData(
            table: "Orders",
            columns:
            [
                "Id", "ClientId", "RestaurantId",
                "CreatedAt", "DeliveredAt"
            ],
            values: new object[,]
            {
                { 1, 1, 1, Utc(1, 12, 0), Utc(1, 12, 30) },
                { 2, 2, 1, Utc(2, 13, 0), Utc(2, 13, 25) },
                { 3, 3, 1, Utc(3, 18, 0), Utc(3, 18, 20) },
                { 4, 1, 1, Utc(4, 19, 0), Utc(4, 19, 20) },
                { 5, 4, 2, Utc(5, 12, 0), Utc(5, 12, 40) },
                { 6, 5, 2, Utc(6, 14, 0), Utc(6, 14, 35) },
                { 7, 1, 2, Utc(7, 18, 0), Utc(7, 18, 45) },
                { 8, 6, 3, Utc(8, 16, 0), Utc(8, 16, 15) },
                { 9, 7, 3, Utc(9, 16, 0), Utc(9, 16, 30) },
                { 10, 1, 3, Utc(10, 16, 0), Utc(10, 16, 15) },
                { 11, 8, 4, Utc(11, 12, 0), Utc(11, 12, 25) },
                { 12, 9, 5, Utc(12, 13, 0), Utc(12, 13, 40) }
            });

        // Позиции заказов с количеством блюд
        migrationBuilder.InsertData(
            table: "OrderItems",
            columns: ["OrderId", "DishId", "Quantity"],
            values: new object[,]
            {
                { 1, 1, 1 },
                { 1, 2, 1 },
                { 2, 2, 2 },
                { 3, 1, 2 },
                { 3, 2, 1 },
                { 4, 1, 1 },
                { 4, 2, 2 },
                { 5, 3, 1 },
                { 5, 4, 1 },
                { 6, 3, 2 },
                { 7, 3, 1 },
                { 7, 4, 2 },
                { 8, 5, 2 },
                { 9, 5, 1 },
                { 10, 5, 3 },
                { 11, 6, 2 },
                { 12, 7, 2 }
            });

        // Обновление последовательностей идентификаторов
        foreach (var table in new[]
        {
            "Clients",
            "Restaurants",
            "DishCategories",
            "Dishes",
            "Orders"
        })
        {
            migrationBuilder.Sql($"""
                SELECT setval(
                    pg_get_serial_sequence('"{table}"', 'Id'),
                    (SELECT MAX("Id") FROM "{table}")
                );
                """);
        }
    }

    /// <summary>
    /// Удаляет первоначальные данные
    /// </summary>
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "OrderItems",
            keyColumns: ["OrderId", "DishId"],
            keyValues: new object[,]
            {
                { 1, 1 }, { 1, 2 }, { 2, 2 },
                { 3, 1 }, { 3, 2 }, { 4, 1 },
                { 4, 2 }, { 5, 3 }, { 5, 4 },
                { 6, 3 }, { 7, 3 }, { 7, 4 },
                { 8, 5 }, { 9, 5 }, { 10, 5 },
                { 11, 6 }, { 12, 7 }
            });

        foreach (var table in new[]
        {
            "Orders", "Dishes", "Clients",
            "Restaurants", "DishCategories"
        })
        {
            migrationBuilder.Sql($"""
                DELETE FROM "{table}" WHERE "Id" BETWEEN 1 AND 12;
                """);
        }
    }

    private static DateTimeOffset Utc(int day, int hour, int minute)
    {
        return new DateTimeOffset(
            2026, 9, day, hour, minute, 0, TimeSpan.Zero);
    }
}
