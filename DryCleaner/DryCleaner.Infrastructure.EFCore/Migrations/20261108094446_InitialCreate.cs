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
                name: "product_category",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    recommended_cleaning_type = table.Column<string>(type: "text", nullable: true),
                    cleaning_price = table.Column<decimal>(type: "numeric", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product_category", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "students",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    phone = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_students", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "product",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    material = table.Column<string>(type: "text", nullable: false),
                    CategoryId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_product", x => x.id);
                    table.ForeignKey(
                        name: "FK_product_product_category_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "product_category",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "order",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    ProductId = table.Column<int>(type: "integer", nullable: false),
                    accepted_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    processing_days = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_order", x => x.id);
                    table.ForeignKey(
                        name: "FK_order_product_ProductId",
                        column: x => x.ProductId,
                        principalTable: "product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_order_students_ClientId",
                        column: x => x.ClientId,
                        principalTable: "students",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "product_category",
                columns: new[] { "id", "cleaning_price", "name", "recommended_cleaning_type" },
                values: new object[,]
                {
                    { 1, 1500m, "Верхняя одежда", "Химчистка" },
                    { 2, 1200m, "Костюмы", "Химчистка" },
                    { 3, 1000m, "Платья", "Деликатная чистка" },
                    { 4, 500m, "Рубашки", "Стирка" },
                    { 5, 700m, "Брюки", "Химчистка" },
                    { 6, 1800m, "Пальто", "Химчистка" },
                    { 7, 1300m, "Куртки", "Химчистка" },
                    { 8, 2000m, "Шторы", "Деликатная чистка" },
                    { 9, 1700m, "Одеяла", "Стирка" },
                    { 10, 900m, "Обувь", "Специальная чистка" }
                });

            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "id", "name", "phone" },
                values: new object[,]
                {
                    { 1, "Иванов Иван Иванович", "+79990000001" },
                    { 2, "Петров Петр Петрович", "+79990000002" },
                    { 3, "Сидорова Анна Сергеевна", "+79990000003" },
                    { 4, "Кузнецов Алексей Владимирович", "+79990000004" },
                    { 5, "Смирнова Елена Андреевна", "+79990000005" },
                    { 6, "Попов Дмитрий Сергеевич", "+79990000006" },
                    { 7, "Васильева Ольга Николаевна", "+79990000007" },
                    { 8, "Новиков Андрей Павлович", "+79990000008" },
                    { 9, "Морозова Мария Игоревна", "+79990000009" },
                    { 10, "Волков Сергей Александрович", "+79990000010" }
                });

            migrationBuilder.InsertData(
                table: "product",
                columns: new[] { "id", "CategoryId", "material", "name" },
                values: new object[,]
                {
                    { 1, 1, "Шерсть", "Пальто классическое" },
                    { 2, 2, "Шерсть", "Костюм мужской" },
                    { 3, 1, "Кашемир", "Пальто женское" },
                    { 4, 1, "Шерсть", "Пальто осеннее" },
                    { 5, 2, "Шерсть", "Костюм женский" },
                    { 6, 2, "Шерсть", "Костюм деловой" },
                    { 7, 3, "Шёлк", "Платье вечернее" },
                    { 8, 4, "Хлопок", "Рубашка белая" },
                    { 9, 5, "Шерсть", "Брюки классические" },
                    { 10, 1, "Кашемир", "Пальто мужское" }
                });

            migrationBuilder.InsertData(
                table: "order",
                columns: new[] { "id", "accepted_at", "ClientId", "processing_days", "ProductId", "status" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 1, 10, 0, 0, 0, 0, DateTimeKind.Utc), 1, 5, 1, 3 },
                    { 2, new DateTime(2025, 3, 10, 0, 0, 0, 0, DateTimeKind.Utc), 1, 7, 2, 3 },
                    { 3, new DateTime(2025, 6, 10, 0, 0, 0, 0, DateTimeKind.Utc), 2, 3, 3, 3 },
                    { 4, new DateTime(2025, 7, 25, 0, 0, 0, 0, DateTimeKind.Utc), 2, 10, 4, 1 },
                    { 5, new DateTime(2025, 8, 10, 0, 0, 0, 0, DateTimeKind.Utc), 3, 6, 5, 3 },
                    { 6, new DateTime(2025, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 3, 8, 6, 3 },
                    { 7, new DateTime(2025, 10, 10, 0, 0, 0, 0, DateTimeKind.Utc), 4, 15, 7, 3 },
                    { 8, new DateTime(2025, 11, 10, 0, 0, 0, 0, DateTimeKind.Utc), 4, 20, 8, 1 },
                    { 9, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Utc), 5, 4, 9, 3 },
                    { 10, new DateTime(2026, 5, 10, 0, 0, 0, 0, DateTimeKind.Utc), 5, 12, 10, 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_order_ClientId",
                table: "order",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_order_ProductId",
                table: "order",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_product_CategoryId",
                table: "product",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "order");

            migrationBuilder.DropTable(
                name: "product");

            migrationBuilder.DropTable(
                name: "students");

            migrationBuilder.DropTable(
                name: "product_category");
        }
    }
}
