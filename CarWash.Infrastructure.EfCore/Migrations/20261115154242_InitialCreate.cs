using System;
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
            migrationBuilder.CreateTable(
                name: "CarWashCustomers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarWashCustomers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarWashProcedures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarCategory = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarWashProcedures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarCategory = table.Column<int>(type: "int", nullable: false),
                    LicensePlate = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CustomerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cars_CarWashCustomers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CarWashCustomers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarId = table.Column<int>(type: "int", nullable: false),
                    ProcedureId = table.Column<int>(type: "int", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    WashBoxNumber = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_CarWashProcedures_ProcedureId",
                        column: x => x.ProcedureId,
                        principalTable: "CarWashProcedures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Cars_CarId",
                        column: x => x.CarId,
                        principalTable: "Cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CarWashCustomers",
                columns: new[] { "Id", "FirstName", "LastName", "Patronymic", "Phone" },
                values: new object[,]
                {
                    { 1, "Антон", "Сопранов", null, "+79010000001" },
                    { 2, "Дмитрий", "Ошпаренный", "Алексеевич", "+79020000002" },
                    { 3, "Боб", "Папс", null, "+79030000003" },
                    { 4, "Андрей", "Быков", "Евгеньевич", "+79040000004" },
                    { 5, "Геннадий", "Бобков", "Андреевич", "+79040000066" },
                    { 6, "Федор", "Афанасьев", "Иванович", "+79060000006" },
                    { 7, "Максим", "Васильев", "Максимович", "+79990000007" },
                    { 8, "Николай", "Новиков", null, "+79990000008" },
                    { 9, "Роман", "Фёдоров", "Романович", "+79990000009" },
                    { 10, "Михаил", "Морозов", "Михайлович", "+79990000010" },
                    { 11, "Анелло", "Лоренцо", null, "+79990000011" },
                    { 12, "Виктор", "Алексеев", "Викторович", "+79990000012" }
                });

            migrationBuilder.InsertData(
                table: "CarWashProcedures",
                columns: new[] { "Id", "CarCategory", "Duration", "Name", "Price" },
                values: new object[,]
                {
                    { 1, 0, new TimeSpan(0, 1, 0, 0, 0), "Комплексная мойка", 1500m },
                    { 2, 0, new TimeSpan(0, 0, 30, 0, 0), "Мойка кузова", 800m },
                    { 3, 0, new TimeSpan(0, 0, 45, 0, 0), "Мойка салона", 1000m },
                    { 4, 0, new TimeSpan(0, 0, 40, 0, 0), "Мойка двигателя", 1200m },
                    { 5, 0, new TimeSpan(0, 2, 0, 0, 0), "Полировка кузова", 3000m },
                    { 6, 4, new TimeSpan(0, 1, 15, 0, 0), "Мойка внедорожника", 2000m },
                    { 7, 5, new TimeSpan(0, 1, 30, 0, 0), "Мойка микроавтобуса", 2500m },
                    { 8, 1, new TimeSpan(0, 2, 0, 0, 0), "Мойка грузового автомобиля", 4000m },
                    { 9, 2, new TimeSpan(0, 2, 30, 0, 0), "Мойка автобуса", 5000m },
                    { 10, 3, new TimeSpan(0, 0, 30, 0, 0), "Мойка мотоцикла", 700m }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "CarCategory", "CustomerId", "LicensePlate" },
                values: new object[,]
                {
                    { 1, "Chevrolet", 0, 1, "А111АА" },
                    { 2, "Cadillac", 0, 1, "А222АА" },
                    { 3, "BMW", 0, 2, "О248ВН" },
                    { 4, "BMW", 0, 2, "О249ВН" },
                    { 5, "Harley-Davidson", 3, 3, "С555СС" },
                    { 6, "Honda", 3, 4, "Е666ЕЕ" },
                    { 7, "Lada", 0, 5, "К777КК" },
                    { 8, "KamAZ", 1, 6, "М571АК" },
                    { 9, "Skoda", 0, 7, "Н101НН" },
                    { 10, "Volvo", 0, 8, "О202ОО" },
                    { 11, "Nissan", 0, 9, "Р303РР" },
                    { 12, "Mazda", 0, 10, "Т404ТТ" },
                    { 13, "Lexus", 0, 10, "Т505ТТ" },
                    { 14, "GMC", 1, 11, "У606УУ" },
                    { 15, "Peugeot", 0, 12, "Х707ХХ" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "CarId", "ProcedureId", "StartTime", "WashBoxNumber" },
                values: new object[,]
                {
                    { 1, 1, 1, new DateTime(2026, 9, 10, 9, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 2, 2, new DateTime(2026, 9, 11, 10, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 3, 1, 1, new DateTime(2026, 9, 12, 11, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, 2, 3, new DateTime(2026, 9, 13, 12, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 5, 3, 1, new DateTime(2026, 9, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 6, 4, 2, new DateTime(2026, 9, 11, 11, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 7, 4, 1, new DateTime(2026, 9, 13, 13, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 8, 5, 10, new DateTime(2026, 9, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 9, 5, 10, new DateTime(2026, 9, 11, 13, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 10, 6, 10, new DateTime(2026, 9, 10, 13, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 11, 6, 10, new DateTime(2026, 9, 11, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 12, 7, 1, new DateTime(2026, 9, 10, 14, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 13, 7, 2, new DateTime(2026, 9, 11, 15, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 14, 7, 1, new DateTime(2026, 9, 15, 11, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 15, 8, 8, new DateTime(2026, 9, 10, 15, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 16, 9, 2, new DateTime(2026, 9, 10, 16, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 17, 9, 3, new DateTime(2026, 9, 11, 16, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 18, 10, 3, new DateTime(2026, 9, 11, 17, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 19, 11, 4, new DateTime(2026, 9, 12, 10, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 20, 11, 5, new DateTime(2026, 9, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 21, 12, 5, new DateTime(2026, 9, 12, 11, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 22, 13, 1, new DateTime(2026, 9, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 23, 14, 8, new DateTime(2026, 9, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 24, 14, 8, new DateTime(2026, 9, 16, 14, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 25, 15, 2, new DateTime(2026, 9, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 26, 2, 3, new DateTime(2026, 9, 17, 11, 30, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 27, 3, 1, new DateTime(2026, 9, 17, 11, 30, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 28, 7, 2, new DateTime(2026, 9, 17, 11, 50, 0, 0, DateTimeKind.Unspecified), 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CustomerId",
                table: "Cars",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CarId",
                table: "Orders",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ProcedureId",
                table: "Orders",
                column: "ProcedureId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "CarWashProcedures");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "CarWashCustomers");
        }
    }
}
