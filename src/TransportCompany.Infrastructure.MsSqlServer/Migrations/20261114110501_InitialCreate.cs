using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TransportCompany.Infrastructure.MsSqlServer.Migrations
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
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassportNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Experience = table.Column<int>(type: "int", nullable: false),
                    License = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "VehicleModels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BodyType = table.Column<int>(type: "int", nullable: false),
                    BodyVolume = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehicleModels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LicensePlate = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    LoadCapacity = table.Column<double>(type: "float", nullable: true),
                    ModelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Vehicles_VehicleModels_ModelId",
                        column: x => x.ModelId,
                        principalTable: "VehicleModels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<int>(type: "int", nullable: false),
                    VehicleId = table.Column<int>(type: "int", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    Origin = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CargoWeight = table.Column<double>(type: "float", nullable: true),
                    TransportationCost = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    Status = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Drivers_DriverId",
                        column: x => x.DriverId,
                        principalTable: "Drivers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trips_Vehicles_VehicleId",
                        column: x => x.VehicleId,
                        principalTable: "Vehicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Name", "Phone" },
                values: new object[,]
                {
                    { 1, "Donald Truck", "88005553535" },
                    { 2, "Cat Dog", "89991112233" },
                    { 3, "John Smith", "89001234567" },
                    { 4, "Emma Watson", "89112345678" },
                    { 5, "Michael Brown", "89223456789" },
                    { 6, "Sarah Johnson", "89334567890" },
                    { 7, "David Miller", "89445678901" },
                    { 8, "Lisa Davis", "89556789012" },
                    { 9, "James Wilson", "89667890123" },
                    { 10, "Maria Garcia", "89778901234" }
                });

            migrationBuilder.InsertData(
                table: "Drivers",
                columns: new[] { "Id", "Experience", "FullName", "License", "PassportNumber" },
                values: new object[,]
                {
                    { 1, 5, "John Johnson", 6, "1234 567890" },
                    { 2, 8, "Mike Tyson", 8, "2345 678901" },
                    { 3, 3, "Conor McGregor", 8, "3456 789012" },
                    { 4, 15, "Anderson Silva", 8, "4567 890123" },
                    { 5, 10, "Georges St-Pierre", 8, "5678 901234" },
                    { 6, 7, "Jon Jones", 8, "6789 012345" },
                    { 7, 20, "Daniel Cormier", 7, "7890 123456" },
                    { 8, 6, "Stipe Miocic", 6, "8901 234567" },
                    { 9, 4, "Israel Adesanya", 0, "9012 345678" },
                    { 10, 1, "Aaaaaa Uuuuuuuu", 0, "1111 345678" }
                });

            migrationBuilder.InsertData(
                table: "VehicleModels",
                columns: new[] { "Id", "BodyType", "BodyVolume" },
                values: new object[,]
                {
                    { 1, 0, 82.0 },
                    { 2, 1, 90.0 },
                    { 3, 2, 75.0 },
                    { 4, 3, 70.0 },
                    { 5, 4, 150.0 },
                    { 6, 5, 120.0 },
                    { 7, 6, 110.0 },
                    { 8, 7, 130.0 },
                    { 9, 8, 200.0 },
                    { 10, 9, 180.0 }
                });

            migrationBuilder.InsertData(
                table: "Vehicles",
                columns: new[] { "Id", "LicensePlate", "LoadCapacity", "ModelId" },
                values: new object[,]
                {
                    { 1, "X005XX05", 20.0, 1 },
                    { 2, "E001MP777", 15.0, 2 },
                    { 3, "A123BC77", 25.5, 3 },
                    { 4, "B456DE99", 18.0, 4 },
                    { 5, "C789FG11", 30.0, 2 },
                    { 6, "D012HI33", 12.5, 6 },
                    { 7, "E345JK55", 22.0, 2 },
                    { 8, "F678LM77", 28.0, 2 },
                    { 9, "G901NO88", 16.0, 9 },
                    { 10, "H234PQ00", 35.0, 10 }
                });

            migrationBuilder.InsertData(
                table: "Trips",
                columns: new[] { "Id", "CargoWeight", "ClientId", "Date", "Destination", "DriverId", "Origin", "Status", "TransportationCost", "VehicleId" },
                values: new object[,]
                {
                    { 1, 15.0, 1, new DateTime(2026, 2, 9, 12, 0, 0, 0, DateTimeKind.Utc), "Казань", 1, "Москва", 1, 15000m, 1 },
                    { 2, 12.5, 2, new DateTime(2026, 2, 14, 12, 0, 0, 0, DateTimeKind.Utc), "Уфа", 2, "Самара", 2, 12000m, 2 },
                    { 3, 20.0, 3, new DateTime(2026, 2, 19, 12, 0, 0, 0, DateTimeKind.Utc), "Пермь", 2, "Уфа", 0, 18000m, 2 },
                    { 4, 8.5, 4, new DateTime(2026, 2, 24, 12, 0, 0, 0, DateTimeKind.Utc), "Краснодар", 3, "Ростов", 2, 9000m, 4 },
                    { 5, 25.0, 5, new DateTime(2026, 2, 27, 12, 0, 0, 0, DateTimeKind.Utc), "Москва", 3, "Сочи", 1, 22000m, 3 },
                    { 6, 10.0, 6, new DateTime(2026, 3, 2, 12, 0, 0, 0, DateTimeKind.Utc), "Омск", 4, "Новосибирск", 0, 11000m, 6 },
                    { 7, 18.5, 7, new DateTime(2026, 3, 4, 12, 0, 0, 0, DateTimeKind.Utc), "Тюмень", 4, "Омск", 0, 16500m, 7 },
                    { 8, 22.0, 8, new DateTime(2026, 2, 21, 12, 0, 0, 0, DateTimeKind.Utc), "Екатеринбург", 4, "Тюмень", 2, 20000m, 7 },
                    { 9, 14.0, 9, new DateTime(2026, 2, 28, 12, 0, 0, 0, DateTimeKind.Utc), "Челябинск", 5, "Екатеринбург", 1, 13500m, 7 },
                    { 10, 13.0, 10, new DateTime(2026, 2, 27, 12, 0, 0, 0, DateTimeKind.Utc), "Астрахань", 6, "Волгоград", 1, 12500m, 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trips_ClientId",
                table: "Trips",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DriverId",
                table: "Trips",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_VehicleId",
                table: "Trips",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_ModelId",
                table: "Vehicles",
                column: "ModelId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Drivers");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "VehicleModels");
        }
    }
}
