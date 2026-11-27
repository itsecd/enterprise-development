using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VetClinic.Infrastructure.PostgreSql.Migrations;

/// <inheritdoc />
public partial class SeedClinicData : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        // Значения видов животных зафиксированы на момент создания миграции.
        migrationBuilder.InsertData(
            table: "Breeds",
            columns: ["Id", "Name", "Species"],
            values: new object[,]
            {
                { 1, "Лабрадор ретривер", 2 },
                { 2, "Немецкая овчарка", 2 },
                { 3, "Французский бульдог", 2 },
                { 4, "Мейн-кун", 1 },
                { 5, "Британская короткошерстная", 1 },
                { 6, "Сфинкс", 1 },
                { 7, "Волнистый попугай", 3 },
                { 8, "Корелла", 3 },
                { 9, "Джунгарский хомяк", 4 },
                { 10, "Морская свинка", 4 },
                { 11, "Бородатая агама", 5 },
                { 12, "Йеменский хамелеон", 5 },
            });

        migrationBuilder.InsertData(
            table: "Specializations",
            columns: ["Id", "Name", "TargetSpecies"],
            values: new object[,]
            {
                { 1, "Терапия кошек", 1 },
                { 2, "Хирургия кошек", 1 },
                { 3, "Дерматология кошек", 1 },
                { 4, "Терапия собак", 2 },
                { 5, "Хирургия собак", 2 },
                { 6, "Орнитология", 3 },
                { 7, "Хирургия птиц", 3 },
                { 8, "Ратология", 4 },
                { 9, "Терапия грызунов", 4 },
                { 10, "Герпетология", 5 },
            });

        migrationBuilder.InsertData(
            table: "Owners",
            columns: ["Id", "FullName", "Phone", "Address"],
            values: new object[,]
            {
                { 1, "Иванов Иван Иванович", "+7-900-111-22-33", "ул. Ленина, д. 10" },
                { 2, "Петров Петр Петрович", "+7-900-222-33-44", null },
                { 3, "Сидоров Сидор Сидорович", "+7-900-333-44-55", "ул. Мира, д. 5" },
                { 4, "Алексеев Алексей Алексеевич", "+7-900-444-55-66", null },
                { 5, "Васильев Василий Васильевич", "+7-900-555-66-77", "ул. Гагарина, д. 1" },
                { 6, "Дмитриев Дмитрий Дмитриевич", "+7-900-666-77-88", "ул. Кирова, д. 12" },
                { 7, "Елисеев Елисей Елисеевич", "+7-900-777-88-99", null },
                { 8, "Федоров Федор Федорович", "+7-900-888-99-00", "пр. Победы, д. 20" },
                { 9, "Григорьев Григорий Григорьевич", "+7-900-999-00-11", null },
                { 10, "Николаев Николай Николаевич", "+7-900-000-11-22", "ул. Чехова, д. 8" },
            });

        migrationBuilder.InsertData(
            table: "Pets",
            columns: ["Id", "Name", "Species", "BreedId", "BirthDate", "WeightKg", "OwnerId"],
            values: new object[,]
            {
                { 1, "Шарик", 2, 1, new DateOnly(2020, 1, 15), 28.5m, 1 },
                { 2, "Барсик", 1, 4, new DateOnly(2021, 4, 10), 6.8m, 1 },
                { 3, "Рекс", 2, 2, new DateOnly(2019, 7, 20), 34.0m, 2 },
                { 4, "Мурка", 1, 5, new DateOnly(2022, 3, 5), 4.2m, 2 },
                { 5, "Кеша", 3, 7, new DateOnly(2023, 2, 1), 0.04m, 3 },
                { 6, "Хома", 4, 9, new DateOnly(2023, 10, 15), 0.05m, 4 },
                { 7, "Горыныч", 5, 11, new DateOnly(2021, 11, 25), 0.45m, 5 },
                { 8, "Белла", 2, 3, new DateOnly(2022, 5, 12), 11.5m, 6 },
                { 9, "Симба", 1, 6, new DateOnly(2020, 8, 30), 3.9m, 7 },
                { 10, "Карлуша", 3, 8, new DateOnly(2022, 1, 18), 0.09m, 8 },
            });

        migrationBuilder.InsertData(
            table: "Veterinarians",
            columns: ["Id", "PassportNumber", "FullName", "BirthYear", "SpecializationId", "ExperienceYears"],
            values: new object[,]
            {
                { 1, "4501 111111", "Смирнов Андрей Викторович", 1980, 1, 18 },
                { 2, "4502 222222", "Кузнецова Анна Павловна", 1985, 2, 12 },
                { 3, "4503 333333", "Морозов Игорь Сергеевич", 1990, 3, 7 },
                { 4, "4504 444444", "Попова Елена Дмитриевна", 1978, 4, 21 },
                { 5, "4505 555555", "Лебедев Максим Олегович", 1988, 5, 9 },
                { 6, "4506 666666", "Новикова Ольга Игоревна", 1992, 6, 5 },
                { 7, "4507 777777", "Соколов Артем Владимирович", 1983, 7, 15 },
                { 8, "4508 888888", "Козлов Денис Юрьевич", 1995, 8, 3 },
                { 9, "4509 999999", "Павлова Марина Сергеевна", 1987, 9, 11 },
                { 10, "4510 000000", "Семенов Кирилл Алексеевич", 1982, 10, 16 },
            });

        migrationBuilder.InsertData(
            table: "Appointments",
            columns: ["Id", "DateTime", "RoomNumber", "IsFollowUp", "PetId", "VeterinarianId"],
            values: new object[,]
            {
                { 1, new DateTime(2026, 9, 10, 10, 0, 0), 101, false, 1, 4 },
                { 2, new DateTime(2026, 9, 15, 11, 0, 0), 101, true, 1, 4 },
                { 3, new DateTime(2026, 9, 20, 14, 0, 0), 101, false, 3, 4 },
                { 4, new DateTime(2026, 8, 25, 12, 0, 0), 101, false, 1, 4 },
                { 5, new DateTime(2026, 9, 12, 9, 30, 0), 102, true, 2, 1 },
                { 6, new DateTime(2026, 9, 18, 15, 0, 0), 102, false, 4, 2 },
                { 7, new DateTime(2026, 9, 5, 10, 0, 0), 103, false, 5, 6 },
                { 8, new DateTime(2026, 9, 14, 16, 0, 0), 103, true, 1, 4 },
                { 9, new DateTime(2026, 9, 22, 11, 30, 0), 104, false, 6, 8 },
                { 10, new DateTime(2026, 9, 28, 13, 0, 0), 105, false, 7, 10 },
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        object[,] commonIds = { { 1 }, { 2 }, { 3 }, { 4 }, { 5 }, { 6 }, { 7 }, { 8 }, { 9 }, { 10 } };

        migrationBuilder.DeleteData("Appointments", ["Id"], commonIds);
        migrationBuilder.DeleteData("Veterinarians", ["Id"], commonIds);
        migrationBuilder.DeleteData("Pets", ["Id"], commonIds);
        migrationBuilder.DeleteData("Owners", ["Id"], commonIds);
        migrationBuilder.DeleteData("Specializations", ["Id"], commonIds);
        migrationBuilder.DeleteData("Breeds", ["Id"],
            new object[,] { { 1 }, { 2 }, { 3 }, { 4 }, { 5 }, { 6 }, { 7 }, { 8 }, { 9 }, { 10 }, { 11 }, { 12 } });
    }
}
