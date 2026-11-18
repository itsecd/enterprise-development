using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DryCleaner.Infrastructure.EfCore.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "text", nullable: false),
                    Patronymic = table.Column<string>(type: "text", nullable: true),
                    LastName = table.Column<string>(type: "text", nullable: false),
                    PhoneNumber = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    RecommendedCleaning = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false),
                    Material = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_ItemCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ItemCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CustomerId = table.Column<int>(type: "integer", nullable: false),
                    ItemId = table.Column<int>(type: "integer", nullable: false),
                    AcceptanceDate = table.Column<DateOnly>(type: "date", nullable: false),
                    CompletionDays = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "FirstName", "LastName", "Patronymic", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Евлампий", "Евлампьев", "Евлампиевич", "+79001112233" },
                    { 2, "Александр", "Сергеев", "Сильвестрович", "+79000000000" },
                    { 3, "Александр", "Сергеев", "Александрович", "+79000000001" },
                    { 4, "Виктор", "Баринов", "Петрович", "+78005553535" },
                    { 5, "Уолтер", "Уайт", null, "+15430000567" },
                    { 6, "Майк", "Вазовский", null, "+23334444555" },
                    { 7, "Сол", "Гудман", null, "+77777777777" },
                    { 8, "Антон", "Чигур", null, "+66666666666" },
                    { 9, "Джесси", "Пинкман", null, "+977712935030" },
                    { 10, "Вито", "Скалетта", null, "+79004301983" }
                });

            migrationBuilder.InsertData(
                table: "ItemCategories",
                columns: new[] { "Id", "Name", "Price", "RecommendedCleaning" },
                values: new object[,]
                {
                    { 1, "Верхняя одежда", 1500m, 0 },
                    { 2, "Пальто", 2000m, 0 },
                    { 3, "Платье", 1200m, 0 },
                    { 4, "Костюм", 1800m, 0 },
                    { 5, "Рубашка", 500m, 3 },
                    { 6, "Брюки", 800m, 3 },
                    { 7, "Постельное бельё", 700m, 2 },
                    { 8, "Спортивная одежда", 600m, 1 },
                    { 9, "Куртка", 1700m, 0 },
                    { 10, "Шуба", 5000m, 0 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "CategoryId", "Material", "Name" },
                values: new object[,]
                {
                    { 1, 2, "Шерсть", "Пальто" },
                    { 2, 4, "Хлопок", "Пиджак" },
                    { 3, 3, "Шёлк", "Платье свадебное" },
                    { 4, 5, "Хлопок", "Рубашка в клеточку" },
                    { 5, 6, "Шерсть", "Брюки" },
                    { 6, 9, "Кожа", "Куртка" },
                    { 7, 10, "Мех", "Шуба норковая" },
                    { 8, 4, "Шерсть", "Костюм тройка" },
                    { 9, 8, "Синтетика", "Футбольная форма" },
                    { 10, 7, "Хлопок", "Постельное бельё" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AcceptanceDate", "CompletionDays", "CustomerId", "ItemId", "Status" },
                values: new object[,]
                {
                    { 1, new DateOnly(2025, 4, 10), 5, 1, 1, 2 },
                    { 2, new DateOnly(2025, 5, 15), 7, 2, 2, 2 },
                    { 3, new DateOnly(2025, 6, 20), 3, 3, 3, 2 },
                    { 4, new DateOnly(2025, 7, 25), 2, 1, 2, 2 },
                    { 5, new DateOnly(2025, 8, 30), 4, 4, 4, 2 },
                    { 6, new DateOnly(2025, 10, 5), 6, 5, 5, 2 },
                    { 7, new DateOnly(2025, 11, 10), 10, 6, 6, 2 },
                    { 8, new DateOnly(2025, 12, 15), 3, 1, 8, 2 },
                    { 9, new DateOnly(2026, 1, 20), 5, 7, 9, 2 },
                    { 10, new DateOnly(2026, 2, 1), 4, 8, 10, 2 },
                    { 11, new DateOnly(2026, 2, 10), 2, 9, 1, 2 },
                    { 12, new DateOnly(2026, 2, 20), 6, 2, 4, 1 },
                    { 13, new DateOnly(2026, 3, 1), 5, 3, 3, 0 },
                    { 14, new DateOnly(2026, 3, 5), 2, 10, 6, 0 },
                    { 15, new DateOnly(2026, 3, 10), 3, 1, 7, 0 },
                    { 16, new DateOnly(2024, 8, 10), 5, 4, 3, 2 },
                    { 17, new DateOnly(2024, 11, 20), 4, 5, 5, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_CategoryId",
                table: "Items",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_ItemId",
                table: "Orders",
                column: "ItemId");

            migrationBuilder.Sql("SELECT setval(pg_get_serial_sequence('\"Customers\"', 'Id'), (SELECT MAX(\"Id\") FROM \"Customers\"));");
            migrationBuilder.Sql("SELECT setval(pg_get_serial_sequence('\"Items\"', 'Id'), (SELECT MAX(\"Id\") FROM \"Items\"));");
            migrationBuilder.Sql("SELECT setval(pg_get_serial_sequence('\"ItemCategories\"', 'Id'), (SELECT MAX(\"Id\") FROM \"ItemCategories\"));");
            migrationBuilder.Sql("SELECT setval(pg_get_serial_sequence('\"Orders\"', 'Id'), (SELECT MAX(\"Id\") FROM \"Orders\"));");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "ItemCategories");
        }
    }
}
