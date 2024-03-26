using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinics.EF.Migrations
{
    /// <inheritdoc />
    public partial class removingserial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_drugDetails_Prescriptions_PrescriptionId",
                table: "drugDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_drugDetails",
                table: "drugDetails");

            migrationBuilder.DropColumn(
                name: "Serial",
                table: "Prescriptions");

            migrationBuilder.RenameTable(
                name: "drugDetails",
                newName: "DrugDetails");

            migrationBuilder.RenameIndex(
                name: "IX_drugDetails_PrescriptionId",
                table: "DrugDetails",
                newName: "IX_DrugDetails_PrescriptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DrugDetails",
                table: "DrugDetails",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_DrugDetails_Prescriptions_PrescriptionId",
                table: "DrugDetails",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DrugDetails_Prescriptions_PrescriptionId",
                table: "DrugDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DrugDetails",
                table: "DrugDetails");

            migrationBuilder.RenameTable(
                name: "DrugDetails",
                newName: "drugDetails");

            migrationBuilder.RenameIndex(
                name: "IX_DrugDetails_PrescriptionId",
                table: "drugDetails",
                newName: "IX_drugDetails_PrescriptionId");

            migrationBuilder.AddColumn<int>(
                name: "Serial",
                table: "Prescriptions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_drugDetails",
                table: "drugDetails",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_drugDetails_Prescriptions_PrescriptionId",
                table: "drugDetails",
                column: "PrescriptionId",
                principalTable: "Prescriptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
