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
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Category = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    LicensePlate = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Brand = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    ClientId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    BoxNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "FullName", "Phone" },
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
                table: "Services",
                columns: new[] { "Id", "Category", "DurationMinutes", "Name", "Price" },
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
                table: "Cars",
                columns: new[] { "Id", "Brand", "ClientId", "LicensePlate" },
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
                table: "Orders",
                columns: new[] { "Id", "BoxNumber", "CarId", "ClientId", "ServiceId", "StartTime" },
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
                name: "IX_Cars_ClientId",
                table: "Cars",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CarId",
                table: "Orders",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ServiceId",
                table: "Orders",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Clients");
        }
    }
}
