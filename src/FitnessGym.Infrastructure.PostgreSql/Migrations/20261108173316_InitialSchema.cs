using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FitnessGym.Infrastructure.PostgreSql.Migrations;

/// <inheritdoc />
public partial class InitialSchema : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Clients",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Phone = table.Column<string>(type: "text", nullable: false),
                SubscriptionStart = table.Column<DateOnly>(type: "date", nullable: false),
                SubscriptionEnd = table.Column<DateOnly>(type: "date", nullable: false),
                PassportNumber = table.Column<string>(type: "text", nullable: false),
                FullName = table.Column<string>(type: "text", nullable: false),
                Gender = table.Column<int>(type: "integer", nullable: false),
                BirthDate = table.Column<DateOnly>(type: "date", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Clients", x => x.Id);
                table.CheckConstraint("CK_Clients_SubscriptionDates", "\"SubscriptionEnd\" >= \"SubscriptionStart\"");
            });

        migrationBuilder.CreateTable(
            name: "Specializations",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false)
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
                    .Annotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                SpecializationId = table.Column<int>(type: "integer", nullable: false),
                ExperienceYears = table.Column<int>(type: "integer", nullable: false),
                PassportNumber = table.Column<string>(type: "text", nullable: false),
                FullName = table.Column<string>(type: "text", nullable: false),
                Gender = table.Column<int>(type: "integer", nullable: false),
                BirthDate = table.Column<DateOnly>(type: "date", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Trainers", x => x.Id);
                table.CheckConstraint("CK_Trainers_ExperienceYears", "\"ExperienceYears\" BETWEEN 0 AND 100");
                table.ForeignKey(
                    name: "FK_Trainers_Specializations_SpecializationId",
                    column: x => x.SpecializationId,
                    principalTable: "Specializations",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "TrainingSessions",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:IdentitySequenceOptions", "'100', '1', '', '', 'False', '1'")
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                ClientId = table.Column<int>(type: "integer", nullable: false),
                TrainerId = table.Column<int>(type: "integer", nullable: false),
                StartsAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                EndsAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                HallName = table.Column<string>(type: "text", nullable: false),
                IsTrial = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TrainingSessions", x => x.Id);
                table.CheckConstraint("CK_TrainingSessions_TimeRange", "\"EndsAt\" IS NULL OR \"EndsAt\" > \"StartsAt\"");
                table.ForeignKey(
                    name: "FK_TrainingSessions_Clients_ClientId",
                    column: x => x.ClientId,
                    principalTable: "Clients",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_TrainingSessions_Trainers_TrainerId",
                    column: x => x.TrainerId,
                    principalTable: "Trainers",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Clients_SubscriptionEnd",
            table: "Clients",
            column: "SubscriptionEnd");

        migrationBuilder.CreateIndex(
            name: "IX_Trainers_SpecializationId",
            table: "Trainers",
            column: "SpecializationId");

        migrationBuilder.CreateIndex(
            name: "IX_TrainingSessions_ClientId",
            table: "TrainingSessions",
            column: "ClientId");

        migrationBuilder.CreateIndex(
            name: "IX_TrainingSessions_HallName_StartsAt",
            table: "TrainingSessions",
            columns: ["HallName", "StartsAt"]);

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
            name: "Specializations");
    }
}
