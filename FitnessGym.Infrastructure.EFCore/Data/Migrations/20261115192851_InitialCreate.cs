using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FitnessGym.Infrastructure.EFCore.Data.Migrations
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
                name: "Persons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PassportNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persons", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Phone = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SubscriptionStart = table.Column<DateOnly>(type: "date", nullable: false),
                    SubscriptionEnd = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_Persons_Id",
                        column: x => x.Id,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Trainers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    SpecializationId = table.Column<int>(type: "int", nullable: false),
                    ExperienceYears = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trainers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trainers_Persons_Id",
                        column: x => x.Id,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trainers_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TrainingSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    TrainerId = table.Column<int>(type: "int", nullable: false),
                    StartsTraining = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    EndsTraining = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    HallName = table.Column<int>(type: "int", nullable: false),
                    IsTrial = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainingSessions_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainingSessions_Trainers_TrainerId",
                        column: x => x.TrainerId,
                        principalTable: "Trainers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Persons",
                columns: new[] { "Id", "BirthDate", "FullName", "Gender", "PassportNumber" },
                values: new object[,]
                {
                    { 1, new DateOnly(1988, 3, 14), "Гусев Роман Валерьевич", 0, "5101 111001" },
                    { 2, new DateOnly(1995, 7, 22), "Ершова Полина Дмитриевна", 1, "5102 111002" },
                    { 3, new DateOnly(1979, 11, 5), "Медведев Артур Игоревич", 0, "5103 111003" },
                    { 4, new DateOnly(1992, 1, 30), "Соколова Виктория Андреевна", 1, "5104 111004" },
                    { 5, new DateOnly(2000, 9, 9), "Фролов Никита Сергеевич", 0, "5105 111005" },
                    { 6, new DateOnly(1985, 5, 18), "Афанасьев Владислав Игоревич", 0, "5106 111006" },
                    { 7, new DateOnly(1998, 12, 3), "Григорьева Валерия Павловна", 1, "5107 111007" },
                    { 8, new DateOnly(1990, 6, 25), "Комарова Дарья Алексеевна", 1, "5108 111008" },
                    { 9, new DateOnly(1983, 8, 11), "Степанов Егор Олегович", 0, "5109 111009" },
                    { 10, new DateOnly(2002, 2, 17), "Филиппова Алёна Максимовна", 1, "5110 111010" },
                    { 11, new DateOnly(2003, 4, 20), "Данилов Кирилл Андреевич", 0, "5111 111011" },
                    { 12, new DateOnly(1996, 10, 8), "Никитина София Романовна", 1, "5112 111012" },
                    { 101, new DateOnly(1991, 4, 2), "Тарасов Андрей Викторович", 0, "5201 222001" },
                    { 102, new DateOnly(1987, 9, 19), "Громова Елена Сергеевна", 1, "5202 222002" },
                    { 103, new DateOnly(1999, 12, 14), "Белов Максим Олегович", 0, "5203 222003" },
                    { 104, new DateOnly(1982, 2, 8), "Орлова Наталья Ивановна", 1, "5204 222004" },
                    { 105, new DateOnly(1978, 7, 21), "Крылов Дмитрий Андреевич", 0, "5205 222005" },
                    { 106, new DateOnly(1993, 5, 16), "Соловьёва Анна Михайловна", 1, "5206 222006" },
                    { 107, new DateOnly(1986, 11, 30), "Николаев Артём Павлович", 0, "5207 222007" },
                    { 108, new DateOnly(1989, 8, 25), "Романова Екатерина Дмитриевна", 1, "5208 222008" },
                    { 109, new DateOnly(2001, 1, 7), "Виноградов Сергей Николаевич", 0, "5209 222009" },
                    { 110, new DateOnly(1994, 6, 12), "Полякова Марина Владимировна", 1, "5210 222010" },
                    { 111, new DateOnly(1975, 3, 4), "Тихонов Игорь Алексеевич", 0, "5211 222011" },
                    { 112, new DateOnly(1984, 10, 23), "Киселёва Ольга Андреевна", 1, "5212 222012" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Аэробика" },
                    { 2, "Аквааэробика" },
                    { 3, "Бодибилдинг" },
                    { 4, "Пауэрлифтинг" },
                    { 5, "Кроссфит" },
                    { 6, "Йога" },
                    { 7, "Пилатес" },
                    { 8, "Стретчинг" },
                    { 9, "Единоборства" },
                    { 10, "Танцы" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "Phone", "SubscriptionEnd", "SubscriptionStart" },
                values: new object[,]
                {
                    { 1, "+7 (900) 200-10-01", new DateOnly(2026, 4, 30), new DateOnly(2026, 2, 14) },
                    { 2, "+7 (900) 200-10-02", new DateOnly(2026, 5, 12), new DateOnly(2026, 3, 16) },
                    { 3, "+7 (900) 200-10-03", new DateOnly(2026, 4, 5), new DateOnly(2025, 11, 16) },
                    { 4, "+7 (900) 200-10-04", new DateOnly(2026, 5, 8), new DateOnly(2026, 1, 15) },
                    { 5, "+7 (900) 200-10-05", new DateOnly(2026, 5, 14), new DateOnly(2026, 3, 31) },
                    { 6, "+7 (900) 200-10-06", new DateOnly(2026, 7, 14), new DateOnly(2026, 4, 15) },
                    { 7, "+7 (900) 200-10-07", new DateOnly(2026, 7, 29), new DateOnly(2026, 4, 30) },
                    { 8, "+7 (900) 200-10-08", new DateOnly(2026, 6, 14), new DateOnly(2026, 2, 14) },
                    { 9, "+7 (900) 200-10-09", new DateOnly(2026, 9, 12), new DateOnly(2026, 3, 16) },
                    { 10, "+7 (900) 200-10-10", new DateOnly(2026, 11, 11), new DateOnly(2026, 5, 14) },
                    { 11, "+7 (900) 200-10-11", new DateOnly(2026, 8, 23), new DateOnly(2026, 5, 25) },
                    { 12, "+7 (900) 200-10-12", new DateOnly(2026, 9, 2), new DateOnly(2026, 6, 4) }
                });

            migrationBuilder.InsertData(
                table: "Trainers",
                columns: new[] { "Id", "ExperienceYears", "SpecializationId" },
                values: new object[,]
                {
                    { 101, 4, 5 },
                    { 102, 6, 6 },
                    { 103, 3, 9 },
                    { 104, 9, 7 },
                    { 105, 11, 4 },
                    { 106, 5, 8 },
                    { 107, 8, 9 },
                    { 108, 7, 10 },
                    { 109, 2, 2 },
                    { 110, 5, 1 },
                    { 111, 13, 3 },
                    { 112, 6, 5 }
                });

            migrationBuilder.InsertData(
                table: "TrainingSessions",
                columns: new[] { "Id", "ClientId", "EndsTraining", "HallName", "IsTrial", "StartsTraining", "TrainerId" },
                values: new object[,]
                {
                    { 1, 6, new DateTime(2026, 5, 6, 11, 0, 0, 0, DateTimeKind.Unspecified), 0, false, new DateTime(2026, 5, 6, 10, 0, 0, 0, DateTimeKind.Unspecified), 101 },
                    { 2, 7, new DateTime(2026, 5, 13, 13, 0, 0, 0, DateTimeKind.Unspecified), 0, false, new DateTime(2026, 5, 13, 12, 0, 0, 0, DateTimeKind.Unspecified), 102 },
                    { 3, 8, new DateTime(2026, 5, 21, 19, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(2026, 5, 21, 18, 0, 0, 0, DateTimeKind.Unspecified), 103 },
                    { 4, 9, new DateTime(2026, 4, 7, 12, 0, 0, 0, DateTimeKind.Unspecified), 0, false, new DateTime(2026, 4, 7, 11, 0, 0, 0, DateTimeKind.Unspecified), 101 },
                    { 5, 10, new DateTime(2026, 6, 8, 16, 0, 0, 0, DateTimeKind.Unspecified), 0, false, new DateTime(2026, 6, 8, 15, 0, 0, 0, DateTimeKind.Unspecified), 101 },
                    { 6, 9, new DateTime(2026, 5, 15, 12, 20, 0, 0, DateTimeKind.Utc), 1, false, new DateTime(2026, 5, 15, 11, 20, 0, 0, DateTimeKind.Utc), 104 },
                    { 7, 1, new DateTime(2026, 5, 15, 11, 45, 0, 0, DateTimeKind.Utc), 2, false, new DateTime(2026, 5, 15, 10, 30, 0, 0, DateTimeKind.Utc), 105 },
                    { 8, 6, new DateTime(2026, 5, 4, 10, 0, 0, 0, DateTimeKind.Unspecified), 3, false, new DateTime(2026, 5, 4, 9, 0, 0, 0, DateTimeKind.Unspecified), 101 },
                    { 9, 7, new DateTime(2026, 5, 9, 15, 0, 0, 0, DateTimeKind.Unspecified), 1, false, new DateTime(2026, 5, 9, 14, 0, 0, 0, DateTimeKind.Unspecified), 102 },
                    { 10, 8, new DateTime(2026, 4, 12, 17, 0, 0, 0, DateTimeKind.Unspecified), 5, true, new DateTime(2026, 4, 12, 16, 0, 0, 0, DateTimeKind.Unspecified), 103 },
                    { 11, 9, new DateTime(2026, 5, 18, 20, 0, 0, 0, DateTimeKind.Unspecified), 3, false, new DateTime(2026, 5, 18, 19, 0, 0, 0, DateTimeKind.Unspecified), 101 },
                    { 12, 10, new DateTime(2026, 4, 23, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, false, new DateTime(2026, 4, 23, 7, 0, 0, 0, DateTimeKind.Unspecified), 102 },
                    { 13, 6, new DateTime(2026, 5, 25, 12, 0, 0, 0, DateTimeKind.Unspecified), 3, false, new DateTime(2026, 5, 25, 11, 0, 0, 0, DateTimeKind.Unspecified), 103 },
                    { 14, 7, new DateTime(2026, 4, 15, 21, 0, 0, 0, DateTimeKind.Unspecified), 4, false, new DateTime(2026, 4, 15, 20, 0, 0, 0, DateTimeKind.Unspecified), 101 },
                    { 15, 8, new DateTime(2026, 6, 5, 13, 0, 0, 0, DateTimeKind.Unspecified), 3, true, new DateTime(2026, 6, 5, 12, 0, 0, 0, DateTimeKind.Unspecified), 102 },
                    { 16, 9, new DateTime(2026, 6, 11, 18, 0, 0, 0, DateTimeKind.Unspecified), 2, false, new DateTime(2026, 6, 11, 17, 0, 0, 0, DateTimeKind.Unspecified), 103 },
                    { 17, 10, new DateTime(2026, 5, 20, 9, 0, 0, 0, DateTimeKind.Unspecified), 4, false, new DateTime(2026, 5, 20, 8, 0, 0, 0, DateTimeKind.Unspecified), 104 },
                    { 18, 6, new DateTime(2026, 5, 27, 14, 0, 0, 0, DateTimeKind.Unspecified), 5, false, new DateTime(2026, 5, 27, 13, 0, 0, 0, DateTimeKind.Unspecified), 101 },
                    { 19, 7, new DateTime(2026, 5, 3, 11, 0, 0, 0, DateTimeKind.Unspecified), 1, false, new DateTime(2026, 5, 3, 10, 0, 0, 0, DateTimeKind.Unspecified), 102 },
                    { 20, 8, new DateTime(2026, 4, 19, 16, 0, 0, 0, DateTimeKind.Unspecified), 0, false, new DateTime(2026, 4, 19, 15, 0, 0, 0, DateTimeKind.Unspecified), 104 },
                    { 21, 9, new DateTime(2026, 6, 14, 19, 0, 0, 0, DateTimeKind.Unspecified), 1, true, new DateTime(2026, 6, 14, 18, 0, 0, 0, DateTimeKind.Unspecified), 105 },
                    { 22, 10, new DateTime(2026, 5, 22, 8, 0, 0, 0, DateTimeKind.Unspecified), 3, false, new DateTime(2026, 5, 22, 7, 0, 0, 0, DateTimeKind.Unspecified), 106 },
                    { 23, 6, new DateTime(2026, 4, 9, 17, 0, 0, 0, DateTimeKind.Unspecified), 5, false, new DateTime(2026, 4, 9, 16, 0, 0, 0, DateTimeKind.Unspecified), 107 },
                    { 24, 7, new DateTime(2026, 5, 15, 21, 0, 0, 0, DateTimeKind.Unspecified), 4, false, new DateTime(2026, 5, 15, 20, 0, 0, 0, DateTimeKind.Unspecified), 108 },
                    { 25, 8, new DateTime(2026, 6, 25, 10, 0, 0, 0, DateTimeKind.Unspecified), 0, true, new DateTime(2026, 6, 25, 9, 0, 0, 0, DateTimeKind.Unspecified), 109 },
                    { 26, 9, new DateTime(2026, 4, 28, 19, 0, 0, 0, DateTimeKind.Unspecified), 3, false, new DateTime(2026, 4, 28, 18, 0, 0, 0, DateTimeKind.Unspecified), 110 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Trainers_SpecializationId",
                table: "Trainers",
                column: "SpecializationId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSessions_ClientId",
                table: "TrainingSessions",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainingSessions_TrainerId",
                table: "TrainingSessions",
                column: "TrainerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrainingSessions");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Trainers");

            migrationBuilder.DropTable(
                name: "Persons");

            migrationBuilder.DropTable(
                name: "Specializations");
        }
    }
}
