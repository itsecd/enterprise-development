using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HotelSystem.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PassportNumber = table.Column<string>(type: "text", nullable: false),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: true),
                    BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Citizenship = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RoomTypes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Category = table.Column<int>(type: "integer", nullable: false),
                    Area = table.Column<double>(type: "double precision", nullable: false),
                    BedCount = table.Column<int>(type: "integer", nullable: false),
                    HasBathOrShower = table.Column<bool>(type: "boolean", nullable: false),
                    PricePerDay = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoomTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Rooms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoomNumber = table.Column<int>(type: "integer", nullable: false),
                    Floor = table.Column<int>(type: "integer", nullable: false),
                    HasBalcony = table.Column<bool>(type: "boolean", nullable: false),
                    RoomTypeId = table.Column<int>(type: "integer", nullable: false)
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
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    RoomId = table.Column<int>(type: "integer", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    NumberOfDays = table.Column<int>(type: "integer", nullable: false)
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
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "BirthDate", "Citizenship", "FirstName", "LastName", "PassportNumber", "Patronymic" },
                values: new object[,]
                {
                    { 1, new DateTime(1995, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Россия", "Алексей", "Александров", "AA100001", "Петрович" },
                    { 2, new DateTime(1988, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "Россия", "Борис", "Белов", "AA100002", "Сергеевич" },
                    { 3, new DateTime(1999, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Россия", "Виктория", "Васильева", "AA100003", "Андреевна" },
                    { 4, new DateTime(1992, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Беларусь", "Георгий", "Громов", "AA100004", "Иванович" },
                    { 5, new DateTime(2001, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Россия", "Дарья", "Дмитриева", "AA100005", "Олеговна" },
                    { 6, new DateTime(1985, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Казахстан", "Евгений", "Егоров", "AA100006", "Максимович" },
                    { 7, new DateTime(1997, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Россия", "Жанна", "Жукова", "AA100007", "Романовна" },
                    { 8, new DateTime(1990, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Армения", "Захар", "Захаров", "AA100008", "Денисович" },
                    { 9, new DateTime(1996, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Россия", "Ирина", "Ван Дам", "AA100009", "Павловна" },
                    { 10, new DateTime(1993, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Россия", "Кирилл", "Кузнецов", "AA100010", null }
                });

            migrationBuilder.InsertData(
                table: "RoomTypes",
                columns: new[] { "Id", "Area", "BedCount", "Category", "HasBathOrShower", "PricePerDay" },
                values: new object[,]
                {
                    { 1, 18.0, 1, 0, true, 3000m },
                    { 2, 22.0, 2, 1, true, 4500m },
                    { 3, 35.0, 2, 2, true, 8000m },
                    { 4, 20.0, 1, 0, false, 3200m },
                    { 5, 25.0, 2, 1, true, 5000m },
                    { 6, 40.0, 3, 2, true, 9500m },
                    { 7, 19.0, 1, 0, true, 3100m },
                    { 8, 24.0, 2, 1, false, 4800m },
                    { 9, 45.0, 4, 2, true, 11000m },
                    { 10, 28.0, 2, 1, true, 5500m }
                });

            migrationBuilder.InsertData(
                table: "Rooms",
                columns: new[] { "Id", "Floor", "HasBalcony", "RoomNumber", "RoomTypeId" },
                values: new object[,]
                {
                    { 1, 1, false, 101, 1 },
                    { 2, 1, true, 102, 2 },
                    { 3, 1, true, 103, 3 },
                    { 4, 2, false, 201, 1 },
                    { 5, 2, true, 202, 2 },
                    { 6, 2, true, 203, 3 },
                    { 7, 3, false, 301, 4 },
                    { 8, 3, true, 302, 5 },
                    { 9, 3, true, 303, 6 },
                    { 10, 4, false, 401, 4 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CheckInDate", "ClientId", "NumberOfDays", "RoomId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, 1 },
                    { 2, new DateTime(2026, 9, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 3, 2 },
                    { 3, new DateTime(2026, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 4, 2 },
                    { 4, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 7, 3 },
                    { 5, new DateTime(2026, 8, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 2, 4 },
                    { 6, new DateTime(2026, 8, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 6, 5 },
                    { 7, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 4, 5 },
                    { 8, new DateTime(2026, 8, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 10, 6 },
                    { 9, new DateTime(2026, 9, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), 9, 5, 6 },
                    { 10, new DateTime(2026, 9, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 3, 6 },
                    { 11, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, 7 },
                    { 12, new DateTime(2026, 8, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, 8 },
                    { 13, new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 8, 9 },
                    { 14, new DateTime(2026, 9, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 6, 10 }
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
