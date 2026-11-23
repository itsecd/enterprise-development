using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BeautySalon.Infrastructure.Migrations
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
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Masters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassportNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Masters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsRegularClient = table.Column<bool>(type: "bit", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    MasterId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Masters_MasterId",
                        column: x => x.MasterId,
                        principalTable: "Masters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "BirthDate", "FullName", "Gender", "Phone" },
                values: new object[,]
                {
                    { 1, new DateTime(1980, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Иванов Иван Иванович", 0, "+7 (900) 000-00-01" },
                    { 2, new DateTime(1990, 6, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Петрова Мария Сергеевна", 1, "+7 (900) 000-00-02" },
                    { 3, new DateTime(2000, 12, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сидоров Алексей Петрович", 0, "+7 (900) 000-00-03" },
                    { 4, new DateTime(1985, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кузнецова Ольга Дмитриевна", 1, "+7 (900) 000-00-04" },
                    { 5, new DateTime(1995, 8, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Смирнов Дмитрий Андреевич", 0, "+7 (900) 000-00-05" },
                    { 6, new DateTime(1988, 3, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Волкова Анна Игоревна", 1, "+7 (900) 000-00-06" },
                    { 7, new DateTime(1975, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Морозов Николай Васильевич", 0, "+7 (900) 000-00-07" },
                    { 8, new DateTime(1992, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Павлова Елена Олеговна", 1, "+7 (900) 000-00-08" },
                    { 9, new DateTime(1998, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Соколов Артем Максимович", 0, "+7 (900) 000-00-09" },
                    { 10, new DateTime(1983, 9, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Лебедева Виктория Сергеевна", 1, "+7 (900) 000-00-10" },
                    { 11, new DateTime(1970, 4, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Козлов Михаил Юрьевич", 0, "+7 (900) 000-00-11" },
                    { 12, new DateTime(1999, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новикова Дарья Александровна", 1, "+7 (900) 000-00-12" },
                    { 13, new DateTime(1987, 8, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Егоров Сергей Владимирович", 0, "+7 (900) 000-00-13" },
                    { 14, new DateTime(1994, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Орлова Ксения Павловна", 1, "+7 (900) 000-00-14" },
                    { 15, new DateTime(1981, 6, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "Федоров Андрей Игоревич", 0, "+7 (900) 000-00-15" }
                });

            migrationBuilder.InsertData(
                table: "Masters",
                columns: new[] { "Id", "ExperienceYears", "FullName", "Gender", "PassportNumber", "Specialization" },
                values: new object[,]
                {
                    { 101, 10, "Петров Петр Петрович", 0, "11 11 111111", "Парикмахер" },
                    { 102, 7, "Смирнова Ольга Ивановна", 1, "22 22 222222", "Косметолог" },
                    { 103, 2, "Сидоров Сидор Сидорович", 0, "33 33 333333", "Массажист" },
                    { 104, 12, "Кузнецова Мария Ивановна", 1, "44 44 444444", "Мастер маникюра" },
                    { 105, 6, "Попов Алексей Николаевич", 0, "55 55 555555", "Визажист" },
                    { 106, 15, "Васильева Елена Дмитриевна", 1, "66 66 666666", "Парикмахер" },
                    { 107, 4, "Михайлов Игорь Олегович", 0, "77 77 777777", "Массажист" },
                    { 108, 8, "Федорова Анна Сергеевна", 1, "88 88 888888", "Косметолог" },
                    { 109, 1, "Николаев Дмитрий Андреевич", 0, "99 99 999999", "Мастер маникюра" },
                    { 110, 9, "Захарова Ирина Викторовна", 1, "10 10 101010", "Визажист" },
                    { 111, 5, "Морозов Павел Валерьевич", 0, "12 12 121212", "Парикмахер" },
                    { 112, 3, "Волкова Оксана Юрьевна", 1, "13 13 131313", "Косметолог" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Category", "Cost", "Duration", "Name" },
                values: new object[,]
                {
                    { 201, "Волосы", 1500m, new TimeSpan(0, 1, 0, 0, 0), "Стрижка" },
                    { 202, "Волосы", 3500m, new TimeSpan(0, 2, 0, 0, 0), "Окрашивание" },
                    { 203, "Ногти", 1000m, new TimeSpan(0, 1, 0, 0, 0), "Маникюр" },
                    { 204, "Ногти", 1500m, new TimeSpan(0, 1, 30, 0, 0), "Педикюр" },
                    { 205, "Лицо", 2500m, new TimeSpan(0, 1, 30, 0, 0), "Чистка лица" },
                    { 206, "Тело", 2000m, new TimeSpan(0, 1, 0, 0, 0), "Массаж спины" },
                    { 207, "Волосы", 800m, new TimeSpan(0, 0, 30, 0, 0), "Укладка" },
                    { 208, "Лицо", 5000m, new TimeSpan(0, 2, 0, 0, 0), "Ботокс" },
                    { 209, "Лицо", 3000m, new TimeSpan(0, 1, 30, 0, 0), "Наращивание ресниц" },
                    { 210, "Лицо", 700m, new TimeSpan(0, 0, 30, 0, 0), "Коррекция бровей" }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "ClientId", "IsRegularClient", "MasterId", "ServiceId", "StartTime" },
                values: new object[,]
                {
                    { 1, 1, false, 101, 201, new DateTime(2023, 10, 1, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, 2, false, 101, 203, new DateTime(2023, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, 3, false, 101, 205, new DateTime(2023, 10, 1, 13, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, 1, true, 102, 201, new DateTime(2023, 11, 1, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, 1, true, 102, 201, new DateTime(2023, 11, 1, 15, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, 2, false, 102, 201, new DateTime(2023, 11, 1, 17, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, 3, false, 102, 201, new DateTime(2023, 11, 1, 19, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, 2, false, 103, 202, new DateTime(2023, 10, 2, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, 3, false, 103, 202, new DateTime(2023, 10, 1, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, 1, true, 103, 202, new DateTime(2023, 10, 2, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 11, 1, true, 103, 202, new DateTime(2023, 10, 1, 11, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 12, 2, false, 104, 203, new DateTime(2023, 11, 2, 12, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 13, 3, false, 104, 203, new DateTime(2023, 12, 1, 16, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 14, 3, false, 102, 204, new DateTime(2023, 10, 5, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 15, 2, false, 103, 204, new DateTime(2023, 10, 5, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 16, 4, false, 104, 206, new DateTime(2023, 9, 5, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 17, 4, false, 104, 206, new DateTime(2023, 12, 7, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 18, 5, false, 105, 207, new DateTime(2023, 9, 12, 10, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 19, 6, false, 106, 208, new DateTime(2023, 9, 11, 14, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 20, 7, false, 108, 209, new DateTime(2023, 11, 3, 12, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ClientId",
                table: "Appointments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_MasterId",
                table: "Appointments",
                column: "MasterId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServiceId",
                table: "Appointments",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Masters");

            migrationBuilder.DropTable(
                name: "Services");
        }
    }
}
