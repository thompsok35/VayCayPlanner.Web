using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VayCayPlanner.Data.Migrations
{
    public partial class Removedtravelgroupidreference : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Travelers_TravelGroups_TravelGroupId1",
                table: "Travelers");

            migrationBuilder.DropIndex(
                name: "IX_Travelers_TravelGroupId1",
                table: "Travelers");

            migrationBuilder.DropColumn(
                name: "TravelGroupId1",
                table: "Travelers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TravelGroupId1",
                table: "Travelers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Travelers_TravelGroupId1",
                table: "Travelers",
                column: "TravelGroupId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Travelers_TravelGroups_TravelGroupId1",
                table: "Travelers",
                column: "TravelGroupId1",
                principalTable: "TravelGroups",
                principalColumn: "Id");
        }
    }
}
