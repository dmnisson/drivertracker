using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DriverTracker.Migrations
{
    public partial class AddPickupRequestEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PickupRequests",
                columns: table => new
                {
                    PickupRequestID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RequestedTime = table.Column<DateTime>(nullable: false),
                    RequestedAddress = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupRequests", x => x.PickupRequestID);
                });

            migrationBuilder.CreateTable(
                name: "AnsweredPickupRequests",
                columns: table => new
                {
                    AnsweredPickupRequestID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Leg = table.Column<int>(nullable: true),
                    PickupRequest = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnsweredPickupRequests", x => x.AnsweredPickupRequestID);
                    table.ForeignKey(
                        name: "FK_AnsweredPickupRequests_Legs_Leg",
                        column: x => x.Leg,
                        principalTable: "Legs",
                        principalColumn: "LegID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AnsweredPickupRequests_PickupRequests_PickupRequest",
                        column: x => x.PickupRequest,
                        principalTable: "PickupRequests",
                        principalColumn: "PickupRequestID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PickupDriverAssignments",
                columns: table => new
                {
                    PickupDriverAssignmentID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Driver = table.Column<int>(nullable: true),
                    PickupRequest = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickupDriverAssignments", x => x.PickupDriverAssignmentID);
                    table.ForeignKey(
                        name: "FK_PickupDriverAssignments_Drivers_Driver",
                        column: x => x.Driver,
                        principalTable: "Drivers",
                        principalColumn: "DriverID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PickupDriverAssignments_PickupRequests_PickupRequest",
                        column: x => x.PickupRequest,
                        principalTable: "PickupRequests",
                        principalColumn: "PickupRequestID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnsweredPickupRequests_Leg",
                table: "AnsweredPickupRequests",
                column: "Leg",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AnsweredPickupRequests_PickupRequest",
                table: "AnsweredPickupRequests",
                column: "PickupRequest",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PickupDriverAssignments_Driver",
                table: "PickupDriverAssignments",
                column: "Driver");

            migrationBuilder.CreateIndex(
                name: "IX_PickupDriverAssignments_PickupRequest",
                table: "PickupDriverAssignments",
                column: "PickupRequest",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnsweredPickupRequests");

            migrationBuilder.DropTable(
                name: "PickupDriverAssignments");

            migrationBuilder.DropTable(
                name: "PickupRequests");
        }
    }
}
