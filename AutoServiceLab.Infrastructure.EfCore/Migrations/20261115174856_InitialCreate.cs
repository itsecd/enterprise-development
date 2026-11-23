using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AutoServiceLab.Infrastructure.EfCore.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "clients",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                full_name = table.Column<string>(type: "text", nullable: false),
                phone = table.Column<string>(type: "text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_clients", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "mechanics",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                full_name = table.Column<string>(type: "text", nullable: false),
                specialization = table.Column<int>(type: "integer", nullable: false),
                passport_number = table.Column<string>(type: "text", nullable: false),
                phone = table.Column<string>(type: "text", nullable: false),
                experience_years = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_mechanics", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "work_types",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                name = table.Column<string>(type: "text", nullable: false),
                category = table.Column<int>(type: "integer", nullable: false),
                duration = table.Column<decimal>(type: "numeric", nullable: false),
                labor_cost = table.Column<decimal>(type: "numeric", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_work_types", x => x.id);
            });

        migrationBuilder.CreateTable(
            name: "cars",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                license_plate = table.Column<string>(type: "text", nullable: false),
                brand = table.Column<string>(type: "text", nullable: false),
                model = table.Column<string>(type: "text", nullable: false),
                year = table.Column<int>(type: "integer", nullable: false),
                client_id = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_cars", x => x.id);
                table.ForeignKey(
                    name: "FK_cars_clients_client_id",
                    column: x => x.client_id,
                    principalTable: "clients",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "service_orders",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                reception_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                issue_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                client_id = table.Column<int>(type: "integer", nullable: false),
                car_id = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_service_orders", x => x.id);
                table.ForeignKey(
                    name: "FK_service_orders_cars_car_id",
                    column: x => x.car_id,
                    principalTable: "cars",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_service_orders_clients_client_id",
                    column: x => x.client_id,
                    principalTable: "clients",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "order_mechanics",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                service_order_id = table.Column<int>(type: "integer", nullable: false),
                mechanic_id = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_order_mechanics", x => x.id);
                table.ForeignKey(
                    name: "FK_order_mechanics_mechanics_mechanic_id",
                    column: x => x.mechanic_id,
                    principalTable: "mechanics",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_order_mechanics_service_orders_service_order_id",
                    column: x => x.service_order_id,
                    principalTable: "service_orders",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "order_works",
            columns: table => new
            {
                id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                service_order_id = table.Column<int>(type: "integer", nullable: false),
                work_type_id = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_order_works", x => x.id);
                table.ForeignKey(
                    name: "FK_order_works_service_orders_service_order_id",
                    column: x => x.service_order_id,
                    principalTable: "service_orders",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_order_works_work_types_work_type_id",
                    column: x => x.work_type_id,
                    principalTable: "work_types",
                    principalColumn: "id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.InsertData(
            table: "clients",
            columns: new[] { "id", "full_name", "phone" },
            values: new object[,]
            {
                { 1, "Иванов Иван Иванович", "+7-927-111-11-11" },
                { 2, "Петрова Мария Сергеевна", "+7-927-222-22-22" },
                { 3, "Сидоров Алексей Петрович", "+7-927-333-33-33" },
                { 4, "Козлова Елена Дмитриевна", "+7-927-444-44-44" },
                { 5, "Морозов Дмитрий Андреевич", "+7-927-555-55-55" },
                { 6, "Волкова Анна Владимировна", "+7-927-666-66-66" },
                { 7, "Зайцев Артем Олегович", "+7-927-777-77-77" },
                { 8, "Соколова Наталья Игоревна", "+7-927-888-88-88" },
                { 9, "Кузнецов Максим Романович", "+7-927-999-99-99" },
                { 10, "Смирнова Ольга Павловна", "+7-927-000-00-00" }
            });

        migrationBuilder.InsertData(
            table: "mechanics",
            columns: new[] { "id", "experience_years", "full_name", "passport_number", "phone", "specialization" },
            values: new object[,]
            {
                { 1, 4, "Сидоров Петр Иванович", "1111 111111", "+7-937-111-11-11", 0 },
                { 2, 6, "Иванов Александр Петрович", "2222 222222", "+7-937-111-11-12", 1 },
                { 3, 8, "Петрова Екатерина Сергеевна", "3333 333333", "+7-937-111-11-13", 2 },
                { 4, 5, "Смирнов Денис Андреевич", "4444 444444", "+7-937-111-11-14", 3 },
                { 5, 7, "Козлова Ольга Дмитриевна", "5555 555555", "+7-937-111-11-15", 0 },
                { 6, 10, "Морозов Владимир Игоревич", "6666 666666", "+7-937-111-11-16", 1 },
                { 7, 3, "Новикова Мария Олеговна", "7777 777777", "+7-937-111-11-17", 2 },
                { 8, 9, "Федоров Алексей Романович", "8888 888888", "+7-937-111-11-18", 3 },
                { 9, 12, "Егоров Михаил Валерьевич", "9999 999999", "+7-937-111-11-19", 4 },
                { 10, 2, "Тимофеев Илья Андреевич", "1010 101010", "+7-937-111-11-10", 5 }
            });

        migrationBuilder.InsertData(
            table: "work_types",
            columns: new[] { "id", "category", "duration", "labor_cost", "name" },
            values: new object[,]
            {
                { 1, 0, 1.0m, 3750m, "Замена масла" },
                { 2, 1, 1.5m, 2500m, "Диагностика двигателя" },
                { 3, 2, 8.0m, 20000m, "Ремонт двигателя" },
                { 4, 5, 2.0m, 5000m, "Замена тормозных колодок" },
                { 5, 4, 1.0m, 2400m, "Замена шин" },
                { 6, 6, 3.0m, 6600m, "Ремонт проводки" },
                { 7, 3, 10.0m, 24000m, "Ремонт АКПП" },
                { 8, 7, 6.0m, 15000m, "Кузовной ремонт" },
                { 9, 2, 4.0m, 8000m, "Замена ремня ГРМ" },
                { 10, 4, 3.0m, 6000m, "Замена амортизаторов" }
            });

        migrationBuilder.InsertData(
            table: "cars",
            columns: new[] { "id", "brand", "client_id", "license_plate", "model", "year" },
            values: new object[,]
            {
                { 1, "Lada", 1, "Х011ХХ163", "Granta", 2020 },
                { 2, "Toyota", 2, "А001АА163", "Camry", 2020 },
                { 3, "BMW", 2, "В002ВВ163", "X5", 2022 },
                { 4, "Mercedes", 3, "С003СС163", "E-Class", 2021 },
                { 5, "Audi", 4, "Е004ЕЕ163", "Q7", 2020 },
                { 6, "Volkswagen", 5, "К005КК163", "Passat", 2019 },
                { 7, "Ford", 6, "М006ММ163", "Focus", 2021 },
                { 8, "Toyota", 7, "Н007НН163", "Land Cruiser", 2022 },
                { 9, "Hyundai", 8, "О008ОО163", "Sonata", 2020 },
                { 10, "Nissan", 9, "Р009РР163", "X-Trail", 2021 },
                { 11, "BMW", 10, "Т010ТТ163", "3 Series", 2020 }
            });

        migrationBuilder.InsertData(
            table: "service_orders",
            columns: new[] { "id", "car_id", "client_id", "issue_date", "reception_date" },
            values: new object[,]
            {
                { 1, 1, 1, new DateTime(2026, 9, 3, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 1, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 2, 2, 2, new DateTime(2026, 9, 7, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 5, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 3, 3, 2, null, new DateTime(2026, 9, 8, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 4, 4, 3, new DateTime(2026, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 10, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 5, 5, 4, new DateTime(2026, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 12, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 6, 5, 4, null, new DateTime(2026, 9, 15, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 7, 6, 5, new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 18, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 8, 7, 6, new DateTime(2026, 9, 23, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 20, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 9, 8, 7, new DateTime(2026, 9, 22, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 21, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 10, 9, 8, null, new DateTime(2026, 9, 22, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 11, 10, 9, new DateTime(2026, 9, 27, 0, 0, 0, 0, DateTimeKind.Utc), new DateTime(2026, 9, 25, 0, 0, 0, 0, DateTimeKind.Utc) },
                { 12, 11, 10, null, new DateTime(2026, 9, 26, 0, 0, 0, 0, DateTimeKind.Utc) }
            });

        migrationBuilder.InsertData(
            table: "order_mechanics",
            columns: new[] { "id", "mechanic_id", "service_order_id" },
            values: new object[,]
            {
                { 1, 1, 1 },
                { 2, 5, 2 },
                { 3, 10, 3 },
                { 4, 9, 4 },
                { 5, 2, 5 },
                { 6, 4, 6 },
                { 7, 3, 7 },
                { 8, 1, 8 },
                { 9, 9, 9 },
                { 10, 1, 10 },
                { 11, 5, 11 },
                { 12, 5, 12 }
            });

        migrationBuilder.InsertData(
            table: "order_works",
            columns: new[] { "id", "service_order_id", "work_type_id" },
            values: new object[,]
            {
                { 1, 1, 1 },
                { 2, 1, 2 },
                { 3, 2, 3 },
                { 4, 3, 4 },
                { 5, 4, 5 },
                { 6, 5, 6 },
                { 7, 6, 7 },
                { 8, 7, 8 },
                { 9, 8, 9 },
                { 10, 9, 10 },
                { 11, 10, 1 },
                { 12, 11, 3 }
            });

        migrationBuilder.CreateIndex(
            name: "IX_cars_client_id",
            table: "cars",
            column: "client_id");

        migrationBuilder.CreateIndex(
            name: "IX_order_mechanics_mechanic_id",
            table: "order_mechanics",
            column: "mechanic_id");

        migrationBuilder.CreateIndex(
            name: "IX_order_mechanics_service_order_id",
            table: "order_mechanics",
            column: "service_order_id");

        migrationBuilder.CreateIndex(
            name: "IX_order_works_service_order_id",
            table: "order_works",
            column: "service_order_id");

        migrationBuilder.CreateIndex(
            name: "IX_order_works_work_type_id",
            table: "order_works",
            column: "work_type_id");

        migrationBuilder.CreateIndex(
            name: "IX_service_orders_car_id",
            table: "service_orders",
            column: "car_id");

        migrationBuilder.CreateIndex(
            name: "IX_service_orders_client_id",
            table: "service_orders",
            column: "client_id");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "order_mechanics");

        migrationBuilder.DropTable(
            name: "order_works");

        migrationBuilder.DropTable(
            name: "mechanics");

        migrationBuilder.DropTable(
            name: "service_orders");

        migrationBuilder.DropTable(
            name: "work_types");

        migrationBuilder.DropTable(
            name: "cars");

        migrationBuilder.DropTable(
            name: "clients");
    }
}
