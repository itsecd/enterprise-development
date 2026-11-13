using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Patronymic = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    DeliveryAddress = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    Rating = table.Column<double>(type: "double precision", nullable: false),
                    OpeningTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    ClosingTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
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
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    WeightGrams = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
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
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    RestaurantId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    DeliveredAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
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
                name: "OrderItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderId = table.Column<int>(type: "integer", nullable: false),
                    DishId = table.Column<int>(type: "integer", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Dishes_DishId",
                        column: x => x.DishId,
                        principalTable: "Dishes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_OrderId",
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
                    { 2, "Бургеры" },
                    { 3, "Закуски" },
                    { 4, "Салаты" },
                    { 5, "Супы" },
                    { 6, "Паста" },
                    { 7, "Роллы" },
                    { 8, "Десерты" },
                    { 9, "Напитки" },
                    { 10, "Завтраки" }
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "DeliveryAddress", "FirstName", "LastName", "Patronymic", "Phone" },
                values: new object[,]
                {
                    { 1, "Самара, ул. Ленина, д. 1", "Илья", "Мэдисон", "Сергеевич", "+79270000228" },
                    { 2, "Самара, ул. Пушкина, д. 10", "Кирилл", "Зырянов", null, "+79270000666" },
                    { 3, "Самара, ул. Гагарина, д. 15", "Иван", "Купитман", "Натанович", "+79080001337" },
                    { 4, "Самара, ул. Мира, д. 20", "Геннадий", "Бобков", "Андреевич", "+79040000066" },
                    { 5, "Самара, ул. Советская, д. 5", "Степан", "Джопс", "Алексеевич", "+79270000077" },
                    { 6, "Самара, ул. Центральная, д. 8", "Пётр", "Гриффин", null, "+79080001109" },
                    { 7, "Самара, ул. Школьная, д. 12", "Ольга", "Дупло", "Викторовна", "+79270000777" },
                    { 8, "Самара, ул. Молодёжная, д. 7", "Денис", "Петров", "Игоревич", "+79270000888" },
                    { 9, "Самара, ул. Новая, д. 18", "Екатерина", "Морозова", "Сергеевна", "+79270000089" },
                    { 10, "Самара, ул. Садовая, д. 25", "Никита", "Сопло", "Сергеевич", "+79270001010" }
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "ClosingTime", "Name", "OpeningTime", "Rating" },
                values: new object[,]
                {
                    { 1, "просп. Ленина, 1", new TimeOnly(23, 0, 0), "Додо Пицца", new TimeOnly(10, 0, 0), 4.7999999999999998 },
                    { 2, "ул. Тухачевского, 80", new TimeOnly(22, 0, 0), "Burger King", new TimeOnly(9, 0, 0), 4.5999999999999996 },
                    { 3, "ул. Коммунистическая, 17", new TimeOnly(23, 30, 0), "Tanuki", new TimeOnly(11, 0, 0), 4.9000000000000004 },
                    { 4, "ул. Гагарина, 66", new TimeOnly(21, 0, 0), "Salad Bar", new TimeOnly(9, 0, 0), 4.2999999999999998 },
                    { 5, "ул. Галактионовская, 133", new TimeOnly(22, 0, 0), "Мушмула", new TimeOnly(10, 0, 0), 4.5999999999999996 },
                    { 6, "ул. Дачная, 12", new TimeOnly(22, 0, 0), "Дари Дури", new TimeOnly(8, 0, 0), 4.4000000000000004 },
                    { 7, "ул. Полевая, 28", new TimeOnly(21, 0, 0), "Surf Coffee", new TimeOnly(8, 0, 0), 4.7999999999999998 },
                    { 8, "ул. Ново-Садовая, 228", new TimeOnly(23, 0, 0), "Чипа Лучо", new TimeOnly(10, 0, 0), 4.2000000000000002 },
                    { 9, "ул. Молодогвардейская,127", new TimeOnly(20, 0, 0), "Белотурка", new TimeOnly(7, 0, 0), 4.7999999999999998 },
                    { 10, "ул. Владимирская, 38", new TimeOnly(22, 0, 0), "Gellert", new TimeOnly(10, 0, 0), 4.0 }
                });

            migrationBuilder.InsertData(
                table: "Dishes",
                columns: new[] { "Id", "CategoryId", "Name", "Price", "RestaurantId", "WeightGrams" },
                values: new object[,]
                {
                    { 1, 1, "Пицца Песто", 750m, 1, 500 },
                    { 2, 1, "Пицца 4 сыра", 650m, 1, 550 },
                    { 3, 2, "Бургер с рваной говядиной", 650m, 2, 300 },
                    { 4, 2, "Чизбушка", 500m, 2, 320 },
                    { 5, 3, "Луковые кольца", 220m, 2, 150 },
                    { 6, 4, "Греческий салат", 420m, 4, 280 },
                    { 7, 5, "Солянка", 380m, 5, 350 },
                    { 8, 6, "Паста Аррабиата", 590m, 8, 400 },
                    { 9, 7, "Роллы с тунцом", 680m, 3, 250 },
                    { 10, 8, "Торт медовик", 300m, 9, 180 },
                    { 11, 9, "Капучино", 450m, 7, 300 },
                    { 12, 10, "Сырники", 350m, 9, 250 }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CreatedAt", "CustomerId", "DeliveredAt", "RestaurantId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 9, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 9, 1, 12, 35, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, new DateTime(2026, 9, 2, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2026, 9, 2, 13, 50, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, new DateTime(2026, 9, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2026, 9, 3, 14, 25, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 4, new DateTime(2026, 9, 4, 15, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2026, 9, 4, 15, 45, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 5, new DateTime(2026, 9, 5, 16, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2026, 9, 5, 16, 30, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 6, new DateTime(2026, 9, 6, 17, 0, 0, 0, DateTimeKind.Unspecified), 6, new DateTime(2026, 9, 6, 17, 40, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 7, new DateTime(2026, 9, 7, 18, 0, 0, 0, DateTimeKind.Unspecified), 7, new DateTime(2026, 9, 7, 18, 20, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 8, new DateTime(2026, 9, 8, 19, 45, 0, 0, DateTimeKind.Unspecified), 8, new DateTime(2026, 9, 8, 19, 55, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 9, new DateTime(2026, 9, 9, 10, 0, 0, 0, DateTimeKind.Unspecified), 9, new DateTime(2026, 9, 9, 10, 10, 0, 0, DateTimeKind.Unspecified), 8 },
                    { 10, new DateTime(2026, 9, 10, 11, 0, 0, 0, DateTimeKind.Unspecified), 10, new DateTime(2026, 9, 10, 11, 30, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 11, new DateTime(2026, 9, 11, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 9, 11, 12, 25, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 12, new DateTime(2026, 9, 12, 13, 0, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2026, 9, 12, 13, 30, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 13, new DateTime(2026, 9, 13, 14, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2026, 9, 13, 14, 20, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 14, new DateTime(2026, 9, 14, 15, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2026, 9, 14, 15, 35, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 15, new DateTime(2026, 9, 15, 16, 0, 0, 0, DateTimeKind.Unspecified), 6, new DateTime(2026, 9, 15, 16, 40, 0, 0, DateTimeKind.Unspecified), 8 }
                });

            migrationBuilder.InsertData(
                table: "OrderItems",
                columns: new[] { "Id", "DishId", "OrderId", "Quantity" },
                values: new object[,]
                {
                    { 1, 1, 1, 1 },
                    { 2, 2, 1, 1 },
                    { 3, 1, 2, 1 },
                    { 4, 2, 2, 2 },
                    { 5, 3, 3, 1 },
                    { 6, 5, 3, 1 },
                    { 7, 4, 4, 2 },
                    { 8, 5, 4, 1 },
                    { 9, 9, 5, 1 },
                    { 10, 9, 6, 1 },
                    { 11, 6, 7, 1 },
                    { 12, 7, 8, 1 },
                    { 13, 8, 9, 1 },
                    { 14, 11, 10, 1 },
                    { 15, 11, 11, 1 },
                    { 16, 11, 12, 2 },
                    { 17, 11, 13, 1 },
                    { 18, 3, 14, 1 },
                    { 19, 8, 15, 1 }
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
                name: "IX_OrderItems_DishId",
                table: "OrderItems",
                column: "DishId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_OrderId",
                table: "OrderItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_RestaurantId",
                table: "Orders",
                column: "RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderItems");

            migrationBuilder.DropTable(
                name: "Dishes");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Restaurants");
        }
    }
}
