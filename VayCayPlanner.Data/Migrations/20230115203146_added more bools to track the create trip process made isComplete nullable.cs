using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VayCayPlanner.Data.Migrations
{
    public partial class addedmoreboolstotrackthecreatetripprocessmadeisCompletenullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Trips_TravelGroups_TravelGroupId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_TravelGroupId",
                table: "Trips");

            migrationBuilder.AddColumn<bool>(
                name: "isDestinationComplete",
                table: "NewTripTemplates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isTravelersComplete",
                table: "NewTripTemplates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "isTripComplete",
                table: "NewTripTemplates",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "isDestinationComplete",
                table: "NewTripTemplates");

            migrationBuilder.DropColumn(
                name: "isTravelersComplete",
                table: "NewTripTemplates");

            migrationBuilder.DropColumn(
                name: "isTripComplete",
                table: "NewTripTemplates");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TravelGroupId",
                table: "Trips",
                column: "TravelGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_TravelGroups_TravelGroupId",
                table: "Trips",
                column: "TravelGroupId",
                principalTable: "TravelGroups",
                principalColumn: "Id");
        }
    }
}
