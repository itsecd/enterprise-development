using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hotel.Infrastructure.Database.Migrations
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
                    LastName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FirstName = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Patronymic = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    NumberPassport = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Citizenship = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Category = table.Column<int>(type: "int", nullable: false),
                    RoomArea = table.Column<int>(type: "int", nullable: false),
                    BedCount = table.Column<int>(type: "int", nullable: false),
                    HasBathroom = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<int>(type: "int", nullable: false),
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
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    DateArrival = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    DayCount = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    RoomId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "BirthDate", "Citizenship", "FirstName", "LastName", "NumberPassport", "Patronymic" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "РФ", "Мария", "Клюева", "1234 678901", "Николаевна" },
                    { 2, new DateTime(1967, 5, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "РФ", "Петр", "Волоконов", "3421 576230", "Васильевич" },
                    { 3, new DateTime(1994, 6, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "РФ", "Виталий", "Валеев", "6234 817462", "Владимирович" },
                    { 4, new DateTime(1975, 6, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "USA", "Angelina", "Jolie", "654783710", null },
                    { 5, new DateTime(1962, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "USA", "Jim", "Carrey", "934801843", null },
                    { 6, new DateTime(1967, 4, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "РФ", "Филипп", "Киркоров", "4932 104581", "Бедросович" },
                    { 7, new DateTime(1981, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "РФ", "Дмитрий", "Билан", "5293 104585", "Николаевич" },
                    { 8, new DateTime(1973, 10, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "РФ", "Сергей", "Безруков", "4823 184751", "Витальевич" },
                    { 9, new DateTime(1953, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "CN", "Xi", "Jinping", "EA3819831", null },
                    { 10, new DateTime(1960, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "KR", "Suk-Yeol", "Yoon", "M48129384", null }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "BedCount", "Category", "HasBathroom", "Price", "RoomArea" },
                values: new object[,]
                {
                    { 1, 1, 1, false, 2500m, 8 },
                    { 2, 1, 1, false, 2700m, 10 },
                    { 3, 1, 2, false, 5000m, 20 },
                    { 4, 1, 2, false, 4000m, 15 },
                    { 5, 2, 3, true, 11000m, 25 },
                    { 6, 2, 3, true, 12000m, 30 },
                    { 7, 3, 4, true, 18000m, 45 },
                    { 8, 4, 4, true, 19500m, 50 },
                    { 9, 2, 5, true, 750000m, 90 },
                    { 10, 6, 5, true, 650000m, 100 }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Floor", "HasBalcony", "Number", "RoomTypeId" },
                values: new object[,]
                {
                    { 1, 1, false, 1, 1 },
                    { 2, 1, false, 3, 1 },
                    { 3, 1, false, 4, 2 },
                    { 4, 1, false, 5, 3 },
                    { 5, 1, false, 6, 4 },
                    { 6, 2, true, 7, 4 },
                    { 7, 2, false, 8, 5 },
                    { 8, 2, false, 10, 5 },
                    { 9, 2, true, 11, 6 },
                    { 10, 2, false, 13, 7 },
                    { 11, 2, true, 14, 7 },
                    { 12, 3, true, 15, 8 },
                    { 13, 3, true, 16, 9 },
                    { 14, 3, true, 17, 9 },
                    { 15, 3, true, 18, 10 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "ClientId", "DateArrival", "DayCount", "RoomId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2021, 5, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 101, 2 },
                    { 2, 6, new DateTime(2025, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 10 },
                    { 3, 8, new DateTime(2015, 10, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 15, 4 },
                    { 4, 4, new DateTime(2009, 8, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 31, 1 },
                    { 5, 7, new DateTime(2026, 2, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 5 },
                    { 6, 3, new DateTime(2017, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 45, 2 },
                    { 7, 10, new DateTime(2022, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 8 },
                    { 8, 2, new DateTime(2001, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), 18, 15 },
                    { 9, 5, new DateTime(2007, 3, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 60, 6 },
                    { 10, 10, new DateTime(2011, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 365, 14 },
                    { 11, 9, new DateTime(1972, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 28, 13 },
                    { 12, 3, new DateTime(2013, 10, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, 9 },
                    { 13, 3, new DateTime(2019, 4, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5 },
                    { 14, 7, new DateTime(2019, 3, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 50, 3 },
                    { 15, 6, new DateTime(2024, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 7 },
                    { 16, 9, new DateTime(2023, 5, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 11 },
                    { 17, 10, new DateTime(2021, 1, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 12 },
                    { 18, 4, new DateTime(2021, 9, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 11, 15 },
                    { 19, 1, new DateTime(2021, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, 6 },
                    { 20, 2, new DateTime(2021, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 60, 15 }
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
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Rooms");

            migrationBuilder.DropTable(
                name: "RoomTypes");
        }
    }
}
