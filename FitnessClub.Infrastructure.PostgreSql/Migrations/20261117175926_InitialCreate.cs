using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnessClub.Infrastructure.PostgreSql.Migrations;

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
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Phone = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                SubscriptionStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                SubscriptionEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                PassportNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                FullName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                Gender = table.Column<int>(type: "integer", nullable: false),
                BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Clients", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Specializations",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Specializations", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Trainers",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                SpecializationId = table.Column<int>(type: "integer", nullable: false),
                ExperienceYears = table.Column<int>(type: "integer", nullable: false),
                PassportNumber = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                FullName = table.Column<string>(type: "character varying(150)", maxLength: 150, nullable: false),
                Gender = table.Column<int>(type: "integer", nullable: false),
                BirthDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
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
            name: "Lessons",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                ClientId = table.Column<int>(type: "integer", nullable: false),
                TrainerId = table.Column<int>(type: "integer", nullable: false),
                DateStart = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                DateEnd = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                Hall = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                IsTrial = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Lessons", x => x.Id);
                table.ForeignKey(
                    name: "FK_Lessons_Clients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "Clients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Lessons_Trainers_TrainerId",
                    column: x => x.TrainerId,
                    principalTable: "Trainers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.InsertData(
            table: "Clients",
            columns: new[] { "Id", "BirthDate", "FullName", "Gender", "PassportNumber", "Phone", "SubscriptionEnd", "SubscriptionStart" },
            values: new object[,]
            {
                { 1, new DateTime(1991, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Петров Пётр Сергеевич", 0, "4501 200301", "+7 911 100-11-11", new DateTime(2026, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 2, new DateTime(1994, 5, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "Орлова Марина Ивановна", 1, "4501 204002", "+7 911 100-22-22", new DateTime(2026, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 9, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 3, new DateTime(1987, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Сидоров Николай Петрович", 0, "4541 220003", "+7 911 100-33-33", new DateTime(2026, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 4, new DateTime(1996, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Белова Ксения Павловна", 1, "4531 200204", "+7 911 100-44-44", new DateTime(2027, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 10, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 5, new DateTime(1989, 1, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), "Гусев Андрей Юрьевич", 0, "4501 200005", "+7 911 100-55-55", new DateTime(2026, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 5, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 6, new DateTime(1993, 4, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дмитриева Светлана Олеговна", 1, "4521 200206", "+7 911 100-66-66", new DateTime(2027, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 7, new DateTime(1985, 7, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Зайцев Роман Викторович", 0, "4511 200307", "+7 911 100-77-77", new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 2, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 8, new DateTime(1998, 2, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Киселёва Дарья Максимовна", 1, "4541 205008", "+7 911 100-88-88", new DateTime(2026, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 9, new DateTime(1990, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "Львов Григорий Артёмович", 0, "4561 208009", "+7 911 100-99-99", new DateTime(2026, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                { 10, new DateTime(1997, 9, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Миронова Валерия Игоревна", 1, "4511 200910", "+7 911 100-00-00", new DateTime(2027, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 7, 17, 0, 0, 0, 0, DateTimeKind.Unspecified) }
            });

        migrationBuilder.InsertData(
            table: "Specializations",
            columns: new[] { "Id", "Name" },
            values: new object[,]
            {
                { 1, "Йога" },
                { 2, "Пилатес" },
                { 3, "Бокс" },
                { 4, "Плавание" },
                { 5, "Кроссфит" },
                { 6, "Теннис" },
                { 7, "Лёгкая атлетика" },
                { 8, "Тяжёлая атлетика" },
                { 9, "Фитнес" },
                { 10, "Реабилитация" }
            });

        migrationBuilder.InsertData(
            table: "Trainers",
            columns: new[] { "Id", "BirthDate", "ExperienceYears", "FullName", "Gender", "PassportNumber", "SpecializationId" },
            values: new object[,]
            {
                { 1, new DateTime(1980, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), 3, "Иванов Пётр Иванович", 0, "4513 104010", 1 },
                { 2, new DateTime(1975, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), 7, "Смирнов Алексей Петрович", 0, "4532 100002", 4 },
                { 3, new DateTime(1988, 7, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), 10, "Кузнецова Мария Сергеевна", 1, "4521 100003", 1 },
                { 4, new DateTime(1990, 1, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Соколов Дмитрий Андреевич", 0, "4567 102004", 3 },
                { 5, new DateTime(1985, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 5, "Попова Елена Викторовна", 1, "4589 100008", 2 },
                { 6, new DateTime(1978, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 12, "Лебедев Артём Олегович", 0, "4512 100006", 5 },
                { 7, new DateTime(1995, 4, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), 1, "Козлов Игорь Владимирович", 0, "4522 101007", 8 },
                { 8, new DateTime(1983, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), 6, "Новикова Ольга Дмитриевна", 1, "4577 100305", 9 },
                { 9, new DateTime(1992, 2, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Морозов Павел Игоревич", 0, "4553 100009", 6 },
                { 10, new DateTime(1986, 4, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Волкова Анна Александровна", 1, "4543 105001", 10 }
            });

        migrationBuilder.InsertData(
            table: "Lessons",
            columns: new[] { "Id", "ClientId", "DateEnd", "DateStart", "Hall", "IsTrial", "TrainerId" },
            values: new object[,]
            {
                { 1, 1, null, new DateTime(2026, 11, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), "Зал йоги", true, 3 },
                { 2, 2, null, new DateTime(2026, 11, 10, 12, 0, 0, 0, DateTimeKind.Unspecified), "Зал йоги", false, 3 },
                { 3, 3, null, new DateTime(2026, 11, 15, 18, 0, 0, 0, DateTimeKind.Unspecified), "Зал йоги", false, 5 },
                { 4, 1, null, new DateTime(2026, 10, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), "Зал йоги", false, 3 },
                { 5, 4, null, new DateTime(2026, 11, 3, 9, 0, 0, 0, DateTimeKind.Unspecified), "Основной зал", false, 1 },
                { 6, 5, null, new DateTime(2026, 11, 8, 11, 0, 0, 0, DateTimeKind.Unspecified), "Основной зал", false, 1 },
                { 7, 6, null, new DateTime(2026, 11, 12, 16, 0, 0, 0, DateTimeKind.Unspecified), "Основной зал", false, 1 },
                { 8, 7, null, new DateTime(2026, 11, 20, 19, 0, 0, 0, DateTimeKind.Unspecified), "Основной зал", true, 1 },
                { 9, 8, new DateTime(2026, 11, 22, 8, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 11, 22, 8, 0, 0, 0, DateTimeKind.Unspecified), "Основной зал", false, 1 },
                { 10, 9, null, new DateTime(2026, 12, 5, 10, 0, 0, 0, DateTimeKind.Unspecified), "Основной зал", false, 1 },
                { 11, 10, null, new DateTime(2026, 11, 6, 10, 0, 0, 0, DateTimeKind.Unspecified), "Бассейн", false, 2 },
                { 12, 1, new DateTime(2026, 11, 13, 11, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 11, 13, 10, 0, 0, 0, DateTimeKind.Unspecified), "Бассейн", true, 2 },
                { 13, 2, null, new DateTime(2026, 11, 21, 10, 0, 0, 0, DateTimeKind.Unspecified), "Бассейн", false, 2 },
                { 14, 3, null, new DateTime(2026, 11, 27, 10, 0, 0, 0, DateTimeKind.Unspecified), "Бассейн", false, 2 },
                { 15, 4, new DateTime(2026, 11, 7, 11, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 11, 7, 10, 0, 0, 0, DateTimeKind.Unspecified), "Зал единоборств", false, 4 },
                { 16, 5, null, new DateTime(2026, 11, 14, 10, 0, 0, 0, DateTimeKind.Unspecified), "Зал единоборств", true, 4 }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Lessons_ClientId",
            table: "Lessons",
            column: "ClientId");

        migrationBuilder.CreateIndex(
            name: "IX_Lessons_TrainerId",
            table: "Lessons",
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
            name: "Lessons");

        migrationBuilder.DropTable(
            name: "Clients");

        migrationBuilder.DropTable(
            name: "Trainers");

        migrationBuilder.DropTable(
            name: "Specializations");
    }
}
