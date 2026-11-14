using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Vet.Infrastructure.EfCore.Migrations
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
                name: "Breeds",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Species = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Breeds", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FullName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Address = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Species = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Specializations", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Pets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Species = table.Column<int>(type: "int", nullable: false),
                    BreedId = table.Column<int>(type: "int", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: true),
                    BodyWeight = table.Column<double>(type: "double", nullable: true),
                    OwnerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pets_Breeds_BreedId",
                        column: x => x.BreedId,
                        principalTable: "Breeds",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pets_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Veterinarians",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PassportNumber = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    FullName = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SpecializationId = table.Column<int>(type: "int", nullable: false),
                    BirthYear = table.Column<int>(type: "int", nullable: false),
                    WorkExperience = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Veterinarians", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Veterinarians_Specializations_SpecializationId",
                        column: x => x.SpecializationId,
                        principalTable: "Specializations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PetId = table.Column<int>(type: "int", nullable: false),
                    VeterinarianId = table.Column<int>(type: "int", nullable: false),
                    AppointmentDateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    RoomNumber = table.Column<int>(type: "int", nullable: false),
                    IsRepeat = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_Pets_PetId",
                        column: x => x.PetId,
                        principalTable: "Pets",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Appointments_Veterinarians_VeterinarianId",
                        column: x => x.VeterinarianId,
                        principalTable: "Veterinarians",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Breeds",
                columns: new[] { "Id", "Name", "Species" },
                values: new object[,]
                {
                    { 1, "Лабрадор", 2 },
                    { 2, "Немецкая овчарка", 2 },
                    { 3, "Британская короткошёрстная", 1 },
                    { 4, "Мейн-кун", 1 },
                    { 5, "Волнистый попугай", 3 },
                    { 6, "Корелла", 3 },
                    { 7, "Сирийский хомяк", 4 },
                    { 8, "Джунгарский хомяк", 4 },
                    { 9, "Белая мышь", 5 },
                    { 10, "Кролик баран", 6 },
                    { 11, "Красноухая черепаха", 7 },
                    { 12, "Среднеазиатская черепаха", 7 }
                });

            migrationBuilder.InsertData(
                table: "Owners",
                columns: new[] { "Id", "Address", "FullName", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, null, "Иванов Иван Иванович", "+7-900-000-00-01" },
                    { 2, null, "Петрова Мария Сергеевна", "+7-900-000-00-02" },
                    { 3, null, "Сидоров Алексей Владимирович", "+7-900-000-00-03" },
                    { 4, null, "Смирнова Анна Дмитриевна", "+7-900-000-00-04" },
                    { 5, null, "Кузнецова Ольга Андреевна", "+7-900-000-00-05" },
                    { 6, null, "Алексеев Андрей Алексеевич", "+7-900-000-00-06" },
                    { 7, null, "Михайлова Мария Ивановна", "+7-900-000-00-07" },
                    { 8, null, "Фёдоров Фёдор Фёдорович", "+7-900-000-00-08" },
                    { 9, null, "Николаев Николай Николаевич", "+7-900-000-00-09" },
                    { 10, null, "Захаров Захар Захарович", "+7-900-000-00-10" }
                });

            migrationBuilder.InsertData(
                table: "Specializations",
                columns: new[] { "Id", "Name", "Species" },
                values: new object[,]
                {
                    { 1, "Терапевт", "[1,2]" },
                    { 2, "Хирург", "[2]" },
                    { 3, "Орнитолог", "[3]" },
                    { 4, "Универсал", "[1,2,3,4,5,6,7]" },
                    { 5, "Фелинолог", "[1]" },
                    { 6, "Герпетолог", "[7]" },
                    { 7, "Ратолог", "[4,5,6]" },
                    { 8, "Офтальмолог", "[1,2]" },
                    { 9, "Стоматолог", "[1,2]" },
                    { 10, "Дерматолог", "[1,2]" },
                    { 11, "Кардиолог", "[1,2]" },
                    { 12, "Невролог", "[1,2]" }
                });

            migrationBuilder.InsertData(
                table: "Pets",
                columns: new[] { "Id", "BirthDate", "BodyWeight", "BreedId", "Name", "OwnerId", "Species" },
                values: new object[,]
                {
                    { 1, null, null, 1, "Рекс", 1, 2 },
                    { 2, null, null, 3, "Барсик", 1, 1 },
                    { 3, null, null, 2, "Белла", 3, 2 },
                    { 4, null, null, 4, "Мурка", 3, 1 },
                    { 5, null, null, 5, "Кеша", 2, 3 },
                    { 6, null, null, 3, "Тиша", 3, 1 },
                    { 7, null, null, 1, "Шарик", 5, 2 },
                    { 8, null, null, 4, "Пушок", 5, 1 },
                    { 9, null, null, 6, "Гоша", 6, 3 },
                    { 10, null, null, 3, "Муся", 7, 1 },
                    { 11, null, null, 7, "Хома", 4, 4 },
                    { 12, null, null, 10, "Снежок", 8, 6 }
                });

            migrationBuilder.InsertData(
                table: "Veterinarians",
                columns: new[] { "Id", "BirthYear", "FullName", "PassportNumber", "SpecializationId", "WorkExperience" },
                values: new object[,]
                {
                    { 1, 1980, "Иванов Иван Иванович", "1001 000001", 1, 15 },
                    { 2, 1975, "Петров Пётр Петрович", "1002 000002", 2, 20 },
                    { 3, 1985, "Сидоров Сидор Сидорович", "1003 000003", 3, 10 },
                    { 4, 1990, "Универсалов Универсал Универсалович", "1004 000004", 4, 8 },
                    { 5, 1988, "Кузнецов Кузьма Кузьмич", "1005 000005", 1, 12 },
                    { 6, 1982, "Соколов Степан Степанович", "1006 000006", 2, 14 },
                    { 7, 1978, "Попов Павел Павлович", "1007 000007", 3, 18 },
                    { 8, 1986, "Лебедев Лев Львович", "1008 000008", 5, 9 },
                    { 9, 1992, "Козлов Кирилл Кириллович", "1009 000009", 4, 5 },
                    { 10, 1984, "Новиков Никита Никитич", "1010 000010", 6, 11 }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDateTime", "IsRepeat", "PetId", "RoomNumber", "VeterinarianId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 10, 15, 13, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 101, 1 },
                    { 2, new DateTime(2026, 10, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 102, 1 },
                    { 3, new DateTime(2026, 10, 15, 15, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 101, 1 },
                    { 4, new DateTime(2026, 10, 15, 16, 0, 0, 0, DateTimeKind.Unspecified), false, 2, 101, 1 },
                    { 5, new DateTime(2026, 9, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), true, 3, 101, 2 },
                    { 6, new DateTime(2026, 10, 15, 17, 0, 0, 0, DateTimeKind.Unspecified), false, 4, 102, 5 },
                    { 7, new DateTime(2026, 10, 15, 18, 0, 0, 0, DateTimeKind.Unspecified), true, 6, 101, 8 },
                    { 8, new DateTime(2026, 11, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), false, 8, 101, 4 },
                    { 9, new DateTime(2026, 10, 15, 19, 0, 0, 0, DateTimeKind.Unspecified), true, 7, 101, 6 },
                    { 10, new DateTime(2026, 10, 15, 20, 0, 0, 0, DateTimeKind.Unspecified), true, 5, 102, 3 },
                    { 11, new DateTime(2026, 10, 15, 21, 0, 0, 0, DateTimeKind.Unspecified), false, 9, 101, 7 },
                    { 12, new DateTime(2026, 10, 15, 22, 0, 0, 0, DateTimeKind.Unspecified), false, 10, 101, 5 },
                    { 13, new DateTime(2026, 10, 15, 23, 0, 0, 0, DateTimeKind.Unspecified), false, 3, 101, 2 },
                    { 14, new DateTime(2026, 9, 15, 12, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 102, 2 },
                    { 15, new DateTime(2026, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 4, 102, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_PetId",
                table: "Appointments",
                column: "PetId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_VeterinarianId",
                table: "Appointments",
                column: "VeterinarianId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_BreedId",
                table: "Pets",
                column: "BreedId");

            migrationBuilder.CreateIndex(
                name: "IX_Pets_OwnerId",
                table: "Pets",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Veterinarians_SpecializationId",
                table: "Veterinarians",
                column: "SpecializationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Pets");

            migrationBuilder.DropTable(
                name: "Veterinarians");

            migrationBuilder.DropTable(
                name: "Breeds");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Specializations");
        }
    }
}
