using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VeterinaryClinic.Infrastructure.PostgreSQL.Migrations;

/// <inheritdoc />
public partial class InitialSchema : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "breeds",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:IdentitySequenceOptions", "'1000', '1', '', '', 'False', '1'")
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "text", nullable: false),
                animal_type = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_breeds", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "owners",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:IdentitySequenceOptions", "'1000', '1', '', '', 'False', '1'")
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                address = table.Column<string>(type: "text", nullable: false),
                last_name = table.Column<string>(type: "text", nullable: false),
                first_name = table.Column<string>(type: "text", nullable: false),
                patronymic = table.Column<string>(type: "text", nullable: true),
                phone = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_owners", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "specializations",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:IdentitySequenceOptions", "'1000', '1', '', '', 'False', '1'")
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "text", nullable: false),
                animal_type = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_specializations", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "pets",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:IdentitySequenceOptions", "'1000', '1', '', '', 'False', '1'")
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "text", nullable: false),
                breed_id = table.Column<int>(type: "integer", nullable: false),
                birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                weight = table.Column<double>(type: "double precision", nullable: false),
                owner_id = table.Column<int>(type: "integer", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_pets", x => x.id);
                table.ForeignKey(
                    name: "FK_pets_breeds_breed_id",
                    column: x => x.breed_id,
                    principalTable: "breeds",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_pets_owners_owner_id",
                    column: x => x.owner_id,
                    principalTable: "owners",
                    principalColumn: "id",
                    onDelete: ReferentialAction.SetNull);
            });

        migrationBuilder.CreateTable(
            name: "veterinarians",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:IdentitySequenceOptions", "'1000', '1', '', '', 'False', '1'")
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                passport_number = table.Column<string>(type: "text", nullable: false),
                birth_date = table.Column<DateOnly>(type: "date", nullable: false),
                specialization_id = table.Column<int>(type: "integer", nullable: false),
                experience_years = table.Column<int>(type: "integer", nullable: false),
                last_name = table.Column<string>(type: "text", nullable: false),
                first_name = table.Column<string>(type: "text", nullable: false),
                patronymic = table.Column<string>(type: "text", nullable: true),
                phone = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_veterinarians", x => x.id);
                table.ForeignKey(
                    name: "FK_veterinarians_specializations_specialization_id",
                    column: x => x.specialization_id,
                    principalTable: "specializations",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "appointments",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:IdentitySequenceOptions", "'1000', '1', '', '', 'False', '1'")
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                pet_id = table.Column<int>(type: "integer", nullable: false),
                veterinarian_id = table.Column<int>(type: "integer", nullable: false),
                appointment_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                room_number = table.Column<int>(type: "integer", nullable: false),
                is_repeated = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_appointments", x => x.id);
                table.ForeignKey(
                    name: "FK_appointments_pets_pet_id",
                    column: x => x.pet_id,
                    principalTable: "pets",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_appointments_veterinarians_veterinarian_id",
                    column: x => x.veterinarian_id,
                    principalTable: "veterinarians",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.InsertData(
            table: "breeds",
            columns: new[] { "id", "animal_type", "name" },
            values: new object[,]
            {
                { 1, "Dog", "Лабрадор" },
                { 2, "Dog", "Немецкая овчарка" },
                { 3, "Cat", "Сиамская" },
                { 4, "Cat", "Британская короткошёрстная" },
                { 5, "Bird", "Корелла" },
                { 6, "Bird", "Волнистый попугай" },
                { 7, "Rabbit", "Карликовый кролик" },
                { 8, "Rabbit", "Вислоухий кролик" },
                { 9, "Reptile", "Королевский питон" },
                { 10, "Reptile", "Красноухая черепаха" },
                { 11, "Rodent", "Сирийский хомяк" }
            });

        migrationBuilder.InsertData(
            table: "owners",
            columns: new[] { "id", "address", "first_name", "last_name", "patronymic", "phone" },
            values: new object[,]
            {
                { 1, "ул. Лесная, 12", "Антон", "Алексеев", "Игоревич", "+79990000001" },
                { 2, "ул. Центральная, 8", "Мария", "Беляева", "Сергеевна", "+79990000002" },
                { 3, "ул. Садовая, 15", "Дмитрий", "Васильев", "Олегович", "+79990000003" },
                { 4, "ул. Полевая, 21", "Елена", "Громова", "Андреевна", "+79990000004" },
                { 5, "ул. Молодёжная, 4", "Илья", "Дорофеев", "Павлович", "+79990000005" },
                { 6, "ул. Школьная, 19", "Ольга", "Ершова", "Викторовна", "+79990000006" },
                { 7, "ул. Новая, 33", "Максим", "Жуков", "Романович", "+79990000007" },
                { 8, "ул. Парковая, 2", "Ирина", "Захарова", "Денисовна", "+79990000008" },
                { 9, "ул. Заречная, 7", "Пётр", "Иванов", "Алексеевич", "+79990000009" },
                { 10, "ул. Речная, 10", "Наталья", "Крылова", "Михайловна", "+79990000010" }
            });

        migrationBuilder.InsertData(
            table: "specializations",
            columns: new[] { "id", "animal_type", "name" },
            values: new object[,]
            {
                { 1, "Dog", "Терапевт" },
                { 2, "Cat", "Хирург" },
                { 3, "Bird", "Орнитолог" },
                { 4, "Rabbit", "Терапевт" },
                { 5, "Reptile", "Герпетолог" },
                { 6, "Rodent", "Ратолог" },
                { 7, "Rodent", "Экзотолог" },
                { 8, "Dog", "Кардиолог" },
                { 9, "Cat", "Стоматолог" },
                { 10, "Rabbit", "Дерматолог" }
            });

        migrationBuilder.InsertData(
            table: "pets",
            columns: new[] { "id", "birth_date", "breed_id", "name", "owner_id", "weight" },
            values: new object[,]
            {
                { 1, new DateOnly(2020, 3, 12), 1, "Бим", 1, 28.5 },
                { 2, new DateOnly(2019, 7, 5), 2, "Альма", 1, 31.199999999999999 },
                { 3, new DateOnly(2021, 1, 18), 3, "Барсик", 2, 4.7000000000000002 },
                { 4, new DateOnly(2022, 6, 23), 4, "Луна", 2, 5.0999999999999996 },
                { 5, new DateOnly(2022, 2, 10), 5, "Кеша", 3, 0.089999999999999997 },
                { 6, new DateOnly(2023, 4, 2), 6, "Ричи", 4, 0.040000000000000001 },
                { 7, new DateOnly(2021, 11, 14), 7, "Снежок", 5, 1.8 },
                { 8, new DateOnly(2020, 9, 9), 8, "Пушок", 6, 2.2000000000000002 },
                { 9, new DateOnly(2018, 5, 30), 9, "Кобра", 7, 1.6000000000000001 },
                { 10, new DateOnly(2017, 8, 17), 10, "Тортила", 8, 0.80000000000000004 },
                { 11, new DateOnly(2022, 4, 1), 1, "Арчи", 9, 24.300000000000001 },
                { 12, new DateOnly(2021, 12, 20), 1, "Джек", 10, 26.100000000000001 },
                { 13, new DateOnly(2020, 10, 11), 2, "Грей", 9, 29.399999999999999 },
                { 14, new DateOnly(2023, 3, 7), 3, "Марта", 10, 3.8999999999999999 },
                { 15, new DateOnly(2019, 12, 28), 4, "Оскар", 4, 6.0 },
                { 16, new DateOnly(2022, 8, 6), 1, "Тиша", 1, 22.699999999999999 },
                { 17, new DateOnly(2023, 1, 25), 7, "Нора", 5, 1.5 },
                { 18, new DateOnly(2021, 5, 19), 6, "Яша", 6, 0.050000000000000003 },
                { 19, new DateOnly(2020, 7, 21), 10, "Феня", 7, 0.90000000000000002 },
                { 20, new DateOnly(2019, 2, 3), 2, "Рокки", 3, 32.0 },
                { 21, new DateOnly(2024, 2, 14), 11, "Пуфик", 7, 0.12 }
            });

        migrationBuilder.InsertData(
            table: "veterinarians",
            columns: new[] { "id", "birth_date", "experience_years", "first_name", "last_name", "passport_number", "patronymic", "phone", "specialization_id" },
            values: new object[,]
            {
                { 1, new DateOnly(1985, 4, 12), 12, "Иван", "Андреев", "4010 100001", "Сергеевич", "+7999111001", 1 },
                { 2, new DateOnly(1988, 9, 23), 10, "Анна", "Борисова", "4010 100002", "Игоревна", "+7999111002", 2 },
                { 3, new DateOnly(1982, 2, 17), 15, "Дмитрий", "Волков", "4010 100003", "Алексеевич", "+7999111003", 3 },
                { 4, new DateOnly(1990, 11, 5), 8, "Ольга", "Гаврилова", "4010 100004", "Петровна", "+7999111004", 4 },
                { 5, new DateOnly(1984, 6, 30), 13, "Максим", "Данилов", "4010 100005", "Романович", "+7999111005", 5 },
                { 6, new DateOnly(1987, 3, 8), 11, "Светлана", "Ефремова", "4010 100006", "Владимировна", "+7999111006", 1 },
                { 7, new DateOnly(1991, 12, 14), 7, "Алексей", "Жданов", "4010 100007", "Олегович", "+7999111007", 2 },
                { 8, new DateOnly(1986, 7, 21), 12, "Марина", "Зимина", "4010 100008", "Андреевна", "+7999111008", 1 },
                { 9, new DateOnly(1989, 5, 19), 9, "Роман", "Ильин", "4010 100009", "Викторович", "+7999111009", 3 },
                { 10, new DateOnly(1992, 8, 3), 6, "Екатерина", "Ковалёва", "4010 100010", "Денисовна", "+7999111010", 6 },
                { 11, new DateOnly(1993, 10, 27), 5, "Наталья", "Лебедева", "4010 100011", "Ильинична", "+7999111011", 7 }
            });

        migrationBuilder.InsertData(
            table: "appointments",
            columns: new[] { "id", "appointment_date", "is_repeated", "pet_id", "room_number", "veterinarian_id" },
            values: new object[,]
            {
                { 1, new DateTime(2026, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 1, 101, 1 },
                { 2, new DateTime(2026, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 2, 101, 1 },
                { 3, new DateTime(2026, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 11, 102, 1 },
                { 4, new DateTime(2026, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 12, 103, 6 },
                { 5, new DateTime(2026, 11, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 16, 101, 8 },
                { 6, new DateTime(2026, 11, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 104, 1 },
                { 7, new DateTime(2026, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 3, 201, 2 },
                { 8, new DateTime(2026, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 4, 201, 2 },
                { 9, new DateTime(2026, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 5, 301, 3 },
                { 10, new DateTime(2026, 10, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 6, 301, 3 },
                { 11, new DateTime(2026, 10, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 7, 401, 4 },
                { 12, new DateTime(2026, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 8, 401, 4 },
                { 13, new DateTime(2026, 10, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 9, 501, 5 },
                { 14, new DateTime(2026, 10, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 10, 501, 5 },
                { 15, new DateTime(2026, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 13, 103, 6 },
                { 16, new DateTime(2026, 10, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 14, 201, 7 },
                { 17, new DateTime(2026, 10, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 15, 201, 7 },
                { 18, new DateTime(2026, 10, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 17, 401, 4 },
                { 19, new DateTime(2026, 10, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 18, 301, 9 },
                { 20, new DateTime(2026, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 19, 501, 5 },
                { 21, new DateTime(2026, 10, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 20, 103, 6 },
                { 22, new DateTime(2026, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 1, 101, 1 },
                { 23, new DateTime(2026, 12, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 2, 101, 1 },
                { 24, new DateTime(2026, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), true, 11, 102, 1 }
            });

        migrationBuilder.CreateIndex(
            name: "IX_appointments_appointment_date_room_number",
            table: "appointments",
            columns: new[] { "appointment_date", "room_number" },
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_appointments_pet_id",
            table: "appointments",
            column: "pet_id");

        migrationBuilder.CreateIndex(
            name: "IX_appointments_veterinarian_id",
            table: "appointments",
            column: "veterinarian_id");

        migrationBuilder.CreateIndex(
            name: "IX_pets_breed_id",
            table: "pets",
            column: "breed_id");

        migrationBuilder.CreateIndex(
            name: "IX_pets_owner_id",
            table: "pets",
            column: "owner_id");

        migrationBuilder.CreateIndex(
            name: "IX_veterinarians_specialization_id",
            table: "veterinarians",
            column: "specialization_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "appointments");

        migrationBuilder.DropTable(
            name: "pets");

        migrationBuilder.DropTable(
            name: "veterinarians");

        migrationBuilder.DropTable(
            name: "breeds");

        migrationBuilder.DropTable(
            name: "owners");

        migrationBuilder.DropTable(
            name: "specializations");
    }
}
