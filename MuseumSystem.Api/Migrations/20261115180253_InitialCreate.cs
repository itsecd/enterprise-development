using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MuseumSystem.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "excursions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    starts_at = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    duration_minutes = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_excursions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "exhibitions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    theme = table.Column<int>(type: "integer", nullable: false),
                    hall_number = table.Column<int>(type: "integer", nullable: false),
                    start_date = table.Column<DateOnly>(type: "date", nullable: false),
                    end_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_exhibitions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "visitors",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    full_name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_visitors", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "excursion_exhibitions",
                columns: table => new
                {
                    excursion_id = table.Column<int>(type: "integer", nullable: false),
                    exhibition_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_excursion_exhibitions", x => new { x.excursion_id, x.exhibition_id });
                    table.ForeignKey(
                        name: "fk_excursion_exhibitions_excursions_excursion_id",
                        column: x => x.excursion_id,
                        principalTable: "excursions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_excursion_exhibitions_exhibitions_exhibition_id",
                        column: x => x.exhibition_id,
                        principalTable: "exhibitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "tickets",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    excursion_id = table.Column<int>(type: "integer", nullable: false),
                    visitor_id = table.Column<int>(type: "integer", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    price = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tickets", x => x.id);
                    table.ForeignKey(
                        name: "fk_tickets_excursions_excursion_id",
                        column: x => x.excursion_id,
                        principalTable: "excursions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_tickets_visitors_visitor_id",
                        column: x => x.visitor_id,
                        principalTable: "visitors",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "excursions",
                columns: new[] { "id", "duration_minutes", "starts_at" },
                values: new object[,]
                {
                    { 1, 60, new DateTime(2026, 1, 15, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 90, new DateTime(2026, 1, 20, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 60, new DateTime(2026, 2, 5, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 45, new DateTime(2026, 2, 10, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 75, new DateTime(2026, 2, 15, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 60, new DateTime(2026, 2, 20, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 90, new DateTime(2026, 3, 1, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 60, new DateTime(2026, 3, 5, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 45, new DateTime(2026, 3, 10, 11, 30, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 75, new DateTime(2026, 3, 15, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 60, new DateTime(2026, 3, 20, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 90, new DateTime(2026, 3, 25, 14, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "exhibitions",
                columns: new[] { "id", "end_date", "hall_number", "name", "start_date", "theme" },
                values: new object[,]
                {
                    { 1, new DateOnly(2026, 3, 10), 1, "Древний Египет", new DateOnly(2026, 1, 10), 0 },
                    { 2, new DateOnly(2026, 4, 1), 2, "Русский авангард", new DateOnly(2026, 2, 1), 1 },
                    { 3, new DateOnly(2026, 5, 1), 3, "Космос", new DateOnly(2026, 1, 15), 2 },
                    { 4, new DateOnly(2026, 6, 1), 4, "Народы Севера", new DateOnly(2026, 3, 1), 3 },
                    { 5, new DateOnly(2026, 7, 1), 5, "Робототехника", new DateOnly(2026, 2, 15), 4 },
                    { 6, new DateOnly(2026, 3, 20), 1, "Средневековье", new DateOnly(2026, 1, 20), 0 },
                    { 7, new DateOnly(2026, 4, 10), 2, "Импрессионизм", new DateOnly(2026, 2, 10), 1 },
                    { 8, new DateOnly(2026, 4, 5), 3, "Физика частиц", new DateOnly(2026, 1, 5), 2 },
                    { 9, new DateOnly(2026, 6, 5), 4, "Традиции Востока", new DateOnly(2026, 3, 5), 3 },
                    { 10, new DateOnly(2026, 7, 20), 5, "Авиация", new DateOnly(2026, 2, 20), 4 },
                    { 11, new DateOnly(2026, 5, 1), 2, "Ренессанс", new DateOnly(2026, 3, 1), 1 },
                    { 12, new DateOnly(2026, 4, 1), 1, "Археология", new DateOnly(2026, 2, 1), 0 }
                });

            migrationBuilder.InsertData(
                table: "visitors",
                columns: new[] { "id", "birth_date", "full_name", "phone" },
                values: new object[,]
                {
                    { 1, new DateOnly(1990, 4, 12), "Андреев Алексей Сергеевич", "+7 (911) 100-00-01" },
                    { 2, new DateOnly(1985, 11, 25), "Борисова Марина Владимировна", "+7 (911) 100-00-02" },
                    { 3, new DateOnly(1978, 2, 18), "Васильев Дмитрий Олегович", "+7 (911) 100-00-03" },
                    { 4, new DateOnly(1992, 6, 30), "Гаврилова Елена Николаевна", "+7 (911) 100-00-04" },
                    { 5, new DateOnly(1996, 9, 5), "Дмитриев Павел Андреевич", "+7 (911) 100-00-05" },
                    { 6, new DateOnly(1983, 12, 1), "Егорова Ольга Ивановна", "+7 (911) 100-00-06" },
                    { 7, new DateOnly(1994, 5, 22), "Жуков Игорь Валерьевич", "+7 (911) 100-00-07" },
                    { 8, new DateOnly(1999, 8, 17), "Зотова Анна Викторовна", "+7 (911) 100-00-08" },
                    { 9, new DateOnly(1975, 1, 9), "Иванов Сергей Петрович", "+7 (911) 100-00-09" },
                    { 10, new DateOnly(2001, 3, 28), "Козлова Мария Дмитриевна", "+7 (911) 100-00-10" },
                    { 11, new DateOnly(2003, 7, 7), "Лебедев Артём Игоревич", "+7 (911) 100-00-11" },
                    { 12, new DateOnly(1988, 10, 10), "Морозова Татьяна Львовна", "+7 (911) 100-00-12" }
                });

            migrationBuilder.InsertData(
                table: "excursion_exhibitions",
                columns: new[] { "excursion_id", "exhibition_id" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 6 },
                    { 2, 2 },
                    { 2, 7 },
                    { 3, 3 },
                    { 3, 8 },
                    { 4, 4 },
                    { 5, 5 },
                    { 5, 10 },
                    { 6, 12 },
                    { 7, 11 },
                    { 8, 8 },
                    { 9, 9 },
                    { 10, 10 },
                    { 11, 1 },
                    { 12, 7 }
                });

            migrationBuilder.InsertData(
                table: "tickets",
                columns: new[] { "id", "excursion_id", "price", "type", "visitor_id" },
                values: new object[,]
                {
                    { 1, 1, 500m, 0, 1 },
                    { 2, 1, 250m, 1, 2 },
                    { 3, 1, 500m, 0, 3 },
                    { 4, 1, 500m, 0, 4 },
                    { 5, 1, 250m, 1, 5 },
                    { 6, 2, 500m, 0, 6 },
                    { 7, 2, 500m, 0, 7 },
                    { 8, 2, 250m, 1, 8 },
                    { 9, 2, 500m, 0, 9 },
                    { 10, 3, 500m, 0, 1 },
                    { 11, 3, 500m, 0, 2 },
                    { 12, 3, 250m, 1, 3 },
                    { 13, 3, 500m, 0, 10 },
                    { 14, 3, 500m, 0, 11 },
                    { 15, 3, 250m, 1, 12 },
                    { 16, 4, 500m, 0, 4 },
                    { 17, 4, 500m, 0, 5 },
                    { 18, 5, 500m, 0, 6 },
                    { 19, 5, 250m, 1, 7 },
                    { 20, 5, 500m, 0, 8 },
                    { 21, 6, 500m, 0, 9 },
                    { 22, 7, 500m, 0, 10 },
                    { 23, 7, 500m, 0, 11 },
                    { 24, 7, 250m, 1, 12 },
                    { 25, 7, 500m, 0, 1 },
                    { 26, 8, 500m, 0, 2 },
                    { 27, 8, 500m, 0, 3 },
                    { 28, 8, 250m, 1, 4 },
                    { 29, 9, 500m, 0, 5 },
                    { 30, 9, 500m, 0, 6 },
                    { 31, 10, 500m, 0, 7 },
                    { 32, 10, 500m, 0, 8 },
                    { 33, 10, 250m, 1, 9 },
                    { 34, 10, 500m, 0, 10 },
                    { 35, 10, 500m, 0, 11 },
                    { 36, 11, 500m, 0, 12 },
                    { 37, 11, 500m, 0, 1 },
                    { 38, 11, 250m, 1, 2 },
                    { 39, 12, 500m, 0, 3 },
                    { 40, 12, 500m, 0, 4 },
                    { 41, 12, 500m, 0, 5 },
                    { 42, 12, 250m, 1, 6 }
                });

            migrationBuilder.CreateIndex(
                name: "ix_excursion_exhibitions_exhibition_id",
                table: "excursion_exhibitions",
                column: "exhibition_id");

            migrationBuilder.CreateIndex(
                name: "ix_tickets_excursion_id",
                table: "tickets",
                column: "excursion_id");

            migrationBuilder.CreateIndex(
                name: "ix_tickets_visitor_id",
                table: "tickets",
                column: "visitor_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "excursion_exhibitions");

            migrationBuilder.DropTable(
                name: "tickets");

            migrationBuilder.DropTable(
                name: "exhibitions");

            migrationBuilder.DropTable(
                name: "excursions");

            migrationBuilder.DropTable(
                name: "visitors");
        }
    }
}
