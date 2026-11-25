using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeautySalon.Infrastructure.SqlServer.Migrations
{
    /// <inheritdoc />
    public partial class InitialSalon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    Category = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    DurationMinutes = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specialists",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassportNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, collation: "Latin1_General_100_CI_AS"),
                    Specialization = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    WorkExperience = table.Column<int>(type: "int", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Patronymic = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specialists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    SpecialistId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    AppointmentDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRegularCustomer = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Specialists_SpecialistId",
                        column: x => x.SpecialistId,
                        principalTable: "Specialists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "Gender", "LastName", "Patronymic", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, new DateOnly(1998, 2, 14), "Иван", 0, "Иванов", "Сергеевич", "+79000000001" },
                    { 2, new DateOnly(1995, 6, 21), "Анна", 1, "Петрова", "Игоревна", "+79000000002" },
                    { 3, new DateOnly(2001, 1, 9), "Мария", 1, "Соколова", "Андреевна", "+79000000003" },
                    { 4, new DateOnly(1992, 11, 3), "Дмитрий", 0, "Кузнецов", "Олегович", "+79000000004" },
                    { 5, new DateOnly(1989, 8, 17), "Ольга", 1, "Морозова", "Викторовна", "+79000000005" },
                    { 6, new DateOnly(1997, 4, 28), "Максим", 0, "Волков", "Алексеевич", "+79000000006" },
                    { 7, new DateOnly(2000, 9, 12), "Елена", 1, "Орлова", "Дмитриевна", "+79000000007" },
                    { 8, new DateOnly(1994, 12, 1), "Алексей", 0, "Смирнов", "Романович", "+79000000008" },
                    { 9, new DateOnly(1987, 3, 25), "Наталья", 1, "Федорова", "Сергеевна", "+79000000009" },
                    { 10, new DateOnly(2002, 7, 30), "Артем", 0, "Никитин", null, "+79000000010" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Category", "DurationMinutes", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Волосы", 90, "Женская стрижка", 2500m },
                    { 2, "Волосы", 60, "Мужская стрижка", 1800m },
                    { 3, "Волосы", 180, "Окрашивание волос", 5500m },
                    { 4, "Ногтевой сервис", 90, "Маникюр", 2200m },
                    { 5, "Ногтевой сервис", 100, "Педикюр", 2800m },
                    { 6, "Макияж", 90, "Макияж", 3500m },
                    { 7, "Брови", 40, "Коррекция бровей", 1200m },
                    { 8, "Уход", 60, "Массаж лица", 2000m },
                    { 9, "Массаж", 90, "Расслабляющий массаж", 4000m },
                    { 10, "Волосы", 60, "Укладка", 2000m }
                });

            migrationBuilder.InsertData(
                table: "Specialists",
                columns: new[] { "Id", "DateOfBirth", "FirstName", "Gender", "LastName", "PassportNumber", "Patronymic", "PhoneNumber", "Specialization", "WorkExperience" },
                values: new object[,]
                {
                    { 1, new DateOnly(1990, 2, 11), "Алина", 1, "Крылова", "4501000001", "Павловна", "+79010000001", "Парикмахер", 3 },
                    { 2, new DateOnly(1988, 5, 19), "Ирина", 1, "Семенова", "4501000002", "Владимировна", "+79010000002", "Парикмахер", 5 },
                    { 3, new DateOnly(1987, 9, 4), "Роман", 0, "Михайлов", "4501000003", "Ильич", "+79010000003", "Барбер", 7 },
                    { 4, new DateOnly(1992, 1, 27), "Екатерина", 1, "Белова", "4501000004", "Олеговна", "+79010000004", "Мастер маникюра", 9 },
                    { 5, new DateOnly(1985, 6, 8), "Денис", 0, "Зайцев", "4501000005", "Андреевич", "+79010000005", "Массажист", 12 },
                    { 6, new DateOnly(1991, 10, 15), "Марина", 1, "Орлова", "4501000006", "Игоревна", "+79010000006", "Бровист", 4 },
                    { 7, new DateOnly(1986, 12, 22), "Николай", 0, "Громов", "4501000007", "Сергеевич", "+79010000007", "Парикмахер", 6 },
                    { 8, new DateOnly(1993, 4, 16), "Светлана", 1, "Власова", "4501000008", "Алексеевна", "+79010000008", "Визажист", 8 },
                    { 9, new DateOnly(1989, 7, 7), "Евгений", 0, "Фролов", "4501000009", "Максимович", "+79010000009", "Массажист", 10 },
                    { 10, new DateOnly(1996, 3, 13), "Дарья", 1, "Романова", "4501000010", "Артемовна", "+79010000010", "Мастер маникюра", 2 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDateTime", "CustomerId", "IsRegularCustomer", "ServiceId", "SpecialistId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 11, 22, 9, 0, 0, 0, DateTimeKind.Unspecified), 1, true, 2, 2 },
                    { 2, new DateTime(2026, 11, 22, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, true, 2, 2 },
                    { 3, new DateTime(2026, 11, 22, 17, 0, 0, 0, DateTimeKind.Unspecified), 3, true, 1, 2 },
                    { 4, new DateTime(2026, 11, 22, 20, 0, 0, 0, DateTimeKind.Unspecified), 4, true, 10, 2 },
                    { 5, new DateTime(2026, 11, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, true, 1, 2 },
                    { 6, new DateTime(2026, 10, 31, 11, 0, 0, 0, DateTimeKind.Unspecified), 1, true, 2, 2 },
                    { 7, new DateTime(2026, 10, 26, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, true, 1, 2 },
                    { 8, new DateTime(2026, 10, 6, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, true, 1, 2 },
                    { 9, new DateTime(2026, 11, 12, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, true, 4, 4 },
                    { 10, new DateTime(2026, 11, 2, 13, 0, 0, 0, DateTimeKind.Unspecified), 2, true, 4, 4 },
                    { 11, new DateTime(2026, 11, 8, 14, 0, 0, 0, DateTimeKind.Unspecified), 5, true, 2, 3 },
                    { 12, new DateTime(2026, 10, 29, 15, 0, 0, 0, DateTimeKind.Unspecified), 5, true, 6, 8 },
                    { 13, new DateTime(2026, 11, 17, 10, 0, 0, 0, DateTimeKind.Unspecified), 8, true, 6, 8 },
                    { 14, new DateTime(2026, 11, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), 8, true, 6, 8 },
                    { 15, new DateTime(2026, 11, 6, 12, 0, 0, 0, DateTimeKind.Unspecified), 8, true, 6, 8 },
                    { 16, new DateTime(2026, 10, 30, 13, 0, 0, 0, DateTimeKind.Unspecified), 8, true, 9, 9 },
                    { 17, new DateTime(2026, 11, 11, 11, 0, 0, 0, DateTimeKind.Unspecified), 4, true, 9, 5 },
                    { 18, new DateTime(2026, 11, 4, 10, 0, 0, 0, DateTimeKind.Unspecified), 9, true, 1, 1 },
                    { 19, new DateTime(2026, 10, 11, 11, 0, 0, 0, DateTimeKind.Unspecified), 9, true, 2, 3 },
                    { 20, new DateTime(2026, 11, 14, 16, 0, 0, 0, DateTimeKind.Unspecified), 3, true, 1, 2 },
                    { 21, new DateTime(2026, 11, 9, 10, 0, 0, 0, DateTimeKind.Unspecified), 6, true, 2, 7 },
                    { 22, new DateTime(2026, 11, 7, 11, 0, 0, 0, DateTimeKind.Unspecified), 7, true, 7, 6 },
                    { 23, new DateTime(2026, 11, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), 10, false, 4, 10 },
                    { 24, new DateTime(2026, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 6, true, 2, 7 },
                    { 25, new DateTime(2026, 10, 9, 11, 0, 0, 0, DateTimeKind.Unspecified), 7, true, 7, 6 },
                    { 26, new DateTime(2026, 10, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), 10, false, 4, 10 },
                    { 27, new DateTime(2026, 10, 13, 16, 0, 0, 0, DateTimeKind.Unspecified), 3, true, 1, 2 },
                    { 28, new DateTime(2026, 11, 3, 9, 0, 0, 0, DateTimeKind.Unspecified), 6, true, 2, 7 },
                    { 29, new DateTime(2026, 11, 1, 9, 0, 0, 0, DateTimeKind.Unspecified), 7, true, 7, 6 },
                    { 30, new DateTime(2026, 10, 28, 9, 0, 0, 0, DateTimeKind.Unspecified), 10, false, 5, 10 },
                    { 31, new DateTime(2026, 11, 16, 9, 0, 0, 0, DateTimeKind.Unspecified), 3, true, 10, 2 },
                    { 32, new DateTime(2026, 11, 18, 9, 0, 0, 0, DateTimeKind.Unspecified), 6, true, 10, 7 },
                    { 33, new DateTime(2026, 10, 24, 9, 0, 0, 0, DateTimeKind.Unspecified), 10, false, 4, 10 },
                    { 34, new DateTime(2026, 11, 19, 9, 0, 0, 0, DateTimeKind.Unspecified), 7, true, 7, 6 },
                    { 35, new DateTime(2026, 10, 22, 9, 0, 0, 0, DateTimeKind.Unspecified), 3, true, 1, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_CustomerId_AppointmentDateTime",
                table: "Appointments",
                columns: new[] { "CustomerId", "AppointmentDateTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServiceId",
                table: "Appointments",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_SpecialistId_AppointmentDateTime",
                table: "Appointments",
                columns: new[] { "SpecialistId", "AppointmentDateTime" });

            migrationBuilder.CreateIndex(
                name: "IX_Specialists_PassportNumber",
                table: "Specialists",
                column: "PassportNumber",
                unique: true,
                filter: "[PassportNumber] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "Specialists");
        }
    }
}
