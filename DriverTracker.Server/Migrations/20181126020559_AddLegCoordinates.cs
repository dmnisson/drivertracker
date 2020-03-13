using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DriverTracker.Migrations
{
    public partial class AddLegCoordinates : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            /*migrationBuilder.AlterColumn<string>(
                name: "StartAddress",
                table: "Legs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);*/

            /*migrationBuilder.AlterColumn<string>(
                name: "DestinationAddress",
                table: "Legs",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);*/

            migrationBuilder.RenameTable("Legs", newName: "LegsTmp");
            migrationBuilder.CreateTable(
                name: "Legs",
                columns: table => new
                {
                    LegID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DriverID = table.Column<int>(nullable: false),
                    StartAddress = table.Column<string>(nullable: false),
                    PickupRequestTime = table.Column<DateTime>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    DestinationAddress = table.Column<string>(nullable: false),
                    ArrivalTime = table.Column<DateTime>(nullable: false),
                    Distance = table.Column<decimal>(nullable: false),
                    Fare = table.Column<decimal>(nullable: false),
                    NumOfPassengersAboard = table.Column<int>(nullable: false),
                    NumOfPassengersPickedUp = table.Column<int>(nullable: false),
                    FuelCost = table.Column<decimal>(nullable: false)
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
                }
            );
            migrationBuilder.Sql("INSERT INTO Legs(LegId, " +
                                 "DriverID, " +
                                 "StartAddress, " +
                                 "PickupRequestTime, " +
                                 "StartTime, " +
                                 "DestinationAddress, " +
                                 "ArrivalTime, " +
                                 "Distance, " +
                                 "Fare, " +
                                 "NumOfPassengersAboard, " +
                                 "NumOfPassengersPickedUp, " +
                                 "FuelCost) " +
                                 "SELECT " +
                                 "LegId, " +
                                 "DriverID, " +
                                 "StartAddress, " +
                                 "PickupRequestTime, " +
                                 "StartTime, " +
                                 "DestinationAddress, " +
                                 "ArrivalTime, " +
                                 "Distance, " +
                                 "Fare, " +
                                 "NumOfPassengersAboard, " +
                                 "NumOfPassengersPickedUp, " +
                                 "FuelCost " +
                                 "FROM LegsTmp"
                                );
            migrationBuilder.DropTable("LegsTmp");


            migrationBuilder.CreateTable(
                name: "LegCoordinates",
                columns: table => new
                {
                    LegID = table.Column<int>(nullable: false),
                    StartLatitude = table.Column<decimal>(nullable: false),
                    StartLongitude = table.Column<decimal>(nullable: false),
                    DestLatitude = table.Column<decimal>(nullable: false),
                    DestLongitude = table.Column<decimal>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    DateModified = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LegCoordinates", x => x.LegID);
                    table.ForeignKey(
                        name: "FK_LegCoordinates_Legs_LegID",
                        column: x => x.LegID,
                        principalTable: "Legs",
                        principalColumn: "LegID",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LegCoordinates");

            /*migrationBuilder.AlterColumn<string>(
                name: "StartAddress",
                table: "Legs",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AlterColumn<string>(
                name: "DestinationAddress",
                table: "Legs",
                nullable: true,
                oldClrType: typeof(string));*/
            migrationBuilder.RenameTable("Legs", newName: "LegsTmp");
            migrationBuilder.CreateTable(
                name: "Legs",
                columns: table => new
                {
                    LegID = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DriverID = table.Column<int>(nullable: false),
                    StartAddress = table.Column<string>(nullable: true),
                    PickupRequestTime = table.Column<DateTime>(nullable: true),
                    StartTime = table.Column<DateTime>(nullable: false),
                    DestinationAddress = table.Column<string>(nullable: true),
                    ArrivalTime = table.Column<DateTime>(nullable: false),
                    Distance = table.Column<decimal>(nullable: false),
                    Fare = table.Column<decimal>(nullable: false),
                    NumOfPassengersAboard = table.Column<int>(nullable: false),
                    NumOfPassengersPickedUp = table.Column<int>(nullable: false),
                    FuelCost = table.Column<decimal>(nullable: false)
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
                }
            );
            migrationBuilder.Sql("INSERT INTO Legs(LegId, " +
                                 "DriverID, " +
                                 "StartAddress, " +
                                 "PickupRequestTime, " +
                                 "StartTime, " +
                                 "DestinationAddress, " +
                                 "ArrivalTime, " +
                                 "Distance, " +
                                 "Fare, " +
                                 "NumOfPassengersAboard, " +
                                 "NumOfPassengersPickedUp, " +
                                 "FuelCost) " +
                                 "SELECT " +
                                 "LegId, " +
                                 "DriverID, " +
                                 "StartAddress, " +
                                 "PickupRequestTime, " +
                                 "StartTime, " +
                                 "DestinationAddress, " +
                                 "ArrivalTime, " +
                                 "Distance, " +
                                 "Fare, " +
                                 "NumOfPassengersAboard, " +
                                 "NumOfPassengersPickedUp, " +
                                 "FuelCost " +
                                 "FROM LegsTmp"
                                );
            migrationBuilder.DropTable("LegsTmp");
        }
    }
}
