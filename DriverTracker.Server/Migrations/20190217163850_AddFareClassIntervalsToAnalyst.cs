using Microsoft.EntityFrameworkCore.Migrations;

namespace DriverTracker.Migrations
{
    public partial class AddFareClassIntervalsToAnalyst : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FareClassIntervalsString",
                table: "Analysts",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FareClassIntervalsString",
                table: "Analysts");
        }
    }
}
