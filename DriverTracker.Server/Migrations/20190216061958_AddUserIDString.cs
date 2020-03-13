using Microsoft.EntityFrameworkCore.Migrations;

namespace DriverTracker.Migrations
{
    public partial class AddUserIDString : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserIDString",
                table: "Drivers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserIDString",
                table: "Analysts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserIDString",
                table: "Drivers");

            migrationBuilder.DropColumn(
                name: "UserIDString",
                table: "Analysts");
        }
    }
}
