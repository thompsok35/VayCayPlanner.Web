using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VayCayPlanner.Data.Migrations
{
    public partial class updatedtheTravelerDestinationstable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailAddress",
                table: "TravelerDestinations");

            migrationBuilder.DropColumn(
                name: "FullName",
                table: "TravelerDestinations");

            migrationBuilder.AddColumn<int>(
                name: "TravelerId",
                table: "TravelerDestinations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "TripId",
                table: "TravelerDestinations",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TravelerId",
                table: "TravelerDestinations");

            migrationBuilder.DropColumn(
                name: "TripId",
                table: "TravelerDestinations");

            migrationBuilder.AddColumn<string>(
                name: "EmailAddress",
                table: "TravelerDestinations",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FullName",
                table: "TravelerDestinations",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true);
        }
    }
}
