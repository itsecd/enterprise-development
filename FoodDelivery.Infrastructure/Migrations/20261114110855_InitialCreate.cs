using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FoodDelivery.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    DeliveryAddress = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Address = table.Column<string>(type: "character varying(300)", maxLength: 300, nullable: false),
                    Rating = table.Column<double>(type: "double precision", nullable: false),
                    OpenTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    CloseTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dishes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                    Weight = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    RestaurantId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dishes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Dishes_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Dishes_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    RestaurantId = table.Column<int>(type: "integer", nullable: false),
                    OrderDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DeliveryDateTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDishes",
                columns: table => new
                {
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    DishId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDishes", x => new { x.OrderId, x.DishId });
                    table.ForeignKey(
                        name: "FK_OrderDishes_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDishes_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Пицца" },
                    { 2, "Суши" },
                    { 3, "Бургеры" },
                    { 4, "Салаты" },
                    { 5, "Супы" },
                    { 6, "Горячие блюда" },
                    { 7, "Десерты" },
                    { 8, "Напитки" },
                    { 9, "Закуски" },
                    { 10, "Соусы" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "DeliveryAddress", "FullName", "Phone" },
                values: new object[,]
                {
                    { 1, "г. Самара, ул. Ленина, д. 10", "Зубенко Михаил Петрович", "+79277777777" },
                    { 2, "г. Самара, ул. Гагарина, д. 15", "Дылда Ольга Олеговна", "+79270001111" },
                    { 3, "г. Самара, ул. Гаражная, д. 21", "Заболотный Антон Иванович", "+79273333333" },
                    { 4, "г. Самара, ул. Гая, д. 8", "Скорлупа Иннокентий Сергеевич", "+79270101010" },
                    { 5, "г. Самара, ул. Революционная, д. 32", "Иванов Иван Иванович", "+79270202020" },
                    { 6, "г. Самара, ул. Ново-Садовая, д. 14", "Петушков Илья Викторович", "+7270000001" },
                    { 7, "г. Самара, ул. Стара-Загора, д. 5", "Костыль Ирина Петровна", "+79371004554" },
                    { 8, "г. Самара, ул. Дыбенко, д. 19", "Петров Петр Петрович", "+7271232323" },
                    { 9, "г. Самара, ул. Гаражная, д. 27", "Плотников Иван Иванович", "+79276547656" },
                    { 10, "г. Самара, ул. Мяги, д. 11", "Колодин Алексей Сергеевич", "+7271044141" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "CloseTime", "Name", "OpenTime", "Rating" },
                values: new object[,]
                {
                    { 1, "г. Самара, ул. Аэродромная, д. 25", new TimeOnly(23, 0, 0), "Ростикс", new TimeOnly(9, 0, 0), 4.7999999999999998 },
                    { 2, "г. Самара, ул. Дыбенко, д. 30", new TimeOnly(23, 0, 0), "Вкусно и точка", new TimeOnly(8, 0, 0), 4.5 },
                    { 3, "г. Самара, ул. Гагарина, д. 42", new TimeOnly(23, 0, 0), "Фуджи", new TimeOnly(10, 0, 0), 4.5999999999999996 },
                    { 4, "г. Самара, ул. Стара-Загора, д. 35", new TimeOnly(23, 0, 0), "Тануки", new TimeOnly(11, 0, 0), 4.7999999999999998 },
                    { 5, "г. Самара, ул. Дыбенко, д. 12", new TimeOnly(23, 15, 0), "Комитет", new TimeOnly(10, 0, 0), 4.9000000000000004 },
                    { 6, "г. Самара, ул. Революционная, д. 51", new TimeOnly(22, 30, 0), "Шашлычная", new TimeOnly(9, 0, 0), 4.2999999999999998 },
                    { 7, "г. Самара, ул. Ново-Садовая, д. 76", new TimeOnly(22, 0, 0), "Перчини", new TimeOnly(8, 30, 0), 4.5999999999999996 },
                    { 8, "г. Самара, ул. Пушкина, д. 14", new TimeOnly(23, 0, 0), "Дом Нино", new TimeOnly(11, 0, 0), 4.9000000000000004 },
                    { 9, "г. Самара, ул. Мяги, д. 28", new TimeOnly(23, 0, 0), "Супра", new TimeOnly(10, 0, 0), 4.9000000000000004 },
                    { 10, "г. Самара, ул. Куйбышева, д. 10", new TimeOnly(18, 0, 0), "Кафе Чаша", new TimeOnly(8, 0, 0), 4.5 }
                });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "CategoryId", "Name", "Price", "RestaurantId", "Weight" },
                values: new object[,]
                {
                    { 1, 1, "Пицца Маргарита", 550m, 1, 450 },
                    { 2, 3, "Чикенбургер", 420m, 1, 300 },
                    { 3, 3, "Бургер классический", 390m, 2, 320 },
                    { 4, 9, "Картофель фри", 190m, 2, 180 },
                    { 5, 2, "Филадельфия", 650m, 3, 280 },
                    { 6, 2, "Калифорния", 590m, 3, 260 },
                    { 7, 5, "Том Ям", 720m, 4, 400 },
                    { 8, 6, "Лапша с курицей", 540m, 4, 350 },
                    { 9, 6, "Стейк из говядины", 1250m, 5, 350 },
                    { 10, 4, "Салат Цезарь", 490m, 5, 250 },
                    { 11, 6, "Шашлык из свинины", 850m, 6, 400 },
                    { 12, 6, "Паста Карбонара", 620m, 7, 350 },
                    { 13, 7, "Тирамису", 390m, 8, 180 },
                    { 14, 6, "Хачапури", 580m, 9, 400 },
                    { 15, 5, "Борщ", 360m, 10, 350 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "ClientId", "DeliveryDateTime", "OrderDateTime", "RestaurantId", "TotalPrice" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 9, 1, 12, 45, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 12, 10, 0, 0, DateTimeKind.Utc), 1, 970m },
                    { 2, 2, new DateTime(2026, 9, 1, 13, 55, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 13, 20, 0, 0, DateTimeKind.Utc), 2, 580m },
                    { 3, 3, new DateTime(2026, 9, 1, 14, 40, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 14, 0, 0, 0, DateTimeKind.Utc), 3, 650m },
                    { 4, 4, new DateTime(2026, 9, 1, 15, 50, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 15, 15, 0, 0, DateTimeKind.Utc), 4, 1260m },
                    { 5, 5, new DateTime(2026, 9, 2, 13, 20, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 2, 12, 30, 0, 0, DateTimeKind.Utc), 5, 1740m },
                    { 6, 6, new DateTime(2026, 9, 2, 13, 45, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 2, 13, 0, 0, 0, DateTimeKind.Utc), 6, 850m },
                    { 7, 7, new DateTime(2026, 9, 2, 14, 50, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 2, 14, 10, 0, 0, DateTimeKind.Utc), 7, 620m },
                    { 8, 8, new DateTime(2026, 9, 2, 15, 35, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 2, 15, 0, 0, 0, DateTimeKind.Utc), 8, 390m },
                    { 9, 9, new DateTime(2026, 9, 3, 12, 10, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 3, 11, 30, 0, 0, DateTimeKind.Utc), 9, 580m },
                    { 10, 10, new DateTime(2026, 9, 3, 12, 30, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 3, 12, 0, 0, 0, DateTimeKind.Utc), 10, 360m },
                    { 11, 1, new DateTime(2026, 9, 3, 13, 25, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 3, 13, 0, 0, 0, DateTimeKind.Utc), 1, 550m },
                    { 12, 2, new DateTime(2026, 9, 3, 14, 30, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 3, 14, 0, 0, 0, DateTimeKind.Utc), 2, 390m },
                    { 13, 3, new DateTime(2026, 9, 4, 12, 50, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 4, 12, 15, 0, 0, DateTimeKind.Utc), 3, 1240m },
                    { 14, 4, new DateTime(2026, 9, 4, 14, 20, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 4, 13, 30, 0, 0, DateTimeKind.Utc), 4, 720m },
                    { 15, 5, new DateTime(2026, 9, 4, 15, 45, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 4, 15, 0, 0, 0, DateTimeKind.Utc), 5, 1250m },
                    { 16, 6, new DateTime(2026, 9, 5, 13, 5, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 5, 12, 30, 0, 0, DateTimeKind.Utc), 1, 970m },
                    { 17, 7, new DateTime(2026, 9, 5, 13, 50, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 5, 13, 15, 0, 0, DateTimeKind.Utc), 2, 580m }
                });

            migrationBuilder.InsertData(
                table: "OrderDishes",
                columns: new[] { "DishId", "OrderId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 2 },
                    { 5, 3 },
                    { 7, 4 },
                    { 8, 4 },
                    { 9, 5 },
                    { 10, 5 },
                    { 11, 6 },
                    { 12, 7 },
                    { 13, 8 },
                    { 14, 9 },
                    { 15, 10 },
                    { 1, 11 },
                    { 3, 12 },
                    { 5, 13 },
                    { 6, 13 },
                    { 7, 14 },
                    { 9, 15 },
                    { 1, 16 },
                    { 2, 16 },
                    { 3, 17 },
                    { 4, 17 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_CategoryId",
                table: "Dishes",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Dishes_RestaurantId",
                table: "Dishes",
                column: "RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDishes_DishId",
                table: "OrderDishes",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RestaurantId",
                table: "Orders",
                column: "RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDishes");

            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
