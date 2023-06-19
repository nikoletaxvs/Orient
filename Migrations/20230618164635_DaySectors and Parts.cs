using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orient.Migrations
{
    public partial class DaySectorsandParts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DaySectors",
                columns: table => new
                {
                    DaySectorId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Sector = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DaySectors", x => x.DaySectorId);
                });

            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    PartId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PartText = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartCareer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PartIcon = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DaySectorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.PartId);
                    table.ForeignKey(
                        name: "FK_Parts_DaySectors_DaySectorId",
                        column: x => x.DaySectorId,
                        principalTable: "DaySectors",
                        principalColumn: "DaySectorId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Parts_DaySectorId",
                table: "Parts",
                column: "DaySectorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "DaySectors");
        }
    }
}
