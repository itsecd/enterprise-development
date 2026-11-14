using BeautySalon.Domain.Seeds;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BeautySalon.Infrastructure.PostgreSQL.Migrations;

/// <summary>
/// Первоначальное заполнение базы фиксированным набором данных первой лабораторной.
/// Явные вставки вместо <c>HasData</c>: у иерархии <c>Person</c> (TPC) ключи клиентов
/// и мастеров пересекаются, а EF требует уникальности ключей внутри иерархии при сидировании модели.
/// </summary>
[DbContext(typeof(SalonDbContext))]
[Migration("20261114175000_SeedSalon")]
public partial class SeedSalon : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        var seed = new BeautySalonSeed();

        foreach (var client in seed.Clients)
        {
            migrationBuilder.InsertData(
                table: "clients",
                columns: ["Id", "FullName", "Gender", "PhoneNumber", "BirthDate"],
                columnTypes: ["integer", "text", "character varying(20)", "character varying(20)", "date"],
                values: [client.Id, client.FullName, client.Gender.ToString(), client.PhoneNumber, client.BirthDate]);
        }

        foreach (var master in seed.Masters)
        {
            migrationBuilder.InsertData(
                table: "masters",
                columns: ["Id", "FullName", "Gender", "PassportNumber", "Specialization", "WorkExperience"],
                columnTypes: ["integer", "text", "character varying(20)", "character varying(20)", "character varying(50)", "integer"],
                values:
                [
                    master.Id, master.FullName, master.Gender.ToString(), master.PassportNumber,
                    master.Specialization.ToString(), master.WorkExperience
                ]);
        }

        foreach (var service in seed.Services)
        {
            migrationBuilder.InsertData(
                table: "Services",
                columns: ["Id", "Name", "Category", "Price", "Duration"],
                columnTypes: ["integer", "character varying(200)", "character varying(50)", "numeric(10,2)", "interval"],
                values:
                [
                    service.Id, service.Name, service.Category.ToString(), service.Price, service.Duration
                ]);
        }

        foreach (var appointment in seed.Appointments)
        {
            migrationBuilder.InsertData(
                table: "Appointments",
                columns: ["Id", "client_id", "master_id", "service_id", "DateTime", "IsRegularClient"],
                columnTypes: ["integer", "integer", "integer", "integer", "timestamp without time zone", "boolean"],
                values:
                [
                    appointment.Id, appointment.Client.Id, appointment.Master.Id, appointment.Service.Id,
                    appointment.DateTime, appointment.IsRegularClient
                ]);
        }

        // Явные идентификаторы не сдвигают счётчики: поднимаем их до максимальных значений,
        // чтобы новые записи не конфликтовали с сидом
        migrationBuilder.Sql(
            """
            SELECT setval('"PersonSequence"',
                (SELECT COALESCE(MAX(id), 1) FROM (
                    SELECT "Id" AS id FROM clients UNION ALL SELECT "Id" AS id FROM masters) AS ids))
            """);
        migrationBuilder.Sql(
            """
            SELECT setval(pg_get_serial_sequence('"Services"', 'Id'),
                (SELECT COALESCE(MAX("Id"), 1) FROM "Services"))
            """);
        migrationBuilder.Sql(
            """
            SELECT setval(pg_get_serial_sequence('"Appointments"', 'Id'),
                (SELECT COALESCE(MAX("Id"), 1) FROM "Appointments"))
            """);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        var seed = new BeautySalonSeed();

        foreach (var appointment in seed.Appointments)
        {
            migrationBuilder.DeleteData("Appointments", "Id", appointment.Id);
        }

        foreach (var service in seed.Services)
        {
            migrationBuilder.DeleteData("Services", "Id", service.Id);
        }

        foreach (var master in seed.Masters)
        {
            migrationBuilder.DeleteData("masters", "Id", master.Id);
        }

        foreach (var client in seed.Clients)
        {
            migrationBuilder.DeleteData("clients", "Id", client.Id);
        }
    }
}
