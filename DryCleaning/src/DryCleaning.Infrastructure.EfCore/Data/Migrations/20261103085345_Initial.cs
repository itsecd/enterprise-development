using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DryCleaning.Infrastructure.EfCore.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    ClientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.ClientId);
                });

            migrationBuilder.CreateTable(
                name: "ItemCategories",
                columns: table => new
                {
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RecommendedCleaningType = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemCategories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Material = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Items_ItemCategories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "ItemCategories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    ProcessingDays = table.Column<int>(type: "int", nullable: false),
                    AcceptDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CompletedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "ClientId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Items_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "ItemId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "ClientId", "FullName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Alexander Kuznetsov", "+7 927 416 5823" },
                    { 2, "Vadim Petrov", "+7 919 813 0876" },
                    { 3, "Sergey Piratov", "+7 927 903 1946" },
                    { 4, "Anna Smirnova", "+7 903 572 1846" },
                    { 5, "Mikhail Volkov", "+7 960 348 2917" },
                    { 6, "Elena Sokolova", "+7 902 681 4359" },
                    { 7, "Dmitry Morozov", "+7 937 154 8620" },
                    { 8, "Olga Novikova", "+7 987 429 3065" },
                    { 9, "Alexey Popov", "+7 905 736 2184" },
                    { 10, "Maria Vasilyeva", "+7 919 582 6473" }
                });

            migrationBuilder.InsertData(
                table: "ItemCategories",
                columns: new[] { "CategoryId", "CategoryName", "Price", "RecommendedCleaningType" },
                values: new object[,]
                {
                    { 1, "Clothing", 1500m, 0 },
                    { 2, "Shoes", 2000m, 1 },
                    { 3, "Accessories", 1000m, 2 },
                    { 4, "Carpets", 3500m, 1 },
                    { 5, "Curtains", 1800m, 2 },
                    { 6, "Bedding", 1200m, 1 },
                    { 7, "Evening Wear", 2500m, 0 },
                    { 8, "Leather Goods", 3000m, 3 },
                    { 9, "Soft Toys", 900m, 3 },
                    { 10, "Shirts", 700m, 4 }
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "ItemId", "CategoryId", "Material", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Wool", "Black Wool Coat" },
                    { 2, 2, "Leather", "Leather Boots" },
                    { 3, 3, "Silk", "Silk Scarf" },
                    { 4, 4, "Wool", "Living Room Carpet" },
                    { 5, 5, "Velvet", "Velvet Curtains" },
                    { 6, 6, "Cotton", "Cotton Duvet" },
                    { 7, 7, "Silk", "Evening Dress" },
                    { 8, 8, "Leather", "Leather Handbag" },
                    { 9, 9, "Plush", "Teddy Bear" },
                    { 10, 10, "Cotton", "White Shirt" },
                    { 11, 1, "Polyester", "Winter Jacket" },
                    { 12, 2, "Suede", "Suede Shoes" },
                    { 13, 3, "Wool", "Wool Hat" },
                    { 14, 4, "Synthetic", "Bedroom Carpet" },
                    { 15, 5, "Linen", "Linen Curtains" },
                    { 16, 6, "Wool", "Wool Blanket" },
                    { 17, 7, "Velvet", "Cocktail Dress" },
                    { 18, 8, "Leather", "Leather Belt" },
                    { 19, 9, "Plush", "Plush Rabbit" },
                    { 20, 10, "Linen", "Blue Shirt" }
                });

            migrationBuilder.InsertData(
                table: "Orders",
                columns: new[] { "Id", "AcceptDate", "ClientId", "CompletedDate", "ItemId", "ProcessingDays", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, null, 1, 10, 1 },
                    { 2, new DateTime(2026, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new DateTime(2026, 8, 20, 0, 0, 0, 0, DateTimeKind.Utc), 2, 5, 3 },
                    { 3, new DateTime(2026, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new DateTime(2026, 7, 20, 0, 0, 0, 0, DateTimeKind.Utc), 3, 4, 2 },
                    { 4, new DateTime(2026, 6, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, new DateTime(2026, 6, 15, 0, 0, 0, 0, DateTimeKind.Utc), 4, 7, 3 },
                    { 5, new DateTime(2026, 5, 1, 0, 0, 0, 0, DateTimeKind.Utc), 1, null, 5, 5, 4 },
                    { 6, new DateTime(2026, 4, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new DateTime(2026, 4, 5, 0, 0, 0, 0, DateTimeKind.Utc), 6, 5, 3 },
                    { 7, new DateTime(2026, 3, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new DateTime(2026, 3, 6, 0, 0, 0, 0, DateTimeKind.Utc), 7, 6, 3 },
                    { 8, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Utc), 2, new DateTime(2026, 2, 11, 0, 0, 0, 0, DateTimeKind.Utc), 8, 8, 3 },
                    { 9, new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc), 2, null, 9, 7, 1 },
                    { 10, new DateTime(2026, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new DateTime(2026, 1, 4, 0, 0, 0, 0, DateTimeKind.Utc), 10, 3, 2 },
                    { 11, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 3, new DateTime(2025, 12, 8, 0, 0, 0, 0, DateTimeKind.Utc), 11, 5, 3 },
                    { 12, new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), 3, null, 12, 6, 1 },
                    { 13, new DateTime(2025, 11, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new DateTime(2025, 11, 3, 0, 0, 0, 0, DateTimeKind.Utc), 13, 4, 3 },
                    { 14, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Utc), 4, new DateTime(2025, 10, 20, 0, 0, 0, 0, DateTimeKind.Utc), 14, 8, 3 },
                    { 15, new DateTime(2025, 9, 20, 0, 0, 0, 0, DateTimeKind.Utc), 5, new DateTime(2025, 9, 23, 0, 0, 0, 0, DateTimeKind.Utc), 15, 5, 3 },
                    { 16, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc), 5, new DateTime(2025, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc), 16, 5, 2 },
                    { 17, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Utc), 6, new DateTime(2025, 8, 7, 0, 0, 0, 0, DateTimeKind.Utc), 17, 6, 3 },
                    { 18, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Utc), 7, new DateTime(2025, 7, 9, 0, 0, 0, 0, DateTimeKind.Utc), 18, 7, 3 },
                    { 19, new DateTime(2026, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc), 8, null, 19, 5, 0 },
                    { 20, new DateTime(2024, 12, 1, 0, 0, 0, 0, DateTimeKind.Utc), 9, new DateTime(2024, 12, 4, 0, 0, 0, 0, DateTimeKind.Utc), 20, 3, 3 }
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
                name: "ItemCategories");
        }
    }
}
