using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AutoService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "mechanics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Passport = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Specialization = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Experience = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_mechanics", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "work_types",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Category = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Price = table.Column<decimal>(type: "numeric(10,2)", precision: 10, scale: 2, nullable: false),
                    Duration = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_work_types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LicensePlate = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    Brand = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Model = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Year = table.Column<int>(type: "integer", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_cars_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    CarId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClientId = table.Column<Guid>(type: "uuid", nullable: false),
                    MechanicId = table.Column<Guid>(type: "uuid", nullable: false),
                    AcceptedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_orders_cars_CarId",
                        column: x => x.CarId,
                        principalTable: "cars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_orders_mechanics_MechanicId",
                        column: x => x.MechanicId,
                        principalTable: "mechanics",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "order_works",
                columns: table => new
                {
                    order_id = table.Column<Guid>(type: "uuid", nullable: false),
                    work_type_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order_works", x => new { x.order_id, x.work_type_id });
                    table.ForeignKey(
                        name: "FK_order_works_orders_order_id",
                        column: x => x.order_id,
                        principalTable: "orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_works_work_types_work_type_id",
                        column: x => x.work_type_id,
                        principalTable: "work_types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "clients",
                columns: new[] { "Id", "FullName", "Phone" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000001"), "Иванов Иван Иванович", "+71234567890" },
                    { new Guid("00000000-0000-0000-0000-000000000002"), "Симонов Илья Геогриевич", "+71234567891" },
                    { new Guid("00000000-0000-0000-0000-000000000003"), "Кораблев Гриша Александрович", "+71234567892" },
                    { new Guid("00000000-0000-0000-0000-000000000004"), "Толстой Илья Сергеевич", "+71234567893" },
                    { new Guid("00000000-0000-0000-0000-000000000005"), "Кольцова Александра Тимофеевна", "+71234567894" },
                    { new Guid("00000000-0000-0000-0000-000000000006"), "Кулова Диана Сергеевна", "+71234567895" },
                    { new Guid("00000000-0000-0000-0000-000000000007"), "Кикиморов Петр Иванович", "+71234567896" },
                    { new Guid("00000000-0000-0000-0000-000000000008"), "Кощеев Кощей Кощеевич", "+71234567897" },
                    { new Guid("00000000-0000-0000-0000-000000000009"), "Кефирова Яга Сергеевна", "+71234567898" },
                    { new Guid("00000000-0000-0000-0000-00000000000a"), "Андреев Андрей Андреевич", "+71234567899" }
                });

            migrationBuilder.InsertData(
                table: "mechanics",
                columns: new[] { "Id", "Experience", "FullName", "Passport", "Specialization" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000016"), 8, "Романова Ольга Владимировна", "7777 123243", "Transmission" },
                    { new Guid("00000000-0000-0000-0000-000000000017"), 6, "Гусеев Гусь Гусевич", "6666 213123", "Electrical" },
                    { new Guid("00000000-0000-0000-0000-000000000018"), 14, "Комаров Комар Комарович", "5555 324255", "Body" },
                    { new Guid("00000000-0000-0000-0000-000000000019"), 5, "Мошкова Мошка Мошковна", "4444 543546", "Diagnostics" },
                    { new Guid("00000000-0000-0000-0000-00000000001a"), 9, "Тараканов Таракан Тараканович", "3333 456546", "Suspension" },
                    { new Guid("00000000-0000-0000-0000-00000000001b"), 7, "Тараканов Торт Тортович", "2222 879789", "Brakes" },
                    { new Guid("00000000-0000-0000-0000-00000000001c"), 12, "Горева Зима Владимировна", "1111 674667", "Painting" },
                    { new Guid("00000000-0000-0000-0000-00000000001d"), 17, "Веснова Весна Владимировна", "9999 812812", "Engine" },
                    { new Guid("00000000-0000-0000-0000-00000000001e"), 3, "Родионов Михаил Петрович", "0000 524267", "Diagnostics" },
                    { new Guid("22222222-2222-2222-2222-222222222222"), 11, "Кириллов Кирилл Кириллович", "8888 123345", "Engine" }
                });

            migrationBuilder.InsertData(
                table: "work_types",
                columns: new[] { "Id", "Category", "Duration", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-000000000029"), "Body", new TimeSpan(0, 4, 0, 0, 0), "Правка вмятины крыла", 6500m },
                    { new Guid("00000000-0000-0000-0000-00000000002a"), "Diagnostics", new TimeSpan(0, 1, 0, 0, 0), "Диагностика ходовой", 3200m },
                    { new Guid("00000000-0000-0000-0000-00000000002b"), "Brakes", new TimeSpan(0, 2, 0, 0, 0), "Замена тормозных дисков", 8900m },
                    { new Guid("00000000-0000-0000-0000-00000000002c"), "Engine", new TimeSpan(0, 12, 0, 0, 0), "Ремонт ГБЦ", 42000m },
                    { new Guid("00000000-0000-0000-0000-00000000002d"), "Engine", new TimeSpan(0, 5, 0, 0, 0), "Замена ремня ГРМ", 18500m },
                    { new Guid("00000000-0000-0000-0000-00000000002e"), "Diagnostics", new TimeSpan(0, 0, 50, 0, 0), "Компьютерная диагностика", 2400m },
                    { new Guid("00000000-0000-0000-0000-00000000002f"), "Electrical", new TimeSpan(0, 0, 20, 0, 0), "Замена АКБ", 1500m },
                    { new Guid("00000000-0000-0000-0000-000000000030"), "Painting", new TimeSpan(0, 8, 0, 0, 0), "Покраска бампера", 16000m },
                    { new Guid("00000000-0000-0000-0000-000000000031"), "Suspension", new TimeSpan(0, 3, 0, 0, 0), "Замена амортизаторов", 9500m },
                    { new Guid("00000000-0000-0000-0000-000000000032"), "Transmission", new TimeSpan(0, 10, 0, 0, 0), "Ремонт МКПП", 35000m }
                });

            migrationBuilder.InsertData(
                table: "cars",
                columns: new[] { "Id", "Brand", "ClientId", "LicensePlate", "Model", "Year" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-00000000000b"), "Skoda", new Guid("00000000-0000-0000-0000-000000000001"), "Т812КМ 78", "Octavia", 2000 },
                    { new Guid("00000000-0000-0000-0000-00000000000c"), "Kia", new Guid("00000000-0000-0000-0000-000000000002"), "Н217УВ 47", "Rio", 2001 },
                    { new Guid("00000000-0000-0000-0000-00000000000d"), "Lada", new Guid("00000000-0000-0000-0000-000000000003"), "М903АС 99", "Vesta", 2002 },
                    { new Guid("00000000-0000-0000-0000-00000000000e"), "Hyundai", new Guid("00000000-0000-0000-0000-000000000004"), "Е441РТ 67", "Creta", 2003 },
                    { new Guid("00000000-0000-0000-0000-00000000000f"), "Mazda", new Guid("00000000-0000-0000-0000-000000000005"), "В562НХ 52", "CX-5", 2004 },
                    { new Guid("00000000-0000-0000-0000-000000000010"), "Chery", new Guid("00000000-0000-0000-0000-000000000006"), "К119МУ 178", "Tiggo 7", 2005 },
                    { new Guid("00000000-0000-0000-0000-000000000011"), "Toyota", new Guid("00000000-0000-0000-0000-000000000007"), "О774ВС 23", "Camry", 2006 },
                    { new Guid("00000000-0000-0000-0000-000000000012"), "Renault", new Guid("00000000-0000-0000-0000-000000000008"), "С528ТЕ 64", "Duster", 2007 },
                    { new Guid("00000000-0000-0000-0000-000000000013"), "VW", new Guid("00000000-0000-0000-0000-000000000009"), "У391АК 16", "Polo", 2008 },
                    { new Guid("00000000-0000-0000-0000-000000000014"), "Nissan", new Guid("00000000-0000-0000-0000-00000000000a"), "Р527ОМ 34", "Qashqai", 2026 }
                });

            migrationBuilder.InsertData(
                table: "orders",
                columns: new[] { "Id", "AcceptedAt", "CarId", "ClientId", "IssuedAt", "MechanicId" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-00000000003e"), new DateTime(2026, 9, 20, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-00000000000c"), new Guid("00000000-0000-0000-0000-000000000002"), new DateTime(2026, 9, 20, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000017") },
                    { new Guid("00000000-0000-0000-0000-00000000003f"), new DateTime(2026, 9, 24, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-00000000000d"), new Guid("00000000-0000-0000-0000-000000000003"), new DateTime(2026, 9, 24, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-00000000001a") },
                    { new Guid("00000000-0000-0000-0000-000000000040"), new DateTime(2026, 9, 27, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-00000000000e"), new Guid("00000000-0000-0000-0000-000000000004"), new DateTime(2026, 9, 27, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("00000000-0000-0000-0000-000000000041"), new DateTime(2026, 9, 30, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-00000000000f"), new Guid("00000000-0000-0000-0000-000000000005"), new DateTime(2026, 10, 1, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000016") },
                    { new Guid("00000000-0000-0000-0000-000000000042"), new DateTime(2026, 10, 3, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000010"), new Guid("00000000-0000-0000-0000-000000000006"), new DateTime(2026, 10, 4, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000018") },
                    { new Guid("00000000-0000-0000-0000-000000000043"), new DateTime(2026, 10, 5, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000011"), new Guid("00000000-0000-0000-0000-000000000007"), new DateTime(2026, 10, 7, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-00000000001d") },
                    { new Guid("00000000-0000-0000-0000-000000000044"), new DateTime(2026, 10, 8, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000012"), new Guid("00000000-0000-0000-0000-000000000008"), null, new Guid("00000000-0000-0000-0000-00000000001b") },
                    { new Guid("00000000-0000-0000-0000-000000000045"), new DateTime(2026, 10, 10, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000013"), new Guid("00000000-0000-0000-0000-000000000009"), null, new Guid("00000000-0000-0000-0000-00000000001e") },
                    { new Guid("00000000-0000-0000-0000-000000000046"), new DateTime(2026, 10, 11, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-000000000014"), new Guid("00000000-0000-0000-0000-00000000000a"), null, new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("00000000-0000-0000-0000-000000000047"), new DateTime(2026, 10, 9, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-00000000000b"), new Guid("00000000-0000-0000-0000-000000000001"), null, new Guid("22222222-2222-2222-2222-222222222222") },
                    { new Guid("00000000-0000-0000-0000-000000000048"), new DateTime(2026, 10, 10, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-00000000000b"), new Guid("00000000-0000-0000-0000-000000000001"), null, new Guid("00000000-0000-0000-0000-000000000019") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new DateTime(2026, 9, 17, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("00000000-0000-0000-0000-00000000000b"), new Guid("00000000-0000-0000-0000-000000000001"), new DateTime(2026, 9, 17, 23, 5, 10, 0, DateTimeKind.Utc), new Guid("22222222-2222-2222-2222-222222222222") }
                });

            migrationBuilder.InsertData(
                table: "order_works",
                columns: new[] { "order_id", "work_type_id" },
                values: new object[,]
                {
                    { new Guid("00000000-0000-0000-0000-00000000003e"), new Guid("00000000-0000-0000-0000-00000000002f") },
                    { new Guid("00000000-0000-0000-0000-00000000003f"), new Guid("00000000-0000-0000-0000-00000000002b") },
                    { new Guid("00000000-0000-0000-0000-000000000040"), new Guid("00000000-0000-0000-0000-00000000002e") },
                    { new Guid("00000000-0000-0000-0000-000000000041"), new Guid("00000000-0000-0000-0000-000000000032") },
                    { new Guid("00000000-0000-0000-0000-000000000042"), new Guid("00000000-0000-0000-0000-000000000030") },
                    { new Guid("00000000-0000-0000-0000-000000000043"), new Guid("00000000-0000-0000-0000-00000000002c") },
                    { new Guid("00000000-0000-0000-0000-000000000044"), new Guid("00000000-0000-0000-0000-00000000002b") },
                    { new Guid("00000000-0000-0000-0000-000000000044"), new Guid("00000000-0000-0000-0000-00000000002f") },
                    { new Guid("00000000-0000-0000-0000-000000000045"), new Guid("00000000-0000-0000-0000-00000000002a") },
                    { new Guid("00000000-0000-0000-0000-000000000045"), new Guid("00000000-0000-0000-0000-00000000002e") },
                    { new Guid("00000000-0000-0000-0000-000000000046"), new Guid("00000000-0000-0000-0000-00000000002d") },
                    { new Guid("00000000-0000-0000-0000-000000000047"), new Guid("00000000-0000-0000-0000-000000000029") },
                    { new Guid("00000000-0000-0000-0000-000000000048"), new Guid("00000000-0000-0000-0000-00000000002a") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-000000000029") },
                    { new Guid("11111111-1111-1111-1111-111111111111"), new Guid("00000000-0000-0000-0000-00000000002a") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_cars_ClientId",
                table: "cars",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_cars_LicensePlate",
                table: "cars",
                column: "LicensePlate",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_clients_Phone",
                table: "clients",
                column: "Phone",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_mechanics_Passport",
                table: "mechanics",
                column: "Passport",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_order_works_work_type_id",
                table: "order_works",
                column: "work_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_orders_CarId",
                table: "orders",
                column: "CarId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_ClientId",
                table: "orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_orders_MechanicId",
                table: "orders",
                column: "MechanicId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order_works");

            migrationBuilder.DropTable(
                name: "orders");

            migrationBuilder.DropTable(
                name: "work_types");

            migrationBuilder.DropTable(
                name: "cars");

            migrationBuilder.DropTable(
                name: "mechanics");

            migrationBuilder.DropTable(
                name: "clients");
        }
    }
}
