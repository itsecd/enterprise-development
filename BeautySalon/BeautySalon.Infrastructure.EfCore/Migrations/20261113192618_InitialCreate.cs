using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeautySalon.Infrastructure.EfCore.Migrations
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
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    birthday = table.Column<DateOnly>(type: "date", nullable: false),
                    phone_number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    full_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_clients", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "masters",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    passport_number = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    specialization = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    work_experience = table.Column<int>(type: "int", nullable: false),
                    full_name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_masters", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "services",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    category = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    price = table.Column<decimal>(type: "decimal(65,30)", nullable: false),
                    duration = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_services", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "appointments",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    MasterId = table.Column<int>(type: "int", nullable: false),
                    start_time = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    is_regular = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_appointments", x => x.id);
                    table.ForeignKey(
                        name: "FK_appointments_clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_appointments_masters_MasterId",
                        column: x => x.MasterId,
                        principalTable: "masters",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_appointments_services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "services",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "clients",
                columns: new[] { "id", "birthday", "full_name", "gender", "phone_number" },
                values: new object[,]
                {
                    { 1, new DateOnly(1990, 4, 15), "Смирнова Марина Алексеевна", 1, "+7 900 000-00-01" },
                    { 2, new DateOnly(1985, 8, 20), "Иванов Сергей Петрович", 0, "+7 900 000-00-02" },
                    { 3, new DateOnly(1995, 2, 10), "Кузнецова Ольга Дмитриевна", 1, "+7 900 000-00-03" },
                    { 4, new DateOnly(1988, 11, 3), "Попов Андрей Викторович", 0, "+7 900 000-00-04" },
                    { 5, new DateOnly(1992, 6, 25), "Васильева Екатерина Олеговна", 1, "+7 900 000-00-05" },
                    { 6, new DateOnly(1998, 1, 18), "Михайлов Денис Сергеевич", 0, "+7 900 000-00-06" },
                    { 7, new DateOnly(1983, 9, 12), "Николаева Алина Романовна", 1, "+7 900 000-00-07" },
                    { 8, new DateOnly(1991, 12, 7), "Федоров Максим Игоревич", 0, "+7 900 000-00-08" },
                    { 9, new DateOnly(1996, 5, 30), "Соколова Виктория Андреевна", 1, "+7 900 000-00-09" },
                    { 10, new DateOnly(1987, 3, 22), "Орлов Роман Александрович", 0, "+7 900 000-00-10" }
                });

            migrationBuilder.InsertData(
                table: "masters",
                columns: new[] { "id", "full_name", "gender", "passport_number", "specialization", "work_experience" },
                values: new object[,]
                {
                    { 1, "Иванова Анна Сергеевна", 1, "4501 123456", "Парикмахер", 10 },
                    { 2, "Петрова Мария Андреевна", 1, "4502 234567", "Визажист", 7 },
                    { 3, "Соколова Елена Викторовна", 1, "4503 345678", "Мастер маникюра", 5 },
                    { 4, "Кузнецов Дмитрий Олегович", 0, "4504 456789", "Парикмахер", 6 },
                    { 5, "Орлова Екатерина Павловна", 1, "4505 567890", "Мастер маникюра", 3 },
                    { 6, "Морозов Алексей Игоревич", 0, "4506 678901", "Барбер", 2 },
                    { 7, "Волкова Ольга Максимовна", 1, "4507 789012", "Визажист", 8 },
                    { 8, "Алексеева Наталья Романовна", 1, "4508 890123", "Косметолог", 4 },
                    { 9, "Лебедев Артем Сергеевич", 0, "4509 901234", "Барбер", 11 },
                    { 10, "Федорова Виктория Ильинична", 1, "4510 012345", "Косметолог", 5 }
                });

            migrationBuilder.InsertData(
                table: "services",
                columns: new[] { "id", "category", "duration", "name", "price" },
                values: new object[,]
                {
                    { 1, "Волосы", 60, "Стрижка", 1800m },
                    { 2, "Ногти", 90, "Маникюр", 1500m },
                    { 3, "Волосы", 180, "Окрашивание", 4500m },
                    { 4, "Волосы", 60, "Укладка", 2000m },
                    { 5, "Ногти", 120, "Педикюр", 2200m },
                    { 6, "Макияж", 90, "Макияж", 2500m },
                    { 7, "Брови", 30, "Коррекция бровей", 1000m },
                    { 8, "Косметология", 90, "Уход за лицом", 3000m },
                    { 9, "Уход", 60, "Массаж", 2500m },
                    { 10, "Ресницы", 90, "Ламинирование ресниц", 2800m }
                });

            migrationBuilder.InsertData(
                table: "appointments",
                columns: new[] { "id", "ClientId", "is_regular", "MasterId", "ServiceId", "start_time" },
                values: new object[,]
                {
                    { 1, 1, false, 1, 1, new DateTime(2026, 9, 25, 9, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, false, 1, 2, new DateTime(2026, 9, 25, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, false, 1, 4, new DateTime(2026, 9, 25, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, true, 2, 1, new DateTime(2026, 9, 1, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, true, 3, 2, new DateTime(2026, 9, 10, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 1, true, 4, 4, new DateTime(2026, 9, 20, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 2, true, 4, 1, new DateTime(2026, 9, 5, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 2, true, 5, 3, new DateTime(2026, 9, 18, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 3, true, 6, 2, new DateTime(2026, 8, 30, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 3, true, 7, 5, new DateTime(2026, 9, 8, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 3, true, 7, 6, new DateTime(2026, 9, 15, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 4, false, 4, 1, new DateTime(2026, 9, 12, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 5, false, 5, 3, new DateTime(2026, 7, 15, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 6, false, 6, 5, new DateTime(2026, 7, 20, 12, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_appointments_ClientId",
                table: "appointments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_MasterId",
                table: "appointments",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_appointments_ServiceId",
                table: "appointments",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "appointments");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "masters");

            migrationBuilder.DropTable(
                name: "services");
        }
    }
}
