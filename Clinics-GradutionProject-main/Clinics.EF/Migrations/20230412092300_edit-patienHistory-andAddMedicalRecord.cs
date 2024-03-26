using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinics.EF.Migrations
{
    /// <inheritdoc />
    public partial class editpatienHistoryandAddMedicalRecord : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Immunizations_PatientHistories_PatientHistoryId",
                table: "Immunizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Medications_PatientHistories_PatientHistoryId",
                table: "Medications");

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

            migrationBuilder.RenameColumn(
                name: "PatientHistoryId",
                table: "Medications",
                newName: "MedicalRecordID");

            migrationBuilder.RenameIndex(
                name: "IX_Medications_PatientHistoryId",
                table: "Medications",
                newName: "IX_Medications_MedicalRecordID");

            migrationBuilder.RenameColumn(
                name: "PatientHistoryId",
                table: "Immunizations",
                newName: "MedicalRecordID");

            migrationBuilder.RenameIndex(
                name: "IX_Immunizations_PatientHistoryId",
                table: "Immunizations",
                newName: "IX_Immunizations_MedicalRecordID");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateReceived",
                table: "Immunizations",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "MedicalRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PatientId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PastMedicalConditions = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PastSurgeries = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hospitalizations = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FatherMedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MotherMedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GrandfatherMedicalHistory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Allergies = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicalRecords_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MedicalRecords_PatientId",
                table: "MedicalRecords",
                column: "PatientId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Immunizations_MedicalRecords_MedicalRecordID",
                table: "Immunizations",
                column: "MedicalRecordID",
                principalTable: "MedicalRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_MedicalRecords_MedicalRecordID",
                table: "Medications",
                column: "MedicalRecordID",
                principalTable: "MedicalRecords",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Immunizations_MedicalRecords_MedicalRecordID",
                table: "Immunizations");

            migrationBuilder.DropForeignKey(
                name: "FK_Medications_MedicalRecords_MedicalRecordID",
                table: "Medications");

            migrationBuilder.DropTable(
                name: "MedicalRecords");

            migrationBuilder.DropColumn(
                name: "DateReceived",
                table: "Immunizations");

            migrationBuilder.RenameColumn(
                name: "MedicalRecordID",
                table: "Medications",
                newName: "PatientHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Medications_MedicalRecordID",
                table: "Medications",
                newName: "IX_Medications_PatientHistoryId");

            migrationBuilder.RenameColumn(
                name: "MedicalRecordID",
                table: "Immunizations",
                newName: "PatientHistoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Immunizations_MedicalRecordID",
                table: "Immunizations",
                newName: "IX_Immunizations_PatientHistoryId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Immunizations_PatientHistories_PatientHistoryId",
                table: "Immunizations",
                column: "PatientHistoryId",
                principalTable: "PatientHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Medications_PatientHistories_PatientHistoryId",
                table: "Medications",
                column: "PatientHistoryId",
                principalTable: "PatientHistories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
