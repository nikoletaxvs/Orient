using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Orient.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "checkbox_answears",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IsChecked = table.Column<bool>(type: "bit", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_checkbox_answears", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "unit1_Questions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    correctAnswear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    answear1 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    answear2 = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    answear3 = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_unit1_Questions", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "checkbox_answears");

            migrationBuilder.DropTable(
                name: "unit1_Questions");
        }
    }
}
