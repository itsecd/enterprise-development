using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace VetClinic.EfCore.Migrations;

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
                Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Type = table.Column<int>(type: "int", nullable: false)
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
                FullName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Phone = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Adress = table.Column<string>(type: "varchar(250)", maxLength: 250, nullable: false)
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
                Title = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                TargetSpecies = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
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
                Name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Type = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                BreedId = table.Column<int>(type: "int", nullable: false),
                Weight = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
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
            name: "Services",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                Title = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Price = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                SpecializationId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Services", x => x.Id);
                table.ForeignKey(
                    name: "FK_Services_Specializations_SpecializationId",
                    column: x => x.SpecializationId,
                    principalTable: "Specializations",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Vets",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                FullName = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                PassportNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                ExpirienceYears = table.Column<int>(type: "int", nullable: false),
                SpecializationId = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Vets", x => x.Id);
                table.ForeignKey(
                    name: "FK_Vets_Specializations_SpecializationId",
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
                DateTime = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                Diagnosis = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                PetId = table.Column<int>(type: "int", nullable: false),
                VetId = table.Column<int>(type: "int", nullable: false),
                RoomNumber = table.Column<int>(type: "int", nullable: false),
                ServiceId = table.Column<int>(type: "int", nullable: false),
                RepeatVisitIndicator = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4")
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
                    name: "FK_Appointments_Services_ServiceId",
                    column: x => x.ServiceId,
                    principalTable: "Services",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Appointments_Vets_VetId",
                    column: x => x.VetId,
                    principalTable: "Vets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.InsertData(
            table: "Breeds",
            columns: new[] { "Id", "Title", "Type" },
            values: new object[,]
            {
                { 1, "Мейн-кун", 0 },
                { 2, "Сиамская", 0 },
                { 3, "Британская короткошерстная", 0 },
                { 4, "Немецкая овчарка", 1 },
                { 5, "Лабрадор-ретривер", 1 },
                { 6, "Джек-рассел-терьер", 1 },
                { 7, "Сизый голубь", 2 },
                { 8, "Почтовый белый", 2 },
                { 9, "Якобин", 2 },
                { 10, "Ангорский кролик", 3 },
                { 11, "Голландский вислоухий", 3 },
                { 12, "Калифорнийский кролик", 3 },
                { 13, "Орловский рысак", 4 },
                { 14, "Арабская чистокровная", 4 },
                { 15, "Фризская", 4 }
            });

        migrationBuilder.InsertData(
            table: "Owners",
            columns: new[] { "Id", "Adress", "FullName", "Phone" },
            values: new object[,]
            {
                { 1, "ул. Ленина, д. 10", "Иванов Иван Иванович", "+7 (999) 111-22-33" },
                { 2, "ул. Садовая, д. 24", "Петрова Анна Сергеевна", "+7 (999) 444-55-66" },
                { 3, "пр. Мира, д. 5", "Сидоров Пётр Петрович", "+7 (999) 777-88-99" },
                { 4, "ул. Гагарина, д. 12", "Кузнецова Ольга Владимировна", "+7 (999) 222-33-44" },
                { 5, "ул. Чехова, д. 8", "Смирнов Дмитрий Александрович", "+7 (999) 555-66-77" },
                { 6, "ул. Пушкина, д. 15", "Васильева Елена Николаевна", "+7 (999) 888-99-00" },
                { 7, "ул. Новая, д. 3", "Попов Сергей Игоревич", "+7 (999) 333-44-55" },
                { 8, "ул. Полевая, д. 21", "Михайлова Татьяна Юрьевна", "+7 (999) 666-77-88" },
                { 9, "ул. Лесная, д. 4", "Федоров Андрей Олегович", "+7 (999) 999-00-11" },
                { 10, "ул. Набережная, д. 30", "Морозова Наталья Сергеевна", "+7 (999) 123-98-76" },
                { 11, "ул. Первомайская, д. 23", "Иванов Сергей Викторович", "+7 (999) 477-55-66" }
            });

        migrationBuilder.InsertData(
            table: "Specializations",
            columns: new[] { "Id", "TargetSpecies", "Title" },
            values: new object[,]
            {
                { 1, "Cat", "Терапевт-фелинолог" },
                { 2, "Cat", "Хирург-онколог кошачьих отделений" },
                { 3, "Dog", "Кинолог-терапевт" },
                { 4, "Dog", "Травматолог-ортопед собак крупных пород" },
                { 5, "Pigeon", "Ветеринар-орнитолог" },
                { 6, "Pigeon", "Хирург-авиарлог" },
                { 7, "Rabbit", "Ветеринар-ратолог" },
                { 8, "Rabbit", "Стоматолог-родентолог" },
                { 9, "Horse", "Ветеринар-ипполог" },
                { 10, "Horse", "Ортопед-подолог" }
            });

        migrationBuilder.InsertData(
            table: "Pets",
            columns: new[] { "Id", "BirthDate", "BreedId", "Name", "OwnerId", "Type", "Weight" },
            values: new object[,]
            {
                { 1, new DateOnly(2022, 4, 10), 1, "Барсик", 1, "Cat", 6.5m },
                { 2, new DateOnly(2023, 6, 15), 2, "Мурка", 2, "Cat", 4m },
                { 3, new DateOnly(2025, 1, 20), 3, "Симба", 3, "Cat", 4.8m },
                { 4, new DateOnly(2020, 11, 5), 4, "Шарик", 1, "Dog", 30.5m },
                { 5, new DateOnly(2021, 8, 25), 5, "Рекс", 4, "Dog", 28.5m },
                { 6, new DateOnly(2024, 3, 14), 6, "Граф", 5, "Dog", 8.7m },
                { 7, new DateOnly(2025, 5, 12), 7, "Кеша", 6, "Pigeon", 0.65m },
                { 8, new DateOnly(2024, 7, 19), 8, "Ворчун", 7, "Pigeon", 0.8m },
                { 9, new DateOnly(2026, 2, 1), 9, "Пират", 8, "Pigeon", 1.15m },
                { 10, new DateOnly(2023, 10, 1), 10, "Пушок", 2, "Rabbit", 3.7m },
                { 11, new DateOnly(2024, 12, 11), 11, "Снежок", 9, "Rabbit", 2.3m },
                { 12, new DateOnly(2025, 4, 3), 12, "Банни", 10, "Rabbit", 4.5m },
                { 13, new DateOnly(2019, 5, 20), 13, "Буцефал", 4, "Horse", 450m },
                { 14, new DateOnly(2021, 9, 15), 14, "Искра", 7, "Horse", 420m },
                { 15, new DateOnly(2020, 6, 30), 15, "Тайфон", 10, "Horse", 510m },
                { 16, new DateOnly(2020, 4, 10), 1, "Мурзик", 11, "Cat", 6m },
                { 17, new DateOnly(2023, 9, 15), 14, "Боцик", 11, "Horse", 375m }
            });

        migrationBuilder.InsertData(
            table: "Services",
            columns: new[] { "Id", "Price", "SpecializationId", "Title" },
            values: new object[,]
            {
                { 1, 1000m, 1, "Первичный осмотр и термометрия кошек" },
                { 2, 1500m, 1, "Комплексная вакцинация кошачьих" },
                { 3, 1800m, 1, "Гигиеническая чистка зубов кошкам" },
                { 4, 1200m, 3, "Клинический осмотр и консультация собак" },
                { 5, 1700m, 3, "Комплексная вакцинация собак (с бешенством)" },
                { 6, 2500m, 3, "УЗИ органов брюшной полости собак" },
                { 7, 900m, 5, "Осмотр птицы и микроскопия мазка" },
                { 8, 800m, 5, "Коррекция (подрезка) переросшего клюва" },
                { 9, 1100m, 5, "Лабораторный анализ оперения на паразитов" },
                { 10, 950m, 7, "Прием ратолога и визуальная диагностика" },
                { 11, 1300m, 7, "Коррекция и подпилка резцов у грызунов" },
                { 12, 1100m, 7, "Вакцинация кроликов от ВГБК и миксоматоза" },
                { 13, 4000m, 9, "Выездной осмотр и аускультация лошади" },
                { 14, 5000m, 9, "Расчистка и ортопедическая обработка копыт" },
                { 15, 6500m, 9, "Рентгенография конечностей лошади" }
            });

        migrationBuilder.InsertData(
            table: "Vets",
            columns: new[] { "Id", "BirthDate", "ExpirienceYears", "FullName", "PassportNumber", "SpecializationId" },
            values: new object[,]
            {
                { 1, new DateOnly(1980, 5, 12), 20, "Др. Алексей Айболит", "4511 111111", 1 },
                { 2, new DateOnly(1990, 8, 14), 6, "Др. Степан Котов", "4511 222222", 2 },
                { 3, new DateOnly(1985, 11, 23), 12, "Др. Елена Быкова", "4512 333333", 3 },
                { 4, new DateOnly(1993, 1, 19), 4, "Др. Михаил Шариков", "4512 444444", 4 },
                { 5, new DateOnly(1992, 7, 4), 6, "Др. Анна Смит", "4514 777777", 5 },
                { 6, new DateOnly(1983, 4, 27), 15, "Др. Григорий Птицын", "4514 888888", 6 },
                { 7, new DateOnly(1988, 9, 30), 10, "Др. Кэррот Роджерс", "4515 999999", 7 },
                { 8, new DateOnly(1995, 2, 10), 3, "Др. Ольга Ушастая", "4515 000000", 8 },
                { 9, new DateOnly(1978, 3, 15), 25, "Др. Иван Дулиттл", "4513 555555", 9 },
                { 10, new DateOnly(1989, 12, 5), 9, "Др. Ксения Конева", "4513 666666", 10 }
            });

        migrationBuilder.InsertData(
            table: "Appointments",
            columns: new[] { "Id", "DateTime", "Diagnosis", "PetId", "RepeatVisitIndicator", "RoomNumber", "ServiceId", "VetId" },
            values: new object[,]
            {
                { 1, new DateTime(2026, 9, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), "Здоров", 1, "Not", 101, 1, 1 },
                { 2, new DateTime(2026, 2, 2, 11, 30, 0, 0, DateTimeKind.Unspecified), "Здорова", 2, "Not", 103, 1, 2 },
                { 3, new DateTime(2026, 9, 8, 10, 30, 0, 0, DateTimeKind.Unspecified), "Зубной камень удален", 2, "Yes", 101, 2, 1 },
                { 4, new DateTime(2026, 9, 12, 15, 45, 0, 0, DateTimeKind.Unspecified), "Витамины прописаны", 3, "Not", 103, 1, 2 },
                { 5, new DateTime(2026, 6, 3, 14, 0, 0, 0, DateTimeKind.Unspecified), "Здоров", 4, "Not", 102, 3, 3 },
                { 6, new DateTime(2026, 7, 4, 15, 0, 0, 0, DateTimeKind.Unspecified), "Назначена диета", 5, "Not", 106, 3, 4 },
                { 7, new DateTime(2026, 9, 9, 12, 0, 0, 0, DateTimeKind.Unspecified), "Плановое УЗИ", 4, "Yes", 102, 4, 3 },
                { 8, new DateTime(2026, 9, 13, 16, 0, 0, 0, DateTimeKind.Unspecified), "Вакцинация выполнена", 6, "Not", 102, 3, 3 },
                { 9, new DateTime(2026, 6, 6, 16, 30, 0, 0, DateTimeKind.Unspecified), "Здоров", 7, "Not", 105, 5, 5 },
                { 10, new DateTime(2026, 7, 14, 10, 0, 0, 0, DateTimeKind.Unspecified), "Клюв в норме", 8, "Yes", 107, 5, 6 },
                { 11, new DateTime(2026, 8, 16, 11, 0, 0, 0, DateTimeKind.Unspecified), "Осмотр крыла", 9, "Not", 105, 5, 5 },
                { 12, new DateTime(2026, 10, 7, 13, 0, 0, 0, DateTimeKind.Unspecified), "Отрастание резцов", 10, "Not", 104, 6, 7 },
                { 13, new DateTime(2026, 10, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), "Зубы в порядке", 11, "Yes", 108, 6, 8 },
                { 14, new DateTime(2026, 10, 17, 9, 30, 0, 0, DateTimeKind.Unspecified), "Здоров", 12, "Not", 104, 6, 7 },
                { 15, new DateTime(2026, 9, 5, 9, 0, 0, 0, DateTimeKind.Unspecified), "Профилактика копыт", 13, "Not", 109, 7, 9 },
                { 16, new DateTime(2026, 9, 12, 9, 0, 0, 0, DateTimeKind.Unspecified), "Профилактика копыт", 17, "Not", 109, 7, 9 },
                { 17, new DateTime(2026, 8, 9, 11, 30, 0, 0, DateTimeKind.Unspecified), "Здоров", 16, "Yes", 103, 1, 2 }
            });

        migrationBuilder.CreateIndex(
            name: "IX_Appointments_PetId",
            table: "Appointments",
            column: "PetId");

        migrationBuilder.CreateIndex(
            name: "IX_Appointments_ServiceId",
            table: "Appointments",
            column: "ServiceId");

        migrationBuilder.CreateIndex(
            name: "IX_Appointments_VetId",
            table: "Appointments",
            column: "VetId");

        migrationBuilder.CreateIndex(
            name: "IX_Pets_BreedId",
            table: "Pets",
            column: "BreedId");

        migrationBuilder.CreateIndex(
            name: "IX_Pets_OwnerId",
            table: "Pets",
            column: "OwnerId");

        migrationBuilder.CreateIndex(
            name: "IX_Services_SpecializationId",
            table: "Services",
            column: "SpecializationId");

        migrationBuilder.CreateIndex(
            name: "IX_Specializations_Title",
            table: "Specializations",
            column: "Title",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Vets_SpecializationId",
            table: "Vets",
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
            name: "Services");

        migrationBuilder.DropTable(
            name: "Vets");

        migrationBuilder.DropTable(
            name: "Breeds");

        migrationBuilder.DropTable(
            name: "Owners");

        migrationBuilder.DropTable(
            name: "Specializations");
    }
}
