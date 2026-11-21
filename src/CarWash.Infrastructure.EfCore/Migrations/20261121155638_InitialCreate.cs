using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarWash.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    full_name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    phone = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    category = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    duration_minutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_services", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    license_plate = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    brand = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    client_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cars", x => x.id);
                    table.ForeignKey(
                        name: "fk_cars_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    service_id = table.Column<int>(type: "int", nullable: false),
                    car_id = table.Column<int>(type: "int", nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    box_number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_orders", x => x.id);
                    table.ForeignKey(
                        name: "fk_orders_cars_car_id",
                        column: x => x.car_id,
                        principalTable: "cars",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_orders_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_orders_services_service_id",
                        column: x => x.service_id,
                        principalTable: "services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "clients",
                columns: new[] { "id", "full_name", "phone" },
                values: new object[,]
                {
                    { 1, "Иванов Иван Иванович", "+78461112233" },
                    { 2, "Петров Сергей Алексеевич", "+78462223344" },
                    { 3, "Сидоров Алексей Михайлович", "+78463334455" },
                    { 4, "Смирнова Елена Павловна", "+78464445566" },
                    { 5, "Кузнецов Дмитрий Николаевич", "+78465556677" },
                    { 6, "Васильев Андрей Игоревич", "+78466667788" },
                    { 7, "Попов Михаил Сергеевич", "+78467778899" },
                    { 8, "Соколова Анна Владимировна", "+78468889900" },
                    { 9, "Михайлов Денис Олегович", "+78469990011" },
                    { 10, "Федоров Роман Денисович", "+78460001122" }
                });

            migrationBuilder.InsertData(
                table: "services",
                columns: new[] { "id", "category", "duration_minutes", "name", "price" },
                values: new object[,]
                {
                    { 1, "Passenger", 20, "Экспресс-мойка", 600m },
                    { 2, "Passenger", 45, "Комплексная мойка", 1400m },
                    { 3, "Suv", 35, "Стандартная мойка SUV", 1100m },
                    { 4, "Passenger", 90, "Химчистка салона", 4500m },
                    { 5, "Truck", 60, "Мойка грузовика", 2800m },
                    { 6, "Suv", 75, "Полировка кузова", 3500m },
                    { 7, "Suv", 30, "Мойка двигателя", 1200m },
                    { 8, "Passenger", 15, "Обработка воском", 500m },
                    { 9, "Suv", 25, "Мойка днища", 900m },
                    { 10, "Minivan", 50, "Мойка минивэна", 1800m }
                });

            migrationBuilder.InsertData(
                table: "cars",
                columns: new[] { "id", "brand", "client_id", "license_plate" },
                values: new object[,]
                {
                    { 1, "Lada Ballada", 1, "А101АА663" },
                    { 2, "Toyota Yenota", 2, "В206ВВ663" },
                    { 3, "CUMAZ 67", 3, "С302СС363" },
                    { 4, "BMW X5group", 4, "Е456ЕЕ163" },
                    { 5, "Kia Rio", 2, "К545КК067" },
                    { 6, "Hyundai Slesaris", 5, "М611ММ663" },
                    { 7, "Ssania R500", 6, "Х701ТР167" },
                    { 8, "GAZelle Prev", 7, "О893ОО557" },
                    { 9, "Mercedes Splinter", 8, "Р999РР347" },
                    { 10, "Geely Monjaro", 9, "Т042ТТ127" },
                    { 11, "Skoda Octavius", 10, "Н777НН163" }
                });

            migrationBuilder.InsertData(
                table: "orders",
                columns: new[] { "id", "box_number", "car_id", "client_id", "service_id", "start_time" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 1, new DateTime(2026, 6, 1, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 1, 1, 1, 1, new DateTime(2026, 6, 1, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 2, 1, 1, 2, new DateTime(2026, 6, 2, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, 1, 1, 8, new DateTime(2026, 6, 3, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, 1, 1, 1, new DateTime(2026, 6, 4, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 2, 2, 2, 2, new DateTime(2026, 6, 1, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 1, 2, 2, 2, new DateTime(2026, 6, 1, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 3, 5, 2, 3, new DateTime(2026, 6, 2, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 2, 5, 2, 1, new DateTime(2026, 6, 3, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 3, 3, 3, 5, new DateTime(2026, 6, 1, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 3, 3, 3, 5, new DateTime(2026, 6, 3, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 3, 3, 3, 3, new DateTime(2026, 6, 5, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 4, 4, 4, 3, new DateTime(2026, 6, 1, 10, 10, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 1, 4, 4, 1, new DateTime(2026, 6, 2, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 2, 6, 5, 4, new DateTime(2026, 6, 1, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, 2, 6, 5, 6, new DateTime(2026, 6, 4, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, 4, 7, 6, 7, new DateTime(2026, 6, 1, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, 5, 8, 7, 10, new DateTime(2026, 6, 1, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, 5, 9, 8, 9, new DateTime(2026, 6, 1, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, 2, 10, 9, 2, new DateTime(2026, 6, 1, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, 2, 11, 10, 4, new DateTime(2026, 6, 6, 10, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "ix_cars_client_id",
                table: "cars",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_car_id",
                table: "orders",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_client_id",
                table: "orders",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_orders_service_id",
                table: "orders",
                column: "service_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "services");

            migrationBuilder.DropTable(
                name: "clients");
        }
    }
}
