using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hotel.Infrastructure.EfCore.Migrations
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
                name: "HotelClients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PassportNumber = table.Column<string>(type: "longtext", nullable: false),
                    FirstName = table.Column<string>(type: "longtext", nullable: false),
                    LastName = table.Column<string>(type: "longtext", nullable: false),
                    MiddleName = table.Column<string>(type: "longtext", nullable: true),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    Citizenship = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HotelClients", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<int>(type: "int", nullable: false),
                    Area = table.Column<double>(type: "double", nullable: false),
                    BedCount = table.Column<int>(type: "int", nullable: false),
                    HasBathroom = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PricePerDay = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    Floor = table.Column<int>(type: "int", nullable: false),
                    HasBalcony = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    RoomTypeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rooms", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Rooms_RoomTypes_RoomTypeId",
                        column: x => x.RoomTypeId,
                        principalTable: "RoomTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    CheckInDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DaysCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_HotelClients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "HotelClients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "HotelClients",
                columns: new[] { "Id", "BirthDate", "Citizenship", "FirstName", "LastName", "MiddleName", "PassportNumber" },
                values: new object[,]
                {
                    { 1, new DateOnly(1990, 5, 12), "Russia", "Alexey", "Ivanov", null, "AA123456" },
                    { 2, new DateOnly(1988, 3, 20), "Russia", "Anna", "Petrova", null, "BB234567" },
                    { 3, new DateOnly(1995, 7, 8), "Russia", "Dmitry", "Smirnov", null, "CC345678" },
                    { 4, new DateOnly(1992, 11, 15), "Russia", "Elena", "Kuznetsova", null, "DD456789" },
                    { 5, new DateOnly(1985, 1, 25), "Russia", "Ivan", "Popov", null, "EE567890" },
                    { 6, new DateOnly(1998, 6, 10), "Russia", "Maria", "Sokolova", null, "FF678901" },
                    { 7, new DateOnly(1987, 9, 3), "Russia", "Nikolay", "Fedorov", null, "GG789012" },
                    { 8, new DateOnly(1993, 12, 18), "Russia", "Olga", "Morozova", null, "HH890123" },
                    { 9, new DateOnly(1991, 4, 7), "Russia", "Pavel", "Volkov", null, "II901234" },
                    { 10, new DateOnly(1996, 8, 22), "Russia", "Sofia", "Orlova", null, "JJ012345" },
                    { 11, new DateOnly(1989, 2, 14), "USA", "John", "Smith", null, "AB987654" }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "Area", "BedCount", "Category", "HasBathroom", "PricePerDay" },
                values: new object[,]
                {
                    { 1, 18.0, 1, 0, true, 2500m },
                    { 2, 22.0, 2, 0, false, 3000m },
                    { 3, 25.0, 2, 1, true, 4000m },
                    { 4, 28.0, 2, 1, true, 4500m },
                    { 5, 30.0, 3, 1, false, 5000m },
                    { 6, 32.0, 2, 2, true, 5500m },
                    { 7, 38.0, 3, 2, false, 6500m },
                    { 8, 40.0, 4, 2, true, 7000m },
                    { 9, 45.0, 2, 3, false, 8000m },
                    { 10, 55.0, 2, 3, true, 10000m }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Floor", "HasBalcony", "RoomNumber", "RoomTypeId" },
                values: new object[,]
                {
                    { 1, 1, false, 101, 1 },
                    { 2, 1, true, 102, 2 },
                    { 3, 2, false, 201, 3 },
                    { 4, 2, true, 202, 4 },
                    { 5, 3, true, 301, 5 },
                    { 6, 3, false, 302, 6 },
                    { 7, 4, true, 401, 7 },
                    { 8, 4, true, 402, 8 },
                    { 9, 5, false, 501, 9 },
                    { 10, 5, true, 502, 10 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CheckInDate", "ClientId", "DaysCount", "RoomId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2026, 9, 1), 1, 3, 7 },
                    { 2, new DateOnly(2026, 9, 4), 2, 5, 7 },
                    { 3, new DateOnly(2026, 9, 9), 3, 2, 7 },
                    { 4, new DateOnly(2026, 9, 11), 4, 4, 7 },
                    { 5, new DateOnly(2026, 9, 15), 5, 7, 7 },
                    { 6, new DateOnly(2026, 8, 1), 6, 3, 3 },
                    { 7, new DateOnly(2026, 8, 4), 7, 6, 3 },
                    { 8, new DateOnly(2026, 8, 10), 8, 2, 3 },
                    { 9, new DateOnly(2026, 8, 12), 9, 5, 3 },
                    { 10, new DateOnly(2026, 7, 1), 10, 4, 10 },
                    { 11, new DateOnly(2026, 7, 5), 1, 3, 10 },
                    { 12, new DateOnly(2026, 7, 8), 2, 2, 10 },
                    { 13, new DateOnly(2026, 6, 1), 3, 4, 2 },
                    { 14, new DateOnly(2026, 6, 5), 4, 3, 2 },
                    { 15, new DateOnly(2026, 5, 1), 5, 6, 5 },
                    { 16, new DateOnly(2026, 5, 7), 6, 3, 5 },
                    { 17, new DateOnly(2026, 4, 1), 7, 2, 1 },
                    { 18, new DateOnly(2026, 4, 10), 8, 4, 4 },
                    { 19, new DateOnly(2026, 3, 1), 9, 5, 6 },
                    { 20, new DateOnly(2026, 3, 10), 10, 2, 8 },
                    { 21, new DateOnly(2026, 8, 5), 1, 4, 4 },
                    { 22, new DateOnly(2026, 8, 3), 2, 7, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ClientId",
                table: "Bookings",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_RoomTypeId",
                table: "Rooms",
                column: "RoomTypeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "HotelClients");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomTypes");
        }
    }
}
