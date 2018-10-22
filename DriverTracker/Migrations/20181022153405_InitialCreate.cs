using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DriverTracker.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Analysts",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(nullable: false),
                    Username = table.Column<string>(nullable: true),
                    FullName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    SMSNumber = table.Column<string>(nullable: true),
                    AccountStatus = table.Column<int>(nullable: false),
                    ReceivesSMSAlertsNewDrivers = table.Column<bool>(nullable: false),
                    ReceivesSMSAlertsDriversTerminated = table.Column<bool>(nullable: false),
                    ReceivesSMSAlertsLongDriverWaits = table.Column<bool>(nullable: false),
                    SMSAlertDriverWaitTime = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analysts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    DriverID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    UserID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    LicenseNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.DriverID);
                });

            migrationBuilder.CreateTable(
                name: "Analyses",
                columns: table => new
                {
                    AnalysisID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    AnalystID = table.Column<int>(nullable: false),
                    DriverID = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Analyses", x => x.AnalysisID);
                    table.ForeignKey(
                        name: "FK_Analyses_Analysts_AnalystID",
                        column: x => x.AnalystID,
                        principalTable: "Analysts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Analyses_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "DriverID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Legs",
                columns: table => new
                {
                    LegID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DriverID = table.Column<int>(nullable: false),
                    PreviousLegID = table.Column<int>(nullable: true),
                    StartAddress = table.Column<string>(nullable: true),
                    PickupRequestTime = table.Column<DateTime>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    DestinationAddress = table.Column<string>(nullable: true),
                    ArrivalTime = table.Column<DateTime>(nullable: false),
                    Distance = table.Column<decimal>(nullable: false),
                    Fare = table.Column<decimal>(nullable: false),
                    NumOfPassengersAboard = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Legs", x => x.LegID);
                    table.ForeignKey(
                        name: "FK_Legs_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "DriverID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Legs_Legs_PreviousLegID",
                        column: x => x.PreviousLegID,
                        principalTable: "Legs",
                        principalColumn: "LegID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_AnalystID",
                table: "Analyses",
                column: "AnalystID");

            migrationBuilder.CreateIndex(
                name: "IX_Analyses_DriverID",
                table: "Analyses",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Legs_DriverID",
                table: "Legs",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Legs_PreviousLegID",
                table: "Legs",
                column: "PreviousLegID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Analyses");

            migrationBuilder.DropTable(
                name: "Legs");

            migrationBuilder.DropTable(
                name: "Analysts");

            migrationBuilder.DropTable(
                name: "Drivers");
        }
    }
}
