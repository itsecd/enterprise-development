using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace VetClinic.Infrastructure.PostgreSql.Migrations;

/// <inheritdoc />
public partial class InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Breeds",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false),
                Species = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Breeds", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Owners",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                FullName = table.Column<string>(type: "text", nullable: false),
                Phone = table.Column<string>(type: "text", nullable: false),
                Address = table.Column<string>(type: "text", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Owners", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Specializations",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false),
                TargetSpecies = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Specializations", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "Pets",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Name = table.Column<string>(type: "text", nullable: false),
                Species = table.Column<int>(type: "integer", nullable: false),
                BreedId = table.Column<int>(type: "integer", nullable: false),
                BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                WeightKg = table.Column<decimal>(type: "numeric", nullable: false),
                OwnerId = table.Column<int>(type: "integer", nullable: false)
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
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Veterinarians",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                PassportNumber = table.Column<string>(type: "text", nullable: false),
                FullName = table.Column<string>(type: "text", nullable: false),
                BirthYear = table.Column<int>(type: "integer", nullable: false),
                SpecializationId = table.Column<int>(type: "integer", nullable: false),
                ExperienceYears = table.Column<int>(type: "integer", nullable: false)
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
            });

        migrationBuilder.CreateTable(
            name: "Appointments",
            columns: table => new
            {
                Id = table.Column<int>(type: "integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                DateTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                RoomNumber = table.Column<int>(type: "integer", nullable: false),
                IsFollowUp = table.Column<bool>(type: "boolean", nullable: false),
                PetId = table.Column<int>(type: "integer", nullable: false),
                VeterinarianId = table.Column<int>(type: "integer", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Appointments", x => x.Id);
                table.ForeignKey(
                    name: "FK_Appointments_Pets_PetId",
                    column: x => x.PetId,
                    principalTable: "Pets",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Appointments_Veterinarians_VeterinarianId",
                    column: x => x.VeterinarianId,
                    principalTable: "Veterinarians",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
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
