using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VayCayPlanner.Data.Migrations
{
    public partial class addedtransportsandloadgings : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Lodgings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: true),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    CheckInDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CheckOutDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    MaxOccupancy = table.Column<int>(type: "int", nullable: true),
                    Nights = table.Column<int>(type: "int", nullable: false),
                    WebLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TravelGroupId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lodgings", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Transports",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    DepartureDestinationId = table.Column<int>(type: "int", nullable: true),
                    FromAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToAddress = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DepartureDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PreferredAirport = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ArrivalDestinationId = table.Column<int>(type: "int", nullable: false),
                    ArrivalDatetime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    TransportType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: true),
                    TravelGroupId = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transports", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lodgings");

            migrationBuilder.DropTable(
                name: "Transports");
        }
    }
}
