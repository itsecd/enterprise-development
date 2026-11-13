using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnessClub.Infrastructure.EfCore.Migrations
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Phone = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    SubscriptionStartDate = table.Column<DateTime>(type: "date", nullable: false),
                    SubscriptionEndDate = table.Column<DateTime>(type: "date", nullable: false),
                    PassportNumber = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Halls",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Capacity = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Halls", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true)
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
                    WorkExperience = table.Column<int>(type: "integer", nullable: false),
                    PassportNumber = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    FullName = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    Gender = table.Column<int>(type: "integer", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "date", nullable: false)
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
                name: "Trainings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientId = table.Column<int>(type: "integer", nullable: false),
                    TrainerId = table.Column<int>(type: "integer", nullable: false),
                    HallId = table.Column<int>(type: "integer", nullable: false),
                    StartTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    DurationMinutes = table.Column<int>(type: "integer", nullable: false),
                    IsTrial = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainings_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trainings_Halls_HallId",
                        column: x => x.HallId,
                        principalTable: "Halls",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Trainings_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: ["Id", "BirthDate", "FullName", "Gender", "PassportNumber", "Phone", "SubscriptionEndDate", "SubscriptionStartDate"],
                values: new object[,]
                {
                    { 1, new DateTime(1991, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Иванов Иван Александрович", 0, "1000 100001", "+79990000001", new DateTime(2027, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 2, new DateTime(1992, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Петрова Анна Александрович", 1, "1000 100002", "+79990000002", new DateTime(2027, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 3, new DateTime(1993, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Смирнов Алексей Александрович", 0, "1000 100003", "+79990000003", new DateTime(2026, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 4, new DateTime(1994, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Кузнецова Мария Александрович", 1, "1000 100004", "+79990000004", new DateTime(2027, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 5, new DateTime(1995, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Соколов Дмитрий Александрович", 0, "1000 100005", "+79990000005", new DateTime(2027, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 6, new DateTime(1996, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Попова Елена Александрович", 1, "1000 100006", "+79990000006", new DateTime(2026, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 7, new DateTime(1997, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Лебедев Сергей Александрович", 0, "1000 100007", "+79990000007", new DateTime(2027, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 8, new DateTime(1998, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Волкова Ольга Александрович", 1, "1000 100008", "+79990000008", new DateTime(2027, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 9, new DateTime(1999, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Козлов Андрей Александрович", 0, "1000 100009", "+79990000009", new DateTime(2026, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) },
                    { 10, new DateTime(2000, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Новикова Наталья Александрович", 1, "1000 100010", "+79990000010", new DateTime(2027, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2026, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Halls",
                columns: ["Id", "Capacity", "Name"],
                values: new object[,]
                {
                    { 1, 12, "Зал №1" },
                    { 2, 14, "Зал №2" },
                    { 3, 16, "Зал №3" },
                    { 4, 18, "Зал №4" },
                    { 5, 20, "Зал №5" },
                    { 6, 22, "Зал №6" },
                    { 7, 24, "Зал №7" },
                    { 8, 26, "Зал №8" },
                    { 9, 28, "Зал №9" },
                    { 10, 30, "Зал №10" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: ["Id", "Description", "Name"],
                values: new object[,]
                {
                    { 1, "Направление тренировок: силовой тренинг.", "Силовой тренинг" },
                    { 2, "Направление тренировок: общий фитнес.", "Общий фитнес" },
                    { 3, "Направление тренировок: йога.", "Йога" },
                    { 4, "Направление тренировок: пилатес.", "Пилатес" },
                    { 5, "Направление тренировок: кардио.", "Кардио" },
                    { 6, "Направление тренировок: бокс.", "Бокс" },
                    { 7, "Направление тренировок: плавание.", "Плавание" },
                    { 8, "Направление тренировок: стретчинг.", "Стретчинг" },
                    { 9, "Направление тренировок: кроссфит.", "Кроссфит" },
                    { 10, "Направление тренировок: функциональный тренинг.", "Функциональный тренинг" }
                });

            migrationBuilder.InsertData(
                table: "Trainers",
                columns: ["Id", "BirthDate", "FullName", "Gender", "PassportNumber", "SpecializationId", "WorkExperience"],
                values: new object[,]
                {
                    { 1, new DateTime(1986, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тренер 1", 0, "2000 200001", 1, 1 },
                    { 2, new DateTime(1987, 2, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тренер 2", 1, "2000 200002", 2, 2 },
                    { 3, new DateTime(1988, 3, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тренер 3", 0, "2000 200003", 3, 3 },
                    { 4, new DateTime(1989, 4, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тренер 4", 1, "2000 200004", 4, 4 },
                    { 5, new DateTime(1990, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тренер 5", 0, "2000 200005", 5, 5 },
                    { 6, new DateTime(1991, 6, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тренер 6", 1, "2000 200006", 6, 6 },
                    { 7, new DateTime(1992, 7, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тренер 7", 0, "2000 200007", 7, 7 },
                    { 8, new DateTime(1993, 8, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тренер 8", 1, "2000 200008", 8, 8 },
                    { 9, new DateTime(1994, 9, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тренер 9", 0, "2000 200009", 9, 9 },
                    { 10, new DateTime(1995, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Тренер 10", 1, "2000 200010", 10, 10 }
                });

            migrationBuilder.InsertData(
                table: "Trainings",
                columns: ["Id", "ClientId", "DurationMinutes", "HallId", "IsTrial", "StartTime", "TrainerId"],
                values: new object[,]
                {
                    { 1, 1, 60, 1, false, new DateTime(2026, 11, 13, 11, 30, 0, 0, DateTimeKind.Utc), 1 },
                    { 2, 2, 60, 2, true, new DateTime(2026, 11, 2, 10, 0, 0, 0, DateTimeKind.Utc), 2 },
                    { 3, 3, 60, 3, false, new DateTime(2026, 11, 3, 10, 0, 0, 0, DateTimeKind.Utc), 3 },
                    { 4, 4, 60, 4, true, new DateTime(2026, 11, 4, 10, 0, 0, 0, DateTimeKind.Utc), 4 },
                    { 5, 5, 60, 5, false, new DateTime(2026, 11, 5, 10, 0, 0, 0, DateTimeKind.Utc), 5 },
                    { 6, 6, 60, 6, true, new DateTime(2026, 11, 6, 10, 0, 0, 0, DateTimeKind.Utc), 6 },
                    { 7, 7, 60, 7, false, new DateTime(2026, 11, 7, 10, 0, 0, 0, DateTimeKind.Utc), 7 },
                    { 8, 8, 60, 8, true, new DateTime(2026, 11, 8, 10, 0, 0, 0, DateTimeKind.Utc), 8 },
                    { 9, 9, 60, 9, false, new DateTime(2026, 11, 9, 10, 0, 0, 0, DateTimeKind.Utc), 9 },
                    { 10, 10, 60, 10, true, new DateTime(2026, 11, 10, 10, 0, 0, 0, DateTimeKind.Utc), 10 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clients_PassportNumber",
                table: "Clients",
                column: "PassportNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_PassportNumber",
                table: "Trainers",
                column: "PassportNumber",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_SpecializationId",
                table: "Trainers",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_ClientId",
                table: "Trainings",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_HallId",
                table: "Trainings",
                column: "HallId");

            migrationBuilder.CreateIndex(
                name: "IX_Trainings_TrainerId",
                table: "Trainings",
                column: "TrainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Trainings");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Halls");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "Specializations");
        }
    }
}
