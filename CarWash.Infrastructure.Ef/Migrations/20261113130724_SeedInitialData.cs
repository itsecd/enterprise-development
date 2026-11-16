using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CarWash.Infrastructure.Ef.Migrations
{
    /// <inheritdoc />
    public partial class SeedInitialData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "FullName", "Phone" },
                values: new object[,]
                {
                    { 1, "Иванов Иван Иванович", "79000000001" },
                    { 2, "Петров Петр Петрович", "79000000002" },
                    { 3, "Сидоров Алексей Олегович", "79000000003" },
                    { 4, "Смирнов Максим Игоревич", "79000000004" },
                    { 5, "Кузнецов Андрей Сергеевич", "79000000005" },
                    { 6, "Попов Дмитрий Андреевич", "79000000006" },
                    { 7, "Волков Роман Ильич", "79000000007" },
                    { 8, "Соколов Павел Андреевич", "79000000008" },
                    { 9, "Орлов Олег Сергеевич", "79000000009" },
                    { 10, "Морозов Антон Петрович", "79000000010" }
                });

            migrationBuilder.InsertData(
                table: "WashServices",
                columns: new[] { "Id", "Category", "Cost", "DurationMinutes", "Name" },
                values: new object[,]
                {
                    { 1, 0, 500m, 30, "Стандартная мойка" },
                    { 2, 0, 400m, 20, "Мойка кузова" },
                    { 3, 0, 800m, 40, "Комплексная мойка" },
                    { 4, 3, 1000m, 45, "Мойка внедорожника" },
                    { 5, 1, 1500m, 60, "Мойка грузовика" },
                    { 6, 4, 300m, 15, "Мойка мотоцикла" },
                    { 7, 2, 900m, 35, "Мойка микроавтобуса" },
                    { 8, 0, 600m, 30, "Уборка салона" },
                    { 9, 0, 200m, 10, "Мойка колёс" },
                    { 10, 0, 700m, 25, "Нанесение воска" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "CustomerId", "LicensePlate" },
                values: new object[,]
                {
                    { 1, "Toyota", 1, "A001AA" },
                    { 2, "Kia", 2, "A002AA" },
                    { 3, "Lada", 3, "A003AA" },
                    { 4, "BMW", 4, "A004AA" },
                    { 5, "Audi", 5, "A005AA" },
                    { 6, "Ford", 6, "A006AA" },
                    { 7, "Skoda", 7, "A007AA" },
                    { 8, "Renault", 8, "A008AA" },
                    { 9, "Hyundai", 9, "A009AA" },
                    { 10, "Volkswagen", 10, "A010AA" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "BoxNumber", "CarId", "CustomerId", "StartTime", "WashServiceId" },
                values: new object[,]
                {
                    { 1, 1, 1, 1, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 2, 1, 1, 1, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 3, 1, 1, 1, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 4, 1, 1, 1, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 5, 1, 1, 1, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 6, 2, 2, 2, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 7, 2, 2, 2, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 8, 2, 2, 2, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 9, 2, 2, 2, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 10, 3, 3, 3, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 11, 3, 3, 3, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 12, 3, 3, 3, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 13, 4, 4, 4, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 14, 4, 4, 4, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 15, 5, 5, 5, new DateTime(2025, 12, 31, 12, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 16, 1, 1, 1, new DateTime(2026, 1, 1, 11, 55, 0, 0, DateTimeKind.Unspecified), 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "WashServices",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "WashServices",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "WashServices",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "WashServices",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "WashServices",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "WashServices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "WashServices",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "WashServices",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "WashServices",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "WashServices",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 5);
        }
    }
}
