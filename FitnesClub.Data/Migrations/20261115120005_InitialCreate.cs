using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnesClub.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MembershipStartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MembershipEndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SpecializationId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    WorkExperienceYears = table.Column<int>(type: "int", nullable: false),
                    PassportNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(25)", maxLength: 25, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainers_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrainerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LessonDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: false),
                    RoomName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    IsTrial = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "BirthDate", "Email", "FirstName", "Gender", "LastName", "MembershipEndDate", "MembershipStartDate", "PassportNumber", "PhoneNumber" },
                values: new object[,]
                {
                    { new Guid("33333333-0000-0000-0000-000000000001"), new DateOnly(1995, 5, 10), null, "Иван", "Male", "Иванов", new DateTime(2026, 10, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), "2000000001", "+79991111111" },
                    { new Guid("33333333-0000-0000-0000-000000000002"), new DateOnly(1998, 8, 15), null, "Анна", "Female", "Петрова", new DateTime(2026, 11, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 6, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), "2000000002", "+79991111112" },
                    { new Guid("33333333-0000-0000-0000-000000000003"), new DateOnly(1992, 1, 20), null, "Алексей", "Male", "Сидоров", new DateTime(2026, 9, 18, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 3, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), "2000000003", "+79991111113" },
                    { new Guid("33333333-0000-0000-0000-000000000004"), new DateOnly(1997, 3, 12), null, "Екатерина", "Female", "Смирнова", new DateTime(2026, 12, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), "2000000004", "+79991111114" },
                    { new Guid("33333333-0000-0000-0000-000000000005"), new DateOnly(1989, 10, 3), null, "Михаил", "Male", "Кузнецов", new DateTime(2026, 8, 29, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 1, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), "2000000005", "+79991111115" },
                    { new Guid("33333333-0000-0000-0000-000000000006"), new DateOnly(1994, 6, 25), null, "Ольга", "Female", "Попова", new DateTime(2026, 10, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), "2000000006", "+79991111116" },
                    { new Guid("33333333-0000-0000-0000-000000000007"), new DateOnly(1990, 9, 17), null, "Артем", "Male", "Васильев", new DateTime(2026, 9, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), "2000000007", "+79991111117" },
                    { new Guid("33333333-0000-0000-0000-000000000008"), new DateOnly(1996, 11, 8), null, "Наталья", "Female", "Соколова", new DateTime(2026, 11, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), "2000000008", "+79991111118" },
                    { new Guid("33333333-0000-0000-0000-000000000009"), new DateOnly(1987, 4, 14), null, "Павел", "Male", "Михайлов", new DateTime(2026, 9, 13, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), "2000000009", "+79991111119" },
                    { new Guid("33333333-0000-0000-0000-000000000010"), new DateOnly(1993, 12, 30), null, "Ирина", "Female", "Новикова", new DateTime(2026, 10, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 28, 12, 0, 0, 0, DateTimeKind.Unspecified), "2000000010", "+79991111120" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("11111111-0000-0000-0000-000000000001"), "Фитнес" },
                    { new Guid("11111111-0000-0000-0000-000000000002"), "Йога" },
                    { new Guid("11111111-0000-0000-0000-000000000003"), "Силовые тренировки" },
                    { new Guid("11111111-0000-0000-0000-000000000004"), "Пилатес" },
                    { new Guid("11111111-0000-0000-0000-000000000005"), "Кардио" },
                    { new Guid("11111111-0000-0000-0000-000000000006"), "Стретчинг" },
                    { new Guid("11111111-0000-0000-0000-000000000007"), "Функциональный тренинг" },
                    { new Guid("11111111-0000-0000-0000-000000000008"), "Кроссфит" },
                    { new Guid("11111111-0000-0000-0000-000000000009"), "Бокс" },
                    { new Guid("11111111-0000-0000-0000-000000000010"), "Танцевальные тренировки" }
                });

            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "Id", "BirthDate", "Email", "FirstName", "Gender", "LastName", "PassportNumber", "PhoneNumber", "SpecializationId", "WorkExperienceYears" },
                values: new object[,]
                {
                    { new Guid("22222222-0000-0000-0000-000000000001"), new DateOnly(1985, 3, 15), null, "Алексей", "Male", "Смирнов", "1000000001", "+79990000001", new Guid("11111111-0000-0000-0000-000000000001"), 8 },
                    { new Guid("22222222-0000-0000-0000-000000000002"), new DateOnly(1990, 7, 20), null, "Елена", "Female", "Волкова", "1000000002", "+79990000002", new Guid("11111111-0000-0000-0000-000000000002"), 6 },
                    { new Guid("22222222-0000-0000-0000-000000000003"), new DateOnly(1982, 11, 5), null, "Дмитрий", "Male", "Соколов", "1000000003", "+79990000003", new Guid("11111111-0000-0000-0000-000000000003"), 12 },
                    { new Guid("22222222-0000-0000-0000-000000000004"), new DateOnly(1992, 2, 10), null, "Ольга", "Female", "Морозова", "1000000004", "+79990000004", new Guid("11111111-0000-0000-0000-000000000004"), 3 },
                    { new Guid("22222222-0000-0000-0000-000000000005"), new DateOnly(1980, 9, 25), null, "Игорь", "Male", "Новиков", "1000000005", "+79990000005", new Guid("11111111-0000-0000-0000-000000000005"), 15 },
                    { new Guid("22222222-0000-0000-0000-000000000006"), new DateOnly(1995, 4, 18), null, "Анна", "Female", "Федорова", "1000000006", "+79990000006", new Guid("11111111-0000-0000-0000-000000000006"), 2 },
                    { new Guid("22222222-0000-0000-0000-000000000007"), new DateOnly(1988, 6, 12), null, "Максим", "Male", "Козлов", "1000000007", "+79990000007", new Guid("11111111-0000-0000-0000-000000000007"), 7 },
                    { new Guid("22222222-0000-0000-0000-000000000008"), new DateOnly(1986, 12, 1), null, "Татьяна", "Female", "Лебедева", "1000000008", "+79990000008", new Guid("11111111-0000-0000-0000-000000000008"), 10 },
                    { new Guid("22222222-0000-0000-0000-000000000009"), new DateOnly(1984, 1, 30), null, "Сергей", "Male", "Петров", "1000000009", "+79990000009", new Guid("11111111-0000-0000-0000-000000000009"), 9 },
                    { new Guid("22222222-0000-0000-0000-000000000010"), new DateOnly(1991, 8, 22), null, "Мария", "Female", "Васильева", "1000000010", "+79990000010", new Guid("11111111-0000-0000-0000-000000000010"), 4 }
                });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "Duration", "IsTrial", "LessonDateTime", "MemberId", "RoomName", "TrainerId" },
                values: new object[,]
                {
                    { new Guid("44444444-0000-0000-0000-000000000001"), new TimeSpan(0, 1, 0, 0, 0), false, new DateTime(2026, 9, 29, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-0000-0000-0000-000000000001"), "Зал №1", new Guid("22222222-0000-0000-0000-000000000001") },
                    { new Guid("44444444-0000-0000-0000-000000000002"), new TimeSpan(0, 2, 0, 0, 0), true, new DateTime(2026, 9, 30, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-0000-0000-0000-000000000002"), "Зал №2", new Guid("22222222-0000-0000-0000-000000000002") },
                    { new Guid("44444444-0000-0000-0000-000000000003"), new TimeSpan(0, 1, 0, 0, 0), false, new DateTime(2026, 9, 26, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-0000-0000-0000-000000000003"), "Зал №1", new Guid("22222222-0000-0000-0000-000000000003") },
                    { new Guid("44444444-0000-0000-0000-000000000004"), new TimeSpan(0, 1, 0, 0, 0), false, new DateTime(2026, 10, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-0000-0000-0000-000000000004"), "Зал №1", new Guid("22222222-0000-0000-0000-000000000001") },
                    { new Guid("44444444-0000-0000-0000-000000000005"), new TimeSpan(0, 2, 0, 0, 0), true, new DateTime(2026, 10, 2, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-0000-0000-0000-000000000005"), "Зал №3", new Guid("22222222-0000-0000-0000-000000000003") },
                    { new Guid("44444444-0000-0000-0000-000000000006"), new TimeSpan(0, 1, 0, 0, 0), false, new DateTime(2026, 10, 3, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-0000-0000-0000-000000000006"), "Зал №1", new Guid("22222222-0000-0000-0000-000000000001") },
                    { new Guid("44444444-0000-0000-0000-000000000007"), new TimeSpan(0, 1, 0, 0, 0), false, new DateTime(2026, 9, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-0000-0000-0000-000000000007"), "Зал №2", new Guid("22222222-0000-0000-0000-000000000004") },
                    { new Guid("44444444-0000-0000-0000-000000000008"), new TimeSpan(0, 2, 0, 0, 0), false, new DateTime(2026, 10, 4, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-0000-0000-0000-000000000008"), "Зал №3", new Guid("22222222-0000-0000-0000-000000000005") },
                    { new Guid("44444444-0000-0000-0000-000000000009"), new TimeSpan(0, 1, 0, 0, 0), true, new DateTime(2026, 10, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-0000-0000-0000-000000000009"), "Зал №1", new Guid("22222222-0000-0000-0000-000000000003") },
                    { new Guid("44444444-0000-0000-0000-000000000010"), new TimeSpan(0, 1, 0, 0, 0), false, new DateTime(2026, 10, 6, 12, 0, 0, 0, DateTimeKind.Unspecified), new Guid("33333333-0000-0000-0000-000000000010"), "Зал №3", new Guid("22222222-0000-0000-0000-000000000005") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_MemberId",
                table: "Bookings",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TrainerId",
                table: "Bookings",
                column: "TrainerId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_SpecializationId",
                table: "Trainers",
                column: "SpecializationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Members");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "Specializations");
        }
    }
}
