using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Carwash.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    full_name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    phone_number = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    category = table.Column<int>(type: "int", nullable: false),
                    duration_in_minutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    license_plate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.id);
                    table.ForeignKey(
                        name: "FK_cars_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    car_id = table.Column<int>(type: "int", nullable: false),
                    service_id = table.Column<int>(type: "int", nullable: false),
                    box_number = table.Column<int>(type: "int", nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.id);
                    table.ForeignKey(
                        name: "FK_orders_cars_car_id",
                        column: x => x.car_id,
                        principalTable: "cars",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_services_service_id",
                        column: x => x.service_id,
                        principalTable: "services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "clients",
                columns: new[] { "id", "full_name", "phone_number" },
                values: new object[,]
                {
                    { 1, "Иван Петров", "+79990000001" },
                    { 2, "Анна Сидорова", "+79990000002" },
                    { 3, "Алексей Смирнов", "+79990000003" },
                    { 4, "Мария Иванова", "+79990000004" },
                    { 5, "Дмитрий Кузнецов", "+79990000005" },
                    { 6, "Елена Попова", "+79990000006" },
                    { 7, "Сергей Волков", "+79990000007" }
                });

            migrationBuilder.InsertData(
                table: "services",
                columns: new[] { "id", "category", "duration_in_minutes", "name", "price" },
                values: new object[,]
                {
                    { 1, 0, 30, "Экспресс-мойка", 800m },
                    { 2, 0, 60, "Комплексная мойка", 1500m },
                    { 3, 1, 90, "Премиум-мойка", 2500m },
                    { 4, 3, 120, "Мойка внедорожника", 3000m },
                    { 5, 2, 150, "Люкс-мойка", 4000m }
                });

            migrationBuilder.InsertData(
                table: "cars",
                columns: new[] { "id", "brand", "client_id", "license_plate" },
                values: new object[,]
                {
                    { 1, "BMW", 1, "А111АА" },
                    { 2, "Toyota", 2, "В222ВВ" },
                    { 3, "Mercedes-Benz", 3, "С333СС" },
                    { 4, "Audi", 4, "Е444ЕЕ" },
                    { 5, "Lexus", 5, "К555КК" },
                    { 6, "Volkswagen", 6, "М666ММ" },
                    { 7, "Volvo", 7, "Н777НН" },
                    { 8, "Range Rover", 1, "О888ОО" }
                });

            migrationBuilder.InsertData(
                table: "orders",
                columns: new[] { "id", "box_number", "car_id", "client_id", "service_id", "start_time" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, 1, new DateTime(2026, 9, 14, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, 1, 1, 2, new DateTime(2026, 9, 15, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, 1, 1, 3, new DateTime(2026, 9, 16, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, 1, 1, 1, new DateTime(2026, 9, 17, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 2, 1, 1, 2, new DateTime(2026, 9, 18, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 1, 2, 2, 1, new DateTime(2026, 9, 14, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 2, 2, 2, 2, new DateTime(2026, 9, 15, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 3, 2, 2, 3, new DateTime(2026, 9, 17, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 1, 2, 2, 1, new DateTime(2026, 9, 18, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 4, 3, 3, 4, new DateTime(2026, 9, 15, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 3, 3, 3, 3, new DateTime(2026, 9, 17, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 1, 3, 3, 1, new DateTime(2026, 9, 19, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 2, 4, 4, 2, new DateTime(2026, 9, 16, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 5, 4, 4, 5, new DateTime(2026, 9, 18, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 1, 4, 4, 1, new DateTime(2026, 9, 20, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, 2, 5, 5, 2, new DateTime(2026, 9, 17, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, 3, 5, 5, 3, new DateTime(2026, 9, 19, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, 1, 6, 6, 1, new DateTime(2026, 9, 18, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, 4, 6, 6, 4, new DateTime(2026, 9, 20, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, 5, 7, 7, 5, new DateTime(2026, 9, 21, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 21, 1, 1, 1, 3, new DateTime(2026, 9, 24, 11, 40, 0, 0, DateTimeKind.Unspecified) },
                    { 22, 4, 2, 2, 4, new DateTime(2026, 9, 24, 11, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 23, 2, 3, 3, 2, new DateTime(2026, 9, 24, 11, 45, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_cars_client_id",
                table: "cars",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_car_id",
                table: "orders",
                column: "car_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_client_id",
                table: "orders",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_service_id",
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
