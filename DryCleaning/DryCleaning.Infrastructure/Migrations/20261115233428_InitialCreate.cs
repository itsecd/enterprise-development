using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DryCleaning.Infrastructure.Migrations
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
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CleaningType = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(65,30)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Material = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    AcceptDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    ExeDays = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CleaningType", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Сухая химчистка", "Верхняя одежда", 1500m },
                    { 2, "Сухая чистка", "Деловые костюмы", 1800m },
                    { 3, "Деликатная чистка", "Платья и юбки", 1100m },
                    { 4, "Аквачистка", "Рубашки и блузы", 600m },
                    { 5, "Бережная стирка", "Трикотаж и свитера", 750m },
                    { 6, "Чистка с пропиткой", "Кожаные изделия", 3500m },
                    { 7, "Комплексная чистка", "Обувь", 2500m },
                    { 8, "Стирка и глажка", "Постельное белье", 500m },
                    { 9, "Паровая обработка", "Шторы и гардины", 950m },
                    { 10, "Аквачистка с сушкой пера", "Пуховики", 2200m }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "FullName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Иванов Иван Иванович", "+79231112233" },
                    { 2, "Петров Петр Петрович", "+79071023344" },
                    { 3, "Сидорова Анна Сергеевна", "+79103334455" },
                    { 4, "Смирнов Алексей Дмитриевич", "+79004445566" },
                    { 5, "Кузнецова Елена Владимировна", "+79005556677" },
                    { 6, "Попов Максим Олегович", "+79056612788" },
                    { 7, "Васильева Ольга Николаевна", "+79007778899" },
                    { 8, "Новиков Артем Игоревич", "+79278889900" },
                    { 9, "Морозова Екатерина Павловна", "+79889990011" },
                    { 10, "Федоров Сергей Александрович", "+79140001122" }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CategoryId", "Material", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Пух", "Пальто демисезонное" },
                    { 2, 2, "Шерсть", "Костюм" },
                    { 3, 3, "Шелк", "Вечернее платье" },
                    { 4, 4, "Хлопок", "Белая рубашка" },
                    { 5, 5, "Шерсть", "Шерстяной свитер" },
                    { 6, 6, "Натуральная кожа", "Косуха" },
                    { 7, 7, "Замша", "Замшевые ботинки" },
                    { 8, 8, "Хлопок", "Постельное бельё" },
                    { 9, 9, "Лен", "Тюль" },
                    { 10, 10, "Полиэстер/Пух", "Зимний пуховик" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AcceptDate", "ClientId", "ExeDays", "ItemId", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 9, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 7, 6, 3 },
                    { 2, new DateTime(2026, 9, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 5, 7, 2 },
                    { 3, new DateTime(2026, 9, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, 14, 1, 1 },
                    { 4, new DateTime(2026, 9, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 4, 2, 1 },
                    { 5, new DateTime(2026, 9, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 2, 4, 1 },
                    { 6, new DateTime(2026, 9, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 3, 3 },
                    { 7, new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, 3, 8, 3 },
                    { 8, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, 10, 10, 3 },
                    { 9, new DateTime(2026, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, 12, 5, 2 },
                    { 10, new DateTime(2025, 8, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, 4, 9, 3 },
                    { 11, new DateTime(2026, 9, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, 1, 4, 1 },
                    { 12, new DateTime(2026, 8, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, 6, 1, 3 },
                    { 13, new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 5, 6, 3 },
                    { 14, new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 5, 7, 3 },
                    { 15, new DateTime(2025, 7, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, 5, 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ClientId",
                table: "Orders",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ItemId",
                table: "Orders",
                column: "ItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
