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
                name: "categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "customers",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    last_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    first_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    patronymic = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    delivery_address = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_customers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "restaurants",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    address = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    rating = table.Column<double>(type: "double precision", nullable: false),
                    opening_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    closing_time = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_restaurants", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "dishes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    weight_grams = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false),
                    category_id = table.Column<int>(type: "integer", nullable: false),
                    restaurant_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dishes", x => x.id);
                    table.ForeignKey(
                        name: "FK_dishes_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_dishes_restaurants_restaurant_id",
                        column: x => x.restaurant_id,
                        principalTable: "restaurants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    customer_id = table.Column<int>(type: "integer", nullable: false),
                    restaurant_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    delivered_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_restaurants_restaurant_id",
                        column: x => x.restaurant_id,
                        principalTable: "restaurants",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    order_id = table.Column<int>(type: "integer", nullable: false),
                    dish_id = table.Column<int>(type: "integer", nullable: false),
                    quantity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_items_dishes_dish_id",
                        column: x => x.dish_id,
                        principalTable: "dishes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_order_items_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "categories",
                columns: new[] { "id", "name" },
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
                table: "customers",
                columns: new[] { "id", "delivery_address", "first_name", "last_name", "patronymic", "phone" },
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
                table: "restaurants",
                columns: new[] { "id", "address", "closing_time", "name", "opening_time", "rating" },
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
                table: "dishes",
                columns: new[] { "id", "category_id", "name", "price", "restaurant_id", "weight_grams" },
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
                table: "orders",
                columns: new[] { "id", "created_at", "customer_id", "delivered_at", "restaurant_id" },
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
                    { 15, new DateTime(2026, 9, 15, 16, 0, 0, 0, DateTimeKind.Unspecified), 6, null, 8 }
                });

            migrationBuilder.InsertData(
                table: "order_items",
                columns: new[] { "id", "dish_id", "order_id", "quantity" },
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
                name: "IX_dishes_category_id",
                table: "dishes",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "IX_dishes_restaurant_id",
                table: "dishes",
                column: "restaurant_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_dish_id",
                table: "order_items",
                column: "dish_id");

            migrationBuilder.CreateIndex(
                name: "IX_order_items_order_id",
                table: "order_items",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_customer_id",
                table: "orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_restaurant_id",
                table: "orders",
                column: "restaurant_id");
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
