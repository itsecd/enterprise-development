using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Museum.Infrastructure.MySql.Migrations
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
                name: "Exhibitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Theme = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    HallNumber = table.Column<int>(type: "int", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Exhibitions", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tours", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Visitors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "varchar(200)", maxLength: 200, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(32)", maxLength: 32, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    BirthDate = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Visitors", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TourExhibitions",
                columns: table => new
                {
                    TourId = table.Column<int>(type: "int", nullable: false),
                    ExhibitionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TourExhibitions", x => new { x.TourId, x.ExhibitionId });
                    table.ForeignKey(
                        name: "FK_TourExhibitions_Exhibitions_ExhibitionId",
                        column: x => x.ExhibitionId,
                        principalTable: "Exhibitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TourExhibitions_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TourId = table.Column<int>(type: "int", nullable: false),
                    VisitorId = table.Column<int>(type: "int", nullable: false),
                    Type = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Tours_TourId",
                        column: x => x.TourId,
                        principalTable: "Tours",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tickets_Visitors_VisitorId",
                        column: x => x.VisitorId,
                        principalTable: "Visitors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Exhibitions",
                columns: new[] { "Id", "EndDate", "HallNumber", "Name", "StartDate", "Theme" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Барокко", new DateTime(2025, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Art" },
                    { 2, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Древний Египет", new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "History" },
                    { 3, new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Космос", new DateTime(2025, 3, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Science" },
                    { 4, new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Дикая природа", new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nature" },
                    { 5, new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Художники", new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Art" },
                    { 6, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "18 век", new DateTime(2025, 5, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "History" },
                    { 7, new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Средневековье", new DateTime(2025, 6, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "History" },
                    { 8, new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Океаны", new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Nature" },
                    { 9, new DateTime(2025, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Артефакты", new DateTime(2025, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "History" },
                    { 10, new DateTime(2025, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Древняя Греция", new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "History" },
                    { 11, new DateTime(2025, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Динозавры", new DateTime(2025, 8, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Science" }
                });

            migrationBuilder.InsertData(
                table: "Tours",
                columns: new[] { "Id", "Date", "DurationMinutes", "StartTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 90, new TimeSpan(0, 10, 0, 0, 0) },
                    { 2, new DateTime(2025, 2, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 60, new TimeSpan(0, 14, 0, 0, 0) },
                    { 3, new DateTime(2025, 3, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 120, new TimeSpan(0, 11, 0, 0, 0) },
                    { 4, new DateTime(2025, 3, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 90, new TimeSpan(0, 15, 0, 0, 0) },
                    { 5, new DateTime(2025, 4, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 60, new TimeSpan(0, 10, 0, 0, 0) },
                    { 6, new DateTime(2025, 4, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), 90, new TimeSpan(0, 13, 0, 0, 0) },
                    { 7, new DateTime(2025, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), 60, new TimeSpan(0, 11, 0, 0, 0) },
                    { 8, new DateTime(2025, 5, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 120, new TimeSpan(0, 14, 0, 0, 0) },
                    { 9, new DateTime(2025, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 90, new TimeSpan(0, 10, 0, 0, 0) },
                    { 10, new DateTime(2025, 6, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), 60, new TimeSpan(0, 15, 0, 0, 0) },
                    { 11, new DateTime(2025, 7, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 90, new TimeSpan(0, 11, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "Visitors",
                columns: new[] { "Id", "BirthDate", "FullName", "Phone" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 5, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Иванов Иван Иванович", "+79001234567" },
                    { 2, new DateTime(1985, 3, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Петрова Мария Сергеевна", "+79007654321" },
                    { 3, new DateTime(2000, 7, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сидоров Алексей Петрович", "+79009876543" },
                    { 4, new DateTime(1978, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Козлова Анна Дмитриевна", "+79001112233" },
                    { 5, new DateTime(1995, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новиков Дмитрий Александрович", "+79004445566" },
                    { 6, new DateTime(1988, 6, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Морозова Елена Владимировна", "+79007778899" },
                    { 7, new DateTime(2003, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Волков Сергей Николаевич", "+79002223344" },
                    { 8, new DateTime(1975, 4, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Соколова Ольга Игоревна", "+79005556677" },
                    { 9, new DateTime(1992, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Баранова Вероника Алексеевна", "+79008889900" },
                    { 10, new DateTime(1983, 2, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "Степанова София Вячеславовна", "+79003334455" },
                    { 11, new DateTime(1998, 8, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Астафьев Вадим Батькович", "+79006667788" }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "Price", "TourId", "Type", "VisitorId" },
                values: new object[,]
                {
                    { 1, 500m, 1, "Adult", 1 },
                    { 2, 250m, 1, "Concession", 2 },
                    { 3, 500m, 2, "Adult", 3 },
                    { 4, 250m, 2, "Concession", 4 },
                    { 5, 600m, 3, "Adult", 5 },
                    { 6, 500m, 4, "Adult", 6 },
                    { 7, 300m, 5, "Concession", 7 },
                    { 8, 500m, 6, "Adult", 8 },
                    { 9, 600m, 7, "Adult", 9 },
                    { 10, 250m, 8, "Concession", 10 },
                    { 11, 500m, 9, "Adult", 11 },
                    { 12, 300m, 10, "Concession", 1 }
                });

            migrationBuilder.InsertData(
                table: "TourExhibitions",
                columns: new[] { "ExhibitionId", "TourId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 },
                    { 3, 2 },
                    { 4, 3 },
                    { 5, 3 },
                    { 1, 4 },
                    { 6, 5 },
                    { 7, 5 },
                    { 8, 6 },
                    { 9, 7 },
                    { 10, 7 },
                    { 2, 8 },
                    { 3, 9 },
                    { 4, 9 },
                    { 5, 10 },
                    { 6, 11 },
                    { 11, 11 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_TourId",
                table: "Tickets",
                column: "TourId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_VisitorId",
                table: "Tickets",
                column: "VisitorId");

            migrationBuilder.CreateIndex(
                name: "IX_TourExhibitions_ExhibitionId",
                table: "TourExhibitions",
                column: "ExhibitionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "TourExhibitions");

            migrationBuilder.DropTable(
                name: "Visitors");

            migrationBuilder.DropTable(
                name: "Exhibitions");

            migrationBuilder.DropTable(
                name: "Tours");
        }
    }
}
