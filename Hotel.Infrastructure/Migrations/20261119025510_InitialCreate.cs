using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hotel.Infrastructure.Migrations;

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
                PassportNumber = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Name = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Surname = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Patronymic = table.Column<string>(type: "longtext", nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                Citizenship = table.Column<string>(type: "longtext", nullable: false)
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
                Area = table.Column<double>(type: "double", nullable: false),
                Beds = table.Column<uint>(type: "int unsigned", nullable: false),
                HasBath = table.Column<bool>(type: "tinyint(1)", nullable: false),
                HasShower = table.Column<bool>(type: "tinyint(1)", nullable: false),
                DayPrice = table.Column<double>(type: "double", nullable: false),
                Category = table.Column<int>(type: "int", nullable: false)
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
                Number = table.Column<uint>(type: "int unsigned", nullable: false),
                Floor = table.Column<uint>(type: "int unsigned", nullable: false),
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
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Bookings",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                ClientId = table.Column<int>(type: "int", nullable: false),
                CheckInDate = table.Column<DateOnly>(type: "date", nullable: false),
                DaysOfStay = table.Column<uint>(type: "int unsigned", nullable: false),
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
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Bookings_Rooms_RoomId",
                    column: x => x.RoomId,
                    principalTable: "Rooms",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.InsertData(
            table: "Clients",
            columns: new[] { "Id", "BirthDate", "Citizenship", "Name", "PassportNumber", "Patronymic", "Surname" },
            values: new object[,]
            {
                { 1, new DateOnly(1965, 2, 15), "Российская Федерация", "Николай", "40 10 200123", "Васильевич", "Степанов" },
                { 2, new DateOnly(2005, 1, 17), "Российская Федерация", "София", "45 25 564209", "Сергеевна", "Оленцова" },
                { 3, new DateOnly(1978, 5, 14), "Российская Федерация", "Сергей", "60 23 123765", "Олегович", "Соколов" },
                { 4, new DateOnly(1991, 8, 26), "Российская Федерация", "Генадий", "69 11 142836", "Петрович", "Воробьёв" },
                { 5, new DateOnly(1975, 12, 12), "Российская Федерация", "Ираида", "67 20 133765", "Владимировна", "Коновалова" },
                { 6, new DateOnly(2001, 10, 4), "Республика Казахстан", "Бахыт", "N81245210", "Омарулы", "Мустафин" },
                { 7, new DateOnly(1987, 9, 8), "Республика Казахстан", "Айару", "N24715589", "Мадикызы", "Богенбай" },
                { 8, new DateOnly(2004, 5, 29), "Республика Казахстан", "Нурмагамбет", "N37281641", null, "Шопак" },
                { 9, new DateOnly(1994, 2, 1), "Украина", "Виталий", "AE574690", "Тарасович", "Кожемятько" },
                { 10, new DateOnly(2000, 5, 30), "Украина", "Аксана", "BB591645", "Петровна", "Коваленко" }
            });

        migrationBuilder.InsertData(
            table: "RoomTypes",
            columns: new[] { "Id", "Area", "Beds", "Category", "DayPrice", "HasBath", "HasShower" },
            values: new object[,]
            {
                { 1, 15.0, 1u, 0, 2000.0, false, false },
                { 2, 19.0, 1u, 0, 2600.0, false, true },
                { 3, 22.0, 1u, 1, 3000.0, false, true },
                { 4, 25.0, 1u, 1, 3200.0, true, true },
                { 5, 28.0, 2u, 1, 3600.0, true, true },
                { 6, 32.0, 1u, 3, 4000.0, true, true },
                { 7, 35.0, 2u, 3, 4800.0, true, true },
                { 8, 40.0, 2u, 4, 6000.0, true, true },
                { 9, 50.0, 3u, 4, 8000.0, true, true },
                { 10, 65.0, 3u, 5, 12000.0, true, true }
            });

        migrationBuilder.InsertData(
            table: "Rooms",
            columns: new[] { "Id", "Floor", "HasBalcony", "Number", "RoomTypeId" },
            values: new object[,]
            {
                { 1, 1u, false, 11u, 1 },
                { 2, 2u, true, 21u, 2 },
                { 3, 1u, false, 12u, 3 },
                { 4, 1u, false, 13u, 4 },
                { 5, 2u, true, 22u, 5 },
                { 6, 3u, true, 23u, 6 },
                { 7, 3u, true, 31u, 7 },
                { 8, 2u, true, 11u, 8 },
                { 9, 4u, true, 41u, 9 },
                { 10, 4u, true, 42u, 10 }
            });

        migrationBuilder.InsertData(
            table: "Bookings",
            columns: new[] { "Id", "CheckInDate", "ClientId", "DaysOfStay", "RoomId" },
            values: new object[,]
            {
                { 1, new DateOnly(2026, 2, 13), 2, 2u, 1 },
                { 2, new DateOnly(2026, 1, 25), 1, 5u, 7 },
                { 3, new DateOnly(2026, 1, 22), 8, 4u, 4 },
                { 4, new DateOnly(2026, 3, 10), 3, 3u, 3 },
                { 5, new DateOnly(2026, 4, 15), 9, 1u, 1 },
                { 6, new DateOnly(2025, 12, 28), 5, 7u, 10 },
                { 7, new DateOnly(2025, 6, 10), 10, 2u, 2 },
                { 8, new DateOnly(2026, 4, 14), 4, 3u, 3 },
                { 9, new DateOnly(2025, 11, 25), 7, 5u, 5 },
                { 10, new DateOnly(2026, 9, 1), 6, 4u, 4 },
                { 11, new DateOnly(2026, 7, 21), 1, 3u, 7 },
                { 12, new DateOnly(2026, 3, 31), 5, 4u, 7 },
                { 13, new DateOnly(2026, 2, 12), 8, 5u, 6 },
                { 14, new DateOnly(2026, 3, 17), 6, 5u, 6 },
                { 15, new DateOnly(2026, 8, 3), 3, 5u, 6 }
            });

        migrationBuilder.CreateIndex(
            name: "idx_Bookings_Room_CheckInDate",
            table: "Bookings",
            columns: new[] { "RoomId", "CheckInDate" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Bookings_ClientId",
            table: "Bookings",
            column: "ClientId");

        migrationBuilder.CreateIndex(
            name: "IX_Clients_PassportNumber",
            table: "Clients",
            column: "PassportNumber",
            unique: true);

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
