using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessGym.Infrastructure.PostgreSql.Migrations;

/// <summary>Фиксированный снимок начальных данных на 8 ноября 2026 года.</summary>
public partial class SeedInitialData : Migration
{
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.InsertData(
            table: "Specializations",
            columns: ["Id", "Name"],
            values: new object[,]
            {
                { 0, "Йога" },
                { 1, "Пилатес" },
                { 2, "Кроссфит" },
                { 3, "Плавание" },
                { 4, "Бокс" },
                { 5, "Тяжёлая атлетика" },
                { 6, "Стретчинг" },
                { 7, "Функциональный тренинг" },
                { 8, "Лёгкая атлетика" },
                { 9, "Реабилитация" },
            });

        migrationBuilder.InsertData(
            table: "Clients",
            columns: ["Id", "PassportNumber", "FullName", "Gender", "BirthDate", "Phone", "SubscriptionStart", "SubscriptionEnd"],
            values: new object[,]
            {
                { 0, "4501 123456", "Андреев Алексей Сергеевич", 0, new DateOnly(1990, 4, 12), "+7 (911) 100-00-01", new DateOnly(2026, 9, 9), new DateOnly(2026, 10, 9) },
                { 1, "4502 234567", "Борисова Марина Владимировна", 1, new DateOnly(1985, 11, 25), "+7 (911) 100-00-02", new DateOnly(2026, 8, 10), new DateOnly(2026, 11, 1) },
                { 2, "4503 345678", "Васильев Дмитрий Олегович", 0, new DateOnly(1978, 2, 18), "+7 (911) 100-00-03", new DateOnly(2026, 7, 11), new DateOnly(2026, 11, 7) },
                { 3, "4504 456789", "Гаврилова Елена Николаевна", 1, new DateOnly(1992, 6, 30), "+7 (911) 100-00-04", new DateOnly(2025, 8, 20), new DateOnly(2026, 9, 24) },
                { 4, "4505 567890", "Дмитриев Павел Андреевич", 0, new DateOnly(1996, 9, 5), "+7 (911) 100-00-05", new DateOnly(2026, 8, 26), new DateOnly(2026, 10, 25) },
                { 5, "4506 678901", "Егорова Ольга Ивановна", 1, new DateOnly(1983, 12, 1), "+7 (911) 100-00-06", new DateOnly(2026, 8, 10), new DateOnly(2027, 2, 6) },
                { 6, "4507 789012", "Жуков Игорь Валерьевич", 0, new DateOnly(1994, 5, 22), "+7 (911) 100-00-07", new DateOnly(2026, 10, 9), new DateOnly(2026, 12, 8) },
                { 7, "4508 890123", "Зотова Анна Викторовна", 1, new DateOnly(1999, 8, 17), "+7 (911) 100-00-08", new DateOnly(2026, 9, 9), new DateOnly(2026, 11, 23) },
                { 8, "4509 901234", "Иванов Сергей Петрович", 0, new DateOnly(1975, 1, 9), "+7 (911) 100-00-09", new DateOnly(2026, 10, 29), new DateOnly(2027, 1, 27) },
                { 9, "4510 012345", "Козлова Мария Дмитриевна", 1, new DateOnly(2001, 3, 28), "+7 (911) 100-00-10", new DateOnly(2026, 11, 7), new DateOnly(2027, 11, 7) },
                { 10, "4511 111111", "Лебедев Артём Игоревич", 0, new DateOnly(2003, 7, 7), "+7 (911) 100-00-11", new DateOnly(2026, 11, 15), new DateOnly(2027, 2, 13) },
                { 11, "4512 222222", "Морозова Татьяна Львовна", 1, new DateOnly(1988, 10, 10), "+7 (911) 100-00-12", new DateOnly(2026, 11, 22), new DateOnly(2027, 2, 20) },
            });

        migrationBuilder.InsertData(
            table: "Trainers",
            columns: ["Id", "PassportNumber", "FullName", "Gender", "BirthDate", "SpecializationId", "ExperienceYears"],
            values: new object[,]
            {
                { 0, "4601 123456", "Соколов Виктор Александрович", 0, new DateOnly(1990, 6, 15), 2, 3 },
                { 1, "4602 234567", "Кузнецова Наталья Сергеевна", 1, new DateOnly(1988, 3, 24), 1, 5 },
                { 2, "4603 345678", "Попов Максим Игоревич", 0, new DateOnly(1998, 11, 2), 4, 2 },
                { 3, "4604 456789", "Лебедева Дарья Александровна", 1, new DateOnly(1985, 7, 19), 0, 8 },
                { 4, "4605 567890", "Новиков Артур Валерьевич", 0, new DateOnly(1980, 1, 30), 5, 12 },
                { 5, "4606 678901", "Фёдорова Ксения Павловна", 1, new DateOnly(1995, 9, 8), 6, 4 },
                { 6, "4607 789012", "Морозов Георгий Львович", 0, new DateOnly(1982, 12, 11), 3, 10 },
                { 7, "4608 890123", "Волкова Ирина Дмитриевна", 1, new DateOnly(1987, 5, 27), 7, 7 },
                { 8, "4609 901234", "Зайцев Роман Олегович", 0, new DateOnly(2000, 2, 14), 8, 1 },
                { 9, "4610 012345", "Павлова Светлана Юрьевна", 1, new DateOnly(1991, 8, 3), 0, 6 },
                { 10, "4611 111111", "Семёнов Кирилл Андреевич", 0, new DateOnly(1979, 4, 21), 2, 15 },
                { 11, "4612 222222", "Голубева Вероника Игоревна", 1, new DateOnly(1993, 10, 16), 9, 9 },
            });

        migrationBuilder.InsertData(
            table: "TrainingSessions",
            columns: ["Id", "ClientId", "TrainerId", "StartsAt", "EndsAt", "HallName", "IsTrial"],
            values: new object[,]
            {
                { 0, 5, 7, new DateTime(2026, 11, 5, 10, 0, 0, DateTimeKind.Unspecified), null, "Силовой зал", false },
                { 1, 6, 4, new DateTime(2026, 11, 12, 12, 0, 0, DateTimeKind.Unspecified), null, "Силовой зал", false },
                { 2, 7, 10, new DateTime(2026, 11, 20, 18, 0, 0, DateTimeKind.Unspecified), null, "Силовой зал", true },
                { 3, 5, 7, new DateTime(2026, 10, 5, 10, 0, 0, DateTimeKind.Unspecified), null, "Силовой зал", false },
                { 4, 5, 0, new DateTime(2026, 12, 5, 10, 0, 0, DateTimeKind.Unspecified), null, "Силовой зал", false },
                { 5, 9, 7, new DateTime(2026, 11, 8, 11, 30, 0, DateTimeKind.Unspecified), new DateTime(2026, 11, 8, 12, 30, 0, DateTimeKind.Unspecified), "Зал групповых занятий", false },
                { 6, 0, 6, new DateTime(2026, 11, 8, 10, 50, 0, DateTimeKind.Unspecified), new DateTime(2026, 11, 8, 11, 50, 0, DateTimeKind.Unspecified), "Зал единоборств", false },
                { 7, 6, 7, new DateTime(2026, 11, 8, 10, 0, 0, DateTimeKind.Unspecified), null, "Бассейн", false },
                { 8, 9, 4, new DateTime(2026, 11, 14, 19, 0, 0, DateTimeKind.Unspecified), null, "Зал единоборств", false },
                { 9, 8, 10, new DateTime(2026, 10, 15, 8, 0, 0, DateTimeKind.Unspecified), null, "Бассейн", false },
                { 10, 5, 1, new DateTime(2026, 11, 22, 9, 0, 0, DateTimeKind.Unspecified), null, "Зал групповых занятий", true },
                { 11, 6, 7, new DateTime(2026, 10, 18, 17, 0, 0, DateTimeKind.Unspecified), null, "Силовой зал", false },
                { 12, 7, 4, new DateTime(2026, 12, 10, 11, 0, 0, DateTimeKind.Unspecified), null, "Кардио-зал", true },
                { 13, 11, 10, new DateTime(2026, 12, 3, 7, 0, 0, DateTimeKind.Unspecified), null, "Бассейн", true },
                { 14, 1, 1, new DateTime(2026, 10, 22, 20, 0, 0, DateTimeKind.Unspecified), null, "Зал единоборств", false },
                { 15, 2, 6, new DateTime(2026, 10, 28, 18, 0, 0, DateTimeKind.Unspecified), null, "Зал групповых занятий", false },
                { 16, 8, 7, new DateTime(2026, 11, 25, 8, 0, 0, DateTimeKind.Unspecified), null, "Бассейн", false },
                { 17, 0, 4, new DateTime(2026, 10, 10, 19, 0, 0, DateTimeKind.Unspecified), null, "Кардио-зал", false },
                { 18, 5, 10, new DateTime(2026, 11, 9, 21, 0, 0, DateTimeKind.Unspecified), null, "Зал единоборств", false },
                { 19, 9, 1, new DateTime(2026, 11, 16, 7, 0, 0, DateTimeKind.Unspecified), null, "Кардио-зал", false },
                { 20, 7, 8, new DateTime(2026, 10, 3, 18, 0, 0, DateTimeKind.Unspecified), null, "Бассейн", true },
                { 21, 6, 4, new DateTime(2026, 12, 14, 18, 0, 0, DateTimeKind.Unspecified), null, "Зал единоборств", false },
                { 22, 1, 2, new DateTime(2026, 10, 8, 8, 0, 0, DateTimeKind.Unspecified), null, "Кардио-зал", true },
                { 23, 8, 9, new DateTime(2026, 11, 17, 20, 0, 0, DateTimeKind.Unspecified), null, "Бассейн", false },
                { 24, 9, 3, new DateTime(2026, 12, 21, 10, 0, 0, DateTimeKind.Unspecified), null, "Зал групповых занятий", true },
                { 25, 5, 11, new DateTime(2026, 11, 27, 8, 0, 0, DateTimeKind.Unspecified), null, "Кардио-зал", false },
            });
    }

    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DeleteData(
            table: "TrainingSessions",
            keyColumn: "Id",
            keyValues: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25]);

        migrationBuilder.DeleteData(
            table: "Trainers",
            keyColumn: "Id",
            keyValues: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]);

        migrationBuilder.DeleteData(
            table: "Clients",
            keyColumn: "Id",
            keyValues: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11]);

        migrationBuilder.DeleteData(
            table: "Specializations",
            keyColumn: "Id",
            keyValues: [0, 1, 2, 3, 4, 5, 6, 7, 8, 9]);
    }
}
