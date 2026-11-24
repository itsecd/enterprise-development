using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Carwash.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class Fixed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_cars_clients_client_id",
                table: "cars");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_cars_car_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_clients_client_id",
                table: "orders");

            migrationBuilder.DropForeignKey(
                name: "FK_orders_services_service_id",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_services",
                table: "services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_orders",
                table: "orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_clients",
                table: "clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_cars",
                table: "cars");

            migrationBuilder.RenameTable(
                name: "services",
                newName: "Services");

            migrationBuilder.RenameTable(
                name: "orders",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "clients",
                newName: "Clients");

            migrationBuilder.RenameTable(
                name: "cars",
                newName: "Cars");

            migrationBuilder.RenameColumn(
                name: "price",
                table: "Services",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Services",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "category",
                table: "Services",
                newName: "Category");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Services",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "duration_in_minutes",
                table: "Services",
                newName: "DurationInMinutes");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Orders",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "start_time",
                table: "Orders",
                newName: "StartTime");

            migrationBuilder.RenameColumn(
                name: "service_id",
                table: "Orders",
                newName: "ServiceId");

            migrationBuilder.RenameColumn(
                name: "client_id",
                table: "Orders",
                newName: "ClientId");

            migrationBuilder.RenameColumn(
                name: "car_id",
                table: "Orders",
                newName: "CarId");

            migrationBuilder.RenameColumn(
                name: "box_number",
                table: "Orders",
                newName: "BoxNumber");

            migrationBuilder.RenameIndex(
                name: "IX_orders_service_id",
                table: "Orders",
                newName: "IX_Orders_ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_orders_client_id",
                table: "Orders",
                newName: "IX_Orders_ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_orders_car_id",
                table: "Orders",
                newName: "IX_Orders_CarId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Clients",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "phone_number",
                table: "Clients",
                newName: "PhoneNumber");

            migrationBuilder.RenameColumn(
                name: "full_name",
                table: "Clients",
                newName: "FullName");

            migrationBuilder.RenameColumn(
                name: "brand",
                table: "Cars",
                newName: "Brand");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Cars",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "license_plate",
                table: "Cars",
                newName: "LicensePlate");

            migrationBuilder.RenameColumn(
                name: "client_id",
                table: "Cars",
                newName: "ClientId");

            migrationBuilder.RenameIndex(
                name: "IX_cars_client_id",
                table: "Cars",
                newName: "IX_Cars_ClientId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Services",
                table: "Services",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Clients",
                table: "Clients",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "Id");

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "FullName", "PhoneNumber" },
                values: new object[,]
                {
                    { 8, "Ольга Морозова", "+79990000008" },
                    { 9, "Николай Новиков", "+79990000009" },
                    { 10, "Татьяна Соколова", "+799900000010" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Category", "DurationInMinutes", "Name", "Price" },
                values: new object[,]
                {
                    { 6, 0, 45, "Мойка двигателя", 1200m },
                    { 7, 1, 120, "Химчистка салона", 5000m },
                    { 8, 2, 180, "Полировка кузова", 6000m },
                    { 9, 0, 40, "Мойка днища", 1000m },
                    { 10, 2, 240, "Нанокерамика", 7000m }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "Brand", "ClientId", "LicensePlate" },
                values: new object[,]
                {
                    { 9, "Skoda", 8, "Р999РР" },
                    { 10, "Kia", 9, "Т101ТТ" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "BoxNumber", "CarId", "ClientId", "ServiceId", "StartTime" },
                values: new object[,]
                {
                    { 26, 4, 7, 7, 8, new DateTime(2026, 9, 22, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 24, 2, 9, 8, 6, new DateTime(2026, 9, 21, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 25, 3, 10, 9, 7, new DateTime(2026, 9, 21, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 27, 5, 9, 8, 9, new DateTime(2026, 9, 22, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 28, 3, 10, 9, 10, new DateTime(2026, 9, 23, 13, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Clients_ClientId",
                table: "Cars",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Cars_CarId",
                table: "Orders",
                column: "CarId",
                principalTable: "Cars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Clients_ClientId",
                table: "Orders",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Services_ServiceId",
                table: "Orders",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Clients_ClientId",
                table: "Cars");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Cars_CarId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Clients_ClientId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Services_ServiceId",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Services",
                table: "Services");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                table: "Orders");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Clients",
                table: "Clients");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 28);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.RenameTable(
                name: "Services",
                newName: "services");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "orders");

            migrationBuilder.RenameTable(
                name: "Clients",
                newName: "clients");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "cars");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "services",
                newName: "price");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "services",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Category",
                table: "services",
                newName: "category");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "services",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DurationInMinutes",
                table: "services",
                newName: "duration_in_minutes");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "orders",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "StartTime",
                table: "orders",
                newName: "start_time");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "orders",
                newName: "service_id");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "orders",
                newName: "client_id");

            migrationBuilder.RenameColumn(
                name: "CarId",
                table: "orders",
                newName: "car_id");

            migrationBuilder.RenameColumn(
                name: "BoxNumber",
                table: "orders",
                newName: "box_number");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ServiceId",
                table: "orders",
                newName: "IX_orders_service_id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_ClientId",
                table: "orders",
                newName: "IX_orders_client_id");

            migrationBuilder.RenameIndex(
                name: "IX_Orders_CarId",
                table: "orders",
                newName: "IX_orders_car_id");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "clients",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "PhoneNumber",
                table: "clients",
                newName: "phone_number");

            migrationBuilder.RenameColumn(
                name: "FullName",
                table: "clients",
                newName: "full_name");

            migrationBuilder.RenameColumn(
                name: "Brand",
                table: "cars",
                newName: "brand");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "cars",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "LicensePlate",
                table: "cars",
                newName: "license_plate");

            migrationBuilder.RenameColumn(
                name: "ClientId",
                table: "cars",
                newName: "client_id");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_ClientId",
                table: "cars",
                newName: "IX_cars_client_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_services",
                table: "services",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_orders",
                table: "orders",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_clients",
                table: "clients",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_cars",
                table: "cars",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_cars_clients_client_id",
                table: "cars",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_cars_car_id",
                table: "orders",
                column: "car_id",
                principalTable: "cars",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_clients_client_id",
                table: "orders",
                column: "client_id",
                principalTable: "clients",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_orders_services_service_id",
                table: "orders",
                column: "service_id",
                principalTable: "services",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
