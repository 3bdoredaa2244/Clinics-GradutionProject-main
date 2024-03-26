using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clinics.EF.Migrations
{
    /// <inheritdoc />
    public partial class addingonline : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Online",
                table: "Reservations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Online",
                table: "Reservations");
        }
    }
}
