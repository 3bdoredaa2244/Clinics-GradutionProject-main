using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinics.EF.Migrations
{
    /// <inheritdoc />
    public partial class relationshipreservationclinics : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservations_Clinics_ClinicId",
                table: "Reservations");

            migrationBuilder.DropIndex(
                name: "IX_Reservations_ClinicId",
                table: "Reservations");

            migrationBuilder.DropColumn(
                name: "ClinicId",
                table: "Reservations");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ClinicId",
                table: "Reservations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_ClinicId",
                table: "Reservations",
                column: "ClinicId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservations_Clinics_ClinicId",
                table: "Reservations",
                column: "ClinicId",
                principalTable: "Clinics",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
