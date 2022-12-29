using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VayCayPlanner.Data.Migrations
{
    public partial class changestotravelgroupandtraveler : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Travelers_TravelGroups_TravelGroupId",
                table: "Travelers");

            migrationBuilder.DropForeignKey(
                name: "FK_Travelers_Trips_TripId",
                table: "Travelers");

            migrationBuilder.DropIndex(
                name: "IX_Travelers_TravelGroupId",
                table: "Travelers");

            migrationBuilder.RenameColumn(
                name: "TripId",
                table: "Travelers",
                newName: "TravelGroupId1");

            migrationBuilder.RenameIndex(
                name: "IX_Travelers_TripId",
                table: "Travelers",
                newName: "IX_Travelers_TravelGroupId1");

            migrationBuilder.AlterColumn<string>(
                name: "TripName",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "TravelGroupId",
                table: "Trips",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OwnerId",
                table: "TravelGroups",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TypeId",
                table: "TravelGroups",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "TravelGroupId",
                table: "Travelers",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Travelers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Travelers",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_TravelGroupId",
                table: "Trips",
                column: "TravelGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Travelers_TravelGroups_TravelGroupId1",
                table: "Travelers",
                column: "TravelGroupId1",
                principalTable: "TravelGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_TravelGroups_TravelGroupId",
                table: "Trips",
                column: "TravelGroupId",
                principalTable: "TravelGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Travelers_TravelGroups_TravelGroupId1",
                table: "Travelers");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_TravelGroups_TravelGroupId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_TravelGroupId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "TravelGroups");

            migrationBuilder.DropColumn(
                name: "TypeId",
                table: "TravelGroups");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Travelers");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Travelers");

            migrationBuilder.RenameColumn(
                name: "TravelGroupId1",
                table: "Travelers",
                newName: "TripId");

            migrationBuilder.RenameIndex(
                name: "IX_Travelers_TravelGroupId1",
                table: "Travelers",
                newName: "IX_Travelers_TripId");

            migrationBuilder.AlterColumn<string>(
                name: "TripName",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "TravelGroupId",
                table: "Trips",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "TravelGroupId",
                table: "Travelers",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Travelers_TravelGroupId",
                table: "Travelers",
                column: "TravelGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Travelers_TravelGroups_TravelGroupId",
                table: "Travelers",
                column: "TravelGroupId",
                principalTable: "TravelGroups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Travelers_Trips_TripId",
                table: "Travelers",
                column: "TripId",
                principalTable: "Trips",
                principalColumn: "Id");
        }
    }
}
