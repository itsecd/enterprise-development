using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarWash.Infrastructure.Ef.Migrations
{
    /// <inheritdoc />
    public partial class OrderTimestampsUtc : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // У старых значений нет смещения; интерпретируем их как UTC.
            migrationBuilder.Sql("""
                ALTER TABLE "Orders" ALTER COLUMN "StartTime" TYPE timestamp with time zone
                    USING "StartTime" AT TIME ZONE 'UTC';
                """);

            // Обновляем один раз только исходный демонстрационный заказ, если его не изменяли.
            migrationBuilder.Sql("""
                UPDATE "Orders"
                SET "StartTime" = CURRENT_TIMESTAMP - INTERVAL '5 minutes'
                WHERE "Id" = 16 AND "CustomerId" = 1 AND "CarId" = 1
                    AND "WashServiceId" = 1 AND "BoxNumber" = 1
                    AND "StartTime" = TIMESTAMPTZ '2026-01-01 11:55:00+00';
                """);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("""
                ALTER TABLE "Orders" ALTER COLUMN "StartTime" TYPE timestamp without time zone
                    USING "StartTime" AT TIME ZONE 'UTC';
                """);
        }
    }
}
