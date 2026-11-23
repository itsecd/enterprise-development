using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hotel.Infrastructure.Migrations
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
                    passport_number = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    surname = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    patronymic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                    citizenship = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "room_types",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    area = table.Column<double>(type: "double", nullable: false),
                    beds = table.Column<uint>(type: "int unsigned", nullable: false),
                    has_bath = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    has_shower = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    day_price = table.Column<double>(type: "double", nullable: false),
                    category = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_room_types", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "rooms",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    number = table.Column<uint>(type: "int unsigned", nullable: false),
                    floor = table.Column<uint>(type: "int unsigned", nullable: false),
                    has_balcony = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    room_type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_rooms", x => x.id);
                    table.ForeignKey(
                        name: "fk_rooms_room_types_room_type_id",
                        column: x => x.room_type_id,
                        principalTable: "room_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "bookings",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    check_in_date = table.Column<DateOnly>(type: "date", nullable: false),
                    days_of_stay = table.Column<uint>(type: "int unsigned", nullable: false),
                    room_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_bookings", x => x.id);
                    table.ForeignKey(
                        name: "fk_bookings_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_bookings_rooms_room_id",
                        column: x => x.room_id,
                        principalTable: "rooms",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "clients",
                columns: new[] { "id", "birth_date", "citizenship", "name", "passport_number", "patronymic", "surname" },
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
                table: "room_types",
                columns: new[] { "id", "area", "beds", "category", "day_price", "has_bath", "has_shower" },
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
                table: "rooms",
                columns: new[] { "id", "floor", "has_balcony", "number", "room_type_id" },
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
                table: "bookings",
                columns: new[] { "id", "check_in_date", "client_id", "days_of_stay", "room_id" },
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
                table: "bookings",
                columns: new[] { "room_id", "check_in_date" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_bookings_client_id",
                table: "bookings",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_passport_number",
                table: "clients",
                column: "passport_number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_rooms_room_type_id",
                table: "rooms",
                column: "room_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookings");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "rooms");

            migrationBuilder.DropTable(
                name: "room_types");
        }
    }
}
