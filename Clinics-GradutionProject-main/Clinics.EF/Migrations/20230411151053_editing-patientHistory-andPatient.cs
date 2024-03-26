using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinics.EF.Migrations
{
    /// <inheritdoc />
    public partial class editingpatientHistoryandPatient : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Patients",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Allergies",
                table: "PatientHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FatherMedicalHistory",
                table: "PatientHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "GrandfatherMedicalHistory",
                table: "PatientHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Hospitalizations",
                table: "PatientHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MotherMedicalHistory",
                table: "PatientHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PastMedicalConditions",
                table: "PatientHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PastSurgeries",
                table: "PatientHistories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Immunizations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Immunizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Immunizations_PatientHistories_PatientHistoryId",
                        column: x => x.PatientHistoryId,
                        principalTable: "PatientHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Medications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PatientHistoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Medications_PatientHistories_PatientHistoryId",
                        column: x => x.PatientHistoryId,
                        principalTable: "PatientHistories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Immunizations_PatientHistoryId",
                table: "Immunizations",
                column: "PatientHistoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Medications_PatientHistoryId",
                table: "Medications",
                column: "PatientHistoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Immunizations");

            migrationBuilder.DropTable(
                name: "Medications");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "Allergies",
                table: "PatientHistories");

            migrationBuilder.DropColumn(
                name: "FatherMedicalHistory",
                table: "PatientHistories");

            migrationBuilder.DropColumn(
                name: "GrandfatherMedicalHistory",
                table: "PatientHistories");

            migrationBuilder.DropColumn(
                name: "Hospitalizations",
                table: "PatientHistories");

            migrationBuilder.DropColumn(
                name: "MotherMedicalHistory",
                table: "PatientHistories");

            migrationBuilder.DropColumn(
                name: "PastMedicalConditions",
                table: "PatientHistories");

            migrationBuilder.DropColumn(
                name: "PastSurgeries",
                table: "PatientHistories");
        }
    }
}
