using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VayCayPlanner.Data.Migrations
{
    public partial class changedthenameofthedestinationtravelers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DestinationTravelers");

            migrationBuilder.CreateTable(
                name: "TravelerDestinations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    DestinationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TravelerDestinations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TravelerDestinations");

            migrationBuilder.CreateTable(
                name: "DestinationTravelers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DestinationId = table.Column<int>(type: "int", nullable: false),
                    EmailAddress = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    FullName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DestinationTravelers", x => x.Id);
                });
        }
    }
}
