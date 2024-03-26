using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinics.EF.Migrations
{
    /// <inheritdoc />
    public partial class EdtingClincTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Geolocation",
                table: "Clinics",
                newName: "phoneNumber");

            migrationBuilder.AddColumn<decimal>(
                name: "latitude",
                table: "Clinics",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "longitude",
                table: "Clinics",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "latitude",
                table: "Clinics");

            migrationBuilder.DropColumn(
                name: "longitude",
                table: "Clinics");

            migrationBuilder.RenameColumn(
                name: "phoneNumber",
                table: "Clinics",
                newName: "Geolocation");
        }
    }
}
