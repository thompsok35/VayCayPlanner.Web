using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VayCayPlanner.Data.Migrations
{
    public partial class removedtheFKtotravelgroupfromsubscriber : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TravelGroups_AspNetUsers_SubscriberId",
                table: "TravelGroups");

            migrationBuilder.DropIndex(
                name: "IX_TravelGroups_SubscriberId",
                table: "TravelGroups");

            migrationBuilder.DropColumn(
                name: "SubscriberId",
                table: "TravelGroups");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SubscriberId",
                table: "TravelGroups",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TravelGroups_SubscriberId",
                table: "TravelGroups",
                column: "SubscriberId");

            migrationBuilder.AddForeignKey(
                name: "FK_TravelGroups_AspNetUsers_SubscriberId",
                table: "TravelGroups",
                column: "SubscriberId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
