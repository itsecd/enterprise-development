using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodDelivery.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LastName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Patronymic = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    DeliveryAddress = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false),
                    Rating = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    OpeningTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    ClosingTime = table.Column<TimeSpan>(type: "time(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restaurants", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "dishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_dishes_categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dishes_restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DeliveredAt = table.Column<DateTime>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(type: "int", nullable: false),
                    DishId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_order_items_dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_order_items_orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Пицца" },
                    { 2, "Бургеры" },
                    { 3, "Суши" },
                    { 4, "Салаты" },
                    { 5, "Супы" },
                    { 6, "Паста" },
                    { 7, "Десерты" },
                    { 8, "Напитки" },
                    { 9, "Завтраки" },
                    { 10, "Закуски" }
                });

            migrationBuilder.InsertData(
                table: "customers",
                columns: new[] { "Id", "DeliveryAddress", "FirstName", "LastName", "Patronymic", "Phone" },
                values: new object[,]
                {
                    { 1, "ул. Садовая, 1", "Иван", "Иванов", "Петрович", "+79000000001" },
                    { 2, "ул. Молодогвардейская, 12", "Алексей", "Петров", "Иванович", "+79000000002" },
                    { 3, "ул. Гагарина, 20", "Мария", "Сидорова", "Александровна", "+79000000003" },
                    { 4, "ул. Ленинградская, 45", "Дмитрий", "Кузнецов", "Сергеевич", "+79000000004" },
                    { 5, "ул. Победы, 8", "Анна", "Смирнова", "Викторовна", "+79000000005" },
                    { 6, "ул. Осипенко, 15", "Максим", "Попов", "Олегович", "+79000000006" },
                    { 7, "ул. Фрунзе, 30", "Елена", "Волкова", "Игоревна", "+79000000007" },
                    { 8, "ул. Куйбышева, 17", "Андрей", "Морозов", "Николаевич", "+79000000008" },
                    { 9, "ул. Советской Армии, 9", "Ольга", "Орлова", "Павловна", "+79000000009" },
                    { 10, "ул. Авроры, 14", "Роман", "Никитин", "Андреевич", "+79000000010" },
                    { 11, "ул. Самарская, 21", "Дарья", "Федорова", "Ильинична", "+79000000011" },
                    { 12, "ул. Полевая, 4", "Николай", "Лебедев", "Васильевич", "+79000000012" },
                    { 13, "ул. Ново-Садовая, 36", "София", "Громова", "Романовна", "+79000000013" },
                    { 14, "ул. Полевой проезд, 11", "Евгений", "Зайцев", "Михайлович", "+79000000014" },
                    { 15, "ул. Мичурина, 19", "Ксения", "Белова", "Олеговна", "+79000000015" }
                });

            migrationBuilder.InsertData(
                table: "restaurants",
                columns: new[] { "Id", "Address", "ClosingTime", "Name", "OpeningTime", "Rating" },
                values: new object[,]
                {
                    { 1, "ул. Ленинградская, 10", new TimeSpan(0, 23, 0, 0, 0), "Вкусно рядом", new TimeSpan(0, 9, 0, 0, 0), 4.8m },
                    { 2, "ул. Молодогвардейская, 25", new TimeSpan(0, 22, 0, 0, 0), "Дом еды", new TimeSpan(0, 10, 0, 0, 0), 4.6m },
                    { 3, "ул. Победы, 15", new TimeSpan(0, 23, 0, 0, 0), "Городская кухня", new TimeSpan(0, 9, 0, 0, 0), 4.5m },
                    { 4, "ул. Гагарина, 42", new TimeSpan(0, 0, 0, 0, 0), "Пицца Хаус", new TimeSpan(0, 10, 0, 0, 0), 4.4m },
                    { 5, "ул. Осипенко, 18", new TimeSpan(0, 23, 0, 0, 0), "Суши Тайм", new TimeSpan(0, 11, 0, 0, 0), 4.7m },
                    { 6, "ул. Московское шоссе, 5", new TimeSpan(0, 1, 0, 0, 0), "Бургер Бар", new TimeSpan(0, 10, 0, 0, 0), 4.3m },
                    { 7, "ул. Советской Армии, 31", new TimeSpan(0, 22, 0, 0, 0), "Семейный ресторан", new TimeSpan(0, 9, 0, 0, 0), 4.2m },
                    { 8, "ул. Куйбышева, 12", new TimeSpan(0, 22, 0, 0, 0), "Кафе Центр", new TimeSpan(0, 8, 0, 0, 0), 4.1m },
                    { 9, "ул. Фрунзе, 7", new TimeSpan(0, 23, 0, 0, 0), "Итальянский дворик", new TimeSpan(0, 11, 0, 0, 0), 4.6m },
                    { 10, "ул. Авроры, 20", new TimeSpan(0, 23, 0, 0, 0), "Азия Фуд", new TimeSpan(0, 10, 0, 0, 0), 4.5m }
                });

            migrationBuilder.InsertData(
                table: "dishes",
                columns: new[] { "Id", "CategoryId", "Name", "Price", "RestaurantId", "Weight" },
                values: new object[,]
                {
                    { 1, 1, "Маргарита", 650m, 4, 500 },
                    { 2, 1, "Пепперони", 750m, 4, 550 },
                    { 3, 1, "Четыре сыра", 800m, 4, 520 },
                    { 4, 2, "Классический бургер", 500m, 6, 350 },
                    { 5, 2, "Чизбургер", 550m, 6, 370 },
                    { 6, 2, "Двойной бургер", 700m, 6, 450 },
                    { 7, 3, "Филадельфия", 650m, 5, 280 },
                    { 8, 3, "Калифорния", 600m, 5, 300 },
                    { 9, 3, "Темпура", 700m, 5, 320 },
                    { 10, 4, "Цезарь", 450m, 1, 250 },
                    { 11, 4, "Греческий салат", 400m, 1, 240 },
                    { 12, 4, "Овощной салат", 350m, 1, 220 },
                    { 13, 5, "Куриный суп", 350m, 3, 400 },
                    { 14, 5, "Солянка", 420m, 3, 400 },
                    { 15, 5, "Том Ям", 650m, 3, 450 },
                    { 16, 6, "Карбонара", 600m, 9, 400 },
                    { 17, 6, "Болоньезе", 650m, 9, 420 },
                    { 18, 6, "Альфредо", 620m, 9, 400 },
                    { 19, 7, "Чизкейк", 350m, 8, 180 },
                    { 20, 7, "Тирамису", 400m, 8, 180 },
                    { 21, 7, "Шоколадный торт", 450m, 8, 200 },
                    { 22, 8, "Кола", 150m, 2, 500 },
                    { 23, 8, "Морс", 120m, 2, 500 },
                    { 24, 8, "Лимонад", 180m, 2, 500 },
                    { 25, 9, "Омлет", 350m, 7, 300 },
                    { 26, 9, "Сырники", 400m, 7, 250 },
                    { 27, 9, "Каша", 300m, 7, 300 },
                    { 28, 10, "Наггетсы", 350m, 10, 250 },
                    { 29, 10, "Картофель фри", 250m, 10, 200 },
                    { 30, 10, "Луковые кольца", 300m, 10, 200 }
                });

            migrationBuilder.InsertData(
                table: "orders",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DeliveredAt", "RestaurantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 9, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 9, 1, 12, 30, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2026, 9, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2026, 9, 2, 12, 25, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, new DateTime(2026, 9, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2026, 9, 3, 12, 45, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, new DateTime(2026, 9, 4, 12, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2026, 9, 4, 12, 20, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, new DateTime(2026, 9, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2026, 9, 5, 12, 35, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 6, new DateTime(2026, 9, 6, 12, 0, 0, 0, DateTimeKind.Unspecified), 6, new DateTime(2026, 9, 6, 12, 40, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 7, new DateTime(2026, 9, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 7, new DateTime(2026, 9, 7, 12, 30, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 8, new DateTime(2026, 9, 8, 12, 0, 0, 0, DateTimeKind.Unspecified), 8, new DateTime(2026, 9, 8, 12, 25, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 9, new DateTime(2026, 9, 9, 12, 0, 0, 0, DateTimeKind.Unspecified), 9, new DateTime(2026, 9, 9, 12, 35, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 10, new DateTime(2026, 9, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), 10, new DateTime(2026, 9, 10, 12, 45, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 11, new DateTime(2026, 9, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), 11, new DateTime(2026, 9, 11, 12, 30, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 12, new DateTime(2026, 9, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), 12, new DateTime(2026, 9, 12, 12, 20, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 13, new DateTime(2026, 9, 13, 12, 0, 0, 0, DateTimeKind.Unspecified), 13, new DateTime(2026, 9, 13, 12, 40, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 14, new DateTime(2026, 9, 14, 12, 0, 0, 0, DateTimeKind.Unspecified), 14, new DateTime(2026, 9, 14, 12, 25, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 15, new DateTime(2026, 9, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), 15, new DateTime(2026, 9, 15, 12, 30, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 16, new DateTime(2026, 9, 16, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 9, 16, 12, 35, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 17, new DateTime(2026, 9, 17, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2026, 9, 17, 12, 25, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 18, new DateTime(2026, 9, 18, 12, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2026, 9, 18, 12, 30, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 19, new DateTime(2026, 9, 19, 12, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2026, 9, 19, 12, 20, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 20, new DateTime(2026, 9, 20, 12, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2026, 9, 20, 12, 40, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 21, new DateTime(2026, 9, 21, 12, 0, 0, 0, DateTimeKind.Unspecified), 6, new DateTime(2026, 9, 21, 12, 35, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 22, new DateTime(2026, 9, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), 7, new DateTime(2026, 9, 22, 12, 30, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 23, new DateTime(2026, 9, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), 8, new DateTime(2026, 9, 23, 12, 25, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 24, new DateTime(2026, 9, 24, 12, 0, 0, 0, DateTimeKind.Unspecified), 9, new DateTime(2026, 9, 24, 12, 20, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 25, new DateTime(2026, 9, 25, 12, 0, 0, 0, DateTimeKind.Unspecified), 10, new DateTime(2026, 9, 25, 12, 30, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 26, new DateTime(2026, 9, 26, 12, 0, 0, 0, DateTimeKind.Unspecified), 11, new DateTime(2026, 9, 26, 12, 25, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 27, new DateTime(2026, 9, 27, 12, 0, 0, 0, DateTimeKind.Unspecified), 12, new DateTime(2026, 9, 27, 12, 35, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 28, new DateTime(2026, 9, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), 13, new DateTime(2026, 9, 28, 12, 30, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 29, new DateTime(2026, 9, 29, 12, 0, 0, 0, DateTimeKind.Unspecified), 14, new DateTime(2026, 9, 29, 12, 25, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 30, new DateTime(2026, 9, 30, 12, 0, 0, 0, DateTimeKind.Unspecified), 15, new DateTime(2026, 9, 30, 12, 30, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 31, new DateTime(2026, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 10, 1, 12, 25, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 32, new DateTime(2026, 10, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2026, 10, 2, 12, 20, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 33, new DateTime(2026, 10, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2026, 10, 3, 12, 30, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 34, new DateTime(2026, 10, 4, 12, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2026, 10, 4, 12, 40, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 35, new DateTime(2026, 10, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2026, 10, 5, 12, 35, 0, 0, DateTimeKind.Unspecified), 9 },
                    { 36, new DateTime(2026, 10, 6, 12, 0, 0, 0, DateTimeKind.Unspecified), 6, new DateTime(2026, 10, 6, 12, 25, 0, 0, DateTimeKind.Unspecified), 10 },
                    { 37, new DateTime(2026, 10, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 7, new DateTime(2026, 10, 7, 12, 30, 0, 0, DateTimeKind.Unspecified), 10 },
                    { 38, new DateTime(2026, 10, 8, 12, 0, 0, 0, DateTimeKind.Unspecified), 8, new DateTime(2026, 10, 8, 12, 20, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 39, new DateTime(2026, 10, 9, 12, 0, 0, 0, DateTimeKind.Unspecified), 9, new DateTime(2026, 10, 9, 12, 25, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 40, new DateTime(2026, 10, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), 10, new DateTime(2026, 10, 10, 12, 35, 0, 0, DateTimeKind.Unspecified), 6 }
                });

            migrationBuilder.InsertData(
                table: "order_items",
                columns: new[] { "Id", "DishId", "OrderId", "Quantity", "UnitPrice" },
                values: new object[,]
                {
                    { 1, 10, 1, 1, 450m },
                    { 2, 11, 1, 1, 400m },
                    { 3, 12, 2, 1, 350m },
                    { 4, 10, 3, 1, 450m },
                    { 5, 11, 4, 1, 400m },
                    { 6, 12, 4, 1, 350m },
                    { 7, 10, 5, 1, 450m },
                    { 8, 12, 5, 1, 350m },
                    { 9, 11, 6, 1, 400m },
                    { 10, 22, 7, 1, 150m },
                    { 11, 23, 8, 1, 120m },
                    { 12, 24, 8, 1, 180m },
                    { 13, 24, 9, 1, 180m },
                    { 14, 22, 10, 1, 150m },
                    { 15, 23, 10, 1, 120m },
                    { 16, 24, 11, 1, 180m },
                    { 17, 13, 12, 1, 350m },
                    { 18, 14, 13, 1, 420m },
                    { 19, 15, 13, 1, 650m },
                    { 20, 15, 14, 1, 650m },
                    { 21, 13, 15, 1, 350m },
                    { 22, 14, 15, 1, 420m },
                    { 23, 15, 16, 1, 650m },
                    { 24, 1, 17, 1, 650m },
                    { 25, 2, 18, 1, 750m },
                    { 26, 3, 18, 1, 800m },
                    { 27, 3, 19, 1, 800m },
                    { 28, 1, 20, 1, 650m },
                    { 29, 2, 20, 1, 750m },
                    { 30, 7, 21, 1, 650m },
                    { 31, 8, 22, 1, 600m },
                    { 32, 9, 22, 1, 700m },
                    { 33, 9, 23, 1, 700m },
                    { 34, 7, 24, 1, 650m },
                    { 35, 8, 24, 1, 600m },
                    { 36, 9, 25, 1, 700m },
                    { 37, 4, 26, 1, 500m },
                    { 38, 5, 27, 1, 550m },
                    { 39, 6, 27, 1, 700m },
                    { 40, 6, 28, 1, 700m },
                    { 41, 4, 29, 1, 500m },
                    { 42, 5, 29, 1, 550m },
                    { 43, 25, 30, 1, 350m },
                    { 44, 26, 31, 1, 400m },
                    { 45, 27, 31, 1, 300m },
                    { 46, 19, 32, 1, 350m },
                    { 47, 20, 32, 1, 400m },
                    { 48, 21, 33, 1, 450m },
                    { 49, 16, 34, 1, 600m },
                    { 50, 17, 34, 1, 650m },
                    { 51, 18, 35, 1, 620m },
                    { 52, 28, 36, 1, 350m },
                    { 53, 29, 37, 1, 250m },
                    { 54, 30, 37, 1, 300m },
                    { 55, 2, 38, 1, 750m },
                    { 56, 8, 39, 1, 600m },
                    { 57, 5, 40, 1, 550m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_dishes_CategoryId",
                table: "dishes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_dishes_RestaurantId",
                table: "dishes",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_DishId",
                table: "order_items",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_OrderId",
                table: "order_items",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_CustomerId",
                table: "orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_RestaurantId",
                table: "orders",
                column: "RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_items");

            migrationBuilder.DropTable(
                name: "dishes");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "categories");

            migrationBuilder.DropTable(
                name: "customers");

            migrationBuilder.DropTable(
                name: "restaurants");
        }
    }
}
